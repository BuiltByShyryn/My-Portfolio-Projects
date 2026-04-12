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
using System.Windows.Shapes;
using System.Threading;
namespace AnimationWpf
{
    /// <summary>
    /// Interaction logic for AnimateWindow.xaml
    /// </summary>
    public partial class AnimateWindow : Window
    {
        public AnimateWindow()
        {
            InitializeComponent();
            ThreadPool.SetMinThreads(10, 10);
        }

        public double BallSize = 20;
        public ManualResetEvent globalevnStart = null;
        public ManualResetEvent envForStart = new ManualResetEvent(true);

        public List<Thread> global_lstThreads = null;

        public List<Thread> lstThreads =new List<Thread>();
      

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            ThreadParam param = new ThreadParam();
            param.form = this;
            param.canvas = this.canvas;
            param.ellipse = new Ellipse();
            param.ellipse.Width = param.ellipse.Height = BallSize;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            byte a = 255;
            byte r = (byte)rnd.Next(255);
            byte g = (byte)rnd.Next(255);
            byte b = (byte)rnd.Next(255);

            param.ellipse.Fill = new SolidColorBrush(Color.FromArgb(a,r,g,b ));
            
            param.canvas.Children.Add(param.ellipse);

            ThreadPool.QueueUserWorkItem(ThreadAnimateFunc, param);
        }

        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            envForStart.Reset();
        }

        private void Button_Click_Resume(object sender, RoutedEventArgs e)
        {
            envForStart.Set();
        }

        public class ThreadParam
        {
            public AnimateWindow form = null;
            public Canvas canvas = null;
            public Ellipse ellipse = null;

        }

        void ThreadAnimateFunc(object obj)
        {
            ThreadParam param = (ThreadParam)obj;
            Random rnd = new Random(
                (int)DateTime.Now.Ticks * Thread.CurrentThread.ManagedThreadId);

            Size ballSize = new Size(BallSize, BallSize);

            double width = 0;
            double height = 0;

            param.canvas.Dispatcher.Invoke(() =>
            {
                width = param.canvas.ActualWidth;
                height = param.canvas.ActualHeight;

                if (width <= 0)
                    width = param.canvas.Width;

                if (height <= 0)
                    height = param.canvas.Height;

                if (width <= 0)
                    width = 450;

                if (height <= 0)
                    height = 350;
            });

            double x1 = 0;
            double y1 = Math.Max(0, height - ballSize.Height);

            double x2 = rnd.Next((int)Math.Max(1, width - ballSize.Width));
            double y2 = rnd.Next(0, (int)Math.Max(1, height / 2));

            double speed = 40;
            double dX = (x2 - x1) / speed;
            double dY = (y2 - y1) / speed;

            double x = x1;
            double y = y1;

            while (true)
            {
                param.form.envForStart.WaitOne();

                if (param.form.globalevnStart != null)
                    param.form.globalevnStart.WaitOne();

                Dispatcher.InvokeAsync(() =>
                {
                    Canvas.SetLeft(param.ellipse, x);
                    Canvas.SetTop(param.ellipse, y);
                });

                x += dX;
                y += dY;

                if (x <= 0 || x >= width - ballSize.Width)
                    dX = -dX;

                if (y <= 0 || y >= height - ballSize.Height)
                    dY = -dY;

                Thread.Sleep(20);
            }
        }
    }
}
