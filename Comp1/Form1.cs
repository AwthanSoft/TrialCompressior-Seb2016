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
using Comp1.Public.CheckFiles.FileOperations;
using Comp1.Public.Lib;
using Comp1.Public.ReaderWriterFile;
using System.Threading;
using Comp1.Public.ReaderWriteFile02;
using System.Text.RegularExpressions;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.CompBy.SeparateFiles.SumSeparateFile;
using Comp1.ByMore.ChangerByMore;
using Comp1.MTF;
using Comp1.MTF.MoveToByMore;
using Comp1.MakeTreeUniq;
using Comp1.ChangerListUniq.MakeChangerUniq;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using Comp1.MergeSort.MakeTreeMergeSort;
using Comp1.QuickSort.MakeTreeQuickSort;




namespace Comp1
{
    // [STAThread]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.StartAsByte();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.InfoStartAsint();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox3.Text);

                textBox3.Text = "";

                textBox3.BackColor = Color.White;


            }
            catch
            {
                textBox3.BackColor = Color.Red;
                return;
            }


            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq(ModNum);
            uniq.MakeFileUniq_int();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.InfoMakeFileUniq_int();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.InfoMakeFileUniqSimlar_int();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.MakeFileUniqSimlar_int();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox3.Text);

                textBox3.Text = "";

                textBox3.BackColor = Color.White;


            }
            catch
            {
                textBox3.BackColor = Color.Red;
                return;
            }

            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq(ModNum);

            uniq.InfoMakeFileUniqSort_int();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq();
            uniq.MakeFileUniqSort_int();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox2.Text);
                
                textBox2.Text="";

                textBox2.BackColor = Color.White;

               
            }
            catch
            {
                textBox2.BackColor = Color.Red;
                return;
            }

            ExmineBlockUniq ex = new ExmineBlockUniq(ModNum);


        }

        private void button14_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox3.Text);

                textBox3.Text = "";

                textBox3.BackColor = Color.White;


            }
            catch
            {
                textBox3.BackColor = Color.Red;
                return;
            }


            Comp1.MergeSort.Merge01.DeCompByMerge01 uniq = new MergeSort.Merge01.DeCompByMerge01(ModNum);
            uniq.MakeFileDeUniq_int();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox3.Text);

                textBox3.Text = "";

                textBox3.BackColor = Color.White;


            }
            catch
            {
                textBox3.BackColor = Color.Red;
                return;
            }



            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq(ModNum);
            uniq.InfoMakeFileUniqStop_int();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox3.Text);

                textBox3.Text = "";

                textBox3.BackColor = Color.White;


            }
            catch
            {
                textBox3.BackColor = Color.Red;
                return;
            }


            Comp1.MergeSort.Merge01.CompByMerge01Uniq uniq = new MergeSort.Merge01.CompByMerge01Uniq(ModNum);
            uniq.InfoMakeFileUniqSort_int();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox4.Text);

                textBox4.Text = "";

                textBox4.BackColor = Color.White;


            }
            catch
            {
                textBox4.BackColor = Color.Red;
                return;
            }

            ExmineBlockUniq ex = new ExmineBlockUniq(ModNum);


        }

        //private void button14_Click(object sender, EventArgs e)
        //{
            


           
        //}

        private void button17_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox4.Text);

                textBox4.Text = "";

                textBox4.BackColor = Color.White;


            }
            catch
            {
                textBox4.BackColor = Color.Red;
                return;
            }

            ReadWriteFile00 FileRead = new ReadWriteFile00();
            FileRead.Extention = "IntTry" + ModNum.ToString();
            BitsToInt bitsToInt = new BitsToInt(ModNum);
            IntBitsOperations IntToBits = new IntBitsOperations(ModNum);

            FileRead.OpenAll();

            while (FileRead.ReadAble == true)
            {
                FileRead.ReadData();

                List<int> intData = bitsToInt.GetInt_bits(ref FileRead.DataRead);
                FileRead.SaveDataByte(IntToBits.GetIntsAsByteArr(ref intData));

            }


            FileRead.CloseAll();

        }

        private void button18_Click(object sender, EventArgs e)
        {
            Comp1.Public.ReaderWriteFile02.ProgressForm02 form = new Public.ReaderWriteFile02.ProgressForm02();

            form.ShowDialog();
        }

       
        private void button20_Click(object sender, EventArgs e)
        {
            //Comp1.Public.ReaderWriteFile02.ProgressForm02 form = new ProgressForm02();
            //form.Show();

            //Comp1.Public.ReaderWriteFile02.ReadWriteFile02 ReaderF = new Public.ReaderWriteFile02.ReadWriteFile02();

            //ReaderF.ReaderInfo.OpeningIsStarting += new EventHandler<FileReaderInfo02EventArgs>(form.Start);
            //ReaderF.ReaderInfo.DataIsSaving += new EventHandler(form.RefrishSaveData);
            //ReaderF.ReaderInfo.DataIsReading += new EventHandler(form.RefrishProgress);

            

            Thread tr = new Thread( new System.Threading.ThreadStart(() =>
            {

                Comp1.Public.ReaderWriteFile02.ReadWriteFile02 ReaderF = new Public.ReaderWriteFile02.ReadWriteFile02();

                    
                    ReaderF.OpenAll();


                    while (ReaderF.ReadAble)
                    {
                        ReaderF.ReadData();
                        ReaderF.SaveDataByte(ReaderF.DataRead);

                    }


                    ReaderF.CloseAll();


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


        }

        private void button26_Click(object sender, EventArgs e)
        {
                int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

               // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

             Thread tr = new Thread( new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.TrialMakeListUniq01 filing = new MakeListUniq.TrialMakeListUniq01(ModNum);
                filing.StartUniq();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();

            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.TrialMakeListUniq01 filing = new MakeListUniq.TrialMakeListUniq01(ModNum);
                filing.StartDeUniq();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniq_4();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Comp1.Public.CheckFiles.UICheck01.TwoChickForm01 form = new Public.CheckFiles.UICheck01.TwoChickForm01();
            form.ShowDialog();

            form.Dispose();

        }

        private void button25_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.TrialMakeListUniq02 filing = new MakeListUniq.TrialMakeListUniq02(ModNum);
                filing.StartUniq();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.TrialMakeListUniq02 filing = new MakeListUniq.TrialMakeListUniq02(ModNum);
                filing.StartDeUniq();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniq_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
            
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniq_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button28_Click(object sender, EventArgs e)
        {

            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniq_1();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniq_1();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniq_4();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniq_3();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button37_Click(object sender, EventArgs e)
        {
           
        }

        private void button38_Click(object sender, EventArgs e)
        {
            
        }

        private void button41_Click(object sender, EventArgs e)
        {
            OldChekers.Check_Form form = new OldChekers.Check_Form();
            form.Show();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button40_Click(object sender, EventArgs e)
        {
                 int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox8.Text);

                // textBox3.Text = "";

                textBox8.BackColor = Color.White;


            }
            catch
            {
                textBox8.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.MTF.TrialMakeMTF01 filing = new Comp1.ChangerNum.MTF.TrialMakeMTF01(ModNum);
                filing.StartMTF01_AsByte();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox8.Text);

                // textBox3.Text = "";

                textBox8.BackColor = Color.White;


            }
            catch
            {
                textBox8.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.MTF.TrialMakeMTF01 filing = new Comp1.ChangerNum.MTF.TrialMakeMTF01(ModNum);
                filing.StartDeMTF01_AsByte();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button43_Click(object sender, EventArgs e)
        {

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.MTF.TrialMakeMTF01 filing = new Comp1.ChangerNum.MTF.TrialMakeMTF01();
                filing.StartMTF01_AsBits();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.MTF.TrialMakeMTF01 filing = new Comp1.ChangerNum.MTF.TrialMakeMTF01();
                filing.StartDeMTF01_AsBits();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniq_3();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button46_Click(object sender, EventArgs e)
        {
           
        }

        private void button47_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button48_Click(object sender, EventArgs e)
        {
            
        }

        private void button49_Click(object sender, EventArgs e)
        {
           

        }

        private void button60_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix1_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniqMix1_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix3_1();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniqMix3_1();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix3_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniqMix3_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox5.Text);

                // textBox3.Text = "";

                textBox5.BackColor = Color.White;


            }
            catch
            {
                textBox5.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix1_3();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix2_3();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartDeUniqMix1_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix1_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix3W2_1();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox6.Text);

                // textBox3.Text = "";

                textBox6.BackColor = Color.White;


            }
            catch
            {
                textBox6.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.MakeListUniq.MixTrialMakeListUniq filing = new MakeListUniq.MixTrialMakeListUniq(ModNum);
                filing.StartUniqMix3W2_2();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button72_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox7.Text);

                // textBox3.Text = "";

                textBox7.BackColor = Color.White;


            }
            catch
            {
                textBox7.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.BWT.AsInt.BWTasNum01 filing = new BWT.AsInt.BWTasNum01(ModNum);
                filing.StartMakeBWT_int();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button70_Click(object sender, EventArgs e)
        {
           

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {

                Comp1.BWT.AsByte.BWTasByte01 filing = new BWT.AsByte.BWTasByte01();
                filing.StartMakeBWT_Byte();


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button71_Click(object sender, EventArgs e)
        {

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {

                Comp1.BWT.AsByte.BWTasByte01 filing = new BWT.AsByte.BWTasByte01();
                filing.StartMakeDeBWT_Byte();


            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button73_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox7.Text);

                // textBox3.Text = "";

                textBox7.BackColor = Color.White;


            }
            catch
            {
                textBox7.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.BWT.AsInt.BWTasNum01 filing = new BWT.AsInt.BWTasNum01(ModNum);
                filing.StartMakeDeBWT_int();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox9.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button76_Click(object sender, EventArgs e)
        {

            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox9.Text);

                textBox9.BackColor = Color.White;

            }
            catch
            {
                textBox9.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.TrialMakeMTL01 filing = new ChangerNum.TrialMakeMTL01(ModNum);
                filing.StartMTL01_AsNum();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button77_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox9.Text);

                textBox9.BackColor = Color.White;

            }
            catch
            {
                textBox9.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.TrialMakeMTL01 filing = new ChangerNum.TrialMakeMTL01(ModNum);
                filing.StartDeMTL01_AsNum();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button74_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.TrialMakeMTL01 filing = new ChangerNum.TrialMakeMTL01();
                filing.StartMTL01_AsBits();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button75_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                Comp1.ChangerNum.TrialMakeMTL01 filing = new ChangerNum.TrialMakeMTL01();
                filing.StartDeMTL01_AsBits();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button78_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                ReaderWriteFileBits02B ReaderNum = new ReaderWriteFileBits02B(true);

                ReaderWriteFileBits02 WriterNum = new ReaderWriteFileBits02(false);

                while (ReaderNum.isAbleRead)
                {
                    WriterNum.WriteBit(ReaderNum.GetBit());
                }

                ReaderNum.CloseFile();
                WriterNum.CloseFile();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();


        }

        private void button79_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                ReaderWriterOneNumMod02 ReaderNum = new ReaderWriterOneNumMod02(true , 20);

                ReaderWriterOneNumMod02 WriterNum = new ReaderWriterOneNumMod02(false , 256);


                int StartCount = 2;
                int EndCount = 65536;

                int Count = StartCount;

                int TempNum = 0;

                while (ReaderNum.isAbleRead)
                {
                    TempNum = ReaderNum.GetOneNumber(Count);

                    if (TempNum > Count)
                        MessageBox.Show("Error" +
                            "\nCount = " + Count.ToString() +
                            "\nTempNum = " + TempNum.ToString());

                    WriterNum.WriteNumber(Count, TempNum);



                    Count++;
                    if (Count == EndCount)
                        Count = StartCount;
                }

                ReaderNum.End();
                WriterNum.End();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button80_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox10.Text);

                // textBox3.Text = "";

                textBox10.BackColor = Color.White;


            }
            catch
            {
                textBox10.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                SumSeparateFile01 filing = new SumSeparateFile01(ModNum);
                filing.SeprateFileWay01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button82_Click(object sender, EventArgs e)
        {

        }

        private void button83_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox10.Text);

                // textBox3.Text = "";

                textBox10.BackColor = Color.White;


            }
            catch
            {
                textBox10.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                SumSeparateFile01 filing = new SumSeparateFile01(ModNum);
                filing.SeprateFileWay02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox10.Text);

                // textBox3.Text = "";

                textBox10.BackColor = Color.White;


            }
            catch
            {
                textBox10.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                SumSeparateFile01 filing = new SumSeparateFile01(ModNum);
                filing.DeSeprateFileWay01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox10.Text);

                // textBox3.Text = "";

                textBox10.BackColor = Color.White;


            }
            catch
            {
                textBox10.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                SumSeparateFile01 filing = new SumSeparateFile01(ModNum);
               string sb= filing.SeprateFileSegmentInfo();

               try
               {
                   richTextBox1.AppendText("\n" + sb);
               }
               catch
               {

               }
            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            textBox11.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button87_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox11.Text);

                // textBox3.Text = "";

                textBox11.BackColor = Color.White;


            }
            catch
            {
                textBox11.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                ChangerByMore01 filing = new ChangerByMore01(ModNum);
                filing.ChangerByMoreWay01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button85_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox11.Text);

                // textBox3.Text = "";

                textBox11.BackColor = Color.White;


            }
            catch
            {
                textBox11.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                ChangerByMore01 filing = new ChangerByMore01(ModNum);
                filing.ChangerByMoreWay02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            textBox12.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button90_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox12.Text);

                // textBox3.Text = "";

                textBox12.BackColor = Color.White;


            }
            catch
            {
                textBox12.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore01 filing = new MoveToByMore01(ModNum);
                filing.StartMoveToByMore();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button91_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox12.Text);

                // textBox3.Text = "";

                textBox12.BackColor = Color.White;


            }
            catch
            {
                textBox12.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore01 filing = new MoveToByMore01(ModNum);
                filing.StartDeMoveToByMore();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox12.Text);

                // textBox3.Text = "";

                textBox12.BackColor = Color.White;


            }
            catch
            {
                textBox12.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore01 filing = new MoveToByMore01(ModNum);
                filing.StartMoveToByMoreW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button93_Click(object sender, EventArgs e)
        {
            int ModNum = 8;
            try
            {
                ModNum = int.Parse(textBox12.Text);

                // textBox3.Text = "";

                textBox12.BackColor = Color.White;


            }
            catch
            {
                textBox12.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore01 filing = new MoveToByMore01(ModNum);
                filing.StartDeMoveToByMoreW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            textBox13.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            textBox14.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button96_Click(object sender, EventArgs e)
        {
            int ModLength = 256;
            try
            {
                ModLength = int.Parse(textBox13.Text);
                textBox13.BackColor = Color.White;
            }
            catch
            {
                textBox13.BackColor = Color.Red;
                return;
            }

            int ModTimes = 1;
            try
            {
                ModTimes = int.Parse(textBox14.Text);
                textBox14.BackColor = Color.White;
            }
            catch
            {
                textBox14.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore02 filing = new MoveToByMore02(ModLength , ModTimes);
                filing.StartMoveToByMoreFullFile();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button97_Click(object sender, EventArgs e)
        {
            int ModLength = 256;
            try
            {
                ModLength = int.Parse(textBox13.Text);
                textBox13.BackColor = Color.White;
            }
            catch
            {
                textBox13.BackColor = Color.Red;
                return;
            }

            int ModTimes = 1;
            try
            {
                ModTimes = int.Parse(textBox14.Text);
                textBox14.BackColor = Color.White;
            }
            catch
            {
                textBox14.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore02 filing = new MoveToByMore02(ModLength, ModTimes);
                filing.StartDeMoveToByMoreFullFile();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button94_Click(object sender, EventArgs e)
        {
            int ModLength = 256;
            try
            {
                ModLength = int.Parse(textBox13.Text);
                textBox13.BackColor = Color.White;
            }
            catch
            {
                textBox13.BackColor = Color.Red;
                return;
            }

            int ModTimes = 1;
            try
            {
                ModTimes = int.Parse(textBox14.Text);
                textBox14.BackColor = Color.White;
            }
            catch
            {
                textBox14.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore02 filing = new MoveToByMore02(ModLength, ModTimes);
                filing.StartMoveToByMoreW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button95_Click(object sender, EventArgs e)
        {
            int ModLength = 256;
            try
            {
                ModLength = int.Parse(textBox13.Text);
                textBox13.BackColor = Color.White;
            }
            catch
            {
                textBox13.BackColor = Color.Red;
                return;
            }

            int ModTimes = 1;
            try
            {
                ModTimes = int.Parse(textBox14.Text);
                textBox14.BackColor = Color.White;
            }
            catch
            {
                textBox14.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MoveToByMore02 filing = new MoveToByMore02(ModLength, ModTimes);
                filing.StartDeMoveToByMoreW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            textBox15.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button101_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01B();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button102_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            int ModNum = 256 ;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            textBox16.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox16.Text);

                textBox15.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox16.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            textBox17.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox17.Text);

                textBox18.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox17.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            textBox18.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

        }

        private void button105_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox18.Text);

                // textBox3.Text = "";

                textBox18.BackColor = Color.White;


            }
            catch
            {
                textBox18.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeChangerUniq01 filing = new MakeChangerUniq01(ModNum);
                filing.StartMakeChangerUniq_ForTreeUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            textBox19.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button106_Click(object sender, EventArgs e)
        {
            int ModReader = 256;
            try
            {
                ModReader = int.Parse(textBox19.Text);
                textBox19.BackColor = Color.White;
            }
            catch
            {
                textBox19.BackColor = Color.Red;
                return;
            }



            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox18.Text);

                // textBox3.Text = "";

                textBox18.BackColor = Color.White;


            }
            catch
            {
                textBox18.BackColor = Color.Red;
                return;
            }

            if (ModNum <= ModReader)
            {
                MessageBox.Show("input is error !");
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeChangerUniq01 filing = new MakeChangerUniq01(ModNum);
                filing.StartMakeChangerUniq_ForListUniq01W02(ModReader);

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button104_Click(object sender, EventArgs e)
        {
            int ModReader = 256;
            try
            {
                ModReader = int.Parse(textBox19.Text);
                textBox19.BackColor = Color.White;
            }
            catch
            {
                textBox19.BackColor = Color.Red;
                return;
            }



            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox18.Text);

                // textBox3.Text = "";

                textBox18.BackColor = Color.White;


            }
            catch
            {
                textBox18.BackColor = Color.Red;
                return;
            }

            if (ModNum <= ModReader)
            {
                MessageBox.Show("input is error !");
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeChangerUniq01 filing = new MakeChangerUniq01(ModNum);
                filing.StartMakeChangerUniq_ForTreeUniqW03(ModReader);

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button107_Click(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            textBox21.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            textBox20.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox20.Text);

                textBox21.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox20.BackColor = System.Drawing.Color.Red;
            }
        }

        private void button110_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox21.Text);

                // textBox3.Text = "";

                textBox21.BackColor = Color.White;


            }
            catch
            {
                textBox21.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq02 filing = new MakeTreeUniq02(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button109_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox21.Text);

                // textBox3.Text = "";

                textBox21.BackColor = Color.White;


            }
            catch
            {
                textBox21.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq02 filing = new MakeTreeUniq02(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button98_Click(object sender, EventArgs e)
        {

        }

        private void button112_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox21.Text);

                // textBox3.Text = "";

                textBox21.BackColor = Color.White;


            }
            catch
            {
                textBox21.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq02 filing = new MakeTreeUniq02(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            textBox22.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            textBox23.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox23.Text);

                textBox24.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox23.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            textBox24.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button114_Click(object sender, EventArgs e)
        {
            int LevelMod = 4;
            try
            {
                LevelMod = int.Parse(textBox22.Text);
                textBox22.BackColor = Color.White;
            }
            catch
            {
                textBox22.BackColor = Color.Red;
                return;
            }



            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox24.Text);

                // textBox3.Text = "";

                textBox24.BackColor = Color.White;


            }
            catch
            {
                textBox24.BackColor = Color.Red;
                return;
            }

            if (ModNum <= LevelMod)
            {
                MessageBox.Show("input is error !");
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq03 filing = new MakeTreeUniq03(ModNum);
                filing.StartMakeTreeUniq_W01(LevelMod);

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button115_Click(object sender, EventArgs e)
        {
          

            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox24.Text);

                // textBox3.Text = "";

                textBox24.BackColor = Color.White;


            }
            catch
            {
                textBox24.BackColor = Color.Red;
                return;
            }

         

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq03 filing = new MakeTreeUniq03(ModNum);
                filing.StartMakeTreeUniq_W05();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            textBox26.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox26.Text);

                textBox27.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox26.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            textBox27.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button118_Click(object sender, EventArgs e)
        {
            //int LevelMod = 4;
            //try
            //{
            //    LevelMod = int.Parse(textBox26.Text);
            //    textBox26.BackColor = Color.White;
            //}
            //catch
            //{
            //    textBox26.BackColor = Color.Red;
            //    return;
            //}



            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox27.Text);

                // textBox3.Text = "";

                textBox27.BackColor = Color.White;


            }
            catch
            {
                textBox27.BackColor = Color.Red;
                return;
            }

            //if (ModNum <= LevelMod)
            //{
            //    MessageBox.Show("input is error !");
            //    return;
            //}

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq04 filing = new MakeTreeUniq04(ModNum);
                filing.StartMakeTreeUniq_W01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button119_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox27.Text);

                // textBox3.Text = "";

                textBox27.BackColor = Color.White;


            }
            catch
            {
                textBox27.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq04 filing = new MakeTreeUniq04(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button120_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox27.Text);

                // textBox3.Text = "";

                textBox27.BackColor = Color.White;


            }
            catch
            {
                textBox27.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq04 filing = new MakeTreeUniq04(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button121_Click(object sender, EventArgs e)
        {





        }

        private void button117_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox27.Text);

                // textBox3.Text = "";

                textBox27.BackColor = Color.White;


            }
            catch
            {
                textBox27.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq04 filing = new MakeTreeUniq04(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02Info();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button122_Click(object sender, EventArgs e)
        {
            int Timers = 4;
            try
            {
                Timers = int.Parse(textBox25.Text);
                textBox25.BackColor = Color.White;
            }
            catch
            {
                textBox25.BackColor = Color.Red;
                return;
            }


            StringBuilder sb = new StringBuilder();

            MakeTreeUniqInfoReaderWriterFile00 filing = new MakeTreeUniqInfoReaderWriterFile00(true);

            int i = 0;
            MakeTreeUniqInfoNode00 tempNod;
            while (filing.isAbleRead && i != Timers)
            {
                tempNod = filing.GetNod();


                sb.Append("\n"
                    + "SegNum = " + tempNod.SegmentNum.ToString("00000")
                    /*+ "  ReadBits = "+tempNod.HowReadBits.ToString("0000")*/
                    + "  SaveBit = " + tempNod.HowSaveBits.ToString("00000")
                    + "  Able = " + tempNod.isAble.ToString()
                    + "  isItSave = " + tempNod.isItSave.ToString()




                    );

                i++;
            }

            filing.CloseFile();

            richTextBox1.AppendText("\n" + sb.ToString() + "\n");
        }

        private void button113_Click(object sender, EventArgs e)
        {

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            textBox29.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox29.Text);

                textBox30.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox30.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            textBox30.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            textBox28.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button125_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox30.Text);

                // textBox3.Text = "";

                textBox30.BackColor = Color.White;


            }
            catch
            {
                textBox30.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq05 filing = new MakeTreeUniq05(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button124_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox30.Text);

                // textBox3.Text = "";

                textBox30.BackColor = Color.White;


            }
            catch
            {
                textBox30.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq05 filing = new MakeTreeUniq05(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button126_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox30.Text);

                // textBox3.Text = "";

                textBox30.BackColor = Color.White;


            }
            catch
            {
                textBox30.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq05 filing = new MakeTreeUniq05(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW01B();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button123_Click(object sender, EventArgs e)
        {

        }

        private void button127_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox30.Text);

                // textBox3.Text = "";

                textBox30.BackColor = Color.White;


            }
            catch
            {
                textBox30.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq05 filing = new MakeTreeUniq05(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01Info();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            textBox25.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button108_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox21.Text);

                // textBox3.Text = "";

                textBox21.BackColor = Color.White;


            }
            catch
            {
                textBox21.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq02 filing = new MakeTreeUniq02(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01B();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button128_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox21.Text);

                // textBox3.Text = "";

                textBox21.BackColor = Color.White;


            }
            catch
            {
                textBox21.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq02 filing = new MakeTreeUniq02(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01Info();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button111_Click(object sender, EventArgs e)
        {

        }

        private void button129_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01Info();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button116_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox24.Text);

                // textBox3.Text = "";

                textBox24.BackColor = Color.White;


            }
            catch
            {
                textBox24.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq03 filing = new MakeTreeUniq03(ModNum);
                filing.StartMakeTreeDeUniq_W05();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button130_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox24.Text);

                // textBox3.Text = "";

                textBox24.BackColor = Color.White;


            }
            catch
            {
                textBox24.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq03 filing = new MakeTreeUniq03(ModNum);
                filing.StartMakeTreeDeUniq_W06();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button131_Click(object sender, EventArgs e)
        {

            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox24.Text);

                // textBox3.Text = "";

                textBox24.BackColor = Color.White;


            }
            catch
            {
                textBox24.BackColor = Color.Red;
                return;
            }



            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq03 filing = new MakeTreeUniq03(ModNum);
                filing.StartMakeTreeUniq_W06();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            textBox32.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox32.Text);

                textBox33.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox32.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            textBox31.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            textBox33.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button134_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button133_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button136_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button135_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW02();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            textBox35.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox35.Text);

                textBox36.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox35.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            textBox34.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            textBox36.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button139_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox36.Text);

                // textBox3.Text = "";

                textBox36.BackColor = Color.White;


            }
            catch
            {
                textBox36.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeMergeSort01Oper filing = new MakeTreeMergeSort01Oper(ModNum);
                filing.StartMakeTreeMergeSort_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button142_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW03();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button143_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW03();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button144_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02OneInfo();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button145_Click(object sender, EventArgs e)
        {
            int Timers = 4;
            try
            {
                Timers = int.Parse(textBox31.Text);
                textBox31.BackColor = Color.White;
            }
            catch
            {
                textBox31.BackColor = Color.Red;
                return;
            }


            StringBuilder sb = new StringBuilder();

            MakeTreeUniqInfoReaderWriterFile00 filing = new MakeTreeUniqInfoReaderWriterFile00(true);

            int i = 0;
            MakeTreeUniqInfoNode00 tempNod;
          //  tempNod.
            while (filing.isAbleRead && i != Timers)
            {
                tempNod = filing.GetNod();
                
                

                sb.Append("\n"
                    + "SegNum = " + tempNod.SegmentNum.ToString("00000")
                    + "  OperationId = " + tempNod.OperationId.ToString("000")
                    + "\nReadBits = "+tempNod.HowReadBits.ToString("0000")
                    + "  WriteBits = " +tempNod.HowWriteBits.ToString("0000")
                    + "\nAble = " + tempNod.isAble.ToString()
                    + "  SaveBit = " + tempNod.HowSaveBits.ToString("00000") 
                    + "  isItSave = " + tempNod.isItSave.ToString()
                    + "  TempSaved = " + tempNod.TempSaved.ToString()
                    +"\n"



                    );


                i++;
            }



            while (filing.isAbleRead)
            {
                tempNod = filing.GetNod();

            }


            filing.CloseFile();

            richTextBox1.AppendText("\n" + sb.ToString() + "\n");
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            textBox38.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox38.Text);

                textBox39.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox38.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            textBox37.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            textBox39.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button150_Click(object sender, EventArgs e)
        {

        }

        private void button151_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox39.Text);

                // textBox3.Text = "";

                textBox39.BackColor = Color.White;


            }
            catch
            {
                textBox39.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeChangerUniq02 filing = new MakeChangerUniq02(ModNum);
                filing.StartMakeChangerForUniq_UniqFileW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button155_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02AllInfo();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button156_Click(object sender, EventArgs e)
        {

        }

        private void button157_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW03AllInfo();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button164_Click(object sender, EventArgs e)
        {
             int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW04();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();

        
        
        }

        private void button162_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox33.Text);

                // textBox3.Text = "";

                textBox33.BackColor = Color.White;


            }
            catch
            {
                textBox33.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq06 filing = new MakeTreeUniq06(ModNum);
                filing.StartMakeTreeUniq_FileUniqW04AllInfo();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button166_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox30.Text);

                // textBox3.Text = "";

                textBox30.BackColor = Color.White;


            }
            catch
            {
                textBox30.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq05 filing = new MakeTreeUniq05(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01Info2_ByHalf();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button167_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeUniq_FileUniqW02_ByHalf();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button168_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeDeUniq_FileUniqW02_ByHalf();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button169_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox15.Text);

                // textBox3.Text = "";

                textBox15.BackColor = Color.White;


            }
            catch
            {
                textBox15.BackColor = Color.Red;
                return;
            }

            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeUniq01 filing = new MakeTreeUniq01(ModNum);
                filing.StartMakeTreeUniq_FileUniqW01Info2_ByHalf();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button132_Click(object sender, EventArgs e)
        {

        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            textBox41.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;

            try
            {

                int Temp = int.Parse(textBox41.Text);

                textBox42.Text = Convert.ToInt32(Math.Pow(2, Temp)).ToString();

            }
            catch
            {
                textBox41.BackColor = System.Drawing.Color.Red;
            }
        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            textBox42.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            textBox40.BackColor = System.Drawing.Color.White;
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9]", "");
            textboxSender.SelectionStart = cursorPosition;
        }

        private void button172_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox42.Text);

                // textBox3.Text = "";

                textBox42.BackColor = Color.White;


            }
            catch
            {
                textBox42.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeQuickSort01Oper filing = new MakeTreeQuickSort01Oper(ModNum);
                filing.StartMakeQuickSort_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button171_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox42.Text);

                // textBox3.Text = "";

                textBox42.BackColor = Color.White;


            }
            catch
            {
                textBox42.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeQuickSort01Oper filing = new MakeTreeQuickSort01Oper(ModNum);
                filing.StartMakeDeQuickSort_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }

        private void button138_Click(object sender, EventArgs e)
        {
            int ModNum = 256;
            try
            {
                ModNum = int.Parse(textBox36.Text);

                // textBox3.Text = "";

                textBox36.BackColor = Color.White;


            }
            catch
            {
                textBox36.BackColor = Color.Red;
                return;
            }


            Thread tr = new Thread(new System.Threading.ThreadStart(() =>
            {
                MakeTreeMergeSort01Oper filing = new MakeTreeMergeSort01Oper(ModNum);
                filing.StartMakeTreeDeMergeSort_FileUniqW01();

            }));

            tr.ApartmentState = ApartmentState.STA;
            tr.Start();
        }
    }
}
