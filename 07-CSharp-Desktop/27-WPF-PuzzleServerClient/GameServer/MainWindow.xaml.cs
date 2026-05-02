using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;

namespace GameServer
{
    public partial class MainWindow : Window
    {
        public string CompName = "";
        public TcpListener SrvSocket = null;
        public bool isStopedServer = false;

        public List<ClientParam> activeClients = new List<ClientParam>();
        public Mutex mtxLstClients = new Mutex(false);

        private readonly List<UserInfo> users = new List<UserInfo>();
        private readonly List<PuzzleInfo> puzzles = new List<PuzzleInfo>();
        private readonly object usersLock = new object();
        private readonly object puzzlesLock = new object();

        private readonly string usersFile;
        private readonly string puzzlesFile;
        private int maxClients = 10;

        public MainWindow()
        {
            InitializeComponent();

            usersFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.txt");
            puzzlesFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "puzzles.txt");

            CompName = Dns.GetHostName();
            this.Title = "Puzzle Server 413: " + CompName;

            IPAddress[] ipList = Dns.GetHostAddresses(CompName);
            cbIpList.Items.Clear();
            cbIpList.Items.Add(IPAddress.Any.ToString());
            cbIpList.Items.Add(IPAddress.Loopback.ToString());

            foreach (IPAddress ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    cbIpList.Items.Add(ip.ToString());
            }

            cbIpList.SelectedIndex = 0;
            btnStartStop.Tag = false;

