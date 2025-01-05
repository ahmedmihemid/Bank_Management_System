using Bank_Management_System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class clsCurrency
{
    public enum enMode { EmptyMode = 0, UpdateMode = 1 }
    private enMode _Mode;

    private string _Country;
    private string _CurrencyCode;
    private string _CurrencyName;
    private float _Rate;

    public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\Bank_DB.mdf;Integrated Security=True";


    private static clsCurrency ConvertReaderToCurrencyObject(SqlDataReader reader)
    {
        return new clsCurrency(enMode.UpdateMode,
            reader["Country"].ToString(),
            reader["CurrencyCode"].ToString(),
            reader["CurrencyName"].ToString(),
            float.Parse(reader["Rate"].ToString()));
    }


    private static clsCurrency GetEmptyObject()
    {
        return new clsCurrency(enMode.EmptyMode, "", "", "", 0);
    }

    private void Update()
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "UPDATE Currencies SET Country = @Country, CurrencyName = @CurrencyName, Rate = @Rate WHERE CurrencyCode = @CurrencyCode";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Country", _Country);
                cmd.Parameters.AddWithValue("@CurrencyCode", _CurrencyCode);
                cmd.Parameters.AddWithValue("@CurrencyName", _CurrencyName);
                cmd.Parameters.AddWithValue("@Rate", _Rate);

                cmd.ExecuteNonQuery();
            }
        }
    }


    public clsCurrency(enMode mode, string country, string currencyCode, string currencyName, float rate)
    {
        _Mode = mode;
        _Country = country;
        _CurrencyCode = currencyCode;
        _CurrencyName = currencyName;
        _Rate = rate;
    }

    public bool IsEmpty() => _Mode == enMode.EmptyMode;

    public string Country => _Country;

    public string CurrencyCode => _CurrencyCode;

    public string CurrencyName => _CurrencyName;

    public float Rate => _Rate;

    public void UpdateRate(float rate)
    {
        _Rate = rate;
        Update();
    }

    public static clsCurrency FindByCountry(string country)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Currencies WHERE UPPER(Country) = @Country";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Country", country.ToUpper());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ConvertReaderToCurrencyObject(reader);
                    }
                }
            }
        }

        return GetEmptyObject();
    }

    public static clsCurrency FindByCode(string currencyCode)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Currencies WHERE UPPER(CurrencyCode) = @CurrencyCode";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CurrencyCode", currencyCode.ToUpper());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ConvertReaderToCurrencyObject(reader);
                    }
                }
            }
        }

        return GetEmptyObject();
    }

    public static bool IsCurrencyExistByCountry(string country)
    {
        return !FindByCountry(country).IsEmpty();
    }

    public static bool IsCurrencyExistByCode(string code)
    {
        return !FindByCode(code).IsEmpty();
    }

    public static List<clsCurrency> GetCurrencyList()
    {
        var currencies = new List<clsCurrency>();

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Currencies";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currencies.Add(ConvertReaderToCurrencyObject(reader));
                    }
                }
            }
        }

        return currencies;
    }

    public float ConvertToUSD(float amount)
    {
        return amount / _Rate;
    }

    public float ConvertToOtherCurrency(float amount, clsCurrency currency2)
    {
        var amountInUSD = ConvertToUSD(amount);

        if (currency2.CurrencyCode == "USD")
        {
            return amountInUSD;
        }

        return amountInUSD * currency2.Rate;
    }
}
