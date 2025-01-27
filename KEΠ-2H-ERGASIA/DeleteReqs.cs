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

        private void button2_Click(object sender, EventArgs e)
        {

            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Guid.TryParse(textBox1.Text, out var phoneNumber) || !await DbManager.DeleteRequest(phoneNumber))
                return;
            Close();
        }
    }
}
