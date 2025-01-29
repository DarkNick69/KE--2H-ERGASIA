using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class EditReqs : Form
    {

        private DbManager.Request _request;

        private Regex _emailRegex = new Regex(@".+@.+\..+");


        public EditReqs()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox1.Enabled = false;
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(textBox8.Text, out var id))
            {
                MessageBox.Show("Το στοιχείο ID είναι λάθος. Παρακαλώ συμπληρώστε ξανά.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var request = await DbManager.GetRequest(id);
            
            if (request == null)
                return;

            _request = request;
            NameTextBox.Text = _request.Name;
            EmailTextBox.Text = _request.Email;
            TelephoneTextBox.Text = _request.PhoneNumber.ToString();
            dateTimePicker1.Text = _request.Birthday;
            RequestTypeTextBox.Text = _request.RequestType;
            AddressTextBox.Text = _request.Address;
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            if (NameTextBox.Text == String.Empty || EmailTextBox.Text == String.Empty || !_emailRegex.IsMatch(EmailTextBox.Text) ||
                TelephoneTextBox.Text == String.Empty
                || !long.TryParse(TelephoneTextBox.Text, out var phoneNumber) || phoneNumber > 9_999_999_999 ||
                phoneNumber < 1_000_000_000 ||
                RequestTypeTextBox.Text == String.Empty || AddressTextBox.Text == String.Empty)
            {
                MessageBox.Show("Τα στοιχεία που θέλετε να αλλάξετε είναι λάθος. Παρακαλώ συμπληρώστε ξανά.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button4.Enabled = true;
                return;
            }
            
            
            _request.Name = NameTextBox.Text;
            _request.Email = EmailTextBox.Text;
            _request.PhoneNumber = phoneNumber;
            _request.Birthday = dateTimePicker1.Text;
            _request.RequestType = RequestTypeTextBox.Text;
            _request.Address = AddressTextBox.Text;
            
            await DbManager.UpdateRequest(_request);
            MessageBox.Show("Τα στοιχεία ενημερώθηκαν επιτυχώς.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
