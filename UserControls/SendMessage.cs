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
    public partial class SendMessage : UserControl
    {
        public SendMessage()
        {
            InitializeComponent();
        }



        private void guna2Button2_Click(object sender, EventArgs e)
        {
          
            if(clsUser.IsUserExist(UserNameTextBox.Text))
            {
                clsUser user = clsUser.Find(UserNameTextBox.Text);
                user.SendMessage(MessageTextBox.Text);
                MessageBox.Show("Your message has been sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("The specified user was not found. Please check the username and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



    }
}
