using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Bank_DB.mdf;Integrated Security=True";


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

        private void button1_Click(object sender, EventArgs e)
        {
           
            PrintDocument printDocument1 = new PrintDocument();

         
            printDocument1.PrintPage += new PrintPageEventHandler(PrintPageHandler);

       
            printDocument1.Print();
        }
      
  

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
          
            Font titleFont = new Font("Arial", 18, FontStyle.Bold); 
            Font headerFont = new Font("Arial", 9, FontStyle.Bold); 
            Font cellFont = new Font("Arial", 7); 
            Brush brush = Brushes.Black;
            Pen linePen = new Pen(Color.Black, 1); 

            int startX = 50;  
            int startY = 50; 
            int offsetY = 0; 
            int columnWidth = 120;

            
            Image logo = Properties.Resources.the_world_bank_logo; 
            e.Graphics.DrawImage(logo, startX, startY, 100, 50);
            offsetY += 60; 

           
            e.Graphics.DrawString("Client List", titleFont, brush, startX, startY + offsetY);
            offsetY += 40; 

            
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                e.Graphics.DrawString(column.HeaderText, headerFont, brush, startX, startY + offsetY);
                startX += columnWidth; 
            }

          
            e.Graphics.DrawLine(linePen, 50, startY + offsetY + 20, startX, startY + offsetY + 20);
            offsetY += 30; 
            startX = 50;

           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        e.Graphics.DrawString(cell.Value.ToString(), cellFont, brush, startX, startY + offsetY);
                        startX += columnWidth;
                    }
                }

               
                e.Graphics.DrawLine(linePen, 50, startY + offsetY + 20, startX, startY + offsetY + 20);
                offsetY += 25; 
                startX = 50;
            }
        }

















    }
}
