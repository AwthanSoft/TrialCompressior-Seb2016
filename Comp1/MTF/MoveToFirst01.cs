using Comp1.Public.Lib;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.ChangerNum.MTF
{


  public class MoveToFirstAsByte01
    {
        #region  Proprties

        private int Mod = 8;
        private List<int> ListNum;
        private int Counter = 0;

        public int StopSize = 256;
        private int Stoping = 0;

        #endregion

        #region Over
        public MoveToFirstAsByte01()
        {

            CreatListNum(Mod);
        }
        public MoveToFirstAsByte01(int ModNum)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
        }
        public MoveToFirstAsByte01(int ModNum, int Stoping)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
            StopSize = Stoping;
        }

        #endregion

        public void CreatListNum(int ModNum)
        {
            Stoping = 0;
            Counter = 0;
            ListNum = new List<int>();
            Mod = ModNum;
            int Timer = Convert.ToInt32(Math.Pow(2, ModNum));
            for (int i = 0; i != Timer; i++)
            {
                ListNum.Add(i);
                Counter++;
            }



        }


        #region Make List MTF

        public List<int> MakListMTF_ByStoping(ref List<int> ListData)
        {
            List<int> DelistSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                if (Stoping == StopSize)
                    CreatListNum(Mod);

                Locate = ListNum.IndexOf(n);
                DelistSave.Add(Locate);

                for (int i = Locate; i != 0; i--)
                {
                    ListNum[i] = ListNum[i - 1];
                }

                ListNum[0] = n;

                Counter++;
                Stoping++;

            }


            return DelistSave;

        }

        #endregion

        #region Make List DeMTF

        public List<int> MakListDeMTF_ByStoping(ref List<int> ListData)
        {
            List<int> DelistSave = new List<int>();
            int NumLocate;
            foreach (int n in ListData)
            {
                if (Stoping == StopSize)
                    CreatListNum(Mod);

                DelistSave.Add(ListNum[n]);

                NumLocate = ListNum[n];
                

                for (int i = n; i != 0; i--)
                {
                    ListNum[i] = ListNum[i - 1];
                }

                ListNum[0] = NumLocate;

                Counter++;
                Stoping++;

            }


            return DelistSave;

        }

        #endregion

       
    }

  public class MoveToFirstAsBits01
  {
      #region  Proprties

      private  int StopSize = 256;
      private int Stoping = 0;

      private bool FirstBit = false;
      private bool SecoundBit = true;

      private void InitialBits()
      {
          FirstBit = false;
          SecoundBit = true;
          Stoping = 0;
      }

      #endregion

      #region Over
      public MoveToFirstAsBits01()
      {

          
      }
      public MoveToFirstAsBits01( int StopingSize)
      {
          StopSize = StopingSize;
      }

      #endregion


      #region Make List MTF AsBits ByStoping

      public byte[] MakeListMTF_ByStoping(ref byte[] DataByte)
      {
          BitArray DataBits = new BitArray(DataByte);

          BitArray SaveBitsArr = new BitArray(DataBits.Length);
          int sb = 0;

          foreach (bool b in DataBits)
          {
              if (Stoping == StopSize)
                  InitialBits();

              ////Way 01
              //if (b == true)
              //{
              //    SaveBitsArr[sb] = SecoundBit; sb++;
              //    FirstBit = !FirstBit;
              //    SecoundBit = !SecoundBit;
              //}
              //else
              //{
              //    SaveBitsArr[sb] = FirstBit; sb++;

              //}

              //Way 02
              if (b == FirstBit)
              {
                  SaveBitsArr[sb] = false; sb++;
              }
              else
              {
                  SaveBitsArr[sb] = true ; sb++;

                  FirstBit = b;
                  
              }


             


              Stoping++;

          }

          byte[] DataSave = new byte[SaveBitsArr.Length / 8];
          SaveBitsArr.CopyTo(DataSave, 0);

          return DataSave;

      }

      #endregion

      #region Make List DeMTF AsBits ByStoping

      public byte[] MakeListDeMTF_ByStoping(ref byte[] DataByte)
      {
          BitArray DataBits = new BitArray(DataByte);

          BitArray SaveBitsArr = new BitArray(DataBits.Length);
          int sb = 0;

          foreach (bool b in DataBits)
          {
              if (Stoping == StopSize)
                  InitialBits();

              if (b == true)
              {
                  SaveBitsArr[sb] = SecoundBit; sb++;
                  FirstBit = !FirstBit;
                  SecoundBit = !SecoundBit;
              }
              else
              {
                  SaveBitsArr[sb] = FirstBit; sb++;

              }


              Stoping++;

          }

          byte[] DataSave = new byte[SaveBitsArr.Length / 8];
          SaveBitsArr.CopyTo(DataSave, 0);

          return DataSave;

      }

     
      #endregion


  }


  public class TrialMakeMTF01
  {
      public StringBuilder RePort;
      private ReadWriteFile02 readerFile;
      private string Extension = "MTF01";
      private string DeExtension = "DeMTF01";
      private int Mod = 8;


      public TrialMakeMTF01()
      {
         
          RePort = new StringBuilder();


      }
      public TrialMakeMTF01(int ModNum)
      {
          Mod = ModNum;
          RePort = new StringBuilder();


      }


      //01
      public void StartMTF01_AsByte()
      {
          readerFile = new ReadWriteFile02(Extension+"AsByte"+"M"+Mod.ToString());
          if (readerFile.IsCancel)
              return;

          MoveToFirstAsByte01 MakeMTF01 = new MoveToFirstAsByte01(Mod, readerFile.ReaderF.StopNumLength);

          readerFile.OpenAll();

          BitsToInt IntReader = new BitsToInt(Mod);
          IntBitsOperations BitsReader = new IntBitsOperations(Mod);

          while (readerFile.ReadAble == true)
          {
              readerFile.ReadData();

              List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

              List<int> MTFdataInt = MakeMTF01.MakListMTF_ByStoping(ref intData);

              byte[] DataByte = BitsReader.GetIntsAsByteArr(ref MTFdataInt);

              readerFile.SaveDataByte(ref DataByte);

          }

          readerFile.CloseAll();



      }
      public void StartDeMTF01_AsByte()
      {
          readerFile = new ReadWriteFile02(DeExtension + "AsByte" + "M" + Mod.ToString());
          if (readerFile.IsCancel)
              return;

          MoveToFirstAsByte01 MakeMTF01 = new MoveToFirstAsByte01(Mod, readerFile.ReaderF.StopNumLength);

          readerFile.OpenAll();

          BitsToInt IntReader = new BitsToInt(Mod);
          IntBitsOperations BitsReader = new IntBitsOperations(Mod);

          while (readerFile.ReadAble == true)
          {
              readerFile.ReadData();

              List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

              List<int> MTFdataInt = MakeMTF01.MakListDeMTF_ByStoping(ref intData);

              byte[] DataByte = BitsReader.GetIntsAsByteArr(ref MTFdataInt);

              readerFile.SaveDataByte(ref DataByte);

          }

          readerFile.CloseAll();



      }
   
      //02
      public void StartMTF01_AsBits()
      {
          readerFile = new ReadWriteFile02(Extension + "AsAsBit");
          if (readerFile.IsCancel)
              return;

          MoveToFirstAsBits01 MakeMTF01 = new MoveToFirstAsBits01( readerFile.ReaderF.StopNumLength);

          readerFile.OpenAll();

          while (readerFile.ReadAble == true)
          {
              readerFile.ReadData();

              byte[] DataByte = MakeMTF01.MakeListMTF_ByStoping(ref readerFile.DataRead);

              readerFile.SaveDataByte(ref DataByte);

          }

          readerFile.CloseAll();



      }
      public void StartDeMTF01_AsBits()
      {
          readerFile = new ReadWriteFile02(DeExtension + "AsAsBit");
          if (readerFile.IsCancel)
              return;

          MoveToFirstAsBits01 MakeMTF01 = new MoveToFirstAsBits01(readerFile.ReaderF.StopNumLength);

          readerFile.OpenAll();

          while (readerFile.ReadAble == true)
          {
              readerFile.ReadData();

              byte[] DataByte = MakeMTF01.MakeListDeMTF_ByStoping(ref readerFile.DataRead);

              readerFile.SaveDataByte(ref DataByte);

          }

          readerFile.CloseAll();



      }
    




  }


}
