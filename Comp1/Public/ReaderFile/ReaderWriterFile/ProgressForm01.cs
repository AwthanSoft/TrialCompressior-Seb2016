using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.Public.ReaderWriterFile
{
    public partial class ProgressForm01 : Form
    {
        public ProgressForm01()
        {
            InitializeComponent();
        }
        public string FileName0;
        public long OrignalFileSize = 0;
        public string Extention;

        public DateTime startTime;
        public TimeSpan NowTime0 = new TimeSpan();
        public bool State = false;

        public long RestSize0 = 0;
        public long SizeDone0 = 0;
        public long SaveSize0 = 0;
        public long BlockReadSize = 0;


        private void ProgressForm01_Load(object sender, EventArgs e)
        {

        }
        public void FillInfo(ReadWriteFile00 filing)
        {
            FileName0 = filing.PathFileRead;
            OrignalFileSize = filing.ReadFileSize;
            Extention = filing.Extention;

            startTime = DateTime.Now;
            NowTime0 = DateTime.Now - startTime;
            State = filing.ReadAble;

            RestSize0 = filing.ReadFileSize;
            SizeDone0 = filing.SizeDone0;
            SaveSize0 = filing.SaveSize0;
            BlockReadSize = filing.BlockReaderLength;


            progressBar1.Maximum = Convert.ToInt32(OrignalFileSize);
            progressBar1.Value = 0;

        }
        public void Refrish(ReadWriteFile00 filing)
        {
            label2.Text = FileName0.ToString();
            label3.Text = OrignalFileSize.ToString();
            label4.Text = Extention;

            label5.Text = startTime.ToString("hh : mm : ss tt ");
            NowTime0 = DateTime.Now - startTime;
            label6.Text = NowTime0.TotalMinutes.ToString();
            label7.Text = filing.ReadAble.ToString();


            label8.Text = filing.RestSize0.ToString();
            label9.Text = filing.SizeDone0.ToString();
            label10.Text = filing.SaveSize0.ToString();
            label12.Text = filing.BlockReaderLength.ToString();


            progressBar1.Value = Convert.ToInt32(filing.SizeDone0);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
