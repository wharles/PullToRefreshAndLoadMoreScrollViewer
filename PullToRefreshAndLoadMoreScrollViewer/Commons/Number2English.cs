using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PullToRefreshAndLoadMoreScrollViewer.Commons
{
    #region Number2English
    public class Number2English
    {
        private string[] StrNO = new string[19];
        private string[] Unit = new string[8];
        private string[] StrTens = new string[9];

        #region NumberToString
        public string NumberToString(double Number)
        {
            string Str;
            string BeforePoint;
            string AfterPoint;
            string tmpStr;
            int nBit;
            string CurString;
            int nNumLen;
            Init();
            Str = Convert.ToString(Math.Round(Number, 2));
            if (Str.IndexOf(".") == -1)
            {
                BeforePoint = Str;
                AfterPoint = "";
            }
            else
            {
                BeforePoint = Str.Substring(0, Str.IndexOf("."));
                AfterPoint = Str.Substring(Str.IndexOf(".") + 1, Str.Length - Str.IndexOf(".") - 1);
            }
            if (BeforePoint.Length > 12)
            {
                return null;
            }
            Str = "";
            while (BeforePoint.Length > 0)
            {
                nNumLen = Len(BeforePoint);
                if (nNumLen % 3 == 0)
                {
                    CurString = Left(BeforePoint, 3);
                    BeforePoint = Right(BeforePoint, nNumLen - 3);
                }
                else
                {
                    CurString = Left(BeforePoint, (nNumLen % 3));
                    BeforePoint = Right(BeforePoint, nNumLen - (nNumLen % 3));
                }
                nBit = Len(BeforePoint) / 3;
                tmpStr = DecodeHundred(CurString);
                if ((BeforePoint == Len(BeforePoint).ToString() || nBit == 0) && Len(CurString) == 3)
                {
                    if (System.Convert.ToInt32(Left(CurString, 1)) != 0 & System.Convert.ToInt32(Right(CurString, 2)) != 0)
                    {
                        tmpStr = Left(tmpStr, tmpStr.IndexOf(Unit[3]) + Len(Unit[3])) + Unit[7] + " " + Right(tmpStr, Len(tmpStr) - (tmpStr.IndexOf(Unit[3]) + Len(Unit[3])));
                    }
                    else
                    {
                        tmpStr = Unit[7] + " " + tmpStr;
                    }
                }
                if (nBit == 0)
                {
                    Str = Convert.ToString(Str + " " + tmpStr).Trim();
                }
                else
                {
                    Str = Convert.ToString(Str + " " + tmpStr + " " + Unit[nBit - 1]).Trim();
                }
                if (Left(Str, 3) == Unit[7])
                {
                    Str = Convert.ToString(Right(Str, Len(Str) - 3)).Trim();
                }
                if (BeforePoint == Len(BeforePoint).ToString())
                {
                    return "";
                }
            }
            BeforePoint = Str;
            if (Len(AfterPoint) > 0)
            {
                AfterPoint = Unit[5] + " " + DecodeHundred(AfterPoint) + " " + Unit[6];
            }
            else
            {
                AfterPoint = Unit[4];
            }
            return BeforePoint + " " + AfterPoint;

        }

        #endregion

        #region Init
        private void Init()
        {
            if (StrNO[0] != "One")
            {
                StrNO[0] = "One";
                StrNO[1] = "Two";
                StrNO[2] = "Three";
                StrNO[3] = "Four";
                StrNO[4] = "Five";
                StrNO[5] = "Six";
                StrNO[6] = "Seven";
                StrNO[7] = "Eight";
                StrNO[8] = "Nine";
                StrNO[9] = "Ten";
                StrNO[10] = "Eleven";
                StrNO[11] = "Twelve";
                StrNO[12] = "Thirteen";
                StrNO[13] = "Fourteen";
                StrNO[14] = "Fifteen";
                StrNO[15] = "Sixteen";
                StrNO[16] = "Seventeen";
                StrNO[17] = "Eighteen";
                StrNO[18] = "Nineteen";
                StrTens[0] = "Ten";
                StrTens[1] = "Twenty";
                StrTens[2] = "Thirty";
                StrTens[3] = "Forty";
                StrTens[4] = "Fifty";
                StrTens[5] = "Sixty";
                StrTens[6] = "Seventy";
                StrTens[7] = "Eighty";
                StrTens[8] = "Ninety";
                Unit[0] = "Thousand";
                Unit[1] = "Million";
                Unit[2] = "Billion";
                Unit[3] = "Hundred";
                Unit[4] = "Only";
                Unit[5] = "Point";
                Unit[6] = "Cent";
                Unit[7] = "And";
            }
        }

        #endregion

        #region DecodeHundred
        private string DecodeHundred(string HundredString)
        {
            int tmp;
            string rtn = "";
            if (Len(HundredString) > 0 && Len(HundredString) <= 3)
            {
                switch (Len(HundredString))
                {
                    case 1:
                        tmp = System.Convert.ToInt32(HundredString);
                        if (tmp != 0)
                        {
                            rtn = StrNO[tmp - 1].ToString();
                        }
                        break;
                    case 2:
                        tmp = System.Convert.ToInt32(HundredString);
                        if (tmp != 0)
                        {
                            if ((tmp < 20))
                            {
                                rtn = StrNO[tmp - 1].ToString();
                            }
                            else
                            {
                                if (System.Convert.ToInt32(Right(HundredString, 1)) == 0)
                                {
                                    rtn = StrTens[Convert.ToInt32(tmp / 10) - 1].ToString();
                                }
                                else
                                {
                                    rtn = Convert.ToString(StrTens[Convert.ToInt32(tmp / 10) - 1] + "-" + StrNO[System.Convert.ToInt32(Right(HundredString, 1)) - 1]);
                                }
                            }
                        }
                        break;
                    case 3:
                        if (System.Convert.ToInt32(Left(HundredString, 1)) != 0)
                        {
                            rtn = Convert.ToString(StrNO[System.Convert.ToInt32(Left(HundredString, 1)) - 1] + " " + Unit[3] + " " + DecodeHundred(Right(HundredString, 2)));
                        }
                        else
                        {
                            rtn = DecodeHundred(Right(HundredString, 2)).ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
            return rtn;
        }

        #endregion

        #region Left
        private string Left(string str, int n)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Substring(0, n);
            return "";
        }
        #endregion

        #region Right
        private string Right(string str, int n)
        {
            return str.Substring(str.Length - n, n);
        }
        #endregion

        #region Len
        private int Len(string str)
        {
            return str.Length;
        }
        #endregion
    }
    #endregion
}
