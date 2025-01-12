using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank_Management_System
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);

        }

        public  string UserName;
        private string Password;

          




        private void guna2Button1_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.Find(UserName, Password);

            if (!clsUser.IsEmpty(user))
            {

                MainScreen mainScreen = new MainScreen();

                Global.CurrentUser = user;
                UserLogTextBox.Text = "";
                PasswordLogTextBox.Text = "";
                clsUser.InsertLoginRsisterInfoToDatabase();
                mainScreen.ShowDialog();
                Hide();
                Close();
            }
            else
            {
                MessageBox.Show("User name not found");
            }
         

        }

        private void UserLogTextBox_TextChanged(object sender, EventArgs e)
        {
            UserName= UserLogTextBox.Text;
        }

        private void PasswordLogTextBox_TextChanged(object sender, EventArgs e)
        {
            Password = PasswordLogTextBox.Text;
        }
    }
}
