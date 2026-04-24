using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace WpfAppSemaphore
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      // настройка пула потоков
      ThreadPool.SetMinThreads(10, 10);
    }

    // Объект событие для синхронного запуска всех потоков
   // ManualResetEvent evntAllStart = new ManualResetEvent(false);
   AutoResetEvent evntAllStart = new AutoResetEvent(false);
        // Объект семафор для регуляции кол-ва активных потоков
        Semaphore semAll = null;

    // списки строк инф. о потоках для вывода в ListBox-списки окна
    List<string> lstAll     = new List<string>();
    List<string> lstActive  = new List<string>();
    List<string> lstDead    = new List<string>();

    // Объекты мьютекс для блокирования множественного доступа к глобальным спискам
    Mutex mtxLstAll    = new Mutex();
    Mutex mtxLstActive = new Mutex();
    Mutex mtxLstDead   = new Mutex();

    int cntAll  = 0; // кол-во всех потоков
    int cntWork = 0; // кол-во активных потоков

    // Создать все потоки
    private void Button_Click( object sender, RoutedEventArgs e )
    {
      // 1) сброс события evntAllStart
      evntAllStart.Reset(); // evntAllStart ==> false

      // 2) запуск всех потоков в пуле потоков
      try
      {
        cntAll = int.Parse( txtAll.Text );
        ThreadPool.SetMinThreads( cntAll, cntAll ); // настройка пула потоков
        for (int i = 0; i < cntAll; i++)
        {
          ThreadPool.QueueUserWorkItem( ThreadProc, this );
        }
      }
      catch (Exception ex )
      {
        MessageBox.Show( ex.Message, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error );
      }
    } // void Button_Click(object sender, RoutedEventArgs e);

    // Запустить потоки
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      try
      {
        // 1) создание и иниц-ция семафора
        cntWork = int.Parse(txtActive.Text);
        semAll  = new Semaphore( cntWork, cntWork );

        // 2) установка события evntAllStart в сигнальное
        //   состояния для ожидающих потоков
        evntAllStart.Set();  // evntAllStart ==> true
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
      }
    } // void Button_Click_1(object sender, RoutedEventArgs e);

    // функция потока
    void ThreadProc(object obj)
    {
      //MainWindow form = (MainWindow)obj;
      MainWindow form = obj as MainWindow;

      int id = Thread.CurrentThread.ManagedThreadId;
      // ГСЧ для иммитации работы потока
      int seed = (int)DateTime.Now.Ticks * id; // иниц-ция ГПСЧ, зерно для иниц-ции генератора псевдослучайных чисел
      Random rnd = new Random( seed );

      int work_time = rnd.Next( 1000, 5000 ); // случ. время работы в мсек
      string title = $"{id}: work time={work_time} ms";

      // 1) добавить поток в список потоков lstAll
      mtxLstAll.WaitOne(); // блокировка множественного доступа при модификации объекта lstAll
        lstAll.Add( title ); // модификация объекта lstAll
      mtxLstAll.ReleaseMutex(); // снятие блокировки
      // 1.1) обновить списки/ListBox в окне
      UpdateListBox();

      // 2) ожидание события для старта работы
      evntAllStart.WaitOne();

      // 3) получить слот/"билет" семафора semAll
      semAll.WaitOne();

      // 4) перенос потока из списка lstAll в список lstActive
      mtxLstAll.WaitOne();
        lstAll.Remove( title );
      mtxLstAll.ReleaseMutex();
      lock (lstActive)
      {
        lstActive.Add( title );
      }
      // 4.1) обновить списки/ListBox в окне
      UpdateListBox();

      // 5) поток что-то делает, иммитация работы потока
      Thread.Sleep( work_time );

      // 6) перенос потока из списка lstActive в список lstDead
      // спин-лок блокировка, spin-lock
      lock (lstActive)
      {
        lstActive.Remove( title );
      }
      Monitor.Enter( lstDead );
        lstDead.Add( title );
      Monitor.Exit( lstDead );
      // 6.1) обновить списки/ListBox в окне
      UpdateListBox();

      // 7) освобождаем слот семафора
      semAll.Release();

    } // void ThreadProc(object obj)

    // обновить списки/ListBox в окне
    void UpdateListBox()
    {
            Dispatcher.Invoke(delegate
            {

                mtxLstAll.WaitOne(); { 
                lbxAll.Items.Clear();
                foreach (var item in lstAll){lbxAll.Items.Add(item);}
                }

                mtxLstAll.ReleaseMutex();
                lock (lbxActive) { 
                lbxActive.Items.Clear();
                foreach(var item in lstActive) { lbxActive.Items.Add(item); }
                }


                Monitor.Enter(lstDead); { 
                lbxDead.Items.Clear();
                foreach(var item in lstDead) { lbxDead.Items.Add(item); };

                }
                //lbxAll.InvalidateVisual();
                // lbxAll.UpdateLayout();
                Monitor.Exit(lstDead);
                lock (this) { 
                this.InvalidateVisual();
                this.UpdateLayout();
                }

            });

    } // void UpdateListBox()

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            evntAllStart.Set();
        }
    } // class MainWindow;
} // namespace WpfAppSemaphore
