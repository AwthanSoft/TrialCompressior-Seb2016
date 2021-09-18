using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.ByMore.ChangerByMore
{
    class ChangerByMoreWriterNode01
    {
        private int ModDataLength = 256;
        private int ModMoreLength = 256;
        public ReaderWriterOneNum02B NumDataWriter1;
        public ReaderWriterOneNum02B NumDataWriter2;
        public ReaderWriterOneNum02B NumMoreWriter;


        //public ChangerByMoreWriterNode01()
        //{

        //}
        //public ChangerByMoreWriterNode01(int LengthDataMod)
        //{
        //    ModDataLength = LengthDataMod;
        //    ModMoreLength = LengthDataMod;

        //  //  NumDataWriter = new ReaderWriteFileNum02();
        //}
        //public ChangerByMoreWriterNode01(int LengthDataMod, int LengthMoreMod)
        //{
        //    ModDataLength = LengthDataMod;
        //    ModMoreLength = LengthMoreMod;
        //}
        public ChangerByMoreWriterNode01(ref ReaderWriterOneNum02B WriterData)
        {
            NumDataWriter1 = WriterData;
            NumDataWriter2 = WriterData;
            NumMoreWriter = WriterData;
        }
        public ChangerByMoreWriterNode01(ref ReaderWriterOneNum02B WriterData, ref ReaderWriterOneNum02B WriterMore)
        {
            NumDataWriter1 = WriterData;
            NumDataWriter2 = WriterData;
            NumMoreWriter = WriterMore;
        }
        public ChangerByMoreWriterNode01(ref ReaderWriterOneNum02B WriterData1, ref ReaderWriterOneNum02B WriterData2, ref ReaderWriterOneNum02B WriterMore)
        {
            NumDataWriter1 = WriterData1;
            NumDataWriter2 = WriterData2;
            NumMoreWriter = WriterMore;
        }



        //public void DataWrite(int num)
        //{
        //    NumDataWriter.WriteNum(num);
        //}
        public void DataWrite(int num, int SecoundNum)
        {
            NumDataWriter1.WriteNumber(num); NumDataWriter2.WriteNumber(SecoundNum);
        }
        //public void MoreWrite(int num)
        //{

        //}
        public void MoreWrite(int KeyMore, int LocateNum)
        {
            NumDataWriter1.WriteNumber(KeyMore);
            NumMoreWriter.WriteNumber(LocateNum);
        }


    }
    class ChangerByMoreKeyNumNod01
    {
        public int KeyMoreNum = 0;

        public ChangerByMoreKeyNumNod01(int KeyMoreNumber)
        {
            KeyMoreNum = KeyMoreNumber;
        }
    }


    class ChangerByMoreNode01
    {
        #region Proprties For Nodes

        public List<ChangerByMoreNode01> NextList;
        public int IdNode = 0;

        public int FirstNumbers = 0;
        public int SecondNumbers = 0;

        #endregion

        #region

        public List<ChangerByMoreNode01> MoreList;
        public bool isInMoreList = false;
        public int LocateInMoreList = 0;
        private ChangerByMoreNode01 TempPo;
        public ChangerByMoreKeyNumNod01 KeyMore;

        #endregion

        #region For Counters
        public int Counter = 0;
        public int FirstLocateCounter = 0;
        public int SecoundLocateCounter = 0;
        public int TotalCounter = 0;

        public void PlusCount()
        {
            Counter++;
        }

        #endregion



        #region Overload

        public ChangerByMoreNode01()
        {

        }
        public ChangerByMoreNode01(int FirstNum, int SecoundNum, ref List<ChangerByMoreNode01> isMoreList)
        {
            FirstNumbers = FirstNum;
            SecondNumbers = SecoundNum;
            MoreList = isMoreList;

            RefrishWritNum(FirstNum, SecoundNum);
        }
        public ChangerByMoreNode01(int FirstNum, int SecoundNum, ref List<ChangerByMoreNode01> isMoreList, int NodesNumber)
        {
            FirstNumbers = FirstNum;
            SecondNumbers = SecoundNum;
            MoreList = isMoreList;
            IdNode = NodesNumber;

            RefrishWritNum(FirstNum, SecoundNum);
        }
        public ChangerByMoreNode01(int NodesNumber)
        {
            IdNode = NodesNumber;
        }

        public void RefrishWritNum(int FrNum, int SecNum)
        {
            WrFirstNum = FrNum;
            WrSecondNum = SecNum;
        }
        #endregion


        #region Writer

        public int WrFirstNum = 0;
        public int WrSecondNum = 0;
        public ChangerByMoreWriterNode01 WriterData;

        public void Write()
        {
            WriteData();
            PlusCount();
            CheckMainList();

        }
        private void WriteData()
        {
            if (isInMoreList)
            {
                WriterData.MoreWrite(KeyMore.KeyMoreNum, LocateInMoreList);
            }
            else
            {
                WriterData.DataWrite(WrFirstNum, WrSecondNum);
            }
        }
        private void RefrishMoreList()
        {
            while (LocateInMoreList != 0)
            {
                if (Counter <= MoreList[LocateInMoreList - 1].Counter)
                {
                    break;
                }
                else
                {
                    TempPo = MoreList[LocateInMoreList - 1];

                    MoreList[LocateInMoreList - 1] = this;
                    MoreList[LocateInMoreList] = TempPo;

                    LocateInMoreList--;
                    TempPo.LocateInMoreList++;

                }

            }

        }
        private void CheckMainList()
        {
            if (isInMoreList)
            {
                RefrishMoreList();
            }
            else
            {
                if (Counter > MoreList[MoreList.Count - 1].Counter)
                {
                    MoreList[MoreList.Count - 1].isInMoreList = false;
                    MoreList[MoreList.Count - 1] = this;
                    isInMoreList = true;
                    LocateInMoreList = MoreList.Count - 1;
                    RefrishMoreList();
                }
                else
                {

                }
            }

        }

        #endregion
        #region  Refrish Writers

        public void RefrishWriter(ChangerByMoreKeyNumNod01 KeyMorNum, ChangerByMoreWriterNode01 WriterDataNod)
        {
            KeyMore = KeyMorNum;
            WriterData = WriterDataNod;
        }
        public void RefrishWriter(ref ChangerByMoreKeyNumNod01 KeyMorNum, ref ChangerByMoreWriterNode01 WriterDataNod)
        {
            KeyMore = KeyMorNum;
            WriterData = WriterDataNod;
        }

        #endregion




    }


    class ChangerByMoreTree01
    {
        private int ModLength = 256;
        private int ModisMoreLength = 256;
        public List<ChangerByMoreNode01> NumberList;
        public List<ChangerByMoreNode01> MoreList;

        public ChangerByMoreTree01()
        {
            Create();
        }
        public ChangerByMoreTree01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
            ModisMoreLength = ModLength;
            Create();
        }
        public ChangerByMoreTree01(int ModLengthNumber, int isMoreLength)
        {
            ModLength = ModLengthNumber;
            ModisMoreLength = isMoreLength;
            Create();
        }

        private void Create()
        {
            MoreList = new List<ChangerByMoreNode01>();

            //01 create && fill NumberList
            int idCounter = 0;
            NumberList = new List<ChangerByMoreNode01>();
            for (int i = 0; i != ModLength; i++)
            {
                NumberList.Add(new ChangerByMoreNode01(i));
                List<ChangerByMoreNode01> TempList = new List<ChangerByMoreNode01>();
                for (int n = 0; n != ModLength; n++)
                {
                    TempList.Add(new ChangerByMoreNode01(i, n, ref MoreList, idCounter));
                    idCounter++;
                }
                NumberList[i].NextList = TempList;
            }

            //02 Create MoreList
            {
                idCounter = 0;
                bool isFinish = false;

                for (int i = 0; i != ModLength; i++)
                {
                    for (int n = 0; n != ModLength; n++)
                    {
                        NumberList[i].NextList[n].isInMoreList = true;
                        NumberList[i].NextList[n].LocateInMoreList = idCounter;

                        MoreList.Add(NumberList[i].NextList[n]);
                        idCounter++;


                        if (idCounter == ModisMoreLength)
                        {
                            isFinish = true;
                            break;
                        }

                    }

                    if (isFinish)
                        break;
                }

            }

        }


        public void RefrishWriter(ChangerByMoreKeyNumNod01 KeyMorNum, ChangerByMoreWriterNode01 WriterDataNod)
        {
            foreach (ChangerByMoreNode01 MNod in NumberList)
            {
                foreach (ChangerByMoreNode01 nod in MNod.NextList)
                {
                    nod.RefrishWriter(ref KeyMorNum, ref WriterDataNod);
                }
            }

        }




    }




    class ChangerByMore01
    {
        public StringBuilder Report;
        private int ModLength = 256;
        private int ModisMoreLength = 256;

        private string Extension = "CBM01ML";
        private string DeExtension = "DeCBM01ML";

        private int FirstChangerNum = 0;
        private int SecoundChangerNum = 1;


        public ChangerByMore01()
        {

        }
        public ChangerByMore01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
            ModisMoreLength = ModLengthNumber;
        }
        public ChangerByMore01(int ModLengthNumber , int MoreLengthMod)
        {
            ModLength = ModLengthNumber;
            ModisMoreLength = MoreLengthMod;
        }

        public void ChangerByMoreWay01()
        {

            ChangerByMoreTree01 Tree = new ChangerByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

        

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            ChangerByMoreWriterNode01 WriterNod = new ChangerByMoreWriterNode01(ref WriterNum);
            ChangerByMoreKeyNumNod01 KeyMore = new ChangerByMoreKeyNumNod01(0);
            Tree.RefrishWriter(KeyMore, WriterNod);




            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension +"BitKey"+ "W01");


            int DataLengthStop = ReaderNum.GetStopNumLength;



            int First = 0;
            // int Secound = 0;
            bool isFirst = true;
            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    if (isFirst)
                    {
                        if (n == FirstChangerNum || n == SecoundChangerNum)
                        {
                            if (n == FirstChangerNum)
                            {
                                WriterBits.WriteBit(false);
                            }
                            else
                            {
                                WriterBits.WriteBit(true);
                            }

                            First = SecoundChangerNum;
                        }
                        else
                        {
                            First = n;
                        }

                        isFirst = false;
                    }
                    else
                    {
                        Tree.NumberList[First].NextList[n].Write();
                        isFirst = true;
                    }
                }


            }

            ReaderNum.End();
            WriterNum.End();
            WriterBits.CloseFile();
          
        }
        public void ChangerByMoreWay02()
        {

            ChangerByMoreTree01 Tree = new ChangerByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";


            ReaderWriterOneNum02B WriterNum1 = new ReaderWriterOneNum02B(false, ModLength,"Wr1"+ Extension + ModLength.ToString() + "W02");
            ReaderWriterOneNum02B WriterNum2 = new ReaderWriterOneNum02B(false, ModLength,"Wr2"+ Extension + ModLength.ToString() + "W02");
            ReaderWriterOneNum02B WriterMore = new ReaderWriterOneNum02B(false, ModLength,"Wmo"+ Extension + ModLength.ToString() + "W02");


            ChangerByMoreWriterNode01 WriterNod = new ChangerByMoreWriterNode01(ref WriterNum1, ref WriterNum2,ref WriterMore);
            ChangerByMoreKeyNumNod01 KeyMore = new ChangerByMoreKeyNumNod01(0);
            Tree.RefrishWriter(KeyMore, WriterNod);


            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, "BK" + Extension + "W02");


            int DataLengthStop = ReaderNum.GetStopNumLength;



            int First = 0;
            // int Secound = 0;
            bool isFirst = true;
            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    if (isFirst)
                    {
                        if (n == FirstChangerNum || n == SecoundChangerNum)
                        {
                            if (n == FirstChangerNum)
                            {
                                WriterBits.WriteBit(false);
                            }
                            else
                            {
                                WriterBits.WriteBit(true);
                            }

                            First = SecoundChangerNum;
                        }
                        else
                        {
                            First = n;
                        }

                        isFirst = false;
                    }
                    else
                    {
                        Tree.NumberList[First].NextList[n].Write();
                        isFirst = true;
                    }
                }


            }

            ReaderNum.End();
            WriterNum1.End();
            WriterNum2.End();
            WriterMore.End();
            WriterBits.CloseFile();

        }
 




       
    }







}
