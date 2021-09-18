using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Comp1.Public.ReaderWriterFile
{
    public partial class FileReaderForm : Form
    {
        public bool FileState = false;
        public long FileSize = 0;
        //public string Name = "";
        public string Extension= "0";
        public string PathFile;
        public string SaveFilePath;
        public int DataReadLength = 256; 





        public FileReaderForm()
        {
            InitializeComponent();
        }

        private void FileReaderForm_Load(object sender, EventArgs e)
        {

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
                //MessageBox.Show(file);
                if (textBox1.Text != null)
                {
                    try
                    {
                        textBox1.BackColor = Color.White;
                        textBox1.Text = file;
                        FileInfo fil = new FileInfo(file);
                        label1.Text = fil.Extension;


                        //Size
                        {
                            label3.Text = fil.Name;
                            long sizefile = fil.Length;
                            StringBuilder ss = new StringBuilder();
                            ss.Append("Size = " + (sizefile / (1024 * 1024)).ToString() + " MB = " + (sizefile / 1024).ToString() + " KB = " + sizefile.ToString() + " Byte ");
                            label2.Text = ss.ToString();
                            FileSize = fil.Length;

                            //SaveFile
                            textBox2.Text = fil.FullName.Remove(fil.FullName.Length - fil.Name.Length);
                            textBox3.Text = fil.Name.Remove(fil.Name.Length - fil.Extension.Length);

                        }

                        FileState = true;

                    }
                    catch
                    {
                        textBox1.BackColor = Color.Red;
                        label1.Text = "";
                        label2.Text = "";
                        label3.Text = "";
                        textBox2.Text = "";
                        FileState = false;




                    }



                }

            }

            /*  DialogResult result = openFileDialog1.ShowDialog();
              if ( result == DialogResult.OK)
              {
                  OpenSomeFile(openFileDialog1.FileName);
              }*/
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Browse Files";


            // openFileDialog1.InitialDirectory = @"C:\Users\ALI\Desktop";

            saveFileDialog1.DefaultExt = "txt";

            saveFileDialog1.FileName = textBox3.Text;
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 3;



            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;

            //  openFileDialog1.ReadOnlyChecked = true;
            // this.openFileDialog1.Multiselect = true;
            //openFileDialog1.ShowReadOnly = true;

            saveFileDialog1.ShowDialog();



            // textBox3.Text = openFileDialog1.FileName;


            foreach (String file in saveFileDialog1.FileNames)
            {
                textBox1.Text = file;
                //MessageBox.Show(file);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                PathFile = textBox1.Text;
                SaveFilePath = textBox2.Text + "/" + textBox3.Text + "." + textBox4.Text;
                if (textBox5.Text != "")
                    DataReadLength = int.Parse(textBox5.Text);

                this.Close();

            }
            catch
            {

                MessageBox.Show("There is an error   !!");


            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;


        }
    }
}
