using Comp1.Public.ReaderFile.ReaderWriteFile02;
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

namespace Comp1.Public.ReaderWriteFile02
{
    public partial class FileReaderForm02 : Form
    {
        private int modNum = 8;
        private int SegmentLength = 256;
        private int KBSegmentLength = 0;

        private int ReaderLength = 256;

        public FileReaderInfo02 ReaderFile = new FileReaderInfo02();
        public string SaveExtension = "";


        public FileReaderForm02()
        {
            InitializeComponent();
        }

        public FileReaderInfo02 Start()
        {
            this.ShowDialog();


            return ReaderFile;
        }

        private void RefrishInfo()
        {
            label21.Text = ReaderFile.FileName.ToString();
            label20.Text = ReaderFile.FileExtension.ToString();
            label19.Text = ReaderFile.PathFile.ToString();
            label14.Text = ReaderFile.FileDir.ToString();
            label25.Text = ReaderFile.FileSize.ToString();

            label26.Text = ReaderFile.SaveName.ToString();
            label31.Text = ReaderFile.SaveExtension.ToString();
            label32.Text = ReaderFile.SaveFileDir.ToString();

            label33.Text = ReaderFile.StopNumLength.ToString();
            label37.Text = ReaderFile.ReaderDataLength.ToString();


        }





        private void FileReaderForm02_Load(object sender, EventArgs e)
        {
            textBox4.Text = SaveExtension;
           // textBox5.Text = ReaderFile.DataReadLength.ToString();

            //textBox7.Text = modNum.ToString();
            textBox11.Text = "8";
            textBox8.Text = "8";

            timer1.Start();

            GetSetting();
            
        }

