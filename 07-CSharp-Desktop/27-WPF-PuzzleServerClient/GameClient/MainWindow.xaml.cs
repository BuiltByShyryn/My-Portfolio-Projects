using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameClient
{
    public partial class MainWindow : Window
    {
        private TcpClient playerVoice;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;

        private readonly List<PuzzleInfo> puzzles = new List<PuzzleInfo>();
        private int[] board;
        private int puzzleSize = 3;
        private int moveCount = 0;
        private string currentPuzzleKind = "SLIDE";
        private int expectedTapNumber = 1;
        private readonly Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            txtPassword.Password = "123";
            board = CreateSolvedBoard(3);
            RenderBoard();
            txtUploadTitle.Text = "My Puzzle";
            AddLog("First time: click Register. After that click Login.");
            AddLog("Sample password now is: 123");
            AddLog("Sliding puzzle: click a number next to the empty cell.");
            AddLog("Tap-order puzzle: click numbers from 1 upward.");
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (playerVoice != null && playerVoice.Connected)
            {
                try
                {
                    writer.WriteLine("DISCONNECT|");
                }
                catch { }
                DisconnectClientUi();
                return;
            }

            try
            {
                playerVoice = new TcpClient();
                playerVoice.Connect(txtIP.Text, int.Parse(txtPort.Text));

                NetworkStream stream = playerVoice.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                listenThread = new Thread(ListenForServer);
                listenThread.IsBackground = true;
                listenThread.Start();

                btnConnect.Content = "Disconnect";
                lblStatus.Text = "Status: connected";
                lblStatus.Foreground = Brushes.Green;
                AddLog("Connected to server");
            }
            catch (Exception ex)
            {
                AddLog("Connect error: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text.Trim() == "" || txtPassword.Password.Trim() == "" || txtFullName.Text.Trim() == "")
            {
                AddLog("Fill login, password and full name first");
                return;
            }

            SendCommand("REGISTER|" + txtLogin.Text + "|" + txtPassword.Password + "|" + txtFullName.Text);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text.Trim() == "" || txtPassword.Password.Trim() == "")
            {
                AddLog("Fill login and password first");
                return;
            }

            SendCommand("LOGIN|" + txtLogin.Text + "|" + txtPassword.Password);
        }

        private void btnGetPuzzles_Click(object sender, RoutedEventArgs e)
        {
            SendCommand("LIST|");
        }

        private void btnLoadPuzzle_Click(object sender, RoutedEventArgs e)
        {
            PuzzleInfo puzzle = lstPuzzles.SelectedItem as PuzzleInfo;
            if (puzzle == null)
            {
                AddLog("Select a puzzle from the list first");
                return;
            }

            SendCommand("GET|" + puzzle.Id);
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            if (currentPuzzleKind == "ORDER")
            {
                ShuffleTapOrderPuzzle();
                return;
            }

            if (board == null || board.Length == 0)
                board = CreateSolvedBoard(3);

            for (int i = 0; i < 50; i++)
            {
                int zeroIndex = Array.IndexOf(board, 0);
                List<int> neighbors = GetNeighbors(zeroIndex);
                int moveIndex = neighbors[rnd.Next(neighbors.Count)];
                Swap(zeroIndex, moveIndex);
            }

            moveCount = 0;
            UpdateMoveText();
            RenderBoard();
            AddLog("Puzzle shuffled");
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string title = txtUploadTitle.Text.Trim();
            if (title == "") title = "Uploaded Puzzle";
            SendCommand("ADD|" + title + "|" + puzzleSize + "|" + currentPuzzleKind + "|" + string.Join(",", board));
        }

        private void ListenForServer()
        {
            try
            {
                while (true)
                {
                    string msg = reader.ReadLine();
                    if (msg == null) break;

                    string[] tmp = msg.Split('|');
                    switch (tmp[0])
                    {
                        case "REGISTER":
                            Dispatcher.Invoke(() => AddLog(tmp[2]));
                            break;

                        case "LOGIN":
                            Dispatcher.Invoke(() =>
                            {
                                if (tmp[1] == "1")
                                {
                                    lblStatus.Text = "Status: logged in";
                                    lblStatus.Foreground = Brushes.DarkGreen;
                                    AddLog("Login successful");
                                    SendCommand("LIST|");
                                }
                                else
                                {
                                    AddLog(tmp[2]);
                                }
                            });
                            break;

                        case "LIST":
                            Dispatcher.Invoke(() => LoadPuzzleList(tmp));
                            break;

                        case "PUZZLE":
                            Dispatcher.Invoke(() => LoadPuzzle(tmp));
                            break;

                        case "ADD":
                            Dispatcher.Invoke(() =>
                            {
                                AddLog(tmp[2]);
                                SendCommand("LIST|");
                            });
                            break;

                        case "BUSY":
                        case "ERROR":
                            Dispatcher.Invoke(() => AddLog(tmp.Length > 1 ? tmp[1] : tmp[0]));
                            break;

                        case "BYE":
                            Dispatcher.Invoke(() => AddLog("Disconnected from server"));
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => AddLog("Listen error: " + ex.Message));
            }
            finally
            {
                Dispatcher.Invoke(DisconnectClientUi);
            }
        }

        private void LoadPuzzleList(string[] tmp)
        {
            puzzles.Clear();
            lstPuzzles.Items.Clear();

            if (tmp.Length < 2)
            {
                AddLog("Wrong list response");
                return;
            }

            for (int i = 2; i < tmp.Length; i++)
            {
                string[] item = tmp[i].Split('~');
                if (item.Length < 4) continue;

                int id;
                int size;
                if (!int.TryParse(item[0], out id)) continue;
                if (!int.TryParse(item[2], out size)) continue;

                PuzzleInfo puzzle = new PuzzleInfo
                {
                    Id = id,
                    Title = item[1],
                    Size = size,
                    Kind = item[3]
                };

                puzzles.Add(puzzle);
                lstPuzzles.Items.Add(puzzle);
            }

            if (puzzles.Count == 0)
                AddLog("No puzzles found on server");
            else
                AddLog("Puzzle list updated");
        }

        private void LoadPuzzle(string[] tmp)
        {
            if (tmp.Length < 6)
            {
                AddLog("Wrong puzzle data");
                return;
            }

            txtPuzzleTitle.Text = tmp[2];

            int size;
            if (!int.TryParse(tmp[3], out size)) size = 3;

            puzzleSize = size;
            currentPuzzleKind = tmp[4];
            board = tmp[5].Split(',').Select(int.Parse).ToArray();
            moveCount = 0;
            expectedTapNumber = 1;
            UpdateMoveText();
            txtUploadTitle.Text = tmp[2];
            RenderBoard();
            AddLog("Puzzle loaded: " + tmp[2]);
        }

        private void SendCommand(string cmd)
        {
            try
            {
                if (writer == null)
                {
                    AddLog("Connect to server first");
                    return;
                }

                writer.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                AddLog("Send error: " + ex.Message);
            }
        }

        private void RenderBoard()
        {
            grdPuzzle.Children.Clear();
            grdPuzzle.RowDefinitions.Clear();
            grdPuzzle.ColumnDefinitions.Clear();

            if (board == null || board.Length == 0) return;

            for (int i = 0; i < puzzleSize; i++)
            {
                grdPuzzle.RowDefinitions.Add(new RowDefinition());
                grdPuzzle.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < board.Length; i++)
            {
                Button btn = new Button();
                btn.Margin = new Thickness(4);
                btn.FontSize = 24;
                btn.Tag = i;
                btn.BorderThickness = new Thickness(2);
                btn.FontWeight = FontWeights.Bold;

                if (currentPuzzleKind == "ORDER")
                {
                    RenderOrderButton(btn, i);
                }
                else if (currentPuzzleKind == "TOGGLE") 
                {
                    RenderToggleButton(btn, i);
                }
                else
                {
                    RenderSlideButton(btn, i);
                }

                Grid.SetRow(btn, i / puzzleSize);
                Grid.SetColumn(btn, i % puzzleSize);
                grdPuzzle.Children.Add(btn);
            }
        }

        private void RenderOrderButton(Button btn, int index)
        {
            if (board[index] == 0)
            {
                btn.Content = "";
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 245, 157));
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(253, 216, 53));
                return;
            }

            btn.Content = board[index].ToString();
            btn.Background = new SolidColorBrush(Color.FromRgb(197, 225, 165));
            btn.BorderBrush = new SolidColorBrush(Color.FromRgb(156, 204, 101));
            btn.Foreground = new SolidColorBrush(Color.FromRgb(51, 105, 30));
            btn.Click += PuzzleButton_Click;
        }

        private void RenderSlideButton(Button btn, int index)
        {
            if (board[index] == 0)
            {
                btn.Content = "";
                btn.Background = new SolidColorBrush(Color.FromRgb(209, 196, 233));
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(149, 117, 205));
                return;
            }

            btn.Content = board[index].ToString();

            if (puzzleSize == 4)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(179, 229, 252));
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(79, 195, 247));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(1, 87, 155));
            }
            else if (board[index] % 2 == 0)
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 224, 178));
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 167, 38));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(93, 64, 55));
            }
            else
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(248, 187, 208));
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(240, 98, 146));
                btn.Foreground = new SolidColorBrush(Color.FromRgb(136, 14, 79));
            }

            btn.Click += PuzzleButton_Click;
        }

        private void PuzzleButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int clickIndex = (int)btn.Tag;

            if (currentPuzzleKind == "TOGGLE")
            {
                List<int> targets = GetNeighbors(clickIndex);
                targets.Add(clickIndex);

                foreach (int idx in targets)
                {
                    board[idx] = (board[idx] == 0) ? 1 : 0;
                }

                moveCount++;
                UpdateMoveText();
                RenderBoard();

                if (board.All(x => x == 0))
                {
                    AddLog("Toggle puzzle solved!");
                    MessageBox.Show("Victory! All lights are out.");
                }
                return;
            }

            if (currentPuzzleKind == "ORDER")
            {
                int clickedValue = board[clickIndex];
                if (clickedValue == expectedTapNumber)
                {
                    board[clickIndex] = 0;
                    expectedTapNumber++;
                    moveCount++;
                    UpdateMoveText();
                    RenderBoard();

                    if (board.All(x => x == 0))
                    {
                        AddLog("Tap-order puzzle solved in " + moveCount + " moves");
                        MessageBox.Show("You win!\nYou clicked all numbers in the correct order.");
                    }
                }
                else
                {
                    AddLog("Wrong number. Try number " + expectedTapNumber);
                }

                return;
            }

            int zeroIndex = Array.IndexOf(board, 0);
            if (GetNeighbors(zeroIndex).Contains(clickIndex))
            {
                Swap(clickIndex, zeroIndex);
                moveCount++;
                UpdateMoveText();
                RenderBoard();

                if (IsSolved())
                {
                    AddLog("Puzzle solved in " + moveCount + " moves");
                    MessageBox.Show("You win!\nPuzzle solved in " + moveCount + " moves.");
                }
            }
        }

        private void RenderToggleButton(Button btn, int index)
        {
            if (board[index] == 1)
            {
                btn.Content = "💡";
                btn.Background = new SolidColorBrush(Color.FromRgb(255, 235, 59));
            }
            else
            {
                btn.Content = "⚫";
                btn.Background = new SolidColorBrush(Color.FromRgb(189, 189, 189));
            }
            btn.Click += PuzzleButton_Click;
        }


        private List<int> GetNeighbors(int index)
        {
            List<int> neighbors = new List<int>();
            int row = index / puzzleSize;
            int col = index % puzzleSize;

            if (row > 0) neighbors.Add(index - puzzleSize);
            if (row < puzzleSize - 1) neighbors.Add(index + puzzleSize);
            if (col > 0) neighbors.Add(index - 1);
            if (col < puzzleSize - 1) neighbors.Add(index + 1);

            return neighbors;
        }

        private void Swap(int a, int b)
        {
            int temp = board[a];
            board[a] = board[b];
            board[b] = temp;
        }

        private bool IsSolved()
        {
            for (int i = 0; i < board.Length - 1; i++)
                if (board[i] != i + 1) return false;

            return board[board.Length - 1] == 0;
        }

        private int[] CreateSolvedBoard(int size)
        {
            int[] arr = new int[size * size];
            for (int i = 0; i < arr.Length - 1; i++)
                arr[i] = i + 1;

            arr[arr.Length - 1] = 0;
            moveCount = 0;
            expectedTapNumber = 1;
            currentPuzzleKind = "SLIDE";
            UpdateMoveText();
            txtPuzzleTitle.Text = "Local Puzzle";
            return arr;
        }

        private void ShuffleTapOrderPuzzle()
        {
            List<int> values = Enumerable.Range(1, puzzleSize * puzzleSize).ToList();
            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                int tmp = values[i];
                values[i] = values[j];
                values[j] = tmp;
            }

            board = values.ToArray();
            currentPuzzleKind = "ORDER";
            expectedTapNumber = 1;
            moveCount = 0;
            UpdateMoveText();
            RenderBoard();
            AddLog("Tap-order puzzle shuffled");
        }

        private void UpdateMoveText()
        {
            txtMoves.Text = "Moves: " + moveCount;
        }

        private void AddLog(string msg)
        {
            txtLog.AppendText(msg + Environment.NewLine);
            txtLog.ScrollToEnd();
        }

        private void DisconnectClientUi()
        {
            try { if (reader != null) reader.Close(); } catch { }
            try { if (writer != null) writer.Close(); } catch { }
            try
            {
                if (playerVoice != null)
                {
                    playerVoice.Close();
                    playerVoice = null;
                }
            }
            catch { }

            reader = null;
            writer = null;

            btnConnect.Content = "Connect";
            lblStatus.Text = "Status: not connected";
            lblStatus.Foreground = Brushes.DarkRed;
        }

        protected override void OnClosed(EventArgs e)
        {
            try { if (writer != null) writer.WriteLine("DISCONNECT|"); } catch { }
            try { if (playerVoice != null) playerVoice.Close(); } catch { }
            base.OnClosed(e);
        }

        public class PuzzleInfo
        {
            public int Id;
            public string Title;
            public int Size;
            public string Kind;

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
