using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Management_System.UserControls
{
    public partial class UserListUC : UserControl
    {
        public UserListUC()
        {
            InitializeComponent();
        }



        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Bank_DB.mdf;Integrated Security=True";



        int NumberOfUsers = 0;

        private void GetNumberOfUsers(List<clsUser> Users)
        {
            NumberOfUsers = 0;
        
            foreach (clsUser user in Users)
            {
                NumberOfUsers++;             
            }

        }

        private void PrintNumberOfUser()
        {
            NumberOfUserslabel.Text = NumberOfUsers.ToString();
        }




        private void UserListUC_Load(object sender, EventArgs e)
        {

                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                string cmd = "SELECT*FROM Users ; ";
                SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);
                SqlDataAdapter dr = new SqlDataAdapter(sqlCommand);
                DataTable data = new DataTable();
                dr.Fill(data);

                GetNumberOfUsers(clsUser.LoadUsersDataFromDatabase());
                PrintNumberOfUser();
                dataGridView1.DataSource = data;
                dataGridView1.Columns[3].Width = 170;
            
        }
    }
}
