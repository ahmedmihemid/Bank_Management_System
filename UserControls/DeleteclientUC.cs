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
    public partial class DeleteclientUC : UserControl
    {
        public DeleteclientUC()
        {
            InitializeComponent();
        }

        
        private void PrintCLientInfo(clsClient client)
        {
            FirstNameTextBox.Text=client.FirstName;
            LastNameTextBox.Text=client.LastName;
            EmailTextBox.Text=client.Email;
            PhoneTextBox.Text=client.Phone;
            PasswordTextBox.Text=client.PinCode;
            BalanceTextBox.Text=client.AccountBalance.ToString();

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

        private void DeleteClient()
        {
            clsClient Client = clsClient.FindClient(AccNumberTextBox.Text);
            if (Client.IsClientExist())
            {
                Client = clsClient.GetNewObject(AccNumberTextBox.Text);
                if(Client.DeleteFromDatabase())
                {
                    PrintEmptyTextBox();
                    MessageBox.Show($"Client with Account Number {Client.AccountNumber} has been successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PrintEmptyTextBox();
            clsClient Client = clsClient.FindClient(AccNumberTextBox.Text);
            if (Client.IsClientExist())
            {
                PrintCLientInfo(Client);
                return;
            }
            MessageBox.Show("The specified account number was not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DeleteClient();
        }
    }
}