        private void GetSetting()
        {
            try
            {
                textBox1.Text = ReaderFileSettings02.Default.MainFilePath;
                if (ReaderFileSettings02.Default.CreateInFolder)
                {
                    checkBox1.Checked = true;
                }
                textBox12.Text = ReaderFileSettings02.Default.ReaderLength.ToString();
                textBox5.Text = ReaderFileSettings02.Default.StopLength.ToString();

            }
            catch
            {
                textBox1.Text ="";
                checkBox1.Checked = true;
                
                textBox12.Text = "256";
                textBox5.Text = "256";
            }
        }
        private void UpdateSetting()
        {
            //MainFile
            try
            {
                ReaderFileSettings02.Default.MainFilePath = ReaderFile.PathFile;
            }
            catch
            {
                ReaderFileSettings02.Default.MainFilePath = "";
            }
            //CreatInFolder
            try
            {
                if (checkBox1.Checked)
                {
                    ReaderFileSettings02.Default.CreateInFolder = true;
                }
                else
                {
                    ReaderFileSettings02.Default.CreateInFolder = false ;
                }
            }
            catch
            {
                ReaderFileSettings02.Default.CreateInFolder = false;
            }
            //LengthReader
            try
            {
                ReaderFileSettings02.Default.ReaderLength = ReaderFile.ReaderDataLength;
            }
            catch
            {
                ReaderFileSettings02.Default.ReaderLength = 256;
            }
            //LengthStop
            try
            {
                ReaderFileSettings02.Default.StopLength = ReaderFile.StopNumLength;
            }
            catch
            {
                ReaderFileSettings02.Default.StopLength = 256;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.Title = "Browse Files";


            // openFileDialog1.InitialDirectory = @"C:\Users\ALI\Desktop";

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
               

            }

            /*  DialogResult result = openFileDialog1.ShowDialog();
              if ( result == DialogResult.OK)
              {
                  OpenSomeFile(openFileDialog1.FileName);
              }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                try
                {
                    ReaderFile = new FileReaderInfo02(textBox1.Text);

                    textBox1.BackColor = Color.White;
                    string file = textBox1.Text;
                    label1.Text = ReaderFile.FileExtension ;


                    //Size
                    {
                        label3.Text = ReaderFile.FileName;
                        long sizefile = ReaderFile.FileSize;
                        StringBuilder ss = new StringBuilder();
                        ss.Append("Size = " + (sizefile / (1024 * 1024)).ToString() + " MB = " + (sizefile / 1024).ToString() + " KB = " + sizefile.ToString() + " Byte ");
                        label2.Text = ss.ToString();

                        //SaveFile
                        textBox2.Text = ReaderFile.SaveFileDir;
                        textBox3.Text = ReaderFile.FileName;

                    }

                  //  ReaderFile.ReaderReady = true;
                    checkBox1.Checked = false;

                }
                catch
                {
                    textBox1.BackColor = Color.Red;
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                    textBox2.Text = "";
                   // ReaderFile.ReaderReady = false;




                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
   
          ////  saveFileDialog1.Title = "Browse Files";


          //  // openFileDialog1.InitialDirectory = @"C:\Users\ALI\Desktop";

          //  saveFileDialog1.DefaultExt = "txt";

          //  saveFileDialog1.FileName = textBox3.Text;
          //  saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
          //  saveFileDialog1.FilterIndex = 3;



          //  saveFileDialog1.CheckFileExists = true;
          //  saveFileDialog1.CheckPathExists = true;

          //  //  openFileDialog1.ReadOnlyChecked = true;
          //  // this.openFileDialog1.Multiselect = true;
          //  //openFileDialog1.ShowReadOnly = true;

          //  saveFileDialog1.ShowDialog();



          //  // textBox3.Text = openFileDialog1.FileName;


          //  foreach (String file in saveFileDialog1.FileNames)
          //  {
          //      textBox1.Text = file;
          //      //MessageBox.Show(file);
          //  }




            folderBrowserDialog1.ShowDialog();

            textBox2.Text = folderBrowserDialog1.SelectedPath;
            ReaderFile.SaveFileDir = folderBrowserDialog1.SelectedPath;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ReaderFile == null)
                return;
            try
            {
                if (checkBox1.Checked)
                    textBox2.Text = ReaderFile.SaveFileDir + @"\" + ReaderFile.FileName;
                else
                    textBox2.Text = ReaderFile.SaveFileDir;

            }
            catch
            {
                textBox2.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReaderFile.ReaderReady = false ;

            //Maybe Problem 00
            ReaderFile = null;
            this.Close();
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
                SegmentLength = int.Parse(textBox5.Text);
                ReaderFile.StopNumLength = SegmentLength;
                //KBSegmentLength = SegmentLength / 1024;
                //textBox6.Text = KBSegmentLength.ToString();

            }
            catch
            {
                textBox5.BackColor = System.Drawing.Color.Red;
                ReaderFile.StopNumLength = 256;
                SegmentLength = ReaderFile.StopNumLength;
                //KBSegmentLength = SegmentLength / 1024;
                //textBox6.Text = KBSegmentLength.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ReaderFile.SaveExtension = textBox4.Text;
                ReaderFile.SaveFileDir = textBox2.Text;
                ReaderFile.SaveName = textBox3.Text;

                ReaderFile.ReaderReady = true;
         //       ReaderFile.IsCancel = false;

                this.Close();
            }
            catch
            {
                ReaderFile.ReaderReady = false ;
                MessageBox.Show("There is an error   !!");


            }
            timer1.Stop();

            UpdateSetting();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

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
                SegmentLength = (Convert.ToInt32(Math.Pow(2, modNum)) * modNum) / 8;
                textBox5.Text = SegmentLength.ToString();

            }
            catch
            {
                textBox7.BackColor = System.Drawing.Color.Red;
                ReaderFile.StopNumLength = 256;
                SegmentLength = ReaderFile.StopNumLength;

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

                KBSegmentLength = int.Parse(textBox6.Text);
                SegmentLength = KBSegmentLength * 1024;
                textBox5.Text = SegmentLength.ToString();

            }
            catch
            {
                textBox6.BackColor = System.Drawing.Color.Red;
                ReaderFile.StopNumLength = 256;
                SegmentLength = ReaderFile.StopNumLength;

            }
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

                int mod = int.Parse(textBox8.Text);
                mod = Convert.ToInt32(Math.Pow(2, mod));
                textBox5.Text = mod.ToString();

            }
            catch
            {
                textBox8.BackColor = System.Drawing.Color.Red;
                

            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox9.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int times = int.Parse(textBox9.Text);
                

            }
            catch
            {
                textBox9.BackColor = System.Drawing.Color.Red;
               

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int times = int.Parse(textBox9.Text);
                SegmentLength = SegmentLength * times;
                textBox5.Text = SegmentLength.ToString();
                textBox12.Text = SegmentLength.ToString();

            }
            catch
            {
                textBox9.BackColor = System.Drawing.Color.Red;
                ReaderFile.StopNumLength = 256;
                SegmentLength = ReaderFile.StopNumLength;

            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox10.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int MB = int.Parse(textBox10.Text);
                KBSegmentLength = MB * 1024;
                textBox6.Text = KBSegmentLength.ToString();

            }
            catch
            {
                textBox10.BackColor = System.Drawing.Color.Red;
                

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            textBox12.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {
                ReaderLength = int.Parse(textBox12.Text);
                ReaderFile.ReaderDataLength = ReaderLength;
              
            }
            catch
            {
                textBox12.BackColor = System.Drawing.Color.Red;
                ReaderFile.ReaderDataLength = 256;
                ReaderLength = ReaderFile.ReaderDataLength;
               
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            textBox11.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox11.Text);

                textBox12.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox11.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            textBox13.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox13.Text);

                textBox12.Text = (Temp * 1024).ToString();

            }
            catch
            {
                textBox13.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            textBox14.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = (int.Parse(textBox14.Text));

                int n = 1024 * Temp;
                textBox13.Text = n.ToString();

            }
            catch
            {
                textBox14.BackColor = System.Drawing.Color.Red;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefrishInfo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefrishInfo();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ReaderFile.SaveExtension = textBox4.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ReaderFile.SaveName = textBox3.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ReaderFile.SaveFileDir = textBox2.Text;
        }









    }
}
