using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;
using Comp1.Public.Lib;
using Comp1.Public.ReaderWriterFile;
namespace Comp1.Public.Lib
{
    public   class IntBitsOperations
    {
        private  int Mod = 32;
        private int LengthArr;
        public List<BitArray> NumBits;

        private bool isMore = true;

        public List<BitArray> SaveIntBits = new List<BitArray>();
        private List<bool> RestBits = new List<bool>();
        



        public IntBitsOperations(int mod)
        {
            Mod = mod;
            if (Mod <= 0 || Mod >= 24)
            {
                isMore = true;
            }
            else
                isMore = false;

            CreateNumBits();
            
        }
        public IntBitsOperations()
        {
            if (Mod <= 0 || Mod >= 24)
            {
                isMore = true;
            }
            else
                isMore = false;

            CreateNumBits();
        }

        private void CreateNumBits()
        {
            if (isMore)
                return;
            LengthArr = Convert.ToInt32(Math.Pow(2, Mod));
            NumBits = new List<BitArray>();

            for (int i = 0; i != LengthArr; i++)
            {
                NumBits.Add(BitArrayOperation.intvaluToBitsArr(i, Mod));
            }


        }




        public BitArray GetIntsAsBitArray(ref List<int> intData)
        {
            BitArray SavBits = new BitArray(intData.Count * Mod);
            int SB = 0;
            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            return SavBits;
        }
        public BitArray GetIntsAsBitArray( List<int> intData)
        {
            BitArray SavBits = new BitArray(intData.Count * Mod);
            int SB = 0;
            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            return SavBits;
        }
        public BitArray GetIntsAsBitArray(ref int[] intData)
        {
            BitArray SavBits = new BitArray(intData.Length * Mod);
            int SB = 0;
            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            return SavBits;
        }
        public BitArray GetIntsAsBitArray( int[] intData)
        {
            BitArray SavBits = new BitArray(intData.Length * Mod);
            int SB = 0;
            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            return SavBits;
        }


