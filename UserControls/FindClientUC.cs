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
    public partial class FindClientUC : UserControl
    {
        public FindClientUC()
        {
            InitializeComponent();
        }



        private void PrintCLientInfo(clsClient client)
        {
            FirstNameTextBox.Text = client.FirstName;
            LastNameTextBox.Text = client.LastName;
            EmailTextBox.Text = client.Email;
            PhoneTextBox.Text = client.Phone;
            PasswordTextBox.Text = client.PinCode;
            BalanceTextBox.Text = client.AccountBalance.ToString();

        }

        private void PrintEmptyTextBox()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            PasswordTextBox.Text = "";
            BalanceTextBox.Text = "";

        }


        private void FindClient()
        {
            PrintEmptyTextBox();
            clsClient client = clsClient.FindClient(AccNumberTextBox.Text);
            if(client.IsClientExist())
            {
                PrintCLientInfo(client);
                return;
            }

            MessageBox.Show("The specified account number was not found. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FindClient();
        }
    }
}
