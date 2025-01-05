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
    public partial class TransferLogUC : UserControl
    {
        public TransferLogUC()
        {
            InitializeComponent();
        }

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


        private void UserControl1_Load(object sender, EventArgs e)
        {

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            string cmd = "SELECT * FROM TransferLog ORDER BY Date DESC; ";
            SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);
            SqlDataAdapter dr = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            dr.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.Columns[0].Width = 230;

        }
    }
}
