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

        /// <summary>
        /// Create the team in the database and associate it with the team members.
        /// The matchup entry id will be saved in the team object that will be added to the tournament.
        /// </summary>
        /// <param name="team">The team that will be created.</param>
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
                SaveTournament(connection, tournament);

                SaveTournamentEntries(connection, tournament);

                SavePrizes(connection, tournament);

                SaveTournamentPrizes(connection, tournament);

                SaveMatchups(connection, tournament);

                SaveMatchupEntries(connection, tournament);

            }
        }

        /// <summary>
        /// Save the tournament entries in database.
        /// The matchup entry id will be added in the entries list of the matchup.
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournament">The tournament object with the rounds.</param>
        private void SaveMatchupEntries(IDbConnection connection, TournamentModel tournament)
        {
            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        // spMatchupEntries_Insert @MatchupId @ParentMatchupId @TeamCompeting @Id = output
                        var p = new DynamicParameters();
                        p.Add("@MatchupId", matchup.Id);
                        
                        if (entry.ParentMatchup == null)
                        {
                            p.Add("@ParentMatchupId", null);
                        }
                        else
                        {
                            p.Add("@ParentMatchupId", entry.ParentMatchup.Id);
                        }


                        if (entry.TeamCompeting == null)
                        {
                            p.Add("@TeamCompetingId", null);
                        }
                        else
                        {
                            p.Add("@TeamCompetingId", entry.TeamCompeting.Id);
                        }
                        
                        p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);

                        entry.Id = p.Get<int>("@Id");
                    }
                }
            }
        }

        /// <summary>
        /// Save the tournament matchups in database.
        /// The matchup id will be added in Rounds list attribute.
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournament">The tournament object with the rounds.</param>
        private void SaveMatchups(IDbConnection connection, TournamentModel tournament)
        {
            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    // spMatchups_Insert @TournamentId @MatchupRound @Id = output
                    var p = new DynamicParameters();
                    p.Add("@TournamentId", tournament.Id);
                    p.Add("@MatchupRound", matchup.MatchupRound);
                    p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                    matchup.Id = p.Get<int>("@Id");
                }
            }
        }

        /// <summary>
        /// Associate the prizes with the tournament in database. 
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournament">The tournament object with the prizes list.</param>
        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel tournament)
        {
            foreach (PrizeModel prize in tournament.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", tournament.Id);
                p.Add("@PrizeId", prize.Id);

                connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Associate the teams with the tournament in database. 
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournament">The tournament object with the entered teams list.</param>
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel tournament)
        {
            // spTournamentEntries_Insert @TournamentId @TeamId @Id = output
            foreach (TeamModel team in tournament.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", tournament.Id);
                p.Add("@TeamId", team.Id);

                connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Save the prizes tournament in the database. Save the id of the prizes in the prizes list.
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournament">The tournament object.</param>
        private void SavePrizes(IDbConnection connection, TournamentModel tournament)
        {
            foreach (PrizeModel prize in tournament.Prizes)
            {
                // spPrizezs_Insert @PlaceNumber @PlaceName @PrizeAmount @PrizePercentage @Id = output
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", prize.PlaceNumber);
                p.Add("@PlaceName", prize.PrizeName);
                p.Add("@PrizeAmount", prize.PrizeAmount);
                p.Add("@PrizePercentage", prize.PrizePercent);

                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizezs_Insert", p, commandType: CommandType.StoredProcedure);

                prize.Id = p.Get<int>("@Id");
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
