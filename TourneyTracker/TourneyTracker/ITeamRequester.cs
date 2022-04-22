using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;

namespace TourneyTracker
{
    public interface ITeamRequester
    {
        void CompleteTeam(TeamModel team);
    }
}
