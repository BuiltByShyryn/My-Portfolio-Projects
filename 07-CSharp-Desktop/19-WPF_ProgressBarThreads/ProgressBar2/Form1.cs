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
using System.Diagnostics.Eventing.Reader;


namespace ProgressBar2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtPeriodTimer.Text = "0,1";
            txtPeriod1.Text = "0,1";
            txtPeriod2.Text = "0,1";

            tmrProgressBar = new System.Windows.Forms.Timer();
            tmrProgressBar.Tick += new EventHandler(TimerRoutine); // ф-ция обработчик события таймера
            tmrProgressBar.Stop();
        }
        private System.Windows.Forms.Timer tmrProgressBar = null;  // new Timer();
        private Thread th1 = null, th2 = null;
        private Mutex stopMutex = new Mutex(false);
        private void TimerRoutine(object sender, EventArgs e)
        {
            tmrProgressBar.Stop();
            this.Invoke(new Action(() =>
            {
                prbTimer.PerformStep();  // prbTimer.value += prbTimer.Step;
            }));
            tmrProgressBar.Start();
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            try
            {
                int interval = (int)(float.Parse(txtPeriodTimer.Text) * 1000);
                tmrProgressBar.Interval = interval; // для таймера интервал
                                                    // настройки прогресс-бара
                prbTimer.Minimum = 1;
                prbTimer.Maximum = 500; // 1000
                prbTimer.Value = prbTimer.Minimum;
                prbTimer.Step = 1;
                //
                tmrProgressBar.Start(); // запук таймера
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // void btnStartTimer_Click(object sender, EventArgs e)

        private void btnPauseTimer_Click(object sender, EventArgs e)
        {
            tmrProgressBar.Stop();
        }
        
        private void btnStart1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string btn_whois = (string)btn.Tag;
            Thread th_temp;
            ProgressBar prb_temp;
           
            if (btn_whois == "Button-1")
            {
                th_temp = th1;
                prb_temp = prbThread1;
            }
            else
            {
                th_temp = th2;
                prb_temp = prbThread2;
            }
            if (th_temp == null)
            {
                th_temp = new Thread(ThreadProc_PB);
                th_temp.IsBackground = true;
                th_temp.Start(prb_temp);
            }
            if (btn_whois == "Button-1") th1 = th_temp;
            else th2 = th_temp;
        }

        private void btnPauseAll_Click(object sender, EventArgs e)
        {
            stopMutex.WaitOne();
        }

        private void btnResumeAll_Click(object sender, EventArgs e)
        {
            stopMutex.ReleaseMutex();
        }

        private void ThreadProc_PB(object param)
        {
// ProgressBar prb = param as ProgressBar;
            ProgressBar prb = (ProgressBar)param;
            int time_out = 200;
            prb.Value = prb.Minimum;
            while (true)
            {
                prb.PerformStep();
                prb.Invalidate(true);
                prb.Update();

                if (prb.Value == prb.Maximum)
                {
                    break;
                }
                Thread.Sleep(time_out);

            }
            if (this.prbThread1 == prb)
            {
                th1 = null;
            }
            else
            {
                th2 = null;
            }

        }



    }
}