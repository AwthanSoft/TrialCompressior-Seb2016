using BWT;
using Comp1.Public.ReaderFile.ReaderWriteFile02;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.BWT.AsByte
{
    public class BWTasByte01
    {
        #region Proprties File
        public StringBuilder RePort;
        private ReadWriteFile02 readerFile;
        private string Extension = "BWTbyte";
        private string DeExtension = "DeBWTbyte";
        private string KeyExtension = "BWTKey";


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

        public BWTasByte01()
        {
            RePort = new StringBuilder();

        }


        #region Make BWT as Byte

        public void StartMakeBWT_Byte()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, 8);
            readerFile.ReaderF.SaveExtension = (Extension +"MS" + ModSave.ToString());

            ReaderWriteFileNum02 WriterNum = new ReaderWriteFileNum02((readerFile.ReaderF.SavePathWethoutEtension + "."+KeyExtension +ModSave.ToString()), ModSave ,false);

            readerFile.OpenAll();
            
            var bwt = new BWTImplementation();

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();
                
                byte[] buffer_out = new byte[readerFile.DataRead.Length];
                int primary_index = 0;
                bwt.bwt_encode(readerFile.DataRead, buffer_out, readerFile.DataRead.Length, ref primary_index);

                readerFile.SaveDataByte(ref buffer_out);
                WriterNum.WriteNum(primary_index);
            }

            readerFile.CloseAll();
            WriterNum.CloseFile();

        }

        #endregion

        #region Make DeBWT as Byte

        public void StartMakeDeBWT_Byte()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, 8);
            readerFile.ReaderF.SaveExtension = (DeExtension + "MS" + ModSave.ToString());

            ReaderWriteFileNum02 ReaderNum = new ReaderWriteFileNum02((readerFile.ReaderF.ReaderPathWethoutEtension + "." + KeyExtension + ModSave.ToString()), ModSave, true);


            readerFile.OpenAll();
            var bwt = new BWTImplementation();

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                byte[] buffer_out = new byte[readerFile.DataRead.Length];
                int primary_index = ReaderNum.GetNum();
                bwt.bwt_decode(readerFile.DataRead, buffer_out, readerFile.DataRead.Length, primary_index);

                readerFile.SaveDataByte(ref buffer_out);
            }

            readerFile.CloseAll();
            ReaderNum.CloseFile();

        }

        #endregion



    }
}
