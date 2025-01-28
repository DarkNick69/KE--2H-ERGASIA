using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class ShowReqs : Form
    {
        public ShowReqs()
        {
            InitializeComponent();
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            textBox1.Enabled = false;
            textBox1.Visible = false;
            label2.Visible = false;
            label2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Enabled = true;
                textBox1.Visible = true;
                label2.Visible = true;
                label2.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Enabled = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label2.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            IEnumerable<DbManager.Request> requests;
            if (radioButton1.Checked)
            {
                requests = await DbManager.GetAllRequests();
               
            } else
            {
                requests = await DbManager.GetRequestsByName(textBox1.Text);
            }

            richTextBox1.Text = string.Empty;

            foreach (var request in requests)
            {
                richTextBox1.Text += request.ToString();
                richTextBox1.Text += "\n-----\n";
            }
        }
    }
}
