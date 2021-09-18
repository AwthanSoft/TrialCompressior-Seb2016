using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeTreeUniq
{

    class MakeTreeUniq03Node
    {
        public MakeTreeUniq03Node nextzero;
        public MakeTreeUniq03Node nextone;
        public bool PastBit;
        public int RealLocation;
        public int RealValue;


        public bool isOpen;
        public bool isModLevel;
        public int NodeLevel;

        public MakeTreeUniq03Node()
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = false;

            isModLevel = false;
            NodeLevel = 0;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq03Node(bool pastBit)
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = pastBit;

            isModLevel = false;
            NodeLevel = 0;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq03Node(bool pastBit , int Level )
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = pastBit;

            isModLevel = false;
            NodeLevel = Level;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq03Node(bool pastBit, bool isLevelMod, int Level)
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = pastBit;

            isModLevel = isLevelMod;
            NodeLevel = Level;

            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq03Node(bool pastBit , bool isLevelMod)
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = pastBit;

            isModLevel = isLevelMod;
            NodeLevel = 0;

            nextzero = null;
            nextone = null;
        }

        public void InitialNode()
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;

        }
        public void InitialCloseNode()
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;

        }

        public void OpenNode()
        {

            this.isOpen = true;
        }

        public void EditLocation(int Locate)
        {

            RealLocation = Locate;
        }
        public void EditRealValue(int Valu)
        {

            RealValue = Valu;
        }

       
    }

    class MakeTreeUniq03Tree
    {

        #region  Properties

        private MakeTreeUniq03Node root;
        private MakeTreeUniq03Node Po;
        private int ModNum = 0;
        private int ModLength = 256;
        private int ModLevel=4;

        public MakeTreeUniq03Node[] ListNodes;

        private bool isStopMod = false;
        public ReaderWriteFileBits02B ReaderBit;
        private int LengthModListReader = 20;
        private ReaderWriterOneNumMod02 ReaderByMod;
       // private ReaderWriterOneNum02B[] ListReaderNum;

        #endregion

        #region OverLoad

        public MakeTreeUniq03Tree()
        {
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq03Tree(int ModNumLength , int LevelMod)
        {
            ModLength = ModNumLength;
            ModLevel = LevelMod;
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq03Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits, int LevelMod)
        {
            ModLength = ModNumLength;
            ModLevel = LevelMod;

            ReaderBit = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq03Node();
            //ListNodes = new MakeTreeUniq03Node[ModLength];

            Po = root;

            ReaderByMod = new ReaderWriterOneNumMod02(true, LengthModListReader, ref ReaderBit);

            //ListReaderNum = new ReaderWriterOneNum02B[2];
            //ListReaderNum[0] = new ReaderWriterOneNum02B(true, 3, ReaderBit);
            //ListReaderNum[1] = new ReaderWriterOneNum02B(true, 4, ReaderBit);


            StopLength = ModLength;


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

        public void initialAll(MakeTreeUniq03Node cr)
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
        private int SumReaderBitForTree = 0; 

        private int SumCreateNod = 0;
        private int SumOpenNode = 0;

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        private void GetSegmentInfo()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBit.SumOfReadBits);

        }


        #endregion

        #region ReaderTree

        #region M1

        private void ReadOne()
        {
            MakeTree();
            SumReaderBitForTree = NumOfReaderBits;
            ReadTree();
            ReadLocate();

            List<int> listNum = new List<int>();

            foreach (MakeTreeUniq03Node nod in ListNodes)
            {
                listNum.Add(nod.RealValue);
            }

            initialAll();

            ReaderBit.SumOfReadBits = 0;

            NumOfReaderBits = 0;
            SumReaderBitForTree = 0;
            SumOpenNode = 0;

        }
        public void ReadOne(ref ReaderWriterOneNumMod02 WriterBitsByMod)
        {
            MakeTree();
            ReadTree();
            ReadLocate();

            List<int> listNum = new List<int>();

            foreach (MakeTreeUniq03Node nod in ListNodes)
            {
                listNum.Add(nod.RealValue);
            }

            
            int TempModLength = listNum.Count;
            WriterBitsByMod.WriteNumber(16777216, TempModLength);

            foreach (int num in listNum)
            {
                WriterBitsByMod.WriteNumber(TempModLength, num);
            }


            initialAll();

            SumOpenNode = 0;
        }
        public List<int> ReadOneToGetList()
        {
            MakeTree();
            ReadTree();
            ReadLocate();

            List<int> listNum = new List<int>();

            foreach (MakeTreeUniq03Node nod in ListNodes)
            {
                listNum.Add(nod.RealValue);
            }



            initialAll();
            GetSegmentInfo();

            ReaderBit.SumOfReadBits = 0;
            SegmentNum++;

            SumOpenNode = 0;


            return listNum;
        }
        public void ReadAll()
        {
            while (ReaderBit.isAbleRead)
            {
                ReadOne();
            }
        }

        #endregion


        #region M1



        #endregion

        #region MakeTree
        private int TempNum = 0;
        private void CreateTree_W1()
        {
            if (root.nextzero == null)
            {
                root.nextzero = new MakeTreeUniq03Node(false, false, 1);
                SumCreateNod++;

                root.nextzero.OpenNode();
                SumOpenNode++;
                CreateTree_W1(root.nextzero, 1);
            }
            else
            {
                root.nextzero.OpenNode();
                SumOpenNode++;
                CreateTree_W1(root.nextzero, 1);
            }

            if (root.nextone == null)
            {
                root.nextone = new MakeTreeUniq03Node(true, false, 1);
                SumCreateNod++;

                root.nextone.OpenNode();
                SumOpenNode++;
                CreateTree_W1(root.nextone, 1);
            }
            else
            {
                root.nextone.OpenNode();
                SumOpenNode++;
                CreateTree_W1(root.nextone, 1);
            }
        }
        private void CreateTree_W1(MakeTreeUniq03Node cr, int TempLevelMod)
        {
            if (ReaderBit.isAbleRead)
            {
                if (TempLevelMod < ModLevel)
                {
                    TempNum = ReaderByMod.GetOneNumber(3);
                    //TempNum = ListReaderNum[0].GetOneNumber();

                    if (TempNum == 1)
                    {
                        NumOfReaderBits++;
                        NumOfReaderBits++;

                        //isMore For 0
                        if (cr.nextzero == null)
                        {
                            cr.nextzero = new MakeTreeUniq03Node(false , false ,TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                        }
                        else
                        {
                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                        }
                    }
                    else
                    {
                        if (TempNum == 2)
                        {
                            NumOfReaderBits++;
                            NumOfReaderBits++;

                            //isMore For 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }
                        }
                        else
                        {
                            NumOfReaderBits++;

                            //isMore For 0 & 1 => 0
                            if (cr.nextzero == null)
                            {
                                cr.nextzero = new MakeTreeUniq03Node(false, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                            }

                            //isMore For 0 & 1 => 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }

                        }
                    }

                }
                else
                {
                    TempNum = ReaderByMod.GetOneNumber(4);
                    //TempNum = ListReaderNum[1].GetOneNumber();
                    NumOfReaderBits++;
                    NumOfReaderBits++;

                    if (TempNum == 2)
                    {
                        //isLess For 0
                        if (cr.nextzero == null)
                        {
                            cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                        }
                        else
                        {
                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                        }
                    }
                    else
                    {
                        if (TempNum == 1)
                        {
                            //isLess For 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W1(cr.nextone, TempLevelMod + 1);
                            }
                        }
                        else
                        {
                            //isLess For 0 & 1 => 0
                            if (TempNum == 3)
                            {
                                if (cr.nextzero == null)
                                {
                                    cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                                    SumCreateNod++;

                                    cr.nextzero.OpenNode();
                                    SumOpenNode++;
                                    CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                                }
                                else
                                {
                                    cr.nextzero.OpenNode();
                                    SumOpenNode++;
                                    CreateTree_W1(cr.nextzero, TempLevelMod + 1);
                                }

                                //isLess For 0 & 1 => 1
                                if (cr.nextone == null)
                                {
                                    cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                    SumCreateNod++;

                                    cr.nextone.OpenNode();
                                    SumOpenNode++;
                                    CreateTree_W1(cr.nextone, TempLevelMod + 1);
                                }
                                else
                                {
                                    cr.nextone.OpenNode();
                                    SumOpenNode++;
                                    CreateTree_W1(cr.nextone, TempLevelMod + 1);
                                }
                            }
                            else
                            {
                                //isLess For Null & Null 
                            }
                        }
                    }
                }
            }

        }


        private void CreateTree_W2()
        {
            List<MakeTreeUniq03Node> ListNodMod = new List<MakeTreeUniq03Node>();
            CreateTree_W2(this.root, 0, ref ListNodMod);

            foreach (MakeTreeUniq03Node nod in ListNodMod)
            {
                if (ReaderBit.isAbleRead)
                {
                    CreateTree_W2(this.root, nod.NodeLevel);
                }
                else
                {
                    break;
                }
            }
        }
        private void CreateTree_W2(MakeTreeUniq03Node cr, int TempLevelMod)
        {
            if (ReaderBit.isAbleRead)
            {
                TempNum = ReaderByMod.GetOneNumber(4);
                //TempNum = ListReaderNum[1].GetOneNumber();
                NumOfReaderBits++;
                NumOfReaderBits++;

                if (TempNum == 1)
                {
                    //isLess For 0
                    if (cr.nextzero == null)
                    {
                        cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                        SumCreateNod++;

                        cr.nextzero.OpenNode();
                        SumOpenNode++;
                        CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                    }
                    else
                    {
                        cr.nextzero.OpenNode();
                        SumOpenNode++;
                        CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                    }
                }
                else
                {
                    if (TempNum == 2)
                    {
                        //isLess For 1
                        if (cr.nextone == null)
                        {
                            cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextone.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextone, TempLevelMod + 1);
                        }
                        else
                        {
                            cr.nextone.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextone, TempLevelMod + 1);
                        }
                    }
                    else
                    {
                        //isLess For 0 & 1 => 0
                        if (TempNum == 3)
                        {
                            if (cr.nextzero == null)
                            {
                                cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                            }

                            //isLess For 0 & 1 => 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1);
                            }
                        }
                        else
                        {
                            //isLess For Null & Null 
                        }

                    }
                }
            }

        }
        private void CreateTree_W2(MakeTreeUniq03Node cr, int TempLevelMod ,ref List<MakeTreeUniq03Node> ListNodMod )
        {
            if (ReaderBit.isAbleRead)
            {
                if (TempLevelMod < ModLevel)
                {
                    TempNum = ReaderByMod.GetOneNumber(3);
                    //TempNum = ListReaderNum[0].GetOneNumber();

                    if (TempNum == 0)
                    {
                        NumOfReaderBits++;
                        NumOfReaderBits++;

                        //isMore For 0
                        if (cr.nextzero == null)
                        {
                            cr.nextzero = new MakeTreeUniq03Node(false, false, TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                        }
                        else
                        {
                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                        }
                    }
                    else
                    {
                        if (TempNum == 2)
                        {
                            NumOfReaderBits++;
                            NumOfReaderBits++;

                            //isMore For 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                        }
                        else
                        {
                            NumOfReaderBits++;

                            //isMore For 0 & 1 => 0
                            if (cr.nextzero == null)
                            {
                                cr.nextzero = new MakeTreeUniq03Node(false, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                            }

                            //isMore For 0 & 1 => 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }

                        }
                    }

                }
                else
                {
                    ListNodMod.Add(cr);
                }
            }

        }


        private void CreateTree_W3()
        {
            List<MakeTreeUniq03Node> ListNodMod = new List<MakeTreeUniq03Node>();
            CreateTree_W2(this.root, 0, ref ListNodMod);

            foreach (MakeTreeUniq03Node nod in ListNodMod)
            {
                if (ReaderBit.isAbleRead)
                {
                    CreateTree_W2(this.root, nod.NodeLevel);
                }
                else
                {
                    break;
                }
            }












        }
        private void CreateTree_W3(MakeTreeUniq03Node cr, int TempLevelMod)
        {
            if (ReaderBit.isAbleRead)
            {
                TempNum = ReaderByMod.GetOneNumber(4);
               // TempNum = ListReaderNum[1].GetOneNumber();
                NumOfReaderBits++;
                NumOfReaderBits++;

                if (TempNum == 1)
                {
                    //isLess For 0
                    if (cr.nextzero == null)
                    {
                        cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                        SumCreateNod++;

                        cr.nextzero.OpenNode();
                        SumOpenNode++;
                        CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                    }
                    else
                    {
                        cr.nextzero.OpenNode();
                        SumOpenNode++;
                        CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                    }
                }
                else
                {
                    if (TempNum == 2)
                    {
                        //isLess For 1
                        if (cr.nextone == null)
                        {
                            cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextone.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextone, TempLevelMod + 1);
                        }
                        else
                        {
                            cr.nextone.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextone, TempLevelMod + 1);
                        }
                    }
                    else
                    {
                        //isLess For 0 & 1 => 0
                        if (TempNum == 3)
                        {
                            if (cr.nextzero == null)
                            {
                                cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1);
                            }

                            //isLess For 0 & 1 => 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1);
                            }
                        }
                        else
                        {
                            //isLess For Null & Null 
                        }

                    }
                }
            }

        }
        private void CreateTree_W3(MakeTreeUniq03Node cr, int TempLevelMod, ref List<MakeTreeUniq03Node> ListNodMod)
        {
            if (ReaderBit.isAbleRead)
            {
                if (TempLevelMod < ModLevel)
                {
                    TempNum = ReaderByMod.GetOneNumber(3);
                    //TempNum = ListReaderNum[0].GetOneNumber();

                    if (TempNum == 0)
                    {
                        NumOfReaderBits++;
                        NumOfReaderBits++;

                        //isMore For 0
                        if (cr.nextzero == null)
                        {
                            cr.nextzero = new MakeTreeUniq03Node(false, false, TempLevelMod + 1);
                            SumCreateNod++;

                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                        }
                        else
                        {
                            cr.nextzero.OpenNode();
                            SumOpenNode++;
                            CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                        }
                    }
                    else
                    {
                        if (TempNum == 2)
                        {
                            NumOfReaderBits++;
                            NumOfReaderBits++;

                            //isMore For 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                        }
                        else
                        {
                            NumOfReaderBits++;

                            //isMore For 0 & 1 => 0
                            if (cr.nextzero == null)
                            {
                                cr.nextzero = new MakeTreeUniq03Node(false, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextzero.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextzero, TempLevelMod + 1, ref ListNodMod);
                            }

                            //isMore For 0 & 1 => 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, false, TempLevelMod + 1);
                                SumCreateNod++;

                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                SumOpenNode++;
                                CreateTree_W2(cr.nextone, TempLevelMod + 1, ref ListNodMod);
                            }

                        }
                    }

                }
                else
                {
                    ListNodMod.Add(cr);
                }
            }

        }

        public int TempOfStopLength = 0;
        public int StopLength = 256;
        private void CreateTree_W4()
        {
            TempOfStopLength = 3;
            root.OpenNode();
     //       TempOfStopLength++;
            if (root.nextzero == null)
            {
                root.nextzero = new MakeTreeUniq03Node(false, false, 1);
                SumCreateNod++;
                root.nextzero.OpenNode();
               // TempOfStopLength++;
                CreateTree_W4(root.nextzero, 1);
            }
            else
            {
                root.nextzero.OpenNode();
              //  TempOfStopLength++;
                CreateTree_W4(root.nextzero, 1);
            }

            if (root.nextone == null)
            {
                root.nextone = new MakeTreeUniq03Node(true, false, 1);
                SumCreateNod++;
                root.nextone.OpenNode();
             //   TempOfStopLength++;
                CreateTree_W4(root.nextone, 1);
            }
            else
            {
                root.nextone.OpenNode();
             //   TempOfStopLength++;
                CreateTree_W4(root.nextone, 1);
            }
        }
        private void CreateTree_W4(MakeTreeUniq03Node cr, int TempLevelMod)
        {
            if (TempOfStopLength == StopLength)
                return;

            if (ReaderBit.isAbleRead)
            {
                {
                    //TempNum = ListReaderNum[1].GetOneNumber();
                    TempNum = ReaderByMod.GetOneNumber(4);

                    if (TempNum == 2)
                    {
                        //isLess For 0
                        if (cr.nextzero == null)
                        {
                            cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                            SumCreateNod++;
                            cr.nextzero.OpenNode();
                            TempOfStopLength++;
                            CreateTree_W4(cr.nextzero, TempLevelMod + 1);
                        }
                        else
                        {
                            cr.nextzero.OpenNode();
                            TempOfStopLength++;
                            CreateTree_W4(cr.nextzero, TempLevelMod + 1);
                        }
                    }
                    else
                    {
                        if (TempNum == 1)
                        {
                            //isLess For 1
                            if (cr.nextone == null)
                            {
                                cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                SumCreateNod++;
                                cr.nextone.OpenNode();
                                TempOfStopLength++;
                                CreateTree_W4(cr.nextone, TempLevelMod + 1);
                            }
                            else
                            {
                                cr.nextone.OpenNode();
                                TempOfStopLength++;
                                CreateTree_W4(cr.nextone, TempLevelMod + 1);
                            }
                        }
                        else
                        {
                            //isLess For 0 & 1 => 0
                            if (TempNum == 3)
                            {
                                if (cr.nextzero == null)
                                {
                                    cr.nextzero = new MakeTreeUniq03Node(false, true, TempLevelMod + 1);
                                    SumCreateNod++;
                                    cr.nextzero.OpenNode();
                                    TempOfStopLength++;
                                    CreateTree_W4(cr.nextzero, TempLevelMod + 1);
                                }
                                else
                                {
                                    cr.nextzero.OpenNode();
                                    TempOfStopLength++;
                                    CreateTree_W4(cr.nextzero, TempLevelMod + 1);
                                }

                                //isLess For 0 & 1 => 1
                                if (cr.nextone == null)
                                {
                                    cr.nextone = new MakeTreeUniq03Node(true, true, TempLevelMod + 1);
                                    SumCreateNod++;
                                    cr.nextone.OpenNode();
                                    TempOfStopLength++;
                                    CreateTree_W4(cr.nextone, TempLevelMod + 1);
                                }
                                else
                                {
                                    cr.nextone.OpenNode();
                                    TempOfStopLength++;
                                    CreateTree_W4(cr.nextone, TempLevelMod + 1);
                                }
                            }
                            else
                            {
                                //isLess For Null & Null 
                            }
                        }
                    }
                }
            }



        }



        private List<MakeTreeUniq03Node> ListNodMod3A = new List<MakeTreeUniq03Node>();
        private List<MakeTreeUniq03Node> ListNodMod3B = new List<MakeTreeUniq03Node>();
        private List<MakeTreeUniq03Node> TempListNodMod3C;
        private void CreateTree_W5()
        {
            ListNodMod3A.Clear();
            ListNodMod3B.Clear();

            #region InitialRoot

            root.OpenNode();
            //       TempOfStopLength++;
            if (root.nextzero == null)
            {
                root.nextzero = new MakeTreeUniq03Node(false, false, 1);
                SumCreateNod++;
                root.nextzero.OpenNode();
                // TempOfStopLength++;
            }
            else
            {
                root.nextzero.OpenNode();
                //  TempOfStopLength
            }

            if (root.nextone == null)
            {
                root.nextone = new MakeTreeUniq03Node(true, false, 1);
                SumCreateNod++;
                root.nextone.OpenNode();
                //   TempOfStopLength++;
            }
            else
            {
                root.nextone.OpenNode();
                //   TempOfStopLength++;
            }

            ListNodMod3A.Add(root.nextzero);
            ListNodMod3A.Add(root.nextone);

            #endregion

            while (ListNodMod3A.Count > 0 && ReaderByMod.isAbleRead)
            {
                #region Fill List
                foreach (MakeTreeUniq03Node cr in ListNodMod3A)
                {
                    if (ReaderByMod.isAbleRead)
                    {
                        {
                            //TempNum = ListReaderNum[1].GetOneNumber();
                            TempNum = ReaderByMod.GetOneNumber(4);

                            if (TempNum == 2)
                            {
                                //isLess For 0
                                if (cr.nextzero == null)
                                {
                                    cr.nextzero = new MakeTreeUniq03Node(false, cr.NodeLevel + 1);
                                    SumCreateNod++;
                                    cr.nextzero.OpenNode();
                                    ListNodMod3B.Add(cr.nextzero);
                                }
                                else
                                {
                                    cr.nextzero.OpenNode();
                                    ListNodMod3B.Add(cr.nextzero);
                                }
                            }
                            else
                            {
                                if (TempNum == 1)
                                {
                                    //isLess For 1
                                    if (cr.nextone == null)
                                    {
                                        cr.nextone = new MakeTreeUniq03Node(true, cr.NodeLevel + 1);
                                        SumCreateNod++;
                                        cr.nextone.OpenNode();
                                        ListNodMod3B.Add(cr.nextone);
                                    }
                                    else
                                    {
                                        cr.nextone.OpenNode();
                                        ListNodMod3B.Add(cr.nextone);
                                    }
                                }
                                else
                                {
                                    //isLess For 0 & 1 => 0
                                    if (TempNum == 3)
                                    {
                                        if (cr.nextzero == null)
                                        {
                                            cr.nextzero = new MakeTreeUniq03Node(false, cr.NodeLevel + 1);
                                            SumCreateNod++;
                                            cr.nextzero.OpenNode();
                                            ListNodMod3B.Add(cr.nextzero);
                                        }
                                        else
                                        {
                                            cr.nextzero.OpenNode();
                                            ListNodMod3B.Add(cr.nextzero);
                                        }

                                        //isLess For 0 & 1 => 1
                                        if (cr.nextone == null)
                                        {
                                            cr.nextone = new MakeTreeUniq03Node(true, cr.NodeLevel + 1);
                                            SumCreateNod++;
                                            cr.nextone.OpenNode();
                                            ListNodMod3B.Add(cr.nextone);
                                        }
                                        else
                                        {
                                            cr.nextone.OpenNode();
                                            ListNodMod3B.Add(cr.nextone);
                                        }
                                    }
                                    else
                                    {
                                        //isLess For Null & Null 
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                #endregion


                ListNodMod3A.Clear();
                TempListNodMod3C = ListNodMod3A;
                ListNodMod3A = ListNodMod3B;
                ListNodMod3B = TempListNodMod3C;
            }

        }






        private void MakeTree()
        {
            //if (isStopMod)
            //{
               
            //}
            //else
            //{

            //}


           // CreateTree_W1();

            CreateTree_W5();
            
        }
        #endregion

        #region ReaderTree
        private int TempNumReadTree = 0;
        private void ReadTreeBy1(MakeTreeUniq03Node cr)
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
            TempNumReadTree = 0;
            root.OpenNode();
            ReadTreeBy1(root);

        }
        #endregion

        #region ReadLocate
        private List<MakeTreeUniq03Node> LocatePropList = new List<MakeTreeUniq03Node>();
        //private MakeTreeUniq03Node TempRoot;
        private int LocateCounter = 0;
        private int TempNumLocate = 0;
        private void ReadLocate()
        {
            LocateCounter = 1;
            TempNumLocate = 0;
            LocatePropList.Clear();
            Po = root;
            root.OpenNode();
            root.EditLocation(0);

            if (Po.nextzero != null)
            {
                if (Po.nextzero.isOpen)
                {
                    LocatePropList.Add(Po.nextzero);
                }
            }
            if (Po.nextone != null)
            {
                if (Po.nextone.isOpen)
                {
                    LocatePropList.Add(Po.nextone);
                }
            }
        

            bool isContinueLocate = true;
           

            while (isContinueLocate)
            {
                if (LocatePropList.Count <= 1)
                {
                    if (LocatePropList.Count == 0)
                    {
                        isContinueLocate = false;
                        break;
                    }
                    else
                    {
                        TempNumLocate = 0;
                        Po = LocatePropList[TempNumLocate];
                        Po.EditLocation(LocateCounter);
                        LocateCounter++;
                      //  LocatePropList.RemoveAt(TempNumLocate);

                        if (Po.nextzero != null)
                        {
                            if (Po.nextzero.isOpen)
                            {
                                LocatePropList.Add(Po.nextzero);
                            }
                        }
                        if (Po.nextone != null)
                        {
                            if (Po.nextone.isOpen)
                            {
                                LocatePropList.Add(Po.nextone);
                            }
                        }
                        LocatePropList.RemoveAt(TempNumLocate);

                    }
                }
                else
                {
                    TempNumLocate = ReaderByMod.GetOneNumber(LocatePropList.Count);
                    Po = LocatePropList[TempNumLocate];
                    Po.EditLocation(LocateCounter);
                    LocateCounter++;
                    //LocatePropList.RemoveAt(TempNumLocate);

                    if (Po.nextzero != null)
                    {
                        if (Po.nextzero.isOpen)
                        {
                            LocatePropList.Add(Po.nextzero);
                        }
                    }
                    if (Po.nextone != null)
                    {
                        if (Po.nextone.isOpen)
                        {
                            LocatePropList.Add(Po.nextone);
                        }
                    }
                    LocatePropList.RemoveAt(TempNumLocate);

                }

            }

            ListNodes = new MakeTreeUniq03Node[LocateCounter];

            InitialListNodes(root);
        }
   
        

        private void InitialListNodes(MakeTreeUniq03Node cr)
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


        #endregion

        #endregion


    }


    class MakeTreeDeUniq03Tree
    {

        #region  Properties

        private MakeTreeUniq03Node root;
        private MakeTreeUniq03Node Po;

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        private ReaderWriteFileBits02B WriterBits;
        private ReaderWriterOneNumMod02 WriterNumByMod;
        private int LengthModListReader = 20;

        #endregion

        #region OverLoad

        public MakeTreeDeUniq03Tree()
        {
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeUniq03Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeUniq03Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            WriterBits = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq03Node();

            Po = root;
            WriterNumByMod = new ReaderWriterOneNumMod02(false, LengthModListReader, ref WriterBits);



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

        public void initialAll(MakeTreeUniq03Node cr)
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

            SegmentNum++;
        }

        #endregion

        #region For info

        private int SegmentNum = -1;
        private int ModSegmentLength = 2048;


        #endregion

        #region ReaderTree

        #region MakeTree
        private int Ci = 0;
        public void ReadNumber_W5(ref ReaderWriterOneNumMod02 ReaderByMod, int StopmodLength)
        {

            StopModLength = StopmodLength;

            initialAll();
            isRootRead = true;
            Po = root;
               

            //BuildTree 
            Ci = 0;
            while (Ci != StopModLength)
            {
                BuildTree(ReaderByMod.GetOneNumber(StopmodLength));
                Ci++;
            }

            WriteFirst_W5();
            InitialListNodes();
            GetLocatePropList();


        }
        public void ReadNumber_W5(ref List<int> ListNumberData , int StopmodLength)
        {


            StopModLength = StopmodLength;

            initialAll();
            isRootRead = true;
            Po = root;

            //BuildTree 
            foreach (int Num in ListNumberData)
            {
                BuildTree(Num);
            }

            WriteFirst_W5();
            InitialListNodes();
            GetLocatePropList();


        }



        private int MiddleNumber = 0;
        private int TempReadNum = 0;
        public void ReadNumber_W6(ref ReaderWriterOneNumMod02 ReaderByMod, int StopmodLength)
        {

            StopModLength = StopmodLength + 1;

            initialAll();
            isRootRead = true;
            Po = root;



            //Middle Number
            MiddleNumber = StopmodLength / 2;
            BuildTree(MiddleNumber);

            //BuildTree 
            Ci = 0;
            while (Ci != StopmodLength)
            {
                TempReadNum = ReaderByMod.GetOneNumber(StopmodLength);

                if (TempReadNum != MiddleNumber)
                {
                    BuildTree(TempReadNum);
                }
                else
                {
                    BuildTree(StopmodLength + 1);
                }
                Ci++;
            }

            WriteFirst_W5();
            InitialListNodes();
            GetLocatePropList();


        }
        public void ReadNumber_W6(ref List<int> ListNumberData, int StopmodLength)
        {

            StopModLength = StopmodLength + 1;

            initialAll();
            isRootRead = true;
            Po = root;



            //Middle Number
            MiddleNumber = StopmodLength / 2;
            BuildTree(MiddleNumber);

            //BuildTree 
            foreach (int Num in ListNumberData)
            {

                if (Num != MiddleNumber)
                {
                    BuildTree(Num);
                }
                else
                {
                    BuildTree(StopmodLength + 1);
                }
            }

            WriteFirst_W5();
            InitialListNodes();
            GetLocatePropList();


        }

        #endregion

        #region BuildTree
     
        //01
        private int StopModLength = 256;
        private int TempLocate = 0;
        private bool isRootRead = true;
        private bool isContainu = true;

        private int TempRootValue = 0;

        private void BuildTree(int TempNum)
        {
             isContainu = true;

            if (!isRootRead)
            {
                while (isContainu)
                {
                    if (TempNum > TempRootValue)
                    {
                       
                        if (Po.nextone == null)
                        {
                            Po.nextone = new MakeTreeUniq03Node(true);

                            Po.nextone.OpenNode();
                            Po.nextone.RealValue = TempNum;
                            Po.nextone.EditLocation(TempLocate);

                            TempLocate++;

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
                                Po.nextone.EditLocation(TempLocate);


                                TempLocate++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }
                    else
                    {
                        //Writ Bits
                      

                        if (Po.nextzero == null)
                        {
                            Po.nextzero = new MakeTreeUniq03Node( false);

                            Po.nextzero.OpenNode();
                            Po.nextzero.RealValue = TempNum;
                            Po.nextzero.EditLocation(TempLocate);


                            TempLocate++;

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
                                Po.nextzero.EditLocation(TempLocate);


                                TempLocate++;

                                Po = root;
                                TempRootValue = root.RealValue;
                                isContainu = false;
                            }
                        }
                    }

                }

                if (TempLocate == StopModLength)
                {
                    isRootRead = true;
                }
            }
            else
            {
               // initialAll();
                TempLocate = 0;
                Po = root;

                isRootRead = false;
                root.OpenNode();
                root.RealValue = TempNum;
                TempRootValue = root.RealValue;
                root.EditLocation(0);

                TempLocate++;
                isContainu = false;

            }

        }

        //02
        private List<MakeTreeUniq03Node> ListNodMod3A = new List<MakeTreeUniq03Node>();
        private List<MakeTreeUniq03Node> ListNodMod3B = new List<MakeTreeUniq03Node>();
        private List<MakeTreeUniq03Node> TempListNodMod3C;
        private int TempNum = 0;
        private void WriteFirst_W5()
        {
            Po = root;
            ListNodMod3A.Clear();
            ListNodMod3B.Clear();

            #region InitialRoot

            root.OpenNode();
            //       TempOfStopLength++;
            if (root.nextzero == null)
            {
                root.nextzero = new MakeTreeUniq03Node(false, false, 1);
                root.nextzero.OpenNode();
                // TempOfStopLength++;
            }
            else
            {
                root.nextzero.OpenNode();
                //  TempOfStopLength
            }

            if (root.nextone == null)
            {
                root.nextone = new MakeTreeUniq03Node(true, false, 1);
                root.nextone.OpenNode();
                //   TempOfStopLength++;
            }
            else
            {
                root.nextone.OpenNode();
                //   TempOfStopLength++;
            }

            ListNodMod3A.Add(root.nextzero);
            ListNodMod3A.Add(root.nextone);

            #endregion

            while (ListNodMod3A.Count > 0)
            {
                #region Fill List
                foreach (MakeTreeUniq03Node cr in ListNodMod3A)
                {
                    #region GetKeyNum

                    if (cr.nextzero != null)
                    {
                        if (cr.nextzero.isOpen)
                        {
                            if (cr.nextone != null)
                            {
                                if (cr.nextone.isOpen)
                                {
                                    TempNum = 3;
                                }
                                else
                                {
                                    TempNum = 2;
                                }
                            }
                            else
                            {
                                TempNum = 2;
                            }
                        }
                        else
                        {
                            if (cr.nextone != null)
                            {
                                if (cr.nextone.isOpen)
                                {
                                    TempNum = 1;
                                }
                                else
                                {
                                    TempNum = 0;
                                }
                            }
                            else
                            {
                                TempNum = 0;
                            }
                        }
                    }
                    else
                    {
                        if (cr.nextone != null)
                        {
                            if (cr.nextone.isOpen)
                            {
                                TempNum = 1;
                            }
                            else
                            {
                                TempNum = 0;
                            }
                        }
                        else
                        {
                            TempNum = 0;
                        }
                    }


                    #endregion

                    
                    {
                        WriterNumByMod.WriteNumber(4, TempNum);

                        if (TempNum == 2)
                        {

                            ListNodMod3B.Add(cr.nextzero);

                        }
                        else
                        {
                            if (TempNum == 1)
                            {
                                //isLess For 1

                                ListNodMod3B.Add(cr.nextone);

                            }
                            else
                            {
                                //isLess For 0 & 1 => 0
                                if (TempNum == 3)
                                {

                                    ListNodMod3B.Add(cr.nextzero);


                                    ListNodMod3B.Add(cr.nextone);

                                }
                                else
                                {
                                    //isLess For Null & Null 
                                }
                            }
                        }
                    }

                }

                #endregion


                ListNodMod3A.Clear();
                TempListNodMod3C = ListNodMod3A;
                ListNodMod3A = ListNodMod3B;
                ListNodMod3B = TempListNodMod3C;
            }

        }

        //03
        private MakeTreeUniq03Node[] ListNodes;
        private void InitialListNodes(MakeTreeUniq03Node cr)
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
        private void InitialListNodes()
        {
            ListNodes = new MakeTreeUniq03Node[StopModLength];
            InitialListNodes(root);
        }

        //04
        private List<MakeTreeUniq03Node> PropList = new List<MakeTreeUniq03Node>();
        private int TempLocatNum = 0;
        private void GetLocatePropList()
        {
            PropList.Clear();
            Po = root;
            PropList.Add(Po.nextzero);
            PropList.Add(Po.nextone);

            StopModLength = ListNodes.Length;

            for (int i = 1; i != StopModLength; i++)
            {
                //temp

                if (PropList.Count < 1)
                    return;

                Po = ListNodes[i];

                //GetLocate
                {
                    TempLocatNum = 0;
                    foreach (MakeTreeUniq03Node nod in PropList)
                    {
                        if (Po == nod)
                        {
                            break;
                        }

                        TempLocatNum++;
                    }
                }

                if (PropList.Count > 1)
                    WriterNumByMod.WriteNumber(PropList.Count, TempLocatNum);
                else
                {
                    TempLocatNum = 0;
                }
                
                Po = PropList[TempLocatNum];


                if (Po.nextzero != null)
                {
                    if (Po.nextzero.isOpen)
                        PropList.Add(Po.nextzero);
                }

                if (Po.nextone != null)
                {
                    if (Po.nextone.isOpen)
                        PropList.Add(Po.nextone);
                }


                PropList.RemoveAt(TempLocatNum);
            }


            //initialAll();
            isRootRead = true;

        }
       
        #endregion

        #endregion


    }



    class MakeTreeUniq03
    {
        public StringBuilder Report;
        private int ModLength = 256;
        private int ModLevel = 4;

        private string Extension = "MTU03ML";
        private string DeExtension = "DeMTU03ML";


        public MakeTreeUniq03()
        {

        }
        public MakeTreeUniq03(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }
        public MakeTreeUniq03(int ModLengthNumber , int LevelMod)
        {
            ModLevel = LevelMod;
            ModLength = ModLengthNumber;
        }


        public void StartMakeTreeUniq_W01(int LevelMod)
        {



            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNumMod02 WriterByMod = new ReaderWriterOneNumMod02(false, 256, ref WriterBits);

            MakeTreeUniq03Tree Tree = new MakeTreeUniq03Tree(ModLength, ref ReaderBits, LevelMod);
            Tree.ReadAll();

          //  WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeUniq_W05()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";
            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W05");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";
            ReaderWriterOneNumMod02 WriterByMod = new ReaderWriterOneNumMod02(false, 5, ref WriterBits);



            MakeTreeUniq03Tree Tree = new MakeTreeUniq03Tree(ModLength, ref ReaderBits , 4);
            List<int> UniqList=new List<int>();


            int TempModLength = 0;

            while (ReaderBits.isAbleRead)
            {
               UniqList = Tree.ReadOneToGetList();

               TempModLength = UniqList.Count;
                 WriterByMod.WriteNumber(16777216, TempModLength);

            //   WriterByMod.WriteNumber(16000000, TempModLength);

               foreach (int num in UniqList)
               {
                   WriterByMod.WriteNumber(TempModLength, num);
               }

            }




              WriterBits.CloseFile();
              ReaderBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_W05()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W05");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNumMod02 ReaderByMod = new ReaderWriterOneNumMod02(true, 5, ref ReaderBits);



            MakeTreeDeUniq03Tree Tree = new MakeTreeDeUniq03Tree(ModLength, ref WriterBits);


          //  List<int> TempUniq = new List<int>();
            int TempModLenth = 0;
            while (ReaderBits.isAbleRead)
            {
                TempModLenth = ReaderByMod.GetOneNumber(16777216);

                Tree.ReadNumber_W5(ref ReaderByMod, TempModLenth);
            }

            ReaderBits.CloseFile();
            WriterBits.CloseFile();

        }

        public void StartMakeTreeUniq_W06()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";
            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W06");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";
            ReaderWriterOneNumMod02 WriterByMod = new ReaderWriterOneNumMod02(false, 256, ref WriterBits);



            MakeTreeUniq03Tree Tree = new MakeTreeUniq03Tree(ModLength, ref ReaderBits, 4);
            List<int> UniqList = new List<int>();


            int TempIndex = 0;
            int MiddleNumber = ModLength / 2 ;

            while (ReaderBits.isAbleRead)
            {
                UniqList = Tree.ReadOneToGetList();
                TempIndex = UniqList.IndexOf(UniqList.Count - 1);
                UniqList[TempIndex] = MiddleNumber;
                UniqList.RemoveAt(0);


                foreach (int num in UniqList)
                {
                    WriterByMod.WriteNumber(ModLength, num);
                }

            }




            WriterBits.CloseFile();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_W06()
        {
            //This W06 Adds to root Middle Number


            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W06");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNumMod02 ReaderByMod = new ReaderWriterOneNumMod02(true, 256, ref ReaderBits);



            MakeTreeDeUniq03Tree Tree = new MakeTreeDeUniq03Tree(ModLength, ref WriterBits);


            //  List<int> TempUniq = new List<int>();
            
            while (ReaderBits.isAbleRead)
            {
                Tree.ReadNumber_W6(ref ReaderByMod, ModLength);
            }

            ReaderBits.CloseFile();
            WriterBits.CloseFile();

        }

    }
}
