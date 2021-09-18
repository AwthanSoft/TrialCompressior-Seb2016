using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderWriteFile02
{
   public  class FileReaderInfo02
    {
        public string FileName = "";
        public string FileExtension = "";
        public string PathFile="";
        public string FileDir = "";
        public long FileSize = 0;

        public string SaveName = "";
        public string SaveExtension = "0";
        public string SaveFileDir = "";

      //  public bool IsCancel = true;

        public string FullSaveFilePath
        {
            get
            {
                return SaveFileDir + @"\" + SaveName + "." + SaveExtension;
            }

        }

        public string SavePathWethoutEtension
        {
            get
            {
                return SaveFileDir + @"\" + SaveName;
            }

        }

        public string ReaderPathWethoutEtension
        {
            get
            {
                
                return FileDir + @"\" + FileName;
            }

        }


        public  bool ReaderReady = false;
        
        public int StopNumLength = 256;

        public int ReaderDataLength = 256;

        public FileReaderInfo02()
        {


        }

        public FileReaderInfo02(string FilePath)
        {
            FileInfo filing;
            if(FilePath=="")
                return;

            try
            {
                filing = new FileInfo(FilePath);
                FileName = Path.GetFileNameWithoutExtension(FilePath);
                FileExtension = Path.GetExtension(FilePath);
                PathFile = FilePath;
                FileDir = filing.DirectoryName;


                SaveName = FileName;
                SaveFileDir = FileDir;

                FileSize = filing.Length;






            }
            catch
            {




            }

        }



       //Adding Bits
        public bool RestBitsExist = false;
        public int AddingBits = 0;


    }



}
