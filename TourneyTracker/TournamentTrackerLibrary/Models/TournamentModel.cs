using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentTrackerLibrary;

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


        public void CreateRounds()
        {
            // Randomize the list of teams
            List<TeamModel> randomizeTeams = RandomizeTeamList(EnteredTeams);
            
            // Calculate the number of rounds
            int rounds = CalculateRounds(randomizeTeams.Count);
            
            // Calculate the number of fake teams (example: three real teams then 1 team to make 4 that is a power of 2)
            int fakeTeams = CalculateFakeTeams(rounds, randomizeTeams.Count);
            
            // Create the first round
            this.Rounds.Add(CreateFirstRound(randomizeTeams, fakeTeams));

            // Create the other rounds
            CreateOtherRounds(rounds);
        }


        public void UpdateScores()
        {
            List<MatchupModel> toScore = new List<MatchupModel>();
            foreach (List<MatchupModel> round in this.Rounds)
            {
                foreach (MatchupModel match in round)
                {
                    if (match.Winner == null && (match.Entries.Count == 1 || match.Entries.Any(x => x.Score != 0)))
                    {
                        toScore.Add(match);
                    }
                }
            }

            MarkWinners(toScore);
            AdvanceWinners(toScore, this);

            toScore.ForEach(x => GlobalConfig.Connection.UpdateMatchup(x));
        }

        /// <summary>
        /// Adavance the winner team to the next round and save it to the database.
        /// </summary>
        /// <param name="models">The list of match with teams to be advanced.</param>
        /// <param name="tournament">The object tournament.</param>
        private static void AdvanceWinners(List<MatchupModel> models, TournamentModel tournament)
        {
            foreach (MatchupModel m in models)
            {
                foreach (List<MatchupModel> round in tournament.Rounds)
                {
                    foreach (MatchupModel rm in round)
                    {
                        foreach (MatchupEntryModel me in rm.Entries)
                        {
                            if (me.ParentMatchup != null)
                            {
                                if (me.ParentMatchup.Id == m.Id)
                                {
                                    me.TeamCompeting = m.Winner;
                                    GlobalConfig.Connection.UpdateMatchup(rm);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get a list of matchups and mark the winners.
        /// </summary>
        /// <param name="matchups">List of matchups objects.</param>
        private void MarkWinners(List<MatchupModel> matchups)
        {
            foreach (MatchupModel matchup in matchups)
            {
                if (matchup.Entries.Count == 1)
                {
                    matchup.Winner = matchup.Entries.First().TeamCompeting;
                    continue;
                }

                if (matchup.Entries[0].Score > matchup.Entries[1].Score)
                {
                    matchup.Winner = matchup.Entries[0].TeamCompeting;
                }
                else if (matchup.Entries[1].Score > matchup.Entries[0].Score)
                {
                    matchup.Winner = matchup.Entries[1].TeamCompeting;
                }
                else
                {
                    throw new Exception("There is no support for ties. Must provide a winner");
                }
            }
        }

        /// <summary>
        /// Create the rest of the round in the matchup.
        /// </summary>
        /// <param name="firstRound">The first round previously created.</param>
        /// <param name="rounds">The number of round that must be played in the tournament.</param>
        private void CreateOtherRounds(int rounds)
        {
            List<List<MatchupModel>> otherRounds = new List<List<MatchupModel>>();
            List<MatchupModel> previousRound = Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();
            int round = 2;

            while (round <= rounds)
            {
                foreach (MatchupModel matchup in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = matchup });
                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }
                Rounds.Add(currentRound);
                previousRound = currentRound;

                currentRound = new List<MatchupModel>();

                round++;
            }
        }


        /// <summary>
        /// Create the first round for the tournament.
        /// </summary>
        /// <param name="teams">List of teams added to the tournament.</param>
        /// <param name="fakeTeams">The number of fake teams to complete the tournament.</param>
        /// <returns>List of MatchupModel that represents the first round of the tournament.</returns>
        private List<MatchupModel> CreateFirstRound(List<TeamModel> teams, int fakeTeams)
        {
            List<MatchupModel> firstRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                currMatchup.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (fakeTeams > 0 || currMatchup.Entries.Count > 1)
                {
                    currMatchup.MatchupRound = 1;
                    firstRound.Add(currMatchup);
                    currMatchup = new MatchupModel();
                    if (fakeTeams > 0)
                    {
                        fakeTeams--;
                    }
                }
            }

            return firstRound;
        }

        /// <summary>
        /// Calculate the number of fake teams to complete a tournament.
        /// </summary>
        /// <param name="rounds">The number of rounds that it will be played in the tournament.</param>
        /// <param name="teams">The number of teams.</param>
        /// <returns>Return the number of fake teams needed to complete the tournament.
        /// Example: There are three teams with two rounds then function returns 1.</returns>
        private int CalculateFakeTeams(int rounds, int teams)
        {
            int fakeTeams = 0;
            int teamsNeeded = 1;
            for (int i = 0; i < rounds; i++)
            {
                teamsNeeded *= 2;
            }

            fakeTeams = teamsNeeded - teams;

            return fakeTeams;
        }

        /// <summary>
        /// Given a number of teams calculate the number of rounds it will be needed for the tournament.
        /// </summary>
        /// <param name="numberOfTeams">Number of teams added to the tournament.</param>
        /// <returns>The number of rounds the tournament will has.</returns>
        private int CalculateRounds(int numberOfTeams)
        {
            int rounds = 1;
            int teamsNeeded = 2;

            while (teamsNeeded < numberOfTeams)
            {
                rounds++;
                teamsNeeded *= 2;
            }
            return rounds;
        }

        
        /// <summary>
        /// Return the same list of teams that receive but with the teams randomized.
        /// </summary>
        /// <param name="teams">The list of teams added to the tournament.</param>
        /// <returns>The list of teams in random order.</returns>
        private List<TeamModel> RandomizeTeamList(List<TeamModel> teams)
        {
            // var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}