using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bank_Management_System
{
    internal class clsMessage
    {
        public enum enMode { EmptyMode = 0,  AddNewMode = 1 }

            private enMode _mode;
            private int _ID;
            private string _SenderUser;
            private string _ReceiverUser;
            private string _MessageText;
            private string _IsRead;
            private string _Date;

        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


        public clsMessage(enMode mode,int ID, string sender, string receiver, string messageText, string isRead,string Date)
        {
                 _mode = mode;
                 _ID = ID; 
                _SenderUser = sender;
                _ReceiverUser = receiver; 
                _MessageText = messageText; 
                _IsRead = isRead; 
                _Date = Date;
        }

         
            public int ID
            {
                get { return _ID; } 
                set { _ID = value; } 
            }

           
            public string SenderUser
            {
                get { return _SenderUser; }
                set { _SenderUser = value; } 
            }

          
            public string ReceiverUser
            {
                get { return _ReceiverUser; } 
                set { _ReceiverUser = value; }
            }

    
            public string MessageText
            {
                get { return _MessageText; } 
                set { _MessageText = value; }
            }

           
            public string IsRead
            {
                get { return _IsRead; } 
                set { _IsRead = value; } 
            }


        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }


        private static clsMessage GetEmptyObject()
        {
            return new clsMessage(enMode.EmptyMode,0, "", "", "", "","");
        }

        public bool IsEmpty() => _mode == enMode.EmptyMode;


        private bool IsIDExist() 
        {
          return !IsEmpty();
        }





        public void SaveMessageToDatabase()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

               
                string query = @"
        INSERT INTO Message 
        ([ID], [SenderUser], [ReceiverUser], [MessageText], [Date], [IsRead]) 
        VALUES 
        (@ID, @SenderUser, @ReceiverUser, @MessageText, @Date, @IsRead)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                   
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this.ID;
                    cmd.Parameters.Add("@SenderUser", SqlDbType.NVarChar).Value = Global.CurrentUser.UserName;
                    cmd.Parameters.Add("@ReceiverUser", SqlDbType.NVarChar).Value =this.ReceiverUser;
                    cmd.Parameters.Add("@MessageText", SqlDbType.NVarChar).Value = this.MessageText;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@IsRead", SqlDbType.NVarChar).Value = this.IsRead;

                    cmd.ExecuteNonQuery();
                }
            }
        }





        public static clsMessage Find(int ID)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = "SELECT ID, SenderUser, ReceiverUser, MessageText, Date, IsRead FROM Message WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", ID); 
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new clsMessage(enMode.AddNewMode,
                 int.Parse(reader["ID"].ToString()),
                 reader["SenderUser"].ToString(),  
                 reader["ReceiverUser"].ToString(), 
                 reader["MessageText"].ToString(),
                 reader["IsRead"].ToString(),
                 (reader["Date"].ToString()) 
               );
                }
            }
            return GetEmptyObject();


        }


        public static int GetNewId()
        {
           
            Random r = new Random();
            int NewID   = r.Next(10000,99999);
            clsMessage message =clsMessage.Find(NewID);
            while(message.IsIDExist())
            {
                NewID = r.Next(10000, 99999);
                message = clsMessage.Find(NewID);
            }

            return NewID;
        }







    }








}
