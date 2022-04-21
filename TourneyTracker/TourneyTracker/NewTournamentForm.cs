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
    public partial class NewTournamentForm : Form
    {
        public NewTournamentForm()
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
            CreatePrizeForm frm = new CreatePrizeForm();
            frm.Show();
        }
    }
}
