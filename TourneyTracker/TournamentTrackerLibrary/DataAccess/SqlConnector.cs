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
                p.Add("@PlaceName", prize.PlaceName);
                p.Add("@PrizeAmount", prize.PrizeAmount);
                p.Add("@PrizePercentage", prize.PrizePercentage);

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


        /// <summary>
        /// Get all the tournament in active from the databse.
        /// </summary>
        /// <returns>A list of all the tournaments with prizes, teams and rounds.</returns>
        public List<TournamentModel> GetTournament_All()
        {
            List<TournamentModel> allTournament = new List<TournamentModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                allTournament = connection.Query<TournamentModel>("dbo.spTournament_GetAll").ToList();

                foreach (TournamentModel tournament in allTournament)
                {
                    tournament.EnteredTeams = GetTeamsByTournament(connection, tournament.Id);

                    tournament.Prizes = GetPrizesByTournament(connection, tournament.Id);

                    tournament.Rounds = GetMatchupsByTournament(connection, tournament.Id);
                }
            }

            return allTournament;
        }

        private List<List<MatchupModel>> GetMatchupsByTournament(IDbConnection connection, int tournamentId)
        {
            List<MatchupModel> allTournamentMatchups = new List<MatchupModel>();
            List<MatchupEntryModel> entries = new List<MatchupEntryModel>();
            List<TeamModel> allTeams = GetTeam_All();

            var p = new DynamicParameters();
            p.Add("@Tournament", tournamentId);
            allTournamentMatchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

            foreach (MatchupModel matchup in allTournamentMatchups)
            {
                p = new DynamicParameters();
                p.Add("@MatchupId", matchup.Id);
                matchup.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", p, commandType: CommandType.StoredProcedure).ToList();

                if (matchup.WinnerId >= 0)
                {
                    matchup.Winner = allTeams.Where(x => x.Id == matchup.WinnerId).First(); 
                }

                foreach (MatchupEntryModel entry in matchup.Entries)
                {
                    if (entry.TeamCompetingId >= 0)
                    {
                        entry.TeamCompeting = allTeams.Where(x => x.Id == entry.TeamCompetingId).First(); 
                    }

                    if (entry.ParentMatchupId > 0)
                    {
                        entry.ParentMatchup = allTournamentMatchups.Where(x => x.Id == entry.ParentMatchupId).First(); 
                    }
                }
            }
            return DivideMatchupsInRounds(allTournamentMatchups);
        }

        private List<List<MatchupModel>> DivideMatchupsInRounds(List<MatchupModel> matchups)
        {
            List<List<MatchupModel>> rounds = new List<List<MatchupModel>>();

            List<MatchupModel> currentRound = new List<MatchupModel>();
            int currentNumberRound = 1;

            foreach (MatchupModel matchup in matchups)
            {
                if (matchup.MatchupRound > currentNumberRound)
                {
                    currentNumberRound = matchup.MatchupRound;
                    rounds.Add(currentRound);
                    currentRound = new List<MatchupModel>();
                }
                currentRound.Add(matchup);
            }
            rounds.Add(currentRound);
            
            return rounds;
        }

        /// <summary>
        /// Get the teams in a tournament.
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournamentId">The tournament id.</param>
        /// <returns></returns>
        private List<TeamModel> GetTeamsByTournament(IDbConnection connection, int tournamentId)
        {
            List<TeamModel> teams = new List<TeamModel>();

            var p = new DynamicParameters();
            p.Add("@Tournament", tournamentId);
            teams = connection.Query<TeamModel>("dbo.spTeam_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

            foreach (TeamModel team in teams)
            {
                p = new DynamicParameters();
                p.Add("@Team", team.Id);
                team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
            }

            return teams;
        }

        /// <summary>
        /// Get the prizes of the given tournament.
        /// </summary>
        /// <param name="connection">The connection to the database.</param>
        /// <param name="tournamentId">The id of the tournament.</param>
        /// <returns></returns>
        public List<PrizeModel> GetPrizesByTournament(IDbConnection connection, int tournamentId)
        {
            List<PrizeModel> prizes = new List<PrizeModel>();

            var p = new DynamicParameters();
            p.Add("@Tournament", tournamentId);
            prizes = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

            return prizes;
        }


    }
}
