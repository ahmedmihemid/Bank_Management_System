using Bank_Management_System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;

public class clsClient : clsPerson
{
    private enum Mode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 }

    private Mode _mode;
    private string _accountNumber;
    private string _pinCode;
    private float _Balance;

    private bool _markedForDelete = false;

    public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


    private clsClient(Mode mode, string firstName, string lastName, string email, string phone,
        string accountNumber, string pinCode, float accountBalance)
        : base(firstName, lastName, email, phone)
    {
        _mode = mode;
        _accountNumber = accountNumber;
        _pinCode = pinCode;
        _Balance = accountBalance;
    }

    public string AccountNumber => _accountNumber;

    public string PinCode
    {
        get => _pinCode;
        set => _pinCode = value;
    }

    public float AccountBalance
    {
        get => _Balance;
        set => _Balance = value;
    }

    public bool MarkedForDelete
    {
        get => _markedForDelete;
        set => _markedForDelete = value;
    }


    private static clsClient GetEmptyClient()
    {
        clsClient client= new clsClient(Mode.EmptyMode, "", "", "", "","", "", 0f);
        return client;
    }

    public bool IsEmpty() => _mode == Mode.EmptyMode;

    public static clsClient GetNewObject(string AccountNumber)
    {
        clsClient client = new clsClient(Mode.AddNewMode, "", "", "", "", AccountNumber, "", 0f);
        return client;
    }
  


    public static void SaveClientsDataToDatabase(clsClient client)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = client._mode == Mode.AddNewMode
                ? "INSERT INTO Clients (FirstName, LastName, Email, Phone, AccountNumber, PinCode, Balance) " +
                  "VALUES (@FirstName, @LastName, @Email, @Phone, @AccountNumber, @PinCode, @Balance)"
                : "UPDATE Clients SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, " +
                  "PinCode = @PinCode, Balance = @Balance WHERE AccountNumber = @AccountNumber";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                cmd.Parameters.AddWithValue("@LastName", client.LastName);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", client.Phone);
                cmd.Parameters.AddWithValue("@AccountNumber", client.AccountNumber);
                cmd.Parameters.AddWithValue("@PinCode", client.PinCode);
                cmd.Parameters.AddWithValue("@Balance", client.AccountBalance);

                cmd.ExecuteNonQuery();
            }
        }
    }













    public static List<clsClient> LoadClientsDataFromDatabase()
    {
        var clients = new List<clsClient>();

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = "SELECT FirstName, LastName, Email, Phone, AccountNumber, PinCode, Balance FROM Clients";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(new clsClient(
                            Mode.UpdateMode,
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            reader["AccountNumber"].ToString(),
                            reader["PinCode"].ToString(),
                            float.Parse(reader["Balance"].ToString())
                        ));
                    }
                }
            }
        }

        return clients;
    }

    public bool DeleteFromDatabase()
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = "DELETE FROM Clients WHERE AccountNumber = @AccountNumber";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }
    
    public static clsClient FindClient(string accountNumber)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = "SELECT FirstName, LastName, Email, Phone, AccountNumber, PinCode, Balance FROM Clients WHERE AccountNumber = @AccountNumber";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new clsClient(
                            Mode.UpdateMode,
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            reader["AccountNumber"].ToString(),
                            reader["PinCode"].ToString(),
                            float.Parse(reader["Balance"].ToString())
                        );
                    }
                }
            }
        }

        return GetEmptyClient();
    }



    public void InsertTransferInfoToDatabase(clsClient client, float Amount)
    {
      

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();

            string query = @"
            INSERT INTO TransferLog 
            ([Date], [UserName], [Senders Acc Number], [Recipients Acc Number], [Amount], [Senders Balance], [Recipients Balance]) 
            VALUES 
            (@Date, @UserName, @SenderAccNumber, @RecipientAccNumber, @Amount, @SenderBalance, @RecipientBalance)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                
                cmd.Parameters.AddWithValue("@Date", DateTime.Now); 
                cmd.Parameters.AddWithValue("@UserName", Global.CurrentUser.UserName);
                cmd.Parameters.AddWithValue("@SenderAccNumber", this.AccountNumber);
                cmd.Parameters.AddWithValue("@RecipientAccNumber", client.AccountNumber);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@SenderBalance", this.AccountBalance - Amount);
                cmd.Parameters.AddWithValue("@RecipientBalance", client.AccountBalance + Amount);

                cmd.ExecuteNonQuery();
            }
        }
    }



    public bool IsClientExist() 
    {
        return (!IsEmpty());
    }

   

    private DateTime GetDateTime()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime;
    }

    public void Deposit(float amount)
    {
        AccountBalance += amount;
        SaveClientsDataToDatabase(this);
    }

    public bool Withdraw(float amount)
    {
        if (amount > AccountBalance)
            return false;

        AccountBalance -= amount;
        SaveClientsDataToDatabase(this);
        return true;
    }
}

