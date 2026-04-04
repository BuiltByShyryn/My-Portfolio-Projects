using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private DiscountContext context;

        public Form1()
        {
            InitializeComponent();
            context = new DiscountContext();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double price = Convert.ToDouble(textBox1.Text);
            double result = context.Calculate(price);
            labelResult.Text = $"Final Price: {result}";
        }


        private void radioButtonNoDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNoDiscount.Checked)
                context.ChangeStrategy(new NoDiscount());
        }

        private void radioButtonCoupon_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCoupon.Checked)
                context.ChangeStrategy(new CouponDiscount());
        }

        private void radioButton3Percent_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3Percent.Checked)
                context.ChangeStrategy(new Discount3());
        }

        private void radioButtonBigPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBigPurchase.Checked)
                context.ChangeStrategy(new BigPurchaseDiscount());
        }

    }
}
