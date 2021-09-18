using Comp1.Public.ReaderFile.ReaderWriteFile02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp1.MakeListUniq.MakeListUniqByNod
{
    class ListUniqByNode01Node
    {
        public ListUniqByNode01Node nextzero;
        public ListUniqByNode01Node nextone;
        public ListUniqByNode01Node Refrance;
        public int Value;
        public bool PastBit = false;
        public bool Continu;
        public bool isTemp = false;
        public BitArray BitsArr;

        public ListUniqByNode01Node Tempnextzero;
        public ListUniqByNode01Node Tempnextone;
        public ListUniqByNode01Node TempRefrance;
        public int TempValue;
        public bool TempPastBit = false;

        public bool TempContinu;
        public bool TempisTemp = false;

        public ListUniqByNode01Node()
        {
            TempValue = 0;
            Value = 0;
            Continu = false;
            TempContinu = false;
            nextzero = null;
            nextone = null;
        }


        //01
        //public int Changeing(int TempingValue)
        //{
        //    bool isTemp = this.TempisTemp;
        //    int TempVal = this.TempValue;

        //    if (this.TempPastBit)
        //    {
        //        this.TempisTemp = !isTemp;
        //        this.TempValue = TempingValue;

        //        this.TempRefrance.Tempnextzero.TempisTemp = isTemp;
        //        this.TempRefrance.Tempnextzero.TempValue = TempingValue;
        //    }
        //    else
        //    {
        //        this.TempisTemp = !isTemp;
        //        this.TempValue = TempingValue;

        //        this.TempRefrance.nextone.TempisTemp = isTemp;
        //        this.TempRefrance.nextone.TempValue = TempingValue;
        //    }
        //    return TempVal;

        //}
        //02
        public int Changeing2(int TempingValue)
        {
            int TempVal = this.TempValue;


            this.TempRefrance.Tempnextzero.TempValue = TempingValue;
            this.TempRefrance.nextone.TempValue = TempingValue;
            
            return TempVal;

        }
        //03
        public int Changeing(int TempingValue)
        {
            bool isTemp = this.TempisTemp;
            int TempVal = this.TempValue;


            if (!isTemp)
            {
                this.TempRefrance.Tempnextzero.TempValue = TempingValue;
                this.TempRefrance.nextone.TempValue = TempingValue;
            
            }
            else
            {
                if (this.TempPastBit)
                {
                    this.TempisTemp = !isTemp;
                    this.TempValue = TempingValue;

                    this.TempRefrance.Tempnextzero.TempisTemp = isTemp;
                    this.TempRefrance.Tempnextzero.TempValue = TempingValue;
                }
                else
                {
                    this.TempisTemp = !isTemp;
                    this.TempValue = TempingValue;

                    this.TempRefrance.nextone.TempisTemp = isTemp;
                    this.TempRefrance.nextone.TempValue = TempingValue;
                }
            }

            return TempVal;

        }




    }
    class ListUniqByNode01SaveKeyBits
    {
        #region  File Proprties

        private string Extension = "LUBN01KBits";
        private string GetPath
        {
            get { return Path.ChangeExtension(Pathfile, Extension); }

        }


        private bool RestBitsExist = false;
        private int AddingBits = 0;
        private bool ReaderMod = false;
        private bool fileIsOpen = false;
        private FileStream filing;
        private string Pathfile;


        private void ImportFilePath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse Files";


            // openFileDialog1.InitialDirectory = @"C:\Users\ALI\Desktop";

            openFileDialog1.DefaultExt = "txt";

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "Comma-Delimited Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;



            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowReadOnly = true;

            openFileDialog1.ShowDialog();



            Pathfile = openFileDialog1.FileName;
            if (Pathfile != null)
                Extension = Path.GetExtension(Pathfile);


            foreach (String file in openFileDialog1.FileNames)
            {
                Pathfile = file;


            }

            /*  DialogResult result = openFileDialog1.ShowDialog();
              if ( result == DialogResult.OK)
              {
                  OpenSomeFile(openFileDialog1.FileName);
              }*/
        }
        public void OpenFile()
        {
            if (fileIsOpen == false)
            {
                if (Pathfile == null)
                    ImportFilePath();


                if (!Directory.Exists(Path.GetDirectoryName(GetPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(GetPath));

                filing = new FileStream(GetPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

                fileIsOpen = true;

                if (ReaderMod)
                {
                    long FileSize = filing.Length;


                    if (RestBitsExist)
                    {
                        //There MayBe is Exeption

                        filing.Seek(FileSize - 1, SeekOrigin.Begin);

                        byte[] Restbits = new byte[1];

                        AddingBits = Convert.ToInt32(Restbits[0]);
                        if (AddingBits == 0)
                            FileSize = FileSize - 1;
                        else
                            FileSize = FileSize - 2;


                    }

                    ProcessTimer1 = Convert.ToInt32(FileSize / ReaderDataLength);
                    ProcessTimer2 = Convert.ToInt32(FileSize % ReaderDataLength);
                    ReadAble = true;

                    if (RestBitsExist && AddingBits != 0)
                        ProcessTimer2 = ProcessTimer2 + 1;

                    filing.Seek(0, SeekOrigin.Begin);
                }
                else
                    filing.Seek(filing.Length, SeekOrigin.Begin);

            }

        }
        public void CloseFile()
        {
            if (fileIsOpen != false)
            {
                SaveEndBitArr();
                filing.Close();
            }

        }

        #endregion

        #region  Data Save


        /********* Data  **************/
        private int SB = 1024 * 8;
        private int BitArrayLength = 1024 * 8;
        private BitArray BitsArrSave = new BitArray(0);

        public void WriteBit(bool bit)
        {
            if (SB == BitArrayLength)
            {
                SaveAllBitArray();

            }

            BitsArrSave[SB] = bit;
            SB++;

        }
        private void SaveAllBitArray()
        {
            if (fileIsOpen != false)
            {
                OpenFile();
            }

            byte[] SaveBitArr = new byte[(BitsArrSave.Length / 8)];

            BitsArrSave.CopyTo(SaveBitArr, 0);
            filing.Write(SaveBitArr, 0, BitsArrSave.Length / 8);
            SB = 0;
            BitsArrSave = new BitArray(BitArrayLength, false);

        }
        private void SaveEndBitArr()
        {
            if (fileIsOpen != false)
            {
                OpenFile();
            }


            {

                byte[] SaveBitArr = new byte[(BitArrayLength / 8) + 1];
                this.BitsArrSave.CopyTo(SaveBitArr, 0);
                filing.Write(SaveBitArr, 0, SB / 8);
                List<bool> TempListBits = new List<bool>();

                //SaveLast
                {
                    {

                        TempListBits = new List<bool>();
                        int SBits = SB - (SB % 8);
                        while (SBits != SB)
                        {
                            TempListBits.Add(this.BitsArrSave[SBits]);
                            SBits++;
                        }

                        int LastBitsNum = TempListBits.Count;

                        if (LastBitsNum != 0)
                        {
                            byte[] SaveLastBit = new byte[2];
                            BitArray lastBitsArr = new BitArray(8);
                            int i = 0;
                            foreach (bool b in TempListBits)
                            {
                                lastBitsArr[i] = b;
                                i++;
                            }
                            lastBitsArr.CopyTo(SaveLastBit, 0);
                            SaveLastBit[1] = numoperation.int32toByte1(LastBitsNum);

                            filing.Write(SaveBitArr, 0, 2);
                        }
                        else
                        {
                            byte[] SaveLastBit = new byte[1];
                            SaveLastBit[0] = numoperation.int32toByte1(LastBitsNum);

                            filing.Write(SaveBitArr, 0, 1);
                        }


                    }
                }


                //BitArray BitsArr = new BitArray(ArrayLength);
                //int sb = 0;

                //foreach (bool b in TempListBits)
                //{
                //    BitsArr[sb] = b;
                //    sb++;
                //}






            }




        }


        #endregion

        #region  Data Read


        /********* Info  **************/
        private bool ReadAble = false;
        private int ProcessTimer1 = 0;
        private int ProcessTimer2 = 0;
        private int Process1 = 0;

        /********* Data  **************/
        private int RB = 0;
        private int ReaderDataLength = 1024;
        private int ArrayReadLength = 0;
        private BitArray BitsArrRead = new BitArray(0);

        /******************************/
        private void ReadData()
        {
            byte[] DataRead;
            if (ReadAble == true)
            {
                if (Process1 != ProcessTimer1)
                {
                    DataRead = new byte[ReaderDataLength];
                    filing.Read(DataRead, 0, ReaderDataLength);

                    Process1++;
                    BitsArrRead = new BitArray(DataRead);
                    ArrayReadLength = BitsArrRead.Count;

                }
                else
                {
                    DataRead = new byte[ProcessTimer2];
                    filing.Read(DataRead, 0, ProcessTimer2);
                    ReadAble = false;

                    BitsArrRead = new BitArray(DataRead);
                    ArrayReadLength = BitsArrRead.Count - AddingBits;



                }

            }
            else
            {
                DataRead = new byte[0];


            }

            RB = 0;

        }
        public bool GetBit()
        {
            if (RB == ArrayReadLength)
            {
                ReadData();
            }

            RB++;
            return BitsArrRead[RB - 1];


        }


        #endregion


        public ListUniqByNode01SaveKeyBits(string MainPathFile)
        {
            Pathfile = MainPathFile;
        }
        public ListUniqByNode01SaveKeyBits(string MainPathFile, bool IsReaderMod)
        {
            Pathfile = MainPathFile;
            ReaderMod = IsReaderMod;
        }
        public ListUniqByNode01SaveKeyBits(string MainPathFile, bool IsReaderMod, bool RestBitsIsExist)
        {
            Pathfile = MainPathFile;
            ReaderMod = IsReaderMod;

            RestBitsExist = RestBitsIsExist;
        }


    }

    class ListUniqByNode01Tree
    {
        public ListUniqByNode01Node root;
        private int NumOfLineNode;
        private int NumOfLevels;

        public List<ListUniqByNode01Node> ListNod;

        private int lengthMod = 8;


        public ListUniqByNode01Tree()
        {
            root = new ListUniqByNode01Node();
            NumOfLevels = 0;
            NumOfLineNode = 0;


        }

        public ListUniqByNode01Tree(int LengthMod)
        {
            root = new ListUniqByNode01Node();
            NumOfLevels = 0;
            NumOfLineNode = 0;
            lengthMod = LengthMod;

            creat(LengthMod);

        }

        private ListUniqByNode01Node createnod()
        {
            return new ListUniqByNode01Node();
        }
        private ListUniqByNode01Node createnodLastLine(int value)
        {
            ListUniqByNode01Node newnod = new ListUniqByNode01Node();
            newnod.Value = value;
            newnod.Continu = true;

            return newnod;
        }

        public void creat(int num)
        {
            if (num <= 0)
                return;
            lengthMod = new int();
            lengthMod = num;
            for (int n = 1; n != num; n++)
            {
                NumOfLineNode = 0;
                creatLevel(this.root);
                NumOfLevels++;
            }

            NumOfLineNode = 0;
            creatLastLevel(this.root);
            NumOfLevels++;
            NumOfLineNode = 0;

            creatTempLevel(this.root);


            MainInitial(this.root);
            this.root.Refrance = null;
            this.root.TempRefrance = null;
            NumOfLineNode = 0;
            

            // AddNodToList(this.root);


        }
        public void creatList()
        {
            ListNod = new List<ListUniqByNode01Node>();

             AddNodToList(this.root);


        }

        private void creatLevel(ListUniqByNode01Node cr)
        {
            if (cr.nextzero != null)
            {
                creatLevel(cr.nextzero);
                creatLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = createnod();
              
                cr.nextone = createnod();
             

            }
        }
        private void creatLastLevel(ListUniqByNode01Node cr)
        {
            int i = 0;
            int Timer = Convert.ToInt32(Math.Pow(2, lengthMod));
            ListUniqByNode01Node po = new ListUniqByNode01Node();
            while (i != Timer)
            {
                BitArray bitNum = Comp1.Public.Lib.BitArrayOperation.intvaluToBitsArr(i, lengthMod);

                po.nextzero = root;

                foreach (bool b in bitNum)
                {
                    if (b == true)
                    {
                        if (po.nextzero.nextone == null)
                        {
                            po.nextzero.nextone = createnod();
                            
                            po.nextzero = po.nextzero.nextone;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextone;
                        }


                        po.nextzero.PastBit = true;
                    }
                    else
                    {
                        if (po.nextzero.nextzero == null)
                        {
                            po.nextzero.nextzero = createnod();
                            
                            po.nextzero = po.nextzero.nextzero;
                        }
                        else
                        {
                            po.nextzero = po.nextzero.nextzero;
                        }

                        po.nextzero.PastBit = false;
                    }



                }
                po.nextzero.Value = i;
                po.nextzero.Continu = true;
                po.nextzero.BitsArr = bitNum;
             //   ListNod.Add(po.nextzero);



                i++;
            }
        }
        private void creatTempLevel(ListUniqByNode01Node cr)
        {
            if (cr.nextzero != null)
            {
                creatTempLevel(cr.nextzero);
                creatTempLevel(cr.nextone);
            }
            else
            {
                cr.nextzero = createnod();
                cr.nextzero.Value = cr.Value;
                cr.nextzero.isTemp = true;
                cr.nextzero.PastBit = false;

                cr.nextone = createnod();
                cr.nextone.Value = cr.Value;
                cr.nextone.isTemp = false ;
                cr.nextone.PastBit = true ;


            }
        }

        private void MainInitial(ListUniqByNode01Node cr)
        {


            if (cr.nextzero != null && cr.nextone != null)
            {
                cr.nextzero.Refrance = cr;
                cr.nextone.Refrance = cr;

                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;

                cr.TempContinu = cr.Continu;

                cr.TempPastBit = cr.PastBit;

                cr.TempValue = cr.Value;

                cr.TempisTemp = cr.isTemp;

                MainInitial(cr.nextzero);
                MainInitial(cr.nextone);
            }
            else
            {
                cr.nextzero = null;
                cr.nextone = null;


                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;

                cr.TempValue = cr.Value;

                cr.TempPastBit = cr.PastBit;

                cr.TempContinu = cr.Continu;
                cr.TempisTemp = cr.isTemp;


                ////NextZero
                //cr.nextzero.Tempnextzero = cr.nextzero.nextzero;
                //cr.nextzero.Tempnextone = cr.nextzero.nextone;
                //cr.nextzero.TempRefrance = cr.nextzero.Refrance;

                //cr.nextzero.TempValue = cr.nextzero.Value;

                //cr.nextzero.TempPastBit = cr.nextzero.PastBit;

                //cr.nextzero.TempContinu = cr.nextzero.Continu;
                //cr.nextzero.TempisTemp = cr.nextzero.isTemp;

                ////NextOne
                //cr.nextone.Tempnextzero = cr.nextone.nextzero;
                //cr.nextone.Tempnextone = cr.nextone.nextone;
                //cr.nextone.TempRefrance = cr.nextone.Refrance;

                //cr.nextone.TempValue = cr.nextone.Value;

                //cr.nextone.TempPastBit = cr.nextone.PastBit;

                //cr.nextone.TempContinu = cr.nextone.Continu;
                //cr.nextone.TempisTemp = cr.nextone.isTemp;

            }
        }

        public void Initial(ListUniqByNode01Node cr)
        {
            if (cr.nextzero != null)
            {
                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;
                cr.TempRefrance = cr.Refrance;
                cr.TempContinu = cr.Continu;

                cr.TempPastBit = cr.PastBit;
                cr.TempValue = cr.Value;

                cr.TempisTemp = cr.isTemp;

                Initial(cr.nextzero);
                Initial(cr.nextone);
            }
            else
            {

                cr.TempRefrance = cr.Refrance;
                cr.TempValue = cr.Value;
                cr.TempContinu = cr.Continu;
                cr.TempPastBit = cr.PastBit;

                cr.Tempnextzero = cr.nextzero;
                cr.Tempnextone = cr.nextone;


                cr.TempisTemp = cr.isTemp;
            }
        }

        private void AddNodToList(ListUniqByNode01Node cr)
        {

            if (!cr.Continu)
            {
                AddNodToList(cr.nextzero);
                AddNodToList(cr.nextone);
            }
            else
                ListNod.Add(cr.nextzero);
        }



        public ListUniqByNode01Node FindNum(int Num)
        {
            for (int i = 0; i != ListNod.Count; i++)
            {
                if (ListNod[i].TempValue == Num)
                {
                    return ListNod[i];
                }
            }

            return null;
        }



        







    }

    class ListUniqByNode01Oper
    {
        public StringBuilder RePort;

        #region  TreeProprties
        private bool isDeUniq = false;

        private int Mod = 8;
        private ListUniqByNode01Tree Tree;
        private ListUniqByNode01Node root;
        private ListUniqByNode01Node po;
       

        #endregion

        private string FilePath;
        private ListUniqByNode01SaveKeyBits SaveBitKey;
        
        private int StopSize;
        private int Stoping = 0;
        private int Counter = 256;


        private void IsStoping()
        {
            if (Stoping == StopSize)
            {
                Tree.Initial(Tree.root);
                this.root = Tree.root;
                this.po = this.root;

                Counter = Convert.ToInt32(Math.Pow(2, Mod));
                Stoping = 0;
            }

        }
        public void Close()
        {
           if(SaveBitKey!=null)
               SaveBitKey.CloseFile();
      
            if (BitsWriter!=null)
                BitsWriter.CloseFile();

            if (NumReaderWriter != null)
                NumReaderWriter.CloseFile();
        }
        private void MakeTree()
        {
            Tree = new ListUniqByNode01Tree(Mod);

            root = Tree.root;
            po = root;
            
            if (isDeUniq)
            {
                Tree.creatList();
                BitsWriter = new ReaderWriteFileBits02(FilePath, false);
                SaveBitKey = new ListUniqByNode01SaveKeyBits(FilePath, true, true);
            }
            else
            {
                SaveBitKey = new ListUniqByNode01SaveKeyBits(FilePath, false);
            }
            SaveBitKey.OpenFile();


        }

        public ListUniqByNode01Oper()
        {
            RePort = new StringBuilder();
            Mod = 8;
            MakeTree();
        }
        public ListUniqByNode01Oper(int mod ,int StopingSize ,string MainFilePath )
        {
            RePort = new StringBuilder();
            Mod = mod;
            Counter = Convert.ToInt32(Math.Pow(2, Mod));
            StopSize = StopingSize;

            FilePath =MainFilePath;

            MakeTree();

        }
        public ListUniqByNode01Oper(int mod, int StopingSize, string MainFilePath, bool isDeUniqMod)
        {
            RePort = new StringBuilder();
            Mod = mod;
            Counter = Convert.ToInt32(Math.Pow(2, Mod));
            StopSize = StopingSize;
            isDeUniq = isDeUniqMod;
            FilePath = MainFilePath;

            MakeTree();

           
              

        }


        //Way 01
        #region Way 01
        public List<int> MakeListUniqW1_ByStop(ref byte[] DataByte)
        {
             List<int> IntList=new List<int>();
            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                if (b == false)
                {
                    if (po.TempContinu)
                    {
                        SaveBitKey.WriteBit(po.Tempnextzero.TempisTemp);

                        //00
                        IntList.Add(po.Tempnextzero.Changeing(Counter));
                      //  IntList.Add(po.Tempnextzero.Changeing2(Counter));
                        Counter++;
                        Stoping++;
                        po = root;

                        IsStoping();
                    }
                    else
                    {
                        po = po.Tempnextzero;
                    }
                }
                else
                {
                    if (po.TempContinu)
                    {
                        SaveBitKey.WriteBit(po.Tempnextone.TempisTemp);
                        //00
                        //IntList.Add(po.Tempnextone.Changeing(Counter));
                        IntList.Add(po.Tempnextone.Changeing2(Counter));
                        Counter++;
                        Stoping++;
                        po = root;

                        IsStoping();
                    }
                    else
                    {
                        po = po.Tempnextone;
                    }
                }


            }


            return IntList;

        }
        #region Properties For DeUniq

        private ReaderWriteFileBits02 BitsWriter;

        public void MakeListDeUniqW1_ByStop(ref List<int> DataNum)
        {
            foreach (int n in DataNum)
            {
                po = Tree.FindNum(n);

                foreach (bool b in po.TempRefrance.BitsArr)
                {
                    BitsWriter.WriteBit(b);
                }

                po = po.TempRefrance;

                if (SaveBitKey.GetBit())
                {
                    if (po.Tempnextzero.TempisTemp)
                    {
                        BitsWriter.WriteBit(false);
                        po.Tempnextzero.Changeing(Counter);
                    }
                    else
                    {
                        BitsWriter.WriteBit(true);
                        po.nextone.Changeing2(Counter);
                    }
                }
                else
                {
                    if (po.Tempnextzero.TempisTemp)
                    {
                        BitsWriter.WriteBit(true);
                        po.nextone.Changeing2(Counter);
                    }
                    else
                    {
                        BitsWriter.WriteBit(false);
                        po.Tempnextzero.Changeing2(Counter);
                    }
                }


                Counter++;
                Stoping++;
                po = root;

                IsStoping();
            }









        }


        #endregion

        #endregion

        #region Way 02

        ReaderWriteFileNum02 NumReaderWriter;
        
        public List<int> MakeListUniqW2_ByStop(ref byte[] DataByte)
        {
            if(NumReaderWriter==null)
                NumReaderWriter=new ReaderWriteFileNum02(FilePath+"TnF",Mod ,false);

            List<int> IntList = new List<int>();
            BitArray BitsArr = new BitArray(DataByte);

            foreach (bool b in BitsArr)
            {
                if (b == false)
                {
                    if (po.TempContinu)
                    {
                        if (po.Tempnextzero.TempisTemp)
                        {
                            SaveBitKey.WriteBit(true);
                            NumReaderWriter.WriteNum(po.nextzero.Value);
                        }
                        else
                        {
                            SaveBitKey.WriteBit(false);
                            
                            //00
                            IntList.Add(po.Tempnextzero.Changeing(Counter));
                            //  IntList.Add(po.Tempnextzero.Changeing2(Counter));
                            Counter++;
                            Stoping++;
                        }

                        
                       
                       
                        
                        po = root;

                        IsStoping();
                    }
                    else
                    {
                        po = po.Tempnextzero;
                    }
                }
                else
                {
                    if (po.TempContinu)
                    {
                        if (po.Tempnextone.TempisTemp)
                        {
                            SaveBitKey.WriteBit(true);
                            NumReaderWriter.WriteNum(po.Tempnextone.TempValue);
                        }
                        else
                        {
                            SaveBitKey.WriteBit(false);

                            //00
                            IntList.Add(po.Tempnextone.Changeing(Counter));
                            //  IntList.Add(po.Tempnextzero.Changeing2(Counter));
                            Counter++;
                            Stoping++;
                        }

                        
                
                        po = root;

                        IsStoping();
                    }
                    else
                    {
                        po = po.Tempnextone;
                    }
                }


            }


            return IntList;

        }



        #endregion

    }


    class ListUniqByNode01
    {




    }

   
   
}
