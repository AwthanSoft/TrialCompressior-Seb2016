using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.ReaderWriteFile02.ReaderSegment02
{
    public class SegmentReader02
    {
        public int modNum = 8;
        public int SegmentLength = 256;
        public int SegmentTimes = 1;
        public string FilingPath;


        public int CurrentStream = 0;
        public byte[] StreamData;
        public bool StateSeek = false;


       // public bool  NextAble=false;
        
        

        public SegmentReader02()
        {
           StreamData=new byte[0];
        }


        public void GetReader(string PathFile , int LengthSgment , int NumOfsegment)
        {
            CurrentStream = NumOfsegment;
            FileInfo filingInfo = new FileInfo(PathFile);
            long LengthFile = filingInfo.Length;
            long SeekStart = LengthSgment * NumOfsegment;
            long SeekEnd = SeekStart + LengthSgment;

          
            #region Seeking
            if (SeekStart < LengthFile)
            {
                if (SeekEnd > LengthFile)
                {
                    LengthSgment = Convert.ToInt32(SeekEnd - LengthFile);

                    if (LengthSgment <= 0)
                        StateSeek = false;
                    else
                        StateSeek = true;
                }
                else
                {
                    StateSeek = true;
                }

            }
            else
            {
                StateSeek = false;

            }

             #endregion


            if (StateSeek)
            {
                StreamData = new byte[LengthSgment];
                using (var filing = new FileStream(PathFile, FileMode.Open, FileAccess.Read))
                {
                    filing.Seek(SeekStart, SeekOrigin.Begin);
                    filing.Read(StreamData, 0, StreamData.Length);
                    filing.Close();
                }

                CurrentStream++;

            }

            
        }
        public void GetReader(string PathFile)
        {

            int LengthSgment = SegmentLength;
            int NumOfsegment = CurrentStream;


            FileInfo filingInfo = new FileInfo(PathFile);
            long LengthFile = filingInfo.Length;
            long SeekStart = LengthSgment * NumOfsegment;
            long SeekEnd = SeekStart + LengthSgment;


            #region Seeking
            if (SeekStart < LengthFile)
            {
                if (SeekEnd > LengthFile)
                {
                    LengthSgment = Convert.ToInt32(SeekEnd - LengthFile);

                    if (LengthSgment <= 0)
                        StateSeek = false;
                    else
                        StateSeek = true;
                }
                else
                {
                    StateSeek = true;
                }

            }
            else
            {
                StateSeek = false;

            }

            #endregion


            if (StateSeek)
            {
                StreamData = new byte[LengthSgment];
                using (var filing = new FileStream(PathFile, FileMode.Open, FileAccess.Read))
                {
                    filing.Seek(SeekStart, SeekOrigin.Begin);
                    filing.Read(StreamData, 0, StreamData.Length);
                    filing.Close();
                }

                CurrentStream++;

            }


        }



    }
}
