using System;
using System.Collections.Generic;
using System.Text;

namespace BWT
{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            const string str = "Better ask the way than to go astray!";

//            byte[] buffer_in = Encoding.UTF8.GetBytes(str);
//            byte[] buffer_out = new byte[buffer_in.Length];
//            byte[] buffer_decode = new byte[buffer_in.Length];

//            BWTImplementation bwt = new BWTImplementation();

//            int primary_index = 0;
//            bwt.bwt_encode(buffer_in, buffer_out, buffer_in.Length, ref primary_index);
//            bwt.bwt_decode(buffer_out, buffer_decode, buffer_in.Length, primary_index);

//            Console.WriteLine("Decoded string: {0}", Encoding.UTF8.GetString(buffer_decode));
//        }
//    }

    class BWTImplementation
    {
        public void bwt_encode(byte[] buf_in, byte[] buf_out, int size, ref int primary_index)
        {
            int[] indices = new int[size];
            for (int i = 0; i < size; i++)
                indices[i] = i;

            Array.Sort(indices, 0, size, new BWTComparator(buf_in, size));

            for (int i = 0; i < size; i++)
                buf_out[i] = buf_in[(indices[i] + size - 1) % size];

            for (int i = 0; i < size; i++)
            {
                if (indices[i] == 1)
                {
                    primary_index = i;
                    return;
                }
            }
        }

        public void bwt_decode(byte[] buf_encoded, byte[] buf_decoded, int size, int primary_index)
        {
            byte[] F = new byte[size];
            int[] buckets = new int[0x100];
            int[] indices = new int[size];

            for (int i = 0; i < 0x100; i++)
                buckets[i] = 0;

            for (int i = 0; i < size; i++)
                buckets[buf_encoded[i]]++;

            for (int i = 0, k = 0; i < 0x100; i++)
            {
                for (int j = 0; j < buckets[i]; j++)
                {
                    F[k++] = (byte)i;
                }
            }

            for (int i = 0, j = 0; i < 0x100; i++)
            {
                while (i > F[j] && j < size - 1)
                {
                    j++;
                }
                buckets[i] = j;
            }

            for (int i = 0; i < size; i++)
                indices[buckets[buf_encoded[i]]++] = i;

            for (int i = 0, j = primary_index; i < size; i++)
            {
                buf_decoded[i] = buf_encoded[j];
                j = indices[j];
            }
        }
    }

    class BWTComparator : IComparer<int>
    {
        private byte[] rotlexcmp_buf = null;
        private int rottexcmp_bufsize = 0;

        public BWTComparator(byte[] array, int size)
        {
            rotlexcmp_buf = array;
            rottexcmp_bufsize = size;
        }

        public int Compare(int li, int ri)
        {
            int ac = rottexcmp_bufsize;
            while (rotlexcmp_buf[li] == rotlexcmp_buf[ri])
            {
                if (++li == rottexcmp_bufsize)
                    li = 0;
                if (++ri == rottexcmp_bufsize)
                    ri = 0;
                if (--ac <= 0)
                    return 0;
            }
            if (rotlexcmp_buf[li] > rotlexcmp_buf[ri])
                return 1;

            return -1;
        }
    }
}
