using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    /// <summary>
    /// Each entry had an team competing in a matchup (1 Matchup had 2 entries).
    /// 
    /// </summary>
    public class MatchupEntryModel
    {
        /// <summary>
        /// The unique identifier for the matchup entry.
        /// </summary>
        public int Id { get; set; }

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
        public MatchupModel ParentMatchup { get; set; }

        public MatchupEntryModel()
        {
            TeamCompeting = null;
            ParentMatchup = null;
        }
    }
}