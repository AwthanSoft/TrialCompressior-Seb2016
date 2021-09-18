using Comp1.Public.Lib;
using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.MakeListUniq
{
   public class MakeListUniq01
    {
       private int Mod = 8;
       List<int> ListNum;
       private int Counter = 0;

        public int Stop = 256;
        private int NumStop = 0;

       public MakeListUniq01(int ModNum)
       {
           Mod = ModNum;
           CreatListNum(ModNum);
       }
       public MakeListUniq01(int ModNum, int Stoping)
       {
           Mod = ModNum;
           CreatListNum(ModNum);
           Stop = Stoping;
       }

       public void CreatListNum(int ModNum)
       {
           NumStop = 0;
           Counter = 0;
           ListNum = new List<int>();
           Mod = ModNum;
           int Timer = Convert.ToInt32(Math.Pow(2, ModNum));
           for (int i = 0; i != Timer; i++)
           {
               ListNum.Add(i);
               Counter++;
           }
           
       }

       #region Make List Uniq

       public List<int> MakeListUniq(ref List<int> ListData)
       {
           List<int> listSave = new List<int>();

           foreach (int n in ListData)
           {
               listSave.Add(ListNum[n]);
               ListNum.Add(Counter);
               Counter++;

               ListNum.RemoveAt(n);

           }


           return listSave;

       }
       public List<int> MakeListUniq( List<int> ListData)
       {
           List<int> listSave = new List<int>();

           foreach (int n in ListData)
           {
               listSave.Add(ListNum[n]);
               ListNum.Add(Counter);
               Counter++;

               ListNum.RemoveAt(n);

           }


           return listSave;

       }
       public List<int> MakeListUniq(ref int[] ListData)
       {
           List<int> listSave = new List<int>();

           foreach (int n in ListData)
           {
               listSave.Add(ListNum[n]);
               ListNum.Add(Counter);
               Counter++;

               ListNum.RemoveAt(n);

           }


           return listSave;

       }
       public List<int> MakeListUniq( int[] ListData)
       {
           List<int> listSave = new List<int>();

           foreach (int n in ListData)
           {
               listSave.Add(ListNum[n]);
               ListNum.Add(Counter);
               Counter++;

               ListNum.RemoveAt(n);

           }


           return listSave;

       }

       public List<int> MakeListUniqByStop(ref List<int> ListData)
       {
           List<int> listSave = new List<int>();

           foreach (int n in ListData)
           {
               if (NumStop == Stop)
                   CreatListNum(Mod);

               listSave.Add(ListNum[n]);
               ListNum.Add(Counter);
               Counter++;
               NumStop++;

               ListNum.RemoveAt(n);

           }


           return listSave;

       }

       #endregion

        #region Make List DeUniq

       public List<int> MakeListDeUniq(ref List<int> ListData)
       {
           List<int> DelistSave = new List<int>();
           int Locate;
           foreach (int n in ListData)
           {
              Locate = ListNum.IndexOf(n);
              DelistSave.Add(Locate); 

               ListNum.RemoveAt(Locate);
               ListNum.Add(Counter);
               Counter++;

               
           }


           return DelistSave;

       }
       public List<int> MakeListDeUniq(List<int> ListData)
       {
           List<int> DelistSave = new List<int>();
           int Locate;
           foreach (int n in ListData)
           {
               Locate = ListNum.IndexOf(n);
               DelistSave.Add(Locate);

               ListNum.RemoveAt(Locate);
               ListNum.Add(Counter);
               Counter++;


           }


           return DelistSave;

       }
       public List<int> MakeListDeUniq(ref int[] ListData)
       {
           List<int> DelistSave = new List<int>();
           int Locate;
           foreach (int n in ListData)
           {
               Locate = ListNum.IndexOf(n);
               DelistSave.Add(Locate);

               ListNum.RemoveAt(Locate);
               ListNum.Add(Counter);
               Counter++;


           }


           return DelistSave;

       }
       public List<int> MakeListDeUniq(int[] ListData)
       {
           List<int> DelistSave = new List<int>();
           int Locate;
           foreach (int n in ListData)
           {
               Locate = ListNum.IndexOf(n);
               DelistSave.Add(Locate);

               ListNum.RemoveAt(Locate);
               ListNum.Add(Counter);
               Counter++;


           }


           return DelistSave;

       }

       public List<int> MakeListDeUniqByStop(ref List<int> ListData)
       {
           List<int> DelistSave = new List<int>();
           int Locate;
           foreach (int n in ListData)
           {
               if (NumStop == Stop)
                   CreatListNum(Mod);

               Locate = ListNum.IndexOf(n);
               DelistSave.Add(Locate);

               ListNum.RemoveAt(Locate);
               ListNum.Add(Counter);
               Counter++;

               NumStop++;

           }


           return DelistSave;

       }
        #endregion


    }
  


   public class TrialMakeListUniq01
   {
       public StringBuilder RePort;
       private ReadWriteFile02 readerFile;
       private string Extension = "MLU01";
       private string DeExtension = "MLDU01";
       private int Mod = 8;

       public TrialMakeListUniq01( int ModNum)
       {
           Mod = ModNum;
           RePort = new StringBuilder();
           
           
       }
       public void StartUniq()
       {
           MakeListUniq01 MakeUniq = new MakeListUniq01(Mod);

           readerFile = new ReadWriteFile02((Mod.ToString() + Extension));
           if (readerFile.IsCancel)
               return;

           readerFile.OpenAll();

           BitsToInt IntReader = new BitsToInt(Mod);
           IntBitsOperations BitsReader = new IntBitsOperations(Mod + 1);

           while (readerFile.ReadAble == true)
           {
               readerFile.ReadData();

               List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
               MakeUniq.CreatListNum(Mod);

               List<int> UniqInt = MakeUniq.MakeListUniq(ref intData);

               byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

               readerFile.SaveDataByte(ref DataByte);
               
           }

           readerFile.CloseAll();



       }

       public void StartDeUniq()
       {
           MakeListUniq01 MakeUniq = new MakeListUniq01(Mod);

           readerFile = new ReadWriteFile02((Mod.ToString() + DeExtension));
           if (readerFile.IsCancel)
               return;

           readerFile.ReaderF.StopNumLength = (Convert.ToInt32(Math.Pow(2, Mod)) * (Mod+1)) / 8;

           readerFile.OpenAll();

           BitsToInt IntReader = new BitsToInt(Mod + 1);
           IntBitsOperations BitsReader = new IntBitsOperations(Mod );

           while (readerFile.ReadAble == true)
           {
               readerFile.ReadData();

               List<int> intData = IntReader.GetInt_bits(ref readerFile.DataRead);
               MakeUniq.CreatListNum(Mod);

               List<int> UniqInt = MakeUniq.MakeListDeUniq(ref intData);

               byte[] DataByte = BitsReader.GetIntsAsByteArr(ref UniqInt);

               readerFile.SaveDataByte(ref DataByte);

           }

           readerFile.CloseAll();



       }





   }

}
