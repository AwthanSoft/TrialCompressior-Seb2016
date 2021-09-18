using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;


namespace OldChekers
{


    public class SortAndChangerNode2
    {
        /*********Orig***********/

        public SortAndChangerNode2 nextNode;
        public List<SortAndChangerNode2> nextTree;
        public InfoSort2Node InfoNode;


        public byte[] OrigByte;
        public int PreRealValue = 0;
        public int RealValue = 0;
        public int RealListlValue = 0;

        public int TotalCount;
        public int LevelCount;


        /********** Big To Small*****/

      
        public bool IsOpen;
        public bool IsBig;

        public bool IsReduce;

        public bool IsRead;
        public bool IsWrite;

        public int ReduceBit = 0;
 

        public byte[] BigOrigByte;

        public byte[] BigByteByte;
        public BitArray BigBitArr;



        /********* Temp **********/

        public byte[] TempOrigByte;
        public byte[] TempWritByt;

        public int TempListlValue;
        public int TempPreRealValue = 0;
        public int TempLevelCount;
        public BitArray TempBitsArr;



        public SortAndChangerNode2()
        {
            IsOpen = false;
            IsBig = false;
            IsReduce = false;
          
            IsRead = false;
            IsWrite = false;

            TotalCount = 0;
            LevelCount = 0;
            TempLevelCount = 0;
            RealListlValue = 0;
            TempListlValue = 0;

        }
    }

    public class InfoSort2Node
    {
        public List<InfoSort2Node> NextList;
        public int PreRealValue = 0;
        public int CurrentValuListlValue = 0;
        public byte[] OrigByteFor;


        public int NumOfBit = 0;

        public bool IsOpen;
        public bool IsBig;

        public bool IsReduce;
        public bool IsAddaition;
    
        public int ReduceNum=0;
        public int Addaition = 0;

        public bool IsRead;



        public BitArray WriteBitArr;
        public byte[] WriteByte;
   

        public InfoSort2Node()
        {

            IsAddaition = false;
            IsOpen = false;
            IsBig = false;
            IsReduce = false;
            IsRead = false;
        }



    }

    public class ListSortAndChanger2
    {
        private int NumOfNodes1;
        private int NumOfNodes2;
        public int NumOfInfoNods=0;


        public List<InfoSort2Node> MainListInfo;
        public int ModNum = 0;

        public ListSortAndChanger2()
        {
            NumOfNodes1 = 0;
            NumOfNodes2 = 0;
        }

        private SortAndChangerNode newNode()
        {
            SortAndChangerNode newnod = new SortAndChangerNode();

            return newnod;
        }

        private void CreatInfoNod(int CurrentValue, int PreRealValue, ref List<InfoSort2Node> ListInfoNod, ref int counterNode, BitArray BitArr)
        {
            if (CurrentValue  == 1)
            {
                InfoSort2Node newnod = new InfoSort2Node();
                newnod.OrigByteFor = new byte[2];
                newnod.OrigByteFor[0] = numoperation2.int32toByte1(PreRealValue);
                newnod.OrigByteFor[1] = numoperation2.int32toByte1(counterNode);
               
                newnod.PreRealValue = PreRealValue;
                newnod.CurrentValuListlValue = counterNode;

                byte[] Writebyte = new byte[1];
                Writebyte[0] = numoperation2.int32toByte1(counterNode);
                newnod.WriteByte = Writebyte;

                newnod.WriteBitArr = BitArr;

                if (newnod.WriteBitArr.Count < 8)
                {
                    newnod.IsReduce = true;
                    newnod.ReduceNum = 8 - newnod.WriteBitArr.Count;
                }
                else
                {
                    if (newnod.WriteBitArr.Count > 8)
                    {
                        newnod.IsAddaition = true;
                        newnod.Addaition = newnod.WriteBitArr.Count - 8;
                    }

                }
                newnod.NumOfBit = BitArr.Count;

                ListInfoNod.Add(newnod);
                NumOfInfoNods++;
                counterNode++;
            }
            else
            {
                CreatInfoNod(CurrentValue / 2, PreRealValue, ref  ListInfoNod, ref  counterNode, AddBitToInforArr(BitArr, false));
                CreatInfoNod(((CurrentValue / 2) + (CurrentValue % 2)), PreRealValue, ref  ListInfoNod, ref  counterNode, AddBitToInforArr(BitArr, true));

            }

            
        }

        public void creatInfoListBy2()
        {
            MainListInfo = new List<InfoSort2Node>();
            int i = 0;
            while (i != 256)
            {
                InfoSort2Node newnod = new InfoSort2Node();
                newnod.OrigByteFor = new byte[1];
                newnod.OrigByteFor[0] = numoperation2.int32toByte1(i);

                newnod.NextList = new List<InfoSort2Node>();
                newnod.WriteBitArr = new BitArray(0);

                newnod.PreRealValue = i;
                newnod.NextList=new List<InfoSort2Node>();

                int CounterNods=0;
                BitArray BitLength= new BitArray(0);

                CreatInfoNod(i + 1, i, ref newnod.NextList, ref CounterNods, AddBitToInforArr(BitLength, false));
                BitArray BitLength2 = new BitArray(0);
                if ((255 - i) != 0)
                    CreatInfoNod((255 - i), i, ref newnod.NextList, ref CounterNods, AddBitToInforArr(BitLength2, true));
                

                MainListInfo.Add(newnod);

                i++;
            }

        }

        public void EditinfoListTo_Way2()
        {

            foreach (InfoSort2Node n in MainListInfo)
            {
                foreach (InfoSort2Node m in n.NextList)
                {
                    BitArray Bits = new BitArray(8 + m.WriteBitArr.Count);
                    int i = 0;

                    foreach(bool b in numoperation2.intvaluToBitsArr(m.PreRealValue,8))
                    {
                        Bits[i] = b; i++;
                    }
                    foreach (bool b in m.WriteBitArr)
                    {
                        Bits[i] = b; i++;
                    }
                    m.WriteBitArr = Bits;
                }
            }



        }



        //Way 3
    
