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
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        TournamentModel tournament = new TournamentModel();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        decimal entryFeeValue = 0;

        public CreateTournamentForm()
        {
            InitializeComponent();
        }

        private void NewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm();
            frm.Show();
        }

        private void NewPrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void CompletePrize(PrizeModel prize)
        {
            tournament.Prizes.Add(prize);
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

        public void CompleteTeam(TeamModel team)
        {
            tournament.EnteredTeams.Add(team);
        }
    }
}
