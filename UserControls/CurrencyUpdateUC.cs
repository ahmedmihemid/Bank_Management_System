using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Management_System.UserControls
{
    public partial class CurrencyUpdateUC : UserControl
    {
        public CurrencyUpdateUC()
        {
            InitializeComponent();
        }

     

        private void PrintCurrencyInfo(clsCurrency currency)
        {

            OldRateTextBox1.Text =currency.Rate.ToString();
        }


        private void GetEmptyText()
        {
            OldRateTextBox1.Text = "";
            NewRateTextBox.Text = "";

        }



        private void UpdateCurrencyRate()
        {


            if (!clsCurrency.IsCurrencyExistByCode(CurrencyCodeTextBox.Text.ToUpper()))
            {
                GetEmptyText();
                MessageBox.Show("Currency code not found. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            clsCurrency currency = clsCurrency.FindByCode(CurrencyCodeTextBox.Text.ToUpper());
            currency.UpdateRate(Convert.ToSingle(NewRateTextBox.Text));
            MessageBox.Show("Currency rate updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
            GetEmptyText();


        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            GetEmptyText();
            if (!clsCurrency.IsCurrencyExistByCode(CurrencyCodeTextBox.Text.ToUpper()))
            {
                MessageBox.Show("Currency code not found. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsCurrency currency = clsCurrency.FindByCode(CurrencyCodeTextBox.Text.ToUpper());
            PrintCurrencyInfo(currency);

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            UpdateCurrencyRate();
        }



    }
}
