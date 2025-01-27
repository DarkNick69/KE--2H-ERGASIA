using KEΠ_2H_ERGASIA.Db;
using System;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class AddInfo : Form
    {
        public AddInfo()
        {
            InitializeComponent();
            SubmissionTimeTextBox.Text = DateTime.Now.ToString("dd/MM/yy/HH:MM");
            PhoneTextBox.Text = "6";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text == String.Empty || EmailTextBox.Text == String.Empty || PhoneTextBox.Text == String.Empty
                || !long.TryParse(PhoneTextBox.Text, out var phoneNumber) || phoneNumber >= 1_000_000_000 ||
                phoneNumber < 100_000_000 ||BirthdayTextBox.Text == String.Empty || TypeTextBox.Text == String.Empty || AddressTextbox.Text == String.Empty)
                return;

            var request = new DbManager.Request(NameTextBox.Text,
                EmailTextBox.Text,
                phoneNumber,
                BirthdayTextBox.Text,
                TypeTextBox.Text,
                AddressTextbox.Text,
                SubmissionTimeTextBox.Text);

            await DbManager.InsertRequest(request);
            
            this.Close();
        }

        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
