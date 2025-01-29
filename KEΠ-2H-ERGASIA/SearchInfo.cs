using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class SearchInfo : Form
    {
        public SearchInfo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(textBox1.Text, out var id))
            {
                MessageBox.Show("Το στοιχείο ID είναι λάθος. Παρακαλώ συμπληρώστε ξανά.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            var request = await DbManager.GetRequest(id);

            if (request == null)
                return;

            richTextBox1.Text = request.ToString();

            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            if (richTextBox1.Text == String.Empty)
            {
                button3.Enabled = true;
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "txt files (*.txt)|*.txt";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file;
                if ((file = fileDialog.OpenFile()) != null)
                {
                    var writer = new StreamWriter(file);
                    writer.Write(richTextBox1.Text);
                    writer.Dispose();
                }
            }
            button3.Enabled = true;
        }
    }
}
