using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;
using Comp1.Public.Lib;
using Comp1.Public.ReaderWriterFile;

namespace Comp1.MergeSort.Merge01
{
     class CompByMerge01Node
    {
        public CompByMerge01Node nextzero;
        public CompByMerge01Node nextone;
        public CompByMerge01Node Refrance;
        public int Value;
        public bool PastBit = false;
        public bool Continu;

        public CompByMerge01Node Tempnextzero;
        public CompByMerge01Node Tempnextone;
        public CompByMerge01Node TempRefrance;
        public int TempValue;
        public bool TempPastBit = false;

        public bool TempContinu;

        public CompByMerge01Node()
        {
            TempValue = 0;
            Value = 0;
            Continu = false;
            TempContinu = false;
            nextzero = null;
            nextone = null;
        }

    }

     class CompByMerge01Tree
    {
        public CompByMerge01Node root;
        private int NumOfLineNode;
        private int NumOfLevels;

        public List<CompByMerge01Node> ListNod = new List<CompByMerge01Node>();

        public int lengthMod = 8;





        public CompByMerge01Tree()
        {
            root = new CompByMerge01Node();
            NumOfLevels = 0;
            NumOfLineNode = 0;


        }

        public CompByMerge01Tree(int LengthMod)
        {
            root = new CompByMerge01Node();
            NumOfLevels = 0;
            NumOfLineNode = 0;
            lengthMod = LengthMod;

            creat(LengthMod);

        }

        private CompByMerge01Node creatnod()
        {
            return new CompByMerge01Node();
        }
        private CompByMerge01Node creatnodLastLine()
        {

            CompByMerge01Node newnod = new CompByMerge01Node();
            newnod.Value = NumOfLineNode;
            NumOfLineNode++;
            newnod.Continu = true;

            return newnod;
        }
        private CompByMerge01Node creatnodLastLine(int value)
        {
            CompByMerge01Node newnod = new CompByMerge01Node();
            newnod.Value = value;
            newnod.Continu = true;

            return newnod;
        }

        public void creat(int num)
        {
            //if (num <= 1)
            //    return;
            lengthMod = new int();
            lengthMod = num;
            for (int n = 1; n != num; n++)
            {
                NumOfLineNode = 0;
                creatLevel(this.root);
                NumOfLevels++;
            }

            NumOfLineNode = 0;
            creatLastLevel(this.root);
            NumOfLevels++;
            NumOfLineNode = 0;

            MainInitial(this.root);
            this.root.Refrance = null;
            this.root.TempRefrance = null;
            NumOfLineNode = 0;

           // AddNodToList(this.root);


        }
        private void creatLevel(CompByMerge01Node cr)
        {
            if (cr.nextzero != null)
            {
                creatLevel(cr.nextzero);
                creatLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = creatnod();
                //   cr.nextzero.Refrance = cr;
                cr.nextone = creatnod();
                //   cr.nextone.Refrance = cr;

            }
        }
        private void creatLastLevel(CompByMerge01Node cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, lengthMod));
            CompByMerge01Node po = new CompByMerge01Node();
            while (i != Timer)
            {
                BitArray bitNum = BitArrayOperation.intvaluToBitsArr(i, lengthMod);

                po.nextzero = root;

                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextzero.nextone == null)
                        {
                            po.nextzero.nextone = creatnod();
                            //      po.nextzero.nextone = po.nextzero.Refrance;
                            po.nextzero = po.nextzero.nextone;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextone;
                        }


