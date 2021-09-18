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
using System.Windows.Forms;

namespace Comp1.MakeTreeUniq
{
    class MakeTreeUniq01Node
    {
        public MakeTreeUniq01Node nextzero;
        public MakeTreeUniq01Node nextone;
        public MakeTreeUniq01Node Refrance;
        public int Value;
        public bool PastBit = false;
        public bool Continu;

        public MakeTreeUniq01Node Tempnextzero;
        public MakeTreeUniq01Node Tempnextone;
        public MakeTreeUniq01Node TempRefrance;
        public int TempValue;
        public bool TempPastBit = false;

        public bool TempContinu;

        public bool Stoping=false;
        public bool TempStoping=false;

        public MakeTreeUniq01Node()
        {
            TempValue = 0;
            Value = 0;
            Continu = false;
            TempContinu = false;
            nextzero = null;
            nextone = null;
        }
        public MakeTreeUniq01Node(int NumValue)
        {
            TempValue = 0;
          //  Value = 0;
            Continu = false;
            TempContinu = false;
            nextzero = null;
            nextone = null;

            Value = NumValue;
        }
       public MakeTreeUniq01Node(bool Pastbit)
        {
            TempValue = 0;
            Value = 0;
            Continu = false;
            TempContinu = false;
            nextzero = null;
            nextone = null;

           PastBit=Pastbit;
        }



        public void InitialNode()
        {
            Tempnextzero = nextzero;
            Tempnextone = nextone;
            TempRefrance = Refrance;

            TempValue = Value;
            TempPastBit = PastBit;
            TempContinu = Continu;

            TempStoping=Stoping;
        }

    }

    class MakeTreeUniq01Tree
    {

        #region  Properties 

        public MakeTreeUniq01Node root;
        private int NumberNodes = 256;
        private int RestNodesNum = 0;
        private  int ModLevel = 8;

        public List<MakeTreeUniq01Node> ListNumber;

       #endregion

        #region OverLoad

        public MakeTreeUniq01Tree()
        {
            Creat();
        }
        public MakeTreeUniq01Tree(int ModNumLength)
        {
            NumberNodes = ModNumLength;
            Creat();
        }


        #endregion

        #region Create

        private void MainModify()
        {
            

            int count = 0;
            int Nodes = 0;
            while (count!=24)
            {
                Nodes = Convert.ToInt32(Math.Pow(2, count));
                if (Nodes < NumberNodes)
                {
                    count++;
                }
                else
                {
                    if (Nodes == NumberNodes)
                    {
                        ModLevel = count;
                        RestNodesNum = 0;

                        break;
                    }
                    else
                    {
                        count--;

                        Nodes = Convert.ToInt32(Math.Pow(2, count));
                        RestNodesNum = NumberNodes - Nodes;

                        ModLevel = count;
                        break;
                    }

                }
            }
            

        }
        private void Creat()
        {
            root = new MakeTreeUniq01Node(false);
            MainModify();
            ListNumber = new List<MakeTreeUniq01Node>(NumberNodes);

            for (int i = 0; i != NumberNodes; i++)
            {
                ListNumber.Add(new MakeTreeUniq01Node());
            }

            root = new MakeTreeUniq01Node();

            for (int n = 1; n != ModLevel; n++)
            {
                creatLevel(this.root);
            }
            creatLastLevel(this.root);
            creatRestLevel(this.root);


            MainInitial();

        }

