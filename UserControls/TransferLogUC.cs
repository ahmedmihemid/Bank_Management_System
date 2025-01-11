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
    public partial class TransferLogUC : UserControl
    {
        public TransferLogUC()
        {
            InitializeComponent();
        }

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Bank_DB.mdf;Integrated Security=True";



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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDocument1 = new PrintDocument();
                printDocument1.PrintPage += new PrintPageEventHandler(PrintPageHandler);
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during printing: " + ex.Message);
            }
        }



        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
           
            Font titleFont = new Font("Arial", 18, FontStyle.Bold); 
            Font headerFont = new Font("Arial", 9, FontStyle.Bold); 
            Font cellFont = new Font("Arial", 7); 
            Brush brush = Brushes.Black; 
            Pen linePen = new Pen(Color.Black, 1); 

            int startX = 20; 
            int startY = 50; 
            int offsetY = 0; 
            int columnWidth = 120; 
   

           
            Image logo = Properties.Resources.the_world_bank_logo; 
            e.Graphics.DrawImage(logo, startX, startY, 100, 50); 
            offsetY += 60; 

          
            e.Graphics.DrawString("Transfer Log", titleFont, brush, startX, startY + offsetY);
            offsetY += 40; 

            
            string[] headers = { "Date", "UserName", "Senders ", "Recipients", "Amount", "Senders B", "Recipients B" };
            foreach (string header in headers)
            {
                e.Graphics.DrawString(header, headerFont, brush, startX, startY + offsetY);
                startX += columnWidth; 
            }

           
            e.Graphics.DrawLine(linePen, 20, startY + offsetY + 20, startX, startY + offsetY + 20);
            offsetY += 30; 
            startX = 20; 

          
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

              
                e.Graphics.DrawLine(linePen, 20, startY + offsetY + 20, startX, startY + offsetY + 20);
                offsetY += 25; 
                startX = 20;
            }
        }













    }
}
