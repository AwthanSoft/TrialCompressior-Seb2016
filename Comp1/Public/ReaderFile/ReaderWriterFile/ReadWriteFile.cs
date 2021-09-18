using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderWriterFile
{
    

    public class ReadWriteFile00
    {
#region  InfoFile
        public string Extention = "";
        public string PathFileRead;


        public bool ReadFileisOpen = false;
        public FileStream Readfiling;
        public long ReadFileSize = 0;

        public int BlockReaderLength = 256;

        public bool ReadAble = false;
        private int ProcessTimer1 = 0;
        private int ProcessTimer2 = 0;
        private int Process1 = 0;


        public string PathFileWrite;
        public bool WriteFileisOpen = false;
        public FileStream Writefiling;
        public long WriteFileSize = 0;
        public byte[] DataRead = new byte[0];


        /*** Progress *********/
        public long RestSize0 = 0;
        public long SizeDone0 = 0;
        public long SaveSize0 = 0;


        private ProgressForm01 ProgressForm = new ProgressForm01();

        /******** BitArr ***********/
        private bool BitArrIsOpen = false;
        private int BitArrSize = 1024 * 1024 * 8;
        private BitArray BitsArr = new BitArray(1024 * 1024 * 8);
        private int SBit = 0;

#endregion


        public ReadWriteFile00()
        {
            FileReaderForm form = new FileReaderForm();
            Extention = form.Extension;
            form.DataReadLength = BlockReaderLength;

            form.ShowDialog();
           
            PathFileRead = form.PathFile;
            ReadFileSize = form.FileSize;
            PathFileWrite = form.SaveFilePath;
            try
            {
                BlockReaderLength = form.DataReadLength;
                if (BlockReaderLength <= 0)
                    BlockReaderLength = 1024 * 1024;
            }
            catch
            {
                BlockReaderLength = 1024 * 1024;
            }




        }
        public ReadWriteFile00(string Extension)
        {
            FileReaderForm form = new FileReaderForm();
            
            form.DataReadLength = BlockReaderLength;
            form.ShowDialog();

            PathFileRead = form.PathFile;
            ReadFileSize = form.FileSize;
            PathFileWrite = form.SaveFilePath;
            try
            {
                BlockReaderLength = form.DataReadLength;
                if (BlockReaderLength <= 0)
                    BlockReaderLength = 1024 * 1024;
            }
            catch
            {
                BlockReaderLength = 1024 * 1024;
            }




        }
        public ReadWriteFile00(string Path, string Extension)
        {
            PathFileRead = Path;
            Extention = Extension;

            FileInfo fil = new FileInfo(PathFileRead);
            string FileCompDerct = fil.FullName.Remove(fil.FullName.Length - fil.Extension.Length);
            Directory.CreateDirectory(FileCompDerct);

            PathFileWrite = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + "." + Extention;

        }
        public ReadWriteFile00(int num)
        {

        }


        public void ReadData(ref byte[] DataArr)
        {
            if (ReadAble == true)
            {
                if (Process1 != ProcessTimer1)
                {
                    byte[] dataFile = new byte[BlockReaderLength];
                    Readfiling.Read(dataFile, 0, BlockReaderLength);

                    Process1++;

                    DataArr = dataFile;

                    RestSize0 = RestSize0 - BlockReaderLength;
                    SizeDone0 = SizeDone0 + BlockReaderLength;
                }
                else
                {
                    byte[] dataFile = new byte[ProcessTimer2];
                    Readfiling.Read(dataFile, 0, ProcessTimer2);
                    ReadAble = false;

                    DataArr = dataFile;

                    RestSize0 = RestSize0 - ProcessTimer2;
                    SizeDone0 = SizeDone0 + ProcessTimer2;

                }

            }
            else
            {
                byte[] dataFile = new byte[0];

                DataArr = dataFile;
            }

            ProgressForm.Refrish(this);
        }
        public void ReadData()
        {
            if (ReadAble == true)
            {
                if (Process1 != ProcessTimer1)
                {
                    DataRead = new byte[BlockReaderLength];
                    Readfiling.Read(DataRead, 0, BlockReaderLength);

                    Process1++;

                    RestSize0 = RestSize0 - BlockReaderLength;
                    SizeDone0 = SizeDone0 + BlockReaderLength;
                }
                else
                {
                    DataRead = new byte[ProcessTimer2];
                    Readfiling.Read(DataRead, 0, ProcessTimer2);
                    ReadAble = false;

                    RestSize0 = RestSize0 - ProcessTimer2;
                    SizeDone0 = SizeDone0 + ProcessTimer2;
                }

            }
            else
            {
                DataRead = new byte[0];


            }

            ProgressForm.Refrish(this);


        }

        public void SaveDataByte(byte[] DataArr)
        {
            Writefiling.Write(DataArr, 0, DataArr.Length);

            SaveSize0 = SaveSize0 + DataArr.Length;
            //     ProgressForm.Refrish(this);
        }
        public void SaveDataByte(ref byte[] DataArr)
        {
            Writefiling.Write(DataArr, 0, DataArr.Length);

            SaveSize0 = SaveSize0 + DataArr.Length;
            //     ProgressForm.Refrish(this);
        }


        /***************   BitArray  *****/
        public void AddBit(bool bit)
        {
            if (SBit != BitArrSize)
            {
                BitsArr[SBit] = bit;
                SBit++;
            }
            else
            {
                SaveBitArr();
            }


        }
        private void SaveBitArr()
        {
            if (BitArrIsOpen == false)
                BitArrIsOpen = true;

            byte[] databit = new byte[(BitsArr.Length / 8) + 1];
            BitsArr.CopyTo(databit, 0);
            Writefiling.Write(databit, 0, SBit / 8);
            SaveSize0 = SaveSize0 + (SBit / 8);

            int restbit = SBit - (SBit % 8);
            List<bool> Temp = new List<bool>();
            //RestData
            if (restbit != 0)
            {
                int StartBit = (SBit / 8) * 8;
                int EndBit = SBit;


                while (StartBit != EndBit)
                {
                    Temp.Add(BitsArr[StartBit]);

                    StartBit++;
                }

            }

            BitsArr = new BitArray(BitArrSize);
            SBit = 0;
            foreach (bool b in Temp)
            {
                BitsArr[SBit] = b;
                SBit++;
            }

        }
        private void CloseBitArr()
        {
            SaveBitArr();
            BitArrIsOpen = false;
            BitsArr = new BitArray(BitArrSize);
            SBit = 0;

        }


        /************Open & Close *******/

        public void OpenAll()
        {
            ReadOpen();
            WriteOpen();

        }
        public void CloseAll()
        {
            ReadClose();
            WriteClose();

        }

        public void ReadOpen()
        {
            if (ReadFileisOpen == false)
            {
                Readfiling = new FileStream(PathFileRead, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                Readfiling.Seek(0, SeekOrigin.Begin);
                ReadFileSize = Readfiling.Length;
                ReadFileisOpen = true;

                ProcessTimer1 = Convert.ToInt32(ReadFileSize / BlockReaderLength);
                ProcessTimer2 = Convert.ToInt32(ReadFileSize % BlockReaderLength);
                ReadAble = true;
                RestSize0 = ReadFileSize;
                SizeDone0 = 0;


                ProgressForm = new ProgressForm01();
                ProgressForm.FillInfo(this);
                ProgressForm.Show();
            }

        }
        public void ReadClose()
        {
            if (ReadFileisOpen != false)
            {
                Readfiling.Close();
                ReadFileisOpen = false;
            }
        }

        public void WriteOpen()
        {
            if (WriteFileisOpen == false)
            {



                Writefiling = new FileStream(PathFileWrite + Extention, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                Writefiling.Seek(Writefiling.Length, SeekOrigin.Begin);


                WriteFileSize = Writefiling.Length;
                SaveSize0 = WriteFileSize;
                WriteFileisOpen = true;

            }

        }
        public void WriteClose()
        {
            if (WriteFileisOpen != false)
            {
                //  if (BitArrIsOpen == true)
                CloseBitArr();


                Writefiling.Close();
                WriteFileisOpen = false;
            }
        }



    }








}
