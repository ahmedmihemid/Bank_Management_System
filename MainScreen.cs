using Bank_Management_System.UserControls;
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

namespace Bank_Management_System
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
        }


     
        DateTime LoginDate= DateTime.Now;
       

        private void MainScreen_Load(object sender, EventArgs e)
        {
            customizeDesing();
            timer1.Start();
            MainPanal.Controls.Clear();
            HomePageUC homeUC = new HomePageUC();
            homeUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(homeUC);
        }

     
        private void PrintDate()
        {
            DateTime TimeAndDate = DateTime.Now;
            DateLabel.Text = TimeAndDate.ToString();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           
            PrintDate();
        }





        private void customizeDesing()
        {
            CLientsButtonsPanal.Visible = false;
            UsersButtonsPanal.Visible = false;
            TransactionsButtonsPanal.Visible = false;
            CurrencyButtonsPanal.Visible = false;
           

        }


        private void hideSubMenu()
        {
            if (CLientsButtonsPanal.Visible == true)
                CLientsButtonsPanal.Visible = false;

            if (UsersButtonsPanal.Visible == true)
                UsersButtonsPanal.Visible = false;

            if (TransactionsButtonsPanal.Visible == true)
                TransactionsButtonsPanal.Visible = false;


            if (CurrencyButtonsPanal.Visible == true)
                CurrencyButtonsPanal.Visible = false;

        }

        private void ShowsubMenu(Panel subMenu)
        {
            if(!subMenu.Visible)
            {
                hideSubMenu();
                subMenu.Visible=true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }




         // this is the Main Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            if(!Global.CurrentUser.CheckAccessPermission(enPermissions.pManageClients))
            {
                MessageBox.Show("You do not have the required permissions");
                return;
            }
          
           ShowsubMenu(CLientsButtonsPanal);
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            if (!Global.CurrentUser.CheckAccessPermission(enPermissions.pManageUsers))
            {
                MessageBox.Show("You do not have the required permissions");
                return;

            }
            ShowsubMenu(UsersButtonsPanal);
        }
    

        private void button18_Click(object sender, EventArgs e)
        {
            if (!Global.CurrentUser.CheckAccessPermission(enPermissions.pTranactions))
            {
                MessageBox.Show("You do not have the required permissions ");
                return;
            }
            ShowsubMenu(TransactionsButtonsPanal);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (!Global.CurrentUser.CheckAccessPermission(enPermissions.pCurrencyExchange))
            {
                MessageBox.Show("You do not have the required permissions ");
                return;
            }

            ShowsubMenu(CurrencyButtonsPanal);
        }









        // this is the Buttons of Manage User Button
        private void button11_Click(object sender, EventArgs e)
        {
          
            MainPanal.Controls.Clear();
            UserListUC userListUC = new UserListUC();
            userListUC.Dock = DockStyle.Fill; 
            MainPanal.Controls.Add(userListUC);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            AddUserUC userUC = new AddUserUC();
            userUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(userUC);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            DeleteUserUC userUC = new DeleteUserUC();
            userUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(userUC);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            UpdateUserUC userUC = new UpdateUserUC();
            userUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(userUC);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            FindUserUC userUC = new FindUserUC();   
            userUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(userUC);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            LoginregisterUC LoinResUC = new LoginregisterUC();
            LoinResUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(LoinResUC);
        }







        // this is the Buttons of Manage Client Button
        private void button2_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            ClientList clientList = new ClientList();
            clientList.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(clientList);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            AddClientUC clientUC = new AddClientUC();
            clientUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(clientUC);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            DeleteclientUC clientUC = new DeleteclientUC();
            clientUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(clientUC);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            UpdateClientUC clientUC = new UpdateClientUC();
            clientUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(clientUC);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            FindClientUC clientUC = new FindClientUC();
            clientUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(clientUC);
        }








        // this is the Buttons of Transactions Button

        private void button17_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            DepositUs depositUs = new DepositUs();
            depositUs.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(depositUs);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            withdrawUC withdrawUC = new withdrawUC();
            withdrawUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(withdrawUC);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            TransferUC transferUC = new TransferUC();
            transferUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(transferUC);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            TransferLogUC TransferUC = new TransferLogUC();
            TransferUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(TransferUC);
        }







        private void button22_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            FindCurrencyUC currencyUC = new FindCurrencyUC();
            currencyUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(currencyUC);

        }

        private void button21_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            CurrencyUpdateUC currencyUC = new CurrencyUpdateUC();
            currencyUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(currencyUC);
        }


        private void button20_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            CurrencyCalculatorUC currencyUC = new CurrencyCalculatorUC();
            currencyUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(currencyUC);
        }


        private void button23_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            ListCurrencyUC currencyUC = new ListCurrencyUC();
            currencyUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(currencyUC);
        }



        // Home Button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            HomePageUC homeUC = new HomePageUC();
            homeUC.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(homeUC);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MainPanal.Controls.Clear();
            Message_UC message = new Message_UC();
            message.Dock = DockStyle.Fill;
            MainPanal.Controls.Add(message);
        }




        // Logout Button

        private void button25_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
