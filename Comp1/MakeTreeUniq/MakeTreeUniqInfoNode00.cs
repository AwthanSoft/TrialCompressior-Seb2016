using Comp1.Public.ReaderWriteFile02;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Comp1.MakeTreeUniq
{
    [Serializable()]
   public class MakeTreeUniqInfoNode00
    {
        public int SegmentNum = 0;
        public int OperationId = 0;

        public int ModSegmentLength = 2048;
        public int HowSaveModOperBits = 8;

        public int HowReadBits = 0;
        public int HowWriteBits = 0;
        public int HowRestBits = 0;

        public int HowSaveBits = 0;
        public int HowAdditionBits = 0;

        public bool isAble = false;

        public bool isItSave = false;
        public int TempSaved = 0;

        public MakeTreeUniqInfoNode00()
        {

        }
        public MakeTreeUniqInfoNode00(int HowSegmentLength)
        {
            ModSegmentLength = HowSegmentLength;

        }



        public void EditModSegmentLength(int HowSegmentLength)
        {
            ModSegmentLength = HowSegmentLength;
        }
        public void EditSegmentNum(int NumSegment)
        {
            SegmentNum = NumSegment;
        }
        public void EditHowReadBits(int ReadBits)
        {
            HowReadBits = ReadBits;
        }

        public void EditHowWriteBits(int NumOfWriteBits)
        {
            HowWriteBits = NumOfWriteBits + HowSaveModOperBits;
            HowRestBits = ModSegmentLength - HowReadBits;

            HowSaveBits = ModSegmentLength - (HowWriteBits + HowRestBits);


            if (HowSaveBits >= 0)
            {
                isAble = true;
            }
            else
            {
                HowAdditionBits = HowSaveBits *  -1;
            }


            /*---------------Temp------------------*/
            TempSaved = HowReadBits - (NumOfWriteBits + HowSaveModOperBits);
            if (TempSaved >= 0)
            {
                isItSave = true;
            }



        }


    }


    public class MakeTreeUniqInfoReaderWriterFile00
    {

        #region  File Proprties

        private bool isReaderMod = false;
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

                fileIsOpen = true;

                if (isReaderMod)
                {
                    InitialReader();
                 //   ReaderS = new StreamReader(Pathfile);
                }
                else
                {
                    InitialWriter();
                 //   WriterS = new StreamWriter(Pathfile);
                }

                fileIsOpen = true;
            }

        }
        public void CloseFile()
        {
            
            if (!isReaderMod)
            {
                SaveAllListNod();
                if (WriterS != null)
                    WriterS.Close();
             
            }
            else
            {
                if (ReaderS != null)
                    ReaderS.Close();
                
            }

            fileIsOpen = false;

            GetInfo();
        }

        #endregion

        #region  Data Save


        /********* Data  **************/
        private List<MakeTreeUniqInfoNode00> ListWriteNod=new List<MakeTreeUniqInfoNode00>();
        private int SN = 0;
        private int WriterListLength = 50;
        private StreamWriter WriterS;
        //private Stream WriterSF;
        //private MemoryStream WriteroStream;

        private int WriterProcessTimer = 0;
        private int SumNodWrite = 0;

        private void InitialWriter()
        {
            WriterS = new StreamWriter(Pathfile);
           // WriterSF = File.Open(Pathfile, FileMode.Create);

           // WriteroStream = new MemoryStream();

        }
        public void WriteNod(ref MakeTreeUniqInfoNode00 Nod)
        {
            if (SN == WriterListLength)
            {
                SaveAllListNod();

            }

            ListWriteNod.Add(Nod);

            SN++;
            SumNodWrite++;


            //For Info
            if (Nod.isItSave)
                SumOfNodIsSaveTrue++;
            if (Nod.isAble)
                SumOfNodIsAbleTrue++;


        }
        private void SaveAllListNod()
        {
            if (fileIsOpen == false)
            {
                OpenFile();
            }



            ////01
            //var serializer = new XmlSerializer(typeof(List<MakeTreeUniqInfoNode00>));
            //serializer.Serialize(WriterS, ListWriteNod);


            ////02
            //var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //binaryFormatter.Serialize(WriterSF, ListWriteNod);


            ////03
            //string sData = "";
            //using (MemoryStream oStream = new MemoryStream())
            //{
            //    XmlSerializer oSerializer = new XmlSerializer(this.GetType());
            //    oSerializer.Serialize(oStream, this);
            //    oStream.Position = 0;
            //    sData = Encoding.UTF8.GetString(oStream.ToArray());
            //}

           
            //04
            string sJSON = new JavaScriptSerializer().Serialize(ListWriteNod);

            WriterS.WriteLine(sJSON);




            ListWriteNod.Clear();
            SN = 0;
            WriterProcessTimer++;
        }

        #endregion

        #region  Data Read

        StreamReader ReaderS;
        Stream ReaderSF;

        private List<MakeTreeUniqInfoNode00> ListReaderNod=new List<MakeTreeUniqInfoNode00>();
        //private int ReaderListLength = 20;
        private int ListReadLength = 0;
        private int RN = 0;

        public bool isAbleRead = true;
        public bool ReadAble = true;

        private void InitialReader()
        {

           // ReaderSF = new FileStream(Pathfile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            ReaderS = new StreamReader(Pathfile ,true);

            isAbleRead = true;

        }

        /********* Info  **************/
       
        private int ReaderProcessTimer = 0;
        private int SumNodRead = 0;


        private void ReadData()
        {
            if (fileIsOpen == false)
            {
                OpenFile();
            }

            if (ReadAble == true)
            {

                ListReaderNod.Clear();

                //TextReader reader = null;
                //try
                //{
                // ////   01
                // //   var serializer = new XmlSerializer(typeof(List<MakeTreeUniqInfoNode00>));
                // //   reader = ReaderS;

                // //   ListReaderNod = (List<MakeTreeUniqInfoNode00>)serializer.Deserialize(reader);


                //    //02
                //    //BinaryFormatter bin = new BinaryFormatter();
                //    //var lizards2 = (List<MakeTreeUniqInfoNode00>)bin.Deserialize(ReaderSF);

                  
                //}
                //finally
                //{
                //    if (reader == null)
                //    {
                //     //   isAbleRead = false;
                //    //    reader.Close();

                //   //     ReadAble = false;
                //    }
                //}





                //03


                ListReaderNod = new JavaScriptSerializer().Deserialize<List<MakeTreeUniqInfoNode00>>(ReaderS.ReadLine());



                ListReadLength = ListReaderNod.Count;
                SumNodRead = SumNodRead + ListReadLength;

                if (ReaderS.EndOfStream)
                    ReadAble = false;
               
            }
            else
            {
               
                isAbleRead = false;
            }

            ReaderProcessTimer++;
            RN = 0;

        }
        private MakeTreeUniqInfoNode00 po;
        public MakeTreeUniqInfoNode00 GetNod()
        {
            if (RN == ListReadLength)
            {
                ReadData();
            }

            RN++;


            po = ListReaderNod[RN - 1];


            //For Info
            if (po.isItSave)
                SumOfNodIsSaveTrue++;
            if (po.isAble)
                SumOfNodIsAbleTrue++;



            return po;

        }


        #endregion


        #region For Info

        private int SumOfNodIsSaveTrue = 0;
        private int SumOfNodIsAbleTrue = 0;
      

        private void GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n"
                   + "\nSumOfNodIsSaveTrue = " + SumOfNodIsSaveTrue.ToString("000")
                   + "\nSumOfNodIsAbleTrue = " + SumOfNodIsAbleTrue.ToString("000")
                   + "\nSumNodWrite        = " + SumNodWrite.ToString("000")


                   + "\n"

                   );

            MessageBox.Show(sb.ToString(), "InfoNodes");
        }

        #endregion


        #region OverLoad

        public MakeTreeUniqInfoReaderWriterFile00(bool IsReaderMod)
        {
            isReaderMod = IsReaderMod;
            ReadWriteFile02 readerFile = new ReadWriteFile02();
            if (readerFile.IsCancel)
                return;

            if (IsReaderMod)
            {
                Pathfile = readerFile.ReaderF.PathFile;
            }
            else
            {
                Pathfile = readerFile.ReaderF.FullSaveFilePath;
            }

           

        }
        public MakeTreeUniqInfoReaderWriterFile00(bool IsReaderMod, string Extension)
        {
            isReaderMod = IsReaderMod;
            ReadWriteFile02 readerFile = new ReadWriteFile02(Extension);
            if (readerFile.IsCancel)
                return;

            if (readerFile.IsCancel)
                return;

            if (IsReaderMod)
            {
                Pathfile = readerFile.ReaderF.PathFile;
            }
            else
            {
                Pathfile = readerFile.ReaderF.FullSaveFilePath;
            }

            isReaderMod = IsReaderMod;

        }
        public MakeTreeUniqInfoReaderWriterFile00(string MainPathFile, bool IsReaderMod)
        {
            isReaderMod = IsReaderMod;
            Pathfile = MainPathFile;
            isReaderMod = IsReaderMod;
        }

        #endregion

    }
}
