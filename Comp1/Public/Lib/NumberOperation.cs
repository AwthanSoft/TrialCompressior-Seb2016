using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1
{
    public class numoperation
    {
        public static byte int32toByte1(int valu)
        {
            byte[] bytenum = new byte[1];
            bytenum = BitConverter.GetBytes(valu);
            return bytenum[0];
        }

    }
}
