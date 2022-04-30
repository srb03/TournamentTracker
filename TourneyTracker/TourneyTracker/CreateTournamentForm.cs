using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentTrackerLibrary;
using TournamentTrackerLibrary.Models;

namespace TourneyTracker
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        enum ErrorMessage
        {
            Success,
            NoTournamentName,
            NoEntryFee,
            NoTeamsAdded,
            NoPrizesCreated
        }

        TournamentModel newTournament = new TournamentModel();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        // Get all the teams from the database
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();

        decimal entryFeeValue = 0;

        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpSelectTeamComboBox();
        }

        private void WireUpSelectTeamComboBox()
        {
            SelectTeamComboBox.DataSource = null;
            SelectTeamComboBox.DataSource = availableTeams;
            SelectTeamComboBox.DisplayMember = "TeamName";
        }

        private void WireUpTeamSelectedListBox()
        {
            TeamsSelectedMatchupsListBox.DataSource = null;
            TeamsSelectedMatchupsListBox.DataSource = newTournament.EnteredTeams;
            TeamsSelectedMatchupsListBox.DisplayMember = "TeamName";
        }

        private void WireUpPrizeCreatedListBox()
        {
            PrizesCreatedListBox.DataSource = null;
            PrizesCreatedListBox.DataSource = newTournament.Prizes;
            PrizesCreatedListBox.DisplayMember = "PrizeNameInList";
        }

        private void NewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this, newTournament.EnteredTeams);
            frm.Show();
        }

        private void NewPrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }


        // Add the prize passed to the list of prizes in the tournament.
        // Refresh the list box of prizes created.
        public void CompletePrize(PrizeModel prize)
        {
            newTournament.Prizes.Add(prize);

            WireUpPrizeCreatedListBox();
        }

        private void EntryFeeTextBox_Leave(object sender, EventArgs e)
        {
            if (ValidateEntryFee())
            {
                EntryFeeTextBox.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", entryFeeValue);
            }
            else
            {
                MessageBox.Show(
                    "The entry fee is not valid.",
                    "Invalid entry fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                EntryFeeTextBox.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", 0);
            }
        }

        private bool ValidateEntryFee()
        {
            bool output = true;

            string EntryFeeString = EntryFeeTextBox.Text;
            try
            {
                entryFeeValue = Decimal.Parse(EntryFeeString, System.Globalization.NumberStyles.Currency);
            }
            catch
            {
                output = false;
            }

            if (entryFeeValue < 0)
            {
                output = false;
            }
            return output;
        }

        // Add the team passed to the list of team in the tournament.
        // Refresh the list box of team selected.
        public void CompleteTeam(TeamModel team)
        {
            newTournament.EnteredTeams.Add(team);

            WireUpTeamSelectedListBox();
        }

        private bool IsName()
        {
            if (TournamentNameTextBox.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Remove team from the selected team list box and add it to the available team combobox.
        private void RemoveTeamsSelectedButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)TeamsSelectedMatchupsListBox.SelectedItem;

            if (team != null)
            {
                newTournament.EnteredTeams.Remove(team);
                availableTeams.Add(team);

                WireUpSelectTeamComboBox();
                WireUpTeamSelectedListBox();
            }
            else
            {
                MessageBox.Show(
                    "There is no team selected. You must select a team to remove from it the list.",
                    "Error: Remove team",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemovePrizesCreatedButton_Click(object sender, EventArgs e)
        {
            PrizeModel prize = (PrizeModel)PrizesCreatedListBox.SelectedItem;

            if (prize != null)
            {
                newTournament.Prizes.Remove(prize);

                WireUpPrizeCreatedListBox();
            }
            else
            {
                MessageBox.Show(
                    "There is no prize selected. You must select a prize to remove it from the list.",
                    "Error: Remove prize",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel team = (TeamModel)SelectTeamComboBox.SelectedItem;

            if (team != null)
            {
                availableTeams.Remove(team);
                newTournament.EnteredTeams.Add(team);

                WireUpSelectTeamComboBox();
                WireUpTeamSelectedListBox();
            }
            else
            {
                MessageBox.Show(
                    "There is no team selected. You must select a team to add it to the tournament.",
                    "Error: Add team",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            switch (ValidateData())
            {
                case ErrorMessage.Success:
                    CreateTournament();
                    break;

                case ErrorMessage.NoTournamentName:
                    MessageBox.Show("There must be a name for the tournament.", "No Team Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case ErrorMessage.NoEntryFee:
                    DialogResult dResult = MessageBox.Show("It seems that you don't set the entry fee. Are you sure you want to continue?", 
                        "No entry fee", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning);
                    if (dResult == DialogResult.No)
                    {
                        break;
                    }
                    CreateTournament();
                    break;
                case ErrorMessage.NoTeamsAdded:
                    MessageBox.Show("The tournament doesn't has enought teams. A tournament must has at least 2 teams", 
                        "No enought teams", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    break;

                case ErrorMessage.NoPrizesCreated:
                    dResult = MessageBox.Show("It seems that you don't created a prize for the winners. Are you sure you want to continue?", 
                        "No prize created", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning);
                    if (dResult == DialogResult.No)
                    {
                        break;
                    }
                    CreateTournament();
                    break;


                default:
                    break;
            }
        }

        private void CreateTournament()
        {
            newTournament.TournamentName = TournamentNameTextBox.Text.Trim();
            newTournament.EntryFee = entryFeeValue;

            newTournament.CreateRounds();
            //GlobalConfig.Connection.CreateTournament(newTournament);

            this.Close();
        }


        private ErrorMessage ValidateData()
        {
            ErrorMessage error = ErrorMessage.Success;

            if (!IsName())
            {
                return error = ErrorMessage.NoTournamentName;
            }
            else if (entryFeeValue == 0)
            {
                error = ErrorMessage.NoEntryFee;
            }
            else if (newTournament.EnteredTeams.Count < 2)
            {
                error = ErrorMessage.NoTeamsAdded;
            }
            else if (newTournament.Prizes.Count == 0)
            {
                error = ErrorMessage.NoPrizesCreated;
            }

            return error;
        }
    }
}
