using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MTF.MoveToByMore
{

    class MoveToByMoreNode02
    {
        #region Proprties For Nodes
       // public List<MoveToByMoreNode02> MainList;
        public int IdNode = 0;


        #endregion

        #region MoreList

        public List<MoveToByMoreNode02> MoreList;
        public int LocateInMoreList = 0;

        private MoveToByMoreNode02 TempPo;

        #endregion 

        #region For Counters
        public int Counter = 0;

        public void PlusCount()
        {
            Counter++;
        }

        #endregion

        #region Overload


        public MoveToByMoreNode02(int IDNode, ref List<MoveToByMoreNode02> isMoreList)
        {
            IdNode = IDNode;
            MoreList = isMoreList;
            LocateInMoreList = IdNode;

        }
        public MoveToByMoreNode02(int IDNode, ref List<MoveToByMoreNode02> isMoreList, int LocaterNode)
        {
            IdNode = IDNode;
            MoreList = isMoreList;
            LocateInMoreList = LocaterNode;

        }

        //public MoveToByMoreNode02(int IDNode, ref List<MoveToByMoreNode02> isMoreList, ref List<MoveToByMoreNode02> isMainList)
        //{
        //    IdNode = IDNode;
        //    MoreList = isMoreList;
        //    LocateInMoreList = IdNode;
        //    MainList = isMainList;

        //}
        //public MoveToByMoreNode02(int IDNode, ref List<MoveToByMoreNode02> isMoreList, ref List<MoveToByMoreNode02> isMainList, int LocaterNode)
        //{
        //    IdNode = IDNode;
        //    MoreList = isMoreList;
        //    LocateInMoreList = LocaterNode;
        //    MainList = isMainList;

        //}
       
       
        #endregion


        #region Writer

        private int TempWriterNum = 0;

        public int Write()
        {
            TempWriterNum = LocateInMoreList;
            PlusCount();
            RefrishMoreList();

            return TempWriterNum;
        }
        public int DeWrite()
        {
            TempWriterNum = IdNode;
            PlusCount();
            RefrishMoreList();

            return TempWriterNum;

        }

        private void RefrishMoreList()
        {
            while (LocateInMoreList != 0)
            {
                if (Counter <= MoreList[LocateInMoreList - 1].Counter)
                {
                    break;
                }
                else
                {
                    TempPo = MoreList[LocateInMoreList - 1];

                    MoreList[LocateInMoreList - 1] = this;
                    MoreList[LocateInMoreList] = TempPo;

                    LocateInMoreList--;
                    TempPo.LocateInMoreList++;

                }

            }

        }
        #endregion


        public void Initial()
        {
            LocateInMoreList = IdNode;
            MoreList[LocateInMoreList] = this;
            Counter = 0;
        }
        public void Initial(int LocaterNode)
        {
            LocateInMoreList = LocaterNode;
            MoreList[LocateInMoreList] = this;
            Counter = 0;
        }




    }
    class MoveToByMoreTree02
    {
        public int TreeId = 0;
        private int ModLength = 256;
        public List<MoveToByMoreNode02> NumberList;
        public List<MoveToByMoreNode02> MoreList;

     //   public int[] WriterCounter;

        public MoveToByMoreTree02()
        {
            Create();
        }
        public MoveToByMoreTree02(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
            Create();
        }
        public MoveToByMoreTree02(int ModLengthNumber , int TreeID)
        {
            ModLength = ModLengthNumber;
            TreeId = TreeID;
            Create();
        }

        #region Create

        public void Initial()
        {

          //  int idCounter = 0;

            foreach (MoveToByMoreNode02 nod in NumberList)
            {
                nod.Initial();
            }

        
        }
        private void Create()
        {
     //       WriterCounter = new int[ModLength];

            MoreList = new List<MoveToByMoreNode02>();

            //01 create && fill NumberList & MoreList 
            {
                NumberList = new List<MoveToByMoreNode02>();
                for (int i = 0; i != ModLength; i++)
                {
                    MoveToByMoreNode02 NewNode = new MoveToByMoreNode02(i, ref MoreList);
                    NumberList.Add(NewNode);
                    MoreList.Add(NewNode);
                }
            }
           

        }

        #endregion

        #region  Writer

        public int GetNumber(int Number)
        {
            return NumberList[Number].Write();
        }
        public int DeGetNumber(int Number)
        {
            return MoreList[Number].DeWrite();
        }

        #endregion

    }

    class MoveToByMoreTimesTree02
    {
         private int ModLength = 256;
        private int ModTime=1;
        public List<MoveToByMoreTree02> TreeList;

        //   public int[] WriterCounter;


        #region OverLoad
        public MoveToByMoreTimesTree02()
        {
            Create();
        }
        public MoveToByMoreTimesTree02(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
            Create();
        }
        public MoveToByMoreTimesTree02(int ModLengthNumber , int ModTimes)
        {
            ModLength = ModLengthNumber;
            ModTime = ModTimes;
            Create();
        }

        #endregion

        #region Create

        private void Create()
        {
            TreeList = new List<MoveToByMoreTree02>();
            for (int i = 0; i != ModTime; i++)
            {
                TreeList.Add(new MoveToByMoreTree02(ModLength , i));
            }
        }
        public void Initial()
        {

            //  int idCounter = 0;

            foreach (MoveToByMoreTree02 Tree in TreeList)
            {
                Tree.Initial();
            }


        }

        #endregion

        #region  Writer

        private int TempNum = 0;
        public int GetNumber(int Number)
        {
            
            for (int i = 0; i != ModTime; i++)
            {
                Number = TreeList[i].GetNumber(Number);
            }


            return Number;
        }
        public int DeGetNumber(int Number)
        {

            for (int i = 0; i != ModTime; i++)
            {
                Number = TreeList[i].DeGetNumber(Number);
            }

            return Number;
        }

        #endregion

    }



    class MoveToByMore02
    {
        public StringBuilder Report;

        private int ModLength = 256;
        private int ModTime = 1;

        private string Extension = "MTbyM02ML";
        private string DeExtension = "DeMTbyM02ML";


        #region OverLoad

        public MoveToByMore02()
        {

        }
        public MoveToByMore02(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
        }
        public MoveToByMore02(int ModLengthNumber, int ModTimes)
        {
            ModLength = ModLengthNumber;
            ModTime = ModTimes;
        }

        #endregion

        public void StartMoveToByMoreFullFile()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

           
            Extension = Extension + ModLength.ToString() + "MT" + ModTime.ToString() + "MR" + "FF";
            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength,Extension );

            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MoveToByMoreTimesTree02 Trees = new MoveToByMoreTimesTree02(ModLength, ModTime);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    WriterNum.WriteNumber(Trees.GetNumber(n));
                }


            }

            ReaderNum.End();
            WriterNum.End();


        }
        public void StartDeMoveToByMoreFullFile()
        {

           ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";


            DeExtension = DeExtension + ModLength.ToString() + "MT" + ModTime.ToString() + "MR" + "FF";
            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension);

            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MoveToByMoreTimesTree02 Trees = new MoveToByMoreTimesTree02(ModLength, ModTime);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    WriterNum.WriteNumber(Trees.DeGetNumber(n));
                }

            }

            ReaderNum.End();
            WriterNum.End();


        }

        public void StartMoveToByMoreW02()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";

            int DataLengthStop = ReaderNum.GetStopNumLength;

            Extension = Extension + ModLength.ToString() + "MT" + ModTime.ToString() + "MR" + DataLengthStop.ToString() +"W02";
            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension);

            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MoveToByMoreTimesTree02 Trees = new MoveToByMoreTimesTree02(ModLength, ModTime);


            

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    WriterNum.WriteNumber(Trees.GetNumber(n));
                }

                Trees.Initial();
            }

            ReaderNum.End();
            WriterNum.End();


        }
        public void StartDeMoveToByMoreW02()
        {

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";


            int DataLengthStop = ReaderNum.GetStopNumLength;


            DeExtension = DeExtension + ModLength.ToString() + "MT" + ModTime.ToString() + "MR" + DataLengthStop.ToString() + "W02";
            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension);

            if (WriterNum.GetIsCancel)
                return;// "isCancel";

            MoveToByMoreTimesTree02 Trees = new MoveToByMoreTimesTree02(ModLength, ModTime);


            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {
                    WriterNum.WriteNumber(Trees.DeGetNumber(n));
                }

                Trees.Initial();
            }

            ReaderNum.End();
            WriterNum.End();


        }







    }

   
}
