using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.QuickSort.MakeTreeQuickSort
{

    class MakeTreeQuickSort01
    {
        #region  Properties

        private int ModNum = 0;
        private int ModLength = 256;
        // private int ModSegmentLength = 0;

        private ReaderWriteFileBits02B WriterBits;


        #endregion

        #region OverLoad

        public MakeTreeQuickSort01()
        {
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeQuickSort01(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeQuickSort01(ref ReaderWriteFileBits02B ReaderBits)
        {
            WriterBits = ReaderBits;
            Creat();
        }
        public MakeTreeQuickSort01(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            WriterBits = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
        }

        #endregion

        #region For info

        private int SegmentNum = -1;
        private int ModSegmentLength = 2048;

        private void GetSegmentInfo()
        {

        }

        #endregion

        #region Sort

        List<int> SaveList = new List<int>();
        public void SortList(ref List<int> ListData)
        {
            QuickSort(ref ListData, 1, ListData.Count);
        }
        public void SortList(ref List<int> ListData, ref ReaderWriteFileBits02B WriterBit)
        {
            WriterBits = WriterBit;

            QuickSort(ref ListData, 1, ListData.Count);
        }

        List<int> LeftList = new List<int>();
        List<int> RigthtList = new List<int>();
        private void QuickSort(ref List<int> unsortedArray, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                #region Sort Array

                int middleIndex = (leftIndex + rightIndex) / 2;
                
                {
                    
                    int R = 0 , L = 0;
                    int endR =  (rightIndex - leftIndex  + 1) / 2;
                    int endL = endR;

                    int i = leftIndex - 1;
                    for (; i != rightIndex; i++)
                    {
                        if (unsortedArray[i] < middleIndex)
                        {
                            LeftList.Add(unsortedArray[i]);
                            WriterBits.WriteBit(false);
                            L++;
                        }
                        else
                        {
                            RigthtList.Add(unsortedArray[i]);
                            WriterBits.WriteBit(true);
                            R++;
                        }

                        if (R == endR || L == endL)
                        {
                            i++;
                            break;
                        }
                    }



                    if (R != endR)
                    {
                        for (; R != endR; i++)
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                    }
                    else
                    {
                        if (L != endL)
                        {
                            for (; L != endL; i++)
                            {
                                LeftList.Add(unsortedArray[i]);
                                L++;
                            }
                        }
                    }


                    //Save
                    {
                        i = leftIndex - 1;
                        foreach (int b in LeftList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        foreach (int b in RigthtList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        LeftList.Clear();
                        RigthtList.Clear();
                    }

                }

                #endregion
                //Sort left (will call Merge to produce a fully sorted left array)
                QuickSort(ref unsortedArray, leftIndex, middleIndex);
                //Sort right (will call Merge to produce a fully sorted right array)
                QuickSort(ref unsortedArray, middleIndex + 1, rightIndex);
                //Merge the sorted left & right to finish off.
            }
        }
        private void QuickSort(ref List<int> unsortedArray, int leftIndex, int rightIndex , int Level)
        {
            if (leftIndex < rightIndex)
            {
                #region Sort Array

                int middleIndex = (leftIndex + rightIndex) / 2;

                {

                    int R = 0, L = 0;
                    int endR = (rightIndex - leftIndex + 1) / 2;
                    int endL = endR;

                    int i = leftIndex - 1;
                    for (; i != rightIndex; i++)
                    {
                        if (unsortedArray[i] < middleIndex)
                        {
                            LeftList.Add(unsortedArray[i]);
                            WriterBits.WriteBit(false);
                            L++;
                        }
                        else
                        {
                            RigthtList.Add(unsortedArray[i]);
                            WriterBits.WriteBit(true);
                            R++;
                        }

                        if (R == endR || L == endL)
                            break;
                    }



                    if (R != endR)
                    {
                        for (; R != endR; i++)
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                    }
                    else
                    {
                        if (L != endL)
                        {
                            for (; L != endL; i++)
                            {
                                LeftList.Add(unsortedArray[i]);
                                L++;
                            }
                        }
                    }


                    //Save
                    {
                        i = leftIndex - 1;
                        foreach (int b in LeftList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        foreach (int b in RigthtList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        LeftList.Clear();
                        RigthtList.Clear();
                    }

                }

                #endregion
                //Sort left (will call Merge to produce a fully sorted left array)
                QuickSort(ref unsortedArray, leftIndex, middleIndex, Level + 1);
                //Sort right (will call Merge to produce a fully sorted right array)
                QuickSort(ref unsortedArray, middleIndex + 1, rightIndex, Level + 1);
                //Merge the sorted left & right to finish off.
            }
        }
    

        #endregion


    }



    class MakeTreeDeQuickSort01
    {
        #region  Properties

        private int ModNum = 0;
        private int ModLength = 256;
        // private int ModSegmentLength = 0;

        private ReaderWriteFileBits02B ReaderBits;


        #endregion

        #region OverLoad

        public MakeTreeDeQuickSort01()
        {
            ReaderBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeQuickSort01(int ModNumLength)
        {
            ModLength = ModNumLength;
            ReaderBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeQuickSort01(ref ReaderWriteFileBits02B ReaderBit)
        {
            ReaderBits = ReaderBit;
            Creat();
        }
        public MakeTreeDeQuickSort01(int ModNumLength, ref ReaderWriteFileBits02B ReaderBit)
        {
            ModLength = ModNumLength;

            ReaderBits = ReaderBit;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
        }

        #endregion

        #region For info

        private int SegmentNum = -1;
        private int ModSegmentLength = 2048;

        private void GetSegmentInfo()
        {

        }

        #endregion

        #region DeSort

        private List<int> NumberList = new List<int>();
        private List<int> SaveNumberList = new List<int>();
        private int Ci = 0;
        public List<int> DeSortOneList()
        {
            NumberList.Clear();
            for (Ci = 0; Ci != ModLength; Ci++)
            {
                NumberList.Add(Ci);
            }
            QuickSort(ref NumberList, 1, NumberList.Count);

            //Save
            {
                SaveNumberList.Clear();
                for (Ci = 0; Ci != ModLength; Ci++)
                {
                    SaveNumberList.Add(NumberList.IndexOf(Ci));
                }

            }

            return SaveNumberList;

        }
        public List<int> DeSortOneList( ref ReaderWriteFileBits02B ReaderBit)
        {
            NumberList.Clear();
            ReaderBits = ReaderBit;
            for (Ci = 0; Ci != ModLength; Ci++)
            {
                NumberList.Add(Ci);
            }
            QuickSort(ref NumberList, 1, NumberList.Count);

            //Save
            {
                SaveNumberList.Clear();
                for (Ci = 0; Ci != ModLength; Ci++)
                {
                    SaveNumberList.Add(NumberList.IndexOf(Ci));
                }

            }

            return SaveNumberList;

        }

        List<int> LeftList = new List<int>();
        List<int> RigthtList = new List<int>();
        private void QuickSort(ref List<int> unsortedArray, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                #region Sort Array

                int middleIndex = (leftIndex + rightIndex) / 2;

                {

                    int R = 0, L = 0;
                    int endR = (rightIndex - leftIndex + 1) / 2;
                    int endL = endR;

                    int i = leftIndex - 1;
                    for (; i != rightIndex; i++)
                    {
                        if (ReaderBits.GetBit())
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                        else
                        {
                            LeftList.Add(unsortedArray[i]);
                            L++;
                        }

                        if (R == endR || L == endL)
                        {
                            i++;
                            break;
                        }
                    }



                    if (R != endR)
                    {
                        for (; R != endR; i++)
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                    }
                    else
                    {
                        if (L != endL)
                        {
                            for (; L != endL; i++)
                            {
                                LeftList.Add(unsortedArray[i]);
                                L++;
                            }
                        }
                    }


                    //Save
                    {
                        i = leftIndex - 1;
                        foreach (int b in LeftList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        foreach (int b in RigthtList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        LeftList.Clear();
                        RigthtList.Clear();
                    }

                }

                #endregion
                //Sort left (will call Merge to produce a fully sorted left array)
                QuickSort(ref unsortedArray, leftIndex, middleIndex);
                //Sort right (will call Merge to produce a fully sorted right array)
                QuickSort(ref unsortedArray, middleIndex + 1, rightIndex);
                //Merge the sorted left & right to finish off.
            }
        }
        private void QuickSort(ref List<int> unsortedArray, int leftIndex, int rightIndex, int Level)
        {
            if (leftIndex < rightIndex)
            {
                #region Sort Array

                int middleIndex = (leftIndex + rightIndex) / 2;

                {

                    int R = 0, L = 0;
                    int endR = (rightIndex - leftIndex + 1) / 2;
                    int endL = endR;

                    int i = leftIndex - 1;
                    for (; i != rightIndex; i++)
                    {
                        if (ReaderBits.GetBit())
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                        else
                        {
                            LeftList.Add(unsortedArray[i]);
                            L++;
                        }

                        if (R == endR || L == endL)
                        {
                            i++;
                            break;
                        }
                    }



                    if (R != endR)
                    {
                        for (; R != endR; i++)
                        {
                            RigthtList.Add(unsortedArray[i]);
                            R++;
                        }
                    }
                    else
                    {
                        if (L != endL)
                        {
                            for (; L != endL; i++)
                            {
                                LeftList.Add(unsortedArray[i]);
                                L++;
                            }
                        }
                    }


                    //Save
                    {
                        i = leftIndex - 1;
                        foreach (int b in LeftList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        foreach (int b in RigthtList)
                        {
                            unsortedArray[i] = b; i++;
                        }

                        LeftList.Clear();
                        RigthtList.Clear();
                    }

                }

                #endregion
                //Sort left (will call Merge to produce a fully sorted left array)
                QuickSort(ref unsortedArray, leftIndex, middleIndex, Level + 1);
                //Sort right (will call Merge to produce a fully sorted right array)
                QuickSort(ref unsortedArray, middleIndex + 1, rightIndex, Level + 1);
                //Merge the sorted left & right to finish off.
            }
        }


        #endregion


    }













    class MakeTreeQuickSort01Oper
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTQS01ML";
        private string DeExtension = "DeMTQS01ML";


        public MakeTreeQuickSort01Oper()
        {

        }
        public MakeTreeQuickSort01Oper(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }


        public void StartMakeQuickSort_FileUniqW01()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";


            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            MakeTreeQuickSort01 TreeSort1 = new MakeTreeQuickSort01(ModLength, ref WriterBits);

            List<int> UniqList = new List<int>();
            while (ReaderNum.isAbleRead)
            {
                UniqList = ReaderNum.GetManyNum(ModLength);
                TreeSort1.SortList(ref UniqList);
                WriterBits.SumOfWriteBits = 0;
            }


            ReaderNum.End();
            WriterBits.CloseFile();

        }
        public void StartMakeDeQuickSort_FileUniqW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MakeTreeDeQuickSort01 TreeDeSort1 = new MakeTreeDeQuickSort01(ModLength, ref ReaderBits);

            List<int> UniqList = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                UniqList = TreeDeSort1.DeSortOneList();

                WriterNum.WriterManyNumber(ref UniqList);

                ReaderBits.SumOfReadBits = 0;

                UniqList.Clear();
            }


            WriterNum.End();
            ReaderBits.CloseFile();

        }



    }

}