        private void creatLevel(MakeTreeUniq01Node cr)
        {
            if (cr.nextzero != null)
            {
                creatLevel(cr.nextzero);
                creatLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = new MakeTreeUniq01Node();

                cr.nextone = new MakeTreeUniq01Node();

            }
        }
        private void creatLastLevel(MakeTreeUniq01Node cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, ModLevel));
            MakeTreeUniq01Node po = new MakeTreeUniq01Node();
            while (i != Timer)
            {

                BitArray bitNum = BitArrayOperation.intvaluToBitsArr(i, ModLevel);

                po.nextzero = root;

                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextzero.nextone == null)
                        {
                            po.nextzero.nextone = new MakeTreeUniq01Node(true);

                            po.nextzero = po.nextzero.nextone;
                        }
                        else
                        {
                            po.nextzero.nextone.PastBit=true;
                            po.nextzero = po.nextzero.nextone;
                        }
                    }
                    else
                    {
                        if (po.nextzero.nextzero == null)
                        {
                            po.nextzero.nextzero = new MakeTreeUniq01Node(false);

                            po.nextzero = po.nextzero.nextzero;
                        }
                        else
                        {
                             po.nextzero.nextzero.PastBit=false;
                            po.nextzero = po.nextzero.nextzero;
                        }
                    }



                }

                po.nextzero.Value = i;
                po.nextzero.Stoping = true;
             

                ListNumber[po.nextzero.Value] = po.nextzero;

                i++;
            }
        }

        private void creatRestLevel(MakeTreeUniq01Node cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, ModLevel));
            MakeTreeUniq01Node po;
            po = root;
            BitArray bitNum;
            while (i != RestNodesNum)
            {
                bitNum = BitArrayOperation.intvaluToBitsArr(i, ModLevel);

                po = root;
                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextone == null)
                        {
                            po.nextone = new MakeTreeUniq01Node(true);

                            po = po.nextone;
                        }
                        else
                        {
                            po.nextone.PastBit=true;
                            po = po.nextone;
                        }
                    }
                    else
                    {
                        if (po.nextzero == null)
                        {
                            po.nextzero = new MakeTreeUniq01Node(false);

                            po = po.nextzero;
                        }
                        else
                        {
                            po.nextzero.PastBit=false;
                            po = po.nextzero;
                        }
                    }
                }


                //Edit
                {
                    //Past
                    {
                        po.nextzero = new MakeTreeUniq01Node(po.Value);
                        po.nextzero.Stoping = true;

                        po.nextzero.PastBit=false;

                        
                        ListNumber[po.nextzero.Value] = po.nextzero;
                    }

                    //One
                    {
                        po.nextone = new MakeTreeUniq01Node(Timer);
                        po.nextone.Stoping = true;

                        po.nextone.PastBit=true;;

                        
                        ListNumber[po.nextone.Value] = po.nextone;
                    }

                    po.Value = 0;
                    po.Stoping = false;
                }
                Timer++;
                i++;
            }
        }



       #endregion

        #region Initial

        private void MainInitial()
        {
            initialRefrance(root);
            root.Refrance = null;
            initialAll(root);
            
        }
        private void initialRefrance(MakeTreeUniq01Node cr)
        {

            if (cr.nextzero != null && cr.nextone != null)
            {
                cr.nextzero.Refrance = cr;
                cr.nextone.Refrance = cr;

                initialRefrance(cr.nextzero);
                initialRefrance(cr.nextone);
            }
            else
            {
                cr.nextzero = null;
                cr.nextone = null;
            }
        }

        public void initialAll(MakeTreeUniq01Node cr)
        {
            if (cr.nextzero != null && cr.nextone != null)
            {
                cr.InitialNode();

                initialAll(cr.nextzero);
                initialAll(cr.nextone);
            }
            else
            {
                cr.InitialNode();
            }
           // root.Refrance = null;
        }

        public void initialAll()
        {
            initialAll(root);
        }

        #endregion


    }


    class MakeTreeUniq01Oper
    {
        #region Proprties

        public StringBuilder sb;
        public StringBuilder RePort;
        private byte[] BytNum = new byte[256];

        //Temp

        private int ModLength = 256;
        private int ModNum = 0;

        private int LengthStop = 256;
        private int NumOfOutNumber = 0;

        private MakeTreeUniq01Tree Tree;
        private MakeTreeUniq01Node root;
        private MakeTreeUniq01Node po;
        private MakeTreeUniq01Node TempRatharPo;
        private MakeTreeUniq01Node Sor;



       #endregion

        #region OverLoad
        
        public MakeTreeUniq01Oper()
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            MakeTree();

        }
        public MakeTreeUniq01Oper(int LengthMod)
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            ModLength = LengthMod;
            MakeTree();

        }

       #endregion

        #region Info

        private int TempReadBits = 0;
        private int SumTempReadBits = 0;
        private int MakeInitialTimes = 0;

        public List<int> ListAdditionBitsinfo = new List<int>();



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

        #region Create

        private void MakeTree()
        {

            Tree = new MakeTreeUniq01Tree(ModLength);
            root = Tree.root;
            po = root;
            TempRatharPo = new MakeTreeUniq01Node();

            LengthStop = ModLength - 2; ;





            Sor = new MakeTreeUniq01Node();


            //info
            TempReadBits = 0;
            NumOfOutNumber = 0;


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

        #region  ReadByteArray

        public List<int> MakeUniqOnly_int(byte[] DataByte)
        {
            BitArray BitsArr = new BitArray(DataByte);
            List<int> TempIntList = new List<int>();

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {
                            TempIntList.Add(po.TempValue);
                            TempIntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            ////Exmine 01
                            //if (po.TempRefrance.Tempnextone.Continu == false)
                            //    MessageBox.Show("Error (Exmine 01) ! ");

                            //


                            //   InitialTree();
                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();

                        }
                        else
                        {
                            TempIntList.Add(po.TempValue);
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {
                            TempIntList.Add(po.TempValue);
                            TempIntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            ////Exmine 01
                            //if (po.TempRefrance.Tempnextzero.Continu == false)
                            //    MessageBox.Show("Error (Exmine 01) ! ");


                            //

                            //   InitialTree();
                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();



                        }
                        else
                        {

                            TempIntList.Add(po.TempValue);
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach



            return TempIntList;
        }
        public void MakeUniqOnly_int(ref ReaderWriteFileBits02B ReaderBits , ref ReaderWriterOneNum02B WriterNum)
        {
            bool b = false;
            while(ReaderBits.isAbleRead)
            {
                b = ReaderBits.GetBit();

                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {

                            WriterNum.WriteNumber(po.TempValue);
                            WriterNum.WriteNumber(po.TempRefrance.Tempnextone.TempValue);

                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            SumTempReadBits = SumTempReadBits + TempReadBits;
                            MakeInitialTimes++;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();

                        }
                        else
                        {
                            WriterNum.WriteNumber(po.TempValue);
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {
                            WriterNum.WriteNumber(po.TempValue);
                            WriterNum.WriteNumber(po.TempRefrance.Tempnextzero.TempValue);


                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            SumTempReadBits = SumTempReadBits + TempReadBits;
                            MakeInitialTimes++;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();



                        }
                        else
                        {

                            WriterNum.WriteNumber(po.TempValue);
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach



           
        }

        public List<int> MakeUniqOnly_ByStop(ref ReaderWriteFileBits02B ReaderBits ,int StopOut)
        {
            int OutCounter = 0;
            List<int> TempListAdd = new List<int>();

            bool b = false;
            while (ReaderBits.isAbleRead)
            {
                b = ReaderBits.GetBit();

                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {

                            TempListAdd.Add(po.TempValue); OutCounter++;
                            TempListAdd.Add(po.TempRefrance.Tempnextone.TempValue); OutCounter++;

                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            SumTempReadBits = SumTempReadBits + TempReadBits;
                            MakeInitialTimes++;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();

                        }
                        else
                        {
                            TempListAdd.Add(po.TempValue); OutCounter++;
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }
                        if (OutCounter == StopOut)
                            break;

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.TempStoping == true)
                    {
                        if (NumOfOutNumber == LengthStop)
                        {
                            TempListAdd.Add(po.TempValue); OutCounter++;
                            TempListAdd.Add(po.TempRefrance.Tempnextzero.TempValue); OutCounter++;


                            Tree.initialAll();
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            NumOfOutNumber = 0;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            SumTempReadBits = SumTempReadBits + TempReadBits;
                            MakeInitialTimes++;


                            //AllIntlist.Add(TempIntList);
                            //TempIntList = new List<int>();



                        }
                        else
                        {

                            TempListAdd.Add(po.TempValue); OutCounter++;
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }

                        if (OutCounter == StopOut)
                            break;
                            
                    }

                }//End if true or false


            }//End foreach





            SegmentNum++;
            GetSegmentInfo(ReaderBits.SumOfReadBits);

            return TempListAdd;
        }

        public List<int> MakeUniqOnly_ByStop_Byhalf(ref ReaderWriteFileBits02B ReaderBits, int StopOut)
        {
            int OutCounter = 0;
            List<int> TempListAdd = new List<int>();

            bool b = false;
            while (ReaderBits.isAbleRead)
            {
                b = ReaderBits.GetBit();

                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempStoping == true)
                    {
                        
                            TempListAdd.Add(po.TempValue); OutCounter++;
                            NumOfOutNumber++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        
                        if (OutCounter == StopOut)
                            break;

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.TempStoping == true)
                    {

                        TempListAdd.Add(po.TempValue); OutCounter++;
                        NumOfOutNumber++;

                        if (po.TempRefrance.TempRefrance == null)
                        {
                            root = po.TempRefrance.Tempnextzero;
                            root.TempRefrance = null;
                        }
                        else
                        {
                            po = po.TempRefrance;
                            TempRatharPo = po.Tempnextzero;

                            TempRatharPo.TempRefrance = po.TempRefrance;

                            if (po.TempRefrance.Tempnextzero == po)
                            {
                                po.TempRefrance.Tempnextzero = TempRatharPo;
                            }
                            else
                            {
                                po.TempRefrance.Tempnextone = TempRatharPo;
                            }
                        }

                        po = root;




                        if (OutCounter == StopOut)
                            break;

                    }

                }//End if true or false


            }//End foreach


            //Initial
            {
                    Tree.initialAll();
                    root = Tree.root;
                    Tree.root.TempRefrance = null;
                    po = root;

                    NumOfOutNumber = 0;

                    //Info
                  //  ListAdditionBitsinfo.Add(TempReadBits);
                    TempReadBits = 0;
                    SumTempReadBits = SumTempReadBits + TempReadBits;
                    MakeInitialTimes++;



            }




            SegmentNum++;
            GetSegmentInfo(ReaderBits.SumOfReadBits);

            return TempListAdd;
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


    class MakeTreeDeUniq01Oper
    {
        #region Proprties

        public StringBuilder sb;
        public StringBuilder RePort;
        private byte[] BytNum = new byte[256];

        //Temp

        private int ModLength = 256;

        private int LengthStop = 256;
        private int NumOfOutNumber = 0;

        private MakeTreeUniq01Tree Tree;
        private MakeTreeUniq01Node root = new MakeTreeUniq01Node();
        private MakeTreeUniq01Node po;
        private MakeTreeUniq01Node TempRatharPo;
        private MakeTreeUniq01Node ReadBitsPo;



        #endregion

        #region OverLoad

        public MakeTreeDeUniq01Oper()
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            MakeTree();

        }
        public MakeTreeDeUniq01Oper(int LengthMod)
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            ModLength = LengthMod;
            MakeTree();

        }

        #endregion

        #region Info

        private int TempReadNum = 0;
        public List<int> ListAdditionBitsinfo = new List<int>();

         private bool ContainueMessage = true;

        #endregion

        #region Create

        private void MakeTree()
        {

            Tree = new MakeTreeUniq01Tree(ModLength);
            root = Tree.root;
            po = root;
            TempRatharPo = new MakeTreeUniq01Node();

            LengthStop = ModLength - 2; ;


            //info
            TempReadNum = 0;
            NumOfOutNumber = 0;
        }

        #endregion

        #region  ReadByteArray

        private List<bool> GetListBit()
        {
            TempReadNum++;
            List<bool> AddBits = new List<bool>();

            po = ReadBitsPo;

            while (ReadBitsPo.TempRefrance != null)
            {
                AddBits.Add(ReadBitsPo.TempPastBit);
                ReadBitsPo = ReadBitsPo.TempRefrance;
            }

            return AddBits;
        }
        private void ReadTree(ref List<bool> TempList , int Num)
        {
            po = root;
            bool b =false;
            for (int i = TempList.Count - 1; i != -1; i--)
            {
            b= TempList[i];
            
                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempStoping == true)
                    {
                        if (po.TempValue != Num)
                        {
                            //Exmine 01
                          //  if (po.TempRefrance.Tempnextone.Continu == false)
                            if (ContainueMessage)
                            {
                                if ((System.Windows.Forms.MessageBox.Show("Error (Exmine 01) ! " + "\nTempReadNum = " + TempReadNum.ToString(), "InfoCounters", System.Windows.Forms.MessageBoxButtons.OKCancel)) == System.Windows.Forms.DialogResult.OK)
                                {

                                }
                                else
                                {
                                    ContainueMessage = false;
                                }
                            }
             

                        }
                       // else
                        {
                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    TempRatharPo.TempPastBit = false;
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    TempRatharPo.TempPastBit = true;
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;

                    if (po.TempStoping == true)
                    {
                        if (po.TempValue != Num)
                        {
                            //Exmine 01
                            //if (po.TempRefrance.Tempnextzero.Continu == false)

                            if (ContainueMessage)
                            {
                                if ((System.Windows.Forms.MessageBox.Show("Error (Exmine 02) ! " + "\nTempReadNum = " + TempReadNum.ToString(), "InfoCounters", System.Windows.Forms.MessageBoxButtons.OKCancel)) == System.Windows.Forms.DialogResult.OK)
                                {

                                }
                                else
                                {
                                    ContainueMessage = false;
                                }
                            }
                        }
                      //  else
                        {
                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    TempRatharPo.TempPastBit = false;
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    TempRatharPo.TempPastBit = true;
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;

                        }


                    }

                }//End if true or false


            }//End for

        }

        public void MakeDeUniqOnly_int(ref ReaderWriterOneNum02B ReaderNum, ref ReaderWriteFileBits02B WriterBits)
        {
            int TempNum = 0;
            List<bool> TempList;

            while (ReaderNum.isAbleRead)
            {
                TempNum = ReaderNum.GetOneNumber();
                

                if (NumOfOutNumber == LengthStop)
                {

                    TempReadNum++;
                    TempReadNum++;
                   


                    WriterBits.WriteBit(Tree.ListNumber[TempNum].TempPastBit);

                    //   InitialTree();
                    Tree.initialAll();
                    root = Tree.root;
                    Tree.root.TempRefrance = null;
                    po = root;

                    NumOfOutNumber = 0;

                    TempNum = ReaderNum.GetOneNumber();
                }
                else
                {
                    NumOfOutNumber++;

                    ReadBitsPo = Tree.ListNumber[TempNum];
                    TempList = GetListBit();

                    for (int i = TempList.Count - 1; i != -1; i--)
                    {
                        WriterBits.WriteBit(TempList[i]);
                    }


                    //ForCheck
                    {
                        ReadTree(ref TempList, TempNum);
                    }



                }//End for


            }
        }

        public void MakeDeUniqOnly_int(ref List<int> UniqListNum, ref ReaderWriteFileBits02B WriterBits)
        {
            
            List<bool> TempList;

            foreach (int TempNum in UniqListNum)
            {
             
                if (NumOfOutNumber == LengthStop)
                {

                    TempReadNum++;
                    TempReadNum++;



                    WriterBits.WriteBit(Tree.ListNumber[TempNum].TempPastBit);

                    //   InitialTree();
                    Tree.initialAll();
                    root = Tree.root;
                    Tree.root.TempRefrance = null;
                    po = root;

                    NumOfOutNumber = 0;

                    break;
                   // TempNum = ReaderNum.GetOneNumber();
                }
                else
                {
                    NumOfOutNumber++;

                    ReadBitsPo = Tree.ListNumber[TempNum];
                    TempList = GetListBit();

                    for (int i = TempList.Count - 1; i != -1; i--)
                    {
                        WriterBits.WriteBit(TempList[i]);
                    }


                    //ForCheck
                    {
                        ReadTree(ref TempList, TempNum);
                    }



                }//End for


            }
        }

        public void MakeDeUniqOnly_int_Byhalf(ref List<int> UniqListNum, ref ReaderWriteFileBits02B WriterBits)
        {

            List<bool> TempList;

            foreach (int TempNum in UniqListNum)
            {

                NumOfOutNumber++;

                ReadBitsPo = Tree.ListNumber[TempNum];
                TempList = GetListBit();

                for (int i = TempList.Count - 1; i != -1; i--)
                {
                    WriterBits.WriteBit(TempList[i]);
                }


                //ForCheck
                {
                    ReadTree(ref TempList, TempNum);
                }



            }


            //End for

            {

                TempReadNum++;
                TempReadNum++;

                //   InitialTree();
                Tree.initialAll();
                root = Tree.root;
                Tree.root.TempRefrance = null;
                po = root;

                NumOfOutNumber = 0;


                // TempNum = ReaderNum.GetOneNumber();
            }



        }




        #endregion

    }


    class MakeTreeUniq01
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTU01ML";
        private string DeExtension = "DeMTU01ML";


        public MakeTreeUniq01()
        {

        }
        public MakeTreeUniq01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }
        

        public void StartMakeTreeUniq_FileUniqW01()
        {

            MakeTreeUniq01Oper Tree = new MakeTreeUniq01Oper(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
         




            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + "BitKey" + "W01");


            int DataLengthStop = ReaderNum.GetStopNumLength;



            int First = 0;
            // int Secound = 0;
            bool isFirst = true;
            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                //    if (isFirst)
                //    {
                //        if (n == FirstChangerNum || n == SecoundChangerNum)
                //        {
                //            if (n == FirstChangerNum)
                //            {
                //                WriterBits.WriteBit(false);
                //            }
                //            else
                //            {
                //                WriterBits.WriteBit(true);
                //            }

                //            First = SecoundChangerNum;
                //        }
                //        else
                //        {
                //            First = n;
                //        }

                //        isFirst = false;
                //    }
                //    else
                //    {
                //        Tree.NumberList[First].NextList[n].Write();
                //        isFirst = true;
                //    }
                }


            }

            ReaderNum.End();
            WriterNum.End();
            WriterBits.CloseFile();

        }
        public void StartMakeTreeUniq_FileUniqW01B()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MakeTreeUniq01Oper Tree = new MakeTreeUniq01Oper(ModLength);

            while (ReaderBits.isAbleRead)
            {
                foreach (int n in Tree.MakeUniqOnly_ByStop(ref ReaderBits , ModLength))
                {
                    WriterNum.WriteNumber(n);
                }
            }


            WriterNum.End();
            ReaderBits.CloseFile();

        }
       
        public void StartMakeTreeUniq_FileUniqW02()
        {


            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);

            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W02");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MakeTreeUniq01Oper TreeOper = new MakeTreeUniq01Oper(ModLength);

            TreeOper.MakeUniqOnly_int(ref ReaderBits, ref WriterNum);

           
            WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_FileUniqW02()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W02");

            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            MakeTreeDeUniq01Oper TreeOper = new MakeTreeDeUniq01Oper(ModLength);

            TreeOper.MakeDeUniqOnly_int(ref ReaderNum ,ref  WriterBits);


            ReaderNum.End();
            WriterBits.CloseFile();

        }
        public void StartMakeTreeUniq_FileUniqW02_ByHalf()
        {
            int HalfModLebgth = ModLength * 2;

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);

            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, HalfModLebgth, Extension + ModLength.ToString() + "W02ByHalf");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MakeTreeUniq01Oper TreeOper = new MakeTreeUniq01Oper(HalfModLebgth);

            while (ReaderBits.isAbleRead)
            {
                foreach (int n in TreeOper.MakeUniqOnly_ByStop_Byhalf(ref ReaderBits, ModLength))
                {
                    WriterNum.WriteNumber(n);
                }
            }
          

            WriterNum.End();
            ReaderBits.CloseFile();

        }

        public void StartMakeTreeDeUniq_FileUniqW02_ByHalf()
        {
            int HalfModLebgth = ModLength * 2;

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, HalfModLebgth);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W02ByHalf");

            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            MakeTreeDeUniq01Oper TreeOper = new MakeTreeDeUniq01Oper(HalfModLebgth);

            List< int > UnqList=new List<int>();
            while (ReaderNum.isAbleRead)
            {
                UnqList.Clear();
                UnqList = ReaderNum.GetManyNum(ModLength) ;
                TreeOper.MakeDeUniqOnly_int_Byhalf(ref UnqList, ref WriterBits);
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



            MakeTreeUniq01Oper Tree = new MakeTreeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

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

                //Oper == 10
                TempUniqList = Tree.MakeUniqOnly_ByStop(ref ReaderBits,ModLength);
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 10;

                
                //Oper == 12W1 == 12
                Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 12, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 12W1 == 18
                Tree2W2.ReadNumber_W2Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 18, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 13W1 == 135
                Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 135, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 13W1 == 136
                Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 136, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 14
                Tree4.ReadNumber_W1(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 14, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //Oper == 15
                Tree5.GetOneReadList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 15, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //Oper == 16  Sort
                TreeSort1.SortList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 16, WriterBitsInfo.SumOfWriteBits);
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
        public void StartMakeTreeUniq_FileUniqW01Info2_ByHalf()
        {
            int TempModLength = ModLength * 2;


            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";


            ReaderWriteFileBits02B WriterBitsInfo = new ReaderWriteFileBits02B(false, Extension + TempModLength.ToString() + "W01");
            if (WriterBitsInfo.GetIsCancel)
                return;// "isCancel";
            WriterBitsInfo.WriteBitToFile = false;




            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00(Path.ChangeExtension(WriterBitsInfo.GetSavePath, Extension + "Info2_ByHalf"), false);




            MakeTreeUniq01Oper Tree = new MakeTreeUniq01Oper(TempModLength);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);
          
            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq05Tree Tree5_ByHalf = new MakeTreeDeUniq05Tree(TempModLength, ref WriterBitsInfo);

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);

            List<int> TempUniqList = new List<int>();
            List<int> TempHalfUniqList = new List<int>();
            int[] TempListNumberForhalf = new int[TempModLength];
            BitArray TempBitsArr = new BitArray(TempModLength, false);

            int CTforHalf = 0;
            int CTi = 0;
            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;
            int i = 0;
            int Rest = 0;
            while (ReaderBits.isAbleRead)
            {
                //ReaderOrignal
                {
                    //Oper == 00
                    TempHalfUniqList = Tree.MakeUniqOnly_ByStop_Byhalf(ref ReaderBits , ModLength);
                    MainSegmentInfo = Tree.SegmentInfoNod;
                    MainSegmentInfo.OperationId = 10;


                    //Oper == 12
                    Tree5_ByHalf.GetOneReadList_ByHalf(ref TempHalfUniqList);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 52, WriterBitsInfo.SumOfWriteBits);
                    WriterBitsInfo.SumOfWriteBits = 0;
                    WriterNodeInfo.WriteNod(ref SegmentInfo);


                    //ForUniqData
                    {
                        TempBitsArr.SetAll(false);
                        foreach (int num in TempHalfUniqList)
                        {
                            TempBitsArr[num] = true;

                        }
                        CTforHalf = 0;//Read
                        CTi = 0;
                        foreach (bool b in TempBitsArr)
                        {
                            if (b)
                            {
                                TempListNumberForhalf[CTi] = CTforHalf;
                                CTforHalf++;
                            }

                            CTi++;
                        }


                        //Add
                        TempUniqList.Clear();

                        foreach (int num in TempHalfUniqList)
                        {
                            TempUniqList.Add(TempListNumberForhalf[num]);

                        }

                    }
                    //W01
                   //  MainSegmentInfo.HowReadBits = MainSegmentInfo.HowReadBits - TempModLength;
                    //W02
                    MainSegmentInfo.HowReadBits = MainSegmentInfo.HowReadBits - ((TempModLength + ModLength) / 2);

                    TempHalfUniqList.Clear();
                }

                //Oper == 11
                Tree1.MakeDeUniqOnly_int(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 11, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 21W1 == 21
                Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 21, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 52W1 == 58
                Tree2W2.ReadNumber_W2Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 28, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 53W1 == 135
                Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 35, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 53W1 == 136
                Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 36, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 41
                Tree4.ReadNumber_W1(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 41, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 51
                Tree5.GetOneReadList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);


                //Oper == 56  Sort
                TreeSort1.SortList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 100, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //ForReast
                {
                    //Rest = MainSegmentInfo.ModSegmentLength - MainSegmentInfo.HowReadBits;
                    //if (Rest > 0 && Rest < MainSegmentInfo.ModSegmentLength)
                    //{
                    //    for (i = 0; i != Rest; i++)
                    //        ReaderBits.GetBit();

                    //}
                    //else
                    //{
                    //    //Error!!
                    //}

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
