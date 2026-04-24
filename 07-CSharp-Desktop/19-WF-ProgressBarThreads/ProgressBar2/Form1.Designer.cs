namespace ProgressBar2
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.prbTimer = new System.Windows.Forms.ProgressBar();
            this.prbThread1 = new System.Windows.Forms.ProgressBar();
            this.prbThread2 = new System.Windows.Forms.ProgressBar();
            this.btnStartTimer = new System.Windows.Forms.Button();
            this.btnPauseTimer = new System.Windows.Forms.Button();
            this.btnStart1 = new System.Windows.Forms.Button();
            this.btnPause1 = new System.Windows.Forms.Button();
            this.btnStart2 = new System.Windows.Forms.Button();
            this.btnPause2 = new System.Windows.Forms.Button();
            this.btnPauseAll = new System.Windows.Forms.Button();
            this.btnResumeAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtPeriodTimer = new System.Windows.Forms.TextBox();
            this.txtPeriod1 = new System.Windows.Forms.TextBox();
            this.txtPeriod2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prbTimer
            // 
            this.prbTimer.Location = new System.Drawing.Point(66, 58);
            this.prbTimer.Name = "prbTimer";
            this.prbTimer.Size = new System.Drawing.Size(537, 52);
            this.prbTimer.TabIndex = 0;
            // 
            // prbThread1
            // 
            this.prbThread1.Location = new System.Drawing.Point(66, 139);
            this.prbThread1.Name = "prbThread1";
            this.prbThread1.Size = new System.Drawing.Size(537, 52);
            this.prbThread1.TabIndex = 1;
            // 
            // prbThread2
            // 
            this.prbThread2.Location = new System.Drawing.Point(66, 220);
            this.prbThread2.Name = "prbThread2";
            this.prbThread2.Size = new System.Drawing.Size(537, 52);
            this.prbThread2.TabIndex = 2;
            // 
            // btnStartTimer
            // 
            this.btnStartTimer.Location = new System.Drawing.Point(619, 58);
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.Size = new System.Drawing.Size(75, 52);
            this.btnStartTimer.TabIndex = 3;
            this.btnStartTimer.Text = "Start";
            this.btnStartTimer.UseVisualStyleBackColor = true;
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
            // 
            // btnPauseTimer
            // 
            this.btnPauseTimer.Location = new System.Drawing.Point(700, 58);
            this.btnPauseTimer.Name = "btnPauseTimer";
            this.btnPauseTimer.Size = new System.Drawing.Size(75, 52);
            this.btnPauseTimer.TabIndex = 4;
            this.btnPauseTimer.Text = "Pause";
            this.btnPauseTimer.UseVisualStyleBackColor = true;
            this.btnPauseTimer.Click += new System.EventHandler(this.btnPauseTimer_Click);
            // 
            // btnStart1
            // 
            this.btnStart1.Location = new System.Drawing.Point(619, 139);
            this.btnStart1.Name = "btnStart1";
            this.btnStart1.Size = new System.Drawing.Size(75, 52);
            this.btnStart1.TabIndex = 5;
            this.btnStart1.Tag = "Button-1";
            this.btnStart1.Text = "Start";
            this.btnStart1.UseVisualStyleBackColor = true;
            this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // btnPause1
            // 
            this.btnPause1.Location = new System.Drawing.Point(700, 139);
            this.btnPause1.Name = "btnPause1";
            this.btnPause1.Size = new System.Drawing.Size(75, 52);
            this.btnPause1.TabIndex = 6;
            this.btnPause1.Text = "Pause";
            this.btnPause1.UseVisualStyleBackColor = true;
            // 
            // btnStart2
            // 
            this.btnStart2.Location = new System.Drawing.Point(619, 220);
            this.btnStart2.Name = "btnStart2";
            this.btnStart2.Size = new System.Drawing.Size(75, 52);
            this.btnStart2.TabIndex = 7;
            this.btnStart2.Tag = "Button-2";
            this.btnStart2.Text = "Start";
            this.btnStart2.UseVisualStyleBackColor = true;
            this.btnStart2.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // btnPause2
            // 
            this.btnPause2.Location = new System.Drawing.Point(700, 220);
            this.btnPause2.Name = "btnPause2";
            this.btnPause2.Size = new System.Drawing.Size(75, 52);
            this.btnPause2.TabIndex = 8;
            this.btnPause2.Text = "Pause";
            this.btnPause2.UseVisualStyleBackColor = true;
            // 
            // btnPauseAll
            // 
            this.btnPauseAll.Location = new System.Drawing.Point(187, 329);
            this.btnPauseAll.Name = "btnPauseAll";
            this.btnPauseAll.Size = new System.Drawing.Size(121, 23);
            this.btnPauseAll.TabIndex = 9;
            this.btnPauseAll.Text = "Pause for all";
            this.btnPauseAll.UseVisualStyleBackColor = true;
            this.btnPauseAll.Click += new System.EventHandler(this.btnPauseAll_Click);
            // 
            // btnResumeAll
            // 
            this.btnResumeAll.Location = new System.Drawing.Point(378, 329);
            this.btnResumeAll.Name = "btnResumeAll";
            this.btnResumeAll.Size = new System.Drawing.Size(124, 23);
            this.btnResumeAll.TabIndex = 10;
            this.btnResumeAll.Text = "Resume for all";
            this.btnResumeAll.UseVisualStyleBackColor = true;
            this.btnResumeAll.Click += new System.EventHandler(this.btnResumeAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(558, 329);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtPeriodTimer
            // 
            this.txtPeriodTimer.Location = new System.Drawing.Point(788, 75);
            this.txtPeriodTimer.Name = "txtPeriodTimer";
            this.txtPeriodTimer.Size = new System.Drawing.Size(100, 20);
            this.txtPeriodTimer.TabIndex = 12;
            // 
            // txtPeriod1
            // 
            this.txtPeriod1.Location = new System.Drawing.Point(788, 156);
            this.txtPeriod1.Name = "txtPeriod1";
            this.txtPeriod1.Size = new System.Drawing.Size(100, 20);
            this.txtPeriod1.TabIndex = 13;
            // 
            // txtPeriod2
            // 
            this.txtPeriod2.Location = new System.Drawing.Point(788, 237);
            this.txtPeriod2.Name = "txtPeriod2";
            this.txtPeriod2.Size = new System.Drawing.Size(100, 20);
            this.txtPeriod2.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(797, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Time of period";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 402);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPeriod2);
            this.Controls.Add(this.txtPeriod1);
            this.Controls.Add(this.txtPeriodTimer);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnResumeAll);
            this.Controls.Add(this.btnPauseAll);
            this.Controls.Add(this.btnPause2);
            this.Controls.Add(this.btnStart2);
            this.Controls.Add(this.btnPause1);
            this.Controls.Add(this.btnStart1);
            this.Controls.Add(this.btnPauseTimer);
            this.Controls.Add(this.btnStartTimer);
            this.Controls.Add(this.prbThread2);
            this.Controls.Add(this.prbThread1);
            this.Controls.Add(this.prbTimer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar prbTimer;
    private System.Windows.Forms.ProgressBar prbThread1;
    private System.Windows.Forms.ProgressBar prbThread2;
    private System.Windows.Forms.Button btnStartTimer;
    private System.Windows.Forms.Button btnPauseTimer;
    private System.Windows.Forms.Button btnStart1;
    private System.Windows.Forms.Button btnPause1;
    private System.Windows.Forms.Button btnStart2;
    private System.Windows.Forms.Button btnPause2;
    private System.Windows.Forms.Button btnPauseAll;
    private System.Windows.Forms.Button btnResumeAll;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.TextBox txtPeriodTimer;
    private System.Windows.Forms.TextBox txtPeriod1;
    private System.Windows.Forms.TextBox txtPeriod2;
    private System.Windows.Forms.Label label1;
  }
}

