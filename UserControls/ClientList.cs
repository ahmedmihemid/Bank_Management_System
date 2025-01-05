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
    public partial class ClientList : UserControl
    {
        public ClientList()
        {
            InitializeComponent();
        }

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


        int NumberOfClients = 0;
        float TotalBalances = 0;

        private void GetNumberOfClientsAndTotalBalances(List<clsClient> clients)
        {
            NumberOfClients = 0;
            TotalBalances = 0;
            foreach (clsClient client in clients)
            {
                NumberOfClients++;
                TotalBalances += client.AccountBalance;
            }

        }


        private void PrintNumberOfClientsAndTotalBalances()
        {

            NumberOfClientslabel.Text = NumberOfClients.ToString();
            TotalBalanceslabel.Text = TotalBalances.ToString()+" $";
        }


        
        private void ClientList_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            string cmd = "SELECT*FROM CLients ; ";
            SqlCommand   sqlCommand = new SqlCommand(cmd, sqlConnection);
            SqlDataAdapter dr=new SqlDataAdapter(sqlCommand);
            DataTable data=new DataTable();
            dr.Fill(data);

            GetNumberOfClientsAndTotalBalances(clsClient.LoadClientsDataFromDatabase());
            PrintNumberOfClientsAndTotalBalances();
            dataGridView1.DataSource = data;
            dataGridView1.Columns[2].Width=180; 
        }

 






      
    }
}
