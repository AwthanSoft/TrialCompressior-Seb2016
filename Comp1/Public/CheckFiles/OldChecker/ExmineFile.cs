using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;

namespace OldChekers
{
     class ExmineLastNode
    {
        public int Counter;

        public ExmineLastNode()
        {
            Counter = 0;
        }

    }


     class ExmineBaseNode
    {
        public bool Open;
        public bool HasDic;
        public byte way;
        public byte[] OrigByte;
        public BitArray OrigBitArr;

        public BitArray TempBitArr;


        public ExmineBaseNode()
        {


        }


    }

     class ExmineNode
    {
        public ExmineNode nextzero;
        public ExmineNode nextone;
        public ExmineBaseNode bases;
        public ExmineLastNode lastNode;
        public long thisCounter;

        public ExmineNode()
        {
            nextzero = null;
            nextone = null;
            thisCounter = 0;
        }

    }

    class ExmineFile
    {
        public StringBuilder sb;


        public ExmineFile()
        {

            sb = new StringBuilder();





        }

        public  string CheckByBit(string firstFile, string secondeFile)
        {

            sb.Append("Time Start CheckByBit = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            StringBuilder sf = new StringBuilder();

             using (var firstfiling = new FileStream(firstFile, FileMode.OpenOrCreate, FileAccess.Read ,FileShare.Read))
             using (var secondefiling = new FileStream(secondeFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int NumErorrtype = 1;

                firstfiling.Seek(0, SeekOrigin.Begin);
                secondefiling.Seek(0, SeekOrigin.Begin);
                long Fl = firstfiling.Length;
                long Sl = secondefiling.Length;


             //   int frl = Convert.ToInt32(firstfiling.Length);
             //   int sel = Convert.ToInt32(secondeFile.Length);
                Int64 lost = 0;




                // 01 Length
                if (Fl != Sl)
                {


                    sf.Append("\nLength = " + "Error ***** (" + NumErorrtype.ToString() + " )\n" +
                        "\nfirstfiling = " + firstfiling.Length.ToString() + "\nsecondefiling = " + secondefiling.Length.ToString() +
                        "\n\n ->  ");



                    if (Fl > Sl)
                    {

                        lost = Fl - Sl;

                        sf.Append("First Is More = " + lost.ToString());
                    }
                    else
                    {
                        lost = Sl - Fl;
                        sf.Append("Secounde Is More = " + lost.ToString());

                    }
                    NumErorrtype++;
                }
                else
                {
                    sf.Append("\nLength = " + "IsCorrect\n\n");
                }


                // 02 Byte
                {
                    int timeff = Convert.ToInt32(firstfiling.Length / 1048576);
                    int timefs = Convert.ToInt32(firstfiling.Length % 1048576);

                    int timesf = Convert.ToInt32(secondefiling.Length / 1048576);
                    int timess = Convert.ToInt32(secondefiling.Length % 1048576);


                    long ByteCorrect = 0;
                    long ByteErorr = 0;


                    //Arr
                    byte[] data1 = new byte[1048576];
                    byte[] data2 = new byte[1048576];

                    int timF = 0, timS = 0;
                    while (timF != timeff && timS != timefs)
                    {
                        firstfiling.Read(data1, 0, 1048576);
                        secondefiling.Read(data2, 0, 1048576);

                        int i = 0;
                        while (i != 1048576)
                        {
                            if (data1[i] == data2[i])
                                ByteCorrect++;
                            else
                                ByteErorr++;

                            i++;
                        }

                        timF++;
                        timS++;
                    }

                    firstfiling.Read(data1, 0, timefs);
                    secondefiling.Read(data2, 0, timess);

                    int n = 0;
                    while (n != timefs && n != timess)
                    {
                        if (data1[n] == data2[n])
                            ByteCorrect++;
                        else
                            ByteErorr++;

                        n++;
                    }



                    if (ByteCorrect == 0 && ByteErorr == 0)
                    {
                        sf.Append("\n\n\n\n\nEqualByte = " + " Correct \n\n");
                    }
                    else
                    {
                        sf.Append("\n\n\n\n\nEqualByte = " + " Erorr ******** ( " + NumErorrtype.ToString() + " )\n" +
                            "\nByteCorrect = " + ByteCorrect.ToString() +
                            "\nByteErorr = " + ByteErorr.ToString() +
                            "\n ->  " + (ByteCorrect > ByteErorr ? "ByteCorrect Is More" : "ByteErorr Is More"));
                        NumErorrtype++;
                    }
                }//End 02








                sf.Append("\n\n\nNumErorrtype = " + (NumErorrtype - 1).ToString() + "\n\n");

                firstfiling.Close();
                secondefiling.Close();
            }
             

             sb.Append("Time End CheckByBit = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));


             return sf.ToString();

        }

        public string CheckBy2Byte(string firstFile)
        {
            sb.Append("\nTime Start CheckBy2Byte = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            StringBuilder sblist = new StringBuilder();

            using (var filing = new FileStream(firstFile, FileMode.Open, FileAccess.Read))
            {

                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int Filelength2 = Convert.ToInt32(Filelength % 52428800);


                //ArrayNode
                bool IsFirst = true;

                List<ExmineNode> List1 = new List<ExmineNode>();
                List<ExmineNode> List2 = new List<ExmineNode>();
                List<ExmineNode> List3 = new List<ExmineNode>();


                {
                    //ExmineNode newNode = new ExmineNode();
                    //newNode.bases = new ExmineBaseNode();
                    //newNode.lastNode = new ExmineLastNode();

                    //newNode.lastNode.Counter = 0;
                    //newNode.bases.OrigByte = new byte[1];

                    for (int i = 0; i != 256; i++)
                    {
                        ExmineNode newNode = new ExmineNode();
                        newNode.bases = new ExmineBaseNode();
                        newNode.lastNode = new ExmineLastNode();
                        newNode.lastNode.Counter = 0;
                        newNode.bases.OrigByte = new byte[1];
                        newNode.bases.OrigByte[0] = numoperation2.int32toByte1(i);

                        List1.Add(newNode);

                        ExmineNode newNode2 = new ExmineNode();
                        newNode2.bases = new ExmineBaseNode();
                        newNode2.lastNode = new ExmineLastNode();
                        newNode2.lastNode.Counter = 0;
                        newNode2.bases.OrigByte = new byte[1];
                        newNode2.bases.OrigByte[0] = numoperation2.int32toByte1(i);


                        List2.Add(newNode2);

                        ExmineNode newNode3 = new ExmineNode();
                        newNode3.bases = new ExmineBaseNode();
                        newNode3.lastNode = new ExmineLastNode();
                        newNode3.lastNode.Counter = 0;
                        newNode3.bases.OrigByte = new byte[1];
                        newNode3.bases.OrigByte[0] = numoperation2.int32toByte1(i);


                        List3.Add(newNode3);
                    }

                }







                //Start ProcessTimer1
                {

                    byte[] datafile1 = new byte[52428800];

                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filing.Read(datafile1, 0, 52428800);

                        foreach (int n in datafile1)
                        {
                            if (IsFirst == true)
                            {
                                List1[n].lastNode.Counter++;
                                IsFirst = !IsFirst;
                            }
                            else
                            {
                                List2[n].lastNode.Counter++;
                                IsFirst = !IsFirst;
                            }
                        }

                        Process1++;
                    }

                } //End ProcessTimer1

                {//Start ProcessTimer2

                    byte[] datafile2 = new byte[Filelength2];

                    filing.Read(datafile2, 0, Filelength2);

                    foreach (int n in datafile2.ToArray())
                    {
                        if (IsFirst == true)
                        {
                            List1[n].lastNode.Counter++;
                            IsFirst = !IsFirst;
                        }
                        else
                        {
                            List2[n].lastNode.Counter++;
                            IsFirst = !IsFirst;
                        }
                    }

                }//End ProcessTimer2


                for (int i = 0; i != 256; i++)
                {
                    List3[i].lastNode.Counter = List1[i].lastNode.Counter + List2[i].lastNode.Counter;

                }



                //Sort Lists
                {
                    if (List1.Count != 0)
                        quicksort(ref List1, 0, List1.Count - 1);

                    if (List2.Count != 0)
                        quicksort(ref List2, 0, List2.Count - 1);
                }



                //  Print Lists
                {
                    //01
                    sblist.Append("\n\nList (1) ***********\n\n");
                    foreach (ExmineNode nod in List1)
                    {
                        sblist.Append((nod.bases.OrigByte[0]).ToString("000") + " = ");
                        sblist.Append(nod.lastNode.Counter.ToString() + "\n");
                    }
                    sblist.Append("\n\n list1Count = " + List1.Count.ToString());


                    //02
                    sblist.Append("\n\nList (2) ***********\n\n");
                    foreach (ExmineNode nod in List2)
                    {
                        sblist.Append((nod.bases.OrigByte[0]).ToString("000") + " = ");
                        sblist.Append(nod.lastNode.Counter.ToString() + "\n");
                    }
                    sblist.Append("\n\n list2Count = " + List2.Count.ToString());

                    //All
                    sblist.Append("\n\nList (All) ***********\n\n");
                    int i = 0;
                    while (i != List1.Count && i != List2.Count)
                    {
                        sblist.Append((List1[i].bases.OrigByte[0]).ToString("000") + " = " +
                        List1[i].lastNode.Counter.ToString("    0    ") +

                        "         " +

                       (List2[i].bases.OrigByte[0]).ToString("000") + " = " +
                        List2[i].lastNode.Counter.ToString("    0    ") + "\n");


                        i++;
                    }

                    sblist.Append("\n\n list1Count = " + List1.Count.ToString() + "    list2Count = " + List2.Count.ToString() + "\n\n\n");
                }



                //Print By 1 One Byte
                {
                   

                   

                    if (List3.Count != 0)
                        quicksort(ref List3, 0, List3.Count - 1);

                    //01
                    sblist.Append("\n\n\n\nList (3) ****** All *****\n\n");
                    foreach (ExmineNode nod in List3)
                    {
                        sblist.Append((nod.bases.OrigByte[0]).ToString("000") + " = ");
                        sblist.Append(nod.lastNode.Counter.ToString() + "\n");
                    }
                    sblist.Append("\n\n list3Count = " + List3.Count.ToString());
                }







                sb.Append("\nTime End CheckBy2Byte = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



                filing.Close();
            }

            return sblist.ToString();

        }

        public string CheckByLocatByte(string firstFile , int LocatByte , bool isTotxt)
        {
            sb.Append("\nTime Start CheckByLocatByte = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
            StringBuilder sblist = new StringBuilder();

            using (var filing = new FileStream(firstFile, FileMode.Open, FileAccess.Read))
            {

                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                // 50 MB = 52428800   ,   10 MB = 10485760

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 10485760);
                int Filelength2 = Convert.ToInt32(Filelength % 10485760);


                //ArrayNode

                ListSortAndChanger2 ListCraet = new ListSortAndChanger2();
                List<SortAndChangerNode2> MainList = new List<SortAndChangerNode2>();
                ListCraet.creatBy2(ref MainList);

                int Locater = 0;

                //Start ProcessTimer1
                {

                    byte[] datafile1 = new byte[10485760];

                    int Process1 = 0;
                    while (Process1 != ProcessTimer1)
                    {
                        filing.Read(datafile1, 0, 10485760);

                        foreach (int n in datafile1)
                        {
                            if (Locater == 256)
                                Locater = 0;

                            MainList[Locater].nextTree[n].LevelCount++;
                            Locater++;
                        }

                        Process1++;
                    }

                } //End ProcessTimer1

                {//Start ProcessTimer2

                    byte[] datafile2 = new byte[Filelength2];

                    filing.Read(datafile2, 0, Filelength2);

                    foreach (int n in datafile2)
                    {
                        if (Locater == 256)
                            Locater = 0;

                        MainList[Locater].nextTree[n].LevelCount++;
                        Locater++;
                    }

                }//End ProcessTimer2



                //
                if (LocatByte <= 0 || LocatByte>256 )
                {

                    //Sort Lists
                    {
                        foreach (SortAndChangerNode2 nod in MainList)
                        {
                            ListCraet.quicksort(ref nod.nextTree, 0, 255);
                        }

                    }



                    //  Print Lists
                    {
                        if (isTotxt == true)
                        {
                            int numList = 0;
                            foreach (SortAndChangerNode2 n in MainList)
                            {
                                sblist.AppendLine(" ");
                                sblist.AppendLine(" ");
                                sblist.Append("List *********** ( " + numList.ToString("000") + " ) ***********");
                                sblist.AppendLine(" ");
                                sblist.AppendLine(" ");

                                foreach (SortAndChangerNode2 nod in n.nextTree)
                                {
                                    sblist.Append(nod.RealListlValue.ToString("000") + " = ");
                                    sblist.Append(nod.LevelCount.ToString("    0    "));
                                    sblist.AppendLine(" ");
                                }

                                numList++;
                            }
                           

                          
                        }
                        else
                        {

                            //01
                            int numList = 0;
                            foreach (SortAndChangerNode2 n in MainList)
                            {
                                sblist.Append("\n\nList ***********( " + numList.ToString("000") + " )***********\n\n");

                                foreach (SortAndChangerNode2 nod in n.nextTree)
                                {
                                    sblist.Append(nod.RealListlValue.ToString("000") + " = ");
                                    sblist.Append(nod.LevelCount.ToString("    0    ") + "\n");
                                }
                                numList++;
                            }
                        }

                    }

                }
                else
                {
                    LocatByte = LocatByte - 1;
                    //Sort NumList Lists
                    {
                            ListCraet.quicksort(ref MainList[LocatByte].nextTree, 0, 255);
                    }



                    //  Print Lists
                    {
                        //01

                        sblist.Append("\n\nList ***********( " + LocatByte.ToString("000") + " )***********\n\n");

                            foreach (SortAndChangerNode2 nod in MainList[LocatByte].nextTree)
                            {
                                sblist.Append(nod.RealListlValue.ToString("000") + " = ");
                                sblist.Append(nod.LevelCount.ToString("    0    ") + "\n");
                            }
                          

                    }
                }



                    sb.Append("\nTime End CheckByLocatByte = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



                    filing.Close();
                

                return sblist.ToString();
            }

        }


        public string checkNumByte16Bits(string filingPath)
        {
            List<ListCompNode> MainList = new List<ListCompNode>();
            ListComp Tr = new ListComp();

            //walk Through

            using (var filing = new FileStream(filingPath, FileMode.Open, FileAccess.Read))
            {
                //Seek Files
                filing.Seek(0, SeekOrigin.Begin);

                //FileLength

                long Filelength = filing.Length;
                int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);


                //CreateList
                MainList = new List<ListCompNode>();

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




            StringBuilder sblist = new StringBuilder();

            //Print
            {
                List<ListCompNode> TempList = new List<ListCompNode>();
                List<ListCompNode> MainTempList = new List<ListCompNode>();

                {

                    //FillTempList 
                    //Compute LevelCount
                    {
                        int Counter = 0;
                        foreach (ListCompNode n in MainList)
                        {
                            Counter = 0;
                            foreach (ListCompNode m in n.nextTree)
                            {
                                Counter = Counter + m.LevelCount;
                                TempList.Add(m);
                            }
                            n.LevelCount = n.LevelCount + Counter;
                        }

                        foreach (ListCompNode n in MainList)
                        {
                            MainTempList.Add(n);
                        }

                    }

                    //SortTemplist2
                    Tr.quicksort(ref TempList, 0, TempList.Count - 1);
                    Tr.quicksort(ref MainTempList, 0, MainTempList.Count - 1);
                    //TotalCounter
                    {
                        foreach (ListCompNode n in MainList)
                        {
                            n.TotalCount = n.LevelCount + (n.nextTree[n.RealValue].LevelCount);
                        }
                    }

                }



                int o = 0;
                foreach (ListCompNode con in TempList)
                {
                    o++;
                    //if (o > 100)
                    //    break;
                    sblist.Append(con.RealValue.ToString("00000") + " = ");
                    sblist.Append(con.LevelCount.ToString() + "  =  " + con.OrigByte[0].ToString() + " <=> " + con.OrigByte[1].ToString());
                    sblist.AppendLine(" ");
                }
                sblist.AppendLine(" ");
                sblist.AppendLine(" ");
                sblist.AppendLine(" ");
                sblist.Append("\n\n Numb = " + o.ToString());

                //int count = Templist2.Count - 1;
                //for (int i = 0; i != 100; i++)
                //{
                //    sblist.Append(Templist2[count - i].RealValue.ToString("00000") + " = ");
                //    sblist.Append(Templist2[count - i].LevelCount.ToString());
                //    sblist.AppendLine(" ");

                //}


            }


            return sblist.ToString();


        }


        // quicksort

        public void quicksort(ref List<ExmineNode> list, int begin, int end)
        {
            ExmineNode pivot = list[(begin + (end - begin) / 2)];
            int left = begin;
            int right = end;
            while (left <= right)
            {
                while (list[left].lastNode.Counter < pivot.lastNode.Counter)
                {
                    left++;
                }
                while (list[right].lastNode.Counter > pivot.lastNode.Counter)
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
        public void swap(ref List<ExmineNode> list, int x, int y)
        {
            ExmineNode temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }


    }

    public class numoperation2
    {


        public numoperation2()
        {

        }

        public static string BitToChar(BitArray bitdata)
        {

            StringBuilder Chars = new StringBuilder();

            foreach (bool b in bitdata)
            {
                if (b == false)
                    Chars.Append("A");
                else
                    Chars.Append("B");
            }


            return Chars.ToString();
        }


        public static BitArray intvaluToBitsArr(int value)
        {
            string s = Convert.ToString(value, 2);
            int[] bits = s.Select(c => int.Parse(c.ToString())).ToArray();
            BitArray bitarr = new BitArray(bits.Length);
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                bitarr[bits.Length - 1 - i] = Convert.ToBoolean(Convert.ToInt32(bits[i]));
            }

            return bitarr;
        }
        public static BitArray intvaluToBitsArr(int value, int ArrLength)
        {
            BitArray bitarr3 = new BitArray(ArrLength);

            int i = 0;
            while (i != ArrLength)
            {
                bitarr3[i] = Convert.ToBoolean(value & (1 << i));
                i++;
            }
            return bitarr3;
        }

        public static int bitarrayToint32(BitArray arry)
        {
            int num = 0;
            for (int i = 0; i <= arry.Count - 1; i++)
                num = num + Convert.ToInt32(arry[i]) * (Convert.ToInt32(Math.Pow(2, i)));

            return num;
        }
        static public string BitarryToString(BitArray arry)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= arry.Count - 1; i++)
            {
                sb.Append(Convert.ToInt32(arry[i]).ToString());
            }
            return sb.ToString();
        }

        static public string ListArrToString(List<bool> arry)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= arry.Count - 1; i++)
            {
                sb.Append(Convert.ToInt32(arry[i]).ToString());
            }
            return sb.ToString();
        }


        public static int int32Toint_(int value, int To)
        {
            int num = 0;
            BitArray valu = intvaluToBitsArr(value, 32);
            for (int i = 0; i <= To - 1; i++)
                num = num + Convert.ToInt32(valu[i]) * Convert.ToInt32((Math.Pow(2, i)));

            return num;
        }
        public static byte int32toByte1(int valu)
        {
            byte[] bytenum = new byte[1];
            bytenum = BitConverter.GetBytes(valu);
            return bytenum[0];
        }

        public static BitArray _2bitArrTo_1Arr(BitArray arr1, BitArray arr2)
        {

            BitArray Arr3 = new BitArray(arr1.Count + arr2.Count);
            int i = arr1.Count;
            for (int c = 0; c <= arr1.Count - 1; c++)
                Arr3[c] = arr1[c];
            for (int n = 0; n <= arr2.Count - 1; n++)
                Arr3[i + n] = arr2[n];

            return Arr3;
        }

        public static int bitarrSqrTo_sys(BitArray arr, int toSys)
        {
            int num = 0;
            for (int i = 0; i <= arr.Count - 1; i++)
            {
                num = num + Convert.ToInt32(Math.Pow(toSys, i)) * Convert.ToInt32(arr[i]);
            }

            return num;
        }

        public static BitArray bitArrsq_sys(int num, int toSys)
        {
            //int valu = 0;
            int sizArr = 0;
            int numval = 0;

            while (!(numval >= num))
            {
                numval = numval + Convert.ToInt32(Math.Pow(toSys, sizArr));
                sizArr++;
            }
            BitArray arr1 = new BitArray(sizArr);

            for (int i = sizArr - 1; i >= 0; i--)
            {
                if (num >= Convert.ToInt32(Math.Pow(toSys, i)))
                {
                    arr1[i] = true;
                    num = num - Convert.ToInt32(Math.Pow(toSys, i));
                }
                else
                    arr1[i] = false;

            }

            return arr1;
        }
        public static int num_sysTo_sys(int number, int fromSys, int toSys)
        {

            int from = bitarrSqrTo_sys(bitArrsq_sys(number, 2), fromSys);
            int valu = bitarrSqrTo_sys(bitArrsq_sys(from, toSys), 2);



            return valu;
        }

        static public BitArray OpsitBitArray(BitArray array)
        {
            BitArray opsitarray = new BitArray(array.Count);
            for (int c = 0; c <= array.Count - 1; c++)
            {
                opsitarray[c] = array[array.Count - 1 - c];
            }
            return opsitarray;
        }


    }

}
