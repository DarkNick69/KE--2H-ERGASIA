using System;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class EditReqs : Form
    {

        private DbManager.Request _request;

        private bool _updating;
        
        public EditReqs()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox1.Enabled = false;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(textBox8.Text, out var id))
                return;
            
            var request = await DbManager.GetRequest(id);
            
            if (!request.found)
                return;

            _request = request.request;
            NameTextBox.Text = _request.Name;
            EmailTextBox.Text = _request.Email;
            TelephoneTextBox.Text = _request.PhoneNumber.ToString();
            BirthdayTextBox.Text = _request.Birthday;
            RequestTypeTextBox.Text = _request.RequestType;
            AddressTextBox.Text = _request.Address;
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (_updating || NameTextBox.Text == String.Empty || EmailTextBox.Text == String.Empty || TelephoneTextBox.Text == String.Empty
                || !long.TryParse(TelephoneTextBox.Text, out var phoneNumber) || phoneNumber > 9_999_999_999 ||
                phoneNumber < 1_000_000_000  ||BirthdayTextBox.Text == String.Empty || RequestTypeTextBox.Text == String.Empty || AddressTextBox.Text == String.Empty)
                return;
            
            _updating = true;
            
            _request.Name = NameTextBox.Text;
            _request.Email = EmailTextBox.Text;
            _request.PhoneNumber = phoneNumber;
            _request.Birthday = BirthdayTextBox.Text;
            _request.RequestType = RequestTypeTextBox.Text;
            _request.Address = AddressTextBox.Text;
            
            await DbManager.UpdateRequest(_request);
            Close();
        }
    }
}
