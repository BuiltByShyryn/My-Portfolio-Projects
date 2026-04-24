namespace WindowsFormsApp5
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
            this.radioButtonNoDiscount = new System.Windows.Forms.RadioButton();
            this.radioButtonCoupon = new System.Windows.Forms.RadioButton();
            this.radioButton3Percent = new System.Windows.Forms.RadioButton();
            this.radioButtonBigPurchase = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButtonNoDiscount
            // 
            this.radioButtonNoDiscount.AutoSize = true;
            this.radioButtonNoDiscount.Location = new System.Drawing.Point(182, 13);
            this.radioButtonNoDiscount.Name = "radioButtonNoDiscount";
            this.radioButtonNoDiscount.Size = new System.Drawing.Size(135, 17);
            this.radioButtonNoDiscount.TabIndex = 0;
            this.radioButtonNoDiscount.TabStop = true;
            this.radioButtonNoDiscount.Text = "No Discount";
            this.radioButtonNoDiscount.UseVisualStyleBackColor = true;
            this.radioButtonNoDiscount.CheckedChanged += new System.EventHandler(this.radioButtonNoDiscount_CheckedChanged);
            // 
            // radioButtonCoupon
            // 
            this.radioButtonCoupon.AutoSize = true;
            this.radioButtonCoupon.Location = new System.Drawing.Point(182, 51);
            this.radioButtonCoupon.Name = "radioButtonCoupon";
            this.radioButtonCoupon.Size = new System.Drawing.Size(59, 17);
            this.radioButtonCoupon.TabIndex = 1;
            this.radioButtonCoupon.TabStop = true;
            this.radioButtonCoupon.Text = "Coupon";
            this.radioButtonCoupon.UseVisualStyleBackColor = true;
            this.radioButtonCoupon.CheckedChanged += new System.EventHandler(this.radioButtonCoupon_CheckedChanged);
            // 
            // radioButton3Percent
            // 
            this.radioButton3Percent.AutoSize = true;
            this.radioButton3Percent.Location = new System.Drawing.Point(182, 88);
            this.radioButton3Percent.Name = "radioButton3Percent";
            this.radioButton3Percent.Size = new System.Drawing.Size(80, 17);
            this.radioButton3Percent.TabIndex = 2;
            this.radioButton3Percent.TabStop = true;
            this.radioButton3Percent.Text = "3% Discount";
            this.radioButton3Percent.UseVisualStyleBackColor = true;
            this.radioButton3Percent.CheckedChanged += new System.EventHandler(this.radioButton3Percent_CheckedChanged);
            // 
            // radioButtonBigPurchase
            // 
            this.radioButtonBigPurchase.AutoSize = true;
            this.radioButtonBigPurchase.Location = new System.Drawing.Point(182, 122);
            this.radioButtonBigPurchase.Name = "radioButtonBigPurchase";
            this.radioButtonBigPurchase.Size = new System.Drawing.Size(126, 17);
            this.radioButtonBigPurchase.TabIndex = 3;
            this.radioButtonBigPurchase.TabStop = true;
            this.radioButtonBigPurchase.Text = "Big Purchase (10% off)";
            this.radioButtonBigPurchase.UseVisualStyleBackColor = true;
            this.radioButtonBigPurchase.CheckedChanged += new System.EventHandler(this.radioButtonBigPurchase_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 182);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(219, 254);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(68, 13);
            this.labelResult.TabIndex = 7;
            this.labelResult.Text = "Final Price: -";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(625, 478);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButtonBigPurchase);
            this.Controls.Add(this.radioButton3Percent);
            this.Controls.Add(this.radioButtonCoupon);
            this.Controls.Add(this.radioButtonNoDiscount);
            this.Name = "Form1";
            this.Text = "Discount Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonNoDiscount;
        private System.Windows.Forms.RadioButton radioButtonCoupon;
        private System.Windows.Forms.RadioButton radioButton3Percent;
        private System.Windows.Forms.RadioButton radioButtonBigPurchase;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelResult;
    }
}
