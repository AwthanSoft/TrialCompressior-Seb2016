using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.CompBy.SeparateFiles.SumSeparateFile
{
     class SumSeparateNode01
    {
        #region Proprties For Nodes
        public List<SumSeparateNode01> NextList = new List<SumSeparateNode01>();
        public int IdNode = 0;
        public int IdCounter = 0;

        
        public int FirstNumbers = 0;
        public int SecondNumbers = 0;
        public int SumNumbers = 0;


        public int PossibleNumbers = 0;

        public List<bool> BitsListKey;
        public List<bool> BitsListPossible;


        #endregion

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

        #region For WriterBits

        public ReaderWriteFileBits02B BitsKeyWriter;
        public ReaderWriteFileBits02B BitsPossibleWriter;

        #endregion

        #region InfoNodes
        //Nodes Info

        public int OrignalBits = 0;
        public int KeyBits = 0;
        public int PossBits = 0;
        public int SumBitsAfter = 0;
        public int numCompBits = 0;
        public bool isComps = false;
        public bool isBitsEqual = false;

        #endregion

        #region Overload

        public SumSeparateNode01()
        {


        }
        public SumSeparateNode01(int NodesNumber)
        {
            IdNode = NodesNumber;
        }

        #endregion

        #region Refrishes For Nods
        public void Refrish(int FirstNum, int SecoundNum, List<bool> ListKey, List<bool> ListPossible, int CounterId,int PossibleNum)
        {
            FirstNumbers = FirstNum;
            SecondNumbers = SecoundNum;
            SumNumbers = FirstNumbers + SecondNumbers;

            BitsListKey = ListKey;
            BitsListPossible = ListPossible;

            IdCounter = CounterId;
            PossibleNumbers = PossibleNum;
        }
        public void RefrishInfoNod(int orignalBitsNum)
        {
            OrignalBits = orignalBitsNum;
            KeyBits = BitsListKey.Count;
            PossBits = BitsListPossible.Count;
            SumBitsAfter = KeyBits + PossBits;

            numCompBits = orignalBitsNum - SumBitsAfter;
            if (numCompBits < 0)
            {
                isComps = false;
            }
            else
            {
                if (numCompBits == 0)
                    isBitsEqual = true;
                else
                    isBitsEqual = false;

                isComps = true;
            }


        }

        #endregion
        #region Writer

        private void WriteKey()
        {
            foreach (bool b in BitsListKey)
            {
                BitsKeyWriter.WriteBit(b);
            }
        }
        private void WritePossible()
        {
            foreach (bool b in BitsListPossible)
            {
                BitsPossibleWriter.WriteBit(b);
            }
        }
        public void Write()
        {
            WriteKey();
            WritePossible();
            Counter++;
        }

        #endregion
        #region  Refrish Writers

        public void RefrishWriter(ref ReaderWriteFileBits02B WriterBits)
        {
            BitsKeyWriter = WriterBits;
            BitsPossibleWriter = WriterBits;
        }
        public void RefrishWriter(ref ReaderWriteFileBits02B WriterBitsKey, ref ReaderWriteFileBits02B WriterBitsPossible)
        {
            BitsKeyWriter = WriterBitsKey;
            BitsPossibleWriter = WriterBitsPossible;
        }

        #endregion

        

    }

     class SumSeparateInfoNode01
    {
        public int IdNode = 0;
        public List<bool> BitsList;
       
        public List<int> FirstNumbers = new List<int>();
        public List<int> SecondNumbers = new List<int>();
    //    public List<int> SumNumbers = new List<int>();
        public List<int> IdCounter = new List<int>();

        public int SumPossibleNumbers = 0;

        public SumSeparateInfoNode01()
        {
            

        }
        public SumSeparateInfoNode01(int NodesNumber)
        {
            IdNode = NodesNumber;
        }

        public void AddNumber(int FirstNum, int SecoundNum , int CounterId , List<bool > ListKey)
        {
            FirstNumbers.Add(FirstNum);
            SecondNumbers.Add(SecoundNum);
            IdCounter.Add(CounterId);
            SumPossibleNumbers++;
            BitsList = ListKey;
        }
        public void RefrishNod(ref List<SumSeparateNode01> MainList)
        {
            if (SumPossibleNumbers <= 1)
            {
                //if (SumPossibleNumbers == 0)
                //{

                //}
                //else
                {

                    List<bool> tempList = new List<bool>();
                    MainList[FirstNumbers[0]].NextList[SecondNumbers[0]].Refrish(FirstNumbers[0], SecondNumbers[0], BitsList, tempList, IdCounter[0], 0);
                
                }
            }
            else
            {
                OneIntBitsTree02 Tree = new OneIntBitsTree02(SumPossibleNumbers);
                for (int i = 0; i != SumPossibleNumbers; i++)
                {
                    MainList[FirstNumbers[i]].NextList[SecondNumbers[i]].Refrish(FirstNumbers[i], SecondNumbers[i], BitsList, Tree.ListNumber[i].BitsList, IdCounter[i], i);
                }
            }


        }
    }

     class SumSeparateSegmentInfoNod01
     {
       public int LengthList = 0;

       public int SumOrig = 0;
       public int SumAfter = 0;
       public int SizComped = 0;

       public int SumNodsIsComp = 0;
       public int SumNodsNoComp = 0;
       public int SumNodIsEqual = 0;

       private bool iSCompAble = false;

       public SumSeparateSegmentInfoNod01()
         {

         }

       public bool CompedAble
       {
           get
           {
               if (SumAfter < SumOrig)
               {
                   iSCompAble = true;
               }
               return iSCompAble;
           }
           

       }

       public void UpdateInfo(ref SumSeparateNode01 po)
       {
           LengthList++;

           SumOrig = SumOrig + po.OrignalBits;
           SumAfter = SumAfter + po.SumBitsAfter;
           SizComped = SizComped + po.numCompBits;

           if (po.isComps)
           {
               if (po.isBitsEqual)
                   SumNodIsEqual++;
               else
                   SumNodsIsComp++;
           }
           else
               SumNodsNoComp++;

           

       }
       public string GetInfoSeg()
       {
           StringBuilder Report = new StringBuilder();

           Report.Append(
               "**************************************"+
               "\nLengthList = " + LengthList.ToString() +
               "\n" +
               "\nSumOrig = " + SumOrig.ToString() + " = " +
                                  (SumOrig / 8).ToString() + "B = " +
                                  ((SumOrig / 8) / 1024) + " KB = " +
                                  (((SumOrig / 8) / 1024) / 1024) + " MB " +
               "\nSumAfter = " + SumAfter.ToString() + " = " +
                                  (SumAfter / 8).ToString() + "B = " +
                                  ((SumAfter / 8) / 1024) + " KB = " +
                                  (((SumAfter / 8) / 1024) / 1024) + " MB " +

               "\n"+
               "\nSumNodsIsComp = " + SumNodsIsComp.ToString()+
               "\nSumNodsNoComp = " + SumNodsNoComp.ToString() +
               "\nSumNodIsEqual = " + SumNodIsEqual.ToString() +
               "\n"+
               "\niSCompAble = "+iSCompAble.ToString()+
               "\nSizComped = " + SizComped.ToString() + " = " +
                                  (SizComped / 8).ToString() + "B = " +
                                  ((SizComped / 8) / 1024) + " KB = " +
                                  (((SizComped / 8) / 1024) / 1024) + " MB " +

                                  "\n");

           return Report.ToString();
       }

     }

     class SumSeparateInfoTree01
     {
         private int ModLength = 256;
         public List<SumSeparateNode01> NumberList;

         public SumSeparateInfoTree01()
         {
             Create();
         }
         public SumSeparateInfoTree01(int ModLengthNumber)
         {
             ModLength = ModLengthNumber;
             Create();
         }

         private void Create()
         {

             //01 Create ListInfoNumber
             int Timer = ((ModLength - 1) * 2) + 1;
             OneIntBitsTree02 Tree = new OneIntBitsTree02(Timer);
             List<SumSeparateInfoNode01> ListInfoNumber = new List<SumSeparateInfoNode01>(Timer);
             for (int i = 0; i != Timer; i++)
             {
                 ListInfoNumber.Add(new SumSeparateInfoNode01(i));
             }


             //02 Fill ListInfoNumber
             int idCounter = 0;
             for (int i = 0; i != ModLength; i++)
             {
                 for (int n = 0; n != ModLength; n++)
                 {
                     ListInfoNumber[i + n].AddNumber(i, n, idCounter, Tree.ListNumber[i + n].BitsList);
                     idCounter++;
                 }
             }


             //03 create && fill NumberList
             idCounter = 0;
             NumberList = new List<SumSeparateNode01>();
             for (int i = 0; i != ModLength; i++)
             {
                 NumberList.Add(new SumSeparateNode01(i));
                 List<SumSeparateNode01> TempList = new List<SumSeparateNode01>();
                 for (int n = 0; n != ModLength; n++)
                 {
                     TempList.Add(new SumSeparateNode01(idCounter));
                     idCounter++;
                 }
                 NumberList[i].NextList = TempList;
             }


             //05 refrish NumberList
             foreach (SumSeparateInfoNode01 nod in ListInfoNumber)
             {
                 nod.RefrishNod(ref NumberList);
             }


             //06 InfoRefrish NumberList
             {
                 OneIntBitsTree02 InfoTree = new OneIntBitsTree02(ModLength);
                  idCounter = 0;
                  int OrigBits = 0;
                 for (int i = 0; i != ModLength; i++)
                 {
                     for (int n = 0; n != ModLength; n++)
                     {
                         OrigBits = InfoTree.ListNumber[i].BitsList.Count + InfoTree.ListNumber[n].BitsList.Count;
                         NumberList[i].NextList[n].RefrishInfoNod(OrigBits);
                     }
                 }
             }








         }


         public SumSeparateSegmentInfoNod01 CompSegmentInfo(ref List<int> ListData)
         {
             SumSeparateSegmentInfoNod01 SegInfo = new SumSeparateSegmentInfoNod01();
             SumSeparateNode01 po;

            // SegInfo.LengthList = ListData.Count;

             bool isFirst = true;
             int First = 0;

             foreach (int n in ListData)
             {
                 if (isFirst)
                 {
                     First = n;
                     isFirst = false;
                 }
                 else
                 {
                     po = NumberList[First].NextList[n];
                     SegInfo.UpdateInfo(ref po);
                     isFirst = true;
                 }
             }

             return SegInfo;
         }

         #region Refrish Writer

         public void RefrishWriterW01(ref ReaderWriteFileBits02B WriterBits)
         {
             foreach (SumSeparateNode01 mainNod in NumberList)
             {
                 foreach (SumSeparateNode01 nod in mainNod.NextList)
                 {
                     nod.RefrishWriter(ref WriterBits);
                 }
             }
         }
         public void RefrishWriterW01(ref ReaderWriteFileBits02B WriterBitsKey, ref ReaderWriteFileBits02B WriterBitsPossible)
         {
             foreach (SumSeparateNode01 mainNod in NumberList)
             {
                 foreach (SumSeparateNode01 nod in mainNod.NextList)
                 {
                     nod.RefrishWriter(ref WriterBitsKey, ref WriterBitsPossible);
                 }
             }
         }

         #endregion

         #region  For Info

         public string InfoTree()
         {
             StringBuilder Report = new StringBuilder();

             Report.Append(
                 "\n\n********* InfoTree **********\n" +
                 "\nMod = " + ModLength.ToString() +
                 "\nModLength = " + (ModLength * 2).ToString() +
                 "\nNodesNumber = " + (ModLength * ModLength).ToString() +

                 "\n\n");

             int[] TreeInfo = new int[ModLength * 2];
             int idCounter = 0;
             for (int i = 0; i != ModLength; i++)
             {
                 for (int n = 0; n != ModLength; n++)
                 {
                     TreeInfo[i + n]++;
                     idCounter++;
                 }
             }


             idCounter = 0;
             foreach (int n in TreeInfo)
             {
                 Report.Append("\n" + idCounter.ToString("00000") + " = " + n.ToString());
             }

             quicksort(ref TreeInfo, 0, TreeInfo.Length - 1);

             Report.Append("\n\n\nAs Sorted *******\n\n\n");
             idCounter = 0;
             foreach (int n in TreeInfo)
             {
                 Report.Append("\n" + idCounter.ToString("00000") + " = " + n.ToString());
             }



             Report.Append("\n\n\n Finish *******\n\n\n");

             return Report.ToString();
         }

         private void CollectCountersLocate()
         {
             #region Collect Counters
             //FirstLocateCounter
             {
                 int Counter = 0;
                 foreach (SumSeparateNode01 mainNod in NumberList)
                 {
                     Counter = 0;
                     foreach (SumSeparateNode01 nod in mainNod.NextList)
                     {
                         Counter = Counter + nod.Counter;
                     }
                     mainNod.FirstLocateCounter = Counter;
                 }
             }


             //SecoundLocateCounter
             {
                 int Number = 0;
                 int Counter = 0;

                 foreach (SumSeparateNode01 mainNod in NumberList)
                 {
                     Counter = 0;
                     foreach (SumSeparateNode01 nod in NumberList)
                     {
                         Counter = Counter + nod.NextList[Number].Counter;
                     }
                     mainNod.SecoundLocateCounter = Counter;
                 }
             }


             //SecoundLocateCounter
             {
                 foreach (SumSeparateNode01 mainNod in NumberList)
                 {
                     mainNod.TotalCounter = mainNod.FirstLocateCounter + mainNod.SecoundLocateCounter;
                 }
             }

             #endregion



         }
         public string InfoCounterLocate()
         {
             CollectCountersLocate();
             StringBuilder Report = new StringBuilder();

             Report.Append(
                "\n\n********* InfoCounterLocate **********\n" +
               
                "\n\n");

             Report.Append("\n\n\n As NoSort *******\n\n\n");

             foreach (SumSeparateNode01 mainNod in NumberList)
             {
                 Report.Append(
                     "\nIdNode = " + mainNod.IdNode.ToString("  0  ") +
                     "\nFCount = " + mainNod.FirstLocateCounter.ToString("     0     ") +
                     "\nSCount = " + mainNod.SecoundLocateCounter.ToString("     0     ") +
                     "\nTcount = " + mainNod.TotalCounter.ToString("     0     ") +

                     "\n");
             }

             Report.Append("\n\n\n Fiinish *******\n\n\n");


             return Report.ToString();
         }

         private void CollectCountersSum()
         {
             int Counter = 0;
             for (int i = 0; i != ModLength; i++)
             {
                 for (int n = 0; n != ModLength; n++)
                 {
                     
                     Counter++;
                 }
             }

         }
         public void InfoCounterSum()
         {
             StringBuilder Report = new StringBuilder();

             Report.Append(
                "\n\n********* InfoCounterSum **********\n" +

                "\n\n");

         }


         #region Sort
         private void quicksort(ref int[] list, int begin, int end)
         {
             int pivot = list[(begin + (end - begin) / 2)];
             int left = begin;
             int right = end;
             while (left <= right)
             {
                 while (list[left] < pivot)
                 {
                     left++;
                 }
                 while (list[right] > pivot)
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
         private void swap(ref int[] list, int x, int y)
         {
             int temp = list[x];
             list[x] = list[y];
             list[y] = temp;
         }

         #endregion

         # endregion

     }

   

     class DeSumSeparateNode01
     {
         public DeSumSeparateNode01 nextzero;
         public DeSumSeparateNode01 nextone;

         public bool Stoping;
         public bool Convert;

         public int FirstNumbers = 0;
         public int SecondNumbers = 0;
         public int SumNumbers = 0;


         public ReaderWriterOneNum02B WriteNumber;
         public ReaderWriteFileBits02B PossReader;
        
         public DeSumSeparateNode01()
         {
             Stoping = false;
             Convert = false;
         }
         public DeSumSeparateNode01(bool TempConvert)
         {
             Stoping = false;
             Convert = TempConvert;
         }



         public void GetPoss(ref DeSumSeparateNode01 po)
         {
             while (!po.Stoping)
             {
                 if (PossReader.GetBit())
                 {
                     po = po.nextone;
                 }
                 else
                 {
                     po = po.nextzero;
                 }
             }
         }
         public void Write()
         {
             WriteNumber.WriteNumber(FirstNumbers);
             WriteNumber.WriteNumber(SecondNumbers);
         }


     }
     class DeSumSeparateInfoTree01
     {
         private int ModLength = 256;

         private ReaderWriteFileBits02B KeyReader;
         private ReaderWriteFileBits02B PossReader;
         private ReaderWriterOneNum02B NumWriter;
         
         public DeSumSeparateNode01 root=new DeSumSeparateNode01();
         private DeSumSeparateNode01 po;

         public DeSumSeparateInfoTree01()
         {
             Create();
         }
         public DeSumSeparateInfoTree01(int ModLengthNumber)
         {
             ModLength = ModLengthNumber;
             Create();
         }

         private void Create()
         {
             SumSeparateInfoTree01 Tree= new SumSeparateInfoTree01(ModLength);
             

             foreach (SumSeparateNode01 mainNod in Tree.NumberList)
             {
                 foreach (SumSeparateNode01 nod in mainNod.NextList)
                 {
                     po = root;
                     foreach (bool b in nod.BitsListKey)
                     {
                         if (b)
                         {
                             if (po.nextone == null)
                             {
                                 po.nextone = new DeSumSeparateNode01();
                                 po = po.nextone;
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
                                 po.nextzero = new DeSumSeparateNode01();
                                 po = po.nextzero;
                             }
                             else
                             {
                                 po = po.nextzero;
                             }
                         }
                     }
                     po.Convert = true;


                     foreach (bool b in nod.BitsListPossible)
                     {
                         if (b)
                         {
                             if (po.nextone == null)
                             {
                                 po.nextone = new DeSumSeparateNode01(true);
                                 po = po.nextone;
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
                                 po.nextzero = new DeSumSeparateNode01(true);
                                 po = po.nextzero;
                             }
                             else
                             {
                                 po = po.nextzero;
                             }
                         }
                     }
                     po.Stoping = true;
                     po.FirstNumbers = nod.FirstNumbers;
                     po.SecondNumbers = nod.SecondNumbers;

                 }
             }

             po = root;
         }

         private void GetKey()
         {
             while (!po.Convert)
             {
                 if (KeyReader.GetBit())
                 {
                     po = po.nextone;
                 }
                 else
                 {
                     po = po.nextzero;
                 }
             }
             po.GetPoss(ref po);
         }

         public void WriteNumber()
         {
             if (KeyReader.isAbleRead)
             {
                 GetKey();
                 po.Write();
                 po = root;
             }
             
         }
         public void WriteAllNumber()
         {
             while(KeyReader.isAbleRead)
             {
                 GetKey();
                 po.Write();
                 po = root;
             }

         }




         public void RefrishWriterW01( ref ReaderWriterOneNum02B NumberWriter ,ref ReaderWriteFileBits02B WriterBits)
         {
             KeyReader = WriterBits;
             NumWriter = NumberWriter;
             PossReader = WriterBits;
             RefrishW01(root);

         }
         public void RefrishWriterW01(ref ReaderWriterOneNum02B NumberWriter, ref ReaderWriteFileBits02B WriterBitsKey, ref ReaderWriteFileBits02B WriterBitsPossible)
         {
             KeyReader = WriterBitsKey;
             NumWriter = NumberWriter;
             PossReader = WriterBitsPossible;
             RefrishW01(root);


         }

         private void RefrishW01(DeSumSeparateNode01 cr)
         {
             if (cr.Convert)
             {
                 
                 if (cr.Stoping)
                 {
                     cr.WriteNumber = NumWriter;
                 }
                 else
                 {
                     cr.PossReader = PossReader;
                     RefrishW01(cr.nextzero);
                     RefrishW01(cr.nextone);
                 }
             }
             else
             {
                 RefrishW01(cr.nextzero);
                 RefrishW01(cr.nextone);
             }
         }

     }




     class SumSeparateFile01
     {
         public StringBuilder Report;
         private int ModLength = 256;
         private int LengthReader = 1024;

         private string Extension = "SSF01ML";
         private string DeExtension = "DeSSF01ML";

         public SumSeparateFile01()
         {

         }
         public SumSeparateFile01(int ModLengthNumber)
         {
             ModLength = ModLengthNumber;

         }

         public void InfoFile()
         {

             SumSeparateInfoTree01 Tree = new SumSeparateInfoTree01(ModLength);
             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);

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
         }
         public void SeprateFileWay01()
         {

             SumSeparateInfoTree01 Tree = new SumSeparateInfoTree01(ModLength);
             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
             ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");

             Tree.RefrishWriterW01(ref WriterBits);


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
                         Tree.NumberList[First].NextList[n].Write();
                         isFirst = true;
                     }
                 }
             }


             ReaderNum.End();
             WriterBits.CloseFile();
         }
         public void SeprateFileWay02()
         {

             SumSeparateInfoTree01 Tree = new SumSeparateInfoTree01(ModLength);
             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
             ReaderWriteFileBits02B WriterBitsKey = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01Key");
             ReaderWriteFileBits02B WriterBitsPoss = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01Poss");


             Tree.RefrishWriterW01(ref WriterBitsKey, ref WriterBitsPoss);


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
                         Tree.NumberList[First].NextList[n].Write();
                         isFirst = true;
                     }
                 }
             }


             ReaderNum.End();
             WriterBitsKey.CloseFile();
             WriterBitsPoss.CloseFile();
         }



         public string SeprateFileSegmentInfo()
         {
             
             ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
             if (ReaderNum.GetIsCancel)
                 return "isCancel";

             SumSeparateInfoTree01 SeprateF = new SumSeparateInfoTree01(ModLength);

             List<SumSeparateSegmentInfoNod01> ListSeg = new List<SumSeparateSegmentInfoNod01>();

             int DataLengthStop = ReaderNum.GetStopNumLength;
             while (ReaderNum.isAbleRead)
             {
                 List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);
                 ListSeg.Add(SeprateF.CompSegmentInfo(ref ListData));
             }

             ReaderNum.End();

            return SegmListInfo(ref ListSeg);

         }
         private string SegmListInfo(ref List<SumSeparateSegmentInfoNod01> ListSeg)
         {
             int SumSegments = ListSeg.Count;
             int SumLengthList = 0;
             int SumOrig = 0;
             int SumAfter = 0;
             int SizComped = 0;
             int SumNodsIsComp = 0;
             int SumNodsNoComp = 0;
             int SumNodIsEqual = 0;

   


             int SegIsComp = 0;
             int SegNoComp = 0;
             int SegEquals = 0;

             int SumSizeIsSave = 0;
             int SumSizIsExtra = 0;
             int SumEqualExtra = 0;

             int SumExtraKey = ListSeg.Count;

             int LastSizeSave = 0;

             bool iSCompAble = false;

             foreach (SumSeparateSegmentInfoNod01 nod in ListSeg)
             {
                 SumLengthList = SumLengthList + nod.LengthList;

                 SumOrig = SumOrig + nod.SumOrig;
                 SumAfter = SumAfter + nod.SumAfter;
                 SizComped = SizComped + nod.SizComped;

                 SumNodsIsComp = SumNodsIsComp + nod.SumNodsIsComp;
                 SumNodsNoComp = SumNodsNoComp + nod.SumNodsNoComp;
                 SumNodIsEqual = SumNodIsEqual + nod.SumNodIsEqual;


                 if (nod.SizComped  > 0)
                 {
                     SegIsComp++;
                     SumSizeIsSave = SumSizeIsSave + nod.SizComped;
                 }
                 else
                 {
                     if (nod.SizComped  == 0)
                     {
                         SegEquals++;
                         SumEqualExtra++;
                     }
                     else
                     {
                         SegNoComp++;
                         SumSizIsExtra = SumSizIsExtra + (nod.SizComped * -1);
                     }
                 }
                

             }
              LastSizeSave = SumSizeIsSave - SumExtraKey;

              if (LastSizeSave <= 0)
                  iSCompAble = false;
              else
                  iSCompAble = true;


             StringBuilder Report = new StringBuilder();
             Report.Append(
            "**************************************" +
            "\nSumSegments = "+SumSegments.ToString()+
            "\nSumLengthList = " + SumLengthList.ToString() +
            "\n" +
            "\nSumOrig = " + SumOrig.ToString() + " = " +
                               (SumOrig / 8).ToString() + "B = " +
                               ((SumOrig / 8) / 1024) + " KB = " +
                               (((SumOrig / 8) / 1024) / 1024) + " MB " +
            "\nSumAfter = " + SumAfter.ToString() + " = " +
                               (SumAfter / 8).ToString() + "B = " +
                               ((SumAfter / 8) / 1024) + " KB = " +
                               (((SumAfter / 8) / 1024) / 1024) + " MB " +

            "\n" +
            "\nSumNodsIsComp = " + SumNodsIsComp.ToString() +
            "\nSumNodsNoComp = " + SumNodsNoComp.ToString() +
            "\nSumNodIsEqual = " + SumNodIsEqual.ToString() +
            "\n" +
            "\niSCompAble = " + iSCompAble.ToString() +
            "\nSizComped = " + SizComped.ToString() + " = " +
                               (SizComped / 8).ToString() + "B = " +
                               ((SizComped / 8) / 1024) + " KB = " +
                               (((SizComped / 8) / 1024) / 1024) + " MB " +


            "\n*****\n"+
            "\nSegIsComp = " + SegIsComp.ToString() +
            "\nSegNoComp = " + SegNoComp.ToString() +
            "\nSegEquals = " + SegEquals.ToString() +
            "\n"+
            "\nSumSizeIsSave = " + SumSizeIsSave.ToString() + " = " +
                               (SumSizeIsSave / 8).ToString() + "B = " +
                               ((SumSizeIsSave / 8) / 1024) + " KB = " +
                               (((SumSizeIsSave / 8) / 1024) / 1024) + " MB " +
            "\nSumSizIsExtra = " + SumSizIsExtra.ToString() + " = " +
                               (SumSizIsExtra / 8).ToString() + "B = " +
                               ((SumSizIsExtra / 8) / 1024) + " KB = " +
                               (((SumSizIsExtra / 8) / 1024) / 1024) + " MB " +
            "\nSumEqualExtra = " + SumEqualExtra.ToString() + " = " +
                               (SumEqualExtra / 8).ToString() + "B = " +
                               ((SumEqualExtra / 8) / 1024) + " KB = " +
                               (((SumEqualExtra / 8) / 1024) / 1024) + " MB " +
            "\nSumExtraKey = " + SumExtraKey.ToString() + " = " +
                               (SumExtraKey / 8).ToString() + "B = " +
                               ((SumExtraKey / 8) / 1024) + " KB = " +
                               (((SumExtraKey / 8) / 1024) / 1024) + " MB " +
            "\n\n"+
            "\niSCompAble = " + iSCompAble.ToString() +
            "\nLastSizeSave = " + LastSizeSave.ToString() + " = " +
                               (LastSizeSave / 8).ToString() + "B = " +
                               ((LastSizeSave / 8) / 1024) + " KB = " +
                               (((LastSizeSave / 8) / 1024) / 1024) + " MB " +
            




                               "\n");

             MessageBox.Show(Report.ToString(), "SumBitsComp", MessageBoxButtons.OKCancel);
                

             return Report.ToString();
         }






         public void DeSeprateFileWay01()
         {

             DeSumSeparateInfoTree01 Tree = new DeSumSeparateInfoTree01(ModLength);
             ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
             ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength ,DeExtension + ModLength.ToString() + "W01");
             

             Tree.RefrishWriterW01(ref WriterNum, ref ReaderBits);

             Tree.WriteAllNumber();
             
             WriterNum.End();
             ReaderBits.CloseFile();
         }

     }



     public class TrialSumSeparateFile01
     {
         public StringBuilder RePort;
         private ReadWriteFile02 readerFile;
         private string Extension = "SSF01ML";
         private string DeExtension = "DeSSF01ML";
         private int ModLength = 256;

         public TrialSumSeparateFile01(int ModNumberLength)
         {
             ModLength = ModNumberLength;
             RePort = new StringBuilder();


         }
         //public void StartUniq()
         //{
         //    MakeListUniq04 MakeUniq = new MakeListUniq04(ModLength);

         //    readerFile = new ReadWriteFile02((ModLength.ToString() + Extension));
         //    if (readerFile.IsCancel)
         //        return;

         //    readerFile.OpenAll();

         //    BitsToInt IntReader = new BitsToInt(ModLength);
         //    IntBitsOperations BitsReader = new IntBitsOperations(ModLength + 1);

         //    while (readerFile.ReadAble == true)
         //    {
         //        readerFile.ReadData();

         //        List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
         //        MakeUniq.CreatListNum(ModLength);

         //        List<int> UniqInt = MakeUniq.MakeListUniq(ref intData);

         //        byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

         //        readerFile.SaveDataByte(ref DataByte);

         //    }

         //    readerFile.CloseAll();



         //}

         //public void StartDeUniq()
         //{
         //    MakeListUniq02 MakeUniq = new MakeListUniq02(ModLength);

         //    readerFile = new ReadWriteFile02((ModLength.ToString() + DeExtension));
         //    if (readerFile.IsCancel)
         //        return;

         //    readerFile.ReaderF.StopNumLength = (Convert.ToInt32(Math.Pow(2, ModLength)) * (ModLength + 1)) / 8;

         //    readerFile.OpenAll();

         //    BitsToInt IntReader = new BitsToInt(ModLength + 1);
         //    IntBitsOperations BitsReader = new IntBitsOperations(ModLength);

         //    while (readerFile.ReadAble == true)
         //    {
         //        readerFile.ReadData();

         //        List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
         //        MakeUniq.CreatListNum(ModLength);

         //        List<int> UniqInt = MakeUniq.MakeListDeUniq(ref intData);

         //        byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

         //        readerFile.SaveDataByte(ref DataByte);

         //    }

         //    readerFile.CloseAll();



         //}





     }


   
}
