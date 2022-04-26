using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    /// <summary>
    /// A tournament is the main element of the application.
    /// Has the teams who plays in the tournament.
    /// Has the prizes for the winner.
    /// Has the rounds that is formed for the matchups.
    /// </summary>
    public class TournamentModel
    {
        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The cost of the entry for each team.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// The teams that play in the tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams = new List<TeamModel>();

        /// <summary>
        /// List of prizes for the winner of the tournament.
        /// </summary>
        public List<PrizeModel> Prizes = new List<PrizeModel>();

        /// <summary>
        /// The rounds of the tournament. Each List is a round so the List of list are all the rounds.
        /// </summary>
        public List<List<MatchupModel>> Rounds = new List<List<MatchupModel>>();
    }
}