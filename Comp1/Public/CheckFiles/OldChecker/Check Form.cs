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
using System.Threading;
using Comp1.Public.CheckFiles.OldChecker.Checker_Count;
using System.Text.RegularExpressions;

namespace OldChekers
{
    public partial class Check_Form : Form
    {
        public Check_Form()
        {
            InitializeComponent();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            using (var origtfiling = new FileStream(textBox1.Text, FileMode.OpenOrCreate, FileAccess.Read))
            {
                origtfiling.Seek(0, SeekOrigin.Begin);


                long length = origtfiling.Length;
                int time1 = Convert.ToInt32(length / 2);
                int time2 = Convert.ToInt32(length % 2);

                byte[] data = new byte[2];

                int i = 0;
                int lineNum = 1;
                int lineTemp = 0;

                while (i != time1)
                {
                    origtfiling.Read(data, 0, 2);
                    BitArray BitNum = new BitArray(data);

                    foreach (bool b in BitNum)
                    {
                        if (b == true)
                            sb.Append("1");
                        else
                            sb.Append("0");

                    }
                    sb.Append("  ");

                    i++;
                      if (lineTemp == 3)
                    {
                        lineTemp = 0;
                        sb.Append("L==" + lineNum.ToString("0000000000000")+" ");
                        lineNum++;
                    }
                    else
                        lineTemp++;

                }

                if (time2 != 0)
                {
                    byte[] data2 = new byte[1];
                    origtfiling.Read(data2, 0, 1);
                    BitArray BitNum = new BitArray(data2);

                    foreach (bool b in BitNum)
                    {
                        if (b == true)
                            sb.Append("1");
                        else
                            sb.Append("0");

                    }



                }

                origtfiling.Close();

            }
            richTextBox1.AppendText(sb.ToString());



        }

        private void button3_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            using (var origtfiling = new FileStream(textBox6.Text, FileMode.OpenOrCreate, FileAccess.Read))
            {
                origtfiling.Seek(0, SeekOrigin.Begin);


                long length = origtfiling.Length;
                int time1 = Convert.ToInt32(length / 2);
                int time2 = Convert.ToInt32(length % 2);

                byte[] data = new byte[2];

                int i = 0;

                int lineNum = 1;
                int lineTemp = 0;

                while (i != time1)
                {
                    origtfiling.Read(data, 0, 2);
                    BitArray BitNum = new BitArray(data);

                    foreach (bool b in BitNum)
                    {
                        if (b == true)
                            sb.Append("1");
                        else
                            sb.Append("0");

                    }
                    sb.Append("  ");
                    
                    i++;


                    if (lineTemp == 3)
                    {
                        lineTemp = 0;
                        sb.Append("L==" + lineNum.ToString("0000000000000")+" ");
                        lineNum++;
                    }
                    else
                        lineTemp++;
                    


                }





                if (time2 != 0)
                {
                    byte[] data2 = new byte[1];
                    origtfiling.Read(data2, 0, 1);
                    BitArray BitNum = new BitArray(data2);

                    foreach (bool b in BitNum)
                    {
                        if (b == true)
                            sb.Append("1");
                        else
                            sb.Append("0");

                    }



                }

                origtfiling.Close();

            }
            richTextBox2.AppendText(sb.ToString());

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
        
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



            textBox6.Text = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                textBox6.Text = file;
                //       MessageBox.Show(file);
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



