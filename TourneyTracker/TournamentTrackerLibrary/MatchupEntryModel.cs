﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary
{
    /// <summary>
    /// Each entry had an team competing in a matchup (1 Matchup had 2 entries).
    /// 
    /// </summary>
    public class MatchupEntryModel
    {
        /// <summary>
        /// The team competing in a matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// The score for a team in a matchup.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The matchup the team competing came from.
        /// Every match has a parent matchup except for the matchups in the first round.
        /// </summary>
        public TeamModel ParentMatchup { get; set; }
    }
}