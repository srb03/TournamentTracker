using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentTrackerLibrary.Models;

namespace TourneyTracker
{
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;
        decimal PrizeAmountValue = 0;
        public CreatePrizeForm(/*IPrizeRequester caller*/)
        {
            InitializeComponent();

            //callingForm = caller;

            // Populate Prize place combo box
            FillPrizePlaceComboBox();

            // Enable Prize amount textbox and disabled prize percent numeric
            ActivatePrizeAmount(true);
            ActivatePrizePercent(false);
        }

        private void FillPrizePlaceComboBox()
        {
            int[] NumberPlaces = { 1, 2 };
            PrizePlaceComboBox.DataSource = NumberPlaces;
        }

        private void ActivatePrizeAmount(bool active)
        {
            PrizeAmountTextBox.Enabled = active;
        }

        private void ActivatePrizePercent(bool active)
        {
            PrizePercentNumericUpDown.Enabled = active;
        }

        private void PrizePercentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ActivatePrizeAmount(false);
            ActivatePrizePercent(true);
        }

        private void PrizeAmountRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ActivatePrizeAmount(true);
            ActivatePrizePercent(false);
        }

        private void PrizeAmountTextBox_Leave(object sender, EventArgs e)
        {
            if (ValidatePrizeAmount())
            {
                PrizeAmountTextBox.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", PrizeAmountValue);
            }
            else
            {
                MessageBox.Show(
                    "The prize amount is not valid.",
                    "Invalid prize amount",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                PrizeAmountTextBox.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", 0);
            }
        }

        private bool ValidatePrizeAmount()
        {
            bool output = true;

            string PrizeAmountString = PrizeAmountTextBox.Text;
            try
            {
                PrizeAmountValue = Decimal.Parse(PrizeAmountString, System.Globalization.NumberStyles.Currency);
            }
            catch
            {
                output = false;
            }

            if (PrizeAmountValue < 0)
            {
                output = false;
            }
            return output;
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            
            
            string errorMessage = ValidateData();
            if (errorMessage.Length == 0)
            {
                int placeNumber = (int)PrizePlaceComboBox.SelectedItem;
                string placeName = PlaceNameTextBox.Text;
                decimal prizeAmount = 0;
                double prizePercent = 0;
                if (PrizeAmountRadioButton.Checked)
                {
                    prizeAmount = Decimal.Parse(PrizeAmountTextBox.Text, System.Globalization.NumberStyles.Currency);
                }
                else
                {
                    prizePercent = double.Parse(PrizePercentNumericUpDown.Value.ToString());
                }
                PrizeModel prize = new PrizeModel(placeNumber, placeName, prizeAmount, prizePercent);
            }
            else
            {
                MessageBox.Show(
                    errorMessage,
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("BIEN LLEGASTE HASTE EL FINAL", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ValidateData()
        {
            string output = "";

            if (string.IsNullOrEmpty(PlaceNameTextBox.Text.Trim()))
            {
                output = "You need to put a Place Name";
            }
            else if (PrizeAmountRadioButton.Checked && !ValidatePrizeAmount())
            {
                output = "The prize amount is not valid.";
            }
            else if (PrizePercentRadioButton.Checked && (PrizePercentNumericUpDown.Value < 0 && PrizePercentNumericUpDown.Value > 100))
            {
                output = "The prize percent is not valida. Must be a value between 0 and 100";
            }

            return output;
        }
    }
}