using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.Lib
{
    class BitsToIntNode
    {
        public BitsToIntNode nextzero;
        public BitsToIntNode nextone;
    
        public int Value;
        public bool BitValue;

        public BitsToIntNode()
        {
            Value = 0;
            BitValue = false;
        }


    }
    class BitsToIntTree
    {
        public BitsToIntNode root;
        //private int NumOfLineNode;
        private int NumOfLevels = 0;

        private int Mod = 8;

        public BitsToIntTree()
        {
            //NumOfLineNode = 0;
            //NumOfLevels = 0;
            Creat();
        }
        public BitsToIntTree(int ModLength)
        {
        //    NumOfLineNode = 0;
        //    NumOfLevels = 0;
            Mod = ModLength;
            Creat();
        }

        private void Creat()
        {
            root = new BitsToIntNode();
            for (int n = 1; n != Mod; n++)
            {

                creatLevel(this.root);
                NumOfLevels++;
            }

            creatLastLevel(this.root);
            NumOfLevels++;
        }
        private BitsToIntNode creatnodLastLine(int num)
        {

            BitsToIntNode newnod = new BitsToIntNode();
            newnod.Value = num;
        
            return newnod;
        }

        private void creatLevel(BitsToIntNode cr)
        {
            if (cr.nextzero != null)
            {
                creatLevel(cr.nextzero);
                creatLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = new BitsToIntNode();

                cr.nextone = new BitsToIntNode();

            }
        }
        private void creatLastLevel(BitsToIntNode cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, Mod));
            BitsToIntNode po = new BitsToIntNode();
            while (i != Timer)
            {
                
                BitArray bitNum = BitArrayOperation.intvaluToBitsArr(i, Mod);

                po.nextzero = root;

                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextzero.nextone == null)
                        {
                            po.nextzero.nextone = creatnodLastLine(i);
                           
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
                            po.nextzero.nextzero = creatnodLastLine(i);

                            po.nextzero = po.nextzero.nextzero;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextzero;
                        }
                    }



                }

                po.nextzero.Value = i;

                i++;
            }
        }


    }


    public class BitsToInt
    {
        private BitsToIntTree Tree;
        private int Mod = 8;

        private BitsToIntNode root;
        private BitsToIntNode po;

        public BitsToInt(int ModLength)
        {
            Mod = ModLength;


            Tree = new BitsToIntTree(Mod);
            root = Tree.root;
            po = root;
        }

        public List<int> GetInt_bits(ref byte[] DataByte)
        {
            List<int> ListInt = new List<int>();

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                if (b == true)
                {
                    if (po.nextone == null)
                    {
                        ListInt.Add(po.Value);
                        po = root.nextone;
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
                        ListInt.Add(po.Value);
                        po = root.nextzero;
                    }
                    else
                    {
                        po = po.nextzero;
                    }
                }
            }


            //Last
            {
                if (po.nextzero == null || po.nextone == null)
                {
                    ListInt.Add(po.Value);
                    po = root;
                }  
            }




            return ListInt;
        }
        public List<int> GetInt_bits( byte[] DataByte)
        {
            List<int> ListInt = new List<int>();

            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                if (b == true)
                {
                    if (po.nextone == null)
                    {
                        ListInt.Add(po.Value);
                        po = root.nextone;
                    }
                    else
                    {
                        po = root.nextone;
                    }
                }
                else
                {
                    if (po.nextzero == null)
                    {
                        ListInt.Add(po.Value);
                        po = root.nextzero;
                    }
                    else
                    {
                        po = root.nextzero;
                    }
                }
            }


            //Last
            {
                if (po.nextzero == null || po.nextone == null)
                {
                    ListInt.Add(po.Value);
                    po = root;
                }
            }




            return ListInt;
        }

    }




}