        public void creatInfoListBy2_Way3()
        {
            MainListInfo = new List<InfoSort2Node>();
            int i = 0;
            while (i != 256)
            {
                InfoSort2Node newnod = new InfoSort2Node();
                newnod.OrigByteFor = new byte[1];
                newnod.OrigByteFor[0] = numoperation2.int32toByte1(i);

                newnod.NextList = new List<InfoSort2Node>();
                newnod.WriteBitArr = new BitArray(0);

                newnod.PreRealValue = i;
                newnod.NextList = new List<InfoSort2Node>();


                CreatInfoNod_Way3(i , ref newnod.NextList);
          
               


                MainListInfo.Add(newnod);

                i++;
            }

        }
        private void CreatInfoNod_Way3(int PreNum, ref List<InfoSort2Node> ListInfoNod)
        {
            if (PreNum < 128)
            {

                List<bool> bits = new List<bool>();

                for (int i = 0; i != 256; i++)
                {
                    int Start1 = 0, End1 = PreNum;
                    int Start2 = PreNum + 1, End2 = 255;
                    int Devid1 = 0, Devid2 = 0;
                    int Mod = 0;


                    bits = new List<bool>();

                    while (true)
                    {

                        if (i <= End1)
                        {
                            bits.Add(false);
                            if (Start1 == End1)
                            {
                                InfoSort2Node newnod = new InfoSort2Node();
                                newnod.OrigByteFor = new byte[2];
                                newnod.OrigByteFor[0] = numoperation2.int32toByte1(PreNum);
                                newnod.OrigByteFor[1] = numoperation2.int32toByte1(i);

                                newnod.PreRealValue = PreNum;
                                newnod.CurrentValuListlValue = i;

                                byte[] Writebyte = new byte[1];
                                Writebyte[0] = numoperation2.int32toByte1(PreNum);
                                newnod.WriteByte = Writebyte;

                                BitArray BitArr = new BitArray(bits.Count);
                                int n = 0;
                                foreach (bool b in bits)
                                {
                                    BitArr[n] = b; n++;
                                }

                                newnod.WriteBitArr = BitArr;

                                if (newnod.WriteBitArr.Count < 8)
                                {
                                    newnod.IsReduce = true;
                                    newnod.ReduceNum = 8 - newnod.WriteBitArr.Count;
                                }
                                else
                                {
                                    if (newnod.WriteBitArr.Count > 8)
                                    {
                                        newnod.IsAddaition = true;
                                        newnod.Addaition = newnod.WriteBitArr.Count - 8;
                                    }

                                }
                                newnod.NumOfBit = BitArr.Count;

                                ListInfoNod.Add(newnod);

                                break;

                            }
                            else
                            {
                                Mod = (End1 + 1) - Start1;

                                Devid1 = (Mod / 2);
                                Devid2 = (Mod / 2) + (Mod % 2);

                                End1 = (Start1 + Devid1) - 1;

                                Start2 = (End1 + 1);
                                End2 = (Start2 + Devid2) - 1;
                            }

                        }
                        else
                        {
                            bits.Add(true);

                            if (Start2 == End2)
                            {
                                InfoSort2Node newnod = new InfoSort2Node();
                                newnod.OrigByteFor = new byte[2];
                                newnod.OrigByteFor[0] = numoperation2.int32toByte1(PreNum);
                                newnod.OrigByteFor[1] = numoperation2.int32toByte1(i);

                                newnod.PreRealValue = PreNum;
                                newnod.CurrentValuListlValue = i;

                                byte[] Writebyte = new byte[1];
                                Writebyte[0] = numoperation2.int32toByte1(PreNum);
                                newnod.WriteByte = Writebyte;

                                BitArray BitArr = new BitArray(bits.Count);
                                int n = 0;
                                foreach (bool b in bits)
                                {
                                    BitArr[n] = b; n++;
                                }

                                newnod.WriteBitArr = BitArr;

                                if (newnod.WriteBitArr.Count < 8)
                                {
                                    newnod.IsReduce = true;
                                    newnod.ReduceNum = 8 - newnod.WriteBitArr.Count;
                                }
                                else
                                {
                                    if (newnod.WriteBitArr.Count > 8)
                                    {
                                        newnod.IsAddaition = true;
                                        newnod.Addaition = newnod.WriteBitArr.Count - 8;
                                    }

                                }
                                newnod.NumOfBit = BitArr.Count;

                                ListInfoNod.Add(newnod);

                                break;

                            }
                            else
                            {
                                Mod = (End2 + 1) - Start2;

                                Devid1 = (Mod / 2);
                                Devid2 = (Mod / 2) + (Mod % 2);

                                Start1 = Start2;
                                End1 = (Start1 + Devid1) - 1;

                                Start2 = (End1 + 1);
                                End2 = (Start2 + Devid2) - 1;
                            }

                        }
                    }

                }
            }
            else
            {





                List<bool> bits = new List<bool>();

                for (int i = 0; i != 256; i++)
                {
                    int Start1 = 0, End1 = PreNum - 1;
                    int Start2 = PreNum, End2 = 255;
                    int Devid1 = 0, Devid2 = 0;
                    int Mod = 0;


                    bits = new List<bool>();

                    while (true)
                    {

                        if (i >= Start2)
                        {
                            bits.Add(true);

                            if (Start2 == End2)
                            {
                                InfoSort2Node newnod = new InfoSort2Node();
                                newnod.OrigByteFor = new byte[2];
                                newnod.OrigByteFor[0] = numoperation2.int32toByte1(PreNum);
                                newnod.OrigByteFor[1] = numoperation2.int32toByte1(i);

                                newnod.PreRealValue = PreNum;
                                newnod.CurrentValuListlValue = i;

                                byte[] Writebyte = new byte[1];
                                Writebyte[0] = numoperation2.int32toByte1(PreNum);
                                newnod.WriteByte = Writebyte;

                                BitArray BitArr = new BitArray(bits.Count);
                                int n = 0;
                                foreach (bool b in bits)
                                {
                                    BitArr[n] = b; n++;
                                }

                                newnod.WriteBitArr = BitArr;

                                if (newnod.WriteBitArr.Count < 8)
                                {
                                    newnod.IsReduce = true;
                                    newnod.ReduceNum = 8 - newnod.WriteBitArr.Count;
                                }
                                else
                                {
                                    if (newnod.WriteBitArr.Count > 8)
                                    {
                                        newnod.IsAddaition = true;
                                        newnod.Addaition = newnod.WriteBitArr.Count - 8;
                                    }

                                }
                                newnod.NumOfBit = BitArr.Count;

                                ListInfoNod.Add(newnod);

                                break;

                            }
                            else
                            {
                                Mod = (End2 + 1) - Start2;

                                Devid1 = (Mod / 2);
                                Devid2 = (Mod / 2) + (Mod % 2);

                                Start1 = Start2;
                                End1 = (Start1 + Devid1) - 1;

                                Start2 = (End1 + 1);
                                End2 = (Start2 + Devid2) - 1;
                            }

                        }
                        else
                        {

                            bits.Add(false);
                            if (Start1 == End1)
                            {
                                InfoSort2Node newnod = new InfoSort2Node();
                                newnod.OrigByteFor = new byte[2];
                                newnod.OrigByteFor[0] = numoperation2.int32toByte1(PreNum);
                                newnod.OrigByteFor[1] = numoperation2.int32toByte1(i);

                                newnod.PreRealValue = PreNum;
                                newnod.CurrentValuListlValue = i;

                                byte[] Writebyte = new byte[1];
                                Writebyte[0] = numoperation2.int32toByte1(PreNum);
                                newnod.WriteByte = Writebyte;

                                BitArray BitArr = new BitArray(bits.Count);
                                int n = 0;
                                foreach (bool b in bits)
                                {
                                    BitArr[n] = b; n++;
                                }

                                newnod.WriteBitArr = BitArr;

                                if (newnod.WriteBitArr.Count < 8)
                                {
                                    newnod.IsReduce = true;
                                    newnod.ReduceNum = 8 - newnod.WriteBitArr.Count;
                                }
                                else
                                {
                                    if (newnod.WriteBitArr.Count > 8)
                                    {
                                        newnod.IsAddaition = true;
                                        newnod.Addaition = newnod.WriteBitArr.Count - 8;
                                    }

                                }
                                newnod.NumOfBit = BitArr.Count;

                                ListInfoNod.Add(newnod);

                                break;

                            }
                            else
                            {
                                Mod = (End1 + 1) - Start1;

                                Devid1 = (Mod / 2);
                                Devid2 = (Mod / 2) + (Mod % 2);

                                End1 = (Start1 + Devid1) - 1;

                                Start2 = (End1 + 1);
                                End2 = (Start2 + Devid2) - 1;
                            }

                        }
                    }

                }
            }


        }



        //By2
        private void CreatNextListBy2(ref List<SortAndChangerNode2> Nextlist, byte PreByte, int PreNum, int BitsCount)
        {
            int i = 0;
            while (i != 256)
            {
                SortAndChangerNode2 newnod = new SortAndChangerNode2();
                newnod.RealValue = NumOfNodes2; NumOfNodes2++;
                newnod.OrigByte = new byte[2];
                newnod.OrigByte[0] = PreByte;
                newnod.OrigByte[1] = numoperation2.int32toByte1(i);

                newnod.TempBitsArr = new BitArray(0);

                newnod.RealListlValue = i;
                newnod.PreRealValue = PreNum;

                if (PreNum >= i)
                {
                    newnod.IsBig = true;

                    //Big To Small
                    {
                        BitArray BitNum = numoperation2.intvaluToBitsArr(i, BitsCount);
                        if (BitsCount >= 8)
                        {
                            newnod.BigByteByte = new byte[2];
                            newnod.BigByteByte[0] = PreByte;
                            newnod.BigByteByte[1] = numoperation2.int32toByte1(i);

                            newnod.BigOrigByte = new byte[2];
                            newnod.BigOrigByte[0] = PreByte;
                            newnod.BigOrigByte[1] = numoperation2.int32toByte1(i);

                            newnod.IsReduce = false;
                            newnod.ReduceBit = 0;
                        }
                        else
                        {
                            newnod.BigByteByte = new byte[1];
                            newnod.BigByteByte[0] = PreByte;
                            newnod.BigBitArr = BitNum;

                            newnod.BigOrigByte = new byte[2];
                            newnod.BigOrigByte[0] = PreByte;
                            newnod.BigOrigByte[1] = numoperation2.int32toByte1(i);


                            newnod.IsOpen = true;
                            newnod.IsReduce = true;
                            newnod.ReduceBit = 8 - BitsCount;
                        }


                    }



                }
                else
                {


                    newnod.IsBig = false;

                    {
                        BitArray BitNum = numoperation2.intvaluToBitsArr(i);

                        if (BitNum.Count >= 8)
                        {
                            newnod.BigByteByte = new byte[2];
                            newnod.BigByteByte[0] = numoperation2.int32toByte1(i);
                            newnod.BigByteByte[1] = PreByte;

                            newnod.BigOrigByte = new byte[2];
                            newnod.BigOrigByte[1] = PreByte;
                            newnod.BigOrigByte[0] = numoperation2.int32toByte1(i);

                            newnod.IsReduce = false;
                            newnod.ReduceBit = 0;
                        }
                        else
                        {
                            newnod.BigByteByte = new byte[1];
                            newnod.BigByteByte[0] = numoperation2.int32toByte1(i);
                            newnod.BigBitArr = numoperation2.intvaluToBitsArr(PreNum, BitNum.Count);

                            newnod.BigOrigByte = new byte[2];
                            newnod.BigOrigByte[1] = PreByte;
                            newnod.BigOrigByte[0] = numoperation2.int32toByte1(i);

                            newnod.IsReduce = true;
                            newnod.ReduceBit = 8 - BitNum.Count;
                        }
                    }


                }



                Nextlist.Add(newnod);
                i++;
            }
        }
        public void creatBy2(ref List<SortAndChangerNode2> thislist)
        {
            thislist = new List<SortAndChangerNode2>();
            ModNum = 2;
            int i = 0;
            while (i != 256)
            {
                SortAndChangerNode2 newnod = new SortAndChangerNode2();
                newnod.OrigByte = new byte[1];
                newnod.OrigByte[0] = numoperation2.int32toByte1(i);
                newnod.RealValue = NumOfNodes1; NumOfNodes1++;
                newnod.nextTree = new List<SortAndChangerNode2>();

                newnod.TempBitsArr = new BitArray(0);

                BitArray BitNum = numoperation2.intvaluToBitsArr(i);
                newnod.RealListlValue = i;

                CreatNextListBy2(ref newnod.nextTree, newnod.OrigByte[0], i, BitNum.Count);

                thislist.Add(newnod);

                i++;
            }


        }


