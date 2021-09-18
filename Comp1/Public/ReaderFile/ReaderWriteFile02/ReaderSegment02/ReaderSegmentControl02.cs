using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Comp1.Public.ReaderWriteFile02.ReaderSegment02
{
    public partial class ReaderSegmentControl02 : UserControl
    {
        public ReaderSegmentControl02()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                //ReaderFile.DataReadLength = int.Parse(textBox1.Text);
            }
            catch
            {
                //ReaderFile.DataReadLength = 256;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                //ReaderFile.DataReadLength = int.Parse(textBox1.Text);
            }
            catch
            {
                //ReaderFile.DataReadLength = 256;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.DefaultExt = "txt";

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;



            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.Multiselect = true;
            openFileDialog1.ShowReadOnly = true;
            openFileDialog1.ShowDialog();



            textBox4.Text = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                textBox4.Text = file;
                //       MessageBox.Show(file);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
