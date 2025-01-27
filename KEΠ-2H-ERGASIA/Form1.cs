using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEΠ_2H_ERGASIA
{
    public partial class Main : Form
    {    
        public Main()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            var addInfo = new AddInfo();
            addInfo.ShowDialog();
            addInfo.Dispose();
            Show();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string message = "Made by P23255, Νικόλαος, Σταμπουλίδης, Ρ23020, Νικόλαος, Βογιατζάκης, Ρ23215 Άλντια, Ντερβίσι";
            string title = "Info";
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            ShowReqs showReqs = new ShowReqs();
            showReqs.ShowDialog();
            showReqs.Dispose();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            DeleteReqs deleteReqs = new DeleteReqs();
            deleteReqs.ShowDialog();
            deleteReqs.Dispose();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            EditReqs editReqs = new EditReqs();
            editReqs.ShowDialog();
            editReqs.Dispose();
            Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.ShowDialog();
            searchInfo.Dispose();
            Show();
        }
    }
}
