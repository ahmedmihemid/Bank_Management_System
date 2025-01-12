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
    public partial class UpdateClientUC : UserControl
    {
        public UpdateClientUC()
        {
            InitializeComponent();
        }




        private bool TextIsEmpty()
        {
            return string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                   string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                   string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                   string.IsNullOrWhiteSpace(BalanceTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PasswordTextBox.Text);
        }

        private bool ValidateInput()
        {
            if (TextIsEmpty())
            {
                MessageBox.Show("All fields are required. Please fill in all the textboxes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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

        private void PrintCLientInfo(clsClient client)
        {
            FirstNameTextBox.Text = client.FirstName;
            LastNameTextBox.Text = client.LastName;
            EmailTextBox.Text = client.Email;
            PhoneTextBox.Text = client.Phone;
            PasswordTextBox.Text = client.PinCode;
            BalanceTextBox.Text = client.AccountBalance.ToString();

        }

        private clsClient _ReadClientInfo(clsClient Client)
        {
            Client.FirstName = FirstNameTextBox.Text;
            Client.LastName = LastNameTextBox.Text;
            Client.Email = EmailTextBox.Text;
            Client.Phone = PhoneTextBox.Text;
            Client.PinCode = PasswordTextBox.Text;
            Client.AccountBalance = Convert.ToSingle(BalanceTextBox.Text);
            return Client;
        }



        private void UpdateClient()
        {
            if (!ValidateInput())
            {
                return;
            }


            clsClient Client = clsClient.FindClient(AccNumberTextBox.Text);
            if (Client.IsClientExist())
            {
                Client = _ReadClientInfo(Client);
                clsClient.SaveClientsDataToDatabase(Client);
                PrintEmptyTextBox();
                MessageBox.Show("The client's information has been successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PrintEmptyTextBox();
            clsClient Client = clsClient.FindClient(AccNumberTextBox.Text);
            if (!Client.IsClientExist())
            {
                MessageBox.Show("The specified account number was not found. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PrintCLientInfo(Client);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            UpdateClient();
        }
    }
}