        public void quicksort(ref List<InfoSort2Node> list, int begin, int end)
        {
            InfoSort2Node pivot = list[(begin + (end - begin) / 2)];
            int left = begin;
            int right = end;
            while (left <= right)
            {
                while (list[left].NumOfBit < pivot.NumOfBit)
                {
                    left++;
                }
                while (list[right].NumOfBit > pivot.NumOfBit)
                {
                    right--;
                }
                if (left <= right)
                {
                    swap(ref list, left, right);
                    left++;
                    right--;
                }
            }
            if (begin < right)
            {
                quicksort(ref list, begin, left - 1);
            }
            if (end > left)
            {
                quicksort(ref list, right + 1, end);
            }
        }
        public void swap(ref List<InfoSort2Node> list, int x, int y)
        {
            InfoSort2Node temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }


        public void quicksort(ref List<SortAndChangerNode2> list, int begin, int end)
        {
            SortAndChangerNode2 pivot = list[(begin + (end - begin) / 2)];
            int left = begin;
            int right = end;
            while (left <= right)
            {
                while (list[left].LevelCount < pivot.LevelCount)
                {
                    left++;
                }
                while (list[right].LevelCount > pivot.LevelCount)
                {
                    right--;
                }
                if (left <= right)
                {
                    swap(ref list, left, right);
                    left++;
                    right--;
                }
            }
            if (begin < right)
            {
                quicksort(ref list, begin, left - 1);
            }
            if (end > left)
            {
                quicksort(ref list, right + 1, end);
            }
        }
        public void swap(ref List<SortAndChangerNode2> list, int x, int y)
        {
            SortAndChangerNode2 temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }



        public string PrintInfo()
        {
            StringBuilder xx = new StringBuilder();

            List<InfoSort2Node> TempInfo = new List<InfoSort2Node>();

            int IsReduce=0;
            int CounterLessOf8 = 0;
            int CountEqual8 = 0;
            int CountEqual1 = 0;
            int IsMorThan8 = 0;
            int CountEqual9 = 0;

            List<int> Counters = new List<int>();
            for (int i = 0; i != 10; i++)
            {
                int n = 0;
                Counters.Add(n);
            }


       //     int IsNull = 0;

            foreach (InfoSort2Node nod in MainListInfo)
            {
                foreach (InfoSort2Node m in nod.NextList)
                {
                    Counters[m.WriteBitArr.Count]++;

                    if (m.IsReduce == true)
                    {
                        IsReduce++;
                        if (m.WriteBitArr.Count < 8)
                            CounterLessOf8++;

                        if (m.WriteBitArr.Count == 1 )
                            CountEqual1++;


                    }
                    if (m.WriteBitArr.Count== 8)
                        CountEqual8++;

                    if (m.WriteBitArr.Count > 8)
                    {
                        IsMorThan8++;

                        if (m.WriteBitArr.Count == 9)
                        {
                            CountEqual9++;
                        }
                    }


                }

            }

            StringBuilder cc = new StringBuilder();
            cc.Append("\n\n      Counters   \n\n\n\n");
            for (int i = 0; i != 10; i++)
            {
                cc.Append("\nCounters" + i.ToString("0") + " = " + Counters[i].ToString());
            }

            xx.Append(
                cc.ToString()+
                "\n\n\n" +
                "\nIsReduce = " + IsReduce.ToString()+
                "\nCounterLessOf8 = "+CounterLessOf8.ToString()+
                "\nCountEqual8 = "+CountEqual8.ToString()+
                "\nIsMorThan8 = "+IsMorThan8.ToString()+
                "\nCountEqual9 = "+CountEqual9.ToString()+
                "\nCountEqual1 = "+CountEqual1.ToString()+


                "\n\n\n");




            return xx.ToString();
        }

        public BitArray AddBitToInforArr(BitArray bitArr, bool stateBit)
        {
             

            BitArray newbitArr = new BitArray(bitArr.Count + 1);
            int i = 0;
            foreach (bool b in bitArr)
            {
                newbitArr[i] = b; i++;
            }
            newbitArr[i] = stateBit;


            return newbitArr;
        }


    }

    public class CompSortAndChanger2
    {
        public StringBuilder sb;
        public StringBuilder RePort;
        private List<List<byte[]>> ListDic;

        private List<SortAndChangerNode2> MainList;

        private ListSortAndChanger2 Tr;
        public int mod;
        private byte[] num = new byte[5];

        public long origFilelength = 0;

        public bool countenu = false;



        public CompSortAndChanger2()
        {
            sb = new StringBuilder();
            RePort = new StringBuilder();
            Tr = new ListSortAndChanger2();
            ListDic = new List<List<byte[]>>();
            num[0] = numoperation2.int32toByte1(0);
            num[1] = numoperation2.int32toByte1(1);
            num[2] = numoperation2.int32toByte1(2);
            num[3] = numoperation2.int32toByte1(3);
            num[4] = numoperation2.int32toByte1(4);

            mod = 0;


        }


        public void ExmineFileBy2(string filingPath)
        {

            // 01 
            WalkThroListSortBy2(filingPath);

            //02
            RePort = new StringBuilder();
            AnalysisListBy2();


        }

        private void WalkThroListSortBy2(string filingPath)
        {
            DateTime start = DateTime.Now;
            sb.Append("\nTime Start WalkThroListSortBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            using (var filing = new FileStream(filingPath, FileMode.Open, FileAccess.Read))
            {
                origFilelength = filing.Length;

                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);


                //CreateList
                MainList = new List<SortAndChangerNode2>();

                Tr.creatBy2(ref MainList);
                bool isFirst = true;
                int Temp = 0;

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {

                    byte[] datafile1 = new byte[52428800];


                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filing.Read(datafile1, 0, 52428800);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                                MainList[Temp].nextTree[n].LevelCount++;
                                isFirst = true;
                            }
                        }

                        Process1++;
                    }

                } //End ProcessTimer1

                //Start ProcessTimer2
                if (ProcessTimer2 != 0)
                {

                    byte[] datafile2 = new byte[ProcessTimer2];

                    filing.Read(datafile2, 0, ProcessTimer2);

                    foreach (int n in datafile2)
                    {
                        if (isFirst == true)
                        {
                            Temp = n;
                            isFirst = false;
                        }
                        else
                        {
                            MainList[Temp].nextTree[n].LevelCount++;
                            isFirst = true;
                        }
                    }

                }//End ProcessTimer2


                //LastByte
                if (isFirst == false)
                {
                    //   MainList[Temp].TotalCount++;
                    MainList[Temp].LevelCount++;
                }

