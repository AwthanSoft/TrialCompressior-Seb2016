using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderFile.ReaderWriteFile02
{

    public class ReaderWriteFileNum02
    {

        #region  File Proprties

        private Comp1.Public.Lib.IntBitsOperations WriterInts;
        private Comp1.Public.Lib.BitsToInt ReaderInts;

        private int ReaderDataLength = 1024;
        private int Mod = 8;
        private bool ReaderMod = false;
        private bool fileIsOpen = false;
        private FileStream filing;
        private string Pathfile;

        public void OpenFile()
        {
            if (fileIsOpen == false)
            {

                filing = new FileStream(Pathfile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

                fileIsOpen = true;

                if (ReaderMod)
                {
                    long FileSize = filing.Length;

                    ProcessTimer1 = Convert.ToInt32(FileSize / ReaderDataLength);
                    ProcessTimer2 = Convert.ToInt32(FileSize % ReaderDataLength);
                    ReadAble = true;


                    filing.Seek(0, SeekOrigin.Begin);

                    ReaderInts = new Lib.BitsToInt(Mod);

                }
                else
                {
                    filing.Seek(filing.Length, SeekOrigin.Begin);
                    WriterInts = new Lib.IntBitsOperations(Mod);
                }

            }

        }
        public void CloseFile()
        {
            if (fileIsOpen != false)
            {
                if (!ReaderMod)
                    SaveEndBitArr();
                    
                
                filing.Close();
            }

        }

        #endregion

        #region  Number Save


        /********* Data  **************/
        private int SN = 1024 ;
        private int NumListReadLength = 1024;
        private List<int> NumListSave = new List<int>();

        public void WriteNum(int Num)
        {
            if (SN == NumListReadLength)
            {
                SaveNumList();
                SN = 0;
                NumListSave = new List<int>();
            }
            
            NumListSave.Add(Num);
            SN++;

        }
        private void SaveNumList()
        {

            if (fileIsOpen == false)
            {
                OpenFile();
            }

            byte[] SaveBitArr = WriterInts.GetIntsAsByteArr(ref NumListSave);

            filing.Write(SaveBitArr, 0, SaveBitArr.Length);
            

        }
        private void SaveEndBitArr()
        {
            

            if (fileIsOpen == false)
            {
                OpenFile();
            }


            {
                NumListSave.Add(0);
                byte[] SaveBitArr = WriterInts.GetIntsAsByteArr(ref NumListSave);
                filing.Write(SaveBitArr, 0, SaveBitArr.Length);

                SN = 0;
                NumListSave = new List<int>();
            
                ////SaveLast
                //{
                // //  WriterInts.GetLastEnd
                //}


                






            }




        }


        #endregion

        #region  Number Read

        /********* Info  **************/
        private bool ReadAble = false;
        private int ProcessTimer1 = 0;
        private int ProcessTimer2 = 0;
        private int Process1 = 0;

        /********* Data  **************/
        private int RN = 0;
       
        private int ListReadLength = 0;
        private List<int> NumListRead = new List<int>();

        /******************************/
        private void ReadNums()
        {
            if (!fileIsOpen)
                OpenFile();

            byte[] DataRead;
            if (ReadAble == true)
            {
                if (Process1 != ProcessTimer1)
                {
                    DataRead = new byte[ReaderDataLength];
                    filing.Read(DataRead, 0, ReaderDataLength);

                    Process1++;
                    NumListRead = ReaderInts.GetInt_bits(ref DataRead);
                    ListReadLength = NumListRead.Count;

                }
                else
                {
                    DataRead = new byte[ProcessTimer2];
                    filing.Read(DataRead, 0, ProcessTimer2);
                    ReadAble = false;

                    NumListRead = ReaderInts.GetInt_bits(ref DataRead);
                    ListReadLength = NumListRead.Count;



                }

            }
            else
            {
                NumListRead = new List<int>();

            }

            RN = 0;

        }
        public int GetNum()
        {
            if (RN == ListReadLength)
            {
                ReadNums();
                RN = 0;
            }

            RN++;
            return NumListRead[RN - 1];


        }


        #endregion



        public ReaderWriteFileNum02(string MainPathFile, int ModNum)
        {
            Pathfile = MainPathFile;
            Mod = ModNum;
        }
        public ReaderWriteFileNum02(string MainPathFile, int ModNum,  bool isReadMod)
        {
            Pathfile = MainPathFile;
            Mod = ModNum;
            ReaderMod = isReadMod;
        }
        public ReaderWriteFileNum02(string MainPathFile, int ModNum , int NumReaderLength)
        {
            Pathfile = MainPathFile;
            Mod = ModNum;
            ReaderDataLength = NumReaderLength;
        }
        public ReaderWriteFileNum02(string MainPathFile, int ModNum, int NumReaderLength , bool isReadMod)
        {
            Pathfile = MainPathFile;
            Mod = ModNum;
            ReaderDataLength = NumReaderLength;
            ReaderMod = isReadMod;
        }
      

    }






    
}
