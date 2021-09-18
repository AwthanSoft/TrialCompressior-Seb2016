using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MTF
{
    class MoveToByMoreNode01
    {
        #region Proprties For Nodes

        public List<MoveToByMoreNode01> NextList;
        public int IdNode = 0;

        public int FirstNumbers = 0;

        public int WrFirstNum = 0;

        #endregion

        #region

        public List<MoveToByMoreNode01> MoreList;
        public bool isInMoreList = false;
        public int LocateInMoreList = 0;
        private MoveToByMoreNode01 TempPo;
        // public ChangerByMoreKeyNumNod01 KeyMore;

        #endregion

        #region For Counters
        public int Counter = 0;
        public int FirstLocateCounter = 0;

        public void PlusCount()
        {
            Counter++;
        }

        #endregion



        #region Overload


        public MoveToByMoreNode01(int FirstNum, ref List<MoveToByMoreNode01> isMoreList)
        {
            FirstNumbers = FirstNum;
            MoreList = isMoreList;

            RefrishWritNum(FirstNum);
        }
        public MoveToByMoreNode01(int FirstNum, ref List<MoveToByMoreNode01> isMoreList, int NodesNumber)
        {
            FirstNumbers = FirstNum;
            MoreList = isMoreList;
            IdNode = NodesNumber;

            RefrishWritNum(FirstNum);
        }
        public MoveToByMoreNode01(int NodesNumber)
        {
            IdNode = NodesNumber;
        }

        public void RefrishWritNum(int FrNum)
        {
            WrFirstNum = FrNum;
        }
        #endregion


        #region Writer


        public ReaderWriterOneNum02B WriterData;

        public void Write()
        {
            WriteData();
            PlusCount();
            RefrishMoreList();

        }
        private void WriteData()
        {
            WriterData.WriteNumber(LocateInMoreList);
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

        public void DeWrite()
        {
            DeWriteData();
            PlusCount();
            RefrishMoreList();

        }
        private void DeWriteData()
        {
            WriterData.WriteNumber(FirstNumbers);
        }

        #endregion



        #region  Refrish Writers

        public void RefrishWriter(ref ReaderWriterOneNum02B WriterDataNod)
        {
            WriterData = WriterDataNod;
        }

        #endregion




    }
    class MoveToByMoreTree01
    {
        private int ModLength = 256;
        public List<MoveToByMoreNode01> NumberList;
        public List<MoveToByMoreNode01> MoreList;

        public MoveToByMoreTree01()
        {
            Create();
        }
        public MoveToByMoreTree01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;
            Create();
        }

        public void RefrishMoreList()
        {
           
            
                int   idCounter = 0;
                for (int i = 0; i != ModLength; i++)
                {
                    NumberList[i].Counter = 0;
                    NumberList[i].isInMoreList = true;
                    NumberList[i].LocateInMoreList = idCounter;


                    MoreList[i] = NumberList[i];
                    idCounter++;
                }

            

        }
        private void Create()
        {
            MoreList = new List<MoveToByMoreNode01>();

            //01 create && fill NumberList
            int idCounter = 0;
            NumberList = new List<MoveToByMoreNode01>();
            for (int i = 0; i != ModLength; i++)
            {
                NumberList.Add(new MoveToByMoreNode01(i, ref MoreList, idCounter));

            }
            //02 Create MoreList
            {

                 idCounter = 0;
                for (int i = 0; i != ModLength; i++)
                {
                    NumberList[i].isInMoreList = true;
                    NumberList[i].LocateInMoreList = idCounter;

                    MoreList.Add(NumberList[i]);
                    idCounter++;
                }
            }

        }


        public void RefrishWriter(ReaderWriterOneNum02B WriterDataNod)
        {

            foreach (MoveToByMoreNode01 nod in NumberList)
            {
                nod.RefrishWriter(ref WriterDataNod);
            }


        }




    }



    class MoveToByMore01
    {
        public StringBuilder Report;
        private int ModLength = 256;

        private string Extension = "MTbyM01ML";
        private string DeExtension = "DeMTbyM01ML";

      

        public MoveToByMore01()
        {

        }
        public MoveToByMore01(int ModLengthNumber)
        {
            ModLength = ModLengthNumber;;
        }
       
        public void StartMoveToByMore()
        {

            MoveToByMoreTree01 Tree = new MoveToByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() );
            Tree.RefrishWriter(WriterNum);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {

                    Tree.NumberList[n].Write();

                }


            }

            ReaderNum.End();
            WriterNum.End();
           

        }

        public void StartDeMoveToByMore()
        {

            MoveToByMoreTree01 Tree = new MoveToByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString());
            Tree.RefrishWriter(WriterNum);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {

                    Tree.MoreList[n].DeWrite();

                }


            }

            ReaderNum.End();
            WriterNum.End();


        }

        public void StartMoveToByMoreW02()
        {

            MoveToByMoreTree01 Tree = new MoveToByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, Extension + ModLength.ToString() + "W02");
            Tree.RefrishWriter(WriterNum);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {

                    Tree.NumberList[n].Write();

                }

                Tree.RefrishMoreList();

            }

            ReaderNum.End();
            WriterNum.End();


        }

        public void StartDeMoveToByMoreW02()
        {

            MoveToByMoreTree01 Tree = new MoveToByMoreTree01(ModLength);

            ReaderWriterOneNum02B ReaderNum = new ReaderWriterOneNum02B(true, ModLength);
            if (ReaderNum.GetIsCancel)
                return;// "isCancel";



            ReaderWriterOneNum02B WriterNum = new ReaderWriterOneNum02B(false, ModLength, DeExtension + ModLength.ToString() + "W02");
            Tree.RefrishWriter(WriterNum);


            int DataLengthStop = ReaderNum.GetStopNumLength;

            while (ReaderNum.isAbleRead)
            {
                List<int> ListData = ReaderNum.GetManyNum(DataLengthStop);

                foreach (int n in ListData)
                {

                    Tree.MoreList[n].DeWrite();

                }
                Tree.RefrishMoreList();

            }

            ReaderNum.End();
            WriterNum.End();


        }
     



    }


}