            textBox1.Text = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                textBox1.Text = file;
                //       MessageBox.Show(file);
            }

           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            using (var origtfiling = new FileStream(textBox1.Text, FileMode.OpenOrCreate, FileAccess.Read))
            {

                

                

                origtfiling.Seek(0, SeekOrigin.Begin);


                long length = origtfiling.Length;
                int time1 = Convert.ToInt32(length / 4);
                int time2 = Convert.ToInt32(length % 4);

                byte[] data = new byte[4];

                int i = 0;

                while (i != time1)
                {
                    origtfiling.Read(data, 0, 4);
                    sb.Append(BitConverter.ToInt32(data, 0).ToString("0000000000000000") + " ");

                

                    i++;

                }

                if (time2 != 0)
                {
                    origtfiling.Read(data, 0, 1);


                  //  sb.Append(Convert.ToInt32(data[4]).ToString() + " ");


               
                }

                origtfiling.Close();

                
            }
            richTextBox1.AppendText(sb.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            using (var origtfiling = new FileStream(textBox6.Text, FileMode.OpenOrCreate, FileAccess.Read))
            {





                origtfiling.Seek(0, SeekOrigin.Begin);


                long length = origtfiling.Length;
                int time1 = Convert.ToInt32(length / 4);
                int time2 = Convert.ToInt32(length % 4);

                byte[] data = new byte[4];

                int i = 0;

                while (i != time1)
                {
                    origtfiling.Read(data, 0, 4);
                    sb.Append(BitConverter.ToInt32(data, 0).ToString("0000000000000000") + " ");



                    i++;

                }

                if (time2 != 0)
                {
                    origtfiling.Read(data, 0, 1);


                    //  sb.Append(Convert.ToInt32(data[4]).ToString() + " ");



                }

                origtfiling.Close();


            }
            richTextBox2.AppendText(sb.ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ExmineFile ex = new ExmineFile();

            richTextBox1.AppendText(ex.CheckByBit(textBox1.Text, textBox6.Text) + "\n\n");
            richTextBox1.AppendText(ex.sb.ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(@textBox1.Text);
            StringBuilder k = new StringBuilder();

            foreach (int n in fileBytes)
                k.Append(n.ToString("000") + " ");

            richTextBox1.AppendText(k.ToString());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(@textBox6.Text);
            StringBuilder k = new StringBuilder();

            foreach (int n in fileBytes)
                k.Append(n.ToString("000") + " ");

            richTextBox2.AppendText(k.ToString());
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ExmineFile exF = new ExmineFile();
            DateTime start = DateTime.Now;

            richTextBox1.AppendText(exF.CheckBy2Byte(textBox1.Text));

            DateTime finish = DateTime.Now;

            richTextBox1.AppendText("\n\n" + exF.sb.ToString() + "\n\n");

            int last = DateTime.Compare(finish, start);
            richTextBox1.AppendText("Time = " + last.ToString() + "\n\n");
            
            TimeSpan sd = finish - start;
            richTextBox1.AppendText(sd.Seconds.ToString());

        }

        private void button21_Click(object sender, EventArgs e)
        {

            ExmineFile exF = new ExmineFile();
            DateTime start = DateTime.Now;

            richTextBox2.AppendText(exF.CheckBy2Byte(textBox6.Text));

            DateTime finish = DateTime.Now;

            richTextBox2.AppendText("\n\n" + exF.sb.ToString() + "\n\n");

            int last = DateTime.Compare(finish, start);
            richTextBox2.AppendText("Time = " + last.ToString() + "\n\n");

            TimeSpan sd = finish - start;
            richTextBox2.AppendText(sd.Seconds.ToString());
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int readSeek = 0;
            if (textBox2.Text != "")
                readSeek = int.Parse(textBox2.Text.ToString());


            StringBuilder sb = new StringBuilder();
            using (var origtfiling = new FileStream(textBox1.Text, FileMode.OpenOrCreate, FileAccess.Read))
            {
                origtfiling.Seek(readSeek * 10240, SeekOrigin.Begin);


                byte[] data = new byte[10240];

                origtfiling.Read(data, 0, 10240);
                int i = 0;
               
                    BitArray BitNum = new BitArray(data);

                    foreach (bool b in BitNum)
                    {
                        i++; 

                        if (b == true)
                            sb.Append("1");
                        else
                            sb.Append("0");

                        if (i == 8)
                        {
                            sb.Append("  "); i = 0;
                        }

                       
                    }

                
            

            

                origtfiling.Close();

            }
            readSeek++;
            textBox2.Text = readSeek.ToString();
            richTextBox1.Text =(sb.ToString());
        


        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            FileInfo info = new FileInfo(textBox1.Text);
            ExmineFile exf = new ExmineFile();

            File.WriteAllText(info.FullName.Remove(info.FullName.Length - info.Extension.Length) + "Lest2.txt", exf.checkNumByte16Bits(textBox1.Text));


            richTextBox1.AppendText("Done   !!");



        }

        private void button25_Click(object sender, EventArgs e)
        {
            int mod = 0;
            try
            {
                mod = int.Parse(textBox2.Text);
            }
            catch
            {
                 mod = 0;
            }
          
                


            ExmineFile exF = new ExmineFile();
            DateTime start = DateTime.Now;

            if (mod <= 0)
            {
                FileInfo info = new FileInfo(textBox1.Text);
                ExmineFile exf = new ExmineFile();

                if (MessageBox.Show("Do you want print to txt ?\nMod = "+mod.ToString(), "PrintInfo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    File.WriteAllText(info.FullName.Remove(info.FullName.Length - info.Extension.Length) + "Lest2.txt", exf.CheckByLocatByte(textBox1.Text, mod , true));
                    
                }
                else
                    richTextBox1.AppendText(exF.CheckByLocatByte(textBox1.Text, mod ,false));
            }
            else
                richTextBox1.AppendText(exF.CheckByLocatByte(textBox1.Text, mod ,false));
  

            DateTime finish = DateTime.Now;

            richTextBox1.AppendText("\n\n" + exF.sb.ToString() + "\n\n");

            int last = DateTime.Compare(finish, start);
            richTextBox1.AppendText("Time = " + last.ToString() + "\n\n");

            TimeSpan sd = finish - start;
            richTextBox1.AppendText(sd.Seconds.ToString());

            mod++;
            textBox2.Text = (mod).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Check_Form_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
        }

        private void button26_Click(object sender, EventArgs e)
        {

            char[] TempText = textBox2.Text.ToCharArray();

            char[] textchar = richTextBox1.Text.ToCharArray();
            int i =0;
            int TempLength=TempText.Length;
            if(TempLength<=0)
                return;

            int t = 0;
            int Start = 0 ,TempStart = 0; bool IsBreaked = false;

            while (i != textchar.Length)
            {
                if (textchar[i] == TempText[0])
                {
                    t = 0;
                    Start = i;
                    TempStart = i;
                    IsBreaked = false;
                    while (t != TempLength)
                    {
                        if (textchar[TempStart] != TempText[t])
                        {
                            IsBreaked = true; 
                            break;
                        }
                        else
                        {
                            TempStart++;
                            t++;
                        }
                       
                        
                    }
                    if (IsBreaked == false)
                    {
                        richTextBox1.Select(Start, TempLength);
                        richTextBox1.SelectionColor = Color.Red;
                        i = TempStart;

                    }
                    else
                        i++;
                }
                else
                    i++;
            }
            
            
            


            
        }

        private void button27_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox2.Text);

                // textBox3.Text = "";

                textBox2.BackColor = Color.White;


            }
            catch
            {
                textBox2.BackColor = Color.Red;

                textBox2.Text = "256";
                
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


            CheckerCountByNum01 filing = new CheckerCountByNum01(ModNum);

            richTextBox1.AppendText(filing.CheckerCountForFile());

        }

        private void button29_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox2.Text);

                // textBox3.Text = "";

                textBox2.BackColor = Color.White;


            }
            catch
            {
                textBox2.BackColor = Color.Red;

                textBox2.Text = "256";

                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


            CheckerCountByNum01 filing = new CheckerCountByNum01(ModNum);

            richTextBox2.AppendText(filing.CheckerCountForFile());

        }

        private void button30_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox2.Text);

                // textBox3.Text = "";

                textBox2.BackColor = Color.White;


            }
            catch
            {
                textBox2.BackColor = Color.Red;

                textBox2.Text = "256";

                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


            CheckerCountByNum01 filing = new CheckerCountByNum01(ModNum);

            richTextBox1.AppendText(filing.CheckerCountForFile_W2());
        }

        private void button31_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox2.Text);

                // textBox3.Text = "";

                textBox2.BackColor = Color.White;


            }
            catch
            {
                textBox2.BackColor = Color.Red;

                textBox2.Text = "256";

                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


            CheckerCountByNum01 filing = new CheckerCountByNum01(ModNum);

            richTextBox2.AppendText(filing.CheckerCountForFile_W2());
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

                int Temp = int.Parse(textBox8.Text);

                textBox2.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox8.BackColor = System.Drawing.Color.Red;
            }
        }
        
    }
}
