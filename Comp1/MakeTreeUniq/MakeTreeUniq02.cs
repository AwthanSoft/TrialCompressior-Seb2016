using Comp1.MergeSort.MakeTreeMergeSort;
using Comp1.Public.Lib;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeTreeUniq
{


    class MakeTreeUniq02Node
    {
        public MakeTreeUniq02Node nextzero;
        public MakeTreeUniq02Node nextone;
        public bool isOpen;
        public bool PastBit;
        public int RealLocation;

        public int RightConterNodes ;
        public int LeftCounterNodes ;
        public int SumNextNode;

        public int RealValue;
        public int RootValue;

        public MakeTreeUniq02Node()
        {
            RealValue = 0;
            RealLocation = 0;

            RightConterNodes = 0;
            LeftCounterNodes = 0;
            SumNextNode = 0;

            RootValue = 0;

            isOpen = false;
            PastBit = false;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq02Node(bool pastBit)
        {
            RealValue = 0;
            RealLocation = 0;

            RightConterNodes = 0;
            LeftCounterNodes = 0;
            SumNextNode = 0;

            RootValue = 0;

            isOpen = false;
            PastBit = pastBit;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq02Node(int Locate)
        {
            RealValue = 0;
            RealLocation = Locate;

            RightConterNodes = 0;
            LeftCounterNodes = 0;
            SumNextNode = 0;

            RootValue = 0;

            isOpen = false;
            PastBit = false;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq02Node(int Locate, bool pastBit)
        {
            RealValue = 0;
            RealLocation = Locate;

            RightConterNodes = 0;
            LeftCounterNodes = 0;
            SumNextNode = 0;

            RootValue = 0;

            isOpen = false;
            PastBit = pastBit;

            nextzero = null;
            nextone = null;
        }


        public void InitialNode()
        {
            RealValue = 0;
            RealLocation = 0;

            RightConterNodes = 0;
            LeftCounterNodes = 0;
            SumNextNode = 0;

            RootValue = 0;

            isOpen = false;

        }
        public void InitialCloseNode()
        {
            RealValue = 0;
            RealLocation = 0;

            RootValue = 0;

            isOpen = false;

        }

        public void OpenNode()
        {

            this.isOpen = true;
        }

        public void PlusRightCount()
        {

            RightConterNodes++;
            SumNextNode++;
        }
        public void PlusLeftCount()
        {

            LeftCounterNodes++;
            SumNextNode++;
        }

        public void EditLocation(int Locate)
        {

            RealLocation = Locate;
        }


        public void ReadNode(int rootValue)
        {
            if (PastBit)
            {
                if (RightConterNodes == 0)
                {
                    RealValue = rootValue + LeftCounterNodes + 1;
                }
                else
                {
                    RealValue = rootValue + LeftCounterNodes + 1 ;
                }
            }
            else
            {
                RealValue = (rootValue - LeftCounterNodes - RightConterNodes) + LeftCounterNodes - 1;
            }
        }

    }

    class MakeTreeUniq02Tree
    {

        #region  Properties

        public MakeTreeUniq02Node root;

        public MakeTreeUniq02Node Po;

        private int ModLength = 256;
        private int ModNum = 0;


        private int TempSumNod = 0;
        private int TempLocate = 1;
        private int TempCreateNode = 0;

        public List<int> ListKeyNumber;
        public MakeTreeUniq02Node[] ListNodes;

        #endregion

        #region OverLoad

        public MakeTreeUniq02Tree()
        {
            Creat();
        }
        public MakeTreeUniq02Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq02Node();
            ListNodes = new MakeTreeUniq02Node[ModLength];
            ListKeyNumber = new  List<int>();

            Po = root;

            //ReaderModLength
            {
                int Sum = 0;

                while (Sum < ModLength)
                {
                    ModNum++;
                    Sum = Convert.ToInt32(Math.Pow(2, ModNum));
                }

                ModSegmentLength = ModLength * ModNum;
            }
        }

        #endregion

        #region Initial

        public void initialAll(MakeTreeUniq02Node cr)
        {
            if (cr == null)
                return;
            else
            {
                cr.InitialNode();
               // if (cr.nextzero != null)
                    initialAll(cr.nextzero);
                  //if(cr.nextone!=null)
                      initialAll(cr.nextone);
            }
            // root.Refrance = null;
        }

        public void initialAll()
        {
            initialAll(root);
        }

        #endregion

        #region For info

        private int NumOfReaderBits = 0;


        
        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        private void GetSegmentInfo(int SumOfReadBits)
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(SumOfReadBits);

        }


        #endregion

        #region ReaderTree
        private int TempNumReadTree = 0;
        private void ReadTree(MakeTreeUniq02Node cr ,int RootValue)
        {
            if (cr == null)
                return;

            if (cr.isOpen)
            {
                cr.ReadNode(RootValue);
                ReadTree(cr.nextzero, cr.RealValue);
                ReadTree(cr.nextone , cr.RealValue);
            }
        }
        private void ReadTreeBy1(MakeTreeUniq02Node cr)
        {
            if (cr == null)
                return;

            if (cr.isOpen)
            {
                ReadTreeBy1(cr.nextzero);
                cr.RealValue = TempNumReadTree; TempNumReadTree++;
                ReadTreeBy1(cr.nextone);
            }
        }
        private void ReadTree()
        {
            ////Way 01
            //root.RealValue = ModLength / 2;
            //root.RealValue = root.LeftCounterNodes;
            //ReadTree(root.nextzero, root.RealValue);
            //ReadTree(root.nextone, root.RealValue);

            //Way 02
            TempNumReadTree = 0;
            root.OpenNode();
            ReadTreeBy1(root);

        }

        private void InitialListNodes(MakeTreeUniq02Node cr)
        {
            if (cr == null)
                return;

            if (cr.isOpen)
            {
                ListNodes[cr.RealLocation] = cr;
                InitialListNodes(cr.nextzero);
                InitialListNodes(cr.nextone);
            }
        }

        public void ReadBit(bool BoolValue)
        {
            if (BoolValue)
            {
                if (Po.nextone != null)
                {
                    Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                    TempCreateNode++;

                    Po.PlusRightCount();
                    Po.nextone.OpenNode();

                    //Po = Po.nextone;
                    TempLocate++;
                    TempSumNod++;

                    Po = root;
                }
                else
                {
                    if (Po.nextone.isOpen)
                    {
                        Po.PlusRightCount();
                        Po = Po.nextone;
                    }
                    else
                    {
                        Po.PlusRightCount();
                        Po.nextone.OpenNode();
                        Po.nextone.EditLocation(TempLocate);

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                }
            }
            else
            {
                if (Po.nextzero != null)
                {
                    Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                    TempCreateNode++;

                    Po.PlusLeftCount();
                    Po.nextzero.OpenNode();

                    //Po = Po.nextone;
                    TempLocate++;
                    TempSumNod++;

                    Po = root;
                }
                else
                {
                    if (Po.nextzero.isOpen)
                    {
                        Po.PlusLeftCount();
                        Po = Po.nextzero;
                    }
                    else
                    {
                        Po.PlusLeftCount();
                        Po.nextzero.OpenNode();
                        Po.nextone.EditLocation(TempLocate);

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                }
            }

            if (TempLocate != ModLength)
            {
                ReadTree();
                ListKeyNumber.Add(root.LeftCounterNodes);
                root.OpenNode();
                InitialListNodes(root);

                initialAll();
                TempLocate = 0;
                TempSumNod = 0;
                Po = root;
            }



        }
        public void ReadBit(ref byte[] dataByte , ref List<int > ListNumberData)
        {
            BitArray bitsArr = new BitArray(dataByte);
            foreach (bool b in bitsArr)
            {
                if (b)
                {
                    if (Po.nextone != null)
                    {
                        Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                        TempCreateNode++;

                        Po.PlusRightCount();
                        Po.nextone.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextone.isOpen)
                        {
                            Po.PlusRightCount();
                            Po = Po.nextone;
                        }
                        else
                        {
                            Po.PlusRightCount();
                            Po.nextone.OpenNode();
                            Po.nextone.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }
                else
                {
                    if (Po.nextzero != null)
                    {
                        Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                        TempCreateNode++;

                        Po.PlusLeftCount();
                        Po.nextzero.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextzero.isOpen)
                        {
                            Po.PlusLeftCount();
                            Po = Po.nextzero;
                        }
                        else
                        {
                            Po.PlusLeftCount();
                            Po.nextzero.OpenNode();
                            Po.nextone.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }

                if (TempLocate != ModLength)
                {
                    ReadTree();
                   // ListKeyNumber.Add(root.LeftCounterNodes);

                    root.EditLocation(ModLength);
                    root.OpenNode();
                    InitialListNodes(root);

                    //SaveData
                    {
                        foreach (MakeTreeUniq02Node nod in ListNodes)
                        {
                            ListNumberData.Add(nod.RealValue);
                        }
                    }

                    initialAll();
                    TempLocate = 0;
                    TempSumNod = 0;
                    Po = root;
                }

            }

        }

        public void ReadBit_W1(ref ReaderWriteFileBits02B ReaderBits, ref ReaderWriterOneNum02B WriterNum)
        {
            
            while(ReaderBits.isAbleRead)
            {
                NumOfReaderBits++;


                if (ReaderBits.GetBit())
                {
                    if (Po.nextone == null)
                    {
                        Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                        TempCreateNode++;

                        Po.PlusRightCount();
                        Po.nextone.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextone.isOpen)
                        {
                            Po.PlusRightCount();
                            Po = Po.nextone;
                        }
                        else
                        {
                            Po.PlusRightCount();
                            Po.nextone.OpenNode();
                            Po.nextone.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }
                else
                {
                    if (Po.nextzero == null)
                    {
                        Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                        TempCreateNode++;

                        Po.PlusLeftCount();
                        Po.nextzero.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextzero.isOpen)
                        {
                            Po.PlusLeftCount();
                            Po = Po.nextzero;
                        }
                        else
                        {
                            Po.PlusLeftCount();
                            Po.nextzero.OpenNode();
                            Po.nextzero.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }

                if (TempLocate == ModLength)
                {
                    ReadTree();
                    // ListKeyNumber.Add(root.LeftCounterNodes);

                    root.EditLocation(0);
                    root.OpenNode();
                    InitialListNodes(root);

                    //SaveData
                    {
                        //MakeTreeUniq02Node tempPo = ListNodes[ListNodes.Length - 1];
                        //ListNodes[ListNodes.Length - 1] = ListNodes[0];
                        //ListNodes[0] = tempPo;

                        //for (int i = 0; i != ModLength; i++)
                        //{

                        //}


                        foreach (MakeTreeUniq02Node nod in ListNodes)
                        {
                            WriterNum.WriteNumber(nod.RealValue);
                        }
                    }

                    initialAll();
                    TempLocate = 1;
                    TempSumNod = 1;
                    Po = root;

                    NumOfReaderBits = 0;
                }

            }


            if (TempLocate < ModLength && TempLocate > 1 )
            {
                ListNodes = new MakeTreeUniq02Node[TempLocate];
                ReadTree();
                // ListKeyNumber.Add(root.LeftCounterNodes);

                root.EditLocation(0);
                root.OpenNode();
                InitialListNodes(root);

                //SaveData
                {
                    //MakeTreeUniq02Node tempPo = ListNodes[ListNodes.Length - 1];
                    //ListNodes[ListNodes.Length - 1] = ListNodes[0];
                    //ListNodes[0] = tempPo;

                    //for (int i = 0; i != ModLength; i++)
                    //{

                    //}


                    foreach (MakeTreeUniq02Node nod in ListNodes)
                    {
                        WriterNum.WriteNumber(nod.RealValue);
                    }
                }

                initialAll();
                TempLocate = 1;
                TempSumNod = 1;
                Po = root;

                NumOfReaderBits = 0;
            }

        }
        public List<int> ReadBit_W1(ref ReaderWriteFileBits02B ReaderBits)
        {

            List<int> ListNum = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                NumOfReaderBits++;


                if (ReaderBits.GetBit())
                {
                    if (Po.nextone == null)
                    {
                        Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                        TempCreateNode++;

                        Po.PlusRightCount();
                        Po.nextone.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextone.isOpen)
                        {
                            Po.PlusRightCount();
                            Po = Po.nextone;
                        }
                        else
                        {
                            Po.PlusRightCount();
                            Po.nextone.OpenNode();
                            Po.nextone.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }
                else
                {
                    if (Po.nextzero == null)
                    {
                        Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                        TempCreateNode++;

                        Po.PlusLeftCount();
                        Po.nextzero.OpenNode();

                        //Po = Po.nextone;
                        TempLocate++;
                        TempSumNod++;

                        Po = root;
                    }
                    else
                    {
                        if (Po.nextzero.isOpen)
                        {
                            Po.PlusLeftCount();
                            Po = Po.nextzero;
                        }
                        else
                        {
                            Po.PlusLeftCount();
                            Po.nextzero.OpenNode();
                            Po.nextzero.EditLocation(TempLocate);

                            //Po = Po.nextone;
                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                        }
                    }
                }

                if (TempLocate == ModLength)
                {
                    ReadTree();
                    // ListKeyNumber.Add(root.LeftCounterNodes);

                    root.EditLocation(0);
                    root.OpenNode();
                    InitialListNodes(root);

                    //SaveData
                    {
                        //MakeTreeUniq02Node tempPo = ListNodes[ListNodes.Length - 1];
                        //ListNodes[ListNodes.Length - 1] = ListNodes[0];
                        //ListNodes[0] = tempPo;

                        //for (int i = 0; i != ModLength; i++)
                        //{

                        //}


                        foreach (MakeTreeUniq02Node nod in ListNodes)
                        {
                            ListNum.Add(nod.RealValue);
                        }
                    }

                    initialAll();
                    TempLocate = 1;
                    TempSumNod = 1;
                    Po = root;

                    NumOfReaderBits = 0;

                    break;
                }

            }




            if (TempLocate < ModLength && TempLocate > 1)
            {
                ListNodes = new MakeTreeUniq02Node[TempLocate];
                ReadTree();
                // ListKeyNumber.Add(root.LeftCounterNodes);

                root.EditLocation(0);
                root.OpenNode();
                InitialListNodes(root);

                //SaveData
                {
                    //MakeTreeUniq02Node tempPo = ListNodes[ListNodes.Length - 1];
                    //ListNodes[ListNodes.Length - 1] = ListNodes[0];
                    //ListNodes[0] = tempPo;

                    //for (int i = 0; i != ModLength; i++)
                    //{

                    //}


                    foreach (MakeTreeUniq02Node nod in ListNodes)
                    {
                        ListNum.Add(nod.RealValue);
                    }
                }

                initialAll();
                TempLocate = 1;
                TempSumNod = 1;
                Po = root;

                NumOfReaderBits = 0;
            }



            //ForInfo
            {
               
                GetSegmentInfo(ReaderBits.SumOfReadBits);
                SegmentNum++;

              //  ReaderBits.SumOfReadBits = 0;
            }


            return ListNum;

        }

    

        #endregion


    }

     class MakeTreeDeUniq02Tree
    {

        #region  Properties

        public MakeTreeUniq02Node root;

        public MakeTreeUniq02Node Po;

        private int ModLength = 256;
        private int ModRootValu = 128;

        private int TempSumNod = 0;
        private int TempLocate = 0;

        private int TempRootValue = 0;
        private bool isRootRead = true;
        

        #endregion

        #region OverLoad

        public MakeTreeDeUniq02Tree()
        {
            Creat();
        }
        public MakeTreeDeUniq02Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq02Node();
            Po = root;
            ModRootValu = ModLength / 2;
        }

        #endregion

        #region Initial

        public void initialAll(MakeTreeUniq02Node cr)
        {
            if (cr == null)
                return;
            else
            {
                cr.InitialCloseNode();
               // if (cr.nextzero != null)
                    initialAll(cr.nextzero);
                  //if(cr.nextone!=null)
                      initialAll(cr.nextone);
            }
            // root.Refrance = null;
        }

        public void initialAll()
        {
            initialAll(root);
        }

        #endregion

        #region For info

        private int NumOfWriterBits = 0;
        private int TempCreateNode = 0;

        #endregion

        #region ReaderTree

        private void ReadNumber(int TempNum, ref List<bool> ListAddBits)
        {
            bool isContainu = true;

            if (root.isOpen)
            {
                while (isContainu)
                {
                    if (TempNum > TempRootValue)
                    {
                        //Writ Bits
                        ListAddBits.Add(true);
                        NumOfWriterBits++;

                        if (Po.nextone != null)
                        {
                            Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                            TempCreateNode++;

                            Po.nextone.OpenNode();
                            Po.nextone.RealValue = TempNum;
                           

                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
                        }
                        else
                        {
                            if (Po.nextone.isOpen)
                            {
                                Po = Po.nextone;
                                TempRootValue = Po.RealValue;
                            }
                            else
                            {
                                Po.nextone.OpenNode();
                                Po.nextone.RealValue = TempNum;


                                TempLocate++;
                                TempSumNod++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }
                    else
                    {
                        //Writ Bits
                        ListAddBits.Add(false);
                        NumOfWriterBits++;

                        if (Po.nextzero != null)
                        {
                            Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                            TempCreateNode++;

                            Po.nextzero.OpenNode();
                            Po.nextzero.RealValue = TempNum;


                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
                        }
                        else
                        {
                            if (Po.nextzero.isOpen)
                            {
                                Po = Po.nextzero;
                                TempRootValue = Po.RealValue;
                            }
                            else
                            {
                                Po.nextzero.OpenNode();
                                Po.nextzero.RealValue = TempNum;


                                TempLocate++;
                                TempSumNod++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }

                    if (TempLocate != ModLength)
                    {
                        isRootRead = true;
                    }
                }
            }
            else
            {
                initialAll();
                TempLocate = 0;
                TempSumNod = 0;
                Po = root;

                NumOfWriterBits = 0;


                isRootRead = false;
                root.OpenNode();
                root.RealValue = TempNum;
            }

        }
        private void ReadNumber(int TempNum, ref ReaderWriteFileBits02B WriterBits)
        {
            bool isContainu = true;

            if (!isRootRead)
            {
                while (isContainu)
                {
                    if (TempNum > TempRootValue)
                    {
                        //Writ Bits
                        WriterBits.WriteBit(true);
                        NumOfWriterBits++;

                        if (Po.nextone == null)
                        {
                            Po.nextone = new MakeTreeUniq02Node(TempLocate, true);

                            TempCreateNode++;

                            Po.nextone.OpenNode();
                            Po.nextone.RealValue = TempNum;


                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
                        }
                        else
                        {
                            if (Po.nextone.isOpen)
                            {
                                Po = Po.nextone;
                                TempRootValue = Po.RealValue;
                            }
                            else
                            {
                                Po.nextone.OpenNode();
                                Po.nextone.RealValue = TempNum;


                                TempLocate++;
                                TempSumNod++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }
                    else
                    {
                        //Writ Bits
                        WriterBits.WriteBit(false);

                        if (Po.nextzero == null)
                        {
                            Po.nextzero = new MakeTreeUniq02Node(TempLocate, false);

                            TempCreateNode++;
                            NumOfWriterBits++;

                            Po.nextzero.OpenNode();
                            Po.nextzero.RealValue = TempNum;


                            TempLocate++;
                            TempSumNod++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
                        }
                        else
                        {
                            if (Po.nextzero.isOpen)
                            {
                                Po = Po.nextzero;
                                TempRootValue = Po.RealValue;
                            }
                            else
                            {
                                Po.nextzero.OpenNode();
                                Po.nextzero.RealValue = TempNum;


                                TempLocate++;
                                TempSumNod++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }

                }

                if (TempLocate == ModLength)
                {
                    isRootRead = true;
                }
            }
            else
            {
                initialAll();
                TempLocate = 0;
                TempSumNod = 0;
                Po = root;

                NumOfWriterBits = 0;

                isRootRead = false;
                root.OpenNode();
                root.RealValue = TempNum;
                TempRootValue = root.RealValue;
            }

        }


        public void ReadNumber_W1(ref List<bool> ListAddBits, ref List<int> ListNumberData)
        {

            foreach (int Num in ListNumberData)
            {
                ReadNumber(Num, ref ListAddBits);
            }

        }
        public void ReadNumber_W1( ref ReaderWriterOneNum02B ReaderNum ,ref ReaderWriteFileBits02B WriterBits)
        {
            while(ReaderNum.isAbleRead)
            {
                
                ReadNumber(ReaderNum.GetOneNumber(), ref WriterBits);
            }
        }
        public void ReadNumber_W1Info(ref List<int> ListNumberData, ref ReaderWriteFileBits02B WriterBits)
        {
            foreach (int n in ListNumberData)
            {
                ReadNumber(n, ref WriterBits);
            }
        }


        public void ReadNumber_W2(ref List<bool> ListAddBits, ref List<int> ListNumberData)
        {

            foreach (int Num in ListNumberData)
            {
                if(isRootRead)
                    ReadNumber(ModRootValu, ref ListAddBits);

                ReadNumber(Num, ref ListAddBits);
            }

        }
        public void ReadNumber_W2(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriteFileBits02B WriterBits)
        {
            while (ReaderNum.isAbleRead)
            {
                if (isRootRead)
                    ReadNumber(ModRootValu, ref WriterBits);

                ReadNumber(ReaderNum.GetOneNumber(), ref WriterBits);
            }
        }
        public void ReadNumber_W2Info(ref List<int> ListNumberData, ref ReaderWriteFileBits02B WriterBits)
        {
            foreach (int n in ListNumberData)
            {
                if (isRootRead)
                    ReadNumber(ModRootValu, ref WriterBits);

                ReadNumber(n, ref WriterBits);
            }
        }

      
      

        #endregion


    }


    class MakeTreeUniq02
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTU02ML";
        private string DeExtension = "DeMTU02ML";


        public MakeTreeUniq02()
        {

        }
        public MakeTreeUniq02(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }


        public void StartMakeTreeUniq_FileUniqW01()
        {



            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq02Tree Tree = new MakeTreeUniq02Tree(ModLength);

            while (ReaderBits.isAbleRead)
            {
                Tree.ReadBit_W1(ref ReaderBits, ref WriterNum);
            }

            WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeUniq_FileUniqW01B()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01B");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq02Tree Tree = new MakeTreeUniq02Tree(ModLength);

            while (ReaderBits.isAbleRead)
            {
                foreach (int n in Tree.ReadBit_W1(ref ReaderBits))
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_FileUniqW01()
        {
            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true , ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq02Tree Tree = new MakeTreeDeUniq02Tree(ModLength - 1);

            while (ReaderNum.isAbleRead)
            {
                Tree.ReadNumber_W1( ref ReaderNum ,ref WriterBits);
            }

            ReaderNum.End();
            WriterBits.CloseFile();

        }



        public void StartMakeTreeDeUniq_FileUniqW02()
        {
            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W02");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq02Tree Tree = new MakeTreeDeUniq02Tree(ModLength);

            while (ReaderNum.isAbleRead)
            {
                Tree.ReadNumber_W2(ref ReaderNum, ref WriterBits);
            }

            ReaderNum.End();
            WriterBits.CloseFile();

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
        public void StartMakeTreeUniq_FileUniqW01Info()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;


            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitsInfo.GetSavePath, Extension + "Info"), false);




            MakeTreeUniq02Tree Tree2W2 = new MakeTreeUniq02Tree(ModLength);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);


            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

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

                //Oper == 20
                TempUniqList = Tree2W2.ReadBit_W1(ref ReaderBits);
                MainSegmentInfo = Tree2W2.SegmentInfoNod;
                MainSegmentInfo.OperationId = 20;

                //Oper == 21
                Tree1.MakeDeUniqOnly_int(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 21, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 22W1 == 22
                Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 22, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 23W1 == 135
                Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 135, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 23W1 == 136
                Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 136, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 24
                Tree4.ReadNumber_W1(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 24, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //Oper == 25
                Tree5.GetOneReadList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 25, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 26  Sort
                TreeSort1.SortList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 26, WriterBitsInfo.SumOfWriteBits);
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