                filing.Close();
            }


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End WalkThroListSortBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));
        }


        public void AnalysisListBy2()
        {

            DateTime start = DateTime.Now;
            sb.Append("\nTime Start AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            List<SortAndChangerNode2> TempTreeList = new List<SortAndChangerNode2>();
            List<byte[]> DicByte = new List<byte[]>();

            //FillList
            {
                foreach (SortAndChangerNode2 n in MainList)
                {
                    foreach (SortAndChangerNode2 m in n.nextTree)
                    {
                        TempTreeList.Add(m);
                    }
                    
                }
                //SortTemp
                Tr.quicksort(ref TempTreeList, 0, TempTreeList.Count - 1);
            }

           


            //Pre Analysis 02
            {
                List<InfoSort2Node> TempListInfo = new List<InfoSort2Node>();
                Tr.creatInfoListBy2();

                //FillInfoList
                {
                    foreach (InfoSort2Node n in Tr.MainListInfo)
                    {
                        foreach (InfoSort2Node m in n.NextList)
                        {
                            TempListInfo.Add(m);
                        }
                    }
                    //SortTemp
                    Tr.quicksort(ref TempListInfo, 0, TempListInfo.Count - 1);

                }

           //     if (MessageBox.Show(TempTreeList.Count.ToString()+" = "+TempListInfo.Count.ToString(), "Size Of Lists", MessageBoxButtons.OKCancel) == DialogResult.Cancel)

                //FirstCal
                {
                    long SecoundSize = 0;
                    int L = TempTreeList.Count - 1;


                    for (int i = 0; i != TempTreeList.Count; i++)
                    {

                        SecoundSize = SecoundSize + ((TempListInfo[i].NumOfBit) * (TempTreeList[L].LevelCount));


                        L--;
                       
                    }

                    {
                        int PreSizeSecound = Convert.ToInt32((origFilelength / 2));
                        int SecoundAfterCo = Convert.ToInt32(SecoundSize / 8);
                        long LastSize = PreSizeSecound + SecoundAfterCo;

                        int SumReduce = (Convert.ToInt32(origFilelength ))-(Convert.ToInt32(  LastSize));

                        StringBuilder xx = new StringBuilder();
                        xx.Append(
                              "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
                              "\nPreSizeSecound = " + PreSizeSecound.ToString() + "  =  " + (PreSizeSecound / 1024).ToString() + " KB " + "  =  " + (PreSizeSecound / 1048576).ToString() + " MB " +
                              "\nSecoundAfterCo = " + SecoundAfterCo.ToString() + "  =  " + (SecoundAfterCo / 1024).ToString() + " KB " + "  =  " + (SecoundAfterCo / 1048576).ToString() + " MB " +
                              "\nSumReduce = " + (SumReduce).ToString() + "  = " + ((SumReduce) / 1024).ToString() + " KB " + "  =  " + ((SumReduce ) / 1048576).ToString() + " MB " +
                              "\n\n\n" +
                              "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +
                              "\n\n\n\n");

                        RePort.Append(xx.ToString() +
                            "\n\n\n"
                          );

                        if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            return;

                    }
                }
            }


            //Pointers

            long TotalReduce = 0;
            int TotalAdd = Convert.ToInt32((origFilelength / 2) / 8);

            //FillList 
            //Compute LevelCount && Reduce
            {
                int Counter = 0;
                int ReduceCounter = 0;
                foreach (SortAndChangerNode2 n in MainList)
                {
                    Counter = 0;
                    ReduceCounter = 0;
                    foreach (SortAndChangerNode2 m in n.nextTree)
                    {
                        Counter = Counter + m.LevelCount;
                        if (m.IsReduce == true)
                            ReduceCounter = ReduceCounter + (m.LevelCount * m.ReduceBit);

                    }
                    n.LevelCount = n.LevelCount + Counter;
                    TotalReduce = TotalReduce + ReduceCounter;
                }

            }

            int LastReduce = Convert.ToInt32(TotalReduce / 8) - TotalAdd;
            //   Reduce = Convert.ToInt32(TotalReduce / 8);

            bool Count = false;
            if (LastReduce > 0)
            {
                countenu = true;
                Count = true;

            }

            RePort.Append("\n\n\n******** RePort ****\n" +
               "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
               "\n" +
               "\ncountenu = " + Count.ToString() +
               "\n\n");

            if (LastReduce > 0)
            {
                long LastSize = origFilelength - LastReduce;
                RePort.Append("\n" +
                    "\nTotalReduceit = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 8 / 1024).ToString() + " KB " + "  =  " + (TotalReduce / 8 / 1048576).ToString() + " MB " + "\n\n\n" +
                  "\nLastReduce = " + LastReduce.ToString() + "  =  " + (LastReduce / 1024).ToString() + " KB " + "  =  " + (LastReduce / 1048576).ToString() + " MB " +
                  "\nTotalAdd = " + TotalAdd.ToString() + "  =  " + (TotalAdd / 1024).ToString() + " KB " + "  =  " + (TotalAdd / 1048576).ToString() + " MB " +
                  "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +

                  "\n\n\n");
            }
            else
            {
                LastReduce = LastReduce * -1;
                RePort.Append("\n" +
                  "\nTotalReduceit = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 8 / 1024).ToString() + " KB " + "  =  " + (TotalReduce / 8 / 1048576).ToString() + " MB " +
                   "\nTotalAdd = " + TotalAdd.ToString() + "  =  " + (TotalAdd / 1024).ToString() + " KB " + "  =  " + (TotalAdd / 1048576).ToString() + " MB " + "\n\n\n" +
                  "\nAdding = " + LastReduce.ToString() + "  =  " + (LastReduce / 1024).ToString() + " KB " + "  =  " + (LastReduce / 1048576).ToString() + " MB " +


                  "\n\n\n\n");


            }






            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));


        }


        public void ExmineFileBy2_Way2(string filingPath)
        {

            // 01 
            WalkThroListSortBy2_Way2(filingPath);

            //02
            RePort = new StringBuilder();
            AnalysisListBy2_Way2();

            if (MessageBox.Show(RePort.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                StartCompBy2_Way2(filingPath);

            }
        }
        private void WalkThroListSortBy2_Way2(string filingPath)
        {
            DateTime start = DateTime.Now;
            sb.Append("\nTime Start WalkThroListSortBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            using (var filing = new FileStream(filingPath, FileMode.Open, FileAccess.Read))
            {
                origFilelength = filing.Length;

                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);


                //CreateList
                MainList = new List<SortAndChangerNode2>();

                Tr.creatBy2(ref MainList);
                bool isFirst = true;
                int Temp = 0;

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {

                    byte[] datafile1 = new byte[52428800];


                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filing.Read(datafile1, 0, 52428800);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                                MainList[Temp].nextTree[n].LevelCount++;
                                isFirst = true;
                            }
                        }

                        Process1++;
                    }

                } //End ProcessTimer1

                //Start ProcessTimer2
                if (ProcessTimer2 != 0)
                {

                    byte[] datafile2 = new byte[ProcessTimer2];

                    filing.Read(datafile2, 0, ProcessTimer2);

                    foreach (int n in datafile2)
                    {
                        if (isFirst == true)
                        {
                            Temp = n;
                            isFirst = false;
                        }
                        else
                        {
                            MainList[Temp].nextTree[n].LevelCount++;
                            isFirst = true;
                        }
                    }

                }//End ProcessTimer2


                //LastByte
                if (isFirst == false)
                {
                    //   MainList[Temp].TotalCount++;
                    MainList[Temp].LevelCount++;
                }

                filing.Close();
            }


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End WalkThroListSortBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));
        }
        public void AnalysisListBy2_Way2()
        {

            DateTime start = DateTime.Now;
            sb.Append("\nTime Start AnalysisListBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            List<SortAndChangerNode2> TempTreeList = new List<SortAndChangerNode2>();
            List<byte[]> DicByte = new List<byte[]>();

            //FillList
            {
                foreach (SortAndChangerNode2 n in MainList)
                {
                    foreach (SortAndChangerNode2 m in n.nextTree)
                    {
                        TempTreeList.Add(m);
                    }

                }
                //SortTemp
              //  Tr.quicksort(ref TempTreeList, 0, TempTreeList.Count - 1);
            }




            //Pre Analysis 02
            {
                List<InfoSort2Node> TempListInfo = new List<InfoSort2Node>();
                Tr.creatInfoListBy2();

                //FillInfoList
                {
                    foreach (InfoSort2Node n in Tr.MainListInfo)
                    {
                        foreach (InfoSort2Node m in n.NextList)
                        {
                            TempListInfo.Add(m);
                        }
                    }
                    //SortTemp
              //     Tr.quicksort(ref TempListInfo, 0, TempListInfo.Count - 1);

                }


                //   if (MessageBox.Show(TempTreeList.Count.ToString()+" = "+TempListInfo.Count.ToString(), "Size Of Lists", MessageBoxButtons.OKCancel) == DialogResult.Cancel)


                
                //FirstCal
                {
                    long SecoundSize = 0;
                    int L = TempTreeList.Count - 1;


                    for (int i = 0; i != TempTreeList.Count; i++)
                    {

                        SecoundSize = SecoundSize + ((TempListInfo[i].NumOfBit) * (TempTreeList[L].LevelCount));


                        L--;

                    }

                    {
                        int PreSizeSecound = Convert.ToInt32((origFilelength / 2));
                        int SecoundAfterCo = Convert.ToInt32(SecoundSize / 8);
                        long LastSize = PreSizeSecound + SecoundAfterCo;

                        int SumReduce = (Convert.ToInt32(origFilelength)) - (Convert.ToInt32(LastSize));

                        StringBuilder xx = new StringBuilder();
                        xx.Append(
                              "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
                              "\nPreSizeSecound = " + PreSizeSecound.ToString() + "  =  " + (PreSizeSecound / 1024).ToString() + " KB " + "  =  " + (PreSizeSecound / 1048576).ToString() + " MB " +
                              "\nSecoundAfterCo = " + SecoundAfterCo.ToString() + "  =  " + (SecoundAfterCo / 1024).ToString() + " KB " + "  =  " + (SecoundAfterCo / 1048576).ToString() + " MB " +
                              "\nSumReduce = " + (SumReduce).ToString() + "  = " + ((SumReduce) / 1024).ToString() + " KB " + "  =  " + ((SumReduce) / 1048576).ToString() + " MB " +
                              "\n\n\n" +
                              "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +
                              "\n\n\n\n");

                        RePort.Append(xx.ToString() +
                            "\n\n\n"
                          );

                        Tr.EditinfoListTo_Way2();


                       // RePort.Append(xx.ToString());

                        //if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        //    return;

                    }
                }
            }



            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End AnalysisListBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));



            ////Pointers

            //long TotalReduce = 0;
            //int TotalAdd = Convert.ToInt32((origFilelength / 2) / 8);

            ////FillList 
            ////Compute LevelCount && Reduce
            //{
            //    int Counter = 0;
            //    int ReduceCounter = 0;
            //    foreach (SortAndChangerNode2 n in MainList)
            //    {
            //        Counter = 0;
            //        ReduceCounter = 0;
            //        foreach (SortAndChangerNode2 m in n.nextTree)
            //        {
            //            Counter = Counter + m.LevelCount;
            //            if (m.IsReduce == true)
            //                ReduceCounter = ReduceCounter + (m.LevelCount * m.ReduceBit);

            //        }
            //        n.LevelCount = n.LevelCount + Counter;
            //        TotalReduce = TotalReduce + ReduceCounter;
            //    }

            //}

            //int LastReduce = Convert.ToInt32(TotalReduce / 8) - TotalAdd;
            ////   Reduce = Convert.ToInt32(TotalReduce / 8);

            //bool Count = false;
            //if (LastReduce > 0)
            //{
            //    countenu = true;
            //    Count = true;

            //}

            //RePort.Append("\n\n\n******** RePort ****\n" +
            //   "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
            //   "\n" +
            //   "\ncountenu = " + Count.ToString() +
            //   "\n\n");

            //if (LastReduce > 0)
            //{
            //    long LastSize = origFilelength - LastReduce;
            //    RePort.Append("\n" +
            //        "\nTotalReduceit = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 8 / 1024).ToString() + " KB " + "  =  " + (TotalReduce / 8 / 1048576).ToString() + " MB " + "\n\n\n" +
            //      "\nLastReduce = " + LastReduce.ToString() + "  =  " + (LastReduce / 1024).ToString() + " KB " + "  =  " + (LastReduce / 1048576).ToString() + " MB " +
            //      "\nTotalAdd = " + TotalAdd.ToString() + "  =  " + (TotalAdd / 1024).ToString() + " KB " + "  =  " + (TotalAdd / 1048576).ToString() + " MB " +
            //      "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +

            //      "\n\n\n");
            //}
            //else
            //{
            //    LastReduce = LastReduce * -1;
            //    RePort.Append("\n" +
            //      "\nTotalReduceit = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 8 / 1024).ToString() + " KB " + "  =  " + (TotalReduce / 8 / 1048576).ToString() + " MB " +
            //       "\nTotalAdd = " + TotalAdd.ToString() + "  =  " + (TotalAdd / 1024).ToString() + " KB " + "  =  " + (TotalAdd / 1048576).ToString() + " MB " + "\n\n\n" +
            //      "\nAdding = " + LastReduce.ToString() + "  =  " + (LastReduce / 1024).ToString() + " KB " + "  =  " + (LastReduce / 1048576).ToString() + " MB " +


            //      "\n\n\n\n");


            //}






            


        }
        public void StartCompBy2_Way2(string pathFile)
        {
            //if (countenu == true)
            //{


            //    if (MessageBox.Show("Start SortComp =  Yes  😄😄😄😄  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //        return;



            //}
            //else
            //{
            //    if (MessageBox.Show(" You Can't Contenue  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //        return;
            //    //    MessageBox.Show(" You Can't Contenue  !!");

            //}


            // MessageBox.Show(" Yes  😄😄😄😄  !!");

            FileInfo fil = new FileInfo(pathFile);
            string FileCompDerct = fil.FullName.Remove(fil.FullName.Length - fil.Extension.Length);
            Directory.CreateDirectory(FileCompDerct);

            //Filing
            //string ForByte = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bytF";
            string ForBits = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bitsFW2";
      


            CompfilingBy2_Way2(pathFile, ForBits);




        }
        private void CompfilingBy2_Way2(string pathfile, string ForBits)
        {
            DateTime startTime = DateTime.Now;
            sb.Append("\nTime Start CompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            using (var filecomp = new FileStream(pathfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var Bitsfiling = new FileStream(ForBits, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {


                List<InfoSort2Node> Mininfolist = Tr.MainListInfo;

                //InfoRest
                int Addbits = 0;
                int RestByte = 0;
                byte[] lastByt = new byte[1];
                lastByt[0] = numoperation2.int32toByte1(0);

                //Seek Files
                filecomp.Seek(0, SeekOrigin.Begin);
                Bitsfiling.Seek(0, SeekOrigin.Begin);

                //FileLength     50MB = 52428800  10MB = 10485760 

                long Filelength = filecomp.Length;

                int ProcessTimer1 = Convert.ToInt32(Filelength / 10485760);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 10485760);

                bool isFirst = true;
                int Temp = 0;


                List<BitArray> savbitList = new List<BitArray>();

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {
                    byte[] datafile1 = new byte[10485760];

                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filecomp.Read(datafile1, 0, 10485760);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                                savbitList.Add(Mininfolist[Temp].NextList[n].WriteBitArr);

                                isFirst = true;
                            }
                        }

                        //Save Data
                        {
                            //Bitsave
                            {
                                int bs = 0;
                                BitArray savbits = new BitArray(savbitList.Count * 17);
                                foreach (BitArray bitArr in savbitList)
                                {
                                    foreach (bool b in bitArr)
                                    {
                                        savbits[bs] = b; bs++;
                                    }
                                }
                                byte[] savbitsbyte = new byte[(savbits.Count / 8) + 1];
                                savbits.CopyTo(savbitsbyte, 0);
                                Bitsfiling.Write(savbitsbyte, 0, bs / 8);

                                int start = (bs / 8) * 8, end = bs;

                                BitArray TempBit = new BitArray(bs % 8);
                                bs = 0;
                                while (start != end)
                                {
                                    TempBit[bs] = savbits[start]; bs++; start++;
                                }
                                savbitList = new List<BitArray>();
                                if (TempBit.Count != 0)
                                    savbitList.Add(TempBit);

                                savbits = null;
                                savbitsbyte = null;

                            }

                        }

                        Process1++;
                    }
                }//End ProcessTimer1

                //Start ProcessTimer2
                if (ProcessTimer2 != 0)
                {
                    byte[] datafile2 = new byte[ProcessTimer2];
                    filecomp.Read(datafile2, 0, ProcessTimer2);

                    foreach (int n in datafile2)
                    {
                        if (isFirst == true)
                        {
                            Temp = n;
                            isFirst = false;
                        }
                        else
                        {
                            savbitList.Add(Mininfolist[Temp].NextList[n].WriteBitArr);

                            isFirst = true;
                        }
                    }

                    //LastPart
                    {
                        if (isFirst == false)
                        {
                           // lastByt = new byte[1];
                            lastByt[0] = MainList[Temp].OrigByte[0];
                            RestByte = 1;
                        }

                    }
                    //Save Data
                    {

                        //Bitsave
                        {
                            int bs = 0;
                            BitArray savbits = new BitArray(savbitList.Count * 17);
                            foreach (BitArray bitArr in savbitList)
                            {
                                foreach (bool b in bitArr)
                                {
                                    savbits[bs] = b; bs++;
                                }
                            }
                            byte[] savbitsbyte = new byte[(savbits.Count / 8) + 1];
                            savbits.CopyTo(savbitsbyte, 0);
                            Bitsfiling.Write(savbitsbyte, 0, bs / 8);

                            int start = (bs / 8) * 8, end = bs;

                            BitArray TempBit = new BitArray(bs % 8);
                            bs = 0;
                            while (start != end)
                            {
                                TempBit[bs] = savbits[start]; bs++; start++;
                            }
                            savbitList = new List<BitArray>();
                            if (TempBit.Count != 0)
                                savbitList.Add(TempBit);


                            savbits = null;
                            savbitsbyte = null;

                        }

                    }


                }//End ProcessTimer2


                //Save RestData And Info
                {
                  


                    //AddBits
                    if (savbitList.Count != 0)
                    {
                        int bs = savbitList[0].Count;
                        Addbits = 8 - (Convert.ToInt32(bs % 8));
                        BitArray bitrest = savbitList[0];


                        byte[] restbitD = new byte[1];
                        bitrest.CopyTo(restbitD, 0);
                        Bitsfiling.Write(restbitD, 0, 1);

                    }
                    Bitsfiling.WriteByte(numoperation2.int32toByte1(Addbits));
                    //RestByte
                    
                    //if (RestByte != 0)
                    //{
                    //    Bitsfiling.WriteByte(lastByt[0]);
                    //}
                    Bitsfiling.WriteByte(numoperation2.int32toByte1(RestByte));
                    Bitsfiling.WriteByte(lastByt[0]);
                }

                //Close Files
                filecomp.Close();
                Bitsfiling.Close();

            }//End Files


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - startTime;

            sb.Append("Time End CompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));

        }


        public void ExmineFileBy2_Way3(string filingPath)
        {

            // 01
            if (MessageBox.Show("Do You Want Analysis ? ", "Ask", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                WalkThroListSortBy2_Way3(filingPath);
                //02
                RePort = new StringBuilder();
                AnalysisListBy2_Way3();

                if (MessageBox.Show(RePort.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    StartCompBy2_Way3(filingPath);

                }

                return;
            }
            else
            {
                Tr.creatInfoListBy2_Way3();
                Tr.EditinfoListTo_Way2();
                if (MessageBox.Show(RePort.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    StartCompBy2_Way3(filingPath);

                }

            }
        }
        private void WalkThroListSortBy2_Way3(string filingPath)
        {
            DateTime start = DateTime.Now;
            sb.Append("\nTime Start WalkThroListSortBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            using (var filing = new FileStream(filingPath, FileMode.Open, FileAccess.Read))
            {
                origFilelength = filing.Length;

                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);


                //CreateList
                MainList = new List<SortAndChangerNode2>();

                Tr.creatBy2(ref MainList);
                bool isFirst = true;
                int Temp = 0;

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {

                    byte[] datafile1 = new byte[52428800];


                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filing.Read(datafile1, 0, 52428800);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                                MainList[Temp].nextTree[n].LevelCount++;
                                isFirst = true;
                            }
                        }

                        Process1++;
                    }

                } //End ProcessTimer1

                //Start ProcessTimer2
                if (ProcessTimer2 != 0)
                {

                    byte[] datafile2 = new byte[ProcessTimer2];

                    filing.Read(datafile2, 0, ProcessTimer2);

                    foreach (int n in datafile2)
                    {
                        if (isFirst == true)
                        {
                            Temp = n;
                            isFirst = false;
                        }
                        else
                        {
                            MainList[Temp].nextTree[n].LevelCount++;
                            isFirst = true;
                        }
                    }

                }//End ProcessTimer2


                //LastByte
                if (isFirst == false)
                {
                    //   MainList[Temp].TotalCount++;
                    MainList[Temp].LevelCount++;
                }

                filing.Close();
            }


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End WalkThroListSortBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));
        }
        public void AnalysisListBy2_Way3()
        {

            DateTime start = DateTime.Now;
            sb.Append("\nTime Start AnalysisListBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            List<SortAndChangerNode2> TempTreeList = new List<SortAndChangerNode2>();
            List<byte[]> DicByte = new List<byte[]>();

            //FillList
            {
                foreach (SortAndChangerNode2 n in MainList)
                {
                    foreach (SortAndChangerNode2 m in n.nextTree)
                    {
                        TempTreeList.Add(m);
                    }

                }
                //SortTemp
             //     Tr.quicksort(ref TempTreeList, 0, TempTreeList.Count - 1);
            }




            //Pre Analysis 02
            {
                List<InfoSort2Node> TempListInfo = new List<InfoSort2Node>();
                Tr.creatInfoListBy2_Way3();

                //FillInfoList
                {
                    foreach (InfoSort2Node n in Tr.MainListInfo)
                    {
                        foreach (InfoSort2Node m in n.NextList)
                        {
                            TempListInfo.Add(m);
                        }
                    }
                    //SortTemp
                 //        Tr.quicksort(ref TempListInfo, 0, TempListInfo.Count - 1);

                }

              //  if (MessageBox.Show(TempTreeList.Count.ToString() + " = " + TempListInfo.Count.ToString(), "Size Of Lists", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                

                //FirstCal
                {
                    long SecoundSize = 0;
                    int L = TempTreeList.Count - 1;


                    for (int i = 0; i != TempTreeList.Count; i++)
                    {

                        SecoundSize = SecoundSize + ((TempListInfo[i].NumOfBit) * (TempTreeList[L].LevelCount));


                        L--;

                    }

                    {
                        int PreSizeSecound = Convert.ToInt32((origFilelength / 2));
                        int SecoundAfterCo = Convert.ToInt32(SecoundSize / 8);
                        long LastSize = PreSizeSecound + SecoundAfterCo;

                        long SumReduce = origFilelength - LastSize;

                        StringBuilder xx = new StringBuilder();
                        xx.Append(
                              "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
                              "\nPreSizeSecound = " + PreSizeSecound.ToString() + "  =  " + (PreSizeSecound / 1024).ToString() + " KB " + "  =  " + (PreSizeSecound / 1048576).ToString() + " MB " +
                              "\nSecoundAfterCo = " + SecoundAfterCo.ToString() + "  =  " + (SecoundAfterCo / 1024).ToString() + " KB " + "  =  " + (SecoundAfterCo / 1048576).ToString() + " MB " +
                              "\nSumReduce = " + (SumReduce).ToString() + "  = " + ((SumReduce) / 1024).ToString() + " KB " + "  =  " + ((SumReduce) / 1048576).ToString() + " MB " +
                              "\n\n\n" +
                              "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +
                              "\n\n\n\n");

                        RePort.Append(xx.ToString() +
                            "\n\n\n"
                          );

                        Tr.EditinfoListTo_Way2();


                        // RePort.Append(xx.ToString());

                        //if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        //    return;

                    }
                }
            }



            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End AnalysisListBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));



        









        }
        public void StartCompBy2_Way3(string pathFile)
        {
            //if (countenu == true)
            //{


            //    if (MessageBox.Show("Start SortComp =  Yes  😄😄😄😄  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //        return;



            //}
            //else
            //{
            //    if (MessageBox.Show(" You Can't Contenue  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //        return;
            //    //    MessageBox.Show(" You Can't Contenue  !!");

            //}


            // MessageBox.Show(" Yes  😄😄😄😄  !!");

            FileInfo fil = new FileInfo(pathFile);
            string FileCompDerct = fil.FullName.Remove(fil.FullName.Length - fil.Extension.Length);
            Directory.CreateDirectory(FileCompDerct);

            //Filing
            //string ForByte = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bytF";
            string ForBits = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bitsFW2";



            CompfilingBy2_Way3(pathFile, ForBits);




        }
        private void CompfilingBy2_Way3(string pathfile, string ForBits)
        {
            DateTime startTime = DateTime.Now;
            sb.Append("\nTime Start CompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            using (var filecomp = new FileStream(pathfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var Bitsfiling = new FileStream(ForBits, FileMode.OpenOrCreate, FileAccess.Write))
            {


                List<InfoSort2Node> Mininfolist = Tr.MainListInfo;

                //InfoRest
                int Addbits = 0;
                int RestByte = 0;
                byte[] lastByt = new byte[1];
                lastByt[0] = numoperation2.int32toByte1(0);

                //Seek Files
                filecomp.Seek(0, SeekOrigin.Begin);
                Bitsfiling.Seek(0, SeekOrigin.Begin);

                //FileLength     50MB = 52428800  10MB = 10485760 

                long Filelength = filecomp.Length;

                int ProcessTimer1 = Convert.ToInt32(Filelength / 10485760);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 10485760);

                bool isFirst = true;
                int Temp = 0;


                List<BitArray> savbitList = new List<BitArray>();

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {
                    byte[] datafile1 = new byte[10485760];

                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filecomp.Read(datafile1, 0, 10485760);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                                savbitList.Add(Mininfolist[Temp].NextList[n].WriteBitArr);

                                isFirst = true;
                            }
                        }

                        //Save Data
                        {
                            //Bitsave
                            {
                                int bs = 0;
                                BitArray savbits = new BitArray(savbitList.Count * 17);
                                foreach (BitArray bitArr in savbitList)
                                {
                                    foreach (bool b in bitArr)
                                    {
                                        savbits[bs] = b; bs++;
                                    }
                                }
                                byte[] savbitsbyte = new byte[(savbits.Count / 8) + 1];
                                savbits.CopyTo(savbitsbyte, 0);
                                Bitsfiling.Write(savbitsbyte, 0, bs / 8);

                                int start = (bs / 8) * 8, end = bs;

                                BitArray TempBit = new BitArray(bs % 8);
                                bs = 0;
                                while (start != end)
                                {
                                    TempBit[bs] = savbits[start]; bs++; start++;
                                }
                                savbitList = new List<BitArray>();
                                if (TempBit.Count != 0)
                                    savbitList.Add(TempBit);

                                savbits = null;
                                savbitsbyte = null;

                            }

                        }

                        Process1++;
                    }
                }//End ProcessTimer1

                //Start ProcessTimer2
                if (ProcessTimer2 != 0)
                {
                    byte[] datafile2 = new byte[ProcessTimer2];
                    filecomp.Read(datafile2, 0, ProcessTimer2);

                    foreach (int n in datafile2)
                    {
                        if (isFirst == true)
                        {
                            Temp = n;
                            isFirst = false;
                        }
                        else
                        {
                            savbitList.Add(Mininfolist[Temp].NextList[n].WriteBitArr);

                            isFirst = true;
                        }
                    }

                    //LastPart
                    {
                        if (isFirst == false)
                        {
                            // lastByt = new byte[1];
                        //    lastByt[0] = MainList[Temp].OrigByte[0];
                            RestByte = 1;
                        }

                    }
                    //Save Data
                    {

                        //Bitsave
                        {
                            int bs = 0;
                            BitArray savbits = new BitArray(savbitList.Count * 17);
                            foreach (BitArray bitArr in savbitList)
                            {
                                foreach (bool b in bitArr)
                                {
                                    savbits[bs] = b; bs++;
                                }
                            }
                            byte[] savbitsbyte = new byte[(savbits.Count / 8) + 1];
                            savbits.CopyTo(savbitsbyte, 0);
                            Bitsfiling.Write(savbitsbyte, 0, bs / 8);

                            int start = (bs / 8) * 8, end = bs;

                            BitArray TempBit = new BitArray(bs % 8);
                            bs = 0;
                            while (start != end)
                            {
                                TempBit[bs] = savbits[start]; bs++; start++;
                            }
                            savbitList = new List<BitArray>();
                            if (TempBit.Count != 0)
                                savbitList.Add(TempBit);


                            savbits = null;
                            savbitsbyte = null;

                        }

                    }


                }//End ProcessTimer2


                //Save RestData And Info
                {



                    //AddBits
                    if (savbitList.Count != 0)
                    {
                        int bs = savbitList[0].Count;
                        Addbits = 8 - (Convert.ToInt32(bs % 8));
                        BitArray bitrest = savbitList[0];


                        byte[] restbitD = new byte[1];
                        bitrest.CopyTo(restbitD, 0);
                        Bitsfiling.Write(restbitD, 0, 1);

                    }
                    Bitsfiling.WriteByte(numoperation2.int32toByte1(Addbits));
                    //RestByte

                    //if (RestByte != 0)
                    //{
                    //    Bitsfiling.WriteByte(lastByt[0]);
                    //}
                    Bitsfiling.WriteByte(numoperation2.int32toByte1(RestByte));
                    Bitsfiling.WriteByte(lastByt[0]);
                }

                //Close Files
                filecomp.Close();
                Bitsfiling.Close();

            }//End Files


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - startTime;

            sb.Append("Time End CompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));

        }



    }



    public class DeSortAndChangerNode2
    {
        public DeSortAndChangerNode2 nextzero;
        public DeSortAndChangerNode2 nextone;
        public byte[] TempWritByt;


        /**************** For Error ***********/
        public int PreNum = 0;
        public int CurrentNum = 0;



        public DeSortAndChangerNode2()
        {
            nextzero = null;
            nextone = null;
        }

    }

    public class DeTreeSortAndChanger2
    {
        public StringBuilder sb;
        private List<DeSortAndChangerNode2> ErrorList = new List<DeSortAndChangerNode2>();

        private int CountOneIsNull = 0;
        private int CountZeroIsNull = 0;
        private int CountPothIsNull = 0;

        public DeSortAndChangerNode2 root;

        public DeTreeSortAndChanger2()
        {
            root = new DeSortAndChangerNode2();
        }

        public void CreatBy2_Way2()
        {

            ListSortAndChanger2 Tr = new ListSortAndChanger2();
            Tr.creatInfoListBy2();
            Tr.EditinfoListTo_Way2();

            DeSortAndChangerNode2 po = new DeSortAndChangerNode2();
            po.nextzero = root;

            //BuildTree
            {
                foreach (InfoSort2Node n in Tr.MainListInfo)
                {
                    foreach (InfoSort2Node m in n.NextList)
                    {
                        po.nextzero = root;

                        foreach (bool b in m.WriteBitArr)
                        {
                            if (b == true)
                            {
                                if (po.nextzero.nextone == null)
                                {
                                    po.nextzero.nextone = (new DeSortAndChangerNode2());
                                    po.nextzero = po.nextzero.nextone;
                                }
                                else
                                {
                                    po.nextzero = po.nextzero.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero.nextzero == null)
                                {
                                    po.nextzero.nextzero = (new DeSortAndChangerNode2());
                                    po.nextzero = po.nextzero.nextzero;
                                }
                                else
                                {
                                    po.nextzero = po.nextzero.nextzero;
                                }
                            }
                        }
                        po.nextzero.TempWritByt = new byte[2];
                        po.nextzero.TempWritByt[0] = numoperation2.int32toByte1(m.PreRealValue);
                        po.nextzero.TempWritByt[1] = numoperation2.int32toByte1(m.CurrentValuListlValue);
                    }
                }
            }



          //  Tr = null;


        }

        public void CreatBy2_Way3()
        {

            ListSortAndChanger2 Tr = new ListSortAndChanger2();
            Tr.creatInfoListBy2_Way3();
            Tr.EditinfoListTo_Way2();

            DeSortAndChangerNode2 po = new DeSortAndChangerNode2();
            po.nextzero = root;

            //BuildTree
            {
                foreach (InfoSort2Node n in Tr.MainListInfo)
                {
                    foreach (InfoSort2Node m in n.NextList)
                    {
                        po.nextzero = root;

                        foreach (bool b in m.WriteBitArr)
                        {
                            if (b == true)
                            {
                                if (po.nextzero.nextone == null)
                                {
                                    po.nextzero.nextone = (new DeSortAndChangerNode2());
                                    po.nextzero = po.nextzero.nextone;

                                    /**********For Error *******/
                                    po.nextzero.PreNum = m.PreRealValue;
                                    po.nextzero.CurrentNum = m.CurrentValuListlValue;
                                }
                                else
                                {
                                    po.nextzero = po.nextzero.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero.nextzero == null)
                                {
                                    po.nextzero.nextzero = (new DeSortAndChangerNode2());
                                    po.nextzero = po.nextzero.nextzero;

                                    /**********For Error *******/
                                    po.nextzero.PreNum = m.PreRealValue;
                                    po.nextzero.CurrentNum = m.CurrentValuListlValue;
                                }
                                else
                                {
                                    po.nextzero = po.nextzero.nextzero;
                                }
                            }
                        }
                        po.nextzero.TempWritByt = new byte[2];
                        po.nextzero.TempWritByt[0] = numoperation2.int32toByte1(m.PreRealValue);
                        po.nextzero.TempWritByt[1] = numoperation2.int32toByte1(m.CurrentValuListlValue);

                        /**********For Error *******/
                        po.nextzero.PreNum = m.PreRealValue;
                        po.nextzero.CurrentNum = m.CurrentValuListlValue;
                    }
                }
            }



            //  Tr = null;


        }


        public string PrintInfoTree()
        {
            CreatBy2_Way3();
            sb = new StringBuilder();

            CountTeePartNull(root);

            sb.Append("\n\n\n" +
                "\nCountOneIsNull  = " + CountOneIsNull.ToString() +
                "\nCountZeroIsNull = " + CountZeroIsNull.ToString() +
                "\nCountPothIsNull = " +CountPothIsNull.ToString()+

                "\n\n\n");

            foreach (DeSortAndChangerNode2 nod in ErrorList)
            {
                sb.Append(
                    "\nPreNum = " + nod.PreNum.ToString() +
                    "\nCurNum = " + nod.CurrentNum.ToString() +

                    "\n\n\n");
                     
                     
            }




            return sb.ToString();

        }
        private void CountTeePartNull(DeSortAndChangerNode2 Cr)
        {

            if (Cr.nextzero == null || Cr.nextone == null)
            {
                if (Cr.nextzero != null && Cr.nextone == null)
                {
                    CountZeroIsNull++;
                    ErrorList.Add(Cr);
                    CountTeePartNull(Cr.nextzero);
                }
                else
                {
                    if (Cr.nextzero == null && Cr.nextone != null)
                    {
                        CountOneIsNull++;
                        ErrorList.Add(Cr);
                        CountTeePartNull(Cr.nextone);
                    }
                    else
                    {
                        CountPothIsNull++;
                    }
                }
               
            }
            else
            {
                CountTeePartNull(Cr.nextzero);
                CountTeePartNull(Cr.nextone);
            }


        }

    }

    public class DeCompSortAndChanger2
    {
        public StringBuilder sb;
        private byte[] num = new byte[5];

        private DeTreeSortAndChanger2 Tr;


        public DeCompSortAndChanger2()
        {
            num[0] = numoperation2.int32toByte1(0);
            num[1] = numoperation2.int32toByte1(1);
            num[2] = numoperation2.int32toByte1(2);
            num[3] = numoperation2.int32toByte1(3);
            num[4] = numoperation2.int32toByte1(4);
            sb = new StringBuilder();

            Tr = new DeTreeSortAndChanger2();

        }

        public void StartDeCompBy2_Way2(string pathFile, string ExtensionFiling)
        {

            FileInfo info = new FileInfo(pathFile);
            string SaveFile = info.FullName.Remove(info.FullName.Length - info.Extension.Length)+ "."+ExtensionFiling;

            DeCompfilingBy2_Way2(SaveFile, pathFile);

        }
        public void StartDeCompBy2_Way2(string pathFile)
        {

            FileInfo info = new FileInfo(pathFile);
            string SaveFile = info.FullName.Remove(info.FullName.Length - info.Extension.Length) + ".DCS2W2";

            DeCompfilingBy2_Way2(SaveFile, pathFile);


        }
        private void DeCompfilingBy2_Way2(string SaveFile, string Comfilepath)
        {
            Tr.CreatBy2_Way2();
            DeSortAndChangerNode2 root = Tr.root;


            DateTime startTime = DateTime.Now;
            sb.Append("\nTime Start DeCompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



            using (var Comfiling = new FileStream(Comfilepath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            using (var saveFiling = new FileStream(SaveFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                //InfoRest
                int Addbit = 0;
                int NumOfRestByt = 0;
                byte[] restbyt = new byte[1];

                //FilesLength
                long Comfilinglength = Comfiling.Length - 3;

                //get Info
                {
                    byte[] infofil = new byte[3];
                    Comfiling.Seek((Comfilinglength), SeekOrigin.Begin);
                    Comfiling.Read(infofil, 0, 3);


                    //Fill Rest
                    Addbit = Convert.ToInt32(infofil[0]);
                    NumOfRestByt = Convert.ToInt32(infofil[1]);
                    restbyt[0] = infofil[2];

                }//End Get Info



                //Seek Files
                Comfiling.Seek(0, SeekOrigin.Begin);
                saveFiling.Seek(0, SeekOrigin.Begin);


                //Count Process
                //FileLength     50MB = 52428800  10MB = 10485760 
                int ProcessTimer1 = Convert.ToInt32(Comfilinglength / 10485760);
                int ProcessTimer2 = Convert.ToInt32(Comfilinglength % 10485760);



                DeSortAndChangerNode2 po;//  =new DeSortAndChangerNode2();



                po = root;



                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {
                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        byte[] dataRead = new byte[10485760];
                        Comfiling.Read(dataRead, 0, 10485760);

                        BitArray bitdata = new BitArray(dataRead);

                        foreach (bool b in bitdata)
                        {
                            if (b == true)
                            {
                                if (po.nextone == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextone;
                                }
                                else
                                {
                                    po = po.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextzero;
                                }
                                else
                                {
                                    po = po.nextzero;
                                }
                            }

                        }

                        Process1++;
                    }
                }//End ProcessTimer1



                //Start Process 2
                if (ProcessTimer2 != 0)
                {
                    byte[] dataRead = new byte[ProcessTimer2];
                    Comfiling.Read(dataRead, 0, ProcessTimer2);

                    BitArray bitdata = new BitArray(dataRead);

                    foreach (bool b in bitdata)
                    {
                        if (b == true)
                        {
                            if (po.nextone == null)
                            {
                                saveFiling.Write(po.TempWritByt, 0, 2);
                                po = root.nextone;
                            }
                            else
                            {
                                po = po.nextone;
                            }
                        }
                        else
                        {
                            if (po.nextzero == null)
                            {
                                saveFiling.Write(po.TempWritByt, 0, 2);
                                po = root.nextzero;
                            }
                            else
                            {
                                po = po.nextzero;
                            }
                        }

                    }

                } //End of Process 2


                //LastPart
                {
                    //RestBits
                    {
                        byte[] restBits = new byte[1];
                        Comfiling.Read(restBits, 0, 1);
                        BitArray bits = new BitArray(restBits);
                        int i = 0;
                        while (i < (8 - Addbit))
                        {
                            if (bits[i] == true)
                            {
                                if (po.nextone == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextone;
                                }
                                else
                                {
                                    po = po.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextzero;
                                }
                                else
                                {
                                    po = po.nextzero;
                                }
                            }

                            i++;
                        }

                        if(po.TempWritByt!=null)
                            saveFiling.Write(po.TempWritByt, 0, 2);

                    }

                    //Restbyte
                    if (NumOfRestByt != 0)
                        saveFiling.WriteByte(restbyt[0]);
                    


                }



                Comfiling.Close();
                saveFiling.Close();
            }


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - startTime;

            sb.Append("Time End DeCompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));

        }


        public void StartDeCompBy2_Way3(string pathFile, string ExtensionFiling)
        {

            FileInfo info = new FileInfo(pathFile);
            string SaveFile = info.FullName.Remove(info.FullName.Length - info.Extension.Length) + "." + ExtensionFiling;

            DeCompfilingBy2_Way3(SaveFile, pathFile);

        }
        public void StartDeCompBy2_Way3(string pathFile)
        {

            FileInfo info = new FileInfo(pathFile);
            string SaveFile = info.FullName.Remove(info.FullName.Length - info.Extension.Length) + "."+"DCS2W2";

            DeCompfilingBy2_Way3(SaveFile, pathFile);


        }
        private void DeCompfilingBy2_Way3(string SaveFile, string Comfilepath)
        {
            Tr.CreatBy2_Way3();
            DeSortAndChangerNode2 root = Tr.root;


            DateTime startTime = DateTime.Now;
            sb.Append("\nTime Start DeCompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



            using (var Comfiling = new FileStream(Comfilepath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            using (var saveFiling = new FileStream(SaveFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                //InfoRest
                int Addbit = 0;
                int NumOfRestByt = 0;
                byte[] restbyt = new byte[1];

                //FilesLength
                long Comfilinglength = Comfiling.Length - 3;

                //get Info
                {
                    byte[] infofil = new byte[3];
                    Comfiling.Seek((Comfilinglength), SeekOrigin.Begin);
                    Comfiling.Read(infofil, 0, 3);


                    //Fill Rest
                    Addbit = Convert.ToInt32(infofil[0]);
                    NumOfRestByt = Convert.ToInt32(infofil[1]);
                    restbyt[0] = infofil[2];

                }//End Get Info



                //Seek Files
                Comfiling.Seek(0, SeekOrigin.Begin);
                saveFiling.Seek(0, SeekOrigin.Begin);


                //Count Process
                //FileLength     50MB = 52428800  10MB = 10485760 
                int ProcessTimer1 = Convert.ToInt32(Comfilinglength / 10485760);
                int ProcessTimer2 = Convert.ToInt32(Comfilinglength % 10485760);



                DeSortAndChangerNode2 po;//  =new DeSortAndChangerNode2();



                po = root;



                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {
                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        byte[] dataRead = new byte[10485760];
                        Comfiling.Read(dataRead, 0, 10485760);

                        BitArray bitdata = new BitArray(dataRead);

                        foreach (bool b in bitdata)
                        {
                            if (b == true)
                            {
                                if (po.nextone == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextone;
                                }
                                else
                                {
                                    po = po.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextzero;
                                }
                                else
                                {
                                    po = po.nextzero;
                                }
                            }

                        }

                        Process1++;
                    }
                }//End ProcessTimer1



                //Start Process 2
                if (ProcessTimer2 != 0)
                {
                    byte[] dataRead = new byte[ProcessTimer2];
                    Comfiling.Read(dataRead, 0, ProcessTimer2);

                    BitArray bitdata = new BitArray(dataRead);

                    foreach (bool b in bitdata)
                    {
                        if (b == true)
                        {
                            if (po.nextone == null)
                            {
                                saveFiling.Write(po.TempWritByt, 0, 2);
                                po = root.nextone;
                            }
                            else
                            {
                                po = po.nextone;
                            }
                        }
                        else
                        {
                            if (po.nextzero == null)
                            {
                                saveFiling.Write(po.TempWritByt, 0, 2);
                                po = root.nextzero;
                            }
                            else
                            {
                                po = po.nextzero;
                            }
                        }

                    }

                } //End of Process 2


                //LastPart
                {
                    //RestBits
                    {
                        byte[] restBits = new byte[1];
                        Comfiling.Read(restBits, 0, 1);
                        BitArray bits = new BitArray(restBits);
                        int i = 0;
                        while (i < (8 - Addbit))
                        {
                            if (bits[i] == true)
                            {
                                if (po.nextone == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextone;
                                }
                                else
                                {
                                    po = po.nextone;
                                }
                            }
                            else
                            {
                                if (po.nextzero == null)
                                {
                                    saveFiling.Write(po.TempWritByt, 0, 2);
                                    po = root.nextzero;
                                }
                                else
                                {
                                    po = po.nextzero;
                                }
                            }

                            i++;
                        }

                        if (po.TempWritByt != null)
                            saveFiling.Write(po.TempWritByt, 0, 2);

                    }

                    //Restbyte
                    if (NumOfRestByt != 0)
                        saveFiling.WriteByte(restbyt[0]);



                }



                Comfiling.Close();
                saveFiling.Close();
            }


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - startTime;

            sb.Append("Time End DeCompfilingBy2_Way2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));

        }







    }

}
