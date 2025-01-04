using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Management_System.UserControls
{
    public partial class withdrawUC : UserControl
    {
        public withdrawUC()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            currentbalance.Text = "";
            clsClient client = clsClient.FindClient(AccNumberTextBox.Text);
            if (!client.IsClientExist())
            {
                MessageBox.Show("The specified account number was not found. Please check the number and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           currentbalance.Text=client.AccountBalance.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            clsClient client = clsClient.FindClient(AccNumberTextBox.Text);
            if (!client.IsClientExist())
            {
                MessageBox.Show("The specified account number was not found. Please check the number and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            client.Withdraw(Convert.ToSingle(AmountTextBox.Text));
            currentbalance.Text = "";
            AccNumberTextBox.Text = "";
            AmountTextBox.Text = "";
            MessageBox.Show("Withdrawal completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
