using BWT.AsInt;
using Comp1.Public.Lib;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.BWT.AsInt
{
   
    public class BWTasNum01
    {
        #region Proprties File
        public StringBuilder RePort;
        private ReadWriteFile02 readerFile;
        private string Extension = "BWTint";
        private string DeExtension = "DeBWTint";
        private string KeyExtension = "BWTKey";

        private int Mod = 8;

        private int CalcModSave(int ReadDataLength, int mod)
        {
            ReadDataLength = ReadDataLength + Convert.ToInt32(Math.Pow(2, mod));
            int Timer = 0;

            while (Timer != 23)
            {
                int num = Convert.ToInt32(Math.Pow(2, Timer));

                if (ReadDataLength <= num)
                    return Timer;



                Timer++;
            }


            return Timer;
        }

        #endregion

        public BWTasNum01(int ModNum)
        {
            Mod = ModNum;
            RePort = new StringBuilder();


        }


        #region Make BWT as int

        public void StartMakeBWT_int()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);
            readerFile.ReaderF.SaveExtension = (Extension + "MD" + Mod.ToString());

            ReaderWriteFileNum02 WriterNum = new ReaderWriteFileNum02((readerFile.ReaderF.SavePathWethoutEtension + "." + KeyExtension + ModSave.ToString()), 32, false);

            readerFile.OpenAll();

            var bwt = new BWTintImplementation();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                int[] intData = IntReader.GetInt_bits(ref readerFile.DataRead).ToArray<int>();
                int[] buffer_out = new int[intData.Length];
                
                int primary_index = 0;
                bwt.bwt_encode(intData, buffer_out, intData.Length, ref primary_index);

                byte[] byteData = BitsReader.GetIntsAsByteArr(ref buffer_out);
                readerFile.SaveDataByte(ref byteData);

                WriterNum.WriteNum(primary_index);
            }

            readerFile.CloseAll();
            WriterNum.CloseFile();

        }

        #endregion

        #region Make DeBWT as int

        public void StartMakeDeBWT_int()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);
            readerFile.ReaderF.SaveExtension = (DeExtension + "MD" + Mod.ToString());

            ReaderWriteFileNum02 ReaderNum = new ReaderWriteFileNum02((readerFile.ReaderF.SavePathWethoutEtension + "." + KeyExtension + ModSave.ToString()), 32, true);

            readerFile.OpenAll();

            var bwt = new BWTintImplementation();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                int[] intData = IntReader.GetInt_bits(ref readerFile.DataRead).ToArray<int>();
                int[] buffer_out = new int[intData.Length];

                int primary_index = ReaderNum.GetNum();
                bwt.bwt_decode(intData, buffer_out, intData.Length, primary_index);

                byte[] byteData = BitsReader.GetIntsAsByteArr(ref buffer_out);
                readerFile.SaveDataByte(ref byteData);

            }

            readerFile.CloseAll();
            ReaderNum.CloseFile();

        }

        #endregion



    }


}
