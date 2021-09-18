using Comp1.Public.Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderFile.ReaderWriteFile02
{
    class OneIntBitsNode02
    {
        public OneIntBitsNode02 nextzero;
        public OneIntBitsNode02 nextone;

        public int Value;
        public List<bool> BitsList;
        public bool Stoping;

        public OneIntBitsNode02()
        {
            Value = 0;
            Stoping = false;
        }
        public OneIntBitsNode02(int NumValue)
        {
            Value = NumValue;
            Stoping = false;
        }


    }
    class OneIntBitsTree02
    {
        public OneIntBitsNode02 root;
        public List<OneIntBitsNode02> ListNumber;
       

        private int NumberNodes = 256;
        private int RestNodesNum = 0;
        private  int ModLevel = 8;

        public OneIntBitsTree02()
        {
            Creat();
        }
        public OneIntBitsTree02(int ModNumLength)
        {
            NumberNodes = ModNumLength;
            Creat();
        }


        private void MainModify()
        {
            

            int count = 0;
            int Nodes = 0;
            while (count!=24)
            {
                Nodes = Convert.ToInt32(Math.Pow(2, count));
                if (Nodes < NumberNodes)
                {
                    count++;
                }
                else
                {
                    if (Nodes == NumberNodes)
                    {
                        ModLevel = count;
                        RestNodesNum = 0;

                        break;
                    }
                    else
                    {
                        count--;

                        Nodes = Convert.ToInt32(Math.Pow(2, count));
                        RestNodesNum = NumberNodes - Nodes;

                        ModLevel = count;
                        break;
                    }

                }
            }
            

        }
        private void Creat()
        {
            MainModify();
            ListNumber = new List<OneIntBitsNode02>(NumberNodes);

            for (int i = 0; i != NumberNodes; i++)
            {
                ListNumber.Add(new OneIntBitsNode02());
            }

            root = new OneIntBitsNode02();

            for (int n = 1; n != ModLevel; n++)
            {
                creatLevel(this.root);
            }
            creatLastLevel(this.root);
            creatRestLevel(this.root);
        }

        private void creatLevel(OneIntBitsNode02 cr)
        {
            if (cr.nextzero != null)
            {
                creatLevel(cr.nextzero);
                creatLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = new OneIntBitsNode02();

                cr.nextone = new OneIntBitsNode02();

            }
        }
        private void creatLastLevel(OneIntBitsNode02 cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, ModLevel));
            OneIntBitsNode02 po = new OneIntBitsNode02();
            while (i != Timer)
            {

                BitArray bitNum = BitArrayOperation.intvaluToBitsArr(i, ModLevel);

                po.nextzero = root;

                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextzero.nextone == null)
                        {
                            po.nextzero.nextone = new OneIntBitsNode02();

                            po.nextzero = po.nextzero.nextone;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextone;
                        }
                    }
                    else
                    {
                        if (po.nextzero.nextzero == null)
                        {
                            po.nextzero.nextzero = new OneIntBitsNode02();

                            po.nextzero = po.nextzero.nextzero;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextzero;
                        }
                    }



                }

                po.nextzero.Value = i;
                po.nextzero.Stoping = true;
                po.nextzero.BitsList = new List<bool>();
                foreach (bool b in bitNum)
                {
                    po.nextzero.BitsList.Add(b);
                }

                ListNumber[po.nextzero.Value] = po.nextzero;

                i++;
            }
        }

        private void creatRestLevel(OneIntBitsNode02 cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, ModLevel));
            OneIntBitsNode02 po;
            po = root;
            BitArray bitNum;
            while (i != RestNodesNum)
            {
                bitNum = BitArrayOperation.intvaluToBitsArr(i, ModLevel);

                po = root;
                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextone == null)
                        {
                            po.nextone = new OneIntBitsNode02();

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
                            po.nextzero = new OneIntBitsNode02();

                            po = po.nextzero;
                        }
                        else
                        {
                            po = po.nextzero;
                        }
                    }
                }


                //Edit
                {
                    //Past
                    {
                        po.nextzero = new OneIntBitsNode02(po.Value);
                        po.nextzero.Stoping = true;

                        po.nextzero.BitsList = po.BitsList;
                        po.nextzero.BitsList.Add(false);

                        ListNumber[po.nextzero.Value] = po.nextzero;
                    }

                    //One
                    {
                        po.nextone = new OneIntBitsNode02(Timer);
                        po.nextone.Stoping = true;

                        po.nextone.BitsList = new List<bool>();
                        foreach (bool b in bitNum)
                        {
                            po.nextone.BitsList.Add(b);
                        }
                        po.nextone.BitsList.Add(true);

                        ListNumber[po.nextone.Value] = po.nextone;
                    }

                    po.Value = 0;
                    po.Stoping = false;
                    po.BitsList = null;
                }
                Timer++;
                i++;
            }
        }


    }



    class ReaderWriterOneNum02B
    {
        #region  Main Proprties
        private bool isReaderMod = true;

        public bool isAbleRead;
        private ReaderWriteFileBits02B ReaderBits;
        private OneIntBitsTree02 Tree;

        private int ModNum = 256;
     //   private bool ReaderMod = false;


        public ReaderWriterOneNum02B(bool isReadMod)
        {
            isReaderMod = isReadMod;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod);
            isAbleRead = true;

            CreateTree();
        }
        public ReaderWriterOneNum02B(bool isReadMod, string Extension)
        {
            isReaderMod = isReadMod;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod, Extension);
            isAbleRead = true;

            CreateTree();
        }
        public ReaderWriterOneNum02B( bool isReadMod , int ModNumLength)
        {
            isReaderMod = isReadMod;
            ModNum = ModNumLength;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod);
            isAbleRead = true;

            CreateTree();
        }
        public ReaderWriterOneNum02B(bool isReadMod, int ModNumLength, ReaderWriteFileBits02B ReaderBit)
        {
            isReaderMod = isReadMod;
            ModNum = ModNumLength;
            ReaderBits = ReaderBit;
            isAbleRead = true;

            CreateTree();
        }
        public ReaderWriterOneNum02B(bool isReadMod, int ModNumLength , string Extension)
        {
            isReaderMod = isReadMod;
            ModNum = ModNumLength;
            ReaderBits = new ReaderWriteFileBits02B(isReaderMod, Extension);
            isAbleRead = true;

            CreateTree();
        }



        private void CreateTree()
        {
            Tree = new OneIntBitsTree02(ModNum);
            root = Tree.root;
            po = root;
        }

        public void End()
        {
            ReaderBits.CloseFile();

        }
        #endregion

        #region  Number Read
        private int AddingBits = 0;
        private OneIntBitsNode02 root;
        private OneIntBitsNode02 po;


        private int GetNumber()
        {
            int Num;
            while (!po.Stoping)
            {
                if (ReaderBits.isAbleRead)
                {
                    if (ReaderBits.GetBit())
                    {
                        po = po.nextone;
                    }
                    else
                    {
                        po = po.nextzero;
                    }
                }
                else
                {
                    po = po.nextzero;
                    isAbleRead = false;
                    AddingBits++;
                }
            }
            Num = po.Value;
            po = root;
            return Num;

        }

        public int GetOneNumber()
        {
            return GetNumber();
        }
        public List<int> GetManyNum(int Length)
        {
            List<int> ListNumber = new List<int>();
            for (int i = 0; i != Length; i++)
            {
                if (isAbleRead)
                {
                    ListNumber.Add(GetNumber());
                }
                else
                {
                    break;
                }
            }
            return ListNumber;
        }

        #endregion

        #region  Number Save
        private void SaveNum(int Num)
        {
            foreach (bool b in Tree.ListNumber[Num].BitsList)
            {
                ReaderBits.WriteBit(b);
            }
        }

        public void WriteNumber(int Number)
        {
            SaveNum(Number);
        }
        public void WriterManyNumber(List<int> Numbers)
        {
            foreach (int n in Numbers)
            {
                SaveNum(n);
            }
        }
        public void WriterManyNumber( ref List<int> Numbers)
        {
            foreach (int n in Numbers)
            {
                SaveNum(n);
            }
        }



        #endregion


        #region For Other Usage

        public int GetStopNumLength
        {
            get
            {
                return ReaderBits.GetStopNumLength ;
            }
        }
        public int GetReaderDataLength
        {
            get
            {
                return ReaderBits.GetReaderDataLength;
            }
        }
        public bool GetIsCancel
        {
            get
            {
                return ReaderBits.GetIsCancel;
            }
        }

        public string GetSavePath
        {
            get
            {
                return ReaderBits.GetSavePath;
            }
        }


        #endregion


    }
}
