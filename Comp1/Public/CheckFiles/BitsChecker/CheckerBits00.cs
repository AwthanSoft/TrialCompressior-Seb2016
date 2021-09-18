using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp1.Public.CheckFiles.BitsChecker
{
   public class CheckerBits00
    {

       public CheckerBits00()
       {

       }


       public static StringBuilder PrintAsLines(ref byte[] DataByte , int LengthMod , int NumOfBlockInLien )
       {
           #region   Make Space
           int LengthOfSpace = 1;
           string Space;
           
           {
               var sp = new StringBuilder();

               for (int i = 0; i != LengthOfSpace;i++)
               {
                   sp.Append(" ");
               }
               Space = sp.ToString();
               
           }


           int LengthOfSpaceLine = 2;
           string LineSpace;

           {
               var lsp = new StringBuilder();

               for (int i = 0; i != LengthOfSpaceLine; i++)
               {
                   lsp.Append(" ");
               }
               LineSpace = lsp.ToString();

           }

           #endregion



           BitArray BitNum = new BitArray(DataByte);
           StringBuilder sb = new StringBuilder();

           int Line = 0 ;
           int Block = 0;
           int NumBits = 0;

           foreach (bool b in BitNum)
           {
               if (b == true)
                   sb.Append("1");
               else
                   sb.Append("0");

               NumBits++;

               if (NumBits == LengthMod)
               {
                   NumBits = 0;
                   sb.Append(Space);
                   Block++;
                   if (Block == NumOfBlockInLien)
                   {
                       Block = 0;

                       sb.Append(LineSpace+"L==" + Line.ToString("00000") + Space);
                       Line++;

                   }

               }

           }

           return sb;
       }



    }


}
