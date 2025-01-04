using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static clsUser;

namespace Bank_Management_System.UserControls
{
    public partial class AddUserUC : UserControl
    {
        public AddUserUC()
        {
            InitializeComponent();
        }


        int Permissions = 0;

        private clsUser _ReadUserInfo(clsUser User)
        {
            User.ID=clsUser.GetNewId();
            User.FirstName=FirstNameTextbox.Text;
            User.LastName=LastNameTextbox.Text;
            User.Email=EmailTextbox.Text;
            User.Phone=PhoneTextbox.Text;
            User.Password=PasswordTextbox.Text;
            User.PermissionsValue=Permissions;
          
            return User;
        }

        private void PrintEmptyTextBox()
        {
            UserNameTextbox.Text="";
            FirstNameTextbox.Text = "";
            LastNameTextbox.Text = "";
            EmailTextbox.Text = "";
            PhoneTextbox.Text = "";
            PasswordTextbox.Text = "";

        }



        private void UncheckAbuttons()
        {
       
            ManageClientPER.Checked = false;
            ManageUserPER.Checked = false;
            ClientTransactionsPER.Checked = false;
            CurrencyExchangePER.Checked = false;
        }


        private void UncheckAll()
        {
            AllPER.Checked = false;
            UncheckAbuttons();
        }

        private void AddUser()
        {
           
            if (!clsUser.IsUserExist(UserNameTextbox.Text))
            {
                clsUser newUser = clsUser.GetAddNewUserObject(UserNameTextbox.Text);
                newUser = _ReadUserInfo(newUser);
                newUser.SaveToDatabase();
                PrintEmptyTextBox();
                UncheckAll();
                MessageBox.Show("A new user has been successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The username already exists. Please try again with a different name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PrintEmptyTextBox();
            }

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





        private void AllPER_CheckedChanged(object sender, EventArgs e)
        {
            if (AllPER.Checked)
            {
                Permissions = (int)clsUser.enPermissions.eAll;
                SetCheckBoxState(false);
                UncheckAbuttons();
               
            }
            else
            {
                if (clsUser.CheckAccessPermission(clsUser.enPermissions.eAll, Permissions))
                {
                    Permissions -= (int)clsUser.enPermissions.eAll;

                }
                SetCheckBoxState(true);
            }
        }

        private void ManageClientPER_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePermission(ManageClientPER, clsUser.enPermissions.pManageClients);
        }

        private void ManageUserPER_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePermission(ManageUserPER, clsUser.enPermissions.pManageUsers);
        }

        private void ClientTransactionsPER_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePermission(ClientTransactionsPER, clsUser.enPermissions.pTranactions);
        }

        private void CurrencyExchangePER_CheckedChanged(object sender, EventArgs e)
        {
             UpdatePermission(CurrencyExchangePER, clsUser.enPermissions.pCurrencyExchange);
        }



        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AddUser();
        }


    }
}
