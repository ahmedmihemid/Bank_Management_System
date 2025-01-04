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
    public partial class TransferUC : UserControl
    {
        public TransferUC()
        {
            InitializeComponent();
        }

      


        private void Transfer()
        {

            clsClient senClient = clsClient.FindClient(SenAccNumberTextBox.Text);
            clsClient recClient = clsClient.FindClient(RecAccNumberTextBox.Text);

            if (!(senClient.IsClientExist() && recClient.IsClientExist()))
            {
                MessageBox.Show("The accoun numbers is  not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            recClient.Deposit(Convert.ToSingle(AmountTextBox.Text));
            senClient.Withdraw(Convert.ToSingle(AmountTextBox.Text));
            senClient.InsertTransferInfoToDatabase(recClient, Convert.ToSingle(AmountTextBox.Text));

            senAccBalanceLeab.Text = "";
            recAccBalanceLeab.Text = "";
            AmountTextBox.Text = "";

            MessageBox.Show("Transfer completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }




        private void guna2Button1_Click(object sender, EventArgs e)
        {
            senAccBalanceLeab.Text = "";
            recAccBalanceLeab.Text = "";
            AmountTextBox.Text = "";

            clsClient senClient=clsClient.FindClient(SenAccNumberTextBox.Text);
            clsClient recClient=clsClient.FindClient(RecAccNumberTextBox.Text);

            if( !(senClient.IsClientExist() && recClient.IsClientExist()) ) 
            {
                MessageBox.Show("the accoun numbers is  not found");
                return;
            }

            senAccBalanceLeab.Text=senClient.AccountBalance.ToString();
            recAccBalanceLeab.Text=recClient.AccountBalance.ToString();

        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Transfer();
        }

        
    }
}
