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
    public partial class HomePageUC : UserControl
    {
        public HomePageUC()
        {
            InitializeComponent();
        }

        private void HomePageUC_Load(object sender, EventArgs e)
        {
            GetUserInfo();
            ShowPermissionsInfo(Global.CurrentUser);
        }


        private void  GetUserInfo()
        {
            UserNamelabel.Text = Global.CurrentUser.UserName;
            FullNamelabel.Text = Global.CurrentUser.FirstName + " " + Global.CurrentUser.LastName;
            Emaillabel.Text = Global.CurrentUser.Email;

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








    }
}
