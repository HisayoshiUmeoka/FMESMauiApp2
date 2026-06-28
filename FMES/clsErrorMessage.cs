using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsErrorMessage
    {
        public static string GetErrMsg(string wErr)
        {
            string strRet = "想定外エラー発生";
            wErr = wErr.Replace("<!--ダミー-->", "");
            int iNo = wErr.IndexOf("#NG#");
            if (iNo == 0)
            {
                int iNo2 = wErr.IndexOf(":", iNo + 4);
                string strNo = wErr.Substring(iNo2 + 1);
                strRet = strNo;
            }
            else
            {
                iNo = wErr.IndexOf("NG");
                if (iNo == 0)
                {
                    int iNo2 = wErr.IndexOf(":", iNo + 2);
                    string strNo = wErr.Substring(iNo2 + 1);
                    strRet = strNo;
                }

            }

            return strRet;
        }
    }
}