            LoadUsers();
            LoadPuzzles();
            UpdatePuzzleList();
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)btnStartStop.Tag == false)
            {
                try
                {
                    isStopedServer = false;
                    string ip = cbIpList.Text;
                    int port = int.Parse(txtPort.Text);
                    int.TryParse(txtMaxClients.Text, out maxClients);
                    if (maxClients <= 0) maxClients = 10;

                    if (ip == IPAddress.Any.ToString())
                        SrvSocket = new TcpListener(IPAddress.Any, port);
                    else
                        SrvSocket = new TcpListener(IPAddress.Parse(ip), port);

                    SrvSocket.Start(100);
                    ThreadPool.QueueUserWorkItem(ServerThreadProc, this);

                    Thread cleanerThread = new Thread(CleanupClientsProc);
                    cleanerThread.IsBackground = true;
                    cleanerThread.Start();

                    btnStartStop.Content = "Stop";
                    btnStartStop.Tag = true;
                    cbIpList.IsEnabled = false;
                    txtPort.IsEnabled = false;
                    txtMaxClients.IsEnabled = false;

                    MsgToLog("Server started\r\n");
                }
                catch (Exception ex)
                {
                    MsgToLog("Start error: " + ex.Message + "\r\n");
                }
            }
            else
            {
                StopServer();
            }
        }

        private void StopServer()
        {
            try
            {
                isStopedServer = true;

                if (SrvSocket != null)
                {
                    SrvSocket.Stop();
                    SrvSocket = null;
                }

                mtxLstClients.WaitOne();
                foreach (ClientParam client in activeClients.ToList())
                {
                    try { client.client.Close(); } catch { }
                }
                activeClients.Clear();
                mtxLstClients.ReleaseMutex();

                UpdateClientListView();

                btnStartStop.Content = "Start";
                btnStartStop.Tag = false;
                cbIpList.IsEnabled = true;
                txtPort.IsEnabled = true;
                txtMaxClients.IsEnabled = true;

                MsgToLog("Server stopped\r\n");
            }
            catch (Exception ex)
            {
                MsgToLog("Stop error: " + ex.Message + "\r\n");
            }
        }

        public void MsgToLog(string msg)
        {
            Dispatcher.Invoke(() =>
            {
                txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + msg);
                txtLog.ScrollToEnd();
            });
        }

        public void ServerThreadProc(object par)
        {
            MainWindow form = (MainWindow)par;

            while (!form.isStopedServer)
            {
                try
                {
                    TcpClient client = form.SrvSocket.AcceptTcpClient();
                    if (client == null) continue;

                    mtxLstClients.WaitOne();
                    int clientCount = activeClients.Count;
                    mtxLstClients.ReleaseMutex();

                    if (clientCount >= maxClients)
                    {
                        using (NetworkStream ns = client.GetStream())
                        using (StreamWriter wr = new StreamWriter(ns, Encoding.UTF8) { AutoFlush = true })
                        {
                            wr.WriteLine("BUSY|Server is full");
                        }
                        client.Close();
                        continue;
                    }

                    ClientParam clientParam = new ClientParam
                    {
                        client = client,
                        form = form,
                        NickName = "Guest",
                        LastActive = DateTime.Now
                    };

                    mtxLstClients.WaitOne();
                    activeClients.Add(clientParam);
                    mtxLstClients.ReleaseMutex();

                    UpdateClientListView();
                    MsgToLog("Client connected: " + client.Client.RemoteEndPoint + "\r\n");

                    ThreadPool.QueueUserWorkItem(ClientThreadProc, clientParam);
                }
                catch (Exception ex)
                {
                    if (!form.isStopedServer)
                        MsgToLog("Accept error: " + ex.Message + "\r\n");
                }
            }
        }

        public void ClientThreadProc(object param)
        {
            ClientParam par = (ClientParam)param;

            try
            {
                using (NetworkStream stream = par.client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                {
                    while (!isStopedServer)
                    {
                        string cmd = reader.ReadLine();
                        if (cmd == null) break;

                        par.LastActive = DateTime.Now;

                        string[] tmp = cmd.Split('|');
                        string name = tmp[0];
                        MsgToLog(GetLogText(name, tmp, par) + "\r\n");

                        switch (name)
                        {
                            case "REGISTER":
                                HandleRegister(tmp, writer);
                                break;
                            case "LOGIN":
                                HandleLogin(tmp, writer, par);
                                break;
                            case "LIST":
                                HandleList(writer, par);
                                break;
                            case "GET":
                                HandleGetPuzzle(tmp, writer, par);
                                break;
                            case "ADD":
                                HandleAddPuzzle(tmp, writer, par);
                                break;
                            case "DISCONNECT":
                                writer.WriteLine("BYE|OK");
                                return;
                            default:
                                writer.WriteLine("ERROR|Unknown command");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgToLog("Client error: " + ex.Message + "\r\n");
            }
            finally
            {
                try { par.client.Close(); } catch { }

                mtxLstClients.WaitOne();
                activeClients.Remove(par);
                mtxLstClients.ReleaseMutex();

                UpdateClientListView();
                MsgToLog("Client disconnected\r\n");
            }
        }

        private void HandleRegister(string[] tmp, StreamWriter writer)
        {
            if (tmp.Length < 4)
            {
                writer.WriteLine("REGISTER|0|Wrong data");
                return;
            }

            string login = tmp[1];
            string password = tmp[2];
            string fullName = tmp[3];

            lock (usersLock)
            {
                if (users.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
                {
                    writer.WriteLine("REGISTER|0|Login already exists");
                    return;
                }

                users.Add(new UserInfo
                {
                    Login = login,
                    PasswordHash = MakeHash(password),
                    FullName = fullName
                });

                SaveUsers();
            }

            writer.WriteLine("REGISTER|1|User created");
            MsgToLog("New user: " + login + "\r\n");
        }

        private void HandleLogin(string[] tmp, StreamWriter writer, ClientParam par)
        {
            if (tmp.Length < 3)
            {
                writer.WriteLine("LOGIN|0|Wrong data");
                return;
            }

            string login = tmp[1];
            string passwordHash = MakeHash(tmp[2]);

            lock (usersLock)
            {
                UserInfo user = users.FirstOrDefault(u =>
                    u.Login.Equals(login, StringComparison.OrdinalIgnoreCase) &&
                    u.PasswordHash == passwordHash);

                if (user == null)
                {
                    writer.WriteLine("LOGIN|0|Wrong login or password");
                    return;
                }

                par.NickName = user.Login;
            }

            UpdateClientListView();
            writer.WriteLine("LOGIN|1|Welcome");
            MsgToLog("User login: " + login + "\r\n");
        }

        private void HandleList(StreamWriter writer, ClientParam par)
        {
            if (par.NickName == "Guest")
            {
                writer.WriteLine("LIST|0");
                return;
            }

            lock (puzzlesLock)
            {
                List<string> parts = new List<string>();
                parts.Add("LIST");
                parts.Add(puzzles.Count.ToString());

                foreach (PuzzleInfo puzzle in puzzles)
                    parts.Add(puzzle.Id + "~" + puzzle.Title + "~" + puzzle.Size + "~" + puzzle.Kind);

                writer.WriteLine(string.Join("|", parts));
            }
        }

        private void HandleGetPuzzle(string[] tmp, StreamWriter writer, ClientParam par)
        {
            if (par.NickName == "Guest")
            {
                writer.WriteLine("ERROR|Login first");
                return;
            }

            if (tmp.Length < 2)
            {
                writer.WriteLine("ERROR|Wrong puzzle id");
                return;
            }

            int puzzleId;
            if (!int.TryParse(tmp[1], out puzzleId))
            {
                writer.WriteLine("ERROR|Wrong puzzle id");
                return;
            }

            lock (puzzlesLock)
            {
                PuzzleInfo puzzle = puzzles.FirstOrDefault(p => p.Id == puzzleId);
                if (puzzle == null)
                {
                    writer.WriteLine("ERROR|Puzzle not found");
                    return;
                }

                writer.WriteLine("PUZZLE|" + puzzle.Id + "|" + puzzle.Title + "|" + puzzle.Size + "|" + puzzle.Kind + "|" + puzzle.Cells);
            }
        }

        private void HandleAddPuzzle(string[] tmp, StreamWriter writer, ClientParam par)
        {
            if (par.NickName == "Guest")
            {
                writer.WriteLine("ADD|0|Login first");
                return;
            }

            if (tmp.Length < 5)
            {
                writer.WriteLine("ADD|0|Wrong data");
                return;
            }

            int size;
            if (!int.TryParse(tmp[2], out size))
            {
                writer.WriteLine("ADD|0|Wrong size");
                return;
            }

            lock (puzzlesLock)
            {
                int nextId = puzzles.Count == 0 ? 1 : puzzles.Max(p => p.Id) + 1;
                puzzles.Add(new PuzzleInfo
                {
                    Id = nextId,
                    Title = tmp[1],
                    Size = size,
                    Kind = tmp[3],
                    Cells = tmp[4]
                });

                SavePuzzles();
            }

            UpdatePuzzleList();
            writer.WriteLine("ADD|1|Puzzle saved");
            MsgToLog("Puzzle added by " + par.NickName + ": " + tmp[1] + "\r\n");
        }

        private void CleanupClientsProc()
        {
            while (!isStopedServer)
            {
                Thread.Sleep(60000);

                List<ClientParam> toRemove = new List<ClientParam>();

                mtxLstClients.WaitOne();
                foreach (ClientParam client in activeClients)
                {
                    if ((DateTime.Now - client.LastActive).TotalMinutes >= 10)
                        toRemove.Add(client);
                }
                mtxLstClients.ReleaseMutex();

                foreach (ClientParam client in toRemove)
                {
                    try
                    {
                        client.client.Close();
                        MsgToLog("Inactive client removed: " + client.NickName + "\r\n");
                    }
                    catch { }
                }
            }
        }

        private void LoadUsers()
        {
            lock (usersLock)
            {
                users.Clear();
                if (!File.Exists(usersFile)) return;

                foreach (string line in File.ReadAllLines(usersFile))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] tmp = line.Split('|');
                    if (tmp.Length < 3) continue;

                    users.Add(new UserInfo
                    {
                        Login = tmp[0],
                        PasswordHash = tmp[1],
                        FullName = tmp[2]
                    });
                }
            }
        }

        private void SaveUsers()
        {
            List<string> lines = new List<string>();
            foreach (UserInfo user in users)
                lines.Add(user.Login + "|" + user.PasswordHash + "|" + user.FullName);

            File.WriteAllLines(usersFile, lines, Encoding.UTF8);
        }

        private void LoadPuzzles()
        {
            lock (puzzlesLock)
            {
                puzzles.Clear();

                if (!File.Exists(puzzlesFile))
                {
                    AddDefaultPuzzles();
                    SavePuzzles();
                    return;
                }

                foreach (string line in File.ReadAllLines(puzzlesFile))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] tmp = line.Split('|');
                    if (tmp.Length < 4) continue;

                    int id;
                    int size;
                    if (!int.TryParse(tmp[0], out id)) continue;
                    if (!int.TryParse(tmp[2], out size)) continue;

                    string kind = "SLIDE";
                    string cells;

                    if (tmp.Length >= 5)
                    {
                        kind = tmp[3];
                        cells = tmp[4];
                    }
                    else
                    {
                        cells = tmp[3];
                    }

                    puzzles.Add(new PuzzleInfo
                    {
                        Id = id,
                        Title = tmp[1],
                        Size = size,
                        Kind = kind,
                        Cells = cells
                    });
                }

                EnsureMissingDefaultPuzzles();
            }
        }

        private void SavePuzzles()
        {
            List<string> lines = new List<string>();
            foreach (PuzzleInfo puzzle in puzzles)
                lines.Add(puzzle.Id + "|" + puzzle.Title + "|" + puzzle.Size + "|" + puzzle.Kind + "|" + puzzle.Cells);

            File.WriteAllLines(puzzlesFile, lines, Encoding.UTF8);
        }

        private void UpdateClientListView()
        {
            Dispatcher.Invoke(() =>
            {
                lstClients.Items.Clear();
                foreach (ClientParam client in activeClients)
                    lstClients.Items.Add(client.NickName);
            });
        }

        private void UpdatePuzzleList()
        {
            Dispatcher.Invoke(() =>
            {
                lstPuzzles.Items.Clear();
                foreach (PuzzleInfo puzzle in puzzles)
                    lstPuzzles.Items.Add(puzzle);
            });
        }

        private string MakeHash(string password)
        {
            return password;
        }

        private void btnSaveLog_Click(object sender, RoutedEventArgs e)
        {
            string fileName = Path.Combine(GetProjectFolder(), "server_log.txt");
            File.WriteAllText(fileName, txtLog.Text, Encoding.UTF8);
            MsgToLog("Log saved to file\r\n");
        }

        private void btnClearLog_Click(object sender, RoutedEventArgs e)
        {
            txtLog.Clear();
        }

        private void btnDeletePuzzle_Click(object sender, RoutedEventArgs e)
        {
            PuzzleInfo puzzle = lstPuzzles.SelectedItem as PuzzleInfo;
            if (puzzle == null)
            {
                MsgToLog("Select a puzzle first\r\n");
                return;
            }

            lock (puzzlesLock)
            {
                PuzzleInfo target = puzzles.FirstOrDefault(p => p.Id == puzzle.Id);
                if (target != null)
                {
                    puzzles.Remove(target);
                    SavePuzzles();
                }
            }

            UpdatePuzzleList();
            MsgToLog("Puzzle deleted: " + puzzle.Title + "\r\n");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            StopServer();
            base.OnClosed(e);
        }

        private string GetProjectFolder()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
        }

        private string GetLogText(string cmd, string[] tmp, ClientParam par)
        {
            switch (cmd)
            {
                case "REGISTER":
                    return "Register request from " + SafePart(tmp, 1);
                case "LOGIN":
                    return "Login request from " + SafePart(tmp, 1);
                case "LIST":
                    return "Puzzle list requested by " + par.NickName;
                case "GET":
                    return "Puzzle download requested: id " + SafePart(tmp, 1);
                case "ADD":
                    return "Puzzle upload requested: " + SafePart(tmp, 1);
                case "DISCONNECT":
                    return "Client asked to disconnect";
                default:
                    return "Command received: " + cmd;
            }
        }

        private string SafePart(string[] tmp, int index)
        {
            if (tmp.Length > index) return tmp[index];
            return "";
        }

        private void AddDefaultPuzzles()
        {
            puzzles.Add(new PuzzleInfo { Id = 1, Title = "Sunny Bunny Warmup", Size = 3, Kind = "SLIDE", Cells = "1,2,3,4,5,6,7,0,8" });
            puzzles.Add(new PuzzleInfo { Id = 2, Title = "Sleepy Cat Steps", Size = 3, Kind = "SLIDE", Cells = "1,2,3,5,0,6,4,7,8" });
            puzzles.Add(new PuzzleInfo { Id = 3, Title = "Moonlight Mix-Up", Size = 3, Kind = "SLIDE", Cells = "8,6,7,2,5,4,3,0,1" });
            puzzles.Add(new PuzzleInfo { Id = 4, Title = "Candy Heart Puzzle", Size = 3, Kind = "SLIDE", Cells = "1,3,6,4,0,2,7,5,8" });
            puzzles.Add(new PuzzleInfo { Id = 5, Title = "Chaos Bunny Puzzle", Size = 3, Kind = "SLIDE", Cells = "2,8,3,1,6,4,7,0,5" });
            puzzles.Add(new PuzzleInfo { Id = 6, Title = "Star Garden 4x4", Size = 4, Kind = "SLIDE", Cells = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,0,15" });
            puzzles.Add(new PuzzleInfo { Id = 7, Title = "Dreamy Room 4x4", Size = 4, Kind = "SLIDE", Cells = "1,2,3,4,5,6,7,8,9,10,11,12,13,0,14,15" });
            puzzles.Add(new PuzzleInfo { Id = 8, Title = "Starlight Tap Dance", Size = 3, Kind = "ORDER", Cells = "3,1,4,6,2,5,7,8,9" });
            puzzles.Add(new PuzzleInfo { Id = 9, Title = "Sparkle Sequence", Size = 3, Kind = "ORDER", Cells = "9,1,8,2,7,3,6,4,5" });
            puzzles.Add(new PuzzleInfo { Id = 10, Title = "Twinkling Memory", Size = 3, Kind = "ORDER", Cells = "5,4,9,1,7,2,8,3,6" });
            puzzles.Add(new PuzzleInfo { Id = 11, Title = "Cosmic Click", Size = 4, Kind = "ORDER", Cells = "3,1,15,2,5,16,13,4,12,8,10,6,11,7,9,14" });
            puzzles.Add(new PuzzleInfo { Id = 12, Title = "Midnight Lanterns", Size = 3, Kind = "TOGGLE", Cells = "0,1,0,1,1,1,0,1,0" });
            puzzles.Add(new PuzzleInfo { Id = 13, Title = "Deep Forest Lanterns", Size = 5, Kind = "TOGGLE", Cells = "0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0" });
            puzzles.Add(new PuzzleInfo { Id = 14, Title = "Grand Master Lanterns", Size = 7, Kind = "TOGGLE", Cells = "1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1" });

        }

        private void EnsureMissingDefaultPuzzles()
        {
            AddDefaultPuzzleIfMissing("Sunny Bunny Warmup", 3, "SLIDE", "1,2,3,4,5,6,7,0,8");
            AddDefaultPuzzleIfMissing("Sleepy Cat Steps", 3, "SLIDE", "1,2,3,5,0,6,4,7,8");
            AddDefaultPuzzleIfMissing("Moonlight Mix-Up", 3, "SLIDE", "8,6,7,2,5,4,3,0,1");
            AddDefaultPuzzleIfMissing("Candy Heart Puzzle", 3, "SLIDE", "1,3,6,4,0,2,7,5,8");
            AddDefaultPuzzleIfMissing("Chaos Bunny Puzzle", 3, "SLIDE", "2,8,3,1,6,4,7,0,5");
            AddDefaultPuzzleIfMissing("Star Garden 4x4", 4, "SLIDE", "1,2,3,4,5,6,7,8,9,10,11,12,13,14,0,15");
            AddDefaultPuzzleIfMissing("Dreamy Room 4x4", 4, "SLIDE", "1,2,3,4,5,6,7,8,9,10,11,12,13,0,14,15");
            AddDefaultPuzzleIfMissing("Starlight Tap Dance", 3, "ORDER", "3,1,4,6,2,5,7,8,9");
            AddDefaultPuzzleIfMissing("Sparkle Sequence", 3, "ORDER", "9,1,8,2,7,3,6,4,5");
            AddDefaultPuzzleIfMissing("Twinkling Memory", 3, "ORDER", "5,4,9,1,7,2,8,3,6");
            AddDefaultPuzzleIfMissing("Cosmic Click", 4, "ORDER", "3,1,15,2,5,16,13,4,12,8,10,6,11,7,9,14");
            AddDefaultPuzzleIfMissing("Midnight Lanterns", 3, "TOGGLE", "0,1,0,1,1,1,0,1,0");
            AddDefaultPuzzleIfMissing("Deep Forest Lanterns", 5, "TOGGLE", "0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0");
            AddDefaultPuzzleIfMissing("Grand Master Lanterns", 7, "TOGGLE", "1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1");
            SavePuzzles();
        }

        private void AddDefaultPuzzleIfMissing(string title, int size, string kind, string cells)
        {
            if (puzzles.Any(p => p.Title == title)) return;

            int nextId = puzzles.Count == 0 ? 1 : puzzles.Max(p => p.Id) + 1;
            puzzles.Add(new PuzzleInfo
            {
                Id = nextId,
                Title = title,
                Size = size,
                Kind = kind,
                Cells = cells
            });
        }

        public class ClientParam
        {
            public string NickName;
            public TcpClient client;
            public MainWindow form;
            public DateTime LastActive;
        }

        public class UserInfo
        {
            public string Login;
            public string PasswordHash;
            public string FullName;
        }

        public class PuzzleInfo
        {
            public int Id;
            public string Title;
            public int Size;
            public string Kind;
            public string Cells;

            public override string ToString()
            {
                string label;
              
                string cleanKind = Kind.Trim().ToUpper();

                if (cleanKind == "ORDER")
                {
                    label = "tap-order";
                }
                else if (cleanKind == "TOGGLE")
                {
                    label = "lanterns"; 
                }
                else
                {
                    label = "sliding";
                }

                return Id + ". " + Title + " (" + Size + "x" + Size + ", " + label + ")";
            }
        }
    }
}
