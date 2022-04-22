using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentTrackerLibrary.Models
{
    /// <summary>
    /// The teams are in a tournament and the team has people.
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// The team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// The list of person who plays for this team.
        /// </summary>
        public List<PersonModel> TeamMembers = new List<PersonModel>();
    }
}