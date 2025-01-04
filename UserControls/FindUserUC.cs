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
    public partial class FindUserUC : UserControl
    {
        public FindUserUC()
        {
            InitializeComponent();
        }


        private void PrintUserInfo(clsUser User)
        {
            FirstNameTextBox.Text = User.FirstName;
            LastNameTextBox.Text = User.LastName;
            EmailTextBox.Text = User.Email;
            PhoneTextBox.Text = User.Phone;
            PasswordTextBox.Text = User.Password;

        }

        private void PrintEmptyTextBox()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            PasswordTextBox.Text = "";

        }


        private void ShowPermissionsInfo(clsUser user)
        {

            if (user.PermissionsValue == (int)clsUser.enPermissions.eAll)
              { AllPER.Checked = user.PermissionsValue == (int)clsUser.enPermissions.eAll; }
            else
            { 
               ManageClientPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pManageClients) == (int)clsUser.enPermissions.pManageClients;

               ManageUserPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pManageUsers) == (int)clsUser.enPermissions.pManageUsers;
 
               TransactionsPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pTranactions) == (int)clsUser.enPermissions.pTranactions;

               CurrencyExchangePER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pCurrencyExchange) == (int)clsUser.enPermissions.pCurrencyExchange;

            }

            DisableAllCheckBox();
        }



        private void SetCheckBoxState(bool state)
        {
            AllPER.Enabled = state;
            ManageClientPER.Enabled = state;
            ManageUserPER.Enabled = state;
            TransactionsPER.Enabled = state;
            CurrencyExchangePER.Enabled = state;
        }

        private void EnableAllCheckBox()
        {
            SetCheckBoxState(true);
        }

        private void DisableAllCheckBox()
        {
            SetCheckBoxState(false);
        }



        private void FindUser()
        {
            PrintEmptyTextBox();
            if (clsUser.IsUserExist(UserNameTextBox.Text))
            {
                clsUser user = clsUser.Find(UserNameTextBox.Text);
                PrintUserInfo(user);
                ShowPermissionsInfo(user);
            }
        }

       
        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FindUser();

        }
    }
}
