using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    /// <summary>
    /// A prize is for the winner or runnerup.
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The numeric place for the winner of this prize: 1 for first place, 2 for second place.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The name of the prize like "First place", "Champion" or "Runner up".
        /// </summary>
        public string PlaceName { get; set; }


        /// <summary>
        /// The cantity of money the winner of this prize will receive.
        /// If this prop is setting then the PrizePercent prop must be 0.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// The percent of the total entry fee acumulated the winner of the prize will receive.
        /// If this prop is setting then the PrizeAmount prop must be 0.
        /// </summary>
        public double PrizePercentage { get; set; }

        public string PrizeNameInList { get { return PlaceNumber.ToString() + " - " + PlaceName; } }


        /// <summary>
        /// Constructor for the Prize. A prize will give to the winner.
        /// </summary>
        /// <param name="placeNumber">The numeric value for the place.</param>
        /// <param name="placeName">The name of the prize.</param>
        /// <param name="prizeAmount">The quantity given to the winner.</param>
        /// <param name="prizePercent">The percent of the total entry fee.</param>
        public PrizeModel(int placeNumber, string placeName, string prizeAmount, double prizePercent)
        {
            PlaceNumber = ValidatePrizePlace(placeNumber);
            PlaceName = ValidatePlaceName(placeName);
            PrizeAmount = ValidatePrizeAmount(prizeAmount);
            PrizePercentage = ValidatePrizePercentage(prizePercent);
        }

        private double ValidatePrizePercentage(double prizePercentage)
        {
            if (prizePercentage >= 0 && prizePercentage <= 100)
            {
                return prizePercentage;
            }
            else
            {
                throw new ArgumentOutOfRangeException("The prize percente must be between 0 and 100.");
            }
        }

        private decimal ValidatePrizeAmount(string prizeAmountString)
        {
            // PrizeAmountTextBox.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", PrizeAmountValue);
            decimal prizeAmount = Decimal.Parse(prizeAmountString, System.Globalization.NumberStyles.Currency);

            if (prizeAmount < 0)
            {
                throw new ArgumentOutOfRangeException("The prize can't be less than 0");
            }

            return prizeAmount;
        }

        private  string ValidatePlaceName(string placeName)
        {
                return placeName.Trim();
        }

        private int ValidatePrizePlace(int prizePlace)
        {
            if (prizePlace == 1 || prizePlace == 2)
            {
                return prizePlace;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public PrizeModel()
        {

        }
    }
}