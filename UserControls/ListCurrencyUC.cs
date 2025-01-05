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
    public partial class ListCurrencyUC : UserControl
    {
        public ListCurrencyUC()
        {
            InitializeComponent();
        }

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


        private void ListCurrencyUC_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            string cmd = "SELECT*FROM Currencies ; ";
            SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);
            SqlDataAdapter dr = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            dr.Fill(data);

            dataGridView1.DataSource = data;
            dataGridView1.Columns[0].Width = 170;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[3].Width = 150;
           

        }
    }
}
