using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary.Models;
using Dapper;
using System.Data;

namespace TournamentTrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";

        // Create a person in the database. Return the Id created for the person.
        public void CreatePerson(PersonModel person)
        {
            // spPeople_Insert @FirstName, @LastName, @EmailAddress,  @CellphoneNumber return @Id
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", person.FirstName);
                p.Add("@LastName", person.LastName);
                p.Add("@EmailAddress", person.EmailAddress);
                p.Add("@CellphoneNumber", person.CellphoneNumber);

                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                person.Id = p.Get<int>("@Id");
            }
        }


        // Return all the people in the database.
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> allPeople = new List<PersonModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                allPeople = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
            return allPeople;
        }

        // Create the team in the database and associate it with the team members
        public void CreateTeam(TeamModel team)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                // spTeams_Insert @TeamName, output -> @Id
                var p = new DynamicParameters();
                p.Add("@TeamName", team.TeamName);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                //Insert the Team in the database
                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                team.Id = p.Get<int>("@Id");

                // Insert the team members in the weak entity
                foreach (PersonModel person in team.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);
                    p.Add("@PersonId", person.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }
            }
        }

        // Return all the teams in the database.
        // The team object includes the team members.
        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> allTeams = new List<TeamModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                // spTeam_GetAll
                allTeams = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();

                foreach (TeamModel team in allTeams)
                {
                    var p = new DynamicParameters();
                    p.Add("@Team", team.Id);

                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return allTeams;
        }

        // Save the tournament, the prizes, the teams
        public void CreateTournament(TournamentModel tournament)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                
            }
        }

        // Create the tournament in the database and return the Id for the tournament
        private void SaveTournament(IDbConnection connection, TournamentModel tournament)
        {
            // spTournament_Insert @TournamentName, @EntryFee, @Id -> output
            var p = new DynamicParameters();
            p.Add("@TournamentName", tournament.TournamentName);
            p.Add("@EntryFee", tournament.EntryFee);

            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("dbo.spTournament_Insert", p, commandType: CommandType.StoredProcedure);

            tournament.Id = p.Get<int>("@Id");
        }
    }
}
