using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comp1.Public.ReaderWriteFile02
{
  public  class FileReaderInfo02EventArgs : EventArgs
    {
      private FileReaderInfo02 ReaderFiling;

      public FileReaderInfo02EventArgs(FileReaderInfo02 ReaderFile)
      {
          ReaderFiling = ReaderFile;
      }

      public FileReaderInfo02 ReadFile
      {
          get
          {
              return ReaderFiling;
          }
      }
          
          
    }

 

}
