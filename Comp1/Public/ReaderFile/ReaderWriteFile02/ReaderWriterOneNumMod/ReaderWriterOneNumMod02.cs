using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod
{
   public class ReaderWriterOneNumMod02
    {
       #region  Main Proprties
        private bool isReaderMod = true;

        public bool isAbleRead;
        private ReaderWriteFileBits02B ReaderBits;

        private int ModNumLength = 256;
        private int ModNumRoot = 8;

        private ReaderWriterOneNum02B[] ListReaderWriterNum;

        public ReaderWriterOneNumMod02()
        {
            isAbleRead = true;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();


        }
        public ReaderWriterOneNumMod02(bool isReadMod)
        {
            isReaderMod = isReadMod;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }
        public ReaderWriterOneNumMod02(bool isReadMod, string Extension)
        {
            isReaderMod = isReadMod;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod, Extension);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }
        public ReaderWriterOneNumMod02( bool isReadMod , int ModNumbLength)
        {
            isReaderMod = isReadMod;
            ModNumLength = ModNumbLength;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }
        public ReaderWriterOneNumMod02(bool isReadMod, int ModNumbLength, ref ReaderWriteFileBits02B ReaderBit)
        {
            isReaderMod = isReadMod;
            ModNumLength = ModNumbLength;
            ReaderBits = ReaderBit;
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }
        public ReaderWriterOneNumMod02(bool isReadMod, ref  ReaderWriteFileBits02B ReaderBit)
        {
            isReaderMod = isReadMod;
            ReaderBits = ReaderBit;
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }
        public ReaderWriterOneNumMod02(bool isReadMod, int ModNumbLength, string Extension)
        {
            isReaderMod = isReadMod;
            ModNumLength = ModNumbLength;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod, Extension);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            isAbleRead = true;
            Create();
        }



        private void Create()
        {
            ListReaderWriterNum = new ReaderWriterOneNum02B[ModNumLength + 1];

            ListReaderWriterNum[0] = new ReaderWriterOneNum02B(isReaderMod, 3,  ReaderBits);
            ListReaderWriterNum[1] = new ReaderWriterOneNum02B(isReaderMod, 4,  ReaderBits);

            for (int i = 2; i <= ModNumLength; i++)
            {
                ListReaderWriterNum[i] = new ReaderWriterOneNum02B(isReaderMod, i,  ReaderBits);
            }


            LengthModListReader = ModNumLength;
            int TempTimerr = 0;

            int Sum = Convert.ToInt32(Math.Pow(2, TempTimerr));

           while (ModNumLength > Sum)
            {
                TempTimerr++;
                Sum = Convert.ToInt32(Math.Pow(2, TempTimerr));
            }

           ModNumRoot = TempTimerr;


        }
        public void End()
        {
            ReaderBits.CloseFile();

        }



        #endregion

        #region  Number Read
     
        private int TempTimer = 8;
        private int SumNum = 0;
        private int StopTimer = 0;
        private int TempOutNum = 0;
        private int LengthModListReader = 256;

        private int GetNumber(int Length)
        {
           
                if (Length <= LengthModListReader)
                {
                    return ListReaderWriterNum[Length].GetOneNumber();
                }

                TempTimer = ModNumRoot;
                SumNum = 0;
                StopTimer = 0;
                TempOutNum = 0;
                SumNum = Convert.ToInt32(Math.Pow(2, TempTimer));

                while (  Length > SumNum )
                {
                    TempTimer++;
                    SumNum = Convert.ToInt32(Math.Pow(2, TempTimer));
                }

                StopTimer = Length - Convert.ToInt32(Math.Pow(2, TempTimer));

              
                if (StopTimer < 0)
                {
                    TempTimer--;
                    SumNum = Convert.ToInt32(Math.Pow(2, TempTimer));
                    StopTimer = Length - Convert.ToInt32(Math.Pow(2, TempTimer));

                    TempTimer--;

                    i = 0;
                    while (TempTimer != -1)
                    {
                        if (ReaderBits.GetBit())
                        {

                            TempOutNum = TempOutNum + Convert.ToInt32(Math.Pow(2, i));
                        }

                        TempTimer--;
                        i++;
                    }

                  
                    if (TempOutNum < StopTimer)
                    {
                        if (ReaderBits.GetBit())
                        {

                            TempOutNum = TempOutNum + SumNum;
                        }
                    }

                }
                else
                {
                    //TempTimer--;
                    //TempOutNum = 0;
                    //while (TempTimer != -1)
                    //{
                    //    if (ReaderBits.GetBit())
                    //    {

                    //        TempOutNum = TempOutNum + Convert.ToInt32(Math.Pow(2, TempTimer));
                    //    }

                    //    TempTimer--;
                    //}

                    TempTimer--;
                    TempOutNum = 0;
                    i = 0;
                    while (TempTimer != -1)
                    {
                        if (ReaderBits.GetBit())
                        {

                            TempOutNum = TempOutNum + Convert.ToInt32(Math.Pow(2, i));
                        }

                        TempTimer--;
                        i++;
                    }

                }

                return TempOutNum;
            }

        public int GetOneNumber(int Length)
        {
            if (ReaderBits.isAbleRead)
            {
                return GetNumber(Length);
            }
            else
            {
                isAbleRead = false;
                return 0;
            }
        }
   
        


        #endregion

        #region  Number Save

        private int WTempTimer = 8;
        private int WSumNum = 0;
        private int WStopTimer = 0;
        private int WTempOutNum = 0;
        private int i = 0;
        private void SaveNum(int ModLength ,int Num)
        {
            if (ModLength <= LengthModListReader)
            {
                ListReaderWriterNum[ModLength].WriteNumber(Num);
            }
            else
            {
                WTempTimer = ModNumRoot;
                WSumNum = 0;
                WStopTimer = 0;
                WTempOutNum = 0;
                WSumNum = Convert.ToInt32(Math.Pow(2, WTempTimer));

                while (ModLength > WSumNum)
                {
                    WTempTimer++;
                    WSumNum = Convert.ToInt32(Math.Pow(2, WTempTimer));
                }

          
                WStopTimer = ModLength - Convert.ToInt32(Math.Pow(2, WTempTimer));

                if (WStopTimer < 0)
                {
                    WTempTimer--;
                    WStopTimer = ModLength - Convert.ToInt32(Math.Pow(2, WTempTimer));

                    if (Num < WStopTimer)
                    {
                        i = 0;
                        while (i != WTempTimer)
                        {
                            ReaderBits.WriteBit(Convert.ToBoolean(Num & (1 << i)));
                            i++;
                        }

                        ReaderBits.WriteBit(false);
                    }
                    else
                    {
                        WSumNum = Convert.ToInt32(Math.Pow(2, WTempTimer));
                        if (Num < WSumNum)
                        {
                            i = 0;
                            while (i != WTempTimer)
                            {
                                ReaderBits.WriteBit(Convert.ToBoolean(Num & (1 << i)));
                                i++;
                            }
                        }
                        else
                        {
                            WTempOutNum = Num - WSumNum;
                            i = 0;
                            while (i != WTempTimer)
                            {
                                ReaderBits.WriteBit(Convert.ToBoolean(WTempOutNum & (1 << i)));
                                i++;
                            }

                            ReaderBits.WriteBit(true);
                        }
                    }

                }
                else
                {
                    if (WStopTimer == 0)
                    {
                        i = 0;
                        while (i != WTempTimer)
                        {
                            ReaderBits.WriteBit(Convert.ToBoolean(Num & (1 << i)));
                            i++;
                        }
                    }
                    else
                    {
                        //isErorr
                        i++;

                    }

                }




            }
        }

        public void WriteNumber(int ModLength, int Number)
        {
            SaveNum(ModLength, Number);
        }
     



        #endregion


        #region For Other Usage

        public int GetStopNumLength
        {
            get
            {
                return ReaderBits.GetStopNumLength;
            }
        }
        public int GetReaderDataLength
        {
            get
            {
                return ReaderBits.GetReaderDataLength;
            }
        }
        public bool GetIsCancel
        {
            get
            {
                return ReaderBits.GetIsCancel;
            }
        }

        public string GetSavePath
        {
            get
            {
                return ReaderBits.GetSavePath;
            }
        }


        #endregion
    }




}
