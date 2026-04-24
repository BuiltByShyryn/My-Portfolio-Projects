using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace RunningProc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Exe - files(*.*) | *.exe|*.*|All files(*.*)";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                txtProgName.Text = ofd.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                // Process.Start(txtProgName.Text,txtArgs.Text);

                startInfo.FileName = txtProgName.Text;
                startInfo.Arguments = txtArgs.Text;
                Process.Start(startInfo);
            }
            catch (Exception ex){
               MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
