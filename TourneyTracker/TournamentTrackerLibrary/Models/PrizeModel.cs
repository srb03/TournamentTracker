using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary
{
    /// <summary>
    /// A prize is for the winner or runnerup.
    /// </summary>
    public class PrizeModel
    {
        /// <summary>
        /// The numeric place for the winner of this prize: 1 for first place, 2 for second place.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// The name of the prize like "First place", "Champion" or "Runner up".
        /// </summary>
        public string PrizeName { get; set; }


        /// <summary>
        /// The cantity of money the winner of this prize will receive.
        /// If this prop is setting then the PrizePercent prop must be 0.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// The percent of the total entry fee acumulated the winner of the prize will receive.
        /// If this prop is setting then the PrizeAmount prop must be 0.
        /// </summary>
        public double PrizePercent { get; set; }
    }
}