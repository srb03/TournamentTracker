using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentTrackerLibrary.Models;
using System.Collections.Generic;

namespace LibraryTests
{
    [TestClass]
    public class TournamentTests
    {
        [TestMethod]
        public void CalculateRounds_Tests()
        {
            // Arrange
            int[] numberOfTeams = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 32 };
            int[] expectedRound = { 1, 2, 2, 3, 3, 3, 3, 4, 4, 5 };

            // Act and Assert
            for (int i = 0; i < numberOfTeams.Length; i++)
            {
                TournamentModel tournament = new TournamentModel();
                Assert.AreEqual(expectedRound[i], tournament.CalculateRounds(numberOfTeams[i]));
            }
        }

        [TestMethod]
        public void CalculateFakeTeams_Tests()
        {
            // Arrange
            int[] numberOfTeams = { 2, 3, 4, 5, 8, 9, 16, 17, 32, 33};
            int[] numberOfRound = { 1, 2, 2, 3, 3, 4, 4,  5,  5,  6};
            
            int[] fakeExpected  = { 0, 1, 0, 3, 0, 7, 0,  15, 0,  31};

            // Act and Assert
            for (int i = 0; i < numberOfTeams.Length; i++)
            {
                TournamentModel t = new TournamentModel();
                Assert.AreEqual(fakeExpected[i], t.CalculateFakeTeams(numberOfRound[i], numberOfTeams[i]));
            }
        }

        [TestMethod]
        public void CreateFirstRound_Tests()
        {
            // Arrange
            TournamentModel t = new TournamentModel();
            TeamModel team1 = new TeamModel();
            team1.TeamName = "Team One";
            TeamModel team2 = new TeamModel();
            team2.TeamName = "Team Two";
            t.EnteredTeams.Add(team1);
            t.EnteredTeams.Add(team2);
            int fakeTeams = 0;

            List<MatchupModel> expectedFirstRound = new List<MatchupModel>();
            MatchupModel matchup = new MatchupModel();
            matchup.Entries.Add(new MatchupEntryModel{ TeamCompeting = team1 });
            matchup.Entries.Add(new MatchupEntryModel{ TeamCompeting = team2 });
            expectedFirstRound.Add(matchup);

            // Act and Assert
            for (int i = 0; i < 2; i++)
            {
                
            }
            Assert.AreEqual(expectedFirstRound, t.CreateFirstRound(t, fakeTeams));
        }
    }
}
