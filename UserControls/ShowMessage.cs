using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank_Management_System.UserControls
{
    public partial class ShowMessage : UserControl
    {
        public ShowMessage()
        {
            InitializeComponent();
        }

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";




        private void Updatestatus()
        {

            if (dataGridView1.CurrentRow != null)
            {
                int messageId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
               

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string query = "UPDATE Message SET IsRead=@IsRead WHERE ID = @ID";


                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID", messageId);
                    cmd.Parameters.AddWithValue("@IsRead", "Read");

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                dataGridView1.CurrentRow.Cells["Isread"].Value = "Read";
            }

        }






        private void ShowMessage_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            string cmd = "SELECT ID, Date, ReceiverUser, IsRead FROM Message WHERE ReceiverUser = @ReceiverUser ORDER BY Date DESC;";
            SqlCommand sqlCommand = new SqlCommand(cmd, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ReceiverUser", Global.CurrentUser.UserName);

            SqlDataAdapter dr = new SqlDataAdapter(sqlCommand);
            DataTable data = new DataTable();
            dr.Fill(data);

            dataGridView1.DataSource = data;
            dataGridView1.Columns[1].Width = 230;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.Columns[3].Width = 150;
            

        }

     

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                  
                    int messageId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                   
                    clsMessage message = clsMessage.Find(messageId);

                    Updatestatus();
                    MessageBox.Show($"Message Details:\n\nSender: {message.SenderUser}\nReceiver: {message.ReceiverUser}\nDate: {message.Date}\n\n{message.MessageText}",
                        "Message Details", MessageBoxButtons.OK);
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
