using Bank_Management_System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class clsUser : clsPerson
{
    private enum Mode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 }

    public enum enPermissions
    {
        eAll = -1, pManageClients = 1, pManageUsers = 2, pTranactions = 4, pCurrencyExchange = 8
    };


    private Mode _mode;
    private int _ID;
    private string _userName;
    private string _password;
    private int _permissions;



  public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";



    // Constructor
    private clsUser(Mode mode, int ID, string firstName, string lastName, string email, string phone,
                    string userName, string password, int permissions)
        : base(firstName, lastName, email, phone)
    {
        _ID = ID;
        _mode = mode;
        _userName = userName;
        _password = password;
        _permissions = permissions;
    }




    public int ID
    {
        get { return _ID; }
        set { _ID = value; }

    }

    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
    }

    public int PermissionsValue
    {
        get => _permissions;
        set => _permissions = value;
    }

    public bool IsEmpty() => _mode == Mode.EmptyMode;

    public static bool IsEmpty(clsUser User) => User.IsEmpty();


    public static List<clsUser> LoadUsersDataFromDatabase()
    {
        var users = new List<clsUser>();
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new clsUser(
                    Mode.UpdateMode,
                    int.Parse(reader["ID"].ToString()),
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Email"].ToString(),
                    reader["Phone"].ToString(),
                    reader["UserName"].ToString(),
                    reader["Password"].ToString(),
                    int.Parse(reader["Permission"].ToString())
                ));
            }
        }
        return users;
    }



    public void SaveToDatabase()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = _mode == Mode.AddNewMode ?
                "INSERT INTO Users (ID, FirstName, LastName, Email, Phone, UserName, Password, Permission) VALUES (@ID, @FirstName, @LastName, @Email, @Phone, @UserName, @Password, @Permission)" :
                "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, Password=@Password, Permission=@Permission WHERE ID=@ID";

            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Permission", PermissionsValue);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


    public bool DeleteFromDatabase()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = "DELETE FROM Users WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }

    public static clsUser Find(string userName)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserName", userName);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new clsUser(
                    Mode.UpdateMode,
                    int.Parse(reader["ID"].ToString()),
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Email"].ToString(),
                    reader["Phone"].ToString(),
                    reader["UserName"].ToString(),
                    reader["Password"].ToString(),
                    int.Parse(reader["Permission"].ToString())
                );
            }
        }
        return GetEmptyUserObject();
    }


    public static clsUser Find(int ID)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            string query = "SELECT * FROM Users WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", ID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new clsUser(
                    Mode.UpdateMode,
                     int.Parse(reader["ID"].ToString()),
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Email"].ToString(),
                    reader["Phone"].ToString(),
                    reader["UserName"].ToString(),
                    reader["Password"].ToString(),
                    int.Parse(reader["Permission"].ToString())
                );
            }
        }
        return GetEmptyUserObject();
    }




    public static clsUser Find(string userName, string password)
    {
        clsUser user = null;
      

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new clsUser(
                        Mode.UpdateMode,
                        int.Parse(reader["ID"].ToString()),
                        reader["FirstName"].ToString(),
                        reader["LastName"].ToString(),
                        reader["Email"].ToString(),
                        reader["Phone"].ToString(),
                        reader["UserName"].ToString(),
                        reader["Password"].ToString(),
                        int.Parse(reader["Permission"].ToString()));

                    }
                    reader.Close();
                }


            }
            connection.Close();
        }
        return user ?? GetEmptyUserObject();
    }




    public static int GetNewId()
    {

        Random r = new Random();
        int NewID = r.Next(10000, 99999);
        clsUser user = clsUser.Find(NewID);
        while (user.IsUserExist())
        {
            NewID = r.Next(10000, 99999);
            user = clsUser.Find(NewID);
        }

        return NewID;
    }




    public static void InsertLoginRsisterInfoToDatabase()
    {


        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = @"
            INSERT INTO LoginRegister 
            ([Date], [UserName], [Password], [Permissions]) 
            VALUES 
            (@Date, @UserName, @Password, @Permissions)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {


                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = Global.CurrentUser.UserName;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Global.CurrentUser.Password;
                cmd.Parameters.Add("@Permissions", SqlDbType.Int).Value = Global.CurrentUser.PermissionsValue;


                cmd.ExecuteNonQuery();
            }
        }
    }




    public bool CheckAccessPermission(enPermissions Permission)
    {
        if (Permission == enPermissions.eAll)
            return true;

        return ((((int)Permission) & this.PermissionsValue) == ((int)Permission)) ? true : false;
    }


    public static bool CheckAccessPermission(enPermissions Permission, int TotalPermission)
    {
        if (Permission == enPermissions.eAll)
            return true;

        return ((((int)Permission) & TotalPermission) == ((int)Permission)) ? true : false;
    }


    public bool IsUserExist() => !IsEmpty();
    public static bool IsUserExist(string userName) => !Find(userName).IsEmpty();

    public static clsUser GetEmptyUserObject() => new clsUser(Mode.EmptyMode, 0, "", "", "", "", "", "", 0);

    public static clsUser GetAddNewUserObject(string UserName) => new clsUser(Mode.AddNewMode, 0, "", "", "", "", UserName, "", 0);


    public void SendMessage(string MessageText)
    {
        clsMessage message = new clsMessage(clsMessage.enMode.AddNewMode, clsMessage.GetNewId(), Global.CurrentUser.UserName, this.UserName, MessageText, "not read", DateTime.Now.ToString());
        message.SaveMessageToDatabase();

    }





}
