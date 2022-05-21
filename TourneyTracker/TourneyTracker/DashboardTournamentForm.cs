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
    public partial class DashboardTournamentForm : Form
    {
        List<TournamentModel> availableTournament = GlobalConfig.Connection.GetTournament_All();

        public DashboardTournamentForm()
        {
            InitializeComponent();

            WireUpAvailableTournamentComboBox();
        }

        private void WireUpAvailableTournamentComboBox()
        {
            AvailableTournamentsComboBox.DataSource = null;
            AvailableTournamentsComboBox.DataSource = availableTournament;
            AvailableTournamentsComboBox.DisplayMember = "TournamentName";
        }

        private void LoadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tournament = (TournamentModel)AvailableTournamentsComboBox.SelectedItem;

            TournamentViewerForm frm = new TournamentViewerForm(tournament);
            frm.Show();
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new CreateTournamentForm();
            frm.Show();
        }
    }
}
