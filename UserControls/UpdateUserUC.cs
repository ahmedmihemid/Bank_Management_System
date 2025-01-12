using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Bank_Management_System.UserControls
{
    public partial class UpdateUserUC : UserControl
    {
        public UpdateUserUC()
        {
            InitializeComponent();
        }

        int Permissions = 0;




        private bool TextIsEmpty()
        {
            return string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                   string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                   string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                   string.IsNullOrWhiteSpace(PasswordTextBox.Text);
        }

        private bool ValidateInput()
        {
            if (TextIsEmpty())
            {
                MessageBox.Show("All fields are required. Please fill in all the textboxes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Permissions == 0)
            {
                MessageBox.Show("No permissions have been set for this action. Please configure the required permissions.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }



        private clsUser _ReadUserInfo(clsUser User)
        {
            User.FirstName = FirstNameTextBox.Text;
            User.LastName = LastNameTextBox.Text;
            User.Email = EmailTextBox.Text;
            User.Phone = PhoneTextBox.Text;
            User.Password = PasswordTextBox.Text;
            User.PermissionsValue = Permissions;

            return User;
        }



        private void PrintUserInfo(clsUser User)
        {
            FirstNameTextBox.Text = User.FirstName;
            LastNameTextBox.Text = User.LastName;
            EmailTextBox.Text = User.Email;
            PhoneTextBox.Text = User.Phone;
            PasswordTextBox.Text = User.Password;
            ShowPermissionsInfo(User);
        }



        private void PrintEmptyTextBox()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            PasswordTextBox.Text = "";
            UncheckAll();
        }



        private void UpdateUser()
        {
            if(!ValidateInput())
            {
                return;
            }


            if (!clsUser.IsUserExist(UserNameTextBox.Text))
            {
                MessageBox.Show("The specified username was not found. Please verify the username and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsUser user = clsUser.Find(UserNameTextBox.Text);
            user = _ReadUserInfo(user);
            user.SaveToDatabase();
            MessageBox.Show("User information has been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void ShowPermissionsInfo(clsUser user)
        {
            
            ManageClientPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pManageClients) == (int)clsUser.enPermissions.pManageClients;

            ManageUserPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pManageUsers) == (int)clsUser.enPermissions.pManageUsers;
            
            ClientTransactionsPER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pTranactions) == (int)clsUser.enPermissions.pTranactions;
           
            CurrencyExchangePER.Checked = (user.PermissionsValue & (int)clsUser.enPermissions.pCurrencyExchange) == (int)clsUser.enPermissions.pCurrencyExchange;
           
            AllPER.Checked = user.PermissionsValue == (int)clsUser.enPermissions.eAll;

            SetCheckBoxState(user.PermissionsValue != (int)clsUser.enPermissions.eAll);
        }




        private void SetCheckBoxState(bool state)
        {
            ManageClientPER.Enabled = state;
            ManageUserPER.Enabled = state;
            ClientTransactionsPER.Enabled = state;
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
        private void UncheckAll()
        {
            AllPER.Checked = false;
            UncheckAbuttons();
        }



        private void UncheckAbuttons()
        {

            ManageClientPER.Checked = false;
            ManageUserPER.Checked = false;
            ClientTransactionsPER.Checked = false;
            CurrencyExchangePER.Checked = false;
        }





        private void UpdatePermission(CheckBox checkBox, clsUser.enPermissions permission)
        {
            if (checkBox.Checked)
            {
                Permissions |= (int)permission;  
            }
            else
            {
                Permissions &= ~(int)permission;  
            }
        }

        private void UpdateAllPermissions()
        {
            Permissions = AllPER.Checked ? (int)clsUser.enPermissions.eAll : 0;
            SetCheckBoxState(!AllPER.Checked);
            if (!AllPER.Checked)
            {
                UncheckAbuttons();
            }
        }




        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PrintEmptyTextBox();
            clsUser User = clsUser.Find(UserNameTextBox.Text);
            if (!clsUser.IsUserExist(UserNameTextBox.Text))
            {
                MessageBox.Show("The specified username was not found. Please verify the username and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Permissions = 0;
            PrintUserInfo(User);
         

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void AllPER_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdateAllPermissions();
        }

        private void ManageClientPER_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdatePermission(ManageClientPER, clsUser.enPermissions.pManageClients);
        }


        private void ManageUserPER_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePermission(ManageUserPER, clsUser.enPermissions.pManageUsers);

        }

        private void ClientTransactionsPER_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdatePermission(ClientTransactionsPER, clsUser.enPermissions.pTranactions);
        }

        private void CurrencyExchangePER_CheckedChanged_1(object sender, EventArgs e)
        {
            UpdatePermission(CurrencyExchangePER, clsUser.enPermissions.pCurrencyExchange);
        }
    }
}
