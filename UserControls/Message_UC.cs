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
    public partial class Message_UC : UserControl
    {
        public Message_UC()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessagePanal.Controls.Clear();
            SendMessage message = new SendMessage();
            message.Dock = DockStyle.Fill;
            MessagePanal.Controls.Add(message);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessagePanal.Controls.Clear();
            ShowMessage message = new ShowMessage();
            message.Dock = DockStyle.Fill;
            MessagePanal.Controls.Add(message);
        }



    }
}
