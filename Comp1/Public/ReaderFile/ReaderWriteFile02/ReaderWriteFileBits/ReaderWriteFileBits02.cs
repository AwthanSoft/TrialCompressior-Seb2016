using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.Public.ReaderFile.ReaderWriteFile02
{
   public class ReaderWriteFileBits02
    {
       
        #region  File Proprties

        private bool RestBitsExist = false;
        private int AddingBits = 0;
        private bool ReaderMod = false;
        private bool fileIsOpen = false;
        private FileStream filing;
        private string Pathfile;

        private void ImportFilePath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse Files";


            // openFileDialog1.InitialDirectory = @"C:\Users\ALI\Desktop";

            openFileDialog1.DefaultExt = "txt";

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;



            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowReadOnly = true;

            openFileDialog1.ShowDialog();



            Pathfile = openFileDialog1.FileName;


            foreach (String file in openFileDialog1.FileNames)
            {
                Pathfile = file;


            }

            /*  DialogResult result = openFileDialog1.ShowDialog();
              if ( result == DialogResult.OK)
              {
                  OpenSomeFile(openFileDialog1.FileName);
              }*/
        }
        public void OpenFile()
        {
            if (fileIsOpen == false)
            {
                if (Pathfile == null)
                    ImportFilePath();


                filing = new FileStream(Pathfile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                
                fileIsOpen = true;

                if (ReaderMod)
                {
                    long FileSize = filing.Length;

                    
                    if (RestBitsExist)
                    {
                        //There MayBe is Exeption
                    
                        filing.Seek(FileSize - 1, SeekOrigin.Begin);

                        byte[] Restbits = new byte[1];

                        AddingBits = Convert.ToInt32(Restbits[0]);
                        if(AddingBits==0)
                            FileSize = FileSize - 1;
                        else
                            FileSize = FileSize - 2;
                       

                    }

                    ProcessTimer1 = Convert.ToInt32(FileSize / ReaderDataLength);
                    ProcessTimer2 = Convert.ToInt32(FileSize % ReaderDataLength);
                    ReadAble = true;

                    if (RestBitsExist && AddingBits != 0)
                        ProcessTimer2 = ProcessTimer2 + 1;

                    filing.Seek(0, SeekOrigin.Begin);
                }
                else
                    filing.Seek(filing.Length, SeekOrigin.Begin);

            }

        }
        public void CloseFile()
        {
            if (!ReaderMod)
                SaveEndBitArr();

            if (fileIsOpen != false)
            {
                SaveEndBitArr();
                filing.Close();
            }

            fileIsOpen = false;
        }

        #endregion

        #region  Data Save


        /********* Data  **************/
        private int SB = 1024 * 8;
        private int BitArrayLength = 1024 * 8;
        private BitArray BitsArrSave = new BitArray(0);

        public void WriteBit(bool bit)
        {
            if (SB == BitArrayLength)
            {
                SaveAllBitArray();
                
            }

            BitsArrSave[SB] = bit;
            SB++;

        }
        private void SaveAllBitArray()
        {
            if (fileIsOpen == false)
            {
                OpenFile();
            }

            byte[] SaveBitArr = new byte[(BitsArrSave.Length / 8)];

            BitsArrSave.CopyTo(SaveBitArr, 0);
            filing.Write(SaveBitArr, 0, BitsArrSave.Length / 8);
            SB = 0;
            BitsArrSave = new BitArray(BitArrayLength, false);

        }
        private void SaveEndBitArr()
        {
            if (fileIsOpen != false)
            {
                OpenFile();
            }


            {

                byte[] SaveBitArr = new byte[(BitArrayLength / 8) + 1];
                this.BitsArrSave.CopyTo(SaveBitArr, 0);
                filing.Write(SaveBitArr, 0, SB / 8);
                List<bool> TempListBits = new List<bool>();

                //SaveLast
                {
                    {

                        TempListBits = new List<bool>();
                        int SBits = SB - (SB % 8);
                        while (SBits != SB)
                        {
                            TempListBits.Add(this.BitsArrSave[SBits]);
                            SBits++;
                        }

                        int LastBitsNum = TempListBits.Count;

                        if (LastBitsNum != 0)
                        {
                            byte[] SaveLastBit = new byte[2];
                            BitArray lastBitsArr = new BitArray(8);
                            int i = 0;
                            foreach (bool b in TempListBits)
                            {
                                lastBitsArr[i] = b;
                                i++;
                            }
                            lastBitsArr.CopyTo(SaveLastBit, 0);
                            SaveLastBit[1] = numoperation.int32toByte1(LastBitsNum);

                            filing.Write(SaveBitArr, 0, 2);
                        }
                        else
                        {
                            byte[] SaveLastBit = new byte[1];
                            SaveLastBit[0] = numoperation.int32toByte1(LastBitsNum);

                            filing.Write(SaveBitArr, 0, 1);
                        }


                    }
                }


                //BitArray BitsArr = new BitArray(ArrayLength);
                //int sb = 0;

                //foreach (bool b in TempListBits)
                //{
                //    BitsArr[sb] = b;
                //    sb++;
                //}






            }




        }


        #endregion

        #region  Data Read


        /********* Info  **************/
        public bool isAbleRead = true;
        private bool LastBit = false;
        private bool ReadAble = false;
        private int ProcessTimer1 = 0;
        private int ProcessTimer2 = 0;
        private int Process1 = 0;

        /********* Data  **************/
        private int RB = 0;
        private int ReaderDataLength = 1024;
        private int ArrayReadLength = 0;
        private BitArray BitsArrRead = new BitArray(0);

       /******************************/
        private void ReadData()
        {
            if (fileIsOpen == false)
            {
                OpenFile();
            }

            byte[] DataRead;
            if (ReadAble == true)
            {
                if (Process1 != ProcessTimer1)
                {
                    DataRead = new byte[ReaderDataLength];
                    filing.Read(DataRead, 0, ReaderDataLength);

                    Process1++;
                    BitsArrRead = new BitArray(DataRead);
                    ArrayReadLength = BitsArrRead.Count;
                   
                }
                else
                {
                    DataRead = new byte[ProcessTimer2];
                    filing.Read(DataRead, 0, ProcessTimer2);
                    ReadAble = false;

                    BitsArrRead = new BitArray(DataRead);
                  //  ArrayReadLength = BitsArrRead.Count - AddingBits ;
                  
                    ArrayReadLength = BitsArrRead.Count - AddingBits  - 1;

                    LastBit = BitsArrRead[ArrayReadLength];


                }

            }
            else
            {
                DataRead = new byte[0];
                BitsArrRead = new BitArray(1);
                BitsArrRead[0] = LastBit;
                ArrayReadLength = 1;
                isAbleRead = false;
            }

            RB = 0;

        }
        public bool GetBit()
        {
            if (RB == ArrayReadLength)
            {
                ReadData();
            }

            RB++;
            return BitsArrRead[RB - 1];
            

        }
        

        #endregion


        public ReaderWriteFileBits02(bool IsReaderMod)
        {
            ReadWriteFile02 readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            if (IsReaderMod)
            {
                Pathfile = readerFile.ReaderF.PathFile;
                ReaderDataLength = readerFile.ReaderF.ReaderDataLength;
            }
            else
            {
                Pathfile = readerFile.ReaderF.FullSaveFilePath;
                //BitArrayLength = readerFile.ReaderF.ReaderDataLength * 8;
                //SB = BitArrayLength;
            }
             
            ReaderMod = IsReaderMod;


        }
        public ReaderWriteFileBits02(bool IsReaderMod , string Extension)
        {
            ReadWriteFile02 readerFile = new ReadWriteFile02(Extension);
            if (readerFile.IsCancel)
                return;

            if (IsReaderMod)
            {
                Pathfile = readerFile.ReaderF.PathFile;
                ReaderDataLength = readerFile.ReaderF.ReaderDataLength;
            }
            else
            {
                Pathfile = readerFile.ReaderF.FullSaveFilePath;
                //BitArrayLength = readerFile.ReaderF.ReaderDataLength * 8;
                //SB = BitArrayLength;
            }

            ReaderMod = IsReaderMod;


        }
        public ReaderWriteFileBits02(string MainPathFile , bool IsReaderMod)
        {
            Pathfile = MainPathFile;
            ReaderMod = IsReaderMod;
        }
        public ReaderWriteFileBits02(string MainPathFile, bool IsReaderMod,bool RestBitsIsExist)
        {
            Pathfile = MainPathFile;
            ReaderMod = IsReaderMod;

            RestBitsExist = RestBitsIsExist;
        }

    }



   public class ReaderWriteFileBits02B
   {

       #region  File Proprties
       private ReadWriteFile02 readerFile;
       private bool ReaderMod = false;
       private bool fileIsOpen = false;

      
       private int AddingBits = 0;
      

       public void OpenFile()
       {
           if (fileIsOpen == false)
           {


               if (ReaderMod)
               {
                   readerFile.ReadOpen();
               }
               else
               {
                   readerFile.WriteOpen();
               }

               fileIsOpen = true;
           }

       }
       public void CloseFile()
       {
           if (!ReaderMod)
               SaveEndBitArr();

           if (fileIsOpen != false)
           {
               readerFile.CloseAll();
           }

           fileIsOpen = false;
       }

       #endregion

       #region  Data Save


       /********* Data  **************/
       private int SB = 1024 * 8;
       private int BitArrayLength = 1024 * 8;
       private BitArray BitsArrSave = new BitArray(0);

       public bool WriteBitToFile = true;

       public void WriteBit(bool bit)
       {
           if (WriteBitToFile)
           {
               if (SB == BitArrayLength)
               {
                   SaveAllBitArray();

               }

               BitsArrSave[SB] = bit;
               SB++;
           }

           SumOfWriteBits++;

       }
       private void SaveAllBitArray()
       {
           if (fileIsOpen == false)
           {
               OpenFile();
           }

           byte[] SaveBitArr = new byte[(BitsArrSave.Length / 8)];

           BitsArrSave.CopyTo(SaveBitArr, 0);
           List<byte> SaveListt = new List<byte>();


           readerFile.SaveDataByte(ref SaveBitArr, 0, BitsArrSave.Length / 8);
           SB = 0;
           BitsArrSave = new BitArray(BitArrayLength, false);

       }
       private void SaveEndBitArr()
       {
           if (fileIsOpen != false)
           {
               OpenFile();
           }


           {

               byte[] SaveBitArr = new byte[(BitArrayLength / 8) + 1];
               this.BitsArrSave.CopyTo(SaveBitArr, 0);
               readerFile.SaveDataByte(ref SaveBitArr, 0, SB / 8);
               List<bool> TempListBits = new List<bool>();

               //SaveLast
               {
                   {

                       TempListBits = new List<bool>();
                       int SBits = SB - (SB % 8);
                       while (SBits != SB)
                       {
                           TempListBits.Add(this.BitsArrSave[SBits]);
                           SBits++;
                       }

                       int LastBitsNum = TempListBits.Count;

                       if (LastBitsNum != 0)
                       {
                           byte[] SaveLastBit = new byte[2];
                           BitArray lastBitsArr = new BitArray(8);
                           int i = 0;
                           foreach (bool b in TempListBits)
                           {
                               lastBitsArr[i] = b;
                               i++;
                           }
                           lastBitsArr.CopyTo(SaveLastBit, 0);
                           SaveLastBit[1] = numoperation.int32toByte1(LastBitsNum);

                           readerFile.SaveDataByte(ref SaveBitArr, 0, 2);
                       }
                       else
                       {
                           byte[] SaveLastBit = new byte[1];
                           SaveLastBit[0] = numoperation.int32toByte1(LastBitsNum);

                           readerFile.SaveDataByte(ref SaveBitArr, 0, 1);
                       }


                   }
               }


               //BitArray BitsArr = new BitArray(ArrayLength);
               //int sb = 0;

               //foreach (bool b in TempListBits)
               //{
               //    BitsArr[sb] = b;
               //    sb++;
               //}






           }




       }

       public int SumOfWriteBits = 0;
       #endregion

       #region  Data Read


       /********* Info  **************/
       public bool isAbleRead = true;
       private bool LastBit = false;
       private bool RestBitsExist = false;

       /********* Data  **************/
       private int RB = 0;
       private int ArrayReadLength = 0;
       private BitArray BitsArrRead = new BitArray(0);

       /******************************/
       private void ReadData()
       {
           if (fileIsOpen == false)
           {
               OpenFile();
           }

           byte[] DataRead;

           if (readerFile.ReadAble == true)
           {
               readerFile.ReadData();
               BitsArrRead = new BitArray(readerFile.DataRead);
               ArrayReadLength = BitsArrRead.Count;

               if (readerFile.ReadAble == false)
               {
                   //ReadAble = false;
                   ArrayReadLength = BitsArrRead.Count - AddingBits - 1;
                   LastBit = BitsArrRead[ArrayReadLength];

               }

           }
           else
           {
               DataRead = new byte[0];
               BitsArrRead = new BitArray(1);
               BitsArrRead[0] = LastBit;
               ArrayReadLength = 1;
               isAbleRead = false;
           }

           RB = 0;

       }
       public bool GetBit()
       {
           SumOfReadBits++;

           if (RB == ArrayReadLength)
           {
               ReadData();
           }

           RB++;
           return BitsArrRead[RB - 1];


       }


       public int SumOfReadBits = 0;


       #endregion

       

       public ReaderWriteFileBits02B(bool IsReaderMod)
       {
           ReaderMod = IsReaderMod;

            readerFile = new ReadWriteFile02();
           if (readerFile.IsCancel)
               return;

           if (IsReaderMod)
           {

           }
           else
           {
               //BitArrayLength = readerFile.ReaderF.ReaderDataLength * 8;
               //SB = BitArrayLength;
           }
           

       }
       public ReaderWriteFileBits02B(bool IsReaderMod , string Extension)
       {
           ReaderMod = IsReaderMod;

           readerFile = new ReadWriteFile02(Extension);
           if (readerFile.IsCancel)
               return;

           if (IsReaderMod)
           {

           }
           else
           {
               //BitArrayLength = readerFile.ReaderF.ReaderDataLength * 8;
               //SB = BitArrayLength;
           }


       }
       public ReaderWriteFileBits02B(bool IsReaderMod ,bool RestBitsIsExist)
       {
           ReaderMod = IsReaderMod;

           readerFile = new ReadWriteFile02();
           if (readerFile.IsCancel)
               return;

           if (IsReaderMod)
           {

           }
           else
           {
               //BitArrayLength = readerFile.ReaderF.ReaderDataLength * 8;
               //SB = BitArrayLength;
           }


       }


       #region For Other Usage

       public int GetStopNumLength
       {
           get
           {
               return readerFile.ReaderF.StopNumLength;
           }
       }
       public int GetReaderDataLength
       {
           get
           {
               return readerFile.ReaderF.ReaderDataLength;
           }
       }
       public bool GetIsCancel
       {
           get
           {
               return readerFile.IsCancel;
           }
       }
       public string GetSavePath
       {
           get
           {
               return readerFile.ReaderF.FullSaveFilePath;
           }
       }


       #endregion

   }




}
