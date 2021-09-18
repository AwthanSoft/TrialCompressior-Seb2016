using Comp1.MakeTreeUniq;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.ChangerListUniq.MakeChangerUniq
{

    class MakeChangerUniq01Nod
    {
        public int Value=0;
        public int Locate=0;

        public int TempValue=0;

        public MakeChangerUniq01Nod()
        {

        }
        public MakeChangerUniq01Nod(int NumValue)
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

    }



    class MakeChangerUniq01Oper
    {
        #region Proprties

        public StringBuilder RePort;
        //Temp

        private int ModLength = 256;

        private List<MakeChangerUniq01Nod> ListNodes;
        private List<int> LeftToRightCount;
        private List<int> RightToLeftCount;


        #endregion

        #region OverLoad

        public MakeChangerUniq01Oper()
        {
            RePort = new StringBuilder();

            MakeTree();

        }
        public MakeChangerUniq01Oper(int LengthMod)
        {
            RePort = new StringBuilder();

            ModLength = LengthMod;
            MakeTree();

        }

        #endregion

        #region Info

        public List<int> ListAddModNuminfo = new List<int>();

        #endregion

        #region Create

        private void MakeTree()
        {
            ListNodes = new List<MakeChangerUniq01Nod>();
            LeftToRightCount = new List<int>();
            RightToLeftCount = new List<int>();

            for (int i = 0; i != ModLength; i++)
            {
                ListNodes.Add(new MakeChangerUniq01Nod(i));

                LeftToRightCount.Add(0);
                RightToLeftCount.Add(0);
            }

            

        }

        #endregion

        #region  Reader

        public int MakeListUnuniq(ref List<int> UniqList, ref List<bool> KeyBit)
        {
            int ModNum = 0;
            if (UniqList.Count != ModLength)
                return 0;

            #region Detect Locate
            {
                int LocateCount = 0;
                foreach (int num in UniqList)
                {
                    ListNodes[num].EditLocate(LocateCount);
                    LocateCount++;
                }//End foreach
            }

            #endregion

            #region Count Left & Rigth
            //Left To Rigth
            {
                int StopLength = ModLength - 1;

                int CountNum = 0;

                int LeftCount = 0;
                int RightCount = 0;

                int TempLeftLocate = -1;
                int TempRightLocate = ModLength;

                int LeftPo = 0;
                int RightPo = 0;

                foreach (MakeChangerUniq01Nod nod in ListNodes)
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

            }



            #endregion

            #region Compare List & Write Bits Key
            {
                List<int> TimesWrite = new List<int>();
                int i = 0;
                int PoList = 0;


                while (i < ModLength)
                {
                    if (LeftToRightCount[i] >= RightToLeftCount[i])
                    {
                        KeyBit.Add(false);
                        TimesWrite.Add(LeftToRightCount[i]);
                        i = i + LeftToRightCount[i];

                        for (int n = 0; n != LeftToRightCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = ModNum;
                            PoList++;
                        }
                        ModNum++;
                    }
                    else
                    {
                        KeyBit.Add(true);
                        TimesWrite.Add(RightToLeftCount[i]);
                        i = i + RightToLeftCount[i];

                        for (int n = 0; n != RightToLeftCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = ModNum; PoList++;
                        }
                        ModNum++;
                    }
                }
            }
            # endregion

            #region Writer Data
            {
                foreach (MakeChangerUniq01Nod nod in ListNodes)
                {
                    UniqList[nod.Locate] = nod.TempValue;
                }
            }
            #endregion

            ListAddModNuminfo.Add(ModNum);
            return ModNum;
        }
        public int MakeListUnuniq(ref List<int> UniqList, ref ReaderWriteFileBits02B WriterBitsKey)
        {
            int ModNum = 0;

            if (UniqList.Count != ModLength)
                return 0;

            #region Detect Locate
            {
                int LocateCount = 0;
                foreach (int num in UniqList)
                {
                    ListNodes[num].EditLocate(LocateCount);
                    LocateCount++;
                }//End foreach
            }

            #endregion

            #region Count Left & Rigth
            //Left To Rigth
            {
                int StopLength = ModLength - 1;

                int CountNum = 0;

                int LeftCount = 0;
                int RightCount = 0;

                int TempLeftLocate = -1;
                int TempRightLocate = ModLength;

                int LeftPo = 0;
                int RightPo = 0;

                foreach (MakeChangerUniq01Nod nod in ListNodes)
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
                List<int> TimesWrite = new List<int>();
                int i = 0;
                int PoList = 0;

                

                while (i < ModLength)
                {
                    if (LeftToRightCount[i] >= RightToLeftCount[i])
                    {
                        WriterBitsKey.WriteBit(false);
                        TimesWrite.Add(LeftToRightCount[i]);
                        

                        for (int n = 0; n != LeftToRightCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = ModNum;
                            PoList++;
                        }
                        i = i + LeftToRightCount[i];
                        ModNum++;
                    }
                    else
                    {
                        WriterBitsKey.WriteBit(true);
                        TimesWrite.Add(RightToLeftCount[i]);
                        

                        for (int n = 0; n != RightToLeftCount[i]; n++)
                        {
                            ListNodes[PoList].TempValue = ModNum; PoList++;
                        }
                        i = i + RightToLeftCount[i];
                        ModNum++;
                    }
                }
            }
            # endregion

            #region Writer Data
            {
                foreach (MakeChangerUniq01Nod nod in ListNodes)
                {
                    UniqList[nod.Locate] = nod.TempValue;
                }
            }
            #endregion

            ListAddModNuminfo.Add(ModNum);
            return ModNum;
        }
      
        #endregion

        #region  ForInfo

        public void Info_01()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("\n*****info 01 ****\n"+
            //    "\nTempReadBits = "+(TempReadBits


        }



        #endregion


    }

    class MakeChangerUniq01
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MCU01ML";
        private string DeExtension = "DeMCU01ML";


        public MakeChangerUniq01()
        {

        }
        public MakeChangerUniq01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }


    

        public void StartMakeChangerUniq_ForTreeUniqW01()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);

            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterKey = new ReaderWriteFileBits02B(false, Extension + "Key");
            if (WriterKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

           
            //string KeyPath=Path.ChangeExtension(WriterNum.GetSavePath,Extension+"Key");

            //ReaderWriteFileBits02 WriterKey = new ReaderWriteFileBits02(KeyPath, false);
            //WriterKey.OpenFile();

            MakeTreeUniq01Oper TreeUniq = new MakeTreeUniq01Oper(ModLength);

            MakeChangerUniq01Oper ChangerOper = new MakeChangerUniq01Oper(ModLength);

            while (ReaderBits.isAbleRead)
            {
                List<int> UniqList = TreeUniq.MakeUniqOnly_ByStop(ref ReaderBits, ModLength);
                ChangerOper.MakeListUnuniq(ref UniqList, ref WriterKey);

                WriterNum.WriterManyNumber(ref UniqList);

            }


            WriterKey.CloseFile();
            WriterNum.End();
            ReaderBits.CloseFile();

        }

        public void StartMakeChangerUniq_ForListUniq01W02(int ModReaderNum)
        {


            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModReaderNum);

            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterKey = new ReaderWriteFileBits02B(false, Extension + "Key");
            if (WriterKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W02");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

          
            //string KeyPath=Path.ChangeExtension(WriterNum.GetSavePath,Extension+"Key");

            //ReaderWriteFileBits02 WriterKey = new ReaderWriteFileBits02(KeyPath, false);
            //WriterKey.OpenFile();


            MakeChangerUniq01Oper ChangerOper = new MakeChangerUniq01Oper(ModLength);

            while (ReaderNum.isAbleRead)
            {
                List<int> UniqList = MakeListUniq_01(ref ReaderNum, ModReaderNum, ModLength - ModReaderNum);
                ChangerOper.MakeListUnuniq(ref UniqList, ref WriterKey);

                WriterNum.WriterManyNumber(ref UniqList);

            }


            WriterKey.CloseFile();
            WriterNum.End();
            ReaderNum.End();

        }

        public void StartMakeChangerUniq_ForTreeUniqW03(int ModRead)
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);

            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterKey = new ReaderWriteFileBits02B(false, Extension + "Key");
            if (WriterKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W03");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq01Oper TreeUniq = new MakeTreeUniq01Oper(ModLength);

           
            while (ReaderBits.isAbleRead)
            {
                List<int> UniqList = TreeUniq.MakeUniqOnly_ByStop(ref ReaderBits, ModLength);

                if (UniqList.Count != ModLength)
                {
                    WriterNum.WriterManyNumber(ref UniqList);
                }
                else
                {
                    List<int> SaveList = MakeListUniq_02(ref WriterKey, ref UniqList, ModRead);
                    WriterNum.WriterManyNumber(ref SaveList);
                }
               

            }


            WriterKey.CloseFile();
            WriterNum.End();
            ReaderBits.CloseFile();

        }


        public List<int> MakeListUniq_01(ref ReaderWriterOneNum02B ReaderNum, int ModNum, int TimerStop)
        {

            int NumStop = 0;
            int Counter = 0;
            List<int> ListNum = new List<int>();

            for (int i = 0; i != ModNum; i++)
            {
                ListNum.Add(i);
                Counter++;
            }

            List<int> listSave = new List<int>();
            int TempNum = 0;

            while (ReaderNum.isAbleRead)
            {
                if (NumStop == TimerStop)
                {
                    foreach (int num in ListNum)
                    {
                        listSave.Add(num);
                    }
                    return listSave;
                }
                else
                {
                    TempNum = ReaderNum.GetOneNumber();
                }

                listSave.Add(ListNum[TempNum]);
                ListNum.Add(Counter);
                Counter++;
                NumStop++;

                ListNum.RemoveAt(TempNum);

            }


            return listSave;

        }

        public List<int> MakeListUniq_02(ref ReaderWriteFileBits02B WriterBits, ref List<int> ListNum, int ModRead)
        {
            

            int Counter = ModRead;

            int BitCount = 0;
            int LoopCounter = 0;
            List<int >ReaderList=ListNum;
             List<int >ReReadList= new List<int>();


            List<int> SaveList = new List<int>();
            int LengthList=0;
            while (ReaderList.Count > 0)
            {
                LoopCounter++;
                LengthList = ReaderList.Count;
                for(int i=0;i!=LengthList;i++)
                {
                    if (ReaderList[i] < Counter)
                    {
                        SaveList.Add(ReaderList[i]);
                        WriterBits.WriteBit(true);
                        Counter++;

                    }
                    else
                    {
                        ReReadList.Add(ReaderList[i]);
                        WriterBits.WriteBit(false);
                    }
                    BitCount++;

                }
                ReaderList = ReReadList;
                ReReadList = new List<int>();

                if (ReaderList.Count == 0)
                    break;
            }


            return SaveList;
        }

        public List<int> MakeListUniq_03(ref ReaderWriteFileBits02B WriterBits, ref List<int> ListNum, int ModRead)
        {


            int Counter = ModRead;

            int BitCount = 0;
            int LoopCounter = 0;
            List<int> ReaderList = ListNum;
            List<int> ReReadList = new List<int>();


            List<int> SaveList = new List<int>();
            int LengthList = 0;
            while (ReaderList.Count > 0)
            {
                LoopCounter++;
                LengthList = ReaderList.Count;
                for (int i = 0; i != LengthList; i++)
                {
                    if (ReaderList[i] < Counter)
                    {
                        SaveList.Add(ReaderList[i]);
                        WriterBits.WriteBit(true);
                        Counter++;

                    }
                    else
                    {
                        ReReadList.Add(ReaderList[i]);
                        WriterBits.WriteBit(false);
                    }
                    BitCount++;

                }
                ReaderList = ReReadList;
                ReReadList = new List<int>();

                if (ReaderList.Count == 0)
                    break;
            }


            return SaveList;
        }

    }
   

}
