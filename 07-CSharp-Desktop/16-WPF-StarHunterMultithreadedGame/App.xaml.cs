using System.Threading;
using System.Windows;

namespace StarHunter_Exam
{
    public partial class App : Application
    {
        // Останавливает от двойного включения игры
        private static Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "MyCuteStarHunterGame";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("The game is already running! (๑ > ᎑ < )");
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }
    }
}