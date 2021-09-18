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
    public class SortAndChangerNode
    {
        /*********Orig***********/

        public SortAndChangerNode nextNode;
        public List<SortAndChangerNode> nextTree;

        public byte[] OrigByte;
        public int PreRealValue = 0;
        public int RealValue = 0;
        public int RealListlValue = 0;

        public int TotalCount;
        public int LevelCount;


        /********** Big To Small*****/ 

        public bool IsOpen;
        public bool IsSorted;
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



        public SortAndChangerNode()
        {
            IsOpen = false;
            IsSorted = false;
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
    public class TempPreAnalysisSortAndChanger
    {
        /************* Temp *********/

        public byte[] TempOrigByte;

        public int LevelCount;

        public int PreRealValue = 0;
        public int RealListlValue = 0;

        public TempPreAnalysisSortAndChanger()
        {
            LevelCount = 0;
        }



    }

    public class ListSortAndChanger
    {
        private int NumOfNodes1;
        private int NumOfNodes2;


        public List<SortAndChangerNode> listRoot;
        public int ModNum = 0;

        public ListSortAndChanger()
        {
            NumOfNodes1 = 0;
            NumOfNodes2 = 0;
        }

        private SortAndChangerNode newNode()
        {
            SortAndChangerNode newnod = new SortAndChangerNode();

            return newnod;
        }


        //By2
        private void CreatNextListBy2(ref List<SortAndChangerNode> Nextlist, byte PreByte, int PreNum, int BitsCount)
        {
            int i = 0;
            while (i != 256)
            {
                SortAndChangerNode newnod = new SortAndChangerNode();
                newnod.RealValue = NumOfNodes2; NumOfNodes2++;
                newnod.OrigByte = new byte[2];
                newnod.OrigByte[0] = PreByte;
                newnod.OrigByte[1] = numoperation2.int32toByte1(i);

                newnod.TempBitsArr = new BitArray(0);

                newnod.RealListlValue = i;
                newnod.PreRealValue = PreNum;

                if (PreNum >= i)
                {
                    newnod.IsSorted = true;
             
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


                    newnod.IsSorted = false;
               
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
        public void creatBy2(ref List<SortAndChangerNode> thislist)
        {
            thislist = new List<SortAndChangerNode>();
            ModNum = 2;
            int i = 0;
            while (i != 256)
            {
                SortAndChangerNode newnod = new SortAndChangerNode();
                newnod.OrigByte = new byte[1];
                newnod.OrigByte[0] = numoperation2.int32toByte1(i);
                newnod.RealValue = NumOfNodes1; NumOfNodes1++;
                newnod.nextTree = new List<SortAndChangerNode>();

                newnod.TempBitsArr = new BitArray(0);

                BitArray BitNum = numoperation2.intvaluToBitsArr(i);
                newnod.RealListlValue = i;

                CreatNextListBy2(ref newnod.nextTree, newnod.OrigByte[0], i, BitNum.Count);

                thislist.Add(newnod);

                i++;
            }


        }


        public void quicksort(ref List<TempPreAnalysisSortAndChanger> list, int begin, int end)
        {
            TempPreAnalysisSortAndChanger pivot = list[(begin + (end - begin) / 2)];
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
        public void swap(ref List<TempPreAnalysisSortAndChanger> list, int x, int y)
        {
            TempPreAnalysisSortAndChanger temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }


        public void quicksort(ref List<SortAndChangerNode> list, int begin, int end)
        {
            SortAndChangerNode pivot = list[(begin + (end - begin) / 2)];
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
        public void swap(ref List<SortAndChangerNode> list, int x, int y)
        {
            SortAndChangerNode temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }



        public void AddIsSortBitForBitArry( SortAndChangerNode node, bool stateBit)
        {
            BitArray bitArr = node.TempBitsArr;

            BitArray newbitArr = new BitArray(bitArr.Count + 1);
            int i = 0;
            foreach (bool b in bitArr)
            {
                newbitArr[i] = b; i++;
            }
            newbitArr[i] = stateBit;


            node.TempBitsArr = newbitArr;
        }


    }

  




    public class CompSortAndChanger
    {
        public StringBuilder sb;
        public StringBuilder RePort;
        private List<List<byte[]>> ListDic;

        private List<SortAndChangerNode> MainList;

        private ListSortAndChanger Tr;
        public int mod;
        private byte[] num = new byte[5];

        public long origFilelength = 0;

        public bool countenu = false;


        public CompSortAndChanger()
        {
            sb = new StringBuilder();
            RePort = new StringBuilder();
            Tr = new ListSortAndChanger();
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
            AnalysisListBy2_FirstTime();
  

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
                MainList = new List<SortAndChangerNode>();

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


        public void AnalysisListBy2_FirstTime()
        {

            DateTime start = DateTime.Now;
            sb.Append("\nTime Start AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            List<TempPreAnalysisSortAndChanger> TempPreAnalysislist = new List<TempPreAnalysisSortAndChanger>();
            List<byte[]> DicByte = new List<byte[]>();

           
            ////Pre Analysis 01
            //{
            

            //    //001 Here Is Need To Fill First Dic
            //    for (int i = 0; i != 256; i++)
            //    {
            //        for (int n = 0; n != 256; n++)
            //        {
            //            if (MainList[i].nextTree[n].IsRead == false)
            //            {
            //                TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
            //                if (i == n)
            //                {
            //                    newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
            //                    newnode.LevelCount = MainList[i].nextTree[n].LevelCount;
            //                    MainList[i].nextTree[n].IsRead = true;

            //                    newnode.PreRealValue = MainList[i].nextTree[n].PreRealValue;
            //                    newnode.RealListlValue = MainList[i].nextTree[n].RealListlValue;
            //                }
            //                else
            //                {
            //                    int count = MainList[i].nextTree[n].LevelCount;
            //                    count = count + MainList[n].nextTree[i].LevelCount;

            //                    newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
            //                    newnode.LevelCount = count;
            //                    MainList[i].nextTree[n].IsRead = true;
            //                    MainList[n].nextTree[i].IsRead = true;

            //                    newnode.PreRealValue = MainList[i].nextTree[n].PreRealValue;
            //                    newnode.RealListlValue = MainList[i].nextTree[n].RealListlValue;

            //                  //  DicByte.Add(MainList[i].nextTree[n].OrigByte);
            //                }
            //                TempPreAnalysislist.Add(newnode);
            //            }
            //        }
            //    }
            //    //SortTemp

            //    Tr.quicksort(ref TempPreAnalysislist, 0, TempPreAnalysislist.Count - 1);
            //    //if (MessageBox.Show(TempPreAnalysislist.Count.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //    //    return;

            //    //FirstCal
            //    {
            //        long FirstReduce = 0;
            //        int L = TempPreAnalysislist.Count - 1;

            //        for (int i = 0; i != 128; i++)
            //        {
            //            DicByte.Add(TempPreAnalysislist[L].TempOrigByte);

            //            BitArray bitnum = numoperation2.intvaluToBitsArr(i);
            //            int BitLength = bitnum.Count;
            //            int Timer = Convert.ToInt32(Math.Pow(2, bitnum.Count));
            //            int modRed = 8 - bitnum.Count;

            //            int n = 0;
            //            while (n != Timer)
            //            {
            //                byte[] bytW = new byte[1];
            //                bytW[0] = numoperation2.int32toByte1(i);
            //                BitArray Bits = numoperation2.intvaluToBitsArr(n, BitLength);

            //                MainList[TempPreAnalysislist[L].PreRealValue].nextTree[TempPreAnalysislist[L].RealListlValue].TempWritByt = bytW;
            //                MainList[TempPreAnalysislist[L].PreRealValue].nextTree[TempPreAnalysislist[L].RealListlValue].TempBitsArr = Bits;

                            


            //                int Reduceit = TempPreAnalysislist[L].LevelCount * modRed;
            //                FirstReduce = FirstReduce + Reduceit;

            //                L--;
            //                n++;
            //            }
            //        }

            //        {
            //            int FirstTotalAdd = Convert.ToInt32((origFilelength / 2) / 8);
            //            int FirstLastReduce = Convert.ToInt32(FirstReduce / 8) - FirstTotalAdd;
            //            StringBuilder xx = new StringBuilder();
            //            xx.Append(
            //                  "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
            //                  "\nTotalAdd = " + FirstTotalAdd.ToString() + "  =  " + (FirstTotalAdd / 1024).ToString() + " KB " + "  =  " + (FirstTotalAdd / 1048576).ToString() + " MB " +
            //                  "\nLastReduce = " + FirstLastReduce.ToString() + "  =  " + (FirstLastReduce / 1024).ToString() + " KB " + "  =  " + (FirstLastReduce / 1048576).ToString() + " MB " +
            //                  "\nFirstReduce = " + (FirstReduce / 8).ToString() + "  = " + ((FirstReduce / 8) / 1024).ToString() + " KB " + "  =  " + ((FirstReduce / 8) / 1048576).ToString() + " MB ");


            //            if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //                return;

            //        }
            //    }
            //}
            ////End


            //Pre Analysis 02
            {

                //FillList
                {
                    List<TempPreAnalysisSortAndChanger> TempPreAnalysislist2 = new List<TempPreAnalysisSortAndChanger>();

                    int Counter = 0;

                    foreach (SortAndChangerNode n in MainList)
                    {
                        Counter = 0;
                        foreach (SortAndChangerNode m in n.nextTree)
                        {
                            Counter = Counter + m.LevelCount;
                            TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
                            newnode.TempOrigByte = m.OrigByte;
                            newnode.LevelCount = m.LevelCount;
                            TempPreAnalysislist2.Add(newnode);
                        }
                        n.LevelCount = n.LevelCount + Counter;
                    }
                    //SortTemp
                    Tr.quicksort(ref TempPreAnalysislist2, 0, TempPreAnalysislist2.Count - 1);

                    //Edit
                    int L = TempPreAnalysislist2.Count - 1;
                    for (int i = 0; i != 256; i++)
                    {
                        for (int n = 0; n != 256; n++)
                        {
                            if (i != n)
                            {
                                if (MainList[i].nextTree[n].IsWrite == false)
                                {

                                    MainList[i].nextTree[n].LevelCount = TempPreAnalysislist2[L].LevelCount;
                                    MainList[i].nextTree[n].IsWrite = true;
                                    L--;
                                    MainList[n].nextTree[i].LevelCount = TempPreAnalysislist2[L].LevelCount;
                                    MainList[n].nextTree[i].IsWrite = true;
                                    L--;
                                }
                            }

                            else
                            {

                            }

                        }
                    }
                    for (int i = 0; i != 256; i++)
                    {
                        MainList[i].nextTree[i].LevelCount = TempPreAnalysislist2[L].LevelCount;
                        MainList[i].nextTree[i].IsWrite = true;
                        L--;
                    }
                    TempPreAnalysislist2 = null;
                }


                //001 Here Is Need To Fill First Dic
                for (int i = 0; i != 256; i++)
                {
                    for (int n = 0; n != 256; n++)
                    {
                        if (MainList[i].nextTree[n].IsRead == false)
                        {
                            TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
                            if (i == n)
                            {
                                newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
                                newnode.LevelCount = MainList[i].nextTree[n].LevelCount;
                                MainList[i].nextTree[n].IsRead = true;
                            }
                            else
                            {
                                int count = MainList[i].nextTree[n].LevelCount;
                                count = count + MainList[n].nextTree[i].LevelCount;

                                newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
                                newnode.LevelCount = count;
                                MainList[i].nextTree[n].IsRead = true;
                                MainList[n].nextTree[i].IsRead = true;
                            }
                            TempPreAnalysislist.Add(newnode);
                        }
                    }
                }
                //SortTemp

                Tr.quicksort(ref TempPreAnalysislist, 0, TempPreAnalysislist.Count - 1);

                //FirstCal
                {
                    long FirstReduce = 0;
                    int L = TempPreAnalysislist.Count - 1;

                    for (int i = 0; i != 128; i++)
                    {
                        BitArray bitnum = numoperation2.intvaluToBitsArr(i);
                        int Timer = Convert.ToInt32(Math.Pow(2, bitnum.Count));
                        int modRed = 8 - bitnum.Count;

                        int n = 0;
                        while (n != Timer)
                        {
                            int Reduceit = TempPreAnalysislist[L].LevelCount * modRed;
                            FirstReduce = FirstReduce + Reduceit;

                            L--;
                            n++;
                        }
                    }

                    {
                        int FirstTotalAdd = Convert.ToInt32((origFilelength / 2) / 8);
                        int FirstLastReduce = Convert.ToInt32(FirstReduce / 8) - FirstTotalAdd;
                        long LastSize = origFilelength - FirstLastReduce;
                        StringBuilder xx = new StringBuilder();
                        xx.Append(
                              "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
                              "\nTotalAdd = " + FirstTotalAdd.ToString() + "  =  " + (FirstTotalAdd / 1024).ToString() + " KB " + "  =  " + (FirstTotalAdd / 1048576).ToString() + " MB " +
                              "\nLastReduce = " + FirstLastReduce.ToString() + "  =  " + (FirstLastReduce / 1024).ToString() + " KB " + "  =  " + (FirstLastReduce / 1048576).ToString() + " MB " +
                              "\nFirstReduce = " + (FirstReduce / 8).ToString() + "  = " + ((FirstReduce / 8) / 1024).ToString() + " KB " + "  =  " + ((FirstReduce / 8) / 1048576).ToString() + " MB " +
                              "\n\n\n" +
                              "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +
                              "\n\n\n\n");

                        RePort.Append(xx.ToString()+
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
                foreach (SortAndChangerNode n in MainList)
                {
                    Counter = 0;
                    ReduceCounter = 0;
                    foreach (SortAndChangerNode m in n.nextTree)
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

        public void ExmineFileBy2_Secound(string filingPath)
        {

            // 01 
            WalkThroListSortBy2(filingPath);

            //02
            RePort = new StringBuilder();
            AnalysisListBy2_SecoundTime();
            StartCompBy2_SecoundTime(filingPath, 2);

        }

        public void AnalysisListBy2_SecoundTime()
        {

            DateTime start = DateTime.Now;
            sb.Append("\nTime Start AnalysisListBy2_SecoundTime = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

            List<TempPreAnalysisSortAndChanger> TempPreAnalysislist = new List<TempPreAnalysisSortAndChanger>();
            ListDic = new List<List<byte[]>>();
            List<byte[]> TempDicByte = new List<byte[]>();

            //Pre Analysis 01
            {

                //001 Here Is Need To Fill First Dic
                for (int i = 0; i != 256; i++)
                {
                    for (int n = 0; n != 256; n++)
                    {
                        if (MainList[i].nextTree[n].IsRead == false)
                        {
                            TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
                            if (i == n)
                            {
                                newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
                                newnode.LevelCount = MainList[i].nextTree[n].LevelCount;
                                MainList[i].nextTree[n].IsRead = true;

                                newnode.PreRealValue = MainList[i].nextTree[n].PreRealValue;
                                newnode.RealListlValue = MainList[i].nextTree[n].RealListlValue;
                            }
                            else
                            {
                                int count = MainList[i].nextTree[n].LevelCount;
                                count = count + MainList[n].nextTree[i].LevelCount;

                                newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
                                newnode.LevelCount = count;
                                MainList[i].nextTree[n].IsRead = true;
                                MainList[n].nextTree[i].IsRead = true;

                                newnode.PreRealValue = MainList[i].nextTree[n].PreRealValue;
                                newnode.RealListlValue = MainList[i].nextTree[n].RealListlValue;

                                //  DicByte.Add(MainList[i].nextTree[n].OrigByte);
                            }
                            TempPreAnalysislist.Add(newnode);
                        }
                    }
                }
                //SortTemp

                Tr.quicksort(ref TempPreAnalysislist, 0, TempPreAnalysislist.Count - 1);
                //if (MessageBox.Show(TempPreAnalysislist.Count.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                //    return;

                //FirstCal
                {
                    long FirstReduce = 0;
                    int L = TempPreAnalysislist.Count - 1;

                    for (int i = 0; i != 128; i++)
                    {
                        TempDicByte.Add(TempPreAnalysislist[L].TempOrigByte);

                        BitArray bitnum = numoperation2.intvaluToBitsArr(i);
                        int BitLength = bitnum.Count;
                        int Timer = Convert.ToInt32(Math.Pow(2, bitnum.Count));
                        int modRed = 8 - bitnum.Count;

                        int n = 0;
                        while (n != Timer)
                        {
                            byte[] bytW = new byte[1];
                            bytW[0] = numoperation2.int32toByte1(i);
                            BitArray Bits = numoperation2.intvaluToBitsArr(n, BitLength);

                            MainList[TempPreAnalysislist[L].PreRealValue].nextTree[TempPreAnalysislist[L].RealListlValue].TempWritByt = bytW;
                            MainList[TempPreAnalysislist[L].PreRealValue].nextTree[TempPreAnalysislist[L].RealListlValue].TempBitsArr = Bits;

                            MainList[TempPreAnalysislist[L].RealListlValue].nextTree[TempPreAnalysislist[L].PreRealValue].TempWritByt = bytW;
                            MainList[TempPreAnalysislist[L].RealListlValue].nextTree[TempPreAnalysislist[L].PreRealValue].TempBitsArr = Bits;


                            int Reduceit = TempPreAnalysislist[L].LevelCount * modRed;
                            FirstReduce = FirstReduce + Reduceit;

                            L--;
                            n++;
                        }

                    }

                    //if (MessageBox.Show(L.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    //    return;

                    {
                        int FirstTotalAdd = Convert.ToInt32((origFilelength / 2) / 8);
                        int FirstLastReduce = Convert.ToInt32(FirstReduce / 8) - FirstTotalAdd;
                        long LastSize=origFilelength-FirstLastReduce;
                        StringBuilder xx = new StringBuilder();
                        xx.Append(
                              "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
                              "\nTotalAdd = " + FirstTotalAdd.ToString() + "  =  " + (FirstTotalAdd / 1024).ToString() + " KB " + "  =  " + (FirstTotalAdd / 1048576).ToString() + " MB " +
                              "\nLastReduce = " + FirstLastReduce.ToString() + "  =  " + (FirstLastReduce / 1024).ToString() + " KB " + "  =  " + (FirstLastReduce / 1048576).ToString() + " MB " +
                              "\nFirstReduce = " + (FirstReduce / 8).ToString() + "  = " + ((FirstReduce / 8) / 1024).ToString() + " KB " + "  =  " + ((FirstReduce / 8) / 1048576).ToString() + " MB "+
                              "\n\n\n"+
                              "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "  =  " + (LastSize / 1048576).ToString() + " MB " +
                              "\n\n\n\n");

                        RePort.Append(xx.ToString()+
                            "\n\n\n"
                          );
                        if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            return;

                    }
                }
            }
            //End


            ////Pre Analysis 02
            //{

            //    //FillList
            //    {
            //        List<TempPreAnalysisSortAndChanger> TempPreAnalysislist2 = new List<TempPreAnalysisSortAndChanger>();

            //        int Counter = 0;

            //        foreach (SortAndChangerNode n in MainList)
            //        {
            //            Counter = 0;
            //            foreach (SortAndChangerNode m in n.nextTree)
            //            {
            //                Counter = Counter + m.LevelCount;
            //                TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
            //                newnode.TempOrigByte = m.OrigByte;
            //                newnode.LevelCount = m.LevelCount;
            //                TempPreAnalysislist2.Add(newnode);
            //            }
            //            n.LevelCount = n.LevelCount + Counter;
            //        }
            //        //SortTemp
            //        Tr.quicksort(ref TempPreAnalysislist2, 0, TempPreAnalysislist2.Count - 1);

            //        //Edit
            //        int L = TempPreAnalysislist2.Count - 1;
            //        for (int i = 0; i != 256; i++)
            //        {
            //            for (int n = 0; n != 256; n++)
            //            {
            //                if (i != n)
            //                {
            //                    if (MainList[i].nextTree[n].IsWrite == false)
            //                    {

            //                        MainList[i].nextTree[n].LevelCount = TempPreAnalysislist2[L].LevelCount;
            //                        MainList[i].nextTree[n].IsWrite = true;
            //                        L--;
            //                        MainList[n].nextTree[i].LevelCount = TempPreAnalysislist2[L].LevelCount;
            //                        MainList[n].nextTree[i].IsWrite = true;
            //                        L--;
            //                    }
            //                }

            //                else
            //                {

            //                }

            //            }
            //        }
            //        for (int i = 0; i != 256; i++)
            //        {
            //            MainList[i].nextTree[i].LevelCount = TempPreAnalysislist2[L].LevelCount;
            //            MainList[i].nextTree[i].IsWrite = true;
            //            L--;
            //        }
            //        TempPreAnalysislist2 = null;
            //    }


            //    //001 Here Is Need To Fill First Dic
            //    for (int i = 0; i != 256; i++)
            //    {
            //        for (int n = 0; n != 256; n++)
            //        {
            //            if (MainList[i].nextTree[n].IsRead == false)
            //            {
            //                TempPreAnalysisSortAndChanger newnode = new TempPreAnalysisSortAndChanger();
            //                if (i == n)
            //                {
            //                    newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
            //                    newnode.LevelCount = MainList[i].nextTree[n].LevelCount;
            //                    MainList[i].nextTree[n].IsRead = true;
            //                }
            //                else
            //                {
            //                    int count = MainList[i].nextTree[n].LevelCount;
            //                    count = count + MainList[n].nextTree[i].LevelCount;

            //                    newnode.TempOrigByte = MainList[i].nextTree[n].OrigByte;
            //                    newnode.LevelCount = count;
            //                    MainList[i].nextTree[n].IsRead = true;
            //                    MainList[n].nextTree[i].IsRead = true;
            //                }
            //                TempPreAnalysislist.Add(newnode);
            //            }
            //        }
            //    }
            //    //SortTemp

            //    Tr.quicksort(ref TempPreAnalysislist, 0, TempPreAnalysislist.Count - 1);

            //    //FirstCal
            //    {
            //        long FirstReduce = 0;
            //        int L = TempPreAnalysislist.Count - 1;

            //        for (int i = 0; i != 128; i++)
            //        {
            //            BitArray bitnum = numoperation2.intvaluToBitsArr(i);
            //            int Timer = Convert.ToInt32(Math.Pow(2, bitnum.Count));
            //            int modRed = 8 - bitnum.Count;

            //            int n = 0;
            //            while (n != Timer)
            //            {
            //                int Reduceit = TempPreAnalysislist[L].LevelCount * modRed;
            //                FirstReduce = FirstReduce + Reduceit;

            //                L--;
            //                n++;
            //            }
            //        }

            //        {
            //            int FirstTotalAdd = Convert.ToInt32((origFilelength / 2) / 8);
            //            int FirstLastReduce = Convert.ToInt32(FirstReduce / 8) - FirstTotalAdd;
            //            StringBuilder xx = new StringBuilder();
            //            xx.Append(
            //                  "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "  =  " + (origFilelength / 1048576).ToString() + " MB " +
            //                  "\nTotalAdd = " + FirstTotalAdd.ToString() + "  =  " + (FirstTotalAdd / 1024).ToString() + " KB " + "  =  " + (FirstTotalAdd / 1048576).ToString() + " MB " +
            //                  "\nLastReduce = " + FirstLastReduce.ToString() + "  =  " + (FirstLastReduce / 1024).ToString() + " KB " + "  =  " + (FirstLastReduce / 1048576).ToString() + " MB " +
            //                  "\nFirstReduce = " + (FirstReduce / 8).ToString() + "  = " + ((FirstReduce / 8) / 1024).ToString() + " KB " + "  =  " + ((FirstReduce / 8) / 1048576).ToString() + " MB ");


            //            if (MessageBox.Show(xx.ToString(), "Report", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            //                return;

            //        }
            //    }
            //}













            ////Pointers

            //long TotalReduce = 0;
            //int TotalAdd = Convert.ToInt32((origFilelength / 2) / 8);

            ////FillList 
            ////Compute LevelCount && Reduce
            //{
            //    int Counter = 0;
            //    int ReduceCounter = 0;
            //    foreach (SortAndChangerNode n in MainList)
            //    {
            //        Counter = 0;
            //        ReduceCounter = 0;
            //        foreach (SortAndChangerNode m in n.nextTree)
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



           // ListDic = new List<List<byte[]>>();



            //EditSortBit
            foreach (SortAndChangerNode n in MainList)
            {
                foreach (SortAndChangerNode m in n.nextTree)
                {
                    Tr.AddIsSortBitForBitArry( m ,m.IsSorted);
                }
            }



            ListDic.Add(TempDicByte);

            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - start;

            sb.Append("Time End AnalysisListBy2_SecoundTime = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));


        }
        public void StartCompBy2_SecoundTime(string pathFile, int NumComp)
        {
            if (countenu == true)
            {


                if (MessageBox.Show("Start SortComp =  Yes  😄😄😄😄  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;



            }
            else
            {
                if (MessageBox.Show(" You Can't Contenue  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                //    MessageBox.Show(" You Can't Contenue  !!");

            }


            // MessageBox.Show(" Yes  😄😄😄😄  !!");

            FileInfo fil = new FileInfo(pathFile);
            string FileCompDerct = fil.FullName.Remove(fil.FullName.Length - fil.Extension.Length);
            Directory.CreateDirectory(FileCompDerct);

            //Filing
            string ForByte = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bytF" + NumComp.ToString();
            string ForBits = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2bitsF" + NumComp.ToString();
            string ForDic = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".SC2dicF" + NumComp.ToString();


            CompfilingBy2_SecoundTime(pathFile, ForByte, ForBits, ForDic);
            



        }
        private void CompfilingBy2_SecoundTime(string pathfile, string ForByte, string ForBits, string ForDic)
        {
            DateTime startTime = DateTime.Now;
            sb.Append("\nTime Start CompfilingBy2_SecoundTime = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            using (var filecomp = new FileStream(pathfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var Bitsfiling = new FileStream(ForBits, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var DicFiling = new FileStream(ForDic, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var ByteFiling = new FileStream(ForByte, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {

               

                //InfoRest
                int Addbits = 0;
                int RestByte = 0;
                byte[] lastByt=new byte[1];

                //Seek Files
                filecomp.Seek(0, SeekOrigin.Begin);
                Bitsfiling.Seek(0, SeekOrigin.Begin);
                ByteFiling.Seek(0, SeekOrigin.Begin);
                DicFiling.Seek(0, SeekOrigin.Begin);

                //SaveDic
                {
                    byte[] BytDicLength = new byte[2];
                    numoperation2.intvaluToBitsArr(ListDic[0].Count, 16).CopyTo(BytDicLength, 0);
                    DicFiling.Write(BytDicLength, 0, 2);
                    foreach (byte[] bytarr in ListDic[0])
                    {
                        DicFiling.Write(bytarr, 0, 2);
                    }
                    ListDic = new List<List<byte[]>>();
                    DicFiling.Close();
                }

                //FileLength     50MB = 52428800

                long Filelength = filecomp.Length;

                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);

                bool isFirst = true;
                int Temp = 0;


                List<BitArray> savbitList = new List<BitArray>();
                List<byte[]> Savebytelist = new List<byte[]>();

                //Start ProcessTimer1
                if (ProcessTimer1 != 0)
                {
                    byte[] datafile1 = new byte[52428800];

                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filecomp.Read(datafile1, 0, 52428800);

                        foreach (int n in datafile1)
                        {
                            if (isFirst == true)
                            {
                                Temp = n;
                                isFirst = false;
                            }
                            else
                            {
                               // Savebytelist.Add(MainList[Temp].nextTree[n].TempWritByt);
                                foreach (byte b in MainList[Temp].nextTree[n].TempWritByt)
                                    ByteFiling.WriteByte(b);
                                savbitList.Add(MainList[Temp].nextTree[n].TempBitsArr);

                                isFirst = true;
                            }
                        }

                        //Save Data
                        {
                            //Bitsave
                            {
                                int bs = 0;
                                BitArray savbits = new BitArray(savbitList.Count * 8);
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

                            ////SaveBytes
                            //{
                            //    foreach (byte[] BytArr in Savebytelist)
                            //    {
                            //        ByteFiling.Write(BytArr, 0, BytArr.Length);
                            //    }
                            //    Savebytelist = new List<byte[]>();
                            //}


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
                          //  Savebytelist.Add(MainList[Temp].nextTree[n].TempWritByt);
                            foreach (byte b in MainList[Temp].nextTree[n].TempWritByt)
                                ByteFiling.WriteByte(b);
                            savbitList.Add(MainList[Temp].nextTree[n].TempBitsArr);

                            isFirst = true;
                        }
                    }

                    //LastPart
                    {
                        if (isFirst == false)
                        {
                            lastByt = new byte[1];
                            lastByt[0] = MainList[Temp].OrigByte[0];
                            RestByte = 1;
                        }

                    }
                    //Save Data
                    {

                        //Bitsave
                        {
                            int bs = 0;
                            BitArray savbits = new BitArray(savbitList.Count * 8);
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

                        ////SaveBytes
                        //{
                        //    foreach (byte[] BytArr in Savebytelist)
                        //    {
                        //        ByteFiling.Write(BytArr, 0, BytArr.Length);
                        //    }
                        //    Savebytelist = new List<byte[]>();
                        //}


                    }


                }//End ProcessTimer2


                //Save RestData And Info
                {
                    //RestByte
                    ByteFiling.WriteByte(numoperation2.int32toByte1(RestByte));
                    if (RestByte != 0)
                    {
                        ByteFiling.WriteByte(lastByt[0]);
                    }


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
                }

                //Close Files
                filecomp.Close();
                Bitsfiling.Close();
                DicFiling.Close();
                ByteFiling.Close();

            }//End Files


            DateTime finish = DateTime.Now;
            TimeSpan sd = finish - startTime;

            sb.Append("Time End CompfilingBy2_SecoundTime = " + DateTime.Now.ToString("hh : mm : ss tt " +
               "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                + "\n\n"));

        }




    }
}
