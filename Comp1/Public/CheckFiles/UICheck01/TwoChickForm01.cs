using Comp1.Public.ReaderWriteFile02.ReaderSegment02;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.Public.CheckFiles.UICheck01
{
    public partial class TwoChickForm01 : Form
    {


        public int modNum = 8;
        public int SegmentLength = 256;
        public int KBSegmentLength = 0;
        public int SegmentTimes = 1;


        private SegmentReader02 SegmentReaderF1;
        private int NumSegmentOfFile1 = 0;
        private int CurrentSegmentF1=0;
        private string PathF1 = "";
        



      //  public 

        public TwoChickForm01()
        {
            

            InitializeComponent();
            SegmentReaderF1 = new SegmentReader02();
            RefreshView();

        }

        #region   My Method

        private void RefreshView()
        {
            textBox8.Text = SegmentTimes.ToString();
            textBox7.Text = modNum.ToString() ;
            textBox4.Text = SegmentLength.ToString();
           // textBox3.Text = KBSegmentLength.ToString();

            textBox1.Text = "";
            textBox1.Text = PathF1.ToString();
            NumOfSegment.Text = NumSegmentOfFile1.ToString();
            textBox6.Text = CurrentSegmentF1.ToString();









        }







        #endregion













        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

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



            textBox1.Text = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                textBox1.Text = file;
                //       MessageBox.Show(file);
            }

            PathF1 = textBox1.Text;



        }

        private void button2_Click(object sender, EventArgs e)
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



            textBox2.Text = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                textBox2.Text = file;
                //       MessageBox.Show(file);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.BackColor = System.Drawing.Color.White;
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                CurrentSegmentF1 = int.Parse(textBox6.Text);
            }
            catch
            {
                CurrentSegmentF1 = 0;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                modNum = int.Parse(textBox7.Text);
                SegmentLength = (Convert.ToInt32(Math.Pow(2, modNum)) * modNum) / 8  ;
                textBox4.Text = SegmentLength.ToString();

            }
            catch
            {
                textBox7.BackColor = System.Drawing.Color.Red;
                textBox4.BackColor = System.Drawing.Color.Red;

                modNum = 8;
                SegmentLength = Convert.ToInt32(Math.Pow(2, modNum)) ;
                
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                SegmentLength = int.Parse(textBox4.Text) ;
                
            }
            catch
            {
                textBox4.BackColor = System.Drawing.Color.Red;
                textBox3.BackColor = System.Drawing.Color.Red;
                SegmentLength = (Convert.ToInt32(Math.Pow(2, modNum)) * modNum) / 8;
                KBSegmentLength = Convert.ToInt32(SegmentLength / 1024);

               
            }
        }

        private void TwoChickForm01_Load(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                SegmentTimes = int.Parse(textBox8.Text);
            }
            catch
            {
                textBox8.BackColor = System.Drawing.Color.Red;
                SegmentTimes = 1;
             //   textBox8.Text = "1";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                KBSegmentLength = int.Parse(textBox3.Text);
                SegmentLength = Convert.ToInt32(KBSegmentLength * 1024);
                textBox4.Text = SegmentLength.ToString();
                
            }
            catch
            {
                textBox3.BackColor = System.Drawing.Color.Red;
                SegmentLength = (Convert.ToInt32(Math.Pow(2, modNum)) * modNum) / 8;
                KBSegmentLength = Convert.ToInt32(SegmentLength / 1024);
                textBox4.Text = SegmentLength.ToString();
               
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            try
            {
                NumOfSegment2.BackColor = Color.White;
                FileInfo filing = new FileInfo(textBox2.Text);
                long filingLength = filing.Length;
               // filingLength = filingLength * modNum;

                NumOfSegment2.Text = (filingLength / SegmentLength).ToString();
            }
            catch
            {
                NumOfSegment2.BackColor = Color.Red;

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                NumOfSegment.BackColor = Color.White;
                FileInfo filing = new FileInfo(textBox1.Text);
                long filingLength = filing.Length;
                // filingLength = filingLength * modNum;
                NumSegmentOfFile1 = Convert.ToInt32(filingLength / SegmentLength);
                NumOfSegment.Text = NumSegmentOfFile1.ToString();
            }
            catch
            {
                NumOfSegment.BackColor = Color.Red;
                NumSegmentOfFile1 = 0;
                NumOfSegment.Text = NumSegmentOfFile1.ToString();
                

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                richTextBox2.Clear();

            if (CurrentSegmentF1 <= 0)
                CurrentSegmentF1 = 0;

            SegmentReaderF1.GetReader(PathF1, SegmentLength, CurrentSegmentF1 );
            if (SegmentReaderF1.StateSeek)
            {
                richTextBox2.AppendText(BitsChecker.CheckerBits00.PrintAsLines(ref SegmentReaderF1.StreamData, modNum, 3).ToString());
                CurrentSegmentF1++;
                RefreshView();
                richTextBox2.BackColor = Color.White;

            }
            else
            {
                richTextBox2.BackColor = Color.Red;
            }
        }













    }
}
