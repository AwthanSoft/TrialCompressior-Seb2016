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
     public class ListCompNode
    {
        public ListCompNode nextNode;
        public List<ListCompNode> nextTree;
        public bool IsOpen;
         public bool IsIt;
        public bool HasDic;
        //public byte way;
        public byte[] OrigByte;
        //public byte[] TempByte;
        //public BitArray OrigBitArr;
        //public BitArray TempBitArr;

        public int RealValue = 0;

        public byte[] ReduceKey;
        public int Reduce = 0;
         public int TotalCount;
         public int LevelCount;


         public ListCompNode()
         {
            // nextNode = null;
             IsIt=false;
             IsOpen = false;
             HasDic = false;
             TotalCount=0;
             LevelCount = 0;
             
         }
    }

     public class ListComp
     {
         private int NumOfNodes1;
         private int NumOfNodes2;
         private int NumOfNodes3;

         public List<ListCompNode> listRoot ;
         public int ModNum = 0;
         public ListComp()
         {
        //     listRoot = new List<ListCompNode>();
             NumOfNodes1 = 0;
             NumOfNodes2 = 0;
             NumOfNodes3 = 0;

         }
         private ListCompNode newNode()
         {
             ListCompNode newnod = new ListCompNode();


             return newnod;
         }


         //By2
         private void CreatNextListBy2(ref List<ListCompNode> Nextlist ,byte PreByte)
         {
             int i = 0;
             while (i != 256)
             {
                 ListCompNode newnod = new ListCompNode();
                 newnod.RealValue = NumOfNodes2; NumOfNodes2++;
                 newnod.OrigByte = new byte[2];
                 newnod.OrigByte[0] = PreByte;
                 newnod.OrigByte[1] = numoperation2.int32toByte1(i);
                 Nextlist.Add(newnod);
                 i++;
             }
         }
         public void creatBy2(ref List<ListCompNode> thislist)
         {
             thislist = new List<ListCompNode>();
             ModNum = 2;
             int i=0;
             while(i != 256)
             {
                 ListCompNode newnod = new ListCompNode();
                 newnod.OrigByte = new byte[1];
                 newnod.OrigByte[0] = numoperation2.int32toByte1(i);
                 newnod.RealValue = NumOfNodes1; NumOfNodes1++;
                 newnod.nextTree = new List<ListCompNode>();

                 CreatNextListBy2(ref newnod.nextTree, newnod.OrigByte[0]);

                 thislist.Add(newnod);

                 i++;
             }


         }


         public void quicksort(ref List<ListCompNode> list, int begin, int end)
         {
             ListCompNode pivot = list[(begin + (end - begin) / 2)];
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
         public void swap(ref List<ListCompNode> list, int x, int y)
         {
             ListCompNode temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }




         //By3
         public void creatBy3(ref List<ListCompNode> thislist)
         {
             thislist = new List<ListCompNode>();
             ModNum = 3;
             int i = 0;
             while (i != 256)
             {
                 ListCompNode newnod = new ListCompNode();
                 newnod.OrigByte = new byte[1];
                 newnod.OrigByte[0] = numoperation2.int32toByte1(i);
                 newnod.RealValue = NumOfNodes1; NumOfNodes1++;
                 newnod.nextTree = new List<ListCompNode>();

                 CreatSecoundListBy3(ref newnod.nextTree, newnod.OrigByte[0]);

                 thislist.Add(newnod);

                 i++;
             }


         }
         private void CreatSecoundListBy3(ref List<ListCompNode> Nextlist, byte PreByte)
         {
             int i = 0;
             while (i != 256)
             {
                 ListCompNode newnod = new ListCompNode();
                 /*********Error********/
                 newnod.RealValue = i; NumOfNodes2++;
                 newnod.OrigByte = new byte[2];
                 newnod.OrigByte[0] = PreByte;
                 newnod.OrigByte[1] = numoperation2.int32toByte1(i);

                 newnod.nextTree = new List<ListCompNode>();
                 CreatTheardListBy3(ref newnod.nextTree, newnod.OrigByte);


                 Nextlist.Add(newnod);
                 i++;
             }
         }
         private void CreatTheardListBy3(ref List<ListCompNode> Nextlist, byte[] PreByte)
         {
             int i = 0;
             while (i != 256)
             {
                 ListCompNode newnod = new ListCompNode();
                 newnod.RealValue = NumOfNodes2; NumOfNodes3++;
                 newnod.OrigByte = new byte[3];
                 newnod.OrigByte[0] = PreByte[0];
                 newnod.OrigByte[1] = PreByte[1];
                 newnod.OrigByte[2] = numoperation2.int32toByte1(i);
                 Nextlist.Add(newnod);
                 i++;
             }
         }




     }


     public class CompFilingByList
     {
         public StringBuilder sb;
         public StringBuilder RePort;
         

         private List<ListCompNode> MainList;
         private List<ListCompNode> Templist2;
         private List<byte> listDic;

         private ListComp Tr;
         public int mod;   
         private byte[] num = new byte[5];

         public long origFilelength = 0;
         public int Reduce = 0;
         public bool countenu=false;
         public bool SizeisEqual = false;

         public CompFilingByList()
        {
            sb = new StringBuilder();
            Tr = new ListComp();

            num[0] = numoperation2.int32toByte1(0);
            num[1] = numoperation2.int32toByte1(1);
            num[2] = numoperation2.int32toByte1(2);
            num[3] = numoperation2.int32toByte1(3);
            num[4] = numoperation2.int32toByte1(4);

            mod = 0;
            Templist2 = new List<ListCompNode>();
       
        }
         



         public void ExmineFileBy2(string filingPath)
         {

             // 01 
             WalkThroListBy2(filingPath);

             //02
             RePort = new StringBuilder();
          //   AnalysisListBy2();
             AnalysisListBy2_2();

         }
         private void WalkThroListBy2(string filingPath)
         {
             DateTime start = DateTime.Now;
             sb.Append("\nTime Start WalkThroListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

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


             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - start;

             sb.Append("Time End WalkThroListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));
         }
         public void AnalysisListBy2()
         {
       
             DateTime start = DateTime.Now;
             sb.Append("\nTime Start AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



             //Pointers

             
             int TotalReduce = 0;
             int TotalHasDic = 0;
             listDic = new List<byte>();
      



           
             //FillList 
             //Compute LevelCount
             {
                 int Counter = 0;
                 foreach (ListCompNode n in MainList)
                 {
                     Counter = 0;
                     foreach (ListCompNode m in n.nextTree)
                     {
                         Counter = Counter + m.LevelCount;
                         Templist2.Add(m);
                     }
                     n.LevelCount = n.LevelCount + Counter;
                 }

             }

             //SortTemplist2
             Tr.quicksort(ref Templist2, 0, Templist2.Count - 1);

             //TotalCounter
             {
                 foreach (ListCompNode n in MainList)
                 {
                     n.TotalCount = n.LevelCount + (n.nextTree[n.RealValue].LevelCount);
                     
                 }
             }


             //Isopen
             {
                 int Mainlocate = 0;



                 int IsRead = 0;
                 int i = Templist2.Count - 1;
                 while(i!=0)
                 {
                     if (IsRead == 256)
                         break;
                     if (Templist2[i].IsOpen == false)
                     {
                         //Close
                         Mainlocate = Convert.ToInt32(Templist2[i].OrigByte[0]);
                     

                         IsRead++;

                         //Calcu
                         if ((MainList[Mainlocate].LevelCount / 8) + 3 < Templist2[i].LevelCount)
                         {
                             int reduce = Templist2[i].LevelCount - ((MainList[Mainlocate].LevelCount / 8) + 3);
                             TotalReduce = TotalReduce + reduce;
                             TotalHasDic++;
                             MainList[Mainlocate].HasDic = true;
                             Templist2[i].IsIt = true;

                             listDic.Add(Templist2[i].OrigByte[0]);
                             listDic.Add(Templist2[i].OrigByte[1]);

                             foreach (ListCompNode n in MainList[Mainlocate].nextTree)
                             {
                                 n.IsOpen = true;
                                 Templist2[i].HasDic = true;
                             }

                         }
                         else
                         {
                             foreach (ListCompNode n in MainList[Mainlocate].nextTree)
                                 n.IsOpen = true;
                         }
                        
                     }

                     i--;
                 }

             }
             if (TotalReduce != 0)
                 countenu = true;

             RePort.Append("\n\n\n******** RePort ****\n" +
                "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "\n" +
                "\ncountenu = " + countenu.ToString() +
                "\n\n");

             if (TotalReduce != 0)
             {
                 long LastSize=origFilelength-TotalReduce;
                 RePort.Append("\nTotalHasDic = " + TotalHasDic.ToString() +
                   "\nTotalReduce = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 1024).ToString() + " KB " +
                   "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "\n\n\n");


             }

             Reduce = TotalReduce;




             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - start;

             sb.Append("Time End AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));


         }

         public void AnalysisListBy2_2()
         {

             DateTime start = DateTime.Now;
             sb.Append("\nTime Start AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));



             //Pointers


             int TotalReduce = 0;
             int TotalHasDic = 0;
             listDic = new List<byte>();


             List<ListCompNode> TempList = new List<ListCompNode>();
             List<ListCompNode> MainTempList = new List<ListCompNode>();


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


             //Isopen
             {
                 int Change = 0;
                 do
                 {
                     Change = 0;
                     int n = 255;
                     while (n != -1)
                     {
                         if (MainTempList[n].IsOpen == false)
                         {
                             int countlist = TempList.Count;
                             int i = countlist - 256;
                             while (i != countlist)
                             {
                                 int count = MainTempList[n].LevelCount + TempList[i].LevelCount;
                                 int addBit = (count / 8) + 4;

                                 int Reduceit = (TempList[i].LevelCount ) - addBit;

                                 if (Reduceit > 0)
                                 {
                                     if (Reduceit > TempList[i].Reduce)
                                     {
                                         if (TempList[i].ReduceKey != null)
                                         {
                                             int Mainlocate = Convert.ToInt32(TempList[i].ReduceKey[0]);
                                             MainList[Mainlocate].IsOpen = false;
                                             TempList[i].Reduce = Reduceit;
                                             TotalReduce = TotalReduce - TempList[i].Reduce + Reduceit;
                                             MainTempList[n].IsOpen = true;
                                             TempList[i].ReduceKey[0] = MainTempList[n].OrigByte[0];
                                             Change++;
                                             break;
                                         }
                                         else
                                         {
                                             TempList[i].ReduceKey = new byte[1];

                                             TempList[i].Reduce = Reduceit;
                                             TotalReduce = TotalReduce + Reduceit;
                                             MainTempList[n].IsOpen = true;
                                             TempList[i].ReduceKey[0] = MainTempList[n].OrigByte[0];
                                             Change++;
                                             break;

                                         }

                                     }
                                     else
                                     {
                                       //  break;
                                     }

                                 }

                                 i++;
                             }

                         }
                         
                         n--;
                     }

                 } while (Change != 0);
                 
                 
             








              
                 //while (i != 0)
                 //{
                 //    if (IsRead >= 256)
                 //        break;






                 //    if (TempList[i].IsOpen == false)
                 //    {
                 //        //Close
                 //        Mainlocate = Convert.ToInt32(TempList[i].OrigByte[0]);


                 //        IsRead++;

                 //        //Calcu
                 //        if ((MainList[Mainlocate].LevelCount / 8) + 3 < TempList[i].LevelCount)
                 //        {
                 //            int reduce = TempList[i].LevelCount - ((MainList[Mainlocate].LevelCount / 8) + 3);
                 //            TotalReduce = TotalReduce + reduce;
                 //            TotalHasDic++;
                 //            MainList[Mainlocate].HasDic = true;
                 //            TempList[i].IsIt = true;

                 //            listDic.Add(TempList[i].OrigByte[0]);
                 //            listDic.Add(TempList[i].OrigByte[1]);

                 //            foreach (ListCompNode n in MainList[Mainlocate].nextTree)
                 //            {
                 //                n.IsOpen = true;
                 //                Templist2[i].HasDic = true;
                 //            }

                 //        }
                 //        else
                 //        {
                 //            foreach (ListCompNode n in MainList[Mainlocate].nextTree)
                 //                n.IsOpen = true;
                 //        }

                 //    }

                 //    i--;
                 //}

             }
             if (TotalReduce != 0)
                 countenu = true;

             RePort.Append("\n\n\n******** RePort ****\n" +
                "\norigFilelength = " + origFilelength.ToString() + "  = " + (origFilelength / 1024).ToString() + " KB " + "\n" +
                "\ncountenu = " + countenu.ToString() +
                "\n\n");

             if (TotalReduce != 0)
             {
                 long LastSize = origFilelength - TotalReduce;
                 RePort.Append("\nTotalHasDic = " + TotalHasDic.ToString() +
                   "\nTotalReduce = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 1024).ToString() + " KB " +
                   "\nLastSize = " + LastSize.ToString() + "  =  " + (LastSize / 1024).ToString() + " KB " + "\n\n\n");


             }

             Reduce = TotalReduce;




             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - start;

             sb.Append("Time End AnalysisListBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));


         }

         private void CompfilingBy2(string pathfile, string saveMainfile, string nextPathFiling)
         {
             DateTime startTime = DateTime.Now;
             sb.Append("\nTime Start CompfilingBy2 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));
             using (var filecomp = new FileStream(pathfile, FileMode.Open, FileAccess.Read, FileShare.Read))
             using (var mainfiling = new FileStream(saveMainfile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
             using (var Nextfiling = new FileStream(nextPathFiling, FileMode.OpenOrCreate, FileAccess.ReadWrite))
             {
            


                 //InfoRest
                 int Addbit = 0;

                 //Seek Files
                 filecomp.Seek(0, SeekOrigin.Begin);
                 mainfiling.Seek(0, SeekOrigin.Begin);
                 Nextfiling.Seek(0, SeekOrigin.Begin);

                 //FileLength     50MB = 52428800

                 long Filelength = filecomp.Length;

                 int ProcessTimer1 = Convert.ToInt32(Filelength / 52428800);
                 int ProcessTimer2 = Convert.ToInt32(Filelength % 52428800);

                 bool isFirst = true;
                 int Temp = 0;

                 //Array_1
                 int bs = 0;
                 byte[] SaveByt = new byte[6553601];
                 BitArray savbit = new BitArray(52428808, false);


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
                                 if (MainList[Temp].nextTree[n].HasDic == true )
                                 {
                                     if (MainList[Temp].nextTree[n].IsIt == true)
                                     {
                                         mainfiling.Write(MainList[Temp].nextTree[n].OrigByte, 0, 1);
                                         savbit[bs] = true; bs++;
                                     }
                                     else
                                     {
                                         mainfiling.Write(MainList[Temp].nextTree[n].OrigByte, 0, 2);
                                         savbit[bs] = false; bs++;
                                     }

                                 }
                                 else
                                 {
                                     mainfiling.Write(MainList[Temp].nextTree[n].OrigByte, 0, 2);

                                 }

                                 isFirst = true;
                             }
                         }

                         //Save Data
                         {
                             savbit.CopyTo(SaveByt, 0);
                             Nextfiling.Write(SaveByt, 0, bs / 8);

                             int start = (bs / 8) * 8, end = bs;
                             bs = 0;
                             while (start != end)
                             {
                                 savbit[bs] = savbit[start]; bs++; start++;
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
                             if (MainList[Temp].nextTree[n].HasDic == true)
                             {
                                 if (MainList[Temp].nextTree[n].IsIt == true)
                                 {
                                     mainfiling.WriteByte(MainList[Temp].nextTree[n].OrigByte[0]);
                                     savbit[bs] = true; bs++;
                                 }
                                 else
                                 {
                                     mainfiling.Write(MainList[Temp].nextTree[n].OrigByte, 0, 2);
                                     savbit[bs] = false; bs++;
                                 }

                             }
                             else
                             {
                                 mainfiling.Write(MainList[Temp].nextTree[n].OrigByte, 0, 2);

                             }

                             isFirst = true;

                         }
                     }

                     //LastPart
                     {
                         if (isFirst == false)
                         {
                             mainfiling.WriteByte(MainList[Temp].OrigByte[0]);
                         }

                     }
                     //Save Data
                     {
                         savbit.CopyTo(SaveByt, 0);
                         Nextfiling.Write(SaveByt, 0, bs / 8);

                         int start = (bs / 8) * 8, end = bs;
                         bs = 0;
                         while (start != end)
                         {
                             savbit[bs] = savbit[start]; bs++; start++;
                         }
                     }


                 }//End ProcessTimer2


                 //Save RestData And Info
                 {
                     //AddBit
                     if (bs != 0)
                     {
                         Addbit = 8 - (Convert.ToInt32(bs % 8));
                         BitArray bitrest = new BitArray(8, false);
                         int c = 0;
                         while (c != bs)
                         {
                             bitrest[c] = savbit[c]; c++;
                         }

                         byte[] restbitD = new byte[1];
                         bitrest.CopyTo(restbitD, 0);
                         Nextfiling.Write(restbitD, 0, 1);
                     }
                     //Info && Dic
                     {
                         
                         Nextfiling.Write(listDic.ToArray(), 0, listDic.Count);
                         /* 0 Addbit 1-4 intOfCountListDic  */
                         byte[] InfoRest = new byte[5];

                         InfoRest[0] = numoperation2.int32toByte1(Addbit);

                         byte[] intListDic = BitConverter.GetBytes(listDic.Count);
                         intListDic.CopyTo(InfoRest, 1);

                         Nextfiling.Write(InfoRest, 0, 5);
                     }

                 }
                 //Close Files

                 filecomp.Close();
                 mainfiling.Close();


             }//End Files

            
             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - startTime;

             sb.Append("Time End Mix2Files = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));

         }
         public void StartCompBy2(string pathFile, string NumComp)
         {
             if (countenu == true)
             {


                 if (MessageBox.Show("Start Comp =  Yes  😄😄😄😄  !!", "StartComp", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                     return;


                 // MessageBox.Show(" Yes  😄😄😄😄  !!");

                 FileInfo fil = new FileInfo(pathFile);
                 string FileCompDerct = fil.FullName.Remove(fil.FullName.Length - fil.Extension.Length);
                 Directory.CreateDirectory(FileCompDerct);

                 //Filing
                 string saveMain = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".minF" + NumComp.ToString();
                 string saveNext = FileCompDerct + "/" + fil.Name.Remove(fil.Name.Length - fil.Extension.Length) + ".nexF" + NumComp.ToString();

                 CompfilingBy2(pathFile, saveMain, saveNext);

             }
             else
             {
                 MessageBox.Show(" You Can't Contenue  !!");
                 
             }



         }


         public string PrintTemplist2()
         {
             StringBuilder sblist = new StringBuilder();
             int o = 0;
             foreach (ListCompNode con in Templist2)
             {
                 o++;
                 //if (o > 100)
                 //    break;
                 sblist.Append(con.RealValue.ToString("00000") + " = ");
                 sblist.Append(con.LevelCount.ToString("   0   ")+"  =  "+con.OrigByte[0].ToString());
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




             

             return sblist.ToString();
         }
         public string PrintMainList()
         {
             StringBuilder sblist = new StringBuilder();

             sblist.Append("\n\n\n\n   ***MainList***  \n\n****  RealValue = && LevelCount = && TotalCount =  ***\n\n");
               sblist.AppendLine(" ");
             foreach (ListCompNode con in MainList) 
             {
                 sblist.Append(con.RealValue.ToString("000") + " = ");
                 sblist.Append(con.LevelCount.ToString("    0    ") + "  =  ");
                 sblist.Append(con.TotalCount.ToString("    0    ") + "\n");
               
                
             }
             sblist.AppendLine(" ");
             sblist.AppendLine(" ");
             sblist.AppendLine(" ");
           





             return sblist.ToString();
         }



         /****************** By 3 ****************************/
         public void WalkThroListBy3(string filingPath)
         {
             DateTime start = DateTime.Now;
             

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
                 MainList = new List<ListCompNode>();

                 sb.Append("\nTime Start Tr.creatBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));   
                 Tr.creatBy3(ref MainList);
                 sb.Append("\nTime End Tr.creatBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

                 sb.Append("\nTime Start WalkThroListBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

                 bool isFirst = true;
                 bool isSecound = false;
                 int Temp1 = 0, Temp2 = 0;

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
                                 Temp1 = n;
                                 isFirst = false;
                                 isSecound = true;
                             }
                             else
                             {
                                 if (isSecound == true)
                                 {
                                     Temp2 = n;
                                     isSecound = false;
                                 }
                                 else
                                 {
                                     MainList[Temp1].nextTree[Temp2].nextTree[n].LevelCount++;
                                     isFirst = true;
                                 }
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
                             Temp1 = n;
                             isFirst = false;
                             isSecound = true;
                         }
                         else
                         {
                             if (isSecound == true)
                             {
                                 Temp2 = n;
                                 isSecound = false;
                             }
                             else
                             {
                                 MainList[Temp1].nextTree[Temp2].nextTree[n].LevelCount++;
                                 isFirst = true;
                             }
                         }
                     }

                 }//End ProcessTimer2


                 //LastByte
                
                 if (isFirst == false)
                 {
                     if (isSecound == false)
                     {
                         MainList[Temp1].nextTree[Temp2].LevelCount++;
                     }
                     else
                     {
                         MainList[Temp1].LevelCount++;
                     }
                 }
               

                 filing.Close();
             }


             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - start;

             sb.Append("Time End WalkThroListBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));
         }
         public void StartAnalysisListBy3()
         {

             DateTime start = DateTime.Now;
             sb.Append("\nTime Start AnalysisListBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " + "\n"));

             RePort = new StringBuilder();

             SortLestAndComputeLevelBy3();

             AnalysisListBy3();

          //   AnalysisListBy2();

             RePort.Append("\n\nSizeisEqual = " + SizeisEqual.ToString());
             RePort.Append("\n\n\n********* TotalReduce = " + Reduce.ToString() + "  =  " + (Reduce / 1024).ToString() + " KB " + "  =  " + (Reduce / (1024 * 1024)) +" MB "+ "\n\n\n\n");





             DateTime finish = DateTime.Now;
             TimeSpan sd = finish - start;

             sb.Append("Time End AnalysisListBy3 = " + DateTime.Now.ToString("hh : mm : ss tt " +
                "\nCompare = ( S = " + sd.Seconds.ToString() + " : M = " + sd.Minutes.ToString()

                 + "\n\n"));

         }
         private void AnalysisListBy3()
         {

             //TotalInfo
             int FirstWithSecound = 0;
             int FirstWithTheard = 0;
             int SecoundWithTherd = 0;
             int NonReduce = 0;
          

                          



             //Pointers

             int TotalReduce = 0;


             //SortByCout&&Compute With Level 3
             {
                 int Level1Reduce = 0;
                 int Level2Reduce = 0;
                 int ReduceWithFirst = 0;


                 //Dictionary
                 List<ListCompNode> MainDic = new List<ListCompNode>();
                 List<byte> MainDicByte = new List<byte>();

                 List<ListCompNode> TempDic = new List<ListCompNode>();
                 ListCompNode NodWithFirst = new ListCompNode();
                 bool isTheNod = false;

                 int Mainlocate = 0;
                 int SecoundLocate = 0;
                 bool DicIsWithfirst = false;


                 foreach (ListCompNode n in MainList)
                 {
                     ReduceWithFirst = 0;
                     Level1Reduce = 0;
                     NodWithFirst = new ListCompNode();
                     TempDic = new List<ListCompNode>();
                     isTheNod = false;
                     DicIsWithfirst = false;

                     foreach (ListCompNode n1 in n.nextTree)
                     {
                         Level2Reduce = 0;

                         Tr.quicksort(ref n1.nextTree, 0, 255);
                         Mainlocate = Convert.ToInt32(n1.nextTree[255].OrigByte[0]);
                         SecoundLocate = Convert.ToInt32(n1.nextTree[255].OrigByte[1]);

                         if ((n.nextTree[SecoundLocate].LevelCount / 8) + 3 < n1.nextTree[255].LevelCount)
                         {
                             Level2Reduce = n1.nextTree[255].LevelCount - ((n.nextTree[SecoundLocate].LevelCount / 8) + 3);
                             TempDic.Add(n1);
                         }



                         //NodWithFirst
                         if ((MainList[Mainlocate].LevelCount / 8) + 3 < (n1.nextTree[255].LevelCount) * 2) 
                         {
                             int Reduceit = ((n1.nextTree[255].LevelCount) * 2) - ((MainList[Mainlocate].LevelCount / 8) + 3);
                             if (Reduceit > ReduceWithFirst)
                                 ReduceWithFirst = Reduceit;
                             NodWithFirst.nextNode = n1;

                         }

                         Level1Reduce = Level1Reduce + Level2Reduce;

                     }

                     if (Level1Reduce == 0 && ReduceWithFirst == 0)
                     {

                     }
                     else
                     {
                         if (Level1Reduce < ReduceWithFirst)
                         {
                             Level1Reduce = ReduceWithFirst;
                             isTheNod = true;

                         }
                         else
                         {
                             isTheNod = false;
                         }
                     }

                     //Secound
                     {
                         Tr.quicksort(ref n.nextTree, 0, 255);

                         Mainlocate = Convert.ToInt32(n.nextTree[255].OrigByte[0]);
                         if ((MainList[Mainlocate].LevelCount / 8) + 3 < n.nextTree[255].LevelCount)
                         {
                             //Error 01
                             // int Reduceit = Level2Reduce = n.nextTree[255].LevelCount - ((MainList[Mainlocate].LevelCount / 8) + 3);
                             int Reduceit = n.nextTree[255].LevelCount - ((MainList[Mainlocate].LevelCount / 8) + 3);

                             if (Reduceit > Level1Reduce)
                             {
                                 Level1Reduce = Reduceit;
                                 DicIsWithfirst = true;

                             }
                             else
                             {
                                 DicIsWithfirst = false;

                             }

                         }

                     }



                     //CopyDic
                     {
                         if (Level1Reduce != 0)
                         {
                             if (DicIsWithfirst == true)
                             {
                                 MainDic.Add(n.nextTree[255]);
                                 MainDicByte.Add(num[3]);
                                 FirstWithSecound++;
                             }
                             else
                             {
                                 if (isTheNod == true)
                                 {
                                     MainDic.Add(NodWithFirst.nextNode);
                                     MainDicByte.Add(num[2]);
                                     FirstWithTheard++;
                                 }
                                 else
                                 {
                                     foreach (ListCompNode TempNode in TempDic)
                                     {
                                         MainDic.Add(NodWithFirst.nextNode);
                                         MainDicByte.Add(num[0]);
                                         SecoundWithTherd++;
                                     }
                                 }

                             }

                         }
                         else
                         {
                             NonReduce++;
                         }

                     }

                     TotalReduce = TotalReduce + Level1Reduce;

                 }
             }


        

             if (TotalReduce != 0)
             {
                 Reduce = TotalReduce;
                 RePort.Append("\n\n\n" +
                     "  continue = True  " +
                     "TotalReduce = " + TotalReduce.ToString() + "  =  " + (TotalReduce / 1024).ToString() + " KB ");

                 RePort.Append(
              "\n\n"+
              "\nFistWithSecound = " + FirstWithSecound.ToString() +
              "\nFirstWithTheard = " + FirstWithTheard.ToString() +
              "\nSecoundWithTherd = " + SecoundWithTherd.ToString() + 
              "\nNonReduce = "+NonReduce.ToString()+
              "\n\n");


             }
             else
             {
                 RePort.Append(
                                    "  continue = False  ");


             }



         }

         private void SortLestAndComputeLevelBy3()
         {
             {
                 int Counter3 = 0;
                 int Counter2 = 0;
                 
                 foreach (ListCompNode n in MainList)
                 {
                     Counter2 = 0;
                     foreach (ListCompNode n1 in n.nextTree)
                     {
                         Counter3 = 0;
                         foreach (ListCompNode n2 in n1.nextTree)
                         {
                             Counter3 = Counter3 + n2.LevelCount;
                         }
                         n1.LevelCount = n1.LevelCount + Counter3;

                         Counter2 = Counter2 + n1.LevelCount;
                     }

                     n.LevelCount = n.LevelCount + Counter2;
                 }

             }

             ////TotalCounter
             //{
             //    long TotalCounter = 0;   
             //    foreach (ListCompNode n in MainList)
             //    {

             //    }
             //}

             ////ExmineSize
             //{
             //    long SumSize = 0;
             //    foreach (ListCompNode n in MainList)
             //    {
             //        SumSize = SumSize + n.TotalCount;
             //    }

             //    if (SumSize == origFilelength)
             //    {
             //        SizeisEqual = true;

             //    }
             //    else
             //    {
             //        MessageBox.Show("Size Isn't Equal ...!" + "\n\n\nCompareSize = " + (origFilelength - SumSize).ToString(), "SizeError");
             //    }





             //}



             //   //SortByCout
             //   {
             //       foreach (ListCompNode n in MainList)
             //       {
             //           foreach (ListCompNode n1 in n.nextTree)
             //           {
             //               Tr.quicksort(ref n1.nextTree , 0,255);

             //           }
             ////           Tr.quicksort(ref n.nextTree, 0, 255);
             //       }
             ////       Tr.quicksort(ref MainList , 0, 255);
             //   }




         }

     }


}