                        po.nextzero.PastBit = true;
                    }
                    else
                    {
                        if (po.nextzero.nextzero == null)
                        {
                            po.nextzero.nextzero = creatnod();
                            //     po.nextzero.nextzero = po.nextzero.Refrance;
                            po.nextzero = po.nextzero.nextzero;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextzero;
                        }

                        po.nextzero.PastBit = false;
                    }



                }
                po.nextzero.Value = i;
                po.nextzero.Continu = true;

                ListNod.Add(po.nextzero);

               

                i++;
            }
        }

        public void MainInitial(CompByMerge01Node cr)
        {


            if (cr.nextzero != null && cr.nextone != null)
            {
                cr.nextzero.Refrance = cr;
                cr.nextone.Refrance = cr;

                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;

                cr.TempContinu = cr.Continu;

                cr.TempPastBit = cr.PastBit;

                cr.TempValue = cr.Value;

                MainInitial(cr.nextzero);
                MainInitial(cr.nextone);
            }
            else
            {
                cr.nextzero = null;
                cr.nextone = null;
                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;

                cr.TempValue = cr.Value;

                cr.TempPastBit = cr.PastBit;

                cr.TempContinu = cr.Continu;

            }
        }
        //private void MainInitialValu()
        //{
        //    ListNod = new List<CompByMerge01Node>();
        //    int i = 0;
        //    int Timer = Convert.ToInt32(Math.Pow(2, lengthMod));
        //    CompByMerge01Node po = new CompByMerge01Node();
        //    while (i != Timer)
        //    {
        //        BitArray bitNum = BitArrayOperation.intvaluToBitsArr(i, lengthMod);

        //        po.nextzero = root;

        //        foreach (bool b in bitNum)
        //        {
        //            if (b == true)
        //            {
        //                po.nextzero = po.nextzero.nextone;
        //                po.nextzero.PastBit = true;
        //                po.nextzero.TempPastBit = true;
        //            }
        //            else
        //            {
        //                po.nextzero = po.nextzero.nextzero;
        //                po.nextzero.PastBit = false;
        //                po.nextzero.TempPastBit = false;
        //            }

        //        }


        //        if (po.nextzero.Continu == true)
        //        {
        //            po.nextzero.Continu = true;
        //            po.nextzero.Value = NumOfLineNode;


        //            po.nextzero.TempValue = po.nextzero.Value;



        //            NumOfLineNode++;

        //            ListNod.Add(po.nextzero);
        //        }

        //        i++;
        //    }
        //}

        public void Initial(CompByMerge01Node cr)
        {
            if (cr.nextzero != null )
            {
                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;
                cr.TempContinu = cr.Continu;

                cr.TempPastBit = cr.PastBit;
                cr.TempValue = cr.Value;

                Initial(cr.nextzero);
                Initial(cr.nextone);
            }
            else
            {

                cr.TempRefrance = cr.Refrance;
                cr.TempValue = cr.Value;
                cr.TempContinu = cr.Continu;
                cr.TempPastBit = cr.PastBit;

                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;

            }
        }

        private void AddNodToList(CompByMerge01Node cr)
        {
            ListNod.Add(cr);
            if (cr.nextzero != null)
            {
                AddNodToList(cr.nextzero);
                AddNodToList(cr.nextone);
            }


        }









    }




     class CompByMerge01Oper
    {
        public StringBuilder sb;
        public StringBuilder RePort;
        private byte[] BytNum = new byte[256];

        //Temp
        
        private int NumOfByte = 0;
        private int Mod = 8;

        public int modLength = 0;

        private CompByMerge01Tree Tree;
        private CompByMerge01Node root;
        private CompByMerge01Node po;
        private CompByMerge01Node TempRatharPo;
        private CompByMerge01Sort Sor;

         /******For Save*********/
        public List<int> IntList;
        public List<List<int>> AllIntlist = new List<List<int>>();
        public List<byte> ByteList;
        public List<byte[]> SortSaveData = new List<byte[]>();




        /*** info  ****/
        private int TempReadBits = 0;
        public List<int> ListAdditionBitsinfo=new List<int>();
        public List<int> ListSortInfo = new List<int>();
        public List<int> ListSimlarInfo = new List<int>();


        public CompByMerge01Oper()
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            Mod = 8;
            MakeTree();

        }
        public CompByMerge01Oper(int mod)
        {

            sb = new StringBuilder();
            RePort = new StringBuilder();

            BytNum = new byte[256];
            {
                for (int i = 0; i != 256; i++)
                {

                    BytNum[i] = numoperation.int32toByte1(i);
                }
            }

            Mod = mod;
            MakeTree();
            
        }
        private void MakeTree()
        {
            IntList = new List<int>();
            ByteList = new List<byte>();
            
            Tree = new CompByMerge01Tree(Mod);

             root = Tree.root;
             po = root;
             TempRatharPo = new CompByMerge01Node();

             modLength = Convert.ToInt32(Math.Pow(2, Mod)) - 2;
             TempReadBits = 0;
             NumOfByte = 0;
             Sor = new CompByMerge01Sort();

        }


         /*********** For Info   ***********/
        public void infoMakeUniqOnly_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            IntList = new List<int>();



                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }
        public void infoMakeUniqSimlar_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                          

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            MakeArrInSimlar_int(IntList.ToArray<int>());

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info

                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            MakeArrInSimlar_int(IntList.ToArray<int>());

                            IntList = new List<int>();



                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }
        public void infoMakeUniqSort_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            IntList = new List<int>();



                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }
        public void infoMakeUniqByStop_int(byte[] DataByte)
        {
           
            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {



                            IntList.Add(po.TempValue);
                            
                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                           

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            IntList = new List<int>();



                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }

        /*********** For File ***********/
        public void MakeUniqOnly_int(byte[] DataByte)
        {
            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.TempContinu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            ////Exmine 01
                            //if (po.TempRefrance.Tempnextone.Continu == false)
                            //    MessageBox.Show("Error (Exmine 01) ! ");

                            //

                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                         //   InitialTree();


                            //Info
                            NumOfByte = 0;
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            AllIntlist.Add(IntList);
                            IntList = new List<int>();

                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.TempContinu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            ////Exmine 01
                            //if (po.TempRefrance.Tempnextzero.Continu == false)
                            //    MessageBox.Show("Error (Exmine 01) ! ");


                            //

                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            Tree.root.TempRefrance = null;
                            po = root;

                            InitialTree();


                            //Info
                            NumOfByte = 0;
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            AllIntlist.Add(IntList);
                            IntList = new List<int>();

                           

                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }
        public void MakeUniqSimlar_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            MakeArrInSimlar_int(ref IntList);
                            AllIntlist.Add(IntList);
                            IntList = new List<int>();

                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            MakeArrInSimlar_int(ref IntList);
                            AllIntlist.Add(IntList);
                            IntList = new List<int>();


                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }
        public void MakeUniqSort_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            SortSaveData.Add(Sor.SaveBitArr);

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            SortSaveData.Add(Sor.SaveBitArr);

                            IntList = new List<int>();



                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }


         /****** For Operation  *********/
        private void MakeArrInSimlar_int(int[] dataInt)
        {
            int LengthStop = dataInt.Length;
            BitArray BitRead = new BitArray(dataInt.Length, false);

            int NumRead = 0;
            int NumNow = 0;
            int Num = 0;

            int i = 0;

            while (NumRead != LengthStop)
            {
                i = 0;

                while (i != LengthStop)
                {
                    if (BitRead[i] == false && dataInt[i] == NumNow)
                    {
                        BitRead[i] = true;
                        dataInt[i] = Num;
                        NumNow++;
                        NumRead++;
                    }

                    i++;
                }
                Num++;
            }

            ListSimlarInfo.Add(Num - 1);


        }
        private void MakeArrInSimlar_int(ref List<int> dataInt)
        {
            int LengthStop = dataInt.Count;
            BitArray BitRead = new BitArray(dataInt.Count, false);

            int NumRead = 0;
            int NumNow = 0;
            int Num = 0;

            int i = 0;

            while (NumRead != LengthStop)
            {
                i = 0;

                while (i != LengthStop)
                {
                    if (BitRead[i] == false && dataInt[i] == NumNow)
                    {
                        BitRead[i] = true;
                        dataInt[i] = Num;
                        NumNow++;
                        NumRead++;
                    }

                    i++;
                }
                Num++;
            }

            ListSimlarInfo.Add(Num - 1);


        }
        private void InitialTree()
        {
            Tree = new CompByMerge01Tree(Mod);

            root = Tree.root;
            po = root;
            TempRatharPo = new CompByMerge01Node();
        }















      

         /***** *****/
        public void  MakeArrayUniq_int( byte[] DataByte)
        {
          
            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;
                            

                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;
                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;


                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach


           

        }
        public void MakeArrayUniq_byte(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            ByteList.Add(BytNum[po.TempValue]);
                            ByteList.Add(BytNum[po.TempRefrance.Tempnextone.TempValue]);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                        }
                        else
                        {
                            ByteList.Add(BytNum[po.TempValue]);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            ByteList.Add(BytNum[po.TempValue]);
                            ByteList.Add(BytNum[po.TempRefrance.Tempnextzero.TempValue]);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;
                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;


                        }
                        else
                        {

                            ByteList.Add(BytNum[po.TempValue]);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }

        /***For Info*****/
        public void InfoMakeArrayUniq_int(byte[] DataByte)
        {

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBits++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextone.TempValue);

                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;

                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            //MakeArrInSimlar_int(IntList.ToArray<int>());

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            IntList = new List<int>();


                        }
                        else
                        {
                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == modLength)
                        {
                            IntList.Add(po.TempValue);
                            IntList.Add(po.TempRefrance.Tempnextzero.TempValue);


                            //
                            NumOfByte = 0;
                            Tree.Initial(Tree.root);
                            root = Tree.root;
                            po = root;


                            //Info
                            ListAdditionBitsinfo.Add(TempReadBits);
                            TempReadBits = 0;

                            //MakeArrInSimlar_int(IntList.ToArray<int>());

                            Sor.SortIntArr(IntList.ToArray<int>());
                            ListSortInfo.Add(Sor.NumOfBits);

                            IntList = new List<int>();


                        }
                        else
                        {

                            IntList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach




        }

     




       

        public static List<int> MakeArrayUniq_int(int modLength, byte[] DataByte)
        {
            List<int> ByteList = new List<int>();
            int Mod=Convert.ToInt32(Math.Pow(2,modLength)) - 2;
            int TempReadBit = 0;
            int NumOfByte = 0;
            CompByMerge01Tree Tr = new CompByMerge01Tree(modLength);
            CompByMerge01Node root = Tr.root;
            CompByMerge01Node po = root ;
            CompByMerge01Node TempRatharPo = new CompByMerge01Node();

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBit++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == Mod)
                        {
                            ByteList.Add(po.TempValue);
                            ByteList.Add(po.TempRefrance.Tempnextone.TempValue);

                            
                            //
                            break;

                        }
                        else
                        {
                            ByteList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == Mod)
                        {
                            ByteList.Add(po.TempValue);
                            ByteList.Add(po.TempRefrance.Tempnextzero.TempValue);

                            //
                            break;


                        }
                        else
                        {
                            
                            ByteList.Add(po.TempValue);
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach


            return ByteList;

        }
        public static List<byte> MakeArrayUniq_Byte( byte[] DataByte)
        {
            int modLength = 8;
            List<byte> ByteList = new List<byte>();
            int Mod = Convert.ToInt32(Math.Pow(2, modLength)) - 2;
            int TempReadBit = 0;
            int NumOfByte = 0;
            CompByMerge01Tree Tr = new CompByMerge01Tree(modLength);
            CompByMerge01Node root = Tr.root;
            CompByMerge01Node po = root;
            CompByMerge01Node TempRatharPo = new CompByMerge01Node();

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                TempReadBit++;

                if (b == false)
                {
                    po = po.Tempnextzero;
                    if (po.Continu == true)
                    {
                        if (NumOfByte == Mod)
                        {

                            ByteList.Add(numoperation.int32toByte1(po.TempValue));
                            ByteList.Add(numoperation.int32toByte1(po.TempRefrance.Tempnextone.TempValue));

                            //
                            break;

                        }
                        else
                        {
                            ByteList.Add(numoperation.int32toByte1(po.TempValue));
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextone;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextone;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;
                        }

                    }

                }
                else
                {
                    po = po.Tempnextone;


                    if (po.Continu == true)
                    {
                        if (NumOfByte == Mod)
                        {
                            ByteList.Add(numoperation.int32toByte1(po.TempValue));
                            ByteList.Add(numoperation.int32toByte1(po.TempRefrance.Tempnextzero.TempValue));


                            //
                            break;


                        }
                        else
                        {

                            ByteList.Add(numoperation.int32toByte1(po.TempValue));
                            NumOfByte++;

                            if (po.TempRefrance.TempRefrance == null)
                            {
                                root = po.TempRefrance.Tempnextzero;
                                root.TempRefrance = null;
                            }
                            else
                            {
                                po = po.TempRefrance;
                                TempRatharPo = po.Tempnextzero;

                                TempRatharPo.TempRefrance = po.TempRefrance;

                                if (po.TempRefrance.Tempnextzero == po)
                                {
                                    po.TempRefrance.Tempnextzero = TempRatharPo;
                                }
                                else
                                {
                                    po.TempRefrance.Tempnextone = TempRatharPo;
                                }
                            }

                            po = root;


                        }


                    }

                }//End if true or false


            }//End foreach


            return ByteList;

        }











    }

    public class CompByMerge01Uniq
    {
        public StringBuilder sb;
        public StringBuilder RePort;

        private ReadWriteFile00 readerFile;
        private int dataBlock = 1024 * 1024;
        private string ExtensionFile = "MergeUniq";
        private CompByMerge01Oper Uniq;
        private int Mod = 8;

      

        public CompByMerge01Uniq()
        {
            sb = new StringBuilder();
            RePort = new StringBuilder();

        }
        public CompByMerge01Uniq(int modlength)
        {
            sb = new StringBuilder();
            RePort = new StringBuilder();
            Mod = modlength;
           

        }

        public void StartAsByte()
        {
            ExtensionFile = ExtensionFile + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
           // readerFile.BlockReaderLength = dataBlock;
            readerFile.OpenAll();

            CompByMerge01Oper Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.MakeArrayUniq_byte(readerFile.DataRead);
                readerFile.SaveDataByte(Uniq.ByteList.ToArray());
                Uniq.ByteList = new List<byte>();

            }

            readerFile.CloseAll();
          //  GetInfo();

        }

        public void InfoStartAsint()
        {
         //   ExtensionFile = ExtensionFile + Mod;
            readerFile = new ReadWriteFile00(ExtensionFile);
             readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();

             Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.InfoMakeArrayUniq_int(readerFile.DataRead);
              
                

            }

            readerFile.ReadClose();
            GetInfo();

        }


        public void GetInfo()
        {
            StringBuilder ss = new StringBuilder();

            int Defult = (Convert.ToInt32(Math.Pow(2, Mod))) * Mod;
         

            int average = 0;
            int AvgSort = 0;
            int i = 0;

            int iSComped = 0;
            int noComped = 0;
            while (i != Uniq.ListAdditionBitsinfo.Count && i != Uniq.ListSortInfo.Count)
            {
                average = average + Uniq.ListAdditionBitsinfo[i] ;
                AvgSort = AvgSort + Uniq.ListSortInfo[i];

                if (average < AvgSort)
                {
                    iSComped++;
                }
                else
                {
                    noComped++;
                }




                i++;
            }

            //UniqAvg
            int sumOfReadBits = average;
            if (Uniq.ListAdditionBitsinfo.Count != 0)
                average = average / (Uniq.ListAdditionBitsinfo.Count);
          
            int Additional = Defult - average;

            //SortAvg
            int sumOfSortBits = AvgSort;
            if(Uniq.ListSortInfo.Count!=0)
                AvgSort = AvgSort / (Uniq.ListSortInfo.Count);


            


            int LastComped = sumOfReadBits - sumOfSortBits;




            ss.Append(
                "\nMod = "+Mod.ToString()+"\n"+
                "\nDefult = " + (Defult).ToString() + " = " + (Defult / Mod).ToString() + " By = " + (Defult / 1024 / Mod).ToString() + " KB = " + ((Defult / 1024) / 1024 / Mod).ToString() + " MB " +
                "\naverage = " + average.ToString() + " = " + (average / Mod).ToString() + " By = " + (average / 1024 / Mod).ToString() + " KB = " + ((average / 1024) / 1024 / Mod).ToString() + " MB " +
                 "\nUniqAdditional = " + Additional.ToString() + " = " + (Additional / Mod).ToString() + " By = " + (Additional / 1024 / Mod).ToString() + " KB = " + ((Additional / 1024) / 1024 / Mod).ToString() + " MB " +
                 "\n\nListCount = " + (Uniq.ListAdditionBitsinfo.Count).ToString() + 


                 "\n ------------------------------- \n" +
                 "\nAvgSort = " + (AvgSort).ToString() + " = " + (AvgSort / Mod).ToString() + " By = " + (AvgSort / 1024 / Mod).ToString() + " KB = " + ((AvgSort / 1024) / 1024 / Mod).ToString() + " MB " +

                  "\n ------------------------------- \n" +

                 "\nsumOfReadBits = " + sumOfReadBits.ToString() + " = " + (sumOfReadBits / 8).ToString() + " By = " + (sumOfReadBits / 1024 / 8).ToString() + " KB = " + ((sumOfReadBits / 1024) / 1024 / 8).ToString() + " MB " +
                 "\nsumOfSortBits = " + sumOfSortBits.ToString() + " = " + (sumOfSortBits / 8).ToString() + " By = " + (sumOfSortBits / 1024 / 8).ToString() + " KB = " + ((sumOfSortBits / 1024) / 1024 / 8).ToString() + " MB " +
                  "\n\nLastComped = " + LastComped.ToString() + " = " + (LastComped / 8).ToString() + " By = " + (LastComped / 1024 / 8).ToString() + " KB = " + ((LastComped / 1024) / 1024 / 8).ToString() + " MB " +


                   "\n ------------------------------- \n" +
                   "\niSComped = "+iSComped.ToString()+
                   "\nnoComped = "+noComped.ToString()+



                   "\n\n-----------------------------------------\n" +

                "\n\n\n");



            if (MessageBox.Show(ss.ToString(), "Additional", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            RePort.Append(ss.ToString() + "\n\n\n" + sb.ToString() + "\n\n\n");
        }




        //01 MakeFile as uniq
        public void InfoMakeFileUniq_int()
        {
           // ExtensionFile = "Uf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();

             Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.infoMakeUniqOnly_int(readerFile.DataRead);
             
            }

            readerFile.CloseAll();
            GetInfo();

        }
        public void MakeFileUniq_int()
        {
 
            ExtensionFile = "Uf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.Extention = ExtensionFile;
            readerFile.BlockReaderLength = dataBlock;
            readerFile.OpenAll();

            IntBitsOperations intOpr = new IntBitsOperations(Mod);
             Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.MakeUniqOnly_int(readerFile.DataRead);
                foreach (List<int> list in Uniq.AllIntlist)
                {
                    readerFile.SaveDataByte(intOpr.GetIntsAsByteArr(list));
                }

                Uniq.AllIntlist = new List<List<int>>();
              

            }

            readerFile.CloseAll();

        }

        //02 MakeFile as uniq Simlar
        public void InfoMakeFileUniqSimlar_int()
        {
            // ExtensionFile = "USf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();

            Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.infoMakeUniqSimlar_int(readerFile.DataRead);

            }

            readerFile.CloseAll();
            GetInfo();

        }
        public void MakeFileUniqSimlar_int()
        {

            ExtensionFile = "USf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.Extention = ExtensionFile;
            readerFile.BlockReaderLength = dataBlock;
            readerFile.OpenAll();

            IntBitsOperations intOpr = new IntBitsOperations(Mod);
            Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.MakeUniqSimlar_int(readerFile.DataRead);
                foreach (List<int> list in Uniq.AllIntlist)
                {
                    readerFile.SaveDataByte(intOpr.GetIntsAsByteArr(list));
                }

                Uniq.AllIntlist = new List<List<int>>();


            }

            readerFile.CloseAll();

        }


        //03 MakeFile as uniq sort
        public void InfoMakeFileUniqSort_int()
        {
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();

            Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.infoMakeUniqSort_int(readerFile.DataRead);

            }

            readerFile.CloseAll();
             GetInfo();

        }
        public void MakeFileUniqSort_int()
        {

            ExtensionFile = "USorf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.Extention = ExtensionFile;
            readerFile.BlockReaderLength = dataBlock;
            readerFile.OpenAll();

            IntBitsOperations intOpr = new IntBitsOperations(Mod);
            Uniq = new CompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.MakeUniqSort_int(readerFile.DataRead);
                foreach (byte[] arr in Uniq.SortSaveData)
                {
                    readerFile.SaveDataByte(arr);
                }

                Uniq.SortSaveData = new List<byte[]>();


            }

            readerFile.CloseAll();

        }


        //04 MakeFile as uniq By Stop Mod
        public void InfoMakeFileUniqStop_int()
        {
            // ExtensionFile = "Uf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.BlockReaderLength = dataBlock;
            readerFile.ReadOpen();

            Uniq = new CompByMerge01Oper(Mod);
            Uniq.modLength = (Convert.ToInt32(Math.Pow(2, Mod)) / 2) - 1;

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                Uniq.infoMakeUniqByStop_int(readerFile.DataRead);

            }

            readerFile.CloseAll();
            GetInfo();

        }

    }




     class CompByMerge01Sort
    {

        private List<List<bool>> BitArrList = new List<List<bool>>();
        private List<bool> LastBitsArr;
        public byte[] SaveBitArr;

        public int NumOfBits = 0;



        public CompByMerge01Sort()
        {

            LastBitsArr = new List<bool>();
        }


         /******* MAke Sort *****************/
        public void SortIntArr(ref int[] IntDataArr)
        {
            NumOfBits = 0;
            MergeSort(ref IntDataArr, 1, IntDataArr.Length);


            //SaveBitsToArr
            {
                BitArrList.Add(LastBitsArr);
                int BitsCount = 0;
                foreach (List<bool> L in BitArrList)
                {
                    BitsCount = BitsCount + L.Count;
                }
                int NumOfBitArr = BitArrList.Count - 1;

                //for info
                NumOfBits = BitsCount - LastBitsArr.Count;

                BitArray BitSave = new BitArray(BitsCount); int SB = 0;
                while (NumOfBitArr != -1)
                {
                    foreach (bool b in BitArrList[NumOfBitArr])
                    {
                        BitSave[SB] = b; SB++;
                    }

                    NumOfBitArr--;
                }



                {
                    LastBitsArr = new List<bool>();
                    int SBits = SB - (SB % 8);
                    while (SBits != SB)
                    {
                        LastBitsArr.Add(BitSave[SBits]);
                        SBits++;
                    }

                }

                //  byte[] SaveBitArr = new byte[(BitsCount / 8) + 1];

                SaveBitArr = new byte[(BitsCount / 8) + 1];
                BitSave.CopyTo(SaveBitArr, 0);

                List<byte> DBL = SaveBitArr.ToList<byte>();
                DBL.RemoveAt(SaveBitArr.Length - 1);
                SaveBitArr = DBL.ToArray();


                BitArrList = new List<List<bool>>();

            }


        }
        public void SortIntArr( int[] IntDataArr)
        {
            NumOfBits = 0;
            MergeSort(ref IntDataArr, 1, IntDataArr.Length);


            //SaveBitsToArr
            {
                BitArrList.Add(LastBitsArr);
                int BitsCount = 0;
                foreach (List<bool> L in BitArrList)
                {
                    BitsCount = BitsCount + L.Count;
                }
                int NumOfBitArr = BitArrList.Count - 1;

                //for info
                NumOfBits = BitsCount;

                BitArray BitSave = new BitArray(BitsCount); int SB = 0;
                while (NumOfBitArr != -1)
                {
                    foreach (bool b in BitArrList[NumOfBitArr])
                    {
                        BitSave[SB] = b; SB++;
                    }

                    NumOfBitArr--;
                }



                {
                    LastBitsArr = new List<bool>();
                    int SBits = SB - (SB % 8);
                    while (SBits != SB)
                    {
                        LastBitsArr.Add(BitSave[SBits]);
                        SBits++;
                    }

                }

                //  byte[] SaveBitArr = new byte[(BitsCount / 8) + 1];

                SaveBitArr = new byte[(BitsCount / 8) + 1];
                BitSave.CopyTo(SaveBitArr, 0);

                List<byte> DBL = SaveBitArr.ToList<byte>();
                DBL.RemoveAt(SaveBitArr.Length - 1);
                SaveBitArr = DBL.ToArray();


                BitArrList = new List<List<bool>>();

            }


        }
     
        private void MergeSort(ref int[] unsortedArray, int leftIndex, int rightIndex)
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
        private void Merge(ref int[] unsortedArray, int leftIndex, int middleIndex, int rightIndex)
        {

            List<bool> BitsList = new List<bool>();
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
                        BitsList.Add(true);

                        y++;
                    }
                    else
                    {
                        BytAdd[BR] = unsortedArray[x]; BR++;
                        BitsList.Add(false);
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
                BitArrList.Add(BitsList);
            }



        }


    }










}

