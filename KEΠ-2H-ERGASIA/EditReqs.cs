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
    public partial class EditReqs : Form
    {
        public EditReqs()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
        }
    }
}
