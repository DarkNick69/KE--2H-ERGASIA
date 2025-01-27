using KEΠ_2H_ERGASIA.Db;
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
    public partial class DeleteReqs : Form
    {
        public DeleteReqs()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(maskedTextBox1.Text, out var phoneNumber) || !await DbManager.DeleteRequest(phoneNumber))
                return;
            this.Close();
        }
    }
}
