using Comp1.Public.Lib;
using Comp1.Public.ReaderWriterFile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.Public.CheckFiles.FileOperations
{
  public  class ExmineBlockUniq
    {
       
        public StringBuilder RePort;

        private int Mod = 8;
        private int LengthStop = 256;
        private BitsToInt BitsConv;

        private ReadWriteFile00 readerFile;
        private int dataBlock = 1024 * 1024;



        /*******Reader info  **********/
        private int count = 0;
        private BitArray BitsRead;
        private int Re = 0;
        private int SumBlocIskUniq = 0;
        private int IsNoUniq = 0;


        public ExmineBlockUniq(int Modlength)
        {
            Mod = Modlength;
            BitsConv = new BitsToInt(Mod);
            LengthStop = Convert.ToInt32(Math.Pow(2, Mod));
            BitsRead = new BitArray(LengthStop, false);
            GetBlockUniq();
        }
        public ExmineBlockUniq()
        {

            BitsConv = new BitsToInt(Mod);
            LengthStop = Convert.ToInt32(Math.Pow(2, Mod));
            BitsRead = new BitArray(LengthStop, false);
            GetBlockUniq();
        }


        public void GetBlockUniq()
        {
            readerFile = new ReadWriteFile00();
            readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();
            List<int> intList;

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();
                intList = BitsConv.GetInt_bits(ref readerFile.DataRead);
                ReadBlockUniq(ref intList);
            }

            readerFile.CloseAll();
            GetInfo();


        }

        private void ReadBlockUniq(ref List<int> intList)
        {
            foreach (int n in intList)
            {
                if (Re == LengthStop)
                {
                    Re = 0;
                    if (count == LengthStop)
                        SumBlocIskUniq++;
                    else
                        IsNoUniq++;
                    BitsRead = new BitArray(LengthStop, false);
                    count = 0;
                }
                if (BitsRead[n] == false)
                {
                    count++;
                    BitsRead[n] = true;
                }

                Re++;
            }

            if (Re == LengthStop)
            {
                Re = 0;
                if (count == LengthStop)
                    SumBlocIskUniq++;
                else
                    IsNoUniq++;
                BitsRead = new BitArray(LengthStop, false);
                count = 0;
            }


           


        }


        public void GetInfo()
        {
            StringBuilder ss = new StringBuilder();

            int SumBlock = IsNoUniq + SumBlocIskUniq;
            ss.Append("\n\n Number of Block is Uniq *********\n\n" +
                "\nMod = "+Mod.ToString()+
                "\nLengthStop = "+LengthStop.ToString()+
                "\n\n*************\n\n"+
                "\nSumBlock = "+SumBlock.ToString()+"\n"+
                "\nSumBlocIskUniq = "+SumBlocIskUniq.ToString()+
                "\nIsNoUniq = "+IsNoUniq.ToString()+





                "\n\n");



            if (MessageBox.Show(ss.ToString(), "isBlockUniq", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

         //   RePort.Append(ss.ToString() + "\n\n\n" + ss.ToString() + "\n\n\n");
        }



    }
}
