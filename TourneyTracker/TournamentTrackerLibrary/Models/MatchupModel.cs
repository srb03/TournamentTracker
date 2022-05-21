using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    /// <summary>
    /// The matchup is the game between to teams.
    /// </summary>
    public class MatchupModel
    {
        /// <summary>
        /// The unique identifier for the matchup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Each entry is for each team. 
        /// The entries contains the team competing and score of each team.
        /// </summary>
        public List<MatchupEntryModel> Entries = new List<MatchupEntryModel>();

        /// <summary>
        /// The id from the database that will be used to identify the winner
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// The winner of the matchup is recorded until the value is null.
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// The number of the round this matchup belong.
        /// </summary>
        public int MatchupRound { get; set; }

        public string MatchupDisplay {
            get 
            {
                string output = "";
                foreach (MatchupEntryModel me in Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = me.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += " vs " + me.TeamCompeting.TeamName;
                        } 
                    }
                    else
                    {
                        output = "Matchup not yet determined";
                        break;
                    }
                }
                return output;
            }
        }

        public MatchupModel()
        {
            WinnerId = -1;
        }


    }
}