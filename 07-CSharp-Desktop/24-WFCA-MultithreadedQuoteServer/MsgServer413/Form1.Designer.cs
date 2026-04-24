namespace MsgServer413
{
  partial class FormMsgServer
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.cbIpList = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnStartStop = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPort = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClear = new System.Windows.Forms.Button();
      this.txtLog = new System.Windows.Forms.TextBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.lvClients = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // cbIpList
      // 
      this.cbIpList.FormattingEnabled = true;
      this.cbIpList.Location = new System.Drawing.Point(6, 40);
      this.cbIpList.Name = "cbIpList";
      this.cbIpList.Size = new System.Drawing.Size(187, 21);
      this.cbIpList.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cbIpList);
      this.groupBox1.Controls.Add(this.txtPort);
      this.groupBox1.Controls.Add(this.btnStartStop);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(466, 77);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = " Server configuration ";
      // 
      // btnStartStop
      // 
      this.btnStartStop.Location = new System.Drawing.Point(316, 38);
      this.btnStartStop.Name = "btnStartStop";
      this.btnStartStop.Size = new System.Drawing.Size(75, 23);
      this.btnStartStop.TabIndex = 4;
      this.btnStartStop.Text = "&Start";
      this.btnStartStop.UseVisualStyleBackColor = true;
      this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(196, 24);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Port Server";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "IP Server";
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point(199, 40);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(100, 20);
      this.txtPort.TabIndex = 1;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.btnSave);
      this.groupBox2.Controls.Add(this.btnClear);
      this.groupBox2.Controls.Add(this.txtLog);
      this.groupBox2.Location = new System.Drawing.Point(12, 242);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(466, 215);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Server Log ";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnSave.Location = new System.Drawing.Point(236, 185);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 2;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnClear
      // 
      this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnClear.Location = new System.Drawing.Point(155, 185);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(75, 23);
      this.btnClear.TabIndex = 1;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      // 
      // txtLog
      // 
      this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLog.Location = new System.Drawing.Point(9, 19);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtLog.Size = new System.Drawing.Size(449, 159);
      this.txtLog.TabIndex = 0;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(403, 463);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 3;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.lvClients);
      this.groupBox3.Location = new System.Drawing.Point(12, 89);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(466, 147);
      this.groupBox3.TabIndex = 4;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = " User List ";
      // 
      // lvClients
      // 
      this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lvClients.HideSelection = false;
      this.lvClients.Location = new System.Drawing.Point(9, 19);
      this.lvClients.Name = "lvClients";
      this.lvClients.Size = new System.Drawing.Size(449, 122);
      this.lvClients.TabIndex = 0;
      this.lvClients.UseCompatibleStateImageBehavior = false;
      this.lvClients.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "N";
      this.columnHeader1.Width = 40;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Nickname";
      this.columnHeader2.Width = 120;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Remote address";
      this.columnHeader3.Width = 130;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Message count";
      this.columnHeader4.Width = 100;
      // 
      // FormMsgServer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(490, 498);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnClose);
      this.MinimumSize = new System.Drawing.Size(450, 480);
      this.Name = "FormMsgServer";
      this.Text = "MsgServer";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbIpList;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnStartStop;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ListView lvClients;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
  }
}

