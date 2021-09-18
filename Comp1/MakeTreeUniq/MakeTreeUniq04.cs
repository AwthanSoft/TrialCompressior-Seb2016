using Comp1.MergeSort.MakeTreeMergeSort;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.MakeTreeUniq
{

    class MakeTreeUniq04Node
    {
        public MakeTreeUniq04Node nextzero;
        public MakeTreeUniq04Node nextone;
        public bool PastBit;
        public int RealLocation;
        public int RealValue;


        public bool isOpen;
      //  public bool isInPropList;

        public MakeTreeUniq04Node()
        {
            RealValue = 0;
            RealLocation = 0;

            isOpen = false;
            PastBit = false;

          //  isInPropList = false;

            nextzero = null;
            nextone = null;
        }
        //public MakeTreeUniq04Node(bool pastBit)
        //{
        //    RealValue = 0;
        //    RealLocation = 0;

        //    isOpen = false;
        //    PastBit = pastBit;

        //    isModLevel = false;
        //    NodeLevel = 0;

        //    nextzero = null;
        //    nextone = null;
        //}
        //public MakeTreeUniq04Node(bool pastBit, int Level)
        //{
        //    RealValue = 0;
        //    RealLocation = 0;

        //    isOpen = false;
        //    PastBit = pastBit;

        //    isModLevel = false;
        //    NodeLevel = Level;

        //    nextzero = null;
        //    nextone = null;
        //}
        //public MakeTreeUniq04Node(bool pastBit, bool isLevelMod, int Level)
        //{
        //    RealValue = 0;
        //    RealLocation = 0;

        //    isOpen = false;
        //    PastBit = pastBit;

        //    isModLevel = isLevelMod;
        //    NodeLevel = Level;

        //    nextzero = null;
        //    nextone = null;
        //}
        //public MakeTreeUniq04Node(bool pastBit, bool isLevelMod)
        //{
        //    RealValue = 0;
        //    RealLocation = 0;

        //    isOpen = false;
        //    PastBit = pastBit;

        //    isModLevel = isLevelMod;
        //    NodeLevel = 0;

        //    nextzero = null;
        //    nextone = null;
        //}

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

    class MakeTreeUniq04Tree
    {

        #region  Properties

        public MakeTreeUniq04Node root;
        public MakeTreeUniq04Node Po;

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        private ReaderWriteFileBits02B ReaderBit;
        private MakeTreeUniq04Node[] ListNodes;
        private ReaderWriterOneNumMod02 ReaderNumByMod;
        private int LengthModListReader = 256;

        #endregion

        #region OverLoad

        public MakeTreeUniq04Tree()
        {
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq04Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq04Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            ReaderBit = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq04Node();
            //   ListNodes = new MakeTreeUniq04Node[ModLength];

            Po = root;
            ReaderNumByMod = new ReaderWriterOneNumMod02(true, LengthModListReader, ref ReaderBit);



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

        public void initialAll(MakeTreeUniq04Node cr)
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

        

        private int SumReaderBit = 0;

        private int SumCreateNod = 0;
        private int SumOpenNode = 0;
        private  StringBuilder sb = new StringBuilder();
        private bool ContainueMessage = false;




        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod ;

        private void GetSegmentInfo()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBit.SumOfReadBits);

        }

        #endregion

        #region ReaderTree

        public List<int> ReadOne()
        {

            MakeTree();
            SumReaderBit = SumReaderBit + ReaderBit.SumOfReadBits;

            List<int> listNum = new List<int>();

            foreach (MakeTreeUniq04Node nod in ListNodes)
            {
                listNum.Add(nod.RealValue);
            }

            initialAll();
            SegmentNum++;


            if (ContainueMessage)
            {


                string ss =(
                    "\nModNum = " + ModNum.ToString() +
                    "\nSegmentNum = " + SegmentNum.ToString() +
                    "\nRealRead = " + ((ModStopLength * ModNum) / 8).ToString() +
                    "\nSumRead = " + ((ReaderBit.SumOfReadBits) / 8).ToString() +
                    "\nAddition = " + (((ModStopLength * ModNum) - (ReaderBit.SumOfReadBits)) / 8).ToString() +
                    "\n" +


                    "\n");

                if (MessageBox.Show(ss, "info", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    ContainueMessage = false;

            }

            GetSegmentInfo();
            ReaderBit.SumOfReadBits = 0;

            return listNum;

        }
        

        public void ReadAll()
        {
            while (ReaderBit.isAbleRead)
            {
                ReadOne();
            }
        }
        public void ReadAll(ref ReaderWriterOneNum02B WriterNum)
        {
            while (ReaderBit.isAbleRead)
            {
                foreach (int n in ReadOne())
                {
                    WriterNum.WriteNumber(n);
                }
            }

        }

    

        #region MakeTree


        private void MakeTree()
        {
            CreateTree_W1();
        }


        private int TempLocatNum = 0;
        private List<MakeTreeUniq04Node> PropList=new List<MakeTreeUniq04Node>();
        private int TempCountLocte = 1;
        private int TempOpenNode = 0;

        private void CreateTree_W1()
        {

            #region Inisitl Tree
            PropList.Clear();
            PropList = new List<MakeTreeUniq04Node>();
            TempCountLocte = 1;
            Po = root;
            Po.OpenNode();
            Po.EditLocation(0);

            if (root.nextzero == null)
            {
                root.nextzero = new MakeTreeUniq04Node();
                SumCreateNod++;
            }
         
            if (root.nextone == null)
            {
                root.nextone = new MakeTreeUniq04Node();
                SumCreateNod++;
            }

            PropList.Add(root.nextzero);
            PropList.Add(root.nextone);

            #endregion

            TempOpenNode = 0;
            while (TempCountLocte != ModStopLength)
            {
                if (ReaderBit.isAbleRead)
                {
                    CreateNod_W1();
                }
                else
                {
                    break;
                }
            }

            ValueTree();
            InitialListNodes();

        }
        private void CreateNod_W1()
        {
            TempLocatNum = ReaderNumByMod.GetOneNumber(PropList.Count);
            Po = PropList[TempLocatNum];
            Po.OpenNode();
            Po.EditLocation(TempCountLocte);
            TempCountLocte++;

            if (Po.nextzero == null)
            {
                Po.nextzero = new MakeTreeUniq04Node();
                SumCreateNod++;
            }

            if (Po.nextone == null)
            {
                Po.nextone = new MakeTreeUniq04Node();
                SumCreateNod++;
            }

            PropList.Add(Po.nextzero);
            PropList.Add(Po.nextone);

            PropList.RemoveAt(TempLocatNum);

            TempOpenNode++;

        }

     

       

        #endregion

        #region ValueTree
        private int TempNumReadTree = 0;
        private void ValueTreeBy1(MakeTreeUniq04Node cr)
        {
            if (cr == null)
                return;

            if (cr.isOpen)
            {
                ValueTreeBy1(cr.nextzero);
                cr.RealValue = TempNumReadTree; TempNumReadTree++;
                ValueTreeBy1(cr.nextone);
            }
        }
        private void ValueTree()
        {
            TempNumReadTree = 0;
            root.OpenNode();
            ValueTreeBy1(root);

        }

        private void InitialListNodes(MakeTreeUniq04Node cr)
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
            ListNodes = new MakeTreeUniq04Node[TempCountLocte];
            InitialListNodes(root);
        }
        #endregion

       

        #endregion


    }


    class MakeTreeDeUniq04Tree
    {

        #region  Properties

        public MakeTreeUniq04Node root;
        public MakeTreeUniq04Node Po;

        private int ModNum = 0;
        private int ModLength = 256;
        private int ModStopLength = 256;

        private ReaderWriteFileBits02B WriterBits;
        private ReaderWriterOneNumMod02 WriterNumByMod;
        private int LengthModListReader = 256;

        #endregion

        #region OverLoad

        public MakeTreeDeUniq04Tree()
        {
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeUniq04Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeUniq04Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            WriterBits = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            root = new MakeTreeUniq04Node();
            
            Po = root;
            WriterNumByMod = new ReaderWriterOneNumMod02(false, LengthModListReader,ref WriterBits);



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

        public void initialAll(MakeTreeUniq04Node cr)
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

            if (SegmentNum != -1)
            {
                //SumSegmentWriteBit = WriterBits.SumOfWriteBits;
                //WriterBits.SumOfWriteBits = 0;
                //AllSumWriteBit = AllSumWriteBit + SumSegmentWriteBit;
            }
            SegmentNum++;
        }

        #endregion

        #region For info

        private int SegmentNum = -1;
        private int SumSegmentWriteBit = 0;

        private int AllSumWriteBit = 0;

        private int ModSegmentLength = 2048;
        private void GetSegmentInfo()
        {

        }

        #endregion

        #region ReaderTree

        #region MakeTree

        public void ReadNumber_W1(ref ReaderWriterOneNum02B WriterNum)
        {
            while (WriterNum.isAbleRead)
            {
                ReadNumber(WriterNum.GetOneNumber());
            }

        }
        public void ReadNumber_W1(ref List<int> ListNumberData)
        {

            foreach (int Num in ListNumberData)
            {
                ReadNumber(Num);
            }

        }

        #endregion

        #region ReadNod

        private List<MakeTreeUniq04Node> PropList = new List<MakeTreeUniq04Node>();
        private int TempLocate = 0;
        private int TempRootValue = 0;
        private bool isRootRead = true;
        private bool isContainu;
      //  private MakeTreeUniq04Node PoL;
        private void ReadNumber(int TempNum)
        {
            isContainu = true;

            if (!isRootRead)
            {
                while (isContainu)
                {
                    if (TempNum > TempRootValue)
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
                            Po = Po.nextone;
                            //
                            GetLocatePropList();

                            TempLocate++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
                        }

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
                            Po = Po.nextzero;
                            //
                            GetLocatePropList();

                            TempLocate++;

                            Po = root;
                            TempRootValue = root.RealValue;
                            isContainu = false;
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
                #region Inisitl Tree
                initialAll();
                TempLocate = 0;

                PropList.Clear();
                PropList = new List<MakeTreeUniq04Node>();

                Po = root;
                Po.OpenNode();

                if (root.nextzero == null)
                {
                    root.nextzero = new MakeTreeUniq04Node();
                }

                if (root.nextone == null)
                {
                    root.nextone = new MakeTreeUniq04Node();
                }

                PropList.Add(root.nextzero);
                PropList.Add(root.nextone);

                #endregion

                root.RealValue = TempNum;
                isRootRead = false;
                
                TempRootValue = root.RealValue;
                TempLocate++;
            }

        }

        private int TempLocatNum = 0;
        private void GetLocatePropList()
        {
            TempLocatNum = 0;
            foreach (MakeTreeUniq04Node nod in PropList)
            {
                if (Po == nod)
                {
                    break;
                }

                TempLocatNum++;
            }

            WriterNumByMod.WriteNumber(PropList.Count, TempLocatNum);
            Po = PropList[TempLocatNum];

            if (Po.nextzero == null)
            {
                Po.nextzero = new MakeTreeUniq04Node();
            }

            if (Po.nextone == null)
            {
                Po.nextone = new MakeTreeUniq04Node();
            }

            PropList.Add(Po.nextzero);
            PropList.Add(Po.nextone);

            PropList.RemoveAt(TempLocatNum);

        }

        #endregion

        #endregion


    }



    

    class MakeTreeUniq04
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTU04ML";
        private string DeExtension = "DeMTU04ML";


        public MakeTreeUniq04()
        {

        }
        public MakeTreeUniq04(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }


        public void StartMakeTreeUniq_W01()
        {

            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            //ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            //if (WriterNum.GetIsCancel)
            //    return;// "isCancel";


            MakeTreeUniq04Tree Tree = new MakeTreeUniq04Tree(ModLength, ref ReaderBits);
            Tree.ReadAll();

            //  WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeUniq_FileUniqW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq04Tree Tree = new MakeTreeUniq04Tree(ModLength, ref ReaderBits);

            while (ReaderBits.isAbleRead)
            {
                Tree.ReadAll(ref WriterNum);
            }


            WriterNum.End();
            ReaderBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_FileUniqW01()
        {
            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";


            MakeTreeDeUniq04Tree Tree = new MakeTreeDeUniq04Tree(ModLength, ref WriterBits);

            while (ReaderNum.isAbleRead)
            {
                Tree.ReadNumber_W1(ref ReaderNum);
            }

            ReaderNum.End();
            WriterBits.CloseFile();

        }


        private MakeTreeUniqInfoNode00 RefrishSegmentInfo(ref MakeTreeUniqInfoNode00 MainInfoNod , int OperId , int NumOfWriteBits)
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

            MakeTreeUniqInfoReaderWriterFile00 WriterNodeInfo = new MakeTreeUniqInfoReaderWriterFile00( Path.ChangeExtension(WriterBitsInfo.GetSavePath, Extension + "Info") ,false);

            MakeTreeUniq04Tree Tree = new MakeTreeUniq04Tree(ModLength, ref ReaderBits);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);


            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);

            List<int> TempUniqList;

             MakeTreeUniqInfoNode00 MainSegmentInfo ;
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
