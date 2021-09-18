using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.CheckFiles.OldChecker.Checker_Count
{

    class CheckerCountByNumNod01
    {
        public List<CheckerCountByNumNod01> NextList;
        public int FirstNumbers = 0;
        public int SecondNumbers = 0;
       

        #region For Counters

        public int Counter = 0;
        public int FirstLocateCounter = 0;
        public int SecoundLocateCounter = 0;
        public int TotalCounter = 0;

        public void count()
        {
            Counter++;
        }

        #endregion


        public CheckerCountByNumNod01(int FNumbers , int SNumbers)
        {
            FirstNumbers = FNumbers;
            SecondNumbers = SNumbers;
        }

    }

    class CheckerCountByNumTree01
    {
        
         private int ModLength = 256;
         public List<CheckerCountByNumNod01> NumberList;
         public CheckerCountByNumNod01[] TempSortList;
        

         public CheckerCountByNumTree01()
         {
             Create();
         }
         public CheckerCountByNumTree01(int ModLengthNumber)
         {
             ModLength = ModLengthNumber;
             Create();
         }
         private void Create()
         {
             //03 create && fill NumberList
             int idCounter = 0;
             NumberList = new List<CheckerCountByNumNod01>();
             for (int i = 0; i != ModLength; i++)
             {
                 NumberList.Add(new CheckerCountByNumNod01(i, 0));
                 List<CheckerCountByNumNod01> TempList = new List<CheckerCountByNumNod01>();
                 for (int n = 0; n != ModLength; n++)
                 {
                     TempList.Add(new CheckerCountByNumNod01(i, n));
                     idCounter++;
                 }
                 NumberList[i].NextList = TempList;
             }
         }

         public void RefrishCounter()
         {
             int MainCounter = 0;
             foreach (CheckerCountByNumNod01 MainNod in NumberList)
             {
                 MainCounter = 0;
                 foreach (CheckerCountByNumNod01 Nod in MainNod.NextList)
                 {
                     MainCounter = MainCounter + Nod.Counter;
                 }
                 MainNod.FirstLocateCounter = MainCounter;
             }

             int TempCounter = 0;
             for (int i = 0; i != ModLength; i++)
             {
                 TempCounter = 0;
                 for (int n = 0; n != ModLength; n++)
                 {
                     TempCounter = TempCounter + NumberList[n].NextList[i].Counter;

                 }
                 NumberList[i].SecoundLocateCounter = TempCounter;
                 NumberList[i].TotalCounter = NumberList[i].FirstLocateCounter + NumberList[i].SecoundLocateCounter;
             } 



         }

         #region Sort

         #region  SortAsFirstCounter

         public void SortAsFirstCounter()
         {
             TempSortList = NumberList.ToArray<CheckerCountByNumNod01>();
             quicksortFirstCounter(ref TempSortList, 0, TempSortList.Length - 1);
         }

         private void quicksortFirstCounter(ref CheckerCountByNumNod01[] list, int begin, int end)
         {
             CheckerCountByNumNod01 pivot = list[(begin + (end - begin) / 2)];
             int left = begin;
             int right = end;
             while (left <= right)
             {
                 while (list[left].FirstLocateCounter < pivot.FirstLocateCounter)
                 {
                     left++;
                 }
                 while (list[right].FirstLocateCounter > pivot.FirstLocateCounter)
                 {
                     right--;
                 }
                 if (left <= right)
                 {
                     swapFirstCounter(ref list, left, right);
                     left++;
                     right--;
                 }
             }
             if (begin < right)
             {
                 quicksortFirstCounter(ref list, begin, left - 1);
             }
             if (end > left)
             {
                 quicksortFirstCounter(ref list, right + 1, end);
             }
         }
         private void swapFirstCounter(ref CheckerCountByNumNod01[] list, int x, int y)
         {
             CheckerCountByNumNod01 temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }

         #endregion
         #region Sort AsSecoundCounter
         public void SortAsSecoundCounter()
         {
             TempSortList = NumberList.ToArray<CheckerCountByNumNod01>();
             quicksortSecoundCounter(ref TempSortList, 0, TempSortList.Length - 1);
         }

         private void quicksortSecoundCounter(ref CheckerCountByNumNod01[] list, int begin, int end)
         {
             CheckerCountByNumNod01 pivot = list[(begin + (end - begin) / 2)];
             int left = begin;
             int right = end;
             while (left <= right)
             {
                 while (list[left].SecoundLocateCounter < pivot.SecoundLocateCounter)
                 {
                     left++;
                 }
                 while (list[right].SecoundLocateCounter > pivot.SecoundLocateCounter)
                 {
                     right--;
                 }
                 if (left <= right)
                 {
                     swapSecoundCounter(ref list, left, right);
                     left++;
                     right--;
                 }
             }
             if (begin < right)
             {
                 quicksortSecoundCounter(ref list, begin, left - 1);
             }
             if (end > left)
             {
                 quicksortSecoundCounter(ref list, right + 1, end);
             }
         }
         private void swapSecoundCounter(ref CheckerCountByNumNod01[] list, int x, int y)
         {
             CheckerCountByNumNod01 temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }

         #endregion
         #region SortAsTotalCounter

         public void SortAsTotalCounter()
         {
             TempSortList = NumberList.ToArray<CheckerCountByNumNod01>();
             quicksortTotalCounter(ref TempSortList, 0, TempSortList.Length - 1);
         }

         private void quicksortTotalCounter(ref CheckerCountByNumNod01[] list, int begin, int end)
         {
             CheckerCountByNumNod01 pivot = list[(begin + (end - begin) / 2)];
             int left = begin;
             int right = end;
             while (left <= right)
             {
                 while (list[left].TotalCounter < pivot.TotalCounter)
                 {
                     left++;
                 }
                 while (list[right].TotalCounter > pivot.TotalCounter)
                 {
                     right--;
                 }
                 if (left <= right)
                 {
                     swapTotalCounter(ref list, left, right);
                     left++;
                     right--;
                 }
             }
             if (begin < right)
             {
                 quicksortTotalCounter(ref list, begin, left - 1);
             }
             if (end > left)
             {
                 quicksortTotalCounter(ref list, right + 1, end);
             }
         }
         private void swapTotalCounter(ref CheckerCountByNumNod01[] list, int x, int y)
         {
             CheckerCountByNumNod01 temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }

         #endregion

         #endregion

    }


    class CheckerCountByNum01
    {
         public StringBuilder Report=new StringBuilder();
         private int ModLength = 256;
         private int LengthReader = 1024;

         public CheckerCountByNum01()
         {

         }
         public CheckerCountByNum01(int ModLengthNumber)
         {
             ModLength = ModLengthNumber;

         }

         public string CheckerCountForFile()
         {
             #region  Read File

            
             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);

             if (ReaderNum.GetIsCancel)
                 return "isCancel";

             CheckerCountByNumTree01 Tree = new CheckerCountByNumTree01(ModLength);

             int First = 0;
             // int Secound = 0;
             bool isFirst = true;
             while (ReaderNum.isAbleRead)
             {
                 List<int> TempList = ReaderNum.GetManyNum(LengthReader);
                 foreach (int n in TempList)
                 {
                     if (isFirst)
                     {
                         First = n;
                         isFirst = false;
                     }
                     else
                     {
                         Tree.NumberList[First].NextList[n].count();
                         isFirst = true;
                     }
                 }
             }


             ReaderNum.End();
             #endregion

             Report = new StringBuilder();
             Tree.RefrishCounter();
             #region

             Report.Append(
                    "\n\n********* InfoCounter **********\n" +
                    "\nModLength = " + ModLength.ToString() +

                    "\n\n");

          

           
             //SortAsFirstCounter
             {
                 Report.Append(
                "\n\n********* SortAsFirstCounter **********\n" +

                "\n\n");
                 Tree.SortAsFirstCounter();

                 foreach (CheckerCountByNumNod01 nod in Tree.TempSortList)
                 {
                     Report.Append(
                         "\n" + nod.FirstNumbers.ToString("0000") + " => " + nod.FirstLocateCounter.ToString("    0    "));
                 }

             }
             //SortAsSecoundCounter
             {
                 Report.Append(
                "\n\n********* SortAsSecoundCounter **********\n" +

                "\n\n");
                 Tree.SortAsSecoundCounter();

                 foreach (CheckerCountByNumNod01 nod in Tree.TempSortList)
                 {
                     Report.Append(
                         "\n" + nod.FirstNumbers.ToString("0000") + " => " + nod.SecoundLocateCounter.ToString("    0    "));
                 }

             }
             //SortAsTotalCounter
             {
                 Report.Append(
                "\n\n********* SortAsTotalCounter **********\n" +

                "\n\n");
                 Tree.SortAsTotalCounter();

                 foreach (CheckerCountByNumNod01 nod in Tree.TempSortList)
                 {
                     Report.Append(
                         "\n" + nod.FirstNumbers.ToString("0000") + " => " + nod.TotalCounter.ToString("    0    "));
                 }

             }


             Report.Append(
             "\n\n********* EndInfoCounter **********\n" +

             "\n\n");

             #endregion



            // System.Windows.Forms.MessageBox.Show(Report.ToString(), "InfoCounters", System.Windows.Forms.MessageBoxButtons.OKCancel);
                


             return Report.ToString();

         }


         public string CheckerCountForFile_W2()
         {
             #region  Read File


             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength ,"txt");

             if (ReaderNum.GetIsCancel)
                 return "isCancel";

             //CheckerCountByNumTree01 Tree = new CheckerCountByNumTree01(ModLength);
             CheckerCountByNumNod01[] ListCounter = new CheckerCountByNumNod01[ModLength];
             {
                 for (int i = 0; i != ModLength; i++)
                 {
                     ListCounter[i] = new CheckerCountByNumNod01(i, 0);

                 }
                
             }


             while (ReaderNum.isAbleRead)
             {
                 List<int> TempList = ReaderNum.GetManyNum(LengthReader);
                 foreach (int n in TempList)
                 {
                     ListCounter[n].count();
                 }
             }


             ReaderNum.End();




             {
                 foreach (CheckerCountByNumNod01 nod in ListCounter)
                 {
                     nod.FirstLocateCounter = nod.Counter;
                 }

             }


             #endregion

             Report = new StringBuilder();
             //       ListCounter.RefrishCounter();

             
            

             if ((System.Windows.Forms.MessageBox.Show("InfoCounters to File", "InfoCounters", System.Windows.Forms.MessageBoxButtons.OKCancel)) == System.Windows.Forms.DialogResult.OK)
             {

                 #region
                 Report.AppendLine(" ");
                 Report.AppendLine(" ");
                 Report.Append(
                        "********* InfoCounter **********" );
                       Report.AppendLine(" "); 
                 Report.Append( "ModLength = " + ModLength.ToString() );
                 Report.AppendLine(" "); Report.AppendLine(" ");



                 //SortAsMostCounter
                 {
                     Report.AppendLine(" ");
                     Report.AppendLine(" ");
                     Report.Append(
                    "********* SortAsMostCounter **********");
                     Report.AppendLine(" ");
                     Report.AppendLine(" ");
                     Report.AppendLine(" ");

                     quicksortFirstCounter(ref ListCounter, 0, ListCounter.Length - 1);


                     foreach (CheckerCountByNumNod01 nod in ListCounter)
                     {
                         Report.AppendLine(" ");
                         Report.Append( nod.FirstNumbers.ToString("00000") + " => " + nod.FirstLocateCounter.ToString("    0    "));
                     }

                 }


                 Report.AppendLine(" ");
                 Report.AppendLine(" ");
                 Report.Append(
                 "********* EndInfoCounter **********\n");
                 Report.AppendLine(" ");
                 Report.AppendLine(" ");

                 #endregion

                 File.WriteAllText(ReaderNum.GetSavePath, Report.ToString());

                 return " print to File";
             }
             else
             {
                 #region

                 Report.Append(
                        "\n\n********* InfoCounter **********\n" +
                        "\nModLength = " + ModLength.ToString() +

                        "\n\n");




                 //SortAsMostCounter
                 {
                     Report.Append(
                    "\n\n********* SortAsMostCounter **********\n" +

                    "\n\n");

                     quicksortFirstCounter(ref ListCounter, 0, ListCounter.Length - 1);


                     foreach (CheckerCountByNumNod01 nod in ListCounter)
                     {
                         Report.Append(
                             "\n" + nod.FirstNumbers.ToString("00000") + " => " + nod.FirstLocateCounter.ToString("    0    "));
                     }

                 }



                 Report.Append(
                 "\n\n********* EndInfoCounter **********\n" +

                 "\n\n");

                 #endregion
             }
            
            



             return Report.ToString();

         }


         #region Sort ListCounter

         private void quicksortFirstCounter(ref CheckerCountByNumNod01[] list, int begin, int end)
         {
             CheckerCountByNumNod01 pivot = list[(begin + (end - begin) / 2)];
             int left = begin;
             int right = end;
             while (left <= right)
             {
                 while (list[left].FirstLocateCounter < pivot.FirstLocateCounter)
                 {
                     left++;
                 }
                 while (list[right].FirstLocateCounter > pivot.FirstLocateCounter)
                 {
                     right--;
                 }
                 if (left <= right)
                 {
                     swapFirstCounter(ref list, left, right);
                     left++;
                     right--;
                 }
             }
             if (begin < right)
             {
                 quicksortFirstCounter(ref list, begin, left - 1);
             }
             if (end > left)
             {
                 quicksortFirstCounter(ref list, right + 1, end);
             }
         }
         private void swapFirstCounter(ref CheckerCountByNumNod01[] list, int x, int y)
         {
             CheckerCountByNumNod01 temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }

         #endregion
    }
}
