using Comp1.ChangerListUniq.MakeChangerUniq;
using Comp1.MergeSort.MakeTreeMergeSort;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeTreeUniq
{

    class MakeTreeUniq06Node
    {
        public MakeTreeUniq06ListNode MainList;

        public int MainListId;

        public int RealLocationInMainList;
        public int RealValue;


        //Temp
        public MakeTreeUniq06ListNode TempMainList;
        public int TempMainListId = 0;
        public int TempRealLocationInMainList = 0;
        public int TempRealValue = 0;



        public bool isOpen;


        public MakeTreeUniq06Node()
        {
            RealValue = 0;
            RealLocationInMainList = 0;
            MainListId = 0;

            isOpen = false;

           
        }
        public MakeTreeUniq06Node( int NodeId)
        {
            RealValue = NodeId;

            RealLocationInMainList = 0;
            MainListId = 0;

            isOpen = false;


        }

        public void Initial()
        {
            TempMainList = MainList;
            TempMainListId = MainListId;
            TempRealLocationInMainList = RealLocationInMainList;
            TempRealValue = RealValue;

        }

        public int GetTempListLocateInTempList()
        {
            return TempMainList.ListLocateInTempList;
        }






        public void EditLocation(int Locate)
        {

            RealLocationInMainList = Locate;
        }



    }

    class MakeTreeUniq06ListNode
    {
        public List<MakeTreeUniq06Node> ListNumber;
        public MakeTreeUniq06Node MainFirstNod;

        public int ListId;
        public int ListLocateInTempList;

        public int NumberNodesInList = 0;

        public bool isClosed;

        public MakeTreeUniq06ListNode()
        {
            ListLocateInTempList = 0;
            ListId = 0;

            isClosed = false;
            ListNumber=new List<MakeTreeUniq06Node>(); 
        }
        public MakeTreeUniq06ListNode(int IdList)
        {
            ListLocateInTempList = 0;
            ListId = IdList;

            isClosed = false;
            ListNumber = new List<MakeTreeUniq06Node>();
        }

        public void MainInitial(ref MakeTreeUniq06Node FirstNod)
        {
            ListLocateInTempList = ListId;

            MainFirstNod = FirstNod;
            FirstNod.MainList = this;

            MainFirstNod.MainListId = ListId;
            MainFirstNod.RealLocationInMainList = 0;

            ListNumber.Add(MainFirstNod);
            NumberNodesInList++;
        }

        public void Initial()
        {
            ListLocateInTempList = ListId;
            isClosed = false;

            ListNumber.Clear();

            NumberNodesInList = 0;
            ListNumber.Add(MainFirstNod);
            NumberNodesInList++;

            
        }

        public void AddNode(ref MakeTreeUniq06Node Node)
        {
            Node.TempMainList = this;
            Node.TempMainListId = ListId;

            Node.TempRealLocationInMainList = NumberNodesInList;
            ListNumber.Add(Node);
            NumberNodesInList++;
        }
        public void AddNode( MakeTreeUniq06Node Node)
        {
            Node.TempMainList = this;
            Node.TempMainListId = ListId;

            Node.TempRealLocationInMainList = NumberNodesInList;
            ListNumber.Add(Node);
            NumberNodesInList++;
        }

        public int GetTempListLocateInTempList()
        {
            return ListLocateInTempList;
        }

        public void CloseList()
        {
            isClosed = true;
        }


    }


    class MakeTreeUniq06Tree
    {

        #region  Properties

        private MakeTreeUniq06Node[] ListNumber;
        private List<MakeTreeUniq06ListNode> MainLists;
        
        private MakeTreeUniq06Node PoNod;
        private MakeTreeUniq06ListNode PoList;

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        public ReaderWriteFileBits02B WriterAdditonBits;
        public ReaderWriterOneNumMod02 WriterAdditonNumByMod;

        public ReaderWriteFileBits02B ReaderBits;

        private int LengthModListReader = 20;

        #endregion

        #region OverLoad

        public MakeTreeUniq06Tree()
        {
            WriterAdditonBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeUniq06Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterAdditonBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeUniq06Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            WriterAdditonBits = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            #region CreatListNumberNods

            ListNumber = new MakeTreeUniq06Node[ModLength];

            for (int i = 0; i != ModLength; i++)
            {
                ListNumber[i] = new MakeTreeUniq06Node(i);
            }

            #endregion

            #region Create MainList

            MainLists = new List<MakeTreeUniq06ListNode>();

            for (int i = 0; i != ModLength; i++)
            {
                MainLists.Add(new MakeTreeUniq06ListNode(i));
            }

            #endregion

            #region  MainInitial

            for (int i = 0; i != ModLength; i++)
            {
                MainLists[i].MainInitial(ref ListNumber[i]);
            }

            #endregion

            WriterAdditonNumByMod = new ReaderWriterOneNumMod02(false, LengthModListReader, ref WriterAdditonBits);

          //  initialAll();




            //Temp
            ModStopLength = ModLength;

            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
        }

        public void GetReaderBits(ref ReaderWriteFileBits02B ReaderBit)
        {
            ReaderBits = ReaderBit;
        }
        #endregion

        #region Initial

        private void initialAll()
        {
            TempMainLists.Clear();
            foreach (MakeTreeUniq06ListNode list in MainLists)
            {
                list.Initial();
                TempMainLists.Add(list);
            }

            foreach (MakeTreeUniq06Node nod in ListNumber)
            {
                nod.Initial();
            }

            LastAddList = 0;
        }

        #endregion

        #region For info

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        public void GetSegmentInfo()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBits.SumOfReadBits);

        }

        #endregion

        #region ReaderOneNumber

        private List<MakeTreeUniq06ListNode> TempMainLists = new List<MakeTreeUniq06ListNode>();
        private int TempListLocatorInTempList = 0;
        private int TempStartC = 0;
        private int TempEndC = 0;
        private int LastAddList = 0;
        private int TempListLenth = 0;
        private int TempOutNumber = 0;
        private int TempEndC2 = 0;

        private int ReadOnNumber(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                TempListLocatorInTempList = PoList.GetTempListLocateInTempList();
                TempEndC = TempMainLists.Count;
                for (TempStartC = TempListLocatorInTempList; TempStartC != TempEndC; TempStartC++)
                {
                    TempMainLists[TempStartC].ListLocateInTempList--;
                }

                //Checker Error 01
                {
                    if (TempMainLists[TempListLocatorInTempList] != PoList) 
                    {
                        return 0;
                    }

                }

                //Last Add list

                LastAddList = TempMainLists[LastAddList].GetTempListLocateInTempList();
                

                //Remove 
                TempMainLists.RemoveAt(TempListLocatorInTempList);


                //Add
                {
                    TempListLenth = PoList.NumberNodesInList;
                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        WriterAdditonNumByMod.WriteNumber(TempListLenth, PoNod.TempRealLocationInMainList);

                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        
                       // 01
                        if (TempEndC != 0)
                        {
                            if (LastAddList == -1)
                                LastAddList = 0;
                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                if (LastAddList == TempEndC)
                                    LastAddList = 0;

                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                        //02
                        //TempEndC2 = PoList.ListNumber.Count;
                        
                        //for (TempStartC = 0; TempStartC != TempEndC2; TempStartC++)
                        //{
                        //    if (TempListLenth == TempEndC)
                        //        TempListLenth = 0;

                        //    TempMainLists[TempListLenth].AddNode( PoList.ListNumber[TempStartC]);
                        //    TempListLenth++;
                        //}
                        //if (TempListLenth == TempEndC)
                        //    TempListLenth = 0;

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumber(Num);

            }

            return TempOutNumber;
        }

        private int ReadOnNumberW2(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                PoList.CloseList();


                //Checker Error 01
                {
                    //if (TempMainLists[TempListLocatorInTempList] != PoList)
                    //{
                    //    return 0;
                    //}

                }

                //Last Add list
                //{
                //    TempEndC = TempMainLists.Count;
                //    while (TempEndC != 0)
                //    {
                //        if (LastAddList == TempEndC)
                //            LastAddList = 0;

                //        if (TempMainLists[LastAddList].isClosed)
                //        {
                //            TempMainLists.RemoveAt(LastAddList);
                //            TempEndC--;
                //        }
                //        else
                //        {
                //            break;
                //        }

                //    }
                //}

                //Add
                {
                    //PoNod = ListNumber[Num];
                    //PoList = PoNod.TempMainList;

                    TempListLenth = PoList.NumberNodesInList;

                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        WriterAdditonNumByMod.WriteNumber(TempListLenth, PoNod.TempRealLocationInMainList);

                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        // 01
                        if (TempEndC != 0)
                        {
                            //if (LastAddList == -1)
                            //    LastAddList = 0;

                           

                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                while (TempEndC != 0)
                                {
                                    if (LastAddList == TempEndC)
                                        LastAddList = 0;

                                    if (TempMainLists[LastAddList].isClosed)
                                    {
                                        TempMainLists.RemoveAt(LastAddList);
                                        TempEndC--;
                                    }
                                    else
                                    {
                                        
                                        TempEndC = TempMainLists.Count;
                                        break;
                                    }

                                }

                                if (TempEndC == 0)
                                    break;
                        

                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumberW2(Num);

            }

            return TempOutNumber;
        }

        private int ReadOnNumberW3(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                PoList.CloseList();

                {
                    LastAddList = 0;

                    TempListLenth = PoList.NumberNodesInList;

                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        WriterAdditonNumByMod.WriteNumber(TempListLenth, PoNod.TempRealLocationInMainList);

                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        // 01
                        if (TempEndC != 0)
                        {
                            //if (LastAddList == -1)
                            //    LastAddList = 0;



                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                while (TempEndC != 0)
                                {
                                    if (LastAddList == TempEndC)
                                        LastAddList = 0;

                                    if (TempMainLists[LastAddList].isClosed)
                                    {
                                        TempMainLists.RemoveAt(LastAddList);
                                        TempEndC--;
                                    }
                                    else
                                    {

                                        TempEndC = TempMainLists.Count;
                                        break;
                                    }

                                }

                                if (TempEndC == 0)
                                    break;


                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumberW3(Num);

            }

            return TempOutNumber;
        }

        private int ReadOnNumberW4(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                PoList.CloseList();

                {
                 //   LastAddList = TempMainLists.Count - 1;

                    TempListLenth = PoList.NumberNodesInList;

                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        WriterAdditonNumByMod.WriteNumber(TempListLenth, PoNod.TempRealLocationInMainList);

                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        // 01
                        if (TempEndC != 0)
                        {
                            //if (LastAddList == -1)
                            //    LastAddList = 0;

                            //GetAddlist
                            {
                                while (TempEndC != 0)
                                {
                                    if (LastAddList == TempEndC)
                                        LastAddList = 0;

                                    if (TempMainLists[LastAddList].isClosed)
                                    {
                                        TempMainLists.RemoveAt(LastAddList);
                                        TempEndC--;
                                    }
                                    else
                                    {

                                        TempEndC = TempMainLists.Count;
                                        break;
                                    }

                                }

                            }

                            if (TempEndC != 0)
                            {
                                foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                                {
                                    TempMainLists[LastAddList].AddNode(nod);
                                }

                                LastAddList++;
                                if (LastAddList >= TempEndC)
                                    LastAddList = 0;

                            }

                        }

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumberW4(Num);

            }

            return TempOutNumber;
        }

        #endregion

        #region ReaderTree

        public List<int> ReadOneList(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumber(num));
            }

            GetSegmentInfo();
            SegmentNum++;

            return listNum;

        }
        public void ReadAll(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumber(ReaderNum.GetOneNumber()));
            }

        }
        private int TempC = 0;
        private List<int> TempAddList = new List<int>();
        public void ReadOneList(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC !=ModStopLength)
            {
                WriterNum.WriteNumber(ReadOnNumber(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;
        }
        public List<int> ReadOneList(ref ReaderWriterOneNum02B ReaderNum)
        {
            TempAddList.Clear();
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                TempAddList.Add(ReadOnNumber(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;

            return TempAddList;
        }


        public List<int> ReadOneListW2(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumberW2(num));
            }

            GetSegmentInfo();
            SegmentNum++;

            return listNum;

        }
        public void ReadAllW2(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumberW2(ReaderNum.GetOneNumber()));
            }

        }
        public void ReadOneListW2(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                WriterNum.WriteNumber(ReadOnNumberW2(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;
        }
        public List<int> ReadOneListW2(ref ReaderWriterOneNum02B ReaderNum)
        {
            TempAddList.Clear();
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                TempAddList.Add(ReadOnNumberW2(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;

            return TempAddList;
        }

        public List<int> ReadOneListW3(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumberW3(num));
            }

            GetSegmentInfo();
            SegmentNum++;

            return listNum;

        }
        public void ReadAllW3(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumberW3(ReaderNum.GetOneNumber()));
            }

        }
        public void ReadOneListW3(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                WriterNum.WriteNumber(ReadOnNumberW3(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;
        }
        public List<int> ReadOneListW3(ref ReaderWriterOneNum02B ReaderNum)
        {
            TempAddList.Clear();
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                TempAddList.Add(ReadOnNumberW3(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;

            return TempAddList;
        }

        public List<int> ReadOneListW4(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumberW4(num));
            }

            GetSegmentInfo();
            SegmentNum++;

            return listNum;

        }
        public void ReadAllW4(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumberW4(ReaderNum.GetOneNumber()));
            }

        }
        public void ReadOneListW4(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                WriterNum.WriteNumber(ReadOnNumberW4(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;
        }
        public List<int> ReadOneListW4(ref ReaderWriterOneNum02B ReaderNum)
        {
            TempAddList.Clear();
            TempC = 0;
            while (ReaderNum.isAbleRead && TempC != ModStopLength)
            {
                TempAddList.Add(ReadOnNumberW4(ReaderNum.GetOneNumber()));
                TempC++;
            }

            GetSegmentInfo();
            SegmentNum++;

            return TempAddList;
        }

        #endregion

    }




    class MakeTreeDeUniq06Tree
    {

        #region  Properties

        private MakeTreeUniq06Node[] ListNumber;
        private List<MakeTreeUniq06ListNode> MainLists;

        private MakeTreeUniq06Node PoNod;
        private MakeTreeUniq06ListNode PoList;

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        public ReaderWriteFileBits02B ReaderAdditonBits;
        public ReaderWriterOneNumMod02 ReaderAdditonNumByMod;

        public ReaderWriteFileBits02B ReaderBits;

        private int LengthModListReader = 20;

        #endregion

        #region OverLoad

        public MakeTreeDeUniq06Tree()
        {
            ReaderAdditonBits = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeDeUniq06Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            ReaderAdditonBits = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeDeUniq06Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            ReaderAdditonBits = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            #region CreatListNumberNods

            ListNumber = new MakeTreeUniq06Node[ModLength];

            for (int i = 0; i != ModLength; i++)
            {
                ListNumber[i] = new MakeTreeUniq06Node(i);
            }

            #endregion

            #region Create MainList

            MainLists = new List<MakeTreeUniq06ListNode>();

            for (int i = 0; i != ModLength; i++)
            {
                MainLists.Add(new MakeTreeUniq06ListNode(i));
            }

            #endregion

            #region  MainInitial

            for (int i = 0; i != ModLength; i++)
            {
                MainLists[i].MainInitial(ref ListNumber[i]);
            }

            #endregion

            ReaderAdditonNumByMod = new ReaderWriterOneNumMod02(true, LengthModListReader, ref ReaderAdditonBits);

            //  initialAll();




            //Temp
            ModStopLength = ModLength;

            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
        }

        #endregion

        #region Initial

        private void initialAll()
        {
            TempMainLists.Clear();
            foreach (MakeTreeUniq06ListNode list in MainLists)
            {
                list.Initial();
                TempMainLists.Add(list);
            }

            foreach (MakeTreeUniq06Node nod in ListNumber)
            {
                nod.Initial();
            }

            LastAddList = 0;
        }

        #endregion

        #region For info

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        private void GetSegmentInfo()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBits.SumOfReadBits);

        }

        #endregion

        #region ReaderOneNumber

        private List<MakeTreeUniq06ListNode> TempMainLists = new List<MakeTreeUniq06ListNode>();
        private int TempListLocatorInTempList = 0;
        private int TempStartC = 0;
        private int TempEndC = 0;
        private int LastAddList = 0;
        private int TempListLenth = 0;
        private int TempOutNumber = 0;
        private int TempEndC2 = 0;

        private int ReadOnNumber(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                TempListLocatorInTempList = PoList.GetTempListLocateInTempList();
                TempEndC = TempMainLists.Count;
                for (TempStartC = TempListLocatorInTempList; TempStartC != TempEndC; TempStartC++)
                {
                    TempMainLists[TempStartC].ListLocateInTempList--;
                }

                //Checker Error 01
                {
                    if (TempMainLists[TempListLocatorInTempList] != PoList)
                    {
                        return 0;
                    }

                }

                //Last Add list

                LastAddList = TempMainLists[LastAddList].GetTempListLocateInTempList();


                //Remove 
                TempMainLists.RemoveAt(TempListLocatorInTempList);


                //Add
                {
                    TempListLenth = PoList.NumberNodesInList;
                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                      //  ReaderAdditonNumByMod.WriteNumber(TempListLenth, PoNod.TempRealLocationInMainList);
                        //TempOutNumber = PoList.MainFirstNod.RealValue;

                        TempOutNumber = PoList.ListNumber[ReaderAdditonNumByMod.GetOneNumber(TempListLenth)].RealValue;

                    }


                    {
                        TempEndC = TempMainLists.Count;


                        // 01
                        if (TempEndC != 0)
                        {
                            if (LastAddList == -1)
                                LastAddList = 0;
                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                if (LastAddList == TempEndC)
                                    LastAddList = 0;

                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                        //02
                        //TempEndC2 = PoList.ListNumber.Count;

                        //for (TempStartC = 0; TempStartC != TempEndC2; TempStartC++)
                        //{
                        //    if (TempListLenth == TempEndC)
                        //        TempListLenth = 0;

                        //    TempMainLists[TempListLenth].AddNode( PoList.ListNumber[TempStartC]);
                        //    TempListLenth++;
                        //}
                        //if (TempListLenth == TempEndC)
                        //    TempListLenth = 0;

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumber(Num);

            }

            return TempOutNumber;
        }
        private int ReadOnNumberW2(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                PoList.CloseList();


                //Checker Error 01
                {
                    //if (TempMainLists[TempListLocatorInTempList] != PoList)
                    //{
                    //    return 0;
                    //}

                }

                //Last Add list
                //{
                //    TempEndC = TempMainLists.Count;
                //    while (TempEndC != 0)
                //    {
                //        if (LastAddList == TempEndC)
                //            LastAddList = 0;

                //        if (TempMainLists[LastAddList].isClosed)
                //        {
                //            TempMainLists.RemoveAt(LastAddList);
                //            TempEndC--;
                //        }
                //        else
                //        {
                //            break;
                //        }

                //    }
                //}

                //Add
                {
                    //PoNod = ListNumber[Num];
                    //PoList = PoNod.TempMainList;

                    TempListLenth = PoList.NumberNodesInList;

                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        TempOutNumber = PoList.ListNumber[ReaderAdditonNumByMod.GetOneNumber(TempListLenth)].RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        // 01
                        if (TempEndC != 0)
                        {
                            //if (LastAddList == -1)
                            //    LastAddList = 0;



                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                while (TempEndC != 0)
                                {
                                    if (LastAddList == TempEndC)
                                        LastAddList = 0;

                                    if (TempMainLists[LastAddList].isClosed)
                                    {
                                        TempMainLists.RemoveAt(LastAddList);
                                        TempEndC--;
                                    }
                                    else
                                    {
                                        TempEndC = TempMainLists.Count;
                                        break;
                                    }

                                }

                                if (TempEndC == 0)
                                    break;


                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumberW2(Num);

            }

            return TempOutNumber;
        }

        private int ReadOnNumberW3(int Num)
        {
            PoNod = ListNumber[Num];

            if (TempMainLists.Count > 0)
            {
                //Get TempListLocatorInTempList  
                PoList = PoNod.TempMainList;
                PoList.CloseList();

                LastAddList = 0;

                {
                    

                    TempListLenth = PoList.NumberNodesInList;

                    if (TempListLenth < 2)
                    {
                        TempOutNumber = PoList.MainFirstNod.RealValue;
                    }
                    else
                    {
                        TempOutNumber = PoList.ListNumber[ReaderAdditonNumByMod.GetOneNumber(TempListLenth)].RealValue;
                    }


                    {
                        TempEndC = TempMainLists.Count;

                        // 01
                        if (TempEndC != 0)
                        {
                            //if (LastAddList == -1)
                            //    LastAddList = 0;



                            foreach (MakeTreeUniq06Node nod in PoList.ListNumber)
                            {
                                while (TempEndC != 0)
                                {
                                    if (LastAddList == TempEndC)
                                        LastAddList = 0;

                                    if (TempMainLists[LastAddList].isClosed)
                                    {
                                        TempMainLists.RemoveAt(LastAddList);
                                        TempEndC--;
                                    }
                                    else
                                    {
                                        TempEndC = TempMainLists.Count;
                                        break;
                                    }

                                }

                                if (TempEndC == 0)
                                    break;


                                TempMainLists[LastAddList].AddNode(nod);
                                LastAddList++;
                            }
                            if (LastAddList == TempEndC)
                                LastAddList = 0;

                        }

                    }

                }

            }
            else
            {
                LastAddList = 0;
                initialAll();

                TempOutNumber = ReadOnNumberW2(Num);

            }

            return TempOutNumber;
        }


        #endregion

        #region ReaderTree

        public List<int> ReadOneList(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumber(num));
            }


            SegmentNum++;

            return listNum;

        }
        public void ReadAll(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumber(ReaderNum.GetOneNumber()));
            }

        }


        public List<int> ReadOneListW2(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumberW2(num));
            }


            SegmentNum++;

            return listNum;

        }
        public void ReadAllW2(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumberW2(ReaderNum.GetOneNumber()));
            }

        }

        public List<int> ReadOneListW3(ref List<int> ListDataNum)
        {
            List<int> listNum = new List<int>();

            foreach (int num in ListDataNum)
            {
                listNum.Add(ReadOnNumberW3(num));
            }


            SegmentNum++;

            return listNum;

        }
        public void ReadAllW3(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderNum.isAbleRead)
            {
                WriterNum.WriteNumber(ReadOnNumberW3(ReaderNum.GetOneNumber()));
            }

        }


        #endregion

    }


    class MakeTreeUniq06
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTU06ML";
        private string DeExtension = "DeMTU06ML";


        public MakeTreeUniq06()
        {

        }
        public MakeTreeUniq06(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }

        public void StartMakeTreeUniq_FileUniqW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);
         

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBit = new ReaderWriteFileBits02B(false , Extension + ModLength.ToString() + "W01Key");
            if (WriterBit.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBit);
            Tree.GetReaderBits(ref ReaderBits);

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
            WriterBit.CloseFile();
        }
        public void StartMakeTreeDeUniq_FileUniqW01()
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

        public void StartMakeTreeUniq_FileUniqW02OneInfo()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W02");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            ReaderWriteFileBits02B WriterBitKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W02Key");
            if (WriterBitKey.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBitKey);
            Tree.GetReaderBits(ref ReaderBits);

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitKey.GetSavePath, Extension + "W2Info"), false);


           

            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;


            
            List<int> datauniq = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                datauniq = Tree.ReadOneListW2(ref ReaderNum);
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 60;

                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 60, WriterBitKey.SumOfWriteBits + MainSegmentInfo.HowReadBits);
                WriterBitKey.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);  

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }

                ReaderBits.SumOfReadBits = 0;
            }

            try
            {
                ReaderBits.CloseFile();
                WriterBitKey.CloseFile();
                WriterNum.End();

                WriterNodeInfo.CloseFile();
            }
            catch
            {
                //error
            }
        }
        public void StartMakeTreeUniq_FileUniqW02AllInfo()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B WriterBitKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W02Key");
            if (WriterBitKey.GetIsCancel)
                return;// "isCancel";
            WriterBitKey.WriteBitToFile = false;


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBitKey);
            Tree.GetReaderBits(ref ReaderBits);

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitKey.GetSavePath, Extension + "W2Info"), false);


            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeChangerUniq02Tree CUTree2 = new MakeChangerUniq02Tree(ModLength);
            ReaderWriterOneNumMod02 WriterNumMod = new ReaderWriterOneNumMod02(false, 20, ref WriterBitsInfo);
            int TempModNum = 0;
            List<int> Tempdatauniq = new List<int>();

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);




            #region Reader


            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;

            List<int> datauniq = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                datauniq = Tree.ReadOneListW2(ref ReaderNum);
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 60;

                if (datauniq.Count != ModLength)
                    break;

                //61
                {
                    //Oper == 61
                    Tree1.MakeDeUniqOnly_int(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 11, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }

                {
                    //Oper == 62W1 == 62
                    Tree2W1.ReadNumber_W1Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 22, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 62W1 == 68
                    Tree2W2.ReadNumber_W2Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 23, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }
                //03
                {
                    //Oper == 63W1 == 135
                    Tree3W5.ReadNumber_W5(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 35, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 43W1 == 136
                    Tree3W6.ReadNumber_W6(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 36, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //04
                {

                    Tree4.ReadNumber_W1(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 41, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //05

                {
                    //Oper == 45
                    Tree5.GetOneReadList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }


                //CangerUniq
                {
                    Tempdatauniq.Clear();
                    foreach (int n in datauniq)
                    {
                        Tempdatauniq.Add(n);
                    }
                    datauniq = ReaderNum.GetManyNum(ModLength);
                    if (datauniq.Count == ModLength)
                    {
                        TempModNum = CUTree2.MakeListUnuniq(ref Tempdatauniq, ref WriterBitsInfo);
                    }
                    else
                        TempModNum = ModLength;

                    foreach (int n in Tempdatauniq)
                    {
                        WriterNumMod.WriteNumber(TempModNum, n);
                    }


                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 90, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                {
                    //Oper == 46  Sort
                    TreeSort1.SortList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 100, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }




                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 0, WriterBitKey.SumOfWriteBits + MainSegmentInfo.HowReadBits);
                WriterNodeInfo.WriteNod(ref SegmentInfo);

               


                WriterBitKey.SumOfWriteBits = 0;
                WriterBitsInfo.SumOfWriteBits = 0;
                datauniq.Clear();
                ReaderBits.SumOfReadBits = 0;
            }

            #endregion

            ReaderNum.End();
           
            

            try
            {
                //ReaderNum.End();
                //WriterBitKey.CloseFile();
                //WriterBitsInfo.CloseFile();

                WriterNodeInfo.CloseFile();

                WriterBitKey.CloseFile();
                WriterBitsInfo.CloseFile();

            }
            catch
            {
                //error
            }
        }

        public void StartMakeTreeUniq_FileUniqW02()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W02");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBit = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W02Key");
            if (WriterBit.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBit);
            Tree.GetReaderBits(ref ReaderBits);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneListW2(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            WriterBit.CloseFile();
        }
        public void StartMakeTreeDeUniq_FileUniqW02()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B ReaderBitKey = new ReaderWriteFileBits02B(true);
            if (ReaderBitKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W02");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq06Tree Tree = new MakeTreeDeUniq06Tree(ModLength, ref ReaderBitKey);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneListW2(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            ReaderBitKey.CloseFile();
        }


        public void StartMakeTreeUniq_FileUniqW03()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W03");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBit = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W03Key");
            if (WriterBit.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBit);
            Tree.GetReaderBits(ref ReaderBits);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneListW3(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            WriterBit.CloseFile();
        }
        public void StartMakeTreeDeUniq_FileUniqW03()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B ReaderBitKey = new ReaderWriteFileBits02B(true);
            if (ReaderBitKey.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W03");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq06Tree Tree = new MakeTreeDeUniq06Tree(ModLength, ref ReaderBitKey);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneListW3(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            ReaderBitKey.CloseFile();
        }
        public void StartMakeTreeUniq_FileUniqW03AllInfo()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B WriterBitKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W03Key");
            if (WriterBitKey.GetIsCancel)
                return;// "isCancel";
            WriterBitKey.WriteBitToFile = false;


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBitKey);
            Tree.GetReaderBits(ref ReaderBits);

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitKey.GetSavePath, Extension + "W3AllInfo"), false);


            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W03");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeChangerUniq02Tree CUTree2 = new MakeChangerUniq02Tree(ModLength);
            ReaderWriterOneNumMod02 WriterNumMod = new ReaderWriterOneNumMod02(false, 20, ref WriterBitsInfo);
            int TempModNum = 0;
            List<int> Tempdatauniq = new List<int>();

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);




            #region Reader


            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;

            List<int> datauniq = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                datauniq = Tree.ReadOneListW3(ref ReaderNum);
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 60;

                if (datauniq.Count != ModLength)
                    break;

                //61
                {
                    //Oper == 61
                    Tree1.MakeDeUniqOnly_int(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 11, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }

                {
                    //Oper == 62W1 == 62
                    Tree2W1.ReadNumber_W1Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 22, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 62W1 == 68
                    Tree2W2.ReadNumber_W2Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 23, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }
                //03
                {
                    //Oper == 63W1 == 135
                    Tree3W5.ReadNumber_W5(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 35, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 43W1 == 136
                    Tree3W6.ReadNumber_W6(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 36, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //04
                {

                    Tree4.ReadNumber_W1(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 41, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //05

                {
                    //Oper == 45
                    Tree5.GetOneReadList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }


                //CangerUniq
                {
                    Tempdatauniq.Clear();
                    foreach (int n in datauniq)
                    {
                        Tempdatauniq.Add(n);
                    }
                    datauniq = ReaderNum.GetManyNum(ModLength);
                    if (datauniq.Count == ModLength)
                    {
                        TempModNum = CUTree2.MakeListUnuniq(ref Tempdatauniq, ref WriterBitsInfo);
                    }
                    else
                        TempModNum = ModLength;

                    foreach (int n in Tempdatauniq)
                    {
                        WriterNumMod.WriteNumber(TempModNum, n);
                    }


                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 90, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                {
                    //Oper == 46  Sort
                    TreeSort1.SortList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 100, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }




                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 0, WriterBitKey.SumOfWriteBits + MainSegmentInfo.HowReadBits);
                WriterNodeInfo.WriteNod(ref SegmentInfo);




                WriterBitKey.SumOfWriteBits = 0;
                WriterBitsInfo.SumOfWriteBits = 0;
                datauniq.Clear();
                ReaderBits.SumOfReadBits = 0;
            }

            #endregion

            ReaderNum.End();



            try
            {
                //ReaderNum.End();
                //WriterBitKey.CloseFile();
                //WriterBitsInfo.CloseFile();

                WriterNodeInfo.CloseFile();

                WriterBitKey.CloseFile();
                WriterBitsInfo.CloseFile();

            }
            catch
            {
                //error
            }
        }

        public void StartMakeTreeUniq_FileUniqW04()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W04");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBit = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W04Key");
            if (WriterBit.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBit);
            Tree.GetReaderBits(ref ReaderBits);

            List<int> dataUnuniq = new List<int>();
            List<int> datauniq = new List<int>();

            while (ReaderNum.isAbleRead)
            {
                dataUnuniq = ReaderNum.GetManyNum(ModLength);
                datauniq = Tree.ReadOneListW4(ref dataUnuniq);

                foreach (int n in datauniq)
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderNum.End();
            WriterBit.CloseFile();
        }
        public void StartMakeTreeUniq_FileUniqW04AllInfo()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength, ReaderBits);


            ReaderWriteFileBits02B WriterBitKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W04Key");
            if (WriterBitKey.GetIsCancel)
                return;// "isCancel";
            WriterBitKey.WriteBitToFile = false;


            MakeTreeUniq06Tree Tree = new MakeTreeUniq06Tree(ModLength, ref WriterBitKey);
            Tree.GetReaderBits(ref ReaderBits);

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitKey.GetSavePath, Extension + "W4AllInfo"), false);


            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W04");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeChangerUniq02Tree CUTree2 = new MakeChangerUniq02Tree(ModLength);
            ReaderWriterOneNumMod02 WriterNumMod = new ReaderWriterOneNumMod02(false, 20, ref WriterBitsInfo);
            int TempModNum = 0;
            List<int> Tempdatauniq = new List<int>();

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);




            #region Reader


            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;

            List<int> datauniq = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                datauniq = Tree.ReadOneListW4(ref ReaderNum);
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 60;

                if (datauniq.Count != ModLength)
                    break;

                //61
                {
                    //Oper == 61
                    Tree1.MakeDeUniqOnly_int(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 11, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }

                {
                    //Oper == 62W1 == 62
                    Tree2W1.ReadNumber_W1Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 22, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 62W1 == 68
                    Tree2W2.ReadNumber_W2Info(ref datauniq, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 23, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }
                //03
                {
                    //Oper == 63W1 == 135
                    Tree3W5.ReadNumber_W5(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 35, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                    //Oper == 43W1 == 136
                    Tree3W6.ReadNumber_W6(ref datauniq, ModLength);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 36, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //04
                {

                    Tree4.ReadNumber_W1(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 41, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                //05

                {
                    //Oper == 45
                    Tree5.GetOneReadList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }


                //CangerUniq
                {
                    Tempdatauniq.Clear();
                    foreach (int n in datauniq)
                    {
                        Tempdatauniq.Add(n);
                    }
                    datauniq = ReaderNum.GetManyNum(ModLength);
                    if (datauniq.Count == ModLength)
                    {
                        TempModNum = CUTree2.MakeListUnuniq(ref Tempdatauniq, ref WriterBitsInfo);
                    }
                    else
                        TempModNum = ModLength;

                    foreach (int n in Tempdatauniq)
                    {
                        WriterNumMod.WriteNumber(TempModNum, n);
                    }


                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 90, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);

                }

                {
                    //Oper == 46  Sort
                    TreeSort1.SortList(ref datauniq);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 100, WriterBitsInfo.SumOfWriteBits + WriterBitKey.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);
                }




                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 0, WriterBitKey.SumOfWriteBits + MainSegmentInfo.HowReadBits);
                WriterNodeInfo.WriteNod(ref SegmentInfo);




                WriterBitKey.SumOfWriteBits = 0;
                WriterBitsInfo.SumOfWriteBits = 0;
                datauniq.Clear();
                ReaderBits.SumOfReadBits = 0;
            }

            #endregion

            ReaderNum.End();



            try
            {
                //ReaderNum.End();
                //WriterBitKey.CloseFile();
                //WriterBitsInfo.CloseFile();

                WriterNodeInfo.CloseFile();

                WriterBitKey.CloseFile();
                WriterBitsInfo.CloseFile();

            }
            catch
            {
                //error
            }
        }



        private MakeTreeUniqInfoNode00 RefrishSegmentInfo(ref MakeTreeUniqInfoNode00 MainInfoNod, int OperId, int NumOfWriteBits)
        {
            MakeTreeUniqInfoNode00 segmentNod = new MakeTreeUniqInfoNode00();


            segmentNod.SegmentNum = MainInfoNod.SegmentNum;
            segmentNod.OperationId = OperId;

            segmentNod.ModSegmentLength = MainInfoNod.ModSegmentLength;
            segmentNod.HowReadBits = MainInfoNod.HowReadBits;

            segmentNod.EditHowWriteBits(NumOfWriteBits);





            return segmentNod;
        }
        public void StartMakeTreeUniq_FileUniqW02Info()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitsInfo.GetSavePath, Extension + "Info"), false);

            MakeTreeUniq04Tree Tree = new MakeTreeUniq04Tree(ModLength, ref ReaderBits);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);


            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);

            List<int> TempUniqList;

            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;
            int i = 0;
            int Rest = 0;
            while (ReaderBits.isAbleRead)
            {

                //Oper == 40
                TempUniqList = Tree.ReadOne();
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 40;

                //Oper == 41
                Tree1.MakeDeUniqOnly_int(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 41, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //Oper == 42W1 == 42
                Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 42, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 42W1 == 48
                Tree2W2.ReadNumber_W2Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 48, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 43W1 == 135
                Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 135, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 43W1 == 136
                Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 136, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 45
                Tree5.GetOneReadList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 45, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 46  Sort
                TreeSort1.SortList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 46, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //ForReast
                {
                    Rest = MainSegmentInfo.ModSegmentLength - MainSegmentInfo.HowReadBits;
                    if (Rest > 0 && Rest < MainSegmentInfo.ModSegmentLength)
                    {
                        for (i = 0; i != Rest; i++)
                            ReaderBits.GetBit();

                    }
                    else
                    {
                        //Error!!
                    }

                    ReaderBits.SumOfReadBits = 0;
                }
            }

            try
            {
                ReaderBits.CloseFile();
                WriterNodeInfo.CloseFile();

                WriterBitsInfo.CloseFile();

            }
            catch
            {
                //error
            }
        }

        
    }
}
