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