using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentTrackerLibrary.Models;

namespace TourneyTracker
{
    public partial class TournamentViewerForm : Form
    {
        List<int> rounds = new List<int>();
        
        // All the matchups in the current round
        List<MatchupModel> AllMatchupsList = new List<MatchupModel>();

        // All the matchups to show in the list
        List<MatchupModel> matchupsToShowList = new List<MatchupModel>();

        // The actual tournament object
        TournamentModel tournament = new TournamentModel();

        public TournamentViewerForm(TournamentModel tournamentPassed)
        {
            tournament = tournamentPassed;

            InitializeComponent();

            SetHeaderLabel();

            LoadRounds();
        }

        private void FillMatchupListBox()
        {
            MatchupsListBox.DataSource = AllMatchupsList;
            MatchupsListBox.DisplayMember = "MatchupDisplay";
        }

        private void RoundsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int selectedRound = (int)RoundsComboBox.SelectedItem;
            //AllMatchupsList = tournament.Rounds.Where(x => x.First().MatchupRound == selectedRound).First();

            if (UnplayedMatchesCheckBox.Checked)
            {
                List<MatchupModel> unplayedMatchups = AllMatchupsList.Where(x => x.Winner == null).ToList();
                AllMatchupsList = unplayedMatchups;
            }
            else
            {
                int selectedRound = (int)RoundsComboBox.SelectedItem;
                AllMatchupsList = tournament.Rounds.Where(x => x.First().MatchupRound == selectedRound).First();
            }

            
            FillMatchupListBox();

            LoadScoreMatchup();
        }

        
        private void LoadScoreMatchup()
        {
            MatchupModel matchup = (MatchupModel)MatchupsListBox.SelectedItem;

            if (matchup != null && matchup.Entries[0].TeamCompeting != null)
            {
                TeamOneLabel.Text = matchup.Entries[0].TeamCompeting.TeamName;
                TeamOneTextBox.Text = matchup.Entries[0].Score.ToString();

                if (matchup.Entries.Count > 1)
                {
                    if (matchup.Entries[1].TeamCompeting != null)
                    {
                        TeamTwoLabel.Text = matchup.Entries[1].TeamCompeting.TeamName;
                        TeamTwoTextBox.Text = matchup.Entries[1].Score.ToString(); 
                    }
                }
                else
                {
                    HideTeamTwoScore(false);
                } 
            }
            else
            {
                HideTeamOneScore(false);
                HideTeamTwoScore(false);
            }
        }

        private void HideTeamOneScore(bool hide)
        {
            VersusLabel.Visible = hide;

            TeamOneLabel.Visible = hide;

            TeamOneTextBox.Visible = hide;
        }

        private void HideTeamTwoScore(bool hide)
        {
            VersusLabel.Visible = hide;

            TeamTwoLabel.Visible = hide;

            TeamTwoTextBox.Visible = hide;
        }


        private void FillRoundComboBox()
        {
            RoundsComboBox.DataSource = rounds;
        }

        private void LoadRounds()
        {
            rounds.Clear();

            for (int i = 1; i <= tournament.Rounds.Count; i++)
            {
                rounds.Add(i);
            }

            FillRoundComboBox();
        }

        private void SetHeaderLabel()
        {
            TitleHeaderLabel.Text = "Tournament: " + tournament.TournamentName;
        }

        private void MatchupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideTeamOneScore(true);
            HideTeamTwoScore(true);
            LoadScoreMatchup();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateScore())
            {
                int scoreTeamOne = 0;
                int scoreTeamTwo = 0;

                bool validScoreTeamOne = int.TryParse(TeamOneTextBox.Text, out scoreTeamOne);
                bool validScoreTeamTwo = int.TryParse(TeamTwoTextBox.Text, out scoreTeamTwo);

                MatchupModel matchup = (MatchupModel)MatchupsListBox.SelectedItem;

                for (int i = 0; i < matchup.Entries.Count; i++)
                {
                    if (i == 0)
                    {
                        matchup.Entries[i].Score = scoreTeamOne;
                    }

                    if (i == 1)
                    {
                        matchup.Entries[i].Score = scoreTeamTwo;
                    }
                }

                tournament.UpdateScores();
            }
            else
            {
                MessageBox.Show("The score is not valid.", 
                    "Invalid score.", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Validates the scores that are available.
        /// </summary>
        /// <returns></returns>
        private bool ValidateScore()
        {
            bool isValidScore = true;

            if (TeamOneTextBox.Visible)
            {
                try
                {
                    int score = 0;
                    isValidScore = int.TryParse(TeamOneTextBox.Text, out score);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if (TeamTwoTextBox.Visible)
            {
                try
                {
                    int score = 0;
                    isValidScore = int.TryParse(TeamTwoTextBox.Text, out score);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return isValidScore;
        }

        private void UnplayedMatchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            List<MatchupModel> unplayedMatchups = new List<MatchupModel>();

            if (UnplayedMatchesCheckBox.Checked)
            {
                unplayedMatchups = AllMatchupsList.Where(x => x.Winner == null).ToList();
                AllMatchupsList = unplayedMatchups;
            }
            else
            {
                int selectedRound = (int)RoundsComboBox.SelectedItem;
                AllMatchupsList = tournament.Rounds.Where(x => x.First().MatchupRound == selectedRound).First();
            }

            FillMatchupListBox();
        }


    }
}