        public byte[] GetIntsAsByteArr(ref List<int> intData)
        {
            if (!isMore)
            {
                BitArray SavBits = new BitArray((intData.Count * Mod) + RestBits.Count);
                int SB = 0;
                foreach (bool b in RestBits)
                {
                    SavBits[SB] = b; SB++;
                }

                foreach (int n in intData)
                {
                    foreach (bool b in NumBits[n])
                    {
                        SavBits[SB] = b; SB++;
                    }
                }

                byte[] Databyte = new byte[((SavBits.Count / 8) + 1)];
                SavBits.CopyTo(Databyte, 0);
                //SaveRest
                {
                    RestBits = new List<bool>();
                    int EndBits = SavBits.Count;
                    int StartBit = (SavBits.Count / 8) * 8;
                    while (StartBit != EndBits)
                    {
                        RestBits.Add(SavBits[StartBit]);
                        StartBit++;
                    }

                }

                List<byte> DBL = Databyte.ToList<byte>();
                DBL.RemoveAt(Databyte.Length - 1);


                return DBL.ToArray();

            }
            else
            {
                List<byte> DBL =new List<byte>();
                foreach (int n in intData)
                {
                    foreach (byte b in BitConverter.GetBytes(n))
                    {
                        DBL.Add(b);
                    }
                }

                return DBL.ToArray();
            }

           // return null;

        }
        public byte[] GetIntsAsByteArr( List<int> intData)
        {
            if (!isMore)
            {
                BitArray SavBits = new BitArray((intData.Count * Mod) + RestBits.Count);
                int SB = 0;
                foreach (bool b in RestBits)
                {
                    SavBits[SB] = b; SB++;
                }

                foreach (int n in intData)
                {
                    foreach (bool b in NumBits[n])
                    {
                        SavBits[SB] = b; SB++;
                    }
                }

                byte[] Databyte = new byte[((SavBits.Count / 8) + 1)];
                SavBits.CopyTo(Databyte, 0);
                //SaveRest
                {
                    RestBits = new List<bool>();
                    int EndBits = SavBits.Count;
                    int StartBit = (SavBits.Count / 8) * 8;
                    while (StartBit != EndBits)
                    {
                        RestBits.Add(SavBits[StartBit]);
                        StartBit++;
                    }

                }

                List<byte> DBL = Databyte.ToList<byte>();
                DBL.RemoveAt(Databyte.Length - 1);

                return DBL.ToArray();
            }
            else
            {
                List<byte> DBL = new List<byte>();
                foreach (int n in intData)
                {
                    foreach (byte b in BitConverter.GetBytes(n))
                    {
                        DBL.Add(b);
                    }
                }

                return DBL.ToArray();
            }

        }
        public byte[] GetIntsAsByteArr(ref int[] intData)
        {
            if(!isMore)
            {
            BitArray SavBits = new BitArray((intData.Length * Mod) + RestBits.Count);
            int SB = 0;
            foreach (bool b in RestBits)
            {
                SavBits[SB] = b; SB++;
            }

            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            byte[] Databyte = new byte[((SavBits.Count / 8) + 1)];
            SavBits.CopyTo(Databyte, 0);
            //SaveRest
            {
                RestBits = new List<bool>();
                int EndBits = SavBits.Count;
                int StartBit = (SavBits.Count / 8) * 8;
                while (StartBit != EndBits)
                {
                    RestBits.Add(SavBits[StartBit]);
                    StartBit++;
                }

            }

            List<byte> DBL = Databyte.ToList<byte>();
            DBL.RemoveAt(Databyte.Length - 1);

            return DBL.ToArray();
            }
            else
            {
                List<byte> DBL = new List<byte>();
                foreach (int n in intData)
                {
                    foreach (byte b in BitConverter.GetBytes(n))
                    {
                        DBL.Add(b);
                    }
                }

                return DBL.ToArray();
            }

        }
        public byte[] GetIntsAsByteArr( int[] intData)
        {
            if(!isMore)
            {
            BitArray SavBits = new BitArray((intData.Length * Mod) + RestBits.Count);
            int SB = 0;
            foreach (bool b in RestBits)
            {
                SavBits[SB] = b; SB++;
            }

            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            byte[] Databyte = new byte[((SavBits.Count / 8) + 1)];
            SavBits.CopyTo(Databyte, 0);
            //SaveRest
            {
                RestBits = new List<bool>();
                int EndBits = SavBits.Count;
                int StartBit = (SavBits.Count / 8) * 8;
                while (StartBit != EndBits)
                {
                    RestBits.Add(SavBits[StartBit]);
                    StartBit++;
                }

            }

            List<byte> DBL = Databyte.ToList<byte>();
            DBL.RemoveAt(Databyte.Length - 1);

            return DBL.ToArray();
            }
            else
            {
                List<byte> DBL = new List<byte>();
                foreach (int n in intData)
                {
                    foreach (byte b in BitConverter.GetBytes(n))
                    {
                        DBL.Add(b);
                    }
                }

                return DBL.ToArray();
            }

        }


        public byte[] GetEndIntsAsByteArr(ref List<int> intData)
        {
            intData.Add(0);

            BitArray SavBits = new BitArray((intData.Count * Mod) + RestBits.Count);
            int SB = 0;
            foreach (bool b in RestBits)
            {
                SavBits[SB] = b; SB++;
            }

            foreach (int n in intData)
            {
                foreach (bool b in NumBits[n])
                {
                    SavBits[SB] = b; SB++;
                }
            }

            byte[] Databyte = new byte[((SavBits.Count / 8) + 1)];

            SavBits.CopyTo(Databyte, 0);
            //SaveRest
            {
                RestBits = new List<bool>();
                int EndBits = SavBits.Count;
                int StartBit = (SavBits.Count / 8) * 8;
                while (StartBit != EndBits)
                {
                    RestBits.Add(SavBits[StartBit]);
                    StartBit++;
                }

            }

            List<byte> DBL = Databyte.ToList<byte>();
            DBL.RemoveAt(Databyte.Length - 1);


            return DBL.ToArray();

        }



    }
}
