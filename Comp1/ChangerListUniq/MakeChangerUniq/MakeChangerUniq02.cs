using Comp1.MakeTreeUniq;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.ChangerListUniq.MakeChangerUniq
{
    class MakeChangerUniq02Nod
    {
        public int Value = 0;
        public int Locate = 0;

        public int TempValue = 0;

        public MakeChangerUniq02Nod()
        {

        }
        public MakeChangerUniq02Nod(int NumValue)
        {
            Value = NumValue;
        }


        public void EditLocate(int LocateNum)
        {
            Locate = LocateNum;
        }
        public void EditTempValue(int TempValues)
        {
            TempValue = TempValues;
        }

        public void Initial()
        {
            Locate = 0;
            TempValue = Value;
        }
    }

    class MakeChangerUniq02Tree
    {
        #region Proprties

        //Temp

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        private List<MakeChangerUniq02Nod> ListNodes;
        private List<int> LeftToRightCount;
        private List<int> RightToLeftCount;


        #endregion

        #region OverLoad

        public MakeChangerUniq02Tree()
        {
            
            Creat();
        }
        public MakeChangerUniq02Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            Creat();
        }
       
        #endregion

        #region Create

        private void Creat()
        {
            #region CreatListNumberNods

            ListNodes = new List<MakeChangerUniq02Nod>();
            LeftToRightCount = new List<int>();
            RightToLeftCount = new List<int>();

            for (int i = 0; i != ModLength; i++)
            {
                ListNodes.Add(new MakeChangerUniq02Nod(i));

                LeftToRightCount.Add(0);
                RightToLeftCount.Add(0);
            }

            #endregion

            #region Cal Mod

            //Temp
            ModStopLength = ModLength;

            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;

            #endregion
        }


        #endregion

        #region Initial

        private int InitialC = 0;
        private void initialAll()
        {
            InitialC=0;
            foreach (MakeChangerUniq02Nod nod in ListNodes)
            {
                nod.Initial();
                LeftToRightCount[InitialC] = 0;
                RightToLeftCount[InitialC] = 0;
                InitialC++;
            }

        }

        #endregion

        #region  Reader

        //ForDetect
        private int TempModNum = 0;
        private int TempLocateCount = 0;
        //For Count Left & Rigth
        private int StopLength = 0;
        private int CountNum = 0;
 
        private int LeftCount = 0;
        private int RightCount = 0;

        private int TempLeftLocate = -1;
        private int TempRightLocate = 0;

        private int LeftPo = 0;
        private int RightPo = 0;

        //for Compare List & Write Bits Key
        private List<int> TimesWrite = new List<int>();
        private int i = 0;
        private int PoList = 0;

        private int n = 0;
        public int MakeListUnuniq(ref List<int> UniqList, ref ReaderWriteFileBits02B WriterBitsKey)
        {
            TempModNum = 0;

            if (UniqList.Count != ModLength)
                return 0;

            #region Detect Locate
            {
                 TempLocateCount = 0;
                foreach (int num in UniqList)
                {
                    ListNodes[num].EditLocate(TempLocateCount);
                    TempLocateCount++;
                }//End foreach
            }

            #endregion

            #region Count Left & Rigth
            //Left To Rigth
            {
                 StopLength = ModLength - 1;

                 CountNum = 0;

                 LeftCount = 0;
                 RightCount = 0;

                 TempLeftLocate = -1;
                 TempRightLocate = ModLength;

                 LeftPo = 0;
                 RightPo = 0;

                foreach (MakeChangerUniq02Nod nod in ListNodes)
                {
                    //LtR
                    {
                        if (nod.Locate < TempLeftLocate)
                        {
                            for (; LeftCount != 0; LeftCount--)
                            {
                                LeftToRightCount[LeftPo] = LeftCount;
                                LeftPo++;
                            }
                            TempLeftLocate = -1;
                        }
                        //   else
                        {
                            LeftCount++;
                            TempLeftLocate = nod.Locate;
                        }
                    }




                    //RtL
                    {
                        if (nod.Locate > TempRightLocate)
                        {
                            for (; RightCount != 0; RightCount--)
                            {
                                RightToLeftCount[RightPo] = RightCount;
                                RightPo++;
                            }
                            TempRightLocate = ModLength;
                        }
                        //   else
                        {
                            RightCount++;
                            TempRightLocate = nod.Locate;
                        }
                    }

                    CountNum++;
                }

                {

                    for (; LeftCount != 0; LeftCount--)
                    {
                        LeftToRightCount[LeftPo] = LeftCount;
                        LeftPo++;
                    }
                    TempLeftLocate = -1;


                    for (; RightCount != 0; RightCount--)
                    {
                        RightToLeftCount[RightPo] = RightCount;
                        RightPo++;
                    }
                    TempRightLocate = ModLength;



                }

            }



            #endregion

            #region Compare List & Write Bits Key
            {
                TimesWrite.Clear();
                 i = 0;
                 PoList = 0;

                while (i < ModLength)
                {
                    if (LeftToRightCount[i] >= RightToLeftCount[i])
                    {
                        WriterBitsKey.WriteBit(false);
                        TimesWrite.Add(LeftToRightCount[i]);


                        for ( n = 0; n != LeftToRightCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = TempModNum;
                            PoList++;
                        }
                        i = i + LeftToRightCount[i];
                        TempModNum++;
                    }
                    else
                    {
                        WriterBitsKey.WriteBit(true);
                        TimesWrite.Add(RightToLeftCount[i]);


                        for ( n = 0; n != RightToLeftCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = TempModNum; PoList++;
                        }
                        i = i + RightToLeftCount[i];
                        TempModNum++;
                    }
                }
            }
            # endregion

            #region Writer Data
            {
                foreach (MakeChangerUniq02Nod nod in ListNodes)
                {
                    UniqList[nod.Locate] = nod.TempValue;
                }
            }
            #endregion

         //   ListAddModNuminfo.Add(ModNum);


            initialAll();
            SegmentNum++;

            return TempModNum;
        }

        #endregion



        #region For info

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        public void GetSegmentInfo(int SumOfReadBits)
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(SumOfReadBits);

        }

        #endregion

    }

    class MakeChangerUniq02
    {
        public StringBuilder Report;
        private int ModLength = 256;
        private int LengthModListReader = 20;

        private string Extension = "MCfU02ML";
        private string DeExtension = "DeMCfU02ML";


        public MakeChangerUniq02()
        {

        }
        public MakeChangerUniq02(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }

        public void StartMakeChangerForUniq_UniqFileW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B WriterBit = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBit.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNumMod02 WriterNumMod = new ReaderWriterOneNumMod02(false, 256, ref WriterBit);

            ReaderWriteFileBits02B WriterBitKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01Key");
            if (WriterBitKey.GetIsCancel)
                return;// "isCancel";


            MakeChangerUniq02Tree Tree = new MakeChangerUniq02Tree(ModLength);

            
            List<int> datauniq = new List<int>();
            int ModNum = 0;
            while (ReaderNum.isAbleRead)
            {
                datauniq = ReaderNum.GetManyNum(ModLength);
                if (datauniq.Count == ModLength)
                {
                    ModNum = Tree.MakeListUnuniq(ref datauniq, ref WriterBitKey);
                }
                else
                    ModNum = ModLength;

                foreach (int n in datauniq)
                {
                    WriterNumMod.WriteNumber(ModNum ,n);
                }


                WriterBit.SumOfWriteBits = 0;
                WriterBitKey.SumOfWriteBits = 0;

                datauniq.Clear();
            }


            ReaderNum.End();
            WriterNumMod.End();
            WriterBitKey.CloseFile();
            
        }
        public void StartMakeDeChangerForUniq_UniqFileW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B ReaderBitKey = new ReaderWriteFileBits02B(true);
            if (ReaderBitKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq06Tree Tree = new MakeTreeDeUniq06Tree(ModLength, ref ReaderBitKey);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneList(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            ReaderBitKey.CloseFile();
        }



    }






    
}
