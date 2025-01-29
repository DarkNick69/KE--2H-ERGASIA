using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Previewer;
using QuestPDF.Companion;

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

            fileDialog.Filter = "txt files (*.txt)|*.txt|pdf files (*.pdf)|*.pdf";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream file;

                if (fileDialog.FileName.EndsWith(".pdf"))
                {
                    WritePdf(fileDialog.FileName);
                } else if ((file = fileDialog.OpenFile()) != null)
                {
                        WriteTxt(file);
                        file.Close();
                }
            }
            button3.Enabled = true;
        }

        private void WritePdf(string file)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Content().Column(x =>
                    {
                        x.Item().Text(richTextBox1.Text);
                    });
                });

            }).GeneratePdf(file);
        }
        private void WriteTxt(Stream file)
        {
            var writer = new StreamWriter(file);
            writer.Write(richTextBox1.Text);
        }
    }
}
