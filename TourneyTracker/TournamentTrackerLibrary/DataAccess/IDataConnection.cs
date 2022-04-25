using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;

namespace TournamentTrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePerson(PersonModel person);
        List<PersonModel> GetPerson_All();
        void CreateTeam(TeamModel team);
    }
}
