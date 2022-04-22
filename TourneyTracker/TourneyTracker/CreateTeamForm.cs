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
using TournamentTrackerLibrary;

namespace TourneyTracker
{
    public partial class CreateTeamForm : Form
    {
        ITeamRequester callingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            // TODO - Validate every fucking field
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string emailAddress = EmailTextBox.Text;
            string cellphoneNumber = PhoneNumberTextBox.Text;
            
            PersonModel person = new PersonModel();
            
            person.FirstName = firstName;
            person.LastName = lastName;
            person.EmailAddress = emailAddress;
            person.CellphoneNumber = cellphoneNumber;

            GlobalConfig.Connection.CreatePerson(person);

            this.Close();
        }
    }
}
