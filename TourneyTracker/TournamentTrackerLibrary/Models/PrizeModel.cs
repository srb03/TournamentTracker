﻿using System;
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


        /// <summary>
        /// Constructor for the Prize. A prize will give to the winner.
        /// </summary>
        /// <param name="placeNumber">The numeric value for the place.</param>
        /// <param name="placeName">The name of the prize.</param>
        /// <param name="prizeAmount">The quantity given to the winner.</param>
        /// <param name="prizePercent">The percent of the total entry fee.</param>
        public PrizeModel(int placeNumber, string placeName, decimal prizeAmount, double prizePercent)
        {
            PlaceNumber = placeNumber;
            PrizeName = placeName;
            PrizeAmount = prizeAmount;
            PrizePercent = prizePercent;
        }
    }
}