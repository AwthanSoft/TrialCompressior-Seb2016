using Comp1.MergeSort.MakeTreeMergeSort;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderFile.ReaderWriteFile02.ReaderWriterOneNumMod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeTreeUniq
{

    class MakeTreeUniq05Tree
    {

        #region  Properties


        private int ModNum = 0;
        private int ModLength = 256;

        private ReaderWriteFileBits02B ReaderBit;
        private ReaderWriterOneNumMod02 ReaderNumByMod;
        private int LengthModListReader = 20;
        private List<int> ListNumbers = new List<int>();


        #endregion

        #region OverLoad

        public MakeTreeUniq05Tree()
        {
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq05Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            ReaderBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeUniq05Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            ReaderBit = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {
         
            //   ListNodes = new MakeTreeUniq04Node[ModLength];

            
            ReaderNumByMod = new ReaderWriterOneNumMod02(true, LengthModListReader, ref ReaderBit);



            //Temp
           

            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
            StopModLengthByHalf = ModLength / 2;

            initialList();

        }

        #endregion

        #region Initial

        private void initialList()
        {
            ListNumbers.Clear();

            for (int i = 0; i != ModLength; i++)
            {
                ListNumbers.Add(i);
            }
            
        }

        #endregion

        #region For info

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;
        public MakeTreeUniqInfoNode00 SegmentInfoNod;

        private void GetSegmentInfo()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBit.SumOfReadBits);

        }
        private void GetSegmentInfo_ByHalf()
        {
            SegmentInfoNod = new MakeTreeUniqInfoNode00(ModSegmentLength / 2);
            SegmentInfoNod.EditSegmentNum(SegmentNum);
            SegmentInfoNod.EditHowReadBits(ReaderBit.SumOfReadBits);

        }


        #endregion

        #region ReaderTree

       private int TempLocatNum = 0;
       private int TempOutNum = 0;
       private bool isInitial = true;


       private int GetOneNum()
        {
            if (ListNumbers.Count == 1)
            {
                isInitial = true;
                TempOutNum = ListNumbers[0];
                initialList();
                return TempOutNum;
            }

           
            TempLocatNum = ReaderNumByMod.GetOneNumber(ListNumbers.Count);
            TempOutNum = ListNumbers[TempLocatNum];

            ListNumbers.RemoveAt(TempLocatNum);
            return TempOutNum;

        }      
       public List<int> GetOneReadList()
        {
            List<int> listNum = new List<int>();
            isInitial = false;
            while (ReaderNumByMod.isAbleRead && isInitial == false)
            {
                listNum.Add(GetOneNum());
            }

            GetSegmentInfo();
            SegmentNum++;
            return listNum;

        }

       private int StopModLengthByHalf = 128;
       private int GetOneNum_ByHalf()
       {
           TempLocatNum = ReaderNumByMod.GetOneNumber(ListNumbers.Count);
           TempOutNum = ListNumbers[TempLocatNum];

           ListNumbers.RemoveAt(TempLocatNum);


           if (ListNumbers.Count == StopModLengthByHalf)
           {
               isInitial = true;
               initialList();
//               return TempOutNum;
           }



           return TempOutNum;

       }
       public List<int> GetOneReadList_ByHalf()
       {
           List<int> listNum = new List<int>();
           isInitial = false;
           while (ReaderNumByMod.isAbleRead && isInitial == false)
           {
               listNum.Add(GetOneNum_ByHalf());
           }

           GetSegmentInfo_ByHalf();
           SegmentNum++;
           return listNum;

       }




        #endregion


    }
    class MakeTreeDeUniq05Tree
    {

        #region  Properties


        private int ModNum = 0;
        private int ModLength = 256;
        //private ReaderWriterOneNum02B ReaderNum;
        private ReaderWriteFileBits02B WriterBit;
        private ReaderWriterOneNumMod02 WriterNumByMod;
        private int LengthModListReader = 20;
        private List<int> ListNumbers = new List<int>();


        #endregion

        #region OverLoad

        public MakeTreeDeUniq05Tree()
        {
            WriterBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeDeUniq05Tree(int ModNumLength)
        {
            ModLength = ModNumLength;
            WriterBit = new ReaderWriteFileBits02B(true);
            Creat();
        }
        public MakeTreeDeUniq05Tree(int ModNumLength, ref ReaderWriteFileBits02B ReaderBits)
        {
            ModLength = ModNumLength;

            WriterBit = ReaderBits;
            Creat();
        }


        #endregion

        #region Create

        private void Creat()
        {

            //   ListNodes = new MakeTreeUniq04Node[ModLength];


            WriterNumByMod = new ReaderWriterOneNumMod02(false, LengthModListReader, ref WriterBit);



            //Temp


            int Sum = 0;

            while (Sum < ModLength)
            {
                ModNum++;
                Sum = Convert.ToInt32(Math.Pow(2, ModNum));
            }

            ModSegmentLength = ModLength * ModNum;
            StopModLengthByHalf = ModLength / 2;

            initialList();

        }

        #endregion

        #region Initial

        private void initialList()
        {
            ListNumbers.Clear();

            for (int i = 0; i != ModLength; i++)
            {
                ListNumbers.Add(i);
            }

        }

        #endregion

        #region For info

        private int SegmentNum = 0;
        private int ModSegmentLength = 2048;

        #endregion

        #region ReaderTree

        private int TempLocatNum = 0;
        private int TempLstCount = 0;
        private bool isInitial = true;
        private int GetOneLocateNum(int Num)
        {
            if (ListNumbers.Count == 2)
            {
                isInitial = true;
                TempLocatNum = ListNumbers.IndexOf(Num);
                initialList();
                return TempLocatNum;
            }


            TempLocatNum = ListNumbers.IndexOf(Num) ;

            ListNumbers.RemoveAt(TempLocatNum);
            return TempLocatNum;

        }

        private int StopModLengthByHalf = 128;
        private int GetOneLocateNum_ByHalf(int Num)
        {
         


            TempLocatNum = ListNumbers.IndexOf(Num);

            ListNumbers.RemoveAt(TempLocatNum);


            if (ListNumbers.Count == StopModLengthByHalf)
            {
                isInitial = true;
                initialList();
               
            }


            return TempLocatNum;

        }

        public void GetOneReadList(ref ReaderWriterOneNum02B ReaderNum)
        {
            
            isInitial = false;
            while (ReaderNum.isAbleRead && isInitial == false)
            {
                TempLstCount = ListNumbers.Count;
                WriterNumByMod.WriteNumber(TempLstCount, GetOneLocateNum(ReaderNum.GetOneNumber()));
            }

            if(isInitial)
                ReaderNum.GetOneNumber();
            
            SegmentNum++;

        }

        public void GetOneReadList(ref List<int> ListNum)
        {
            if (!isInitial)
                initialList();

                isInitial = false;
            foreach (int num in ListNum)
            {
                if (!isInitial)
                {
                    TempLstCount = ListNumbers.Count;
                    WriterNumByMod.WriteNumber(TempLstCount, GetOneLocateNum(num));
                }
                else
                {
                    isInitial = false;
                }
            }

        }
        public void GetOneReadList_ByHalf(ref List<int> ListNum)
        {
            if (!isInitial)
                initialList();

            isInitial = false;
            foreach (int num in ListNum)
            {
                if (!isInitial)
                {
                    TempLstCount = ListNumbers.Count;
                    WriterNumByMod.WriteNumber(TempLstCount, GetOneLocateNum_ByHalf(num));
                }
                else
                {
                    isInitial = false;
                }
            }

            initialList();

        }







        #endregion


    }


    class MakeTreeUniq05
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTU05ML";
        private string DeExtension = "DeMTU05ML";


        public MakeTreeUniq05()
        {

        }
        public MakeTreeUniq05(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }



        public void StartMakeTreeUniq_FileUniqW01()
        {
            ReaderWriteFileBits02B ReaderBits = new ReaderWriteFileBits02B(true);
            if (ReaderBits.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W01");
            if (WriterNum.GetIsCancel)
                return;// "isCancel";


            MakeTreeUniq05Tree Tree = new MakeTreeUniq05Tree(ModLength, ref ReaderBits);
            
            while (ReaderBits.isAbleRead)
            {
                foreach (int n in Tree.GetOneReadList())
                {
                    WriterNum.WriteNumber(n);
                }
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


            MakeTreeDeUniq05Tree Tree = new MakeTreeDeUniq05Tree(ModLength, ref WriterBits);

            while (ReaderNum.isAbleRead)
            {
                Tree.GetOneReadList(ref ReaderNum);
            }

            ReaderNum.End();
            WriterBits.CloseFile();

        }
        public void StartMakeTreeDeUniq_FileUniqW01B()
        {
            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            ReaderWriteFileBits02B WriterBits = new ReaderWriteFileBits02B(false, DeExtension + ModLength.ToString() + "W01");
            if (WriterBits.GetIsCancel)
                return;// "isCancel";
          
            

            MakeTreeDeUniq05Tree Tree = new MakeTreeDeUniq05Tree(ModLength, ref WriterBits);
            List<int> TempListUniq = new List<int>();
            while (ReaderNum.isAbleRead)
            {
                TempListUniq = ReaderNum.GetManyNum(ModLength);
                if (TempListUniq.Count == ModLength)
                    Tree.GetOneReadList(ref TempListUniq);
                
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



            MakeTreeUniq05Tree Tree = new MakeTreeUniq05Tree(ModLength, ref ReaderBits);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);


            List<int> TempUniqList;

            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;
            int i = 0;
            int Rest = 0;
            while (ReaderBits.isAbleRead)
            {

                //Oper == 50
                TempUniqList = Tree.GetOneReadList();
                MainSegmentInfo = Tree.SegmentInfoNod;
                MainSegmentInfo.OperationId = 50;

                //Oper == 51
                Tree1.MakeDeUniqOnly_int(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 52W1 == 52
                Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 52, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 52W1 == 58
                Tree2W2.ReadNumber_W2Info(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 58, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 53W1 == 135
                Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 135, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);
                //Oper == 53W1 == 136
                Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 136, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 54
                Tree4.ReadNumber_W1(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 54, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 56  Sort
                TreeSort1.SortList(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 56, WriterBitsInfo.SumOfWriteBits);
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



            MakeTreeUniq05Tree Tree = new MakeTreeUniq05Tree(TempModLength, ref ReaderBits);

            MakeTreeDeUniq01Oper Tree1 = new MakeTreeDeUniq01Oper(ModLength);
            MakeTreeDeUniq01Oper Tree1_ByHalf = new MakeTreeDeUniq01Oper(TempModLength);

            MakeTreeDeUniq02Tree Tree2W1 = new MakeTreeDeUniq02Tree(ModLength - 1);
            MakeTreeDeUniq02Tree Tree2W2 = new MakeTreeDeUniq02Tree(ModLength);

            MakeTreeDeUniq03Tree Tree3W5 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq03Tree Tree3W6 = new MakeTreeDeUniq03Tree(ModLength, ref WriterBitsInfo);

            MakeTreeDeUniq04Tree Tree4 = new MakeTreeDeUniq04Tree(ModLength, ref WriterBitsInfo);
            MakeTreeDeUniq05Tree Tree5 = new MakeTreeDeUniq05Tree(ModLength, ref WriterBitsInfo);

            MakeTreeMergeSort01 TreeSort1 = new MakeTreeMergeSort01(ModLength, ref WriterBitsInfo);


            List<int> TempUniqList=new List<int>();
            List<int> TempHalfUniqList = new List<int>();
            int[] TempListNumberForhalf = new int[TempModLength];
            BitArray TempBitsArr = new BitArray(TempModLength, false);
            
            int CTforHalf = 0;
            int CTi=0;
            MakeTreeUniqInfoNode00 MainSegmentInfo;
            MakeTreeUniqInfoNode00 SegmentInfo;
            //  int OperationNum = 40;
            int i = 0;
            int Rest = 0;
            while (ReaderBits.isAbleRead)
            {
                //ReaderOrignal
                {
                    //Oper == 50
                    TempHalfUniqList = Tree.GetOneReadList_ByHalf();
                    MainSegmentInfo = Tree.SegmentInfoNod;
                    MainSegmentInfo.OperationId = 50;


                    //Oper == 12
                    Tree1_ByHalf.MakeDeUniqOnly_int_Byhalf(ref TempHalfUniqList, ref WriterBitsInfo);
                    SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 12, WriterBitsInfo.SumOfWriteBits);
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
                        CTi=0;
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
                   // MainSegmentInfo.HowReadBits = MainSegmentInfo.HowReadBits - TempModLength;
                    //W02
                    MainSegmentInfo.HowReadBits = MainSegmentInfo.HowReadBits - ((TempModLength + ModLength) / 2);

                    TempHalfUniqList.Clear();
                }

                //Oper == 51
                Tree1.MakeDeUniqOnly_int(ref TempUniqList, ref WriterBitsInfo);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 51, WriterBitsInfo.SumOfWriteBits);
                WriterBitsInfo.SumOfWriteBits = 0;
                WriterNodeInfo.WriteNod(ref SegmentInfo);

                ////Oper == 52W1 == 52
                //Tree2W1.ReadNumber_W1Info(ref TempUniqList, ref WriterBitsInfo);
                //SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 52, WriterBitsInfo.SumOfWriteBits);
                //WriterBitsInfo.SumOfWriteBits = 0;
                //WriterNodeInfo.WriteNod(ref SegmentInfo);
                ////Oper == 52W1 == 58
                //Tree2W2.ReadNumber_W2Info(ref TempUniqList, ref WriterBitsInfo);
                //SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 58, WriterBitsInfo.SumOfWriteBits);
                //WriterBitsInfo.SumOfWriteBits = 0;
                //WriterNodeInfo.WriteNod(ref SegmentInfo);

                ////Oper == 53W1 == 135
                //Tree3W5.ReadNumber_W5(ref TempUniqList, ModLength);
                //SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 135, WriterBitsInfo.SumOfWriteBits);
                //WriterBitsInfo.SumOfWriteBits = 0;
                //WriterNodeInfo.WriteNod(ref SegmentInfo);
                ////Oper == 53W1 == 136
                //Tree3W6.ReadNumber_W6(ref TempUniqList, ModLength);
                //SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 136, WriterBitsInfo.SumOfWriteBits);
                //WriterBitsInfo.SumOfWriteBits = 0;
                //WriterNodeInfo.WriteNod(ref SegmentInfo);

                //Oper == 54
                Tree4.ReadNumber_W1(ref TempUniqList);
                SegmentInfo = RefrishSegmentInfo(ref MainSegmentInfo, 54, WriterBitsInfo.SumOfWriteBits);
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
