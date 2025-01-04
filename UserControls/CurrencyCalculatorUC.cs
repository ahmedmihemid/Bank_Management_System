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
    public partial class CurrencyCalculatorUC : UserControl
    {
        public CurrencyCalculatorUC()
        {
            InitializeComponent();
        }




        private void GetEmptyText()
        {
            FromTextBox.Text = "";
            ToTextBox.Text = "";
            AmountTextBox.Text = "";
            ResoltTextBox.Text = "";

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

         

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

           

            if (clsCurrency.IsCurrencyExistByCode(FromTextBox.Text.ToUpper()) && clsCurrency.IsCurrencyExistByCode(ToTextBox.Text.ToUpper()))
            {

                clsCurrency currency1 = clsCurrency.FindByCode(FromTextBox.Text.ToUpper());
                clsCurrency currency2 = clsCurrency.FindByCode(ToTextBox.Text.ToUpper());

                ResoltTextBox.Text= currency1.ConvertToOtherCurrency(Convert.ToSingle(AmountTextBox.Text),currency2).ToString();
                MessageBox.Show("Conversion completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            GetEmptyText();
            MessageBox.Show("One or both currency codes are invalid. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



    }
}
