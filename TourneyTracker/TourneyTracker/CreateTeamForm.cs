using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using TournamentTrackerLibrary.Models;
using TournamentTrackerLibrary;

namespace TourneyTracker
{
    public partial class CreateTeamForm : Form
    {
        enum ErrorMessage
        {
            Success,
            NoTeamName,
            IncompleteTeamMember,
            NoTeamMembersSelected
        }

        ITeamRequester callingForm;

        // Get all the persons from the database
        List<PersonModel> availableTeamMember = GlobalConfig.Connection.GetPerson_All();

        // This is the team model that will be returned to the CreateTournament form
        TeamModel newTeam = new TeamModel();

        public CreateTeamForm(ITeamRequester caller, List<TeamModel> selectedTeams)
        {
            // Save in a local variable the entire form to use the method CompleteTeam.
            InitializeComponent();

            callingForm = caller;

            WireUpSelectTeamMemberComboBox();

            WireUpSelectedTeamMembersListBox();

            GetAvailableTeamMembers(selectedTeams);
        }

        // Get the selected teams with their members as parameter .
        // Then it will remove the team members picked up for another team from the availableTeamMember list.
        private void GetAvailableTeamMembers(List<TeamModel> selectedTeams)
        {
            foreach (TeamModel team in selectedTeams)
            {
                foreach (PersonModel person in team.TeamMembers)
                {
                    availableTeamMember.Remove(person);
                }
            }
        }

        private void WireUpSelectTeamMemberComboBox()
        {
            SelectTeamMemberComboBox.DataSource = null;
            SelectTeamMemberComboBox.DataSource = availableTeamMember;
            SelectTeamMemberComboBox.DisplayMember = "FullName";
        }

        private void WireUpSelectedTeamMembersListBox()
        {
            TeamMembersSelectedListBox.DataSource = null;
            TeamMembersSelectedListBox.DataSource = newTeam.TeamMembers;
            TeamMembersSelectedListBox.DisplayMember = "FullName";
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            string errorMessage = ValidateData();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string emailAddress = EmailTextBox.Text.Trim();
            string cellphoneNumber = PhoneNumberTextBox.Text.Trim();

            List<char> chrToRemove = new List<char>(){' ', '-', '_', ',', ';', '.', ':'};
            cellphoneNumber = FilterString(cellphoneNumber, chrToRemove);

            PersonModel person = new PersonModel();

            person.FirstName = firstName;
            person.LastName = lastName;
            person.EmailAddress = emailAddress;
            person.CellphoneNumber = cellphoneNumber;

            GlobalConfig.Connection.CreatePerson(person);

            newTeam.TeamMembers.Add(person);
            // Update de TeamMembersSelectedListBox
            WireUpSelectedTeamMembersListBox();

            // Clean the form
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
        }

        private string ValidateData()
        {
            string output = "";

            if (!IsFirstName(FirstNameTextBox.Text))
            {
                output = "The first name is not valid";
            }
            else if (!IsLastName(LastNameTextBox.Text))
            {
               output = "The last name is not valid"; 
            }
            else if(!IsEmail(EmailTextBox.Text))
            {
                output = "The email is not valid";
            }
            else if (!IsPhoneNumber(PhoneNumberTextBox.Text))
            {
                output = "The phone number is not valid";
            }

            return output;
        }

        private bool IsPhoneNumber(string phone)
        {
            const string regularExp = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            if (string.IsNullOrEmpty(phone.Trim()))
            {
                return true;
            }

            return Regex.IsMatch(phone, regularExp);
        }

