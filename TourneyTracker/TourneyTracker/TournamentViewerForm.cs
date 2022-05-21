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
        
        
        List<MatchupModel> matchupsList = new List<MatchupModel>();
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
            MatchupsListBox.DataSource = matchupsList;
            MatchupsListBox.DisplayMember = "MatchupDisplay";
        }

        private void RoundsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRound = (int)RoundsComboBox.SelectedItem;
            matchupsList = tournament.Rounds.Where(x => x.First().MatchupRound == selectedRound).First();
            FillMatchupListBox();

            LoadScoreMatchup();
        }

        
        private void LoadScoreMatchup()
        {
            MatchupModel matchup = (MatchupModel)MatchupsListBox.SelectedItem;

            TeamOneLabel.Text = matchup.Entries[0].TeamCompeting.TeamName;
            TeamOneTextBox.Text = matchup.Entries[0].Score.ToString();

            if (matchup.Entries.Count > 1)
            {
                TeamTwoLabel.Text = matchup.Entries[1].TeamCompeting.TeamName;
                TeamTwoTextBox.Text = matchup.Entries[1].Score.ToString();
            }
            else
            {
                TeamTwoLabel.Text = "Direct pass";
            }
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
            LoadScoreMatchup();
        }
    }
}
