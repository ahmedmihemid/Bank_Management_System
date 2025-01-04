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
    public partial class AddClientUC : UserControl
    {
        public AddClientUC()
        {
            InitializeComponent();
        }

        private clsClient _ReadClientInfo(clsClient Client)
        {
            Client.FirstName=FirstNameTextBox.Text;
            Client.LastName=LastNameTextBox.Text;
            Client.Email=EmailTextBox.Text;
            Client.Phone=PhoneTextBox.Text;
            Client.PinCode=PinCodeTextBox.Text;
            Client.AccountBalance=Convert.ToSingle(BalanceTextBox.Text);
            return Client;
        }

        private void PrintEmptyTextBox()
        {
            AccNumberTextBox.Text = "";
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            PinCodeTextBox.Text = "";
            BalanceTextBox.Text = "";

        }



        private  void  AddClient()
        {
            clsClient newClient=clsClient.FindClient(AccNumberTextBox.Text);
            if(!newClient.IsClientExist())
            {
                newClient = clsClient.GetNewObject(AccNumberTextBox.Text);
                newClient= _ReadClientInfo(newClient);
                clsClient.SaveClientsDataToDatabase(newClient);
                PrintEmptyTextBox();
                MessageBox.Show("A new client has been successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The account number already exists. Please try again with a different number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AddClient();
        }
    }
}
