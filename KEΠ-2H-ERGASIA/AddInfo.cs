using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class AddInfo : Form
    {
        private Guid _id = Guid.NewGuid();

        private Regex _emailRegex = new Regex(@".+@.+\..+");
        public AddInfo()
        {
            InitializeComponent();
            SubmissionTimeTextBox.Text = DateTime.Now.ToString("dd/MM/yy/HH:MM");
            PhoneTextBox.Text = "6";
            IdTextBox.Text = _id.ToString();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (NameTextBox.Text == String.Empty || EmailTextBox.Text == String.Empty || !_emailRegex.IsMatch(EmailTextBox.Text) ||
                PhoneTextBox.Text == String.Empty
                || !long.TryParse(PhoneTextBox.Text, out var phoneNumber) || phoneNumber > 9_999_999_999 ||
                phoneNumber < 1_000_000_000 || BirthdayTextBox.Text == String.Empty ||
                TypeTextBox.Text == String.Empty || AddressTextbox.Text == String.Empty)
            {
                MessageBox.Show("Τα στοιχεία εγγραφής είναι λάθος. Παρακαλώ συμπληρώστε ξανά.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = true;
                return;
            }

            var request = new DbManager.Request(
                _id,
                NameTextBox.Text,
                EmailTextBox.Text,
                phoneNumber,
                BirthdayTextBox.Text,
                TypeTextBox.Text,
                AddressTextbox.Text,
                SubmissionTimeTextBox.Text);

            await DbManager.InsertRequest(request);
            MessageBox.Show("Τα στοιχεία συμπληρώθηκαν επιτυχώς.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


            Close();
        }

        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
