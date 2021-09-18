using Comp1.Public.Lib;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.ChangerNum
{
    public class MoveToLastAsNum01
    {
        #region  Proprties

        private int Mod = 8;
        private List<int> ListNum;
        private int Counter = 256;

        public int StopSize = 256;
        private int Stoping = 0;

        #endregion

        #region Over
        public MoveToLastAsNum01()
        {

            CreatListNum(Mod);
        }
        public MoveToLastAsNum01(int ModNum)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
        }
        public MoveToLastAsNum01(int ModNum, int Stoping)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
            StopSize = Stoping;
        }

        #endregion

        public void CreatListNum(int ModNum)
        {
            Stoping = 0;
           // Counter = 0;
            ListNum = new List<int>();
            Mod = ModNum;

            
            int Timer = Convert.ToInt32(Math.Pow(2, ModNum));
            for (int i = 0; i != Timer; i++)
            {
                ListNum.Add(i);
           //     Counter++;
            }

            Counter = ListNum.Count - 1;

        }


        #region Make List MTL

        public List<int> MakListMTL_ByStoping(ref List<int> ListData)
        {
            List<int> listSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                if (Stoping == StopSize)
                    CreatListNum(Mod);

                Locate = ListNum.IndexOf(n);
                listSave.Add(Locate);

                for (int i = Locate; i != Counter; i++)
                {
                    ListNum[i] = ListNum[i + 1];
                }

                ListNum[Counter] = n;

                //ListNum.RemoveAt(Locate);
                //ListNum.Add(n);
                

//                Counter++;
                Stoping++;

            }


            return listSave;

        }

        #endregion

        #region Make List DeMTL

        public List<int> MakListDeMTL_ByStoping(ref List<int> ListData)
        {
            List<int> DelistSave = new List<int>();
            int NumLocate;
            foreach (int n in ListData)
            {
                if (Stoping == StopSize)
                    CreatListNum(Mod);

                DelistSave.Add(ListNum[n]);

                
                NumLocate = ListNum[n];

                for (int i = n; i != Counter; i++)
                {
                    ListNum[i] = ListNum[i + 1];
                }

                ListNum[Counter] = NumLocate;

                //ListNum.RemoveAt(n);
                //ListNum.Add(NumLocate);

              //  Counter++;
                Stoping++;

            }


            return DelistSave;

        }

        #endregion


    }

    public class MoveToLastAsBits01
    {
        #region  Proprties

        private int StopSize = 256;
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
        public MoveToLastAsBits01()
        {


        }
        public MoveToLastAsBits01(int StopingSize)
        {
            StopSize = StopingSize;
        }

        #endregion


        #region Make List MTL AsBits ByStoping

        public byte[] MakeListMTL_ByStoping(ref byte[] DataByte)
        {
            BitArray DataBits = new BitArray(DataByte);

            BitArray SaveBitsArr = new BitArray(DataBits.Length);
            int sb = 0;

            foreach (bool b in DataBits)
            {
                if (Stoping == StopSize)
                    InitialBits();


                //Way 02
                if (b == FirstBit)
                {
                    SaveBitsArr[sb] = false; sb++;
                    FirstBit =! b;
                }
                else
                {
                    SaveBitsArr[sb] = true; sb++;
                }





                Stoping++;

            }

            byte[] DataSave = new byte[SaveBitsArr.Length / 8];
            SaveBitsArr.CopyTo(DataSave, 0);

            return DataSave;

        }

        #endregion

        #region Make List DeMTL AsBits ByStoping

        public byte[] MakeListDeMTL_ByStoping(ref byte[] DataByte)
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
                    
                }
                else
                {
                    SaveBitsArr[sb] = FirstBit; sb++;
                    FirstBit = !FirstBit;
                    SecoundBit = !SecoundBit;
                }


                Stoping++;

            }

            byte[] DataSave = new byte[SaveBitsArr.Length / 8];
            SaveBitsArr.CopyTo(DataSave, 0);

            return DataSave;

        }


        #endregion


    }

    public class TrialMakeMTL01
    {
        public StringBuilder RePort;
        private ReadWriteFile02 readerFile;
        private string Extension = "MTL01";
        private string DeExtension = "DeMTL01";
        private int Mod = 8;


        public TrialMakeMTL01()
        {

            RePort = new StringBuilder();


        }
        public TrialMakeMTL01(int ModNum)
        {
            Mod = ModNum;
            RePort = new StringBuilder();


        }


        //01
        public void StartMTL01_AsNum()
        {
            readerFile = new ReadWriteFile02(Extension + "AsNum" + "M" + Mod.ToString());
            if (readerFile.IsCancel)
                return;

            MoveToLastAsNum01 MakeMTL01 = new MoveToLastAsNum01(Mod, readerFile.ReaderF.StopNumLength);

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> MTFdataInt = MakeMTL01.MakListMTL_ByStoping(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref MTFdataInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeMTL01_AsNum()
        {
            readerFile = new ReadWriteFile02(DeExtension + "AsNum" + "M" + Mod.ToString());
            if (readerFile.IsCancel)
                return;

            MoveToLastAsNum01 MakeMTL01 = new MoveToLastAsNum01(Mod, readerFile.ReaderF.StopNumLength);

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> MTFdataInt = MakeMTL01.MakListDeMTL_ByStoping(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref MTFdataInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }

        //02
        public void StartMTL01_AsBits()
        {
            readerFile = new ReadWriteFile02(Extension + "AsAsBit");
            if (readerFile.IsCancel)
                return;

            MoveToLastAsBits01 MakeMTF01 = new MoveToLastAsBits01(readerFile.ReaderF.StopNumLength);

            readerFile.OpenAll();

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                byte[] DataByte = MakeMTF01.MakeListMTL_ByStoping(ref readerFile.DataRead);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeMTL01_AsBits()
        {
            readerFile = new ReadWriteFile02(DeExtension + "AsAsBit");
            if (readerFile.IsCancel)
                return;

            MoveToLastAsBits01 MakeMTF01 = new MoveToLastAsBits01(readerFile.ReaderF.StopNumLength);

            readerFile.OpenAll();

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                byte[] DataByte = MakeMTF01.MakeListDeMTL_ByStoping(ref readerFile.DataRead);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }





    }

}
