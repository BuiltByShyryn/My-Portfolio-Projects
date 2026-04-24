using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace MsgServer413
{
  public partial class FormMsgServer : Form
  {
    public FormMsgServer()
    {
      InitializeComponent();

      ThreadPool.SetMinThreads(20, 20);

      // получить имя машины
      this.Text = "Server: " + CompName + " (stoped)";

      // Заполение списка IP-адресов в cbIpList

      // порт по умолчанию
      txtPort.Text = "12345";

      // true  - сервер запущен "Sto&p"  Alt+P
      // false - сервер не запущен "&Start"  Alt+S
      btnStartStop.Tag = false; 

    } // public FormMsgServer()


    // Глобальные переменные программы сервер
    public string CompName  = "тут должно быть имя машины";
    public Socket SrvSocket = null; // рабочий сокет сервера
    public IPEndPoint SrvEP = null; // конечная точка активного сервера
    public bool   isStopedServer = false; // false - сигнал для останова сервера

    // список активных клиентов
    public List<ClientParam> lstClients = new List<ClientParam>();

    // объект мьютекс для защиты lstClients в потоках
    public Mutex mtxLstClients = new Mutex(false);


    public List<string> quotes = new List<string> {
    "The way to get started is to quit talking and begin doing.",
    "Don't cry because it's over, smile because it happened.",
    "Everything you can imagine is real.",
    "Do what you can, with what you have, where you are."
};
    private int maxClients = 5; // Task 3
    private string secretPass = "12345"; // Task 4

   private void btnStartStop_Click(object sender, EventArgs e)
    {
      if( (bool)btnStartStop.Tag is false )
      { // сервер не запущен "Start" - запускаем сервер
        try
        {
          isStopedServer = false; // сервер работает
                    int port = int.Parse(txtPort.Text);
                    SrvEP = new IPEndPoint(IPAddress.Any, port);// 1) создать End Point/Конечная Точка сервера
                    SrvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);// 2) создать сокет сервера
                    SrvSocket.Bind(SrvEP);// 3) привязать адрес
                    SrvSocket.Listen(10);// 4) включить прослушивание сокета - публикация сервера
                    Thread srvThread = new Thread(ServerThreadProc);
                    srvThread.IsBackground = true;
                    srvThread.Start();// 5) запустить поток сервера

                    MsgToLog($"\r\n[!] Server started on port {port}...");
                   

                    btnStartStop.Text = "Sto&p";
          btnStartStop.Tag = true;
                   
        }
        catch (Exception ex)
        {
          txtLog.Text += "Ошибка запуска сервера: " + ex.Message
            + "\r\n"; // - для перехода на новую строку
        }
      }
      else // if( (bool)btnStartStop.Tag is true)
      { // сервер запущен - останавливаем сервер
        try
        {
          isStopedServer = true; // сигнал, что сервер должен остановиться
          SrvSocket.Close(); // принудительное закрытие сокета
          SrvSocket = null;  // обнуление переменной
          btnStartStop.Text = "&Start";
          btnStartStop.Tag  = false;

        } catch(Exception ex)
        {
          txtLog.Text += "Ошибка останова сервера: " + ex.Message
            + "\r\n"; // - для перехода на новую строку
        }
      }
            

    } // btnStartStop_Click()

    // метод для записи в окно сообщений
    public void MsgToLog( string msg )
    {
      if (this.InvokeRequired)
        this.Invoke(new Action(() => { txtLog.Text += msg; }));
      else
        txtLog.Text += msg;
    }

        // процедура потока сервера
        public void ServerThreadProc(object par)
        {
            while (!isStopedServer)
            {
                try
                {
                   
                    Socket clientSocket = SrvSocket.Accept();

                    mtxLstClients.WaitOne();
                   
                    if (lstClients.Count >= maxClients)
                    {
                        byte[] msg = Encoding.UTF8.GetBytes("SERVER_BUSY: Max capacity reached. Try later.");
                        clientSocket.Send(msg);
                        clientSocket.Close();
                        mtxLstClients.ReleaseMutex();
                        continue;
                    }

                    
                    ClientParam cp = new ClientParam
                    {
                        ClientSocket = clientSocket,
                        ConnectTime = DateTime.Now
                    };

                   
                    lstClients.Add(cp);
                    mtxLstClients.ReleaseMutex();

                    
                    ThreadPool.QueueUserWorkItem(ClientThreadProc, cp);
                }
                catch { if (isStopedServer) break; }
            }
        }

        // Ф-ция потока работы с отдельным удаленным клиентом
        public void ClientThreadProc(object par)
        {
            ClientParam cp = (ClientParam)par;
            Socket s = cp.ClientSocket;
            int quotesSent = 0;

            try
            {

                byte[] buffer = new byte[1024];
                int bytes = s.Receive(buffer); 
                string auth = Encoding.UTF8.GetString(buffer, 0, bytes);
                s.Send(Encoding.UTF8.GetBytes("AUTH_OK"));

                if (!auth.Contains(":" + secretPass))
                {
                    s.Send(Encoding.UTF8.GetBytes("AUTH_FAILED"));
                    s.Close(); return;
                }

                MsgToLog($"\r\n[+] {auth.Split(':')[0]} joined at {cp.ConnectTime}");

              
                while (quotesSent < 5) 
                {
                    
                    bytes = s.Receive(buffer);
                    if (bytes == 0) break;

                    Random r = new Random();
                    string q = quotes[r.Next(quotes.Count)];
                    s.Send(Encoding.UTF8.GetBytes(q));
                    quotesSent++;
                }

                s.Send(Encoding.UTF8.GetBytes("LIMIT_REACHED: Goodbye!"));
            }
            catch { }
            finally
            {
                mtxLstClients.WaitOne();
                lstClients.Remove(cp);
                mtxLstClients.ReleaseMutex();
                MsgToLog($"\r\n[-] Client disconnected at {DateTime.Now}");
                s.Close();
            }
        }
        // обновление списка
        void UpdateClientListView()
    {
    } // void UpdateClientListView()

        // класс параметров для клиентского потока
        public class ClientParam
        {
            public Socket ClientSocket;
            public DateTime ConnectTime;
        } // class ClientParam

    } // class FormMsgServer;
} // namespace MsgServer413
