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
    public partial class DeleteUserUC : UserControl
    {
        public DeleteUserUC()
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



        private void DeleteUser()
        {
            if (clsUser.IsUserExist(UserNameTextBox.Text))
            {
                clsUser User = clsUser.Find(UserNameTextBox.Text);
                if (User.DeleteFromDatabase())
                {
                    PrintEmptyTextBox();
                    MessageBox.Show($"User with User Name {User.UserName} has been successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PrintEmptyTextBox();
            if (clsUser.IsUserExist(UserNameTextBox.Text))
            {
                clsUser User = clsUser.Find(UserNameTextBox.Text);
                PrintUserInfo(User);
                return;
            }
            MessageBox.Show("The specified User Name was not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }
    }
}
