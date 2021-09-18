using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Collections;



namespace Comp1.Public.ReaderWriteFile02
{





    public class ReaderWriterInfo02
    {
        private ProgressForm02 form;
        Task tr;
        public long FileReadSize = 0;
        public int DataReadLength = 256;

        public long RestSize0 = 0;
        public long SizeDone0 = 0;
        public long SizeSaved0 = 0;


        public double Progress = 0;


        #region Properties

        //delegate void StartOpening( FileReaderInfo02 ReaderF);
        //delegate void SavingData(long SaveSize);
        //delegate void ReadingData(long restData , long DataDone , Double ReadErProgress);


        public event EventHandler<FileReaderInfo02EventArgs> OpeningIsStarting;
        public event EventHandler DataIsSaving;
        public event EventHandler DataIsReading;
        public event EventHandler OpeningIsEnd;


        #endregion




        public ReaderWriterInfo02()
        {
            form = new ProgressForm02();
            OpeningIsStarting += new EventHandler<FileReaderInfo02EventArgs>(form.Start);
       //     DataIsSaving += new EventHandler(form.RefrishSaveData);
      //      DataIsReading += new EventHandler(form.RefrishProgress);
       //     OpeningIsEnd += new EventHandler(form.EndOpning);

            form.ReaderInfo = this;

         //   tr = new Thread( new System.Threading.ThreadStart(() =>
         //   {

            //ProgressForm02 form;
         //       form.ShowDialog();

         //   }));

         ////   tr.IsBackground = true;
         //   tr.ApartmentState = ApartmentState.MTA;

          
         //   tr.Join();


        }
        public ReaderWriterInfo02(int x)
        {
            



        }


      


        public void StartOpen(ref FileReaderInfo02 ReaderF)
        {
            form.filingInfo = ReaderF;
            OnOpeningIsStarting(ReaderF);
            

            tr = new Task(() =>
            {
             //   form.Show();
                form.ShowDialog();

            });
           // tr.ApartmentState = ApartmentState.STA;
            tr.Start();

        }
       

        public void DataReading()
        {
            RestSize0 = RestSize0 - DataReadLength;
            SizeDone0 = SizeDone0 + DataReadLength;
            Progress = (100) / (Convert.ToDouble(FileReadSize) / Convert.ToDouble(SizeDone0));

            //Event
            OnDataIsReading();

        }
        public void DataReading(int DataLength)
        {
            RestSize0 = RestSize0 - DataLength;
            SizeDone0 = SizeDone0 + DataLength;
            Progress = (100) / (Convert.ToDouble(FileReadSize) / Convert.ToDouble(SizeDone0));

            //Event
            OnDataIsReading();

        }
        public void EndReadingData()
        {
            //event
            OnOpeningIsEnd();
        }
        public void DataSaveing(int SizeSaved)
        {
            SizeSaved0 = SizeSaved0 + SizeSaved;



            //Event
            OnDataIsSaving();
        }




        public virtual void OnOpeningIsStarting(FileReaderInfo02 ReaderF)
        {
            if (OpeningIsStarting != null)
            {
                OpeningIsStarting(this, new FileReaderInfo02EventArgs(ReaderF));
            }
        }
        public virtual void OnDataIsSaving()
        {
            if (DataIsSaving != null)
            {
                DataIsSaving(this, new EventArgs());
            }
        }
        public virtual void OnDataIsReading()
        {
            if (DataIsReading != null)
            {
                DataIsReading(this, new EventArgs());
            }
        }
        public virtual void OnOpeningIsEnd()
        {
            if (OpeningIsEnd != null)
            {
                OpeningIsEnd(null, new EventArgs());
            }
        }





    }


  public  class ReadWriteFile02  
  {
      #region MainObject
      public FileReaderInfo02 ReaderF;
      private ReaderWriterInfo02 ReaderInfo;

      public bool IsCancel = false;


      #endregion

      #region FileStream

      private  bool ReadFileisOpen = false;
      private FileStream Readfiling;

      private bool WriteFileisOpen = false;
      private FileStream Writefiling;
      private long WriteFileSize = 0;
      public byte[] DataRead = new byte[0];


      #endregion

      #region Processes Info

      public bool ReadAble = false;
      private int ProcessTimer1 = 0;
      private int ProcessTimer2 = 0;
      private int Process1 = 0;


      #endregion


      #region Overload Method

      public ReadWriteFile02()
      {
          FileReaderForm02 form = new FileReaderForm02();

          ReaderF = form.Start();
          form.Dispose();

          //IsCancel
          if (ReaderF == null)
          {
              IsCancel = true;
              return;
          }
          if (!ReaderF.ReaderReady)
          {
              IsCancel = true;
              return;
          }

          ReaderInfo = new ReaderWriterInfo02();

          


      }
      public ReadWriteFile02(ReaderWriterInfo02 ReadingInfo )
      {
          FileReaderForm02 form = new FileReaderForm02();

          ReaderF = form.Start();
          form.Dispose();
          //IsCancel
          if (ReaderF == null)
          {
              IsCancel = true;
              return;
          }
          if (!ReaderF.ReaderReady)
          {
              IsCancel = true;
              return;
          }
          ReaderInfo = ReadingInfo;


      }
      public ReadWriteFile02(string SaveExtension)
      {
          FileReaderForm02 form = new FileReaderForm02();
          form.SaveExtension = SaveExtension;

          ReaderF = form.Start();
          form.Dispose();

          //IsCancel
          if (ReaderF == null)
          {
              IsCancel = true;
              return;
          }
          if (!ReaderF.ReaderReady)
          {
              IsCancel = true;
              return;
          }
          ReaderInfo = new ReaderWriterInfo02();

      }