        private bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email.Trim()))
            {
                return true;
            }

            try
            {
                // Normalize domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        // Examines the domain part of the email and normalize it.
        string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert to unicode domain name.
            var idn = new IdnMapping();

            // Pull out and process domain names (throws ArgumentException on invalid)
            string domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }

        // Validate that the string is a first name. First name can't be null or "".
        // Not accept accents.
        private bool IsFirstName(string name)
        {
            const string regularExp = @"^[a-zA-ZáéíóúÁÉÍÓÚ\s]+$";

            if (string.IsNullOrEmpty(name.Trim()))
            {
                return false;
            }

            return Regex.IsMatch(name, regularExp, RegexOptions.CultureInvariant);
        }

        // Validate that the string is a last name. Last name can be null or "".
        // Not accept accents.
        private bool IsLastName(string name)
        {
            const string regularExp = @"^[a-zA-ZáéíóúÁÉÍÓÚ\s]+$";

            if (string.IsNullOrEmpty(name.Trim()))
            {
                return true;
            }

            return Regex.IsMatch(name, regularExp);
        }

        private string FilterString(string str, List<char> list)
        {
            foreach (char c in list)
            {
                str = str.Replace(c.ToString(), string.Empty);
            }
            return str;
        }

        private void CreateTeamButton_Click(object sender, EventArgs e)
        {
            switch (ValidateCreateTeam())
            {
                case ErrorMessage.Success:
                    CreateTeam();
                    break;

                case ErrorMessage.NoTeamName:
                    MessageBox.Show("There must be a name for the team.", "No Team Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case ErrorMessage.IncompleteTeamMember:
                    DialogResult dResult = MessageBox.Show("It seems that you want to create a team member but doesn't complete it. Are you sure you want yo continue?", "No Team Name", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.No)
                    {
                        break;
                    }
                    CreateTeam();
                    break;

                case ErrorMessage.NoTeamMembersSelected:
                    dResult = MessageBox.Show("You have no added team members to this team. Are you sure you want yo continue?", "Incomplete team member", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dResult == DialogResult.No)
                    {
                        break;
                    }
                    CreateTeam();
                    break;

                default:
                    break;
            }
        }

        private void CreateTeam()
        {
            // Set the team name
            newTeam.TeamName = TeamNameTextBox.Text.Trim();

            // There is no need to set the TeamMembers list because that is 
            // done everytime an team member is added or created.

            // We save the team in the data base.
            GlobalConfig.Connection.CreateTeam(newTeam);


            // Use the function CompleteTeam in the CreateTournament form.
            callingForm.CompleteTeam(newTeam);

            this.Close();
        }

        private ErrorMessage ValidateCreateTeam()
        {
            ErrorMessage error = ErrorMessage.Success;

            if (!IsName())
            {
                error = ErrorMessage.NoTeamName;
            }
            else if (IncompleteTeamMember())
            {
                error = ErrorMessage.IncompleteTeamMember;
            }
            else if (newTeam.TeamMembers.Count == 0)
            {
                error = ErrorMessage.NoTeamMembersSelected;
            }

            return error;
        }

        // True if there's some data in the form to create a new team member
        private bool IncompleteTeamMember()
        {
            if (FirstNameTextBox.Text.Trim().Length > 0 || 
                LastNameTextBox.Text.Trim().Length > 0 || 
                EmailTextBox.Text.Trim().Length > 0 || 
                PhoneNumberTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddTeamMemberButton_Click(object sender, EventArgs e)
        {
            // Get the person selected in the combo box
            PersonModel person = (PersonModel)SelectTeamMemberComboBox.SelectedItem;

            if (person != null)
            {
                // Remove it from the list of availble people
                availableTeamMember.Remove(person);

                // Add it to the list of person of this new team
                newTeam.TeamMembers.Add(person);

                // Refresh the combo box and the list box
                WireUpSelectTeamMemberComboBox();
                WireUpSelectedTeamMembersListBox(); 
            }
            else
            {
                MessageBox.Show(
                    "There is no person selected. You must select a person to add it to the team.",
                    "Error: Add person to the team",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RemoveTeamMemberSelectedButton_Click(object sender, EventArgs e)
        {
            // Get the person selected in the list box
            PersonModel person = (PersonModel)TeamMembersSelectedListBox.SelectedItem;

            if (person != null)
            {
                // Remove it from the list of selected people
                newTeam.TeamMembers.Remove(person);

                // Add it to the list of person available
                availableTeamMember.Add(person);

                // Refresh the combo box and the list box
                WireUpSelectTeamMemberComboBox();
                WireUpSelectedTeamMembersListBox();
            }
            else
            {
                MessageBox.Show(
                    "There is no person selected. You must select a person to remove from the list.", 
                    "Error: Remove person", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private bool IsName()
        {
            if (TeamNameTextBox.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
