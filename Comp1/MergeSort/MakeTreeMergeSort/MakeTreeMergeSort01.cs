using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MergeSort.MakeTreeMergeSort
{


    class MakeTreeMergeSort01
    {
        #region  Properties

        private int ModNum = 0;
        private int ModLength = 256;
        // private int ModSegmentLength = 0;

        private ReaderWriteFileBits02B WriterBits;


        #endregion

        #region OverLoad

        public MakeTreeMergeSort01()
        {
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeMergeSort01(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterBits = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeMergeSort01(ref ReaderWriteFileBits02B ReaderBits)
        {
            WriterBits = ReaderBits;
            Creat();
        }
        public MakeTreeMergeSort01(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
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

        public void SortList(ref List<int> ListData)
        {
            MergeSort(ref ListData, 1, ListData.Count);
        }
        public void SortList(ref List<int> ListData, ref ReaderWriteFileBits02B WriterBit)
        {
            WriterBits = WriterBit;

            MergeSort(ref ListData, 1, ListData.Count);
        }

        private void MergeSort(ref List<int> unsortedArray, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int middleIndex = (leftIndex + rightIndex) / 2;
                //Sort left (will call Merge to produce a fully sorted left array)
                MergeSort(ref unsortedArray, leftIndex, middleIndex);
                //Sort right (will call Merge to produce a fully sorted right array)
                MergeSort(ref unsortedArray, middleIndex + 1, rightIndex);
                //Merge the sorted left & right to finish off.
                Merge(ref unsortedArray, leftIndex, middleIndex, rightIndex);
            }
        }
        private void Merge(ref List<int> unsortedArray, int leftIndex, int middleIndex, int rightIndex)
        {
            int lengthLeft = middleIndex - leftIndex + 1;
            int lengthRight = rightIndex - middleIndex;

            int x = leftIndex - 1, y = middleIndex;
            int endX = x + lengthLeft;
            int endY = y + lengthRight;

            int[] BytAdd = new int[lengthLeft + lengthRight];
            int BR = 0;

            while (true)
            {
                if (x < endX && y < endY)
                {
                    if (unsortedArray[x] > unsortedArray[y])
                    {
                        BytAdd[BR] = unsortedArray[y]; BR++;
                        WriterBits.WriteBit(true);

                        y++;
                    }
                    else
                    {
                        BytAdd[BR] = unsortedArray[x]; BR++;
                        WriterBits.WriteBit(false);
                        x++;
                    }
                }
                else
                {
                    while (x < endX)
                    {
                        BytAdd[BR] = unsortedArray[x]; BR++;
                        x++;
                    }
                    while (y < endY)
                    {
                        BytAdd[BR] = unsortedArray[y]; BR++;
                        y++;
                    }

                    break;
                }

            }

            //Save
            {
                BR = leftIndex - 1;
                foreach (int b in BytAdd)
                {
                    unsortedArray[BR] = b; BR++;
                }
               
            }

        }


        #endregion


    }

    class MakeTreeDeMergeSort01
    {
        #region  Properties

        private int ModNum = 0;
        private int ModLength = 256;
        // private int ModSegmentLength = 0;

        private ReaderWriteFileBits02B ReaderBit;


        #endregion

        #region OverLoad

        public MakeTreeDeMergeSort01()
        {
            ReaderBit = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeMergeSort01(int ModNumLength)
        {
            ModLength = ModNumLength;
            ReaderBit = new ReaderWriteFileBits02B(false);
            Creat();
        }
        public MakeTreeDeMergeSort01(ref ReaderWriteFileBits02B ReaderBits)
        {
            ReaderBit = ReaderBits;
            Creat();
        }
        public MakeTreeDeMergeSort01(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            ReaderBit = ReaderBits;
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
            MergeSort(ref NumberList, 1, NumberList.Count);

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
        public List<int> DeSortOneList(ref ReaderWriteFileBits02B ReaderBits)
        {
            NumberList.Clear();
            ReaderBit = ReaderBits;
            for (Ci = 0; Ci != ModLength; Ci++)
            {
                NumberList.Add(Ci);
            }
            MergeSort(ref NumberList, 1, NumberList.Count);

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

        private bool GetBit()
        {
            return ReaderBit.GetBit();
        }
        private void MergeSort(ref List<int> unsortedArray, int leftIndex, int rightIndex )
        {
            if (leftIndex < rightIndex)
            {
               
                int middleIndex = (leftIndex + rightIndex) / 2;
                //Sort left (will call Merge to produce a fully sorted left array)
                MergeSort(ref unsortedArray, leftIndex, middleIndex);
                //Sort right (will call Merge to produce a fully sorted right array)
                MergeSort(ref unsortedArray, middleIndex + 1, rightIndex);
                //Merge the sorted left & right to finish off.
                Merge(ref unsortedArray, leftIndex, middleIndex, rightIndex);
            }
        }
        private void Merge(ref List<int> unsortedArray, int leftIndex, int middleIndex, int rightIndex)
        {
           
            int lengthLeft = middleIndex - leftIndex + 1;
            int lengthRight = rightIndex - middleIndex;

            int x = leftIndex - 1, y = middleIndex;
            int endX = x + lengthLeft;
            int endY = y + lengthRight;

            int[] BytAdd = new int[lengthLeft + lengthRight];
            int BR = 0;

            while (true)
            {
                if (x < endX && y < endY)
                {
                    if (GetBit())
                    {
                        BytAdd[BR] = unsortedArray[y]; BR++;

                        y++;
                    }
                    else
                    {
                        BytAdd[BR] = unsortedArray[x]; BR++;
                        x++;
                    }
                }
                else
                {
                    while (x < endX)
                    {
                        BytAdd[BR] = unsortedArray[x]; BR++;
                        x++;
                    }
                    while (y < endY)
                    {
                        BytAdd[BR] = unsortedArray[y]; BR++;
                        y++;
                    }

                    break;
                }

            }

            //Save
            {
                BR = leftIndex - 1;
                foreach (int b in BytAdd)
                {
                    unsortedArray[BR] = b; BR++;
                }

            }

        }


        #endregion


    }


  


    class MakeTreeMergeSort01Oper
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTMS01ML";
        private string DeExtension = "DeMTMS01ML";


        public MakeTreeMergeSort01Oper()
        {

        }
        public MakeTreeMergeSort01Oper(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }


        public void StartMakeTreeMergeSort_FileUniqW01()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";


            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, Extension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBits);

            List<int> UniqList = new List<int>();
            while (ReaderNum.isAbleRead)
            {
                UniqList = ReaderNum.GetManyNum(ModLength);
                TreeSort1.SortList(ref UniqList);
            }


            ReaderNum.End();
            WriterBits.CloseFile();

        }
        public void StartMakeTreeDeMergeSort_FileUniqW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";

            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MakeTreeDeMergeSort01 TreeDeSort1 = new MakeTreeDeMergeSort01(ModLength, ref ReaderBits);

            List<int> UniqList = new List<int>();
            while (ReaderBits.isAbleRead)
            {
                UniqList = TreeDeSort1.DeSortOneList(ref ReaderBits);

                WriterNum.WriterManyNumber(ref UniqList);

                ReaderBits.SumOfReadBits = 0;

                UniqList.Clear();
            }


            WriterNum.End();
            ReaderBits.CloseFile();

        }



    }

}
