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
        ITeamRequester callingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
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

        }
    }
}
