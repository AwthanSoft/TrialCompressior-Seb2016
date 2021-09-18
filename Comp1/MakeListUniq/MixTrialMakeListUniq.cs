using Comp1.MakeListUniq.MakeListUniqByNod;
using Comp1.Public.Lib;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeListUniq
{
    public class MixTrialMakeListUniq
    {
        public StringBuilder RePort;
        private ReadWriteFile02 readerFile;
        private string Extension = "MLU";
        private string DeExtension = "MLDU";

        private int Mod = 8;

        public MixTrialMakeListUniq(int ModNum)
        {
            Mod = ModNum;
            RePort = new StringBuilder();


        }

        #region UniqByModSave

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
        //01
        public void StartUniq_1()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "N01" + "M" + Mod.ToString() + "MS" + ModSave.ToString());


            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(ModSave);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq1.MakeListUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeUniq_1()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "N01" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(ModSave);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);


                List<int> UniqInt = MakeUniq1.MakeListDeUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }

        //02
        public void StartUniq_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "N02" + "M" + Mod.ToString() + "MS" + ModSave.ToString());


            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(ModSave);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq2.MakeListUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeUniq_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "N02" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(ModSave);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);


                List<int> UniqInt = MakeUniq2.MakeListDeUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }

        //03
        public void StartUniq_3()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "N03" + "M" + Mod.ToString() + "MS" + ModSave.ToString());
            readerFile.OpenAll();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath));


            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(ModSave);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> UniqInt = MakeUniq3.MakeListUniqW1_ByStop(ref readerFile.DataRead);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }
        public void StartDeUniq_3()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "N03" + "M" + Mod.ToString() + "MS" + ModSave.ToString());
            readerFile.ReadOpen();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath), true);


            BitsToInt IntReader = new BitsToInt(ModSave);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> UniqInt = IntReader.GetInt_bits(ref readerFile.DataRead);

                MakeUniq3.MakeListDeUniqW1_ByStop(ref UniqInt);



            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }

        //04
        public void StartUniq_4()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq04 MakeUniq4 = new MakeListUniq04(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "N04" + "M" + Mod.ToString() + "MS" + ModSave.ToString());


            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(ModSave);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq4.MakeListUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeUniq_4()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq04 MakeUniq4 = new MakeListUniq04(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "N04" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(ModSave);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);


                List<int> UniqInt = MakeUniq4.MakeListDeUniqByStop(ref intData);

                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }






        #endregion


        #region MixListUniq

        //Mix 1 & 2
        public void StartUniqMix1_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);
            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);
            readerFile.ReaderF.SaveExtension = (Extension + "X1_2" + "M" + Mod.ToString() + "MS" + ModSave.ToString());


            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq1.MakeListUniqByStop(ref intData);
                List<int> DeUniqInt = MakeUniq2.MakeListDeUniqByStop(ref UniqInt);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }
        public void StartDeUniqMix1_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);
            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "X1_2" + "M" + Mod.ToString() + "MS" + ModSave.ToString());


            readerFile.OpenAll();

            BitsToInt IntReader = new BitsToInt(Mod);
            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq2.MakeListUniqByStop(ref intData);
                List<int> DeUniqInt = MakeUniq1.MakeListDeUniqByStop(ref UniqInt);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }

            readerFile.CloseAll();



        }

        //Mix 3 & 2
        public void StartUniqMix3_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X3_2" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath));

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = MakeUniq3.MakeListUniqW1_ByStop(ref readerFile.DataRead);

                List<int> DeUniqInt = MakeUniq2.MakeListDeUniqByStop(ref intData);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }
        public void StartDeUniqMix3_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "X3_2" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.ReadOpen();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath), true);

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);
            BitsToInt IntReader = new BitsToInt(Mod);
            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq2.MakeListUniqByStop(ref intData);

                MakeUniq3.MakeListDeUniqW1_ByStop(ref UniqInt);


            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }

        //Mix 3 & 1
        public void StartUniqMix3_1()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X3_1" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath));

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = MakeUniq3.MakeListUniqW1_ByStop(ref readerFile.DataRead);

                List<int> DeUniqInt = MakeUniq1.MakeListDeUniqByStop(ref intData);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }
        public void StartDeUniqMix3_1()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (DeExtension + "X3_1" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.ReadOpen();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath), true);

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);
            BitsToInt IntReader = new BitsToInt(Mod);
            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq1.MakeListUniqByStop(ref intData);

                MakeUniq3.MakeListDeUniqW1_ByStop(ref UniqInt);


            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }

        //Mix 1 & 3
        public void StartUniqMix1_3()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X1_3" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath), true);


            readerFile.ReadOpen();

            BitsToInt IntReader = new BitsToInt(Mod);
            //  IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq1.MakeListUniqByStop(ref intData);
                MakeUniq3.MakeListDeUniqW1_ByStop(ref UniqInt);



            }

            readerFile.CloseAll();
            MakeUniq3.Close();


        }

        //Mix 2 & 3
        public void StartUniqMix2_3()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X2_3" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath), true);


            readerFile.ReadOpen();

            BitsToInt IntReader = new BitsToInt(Mod);
            //  IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);

                List<int> UniqInt = MakeUniq2.MakeListUniqByStop(ref intData);
                MakeUniq3.MakeListDeUniqW1_ByStop(ref UniqInt);



            }

            readerFile.CloseAll();
            MakeUniq3.Close();


        }


        //Mix 3W2 & 1
        public void StartUniqMix3W2_1()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq01 MakeUniq1 = new MakeListUniq01(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X3_1" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath));

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = MakeUniq3.MakeListUniqW2_ByStop(ref readerFile.DataRead);

                List<int> DeUniqInt = MakeUniq1.MakeListDeUniqByStop(ref intData);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }


        //Mix 3W2 & 1
        public void StartUniqMix3W2_2()
        {
            readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;


            MakeListUniq02 MakeUniq2 = new MakeListUniq02(Mod, readerFile.ReaderF.StopNumLength);

            int ModSave = CalcModSave(readerFile.ReaderF.StopNumLength, Mod);

            readerFile.ReaderF.SaveExtension = (Extension + "X3_1" + "M" + Mod.ToString() + "MS" + ModSave.ToString());

            readerFile.OpenAll();
            ListUniqByNode01Oper MakeUniq3 = new ListUniqByNode01Oper(Mod, readerFile.ReaderF.StopNumLength, (readerFile.ReaderF.FullSaveFilePath));

            IntBitsOperations BitsReader = new IntBitsOperations(Mod);

            while (readerFile.ReadAble == true)
            {
                readerFile.ReadData();

                List<int> intData = MakeUniq3.MakeListUniqW2_ByStop(ref readerFile.DataRead);

                List<int> DeUniqInt = MakeUniq2.MakeListDeUniqByStop(ref intData);


                byte[] DataByte = BitsReader.GetIntsAsByteArr(ref DeUniqInt);

                readerFile.SaveDataByte(ref DataByte);

            }
            MakeUniq3.Close();
            readerFile.CloseAll();



        }




        #endregion


    }

}
