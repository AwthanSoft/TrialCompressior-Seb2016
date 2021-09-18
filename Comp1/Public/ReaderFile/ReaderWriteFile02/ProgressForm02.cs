using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Comp1.Public.ReaderWriteFile02
{
    public partial class ProgressForm02 : Form
    {
        public DateTime startTime;
        public TimeSpan NowTime0 = new TimeSpan();
        public bool State = false;

        private Task _thread { get; set; }


        public ProgressForm02()
        {
            InitializeComponent();
            _thread = new Task(() => this.Show());
            
        }

        public void StartShow()
        {
            _thread.RunSynchronously();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProgressForm02_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Refresh();
        }

        public FileReaderInfo02 filingInfo;
        public   void Start(object sender , FileReaderInfo02EventArgs e)
        {

            //this.Show();
            startTime = DateTime.Now;

            label2.Text = e.ReadFile.FileName;
            label3.Text = e.ReadFile.FileSize.ToString();
            label4.Text = e.ReadFile.FileExtension;
            label13.Text = e.ReadFile.SaveExtension;
            label16.Text = e.ReadFile.StopNumLength.ToString();

            label5.Text = startTime.ToString("hh : mm : ss tt ");
            label6.Text = "";




            label7.Text = true.ToString();
            label12.Text = e.ReadFile.ReaderDataLength.ToString();


            

            this.Refresh();

        }
        public void RefrishSaveData(object sender, EventArgs e)
        {
            //ReaderWriterInfo02 info = (ReaderWriterInfo02)sender;
            //label10.Text = info.SizeSaved0.ToString();
        }

        public ReaderWriterInfo02 ReaderInfo;
        public void RefrishProgress(object sender, EventArgs e)
        {
          //  ReaderWriterInfo02 info = (ReaderWriterInfo02)sender;
            //label8.Text = info.RestSize0.ToString();
            //label9.Text = info.SizeDone0.ToString();
           // this.progressBar1.Value = Convert.ToInt32(info.Progress);

            //this.progressBar1.Value = Convert.ToInt32(ReaderInfo.Progress);

          //  label14.Text = info.Progress.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                EndOpning(sender, e);
               // return;
            }

            label8.Text = ReaderInfo.RestSize0.ToString();
            label9.Text = ReaderInfo.SizeDone0.ToString();

            //label14.Text = ReaderInfo.Progress.ToString();
            label14.Text = String.Format("{0:0.##}%", ReaderInfo.Progress.ToString());
            this.progressBar1.Value = Convert.ToInt32(ReaderInfo.Progress);

            label10.Text = ReaderInfo.SizeSaved0.ToString();


            NowTime0 = DateTime.Now - startTime;
            label6.Text =String.Format("{0:0.##}", NowTime0.TotalSeconds.ToString());
           
         //   this.Refresh();
        }


        public void EndOpning(object sender, EventArgs e)
        {

            timer1.Stop();

            try
            {

                this.label7.Text = false.ToString();


                //NowTime0 = DateTime.Now - startTime;
                //label6.Text = NowTime0.TotalMinutes.ToString();

                this.Refresh();
            }
            catch
            {


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

    }
}
