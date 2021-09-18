using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.Lib
{
    class BitArrayOperation
    {
        public static BitArray intvaluToBitsArr(int value)
        {
            string s = Convert.ToString(value, 2);
            int[] bits = s.Select(c => int.Parse(c.ToString())).ToArray();
            BitArray bitarr = new BitArray(bits.Length);
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                bitarr[bits.Length - 1 - i] = Convert.ToBoolean(Convert.ToInt32(bits[i]));
            }

            return bitarr;
        }
        public static BitArray intvaluToBitsArr(int value, int ArrLength)
        {
            BitArray bitarr3 = new BitArray(ArrLength);

            int i = 0;
            while (i != ArrLength)
            {
                bitarr3[i] = Convert.ToBoolean(value & (1 << i));
                i++;
            }
            return bitarr3;
        }


    }
}
