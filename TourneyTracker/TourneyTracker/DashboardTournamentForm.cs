using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourneyTracker
{
    public partial class DashboardTournamentForm : Form
    {
        public DashboardTournamentForm()
        {
            InitializeComponent();
        }

        private void LoadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentViewerForm frm = new TournamentViewerForm();
            //this.Enabled = false;
            frm.Show();
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            NewTournamentForm frm = new NewTournamentForm();
            frm.Show();
        }
    }
}
