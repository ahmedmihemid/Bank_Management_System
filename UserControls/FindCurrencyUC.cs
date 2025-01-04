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

    public partial class FindCurrencyUC : UserControl
    {

        private static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB; Database=bank_system_DB; Trusted_Connection=True;";
        public FindCurrencyUC()
        {
            InitializeComponent();
        }


        private void PrintCurrencyInfo(clsCurrency currency)
        {
            CountryTextBox.Text=currency.Country;
            CurrencyNameTextBox.Text=currency.CurrencyName;
            CurrencyCodeTextBox.Text=currency.CurrencyCode;
            RateTextBox.Text=currency.Rate.ToString();
        }


        private void GetEmptyText()
        {
            CountryTextBox.Text = "";
            CurrencyNameTextBox.Text = "";
            CurrencyCodeTextBox.Text = "";
            RateTextBox.Text = "";
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            if (!clsCurrency.IsCurrencyExistByCode(CurrencyCodeTextBox.Text.ToUpper()))
            {
                GetEmptyText();
                MessageBox.Show("The specified currency code was not found. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            clsCurrency currency = clsCurrency.FindByCode(CurrencyCodeTextBox.Text);
            PrintCurrencyInfo(currency);
            MessageBox.Show("Currency information retrieved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        


    }
}