      #endregion

      #region Read & Save Data
      public void ReadData(ref byte[] DataArr)
      {
          if (ReadAble == true)
          {
              if (Process1 != ProcessTimer1)
              {
                  byte[] dataFile = new byte[ReaderF.ReaderDataLength];
                  Readfiling.Read(dataFile, 0, ReaderF.ReaderDataLength);

                  Process1++;

                  DataArr = dataFile;

                  ReaderInfo.DataReading();
              }
              else
              {
                  byte[] dataFile = new byte[ProcessTimer2];
                  Readfiling.Read(dataFile, 0, ProcessTimer2);
                  ReadAble = false;

                  DataArr = dataFile;

                  ReaderInfo.DataReading(ProcessTimer2);
                  ReaderInfo.EndReadingData();


              }

          }
          else
          {
              byte[] dataFile = new byte[0];

              DataArr = dataFile;
              ReaderInfo.EndReadingData();

          }

         
      }
      public void ReadData()
      {
          if (ReadAble == true)
          {
              if (Process1 != ProcessTimer1)
              {
                  DataRead = new byte[ReaderF.ReaderDataLength];
                  Readfiling.Read(DataRead, 0, ReaderF.ReaderDataLength);

                  Process1++;

                  ReaderInfo.DataReading();
              }
              else
              {
                  DataRead = new byte[ProcessTimer2];
                  Readfiling.Read(DataRead, 0, ProcessTimer2);
                  ReadAble = false;

                  ReaderInfo.DataReading(ProcessTimer2);
                  ReaderInfo.EndReadingData();

              }

          }
          else
          {
              DataRead = new byte[0];
              ReaderInfo.EndReadingData();

          }

        

      }

      public void SaveDataByte(byte[] DataArr)
      {
          Writefiling.Write(DataArr, 0, DataArr.Length);

          ReaderInfo.DataSaveing(DataArr.Length);
      }
      public void SaveDataByte(ref byte[] DataArr)
      {
          Writefiling.Write(DataArr, 0, DataArr.Length);

          ReaderInfo.DataSaveing(DataArr.Length); ;
      }
      public void SaveDataByte(byte[] DataArr, int Start , int Count)
      {
          Writefiling.Write(DataArr, Start, Count);

          ReaderInfo.DataSaveing(Count);
      }
      public void SaveDataByte(ref byte[] DataArr, int Start, int Count)
      {
          Writefiling.Write(DataArr, Start, Count);

          ReaderInfo.DataSaveing(Count);
      }

      #endregion

      #region Open Files Method

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


      
      private void GetAddingBits()
      {
         

          if (ReaderF.RestBitsExist)
          {
              long FileSize = ReaderF.FileSize;

              //There MayBe is Exeption

              Readfiling.Seek(FileSize - 1, SeekOrigin.Begin);

              byte[] Restbits = new byte[1];
              Readfiling.Read(Restbits, 0, 1);

              ReaderF.AddingBits = Convert.ToInt32(Restbits[0]);
              if (ReaderF.AddingBits == 0)
                  FileSize = FileSize - 1;
              else
                  FileSize = FileSize - 2;


              ReaderF.FileSize = FileSize;

          }

      }
      public List<bool> GetLastBits()
      {
          List<bool> listBits = new List<bool>();
          if (ReaderF.AddingBits > 0)
          {
              byte[] Restbits = new byte[1];
              Readfiling.Read(Restbits, 0, 1);
              int Count=8-ReaderF.AddingBits;
              BitArray bitArr = new BitArray(Restbits);

              
              for (int i = 0; i != Count; i++)
              {
                  listBits.Add(bitArr[i]);
              }
           
          }
          return listBits;
      }
      public void ReadOpen()
      {
          if (ReadFileisOpen == false)
          {
              Readfiling = new FileStream(ReaderF.PathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
              Readfiling.Seek(0, SeekOrigin.Begin);

        //      GetAddingBits();
              //ReadFileSize = Readfiling.Length;
              ReadFileisOpen = true;

              ReaderInfo.DataReadLength = ReaderF.ReaderDataLength;

              ProcessTimer1 = Convert.ToInt32(ReaderF.FileSize / ReaderF.ReaderDataLength);
              ProcessTimer2 = Convert.ToInt32(ReaderF.FileSize % ReaderF.ReaderDataLength);
              ReadAble = true;

              ReaderInfo.RestSize0 = ReaderF.FileSize;
              ReaderInfo.SizeDone0 = 0;
              ReaderInfo.FileReadSize = ReaderF.FileSize;

              ReaderInfo.StartOpen(ref ReaderF);

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

              if (!Directory.Exists(ReaderF.SaveFileDir))
                  Directory.CreateDirectory(ReaderF.SaveFileDir);

              Writefiling = new FileStream(ReaderF.FullSaveFilePath , FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
              Writefiling.Seek(Writefiling.Length, SeekOrigin.Begin);

              ReaderInfo.SizeSaved0 = Writefiling.Length;

              WriteFileSize = Writefiling.Length;
              WriteFileisOpen = true;

          }

      }
      public void WriteClose()
      {
          if (WriteFileisOpen != false)
          {
            
              Writefiling.Close();
              WriteFileisOpen = false;
          }
      }

      #endregion





     








  }
}
