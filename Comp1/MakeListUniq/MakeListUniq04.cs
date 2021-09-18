using Comp1.Public.Lib;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeListUniq
{
    public class MakeListUniq04
    {
        private int Mod = 8;
        List<int> ListNum;
        List<int> ListTempNum;
        private int Counter = 0;

        public int Stop = 256;
        private int NumStop = 0;

        public MakeListUniq04(int ModNum)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
        }
        public MakeListUniq04(int ModNum, int Stoping)
        {
            Mod = ModNum;
            CreatListNum(ModNum);
            Stop = Stoping;
        }


        public void CreatListNum(int ModNum)
        {
            NumStop = 0;
            Counter = 0;
            ListNum = new List<int>();
            ListTempNum = new List<int>();
            Mod = ModNum;
            int Timer = Convert.ToInt32(Math.Pow(2, ModNum));
            for (int i = 0; i != Timer; i++)
            {
                ListNum.Add(i);
                ListTempNum.Add(i);
                Counter++;
            }



        }

        #region Make List Uniq

        public List<int> MakeListUniq(ref List<int> ListData)
        {
            List<int> listSave = new List<int>();

            foreach (int n in ListData)
            {
                listSave.Add(ListNum[n]);

                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }

            }


            return listSave;

        }
        public List<int> MakeListUniq(List<int> ListData)
        {
            List<int> listSave = new List<int>();

            foreach (int n in ListData)
            {
                listSave.Add(ListNum[n]);
                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
            }


            return listSave;

        }
        public List<int> MakeListUniq(ref int[] ListData)
        {
            List<int> listSave = new List<int>();

            foreach (int n in ListData)
            {
                listSave.Add(ListNum[n]);
                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
            }


            return listSave;

        }
        public List<int> MakeListUniq(int[] ListData)
        {
            List<int> listSave = new List<int>();

            foreach (int n in ListData)
            {
                listSave.Add(ListNum[n]);
                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
            }


            return listSave;

        }

        public List<int> MakeListUniqByStop(ref List<int> ListData)
        {
            int CurrentNumber;
            int CurrentLocated;
            int locate;
            int LocateNumber;
            int TempNumber;

            List<int> listSave = new List<int>();

            foreach (int n in ListData)
            {
                if (NumStop == Stop)
                    CreatListNum(Mod);

                listSave.Add(ListNum[n]);

                ListNum[n] = Counter;

                Counter++;
                NumStop++;

                //ListNum.RemoveAt(n);

                if (ListTempNum.Count != 0)
                {
                    CurrentNumber = n;
                    locate=ListTempNum.IndexOf(CurrentNumber);
                    if (locate != -1)
                    {
                        ListTempNum.RemoveAt(locate);
                        if (ListTempNum.Count != 0)
                        {
                            TempNumber = ListTempNum[0];
                            LocateNumber = ListNum.IndexOf(TempNumber);

                            if (LocateNumber != -1)
                            {
                                CurrentLocated = ListNum[CurrentNumber];

                                ListNum[CurrentNumber] = TempNumber;
                                ListNum[LocateNumber] = CurrentLocated;

                                //Temp  Improving
                                {
                                    ListTempNum.Add(TempNumber);
                                    ListTempNum.RemoveAt(0);
                                }


                            }

                        }
                    }
                    
                }

               


            }


            return listSave;

        }

        #endregion

        #region Make List DeUniq

        public List<int> MakeListDeUniq(ref List<int> ListData)
        {
            List<int> DelistSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                Locate = ListNum.IndexOf(n);
                DelistSave.Add(Locate);

                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
              

            }


            return DelistSave;

        }
        public List<int> MakeListDeUniq(List<int> ListData)
        {
            List<int> DelistSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                Locate = ListNum.IndexOf(n);
                DelistSave.Add(Locate);

                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
              


            }


            return DelistSave;

        }
        public List<int> MakeListDeUniq(ref int[] ListData)
        {
            List<int> DelistSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                Locate = ListNum.IndexOf(n);
                DelistSave.Add(Locate);

                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
              


            }


            return DelistSave;

        }
        public List<int> MakeListDeUniq(int[] ListData)
        {
            List<int> DelistSave = new List<int>();
            int Locate;
            foreach (int n in ListData)
            {
                Locate = ListNum.IndexOf(n);
                DelistSave.Add(Locate);

                for (int i = 0; i != ListNum.Count; i++)
                {
                    ListNum[i]++;
                }
              


            }


            return DelistSave;


        }

        public List<int> MakeListDeUniqByStop(ref List<int> ListData)
        {
            int Locater;
            int CurrentNumber;
            int CurrentLocated;
            int locate;
            int LocateNumber;
            int TempNumber;

            List<int> DelistSave = new List<int>();

            foreach (int n in ListData)
            {

                if (NumStop == Stop)
                    CreatListNum(Mod);

                Locater = ListNum.IndexOf(n);
                DelistSave.Add(Locater);

                ListNum[Locater] = Counter;

                Counter++;
                NumStop++;

                //ListNum.RemoveAt(n);

                if (ListTempNum.Count != 0)
                {
                    CurrentNumber = Locater;
                    locate = ListTempNum.IndexOf(CurrentNumber);
                    if (locate != -1)
                    {
                        ListTempNum.RemoveAt(locate);
                        if (ListTempNum.Count != 0)
                        {
                            TempNumber = ListTempNum[0];
                            LocateNumber = ListNum.IndexOf(TempNumber);

                            if (LocateNumber != -1)
                            {
                                CurrentLocated = ListNum[CurrentNumber];

                                ListNum[CurrentNumber] = TempNumber;
                                ListNum[LocateNumber] = CurrentLocated;

                                //Temp  Improving
                                {
                                    ListTempNum.Add(TempNumber);
                                    ListTempNum.RemoveAt(0);
                                }


                            }

                        }
                    }

                }




            }


            return DelistSave;

        }
        #endregion


    }

    public class TrialMakeListUniq04
    {
        public StringBuilder RePort;
        private ReadWriteFile02 readerFile;
        private string Extension = "MLU04";
        private string DeExtension = "MLDU04";
        private int Mod = 8;

        public TrialMakeListUniq04(int ModNum)
        {
            Mod = ModNum;
            RePort = new StringBuilder();


        }
        public void StartUniq()
        {
            MakeListUniq04 MakeUniq = new MakeListUniq04(Mod);

            readerFile = new ReadWriteFile02((Mod.ToString() + Extension));
            if (readerFile.IsCancel)
                return;

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod + 1);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
                MakeUniq.CreatListNum(Mod);

                List<int> UniqInt = MakeUniq.MakeListUniq(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }

        public void StartDeUniq()
        {
            MakeListUniq02 MakeUniq = new MakeListUniq02(Mod);

            readerFile = new ReadWriteFile02((Mod.ToString() + DeExtension));
            if (readerFile.IsCancel)
                return;

            readerFile.ReaderF.StopNumLength = (Convert.ToInt32(Math.Pow(2, Mod)) * (Mod + 1)) / 8;

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod + 1);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
                MakeUniq.CreatListNum(Mod);

                List<int> UniqInt = MakeUniq.MakeListDeUniq(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }





    }


}
