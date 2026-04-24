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
namespace AnimationWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        public  List<Thread> lstThreads = new List<Thread>();
        public ManualResetEvent evnStart = new ManualResetEvent(true);
        public Mutex forStopAll = new Mutex(true);
        private void Button_Click_NewWindow(object sender, RoutedEventArgs e)
        {
            
            AnimateWindow window = new AnimateWindow();
            
            
            window.Show();
            //window.ShowDialog();
        }

        private void Button_Click_NewThread(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(AnimateThreadFunc);
            t1.SetApartmentState(ApartmentState.STA);
            t1.IsBackground = true;
            lock (lstThreads)
            {
                lstThreads.Add(t1);
            }
            t1.Start(this);
        }


        private void AnimateThreadFunc(object param)
        {
            MainWindow form = (MainWindow)param;
            AnimateWindow window = new AnimateWindow();

            window.globalevnStart = form.evnStart;
            window.global_lstThreads = form.lstThreads;

            window.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        private void Button_Click_ResumeAll(object sender, RoutedEventArgs e)
        {
            evnStart.Set();
        }

        private void Button_Click_PauseAll(object sender, RoutedEventArgs e)
        {
            evnStart.Reset();
        }

        private void Button_Click_CloseAll(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_StartThreadPool(object sender, RoutedEventArgs e)
        {

        }
    }
}
