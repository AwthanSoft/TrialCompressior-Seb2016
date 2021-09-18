using Comp1.Public.Lib;
using Comp1.Public.ReaderWriterFile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.MergeSort.Merge01
{





    class DeCompByMerge01Oper
    {
        public StringBuilder RePort;
        private byte[] BytNum = new byte[256];

        private int Mod = 8;
        private int modLength = 0;

        //Temp
        private int NumOfByte = 0;
        private int NumOfReaderint = 0;
        


        private CompByMerge01Tree Tree;
        private CompByMerge01Node root;
        private CompByMerge01Node po;
        private CompByMerge01Node TempRatharPo;
        private CompByMerge01Node ReadBitsPo = new CompByMerge01Node();

        private BitsToInt IntConv;

        /******For Save*********/
        private List<bool> AddBits = new List<bool>();
        private List<List<bool>> AllListBits = new List<List<bool>>();
        private List<bool> LastBitsArr = new List<bool>();
        public List<byte[]> AllSaveByte = new List<byte[]>();
       // private byte[] SaveBitArr;
        
     


        public DeCompByMerge01Oper()
        {
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
        public DeCompByMerge01Oper(int mod)
        {

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

            Tree = new CompByMerge01Tree(Mod);
            root = Tree.root;
            po = root;
            TempRatharPo = new CompByMerge01Node();

            modLength = Convert.ToInt32(Math.Pow(2, Mod)) - 1;
            NumOfByte = 0;
            IntConv = new BitsToInt(Mod);





            {
                NumOfByte = 0;

                Tree.Initial(Tree.root);
                root = Tree.root;
                po = root;

            }

        }



        /******** 01 Get DeUniq  **********/
        public void MakeDeUniq(ref byte[] DataByt)
        {
            List<int> IntData = IntConv.GetInt_bits(ref DataByt);
            foreach (int n in IntData)
            {
                ReadBitsPo = Tree.ListNod[n];

                //Error 01
                if(ReadBitsPo.Value!=n)
                    MessageBox.Show("Error (Exmine 01) ! ");
                    

                ReadTree();
            }
            
          
        }



        /******** Get DeUniq ***********/
        private void InitialTree()
        {
            Tree = new CompByMerge01Tree(Mod);
            root = Tree.root;
            po = root;
            TempRatharPo = new CompByMerge01Node();

        }
        private void ReadTree()
        {

            NumOfReaderint++;

            if (NumOfByte == modLength)
            {
            //    InitialTree();
                NumOfByte = 0;

             //   Tree.MainInitial(Tree.root);
                Tree.Initial(Tree.root);
                ////   Tree.InitialWay2();
                root = Tree.root;
                po = Tree.root;
                root.TempRefrance = null;
                SaveBits();

                return;
            }


                AddBits = new List<bool>();
                po = ReadBitsPo;

                while (ReadBitsPo.TempRefrance != null)
                {
                    AddBits.Add(ReadBitsPo.TempPastBit);
                    ReadBitsPo = ReadBitsPo.TempRefrance;
                }

              

                //if (po.TempPastBit == false)

                if (po.TempRefrance.Tempnextzero == po)
                {
                        if (po.TempRefrance.TempRefrance == null)
                        {
                            root = po.TempRefrance.Tempnextone;
                            root.TempRefrance = null;
                        }
                        else
                        {
                            TempRatharPo = po.TempRefrance.Tempnextone;
                            TempRatharPo.TempPastBit = po.TempRefrance.TempPastBit;

                            po = po.TempRefrance;
                            TempRatharPo.TempRefrance = po.TempRefrance;


                            if (po.TempPastBit == false)
                            {
                                po.TempRefrance.Tempnextzero = TempRatharPo;
                            }
                            else
                            {
                                po.TempRefrance.Tempnextone = TempRatharPo;
                            }


                            //po.TempRefrance.Tempnextone.TempPastBit = po.TempRefrance.TempPastBit;

                            //po = po.TempRefrance;
                            //TempRatharPo = po.Tempnextone;

                            //TempRatharPo.TempRefrance = po.TempRefrance;

                            //if (po.TempRefrance.Tempnextzero == po)
                            //{
                            //    po.TempRefrance.Tempnextzero = TempRatharPo;
                            //}
                            //else
                            //{
                            //    po.TempRefrance.Tempnextone = TempRatharPo;
                            //}

                          

                        }
                    

                }
                else
                {
                    if (po.TempRefrance.nextone != po)
                        MessageBox.Show("Error (Exmine 02) ! " + "\nNumOfReaderint = " + NumOfReaderint.ToString());
                    
                        if (po.TempRefrance.TempRefrance == null)
                        {
                            root = po.TempRefrance.nextzero;
                            root.TempRefrance = null;
                        }
                        else
                        {
                            TempRatharPo = po.TempRefrance.nextzero;
                            TempRatharPo.TempPastBit = po.TempRefrance.TempPastBit;

                            po = po.TempRefrance;
                            TempRatharPo.TempRefrance = po.TempRefrance;


                            if (po.TempPastBit == false)
                            {
                                po.TempRefrance.Tempnextzero = TempRatharPo;
                            }
                            else
                            {
                                po.TempRefrance.Tempnextone = TempRatharPo;
                            }




                            //po.TempRefrance.Tempnextzero.TempPastBit = po.TempRefrance.TempPastBit;

                            //po = po.TempRefrance;
                            //TempRatharPo = po.Tempnextzero;

                            //TempRatharPo.TempRefrance = po.TempRefrance;

                            //if (po.TempRefrance.Tempnextzero == po)
                            //{
                            //    po.TempRefrance.Tempnextzero = TempRatharPo;
                            //}
                            //else
                            //{
                            //    po.TempRefrance.Tempnextone = TempRatharPo;
                            //}

                        
                    }
                }


                AllListBits.Add(AddBits);
                NumOfByte++;
            

        }
        private void SaveBits()
        {
            //SaveBitsToArr
            {


                int BitsCount = 0;
                foreach (List<bool> ListB in AllListBits)
                {
                    BitsCount = BitsCount + ListB.Count;

                }
                BitsCount = BitsCount + LastBitsArr.Count;


                BitArray BitSave = new BitArray(BitsCount); int SB = 0;

                //SaveLastBits
                {
                    foreach (bool b in LastBitsArr)
                    {
                        BitSave[SB] = b;
                        SB++;
                    }

                }


                int NumOfList = 0;
                foreach (List<bool> ListB in AllListBits)
                {
                    NumOfList = ListB.Count - 1;
                    while (NumOfList != -1)
                    {
                        BitSave[SB] = ListB[NumOfList];
                        SB++;
                        NumOfList--;
                    }

                }


                LastBitsArr = new List<bool>();
                int SBits = SB - 1;
                //02Prop
                int EndBits = SB - (SB % 8) - 1;
                while (SBits != EndBits)
                {
                    LastBitsArr.Add(BitSave[SBits]);
                    SBits--;
                }

                AllListBits = new List<List<bool>>();
                //  AllListBits.Add(LastBitsArr);


              byte[]  SaveBitArr = new byte[(BitsCount / 8) + 1];

                BitSave.CopyTo(SaveBitArr, 0);

                List<byte> BDL = SaveBitArr.ToList<byte>();
                BDL.RemoveAt(SaveBitArr.Length - 1);
                SaveBitArr = BDL.ToArray();

                AllSaveByte.Add(SaveBitArr);


            }

        }






    }

    public class DeCompByMerge01
    {
        public StringBuilder RePort;

        private ReadWriteFile00 readerFile;
        private int dataBlock = 1024 * 4;
        private string ExtensionFile = "MergeUniq";
        private DeCompByMerge01Oper DeOper;
        private int Mod = 8;



        public DeCompByMerge01()
        {
           
            RePort = new StringBuilder();

        }
        public DeCompByMerge01(int modlength)
        {
            RePort = new StringBuilder();
            Mod = modlength;

            //

        }




        //01 DeMakeFile as uniq
        public void MakeFileDeUniq_int()
        {

            ExtensionFile = "DeUf" + Mod.ToString();
            readerFile = new ReadWriteFile00(ExtensionFile);
            readerFile.Extention = ExtensionFile;
            readerFile.BlockReaderLength = dataBlock;
            readerFile.OpenAll();

           // BitsToInt IntConv = new BitsToInt(Mod);
            DeOper = new DeCompByMerge01Oper(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                DeOper.MakeDeUniq(ref readerFile.DataRead);

                foreach (byte[] listByte in DeOper.AllSaveByte)
                {

                    readerFile.SaveDataByte(listByte);
              
                }
                DeOper.AllSaveByte = new List<byte[]>();

            }

            readerFile.CloseAll();

        }





    }





}
