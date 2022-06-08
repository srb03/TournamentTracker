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
        enum ErrorMessage
        {
            Success,
            NoPrizePlaceSelected,
            NoPlaceName,
            NoPrizeAmount,
            NoPrizePercent
        }

        IPrizeRequester callingForm;
        decimal PrizeAmountValue = 0;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

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
            ErrorMessage errorMessage = ValidateData();
            switch (ValidateData())
            {
                case ErrorMessage.Success:
                    CreatePrize();
                    break;

                case ErrorMessage.NoPrizePlaceSelected:
                    MessageBox.Show(
                    "You must select a place number for the prize.",
                    "No place selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    break;

                case ErrorMessage.NoPlaceName:
                    DialogResult dResult = MessageBox.Show(
                        "You not named this prize. You want to continue?", 
                        "No prize name",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (dResult == DialogResult.Yes)
                    {
                        CreatePrize();
                    }
                    break;

                case ErrorMessage.NoPrizeAmount:
                    MessageBox.Show(
                        "The prize amount you set is not valid.", 
                        "Invalid prize amount",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    
                    break;

                case ErrorMessage.NoPrizePercent:
                    MessageBox.Show(
                        "The prize percent you set is not valid.", 
                        "Invalid prize percent",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    break;

                default:
                    break;
            }
        }

        private void CreatePrize()
        {
            int placeNumber = (int)PrizePlaceComboBox.SelectedItem;
            string placeName = PlaceNameTextBox.Text;
            string prizeAmount = "$0.00";
            double prizePercent = 0;

            if (PrizeAmountRadioButton.Checked)
            {
                prizeAmount = PrizeAmountTextBox.Text;
            }
            else
            {
                prizePercent = double.Parse(PrizePercentNumericUpDown.Value.ToString());
            }

            try
            {
                PrizeModel prize = new PrizeModel(placeNumber, placeName, prizeAmount, prizePercent);
                callingForm.CompletePrize(prize);
                this.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                PrizePlaceComboBox.SelectedIndex = 0;
                PlaceNameTextBox.Text = "";
                PrizeAmountTextBox.Text = "$0.00";
                PrizePercentNumericUpDown.Value = 0;
            }
        }

        private ErrorMessage ValidateData()
        {
            ErrorMessage output = ErrorMessage.Success;

            if (PrizePlaceComboBox.SelectedItem == null)
            {
                output = ErrorMessage.NoPrizePlaceSelected;
            }
            else if (string.IsNullOrEmpty(PlaceNameTextBox.Text.Trim()))
            {
                output = ErrorMessage.NoPlaceName;
            }
            else if (PrizeAmountRadioButton.Checked && !ValidatePrizeAmount())
            {
                output = ErrorMessage.NoPrizeAmount;
            }
            else if (PrizePercentRadioButton.Checked && (PrizePercentNumericUpDown.Value < 0 && PrizePercentNumericUpDown.Value > 100))
            {
                output = ErrorMessage.NoPrizePercent;
            }

            return output;
        }
    }
}