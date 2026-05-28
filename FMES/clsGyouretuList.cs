using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMES
{
    internal class clsGyouretuList
    {
        public string _SasizuNo;

        public clsGyouretuHeader _Header;
        public List<clsGyouretu> _Datas = new List<clsGyouretu>();
        public bool GetList(string wURL, ref string srtErrMsg)
        {
            bool bRet = false;
            int igyou = 0;//行
            int icnt = 0;
            int iretu = 0;//列
            string strErr = string.Empty;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("NG") != 0 && strRet.IndexOf("#NG#") != 0)
                {
                    List<string> lstRec = GetRecStart(strRet);
                    foreach (string wStr in lstRec)
                    {
                        if (icnt == 0)
                        {
                            _Header = new clsGyouretuHeader(wStr);
                        }
                        else
                        {
                            clsGyouretu wKaisou = new clsGyouretu(wStr);

                            _Datas.Add(wKaisou);
                        }
                        icnt += 1;
                    }
                    lstRec.Clear();
                    lstRec = null;
                    bRet = true;
                }
                else
                {
                    bRet = false;
                    srtErrMsg = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        private List<string> GetRecStart(string strW)
        {
            List<string> lstRec = new List<string>();
            string strLeft;
            string endword= string.Empty;

            //int iNo = strW.IndexOf("</div>");



                strLeft = strW.Replace("<div>", "");
                //iNo=0;
                if (strW.IndexOf("<br />") >-1)
                {
                    endword = "<br />";
                }
            else if (strW.IndexOf("<br/>") > -1)
            {
                endword = "<br/>";
            }
            else if (strLeft.IndexOf("\r\n")>-1)
                {
                    endword = "\r\n";

                }
                else if (strLeft.IndexOf("\n") > -1)
                {
                    endword = "\n";
                }
//                    int iNo2 = strLeft.IndexOf(endword);
//                if (iNo2 < -1)
//                {
//                    iNo2 = strLeft.IndexOf(endword);
//                }



                //strLeft = strW.Substring(iNo + 6);
                while (strLeft.Length > 0)
                {
                string strW2 = string.Empty;
                int iNo3 = strLeft.IndexOf(endword);
                //Debug.WriteLine($"iNo3 の値 = {iNo3}");
                if (iNo3 > -1)
                    {
                        strW2 = strLeft.Substring(0, iNo3).Trim();
                        strLeft = strLeft.Substring(iNo3 + endword.Length);
                //Debug.WriteLine($"strLeft の値 = {strLeft}");
                }
                    else
                    {
                        strW2 = strLeft.Trim();
                        strLeft = "";
                    }
                    if (strW2.Length > 0)
                    {
                        lstRec.Add(strW2);
                    }
                }
            return lstRec;
        }

        private List<string> GetRecStart_keep(string strW)
        {
            List<string> lstRec = new List<string>();
            string strLeft;
            string endword = string.Empty;

            //int iNo = strW.IndexOf("</div>");



            strLeft = strW.Replace("<div>", "");
            //iNo=0;
            if (strW.IndexOf("<br/>") > -1)
            {
                endword = "<br/>";
            }
            else if (strLeft.IndexOf("\r\n") > -1)
            {
                endword = "\r\n";

            }
            else if (strLeft.IndexOf("\n") > -1)
            {
                endword = "\n";
            }
            int iNo2 = strLeft.IndexOf(endword);
            if (iNo2 < -1)
            {
                iNo2 = strLeft.IndexOf(endword);
            }



            //strLeft = strW.Substring(iNo + 6);
            while (strLeft.Length > 0)
            {
                string strW2 = string.Empty;
                int iNo3 = strLeft.IndexOf(endword);
                if (iNo3 > -1)
                {
                    strW2 = strLeft.Substring(0, iNo3).Trim();
                    strLeft = strLeft.Substring(iNo3 + endword.Length);
                }
                else
                {
                    strW2 = strLeft.Trim();
                    strLeft = "";
                }
                if (strW2.Length > 0)
                {
                    lstRec.Add(strW2);
                }
            }
            return lstRec;
        }
        public int FindData(int wgyou, int wretu)
        {
            int ret = -1;
            int imax = _Datas.Count-1;
            for (int i = 0; i <= imax; i++)
            {
                if (_Datas[i]._gyouno == wgyou && _Datas[i]._retuno == wretu)
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }
        public void freeThis()
        {
            if (_Header != null)
            {
                _Header.freeThis();
            }
            if (_Datas != null)
            {
                int imax = _Datas.Count;
                for (int i = 0; i < imax; i++)
                {
                    _Datas[i].freeThis();
                    _Datas[i] = null;
                }
                _Datas.Clear();
                _Datas = null;
            }
        }
        public static string ConvURL2Log(string strLog)
        {
            strLog = strLog.Replace("http://", "");
            strLog = strLog.Replace("https://", "");
            strLog = strLog.Replace("/", "【｜｜】");
            return strLog;
        }

        public static bool SendLog(string strLog, ref string strErr)
        {
            bool bRet = true;
            if (clsGlobalVar.g_logWrite == 1)
            {

                strLog = ConvURL2Log(strLog);
                string wURL = clsGlobalVar.GetCurURL() + "users/tabaccesslog/" + strLog;
                //_QueURL = wURL;
                string strRet = clsWebUpDown.GetWebResponce(wURL);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {

                    }
                    else if (strRet.IndexOf("NG") > -1)
                    {
                        bRet = false;
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                    }
                }
            }
            return bRet;
        }
}
}



