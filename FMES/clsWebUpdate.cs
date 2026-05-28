using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace FMES
{
    class clsWebUpdate
    {
        public static string _QueURL ;

        public static int SendStartStop(int wUserID, int wSasizuID, int wKouteiID, int wKouteiVer)
        {
            int iRet = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabprocesstime/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiID + "/" + wUserID + "/" + wSasizuID + "/" + wKouteiVer;
            _QueURL = wURL;
            string strErr = string.Empty;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                }
                else
                {
                    int iNoW = strRet.IndexOf("OK");
                    if (iNoW > -1)
                    {
                        int iNo = strRet.IndexOf(",", iNoW);
                        int iNo2 = strRet.IndexOf("<", iNoW);
                        if (iNo > -1)
                        {
                            string strID = strRet.Substring(iNo + 1, iNo2 - iNo - 1);
                            strID = strID.Trim();
                            try
                            {
                                iRet = int.Parse(strID);
                            }
                            catch (Exception)
                            {
                                //throw;
                                iRet = 0;
                            }
                        }
                    }
                    else
                    {
                        iRet = 0;
                    }
                }
            }
            return iRet;
        }
        public static bool SendTrmData(string wAuth, string wtrmcode, string wtrmname, ref string strErr)
        {
            bool bRet = true;
            //下記が仕様上正しいurlだが美味く行かないので現物合わせで①と名前を逆にしする事にした。
            string wURL = clsGlobalVar.GetCurURL() + "users/tablicenseregistration/" + wAuth + wtrmcode + "/" + wtrmname;
            //string wURL = clsGlobalVar.GetCurURL() + "users/tablicenseregistration/" + wtrmname + wtrmcode + "/" + wAuth;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        public static bool Sendchecklicense(string wAuth, string wtrmcode, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tablicensecheck/" + wAuth + wtrmcode;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }

        public static bool SendLotData(int wUserID, int wKouteiID, int wKouteiShousaiID, int wKouteiKekkaID, string strLotPara, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tablotupdate/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiKekkaID + "/" +  wUserID + "/" + wKouteiID + "/" + wKouteiShousaiID + "/" + strLotPara ;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }

        public static bool SendCheckData(int wUserID, int wKouteiID, int wKouteiShousaiID, int wKouteiKekkaID, string wSasizuNo, string strLotPara, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabcheckupdate/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiKekkaID + "/" + wUserID + "/" + wKouteiID + "/" + wKouteiShousaiID + "/" + wSasizuNo + "/" + strLotPara;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        public static bool SendCheckData2(int wUserID, int wKouteiID, int wKouteiShousaiID, int wKouteiKekkaID, string wSasizuNo, int wKensaBashoID, int wChecked, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabcheckupdate2/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiKekkaID + "/" + wUserID + "/" + wKouteiID + "/" + wKouteiShousaiID + "/" + wSasizuNo + "/" + wKensaBashoID + "/" + wChecked;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }

        public static bool SendPartsDataVal(int wUserID, int wSasizuID, int wKouteiID, int whinnmokuID, string strPara, ref string strErr)
        {
            bool bRet = true;

            string wURL = clsGlobalVar.GetCurURL() + "users/updateparts/" + clsGlobalVar.GetLanguageStr() + "/" + wSasizuID + "/" + wKouteiID + "/" + strPara + "/" + whinnmokuID;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }

        public static bool SendResultData(int wUserID, int wSasizuID, int wKouteiID, int wKouteiShousaiID, int wKensaBashoID, int wKensaBashoShousaiID, int wKouteiKekkaID, int iPass, decimal dPara, string strPara, string strCombo, int iSelected, int wKouteiVer, ref string strErr)
        {
            bool bRet = true;

            string wURL = clsGlobalVar.GetCurURL() + "users/tabprocessdetailresult/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiKekkaID + "/" + wSasizuID + "/" + wUserID + "/" + wKouteiID + "/" + wKouteiShousaiID + "/" + wKensaBashoID + "/" + ConvPass2Str(iPass) + "/" + ConvdVal2Str(dPara) + "/" + ConvstrVal2Str(strPara) + "/" + ConvstrVal2Str(strCombo) + "/" + wKensaBashoShousaiID.ToString() + "/" + iSelected.ToString() + "/" + wKouteiVer.ToString();
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        public static int SendProStart(int wUserID, int wSasizuID, int wKouteiID, int wKouteiVer, ref string strErr)
        {
            int iRet = 0;
            //■メモ■processstartの仕様不明で現状このままにしているが、指図IDを引数2箇所で送信しているのは本来は間違い。 tabprocesstart で仕様書にスペルミスで記載あり。
            string wURL = clsGlobalVar.GetCurURL() + "users/tabprocessstart/" + clsGlobalVar.GetLanguageStr() + "/" + wSasizuID + "/" + wKouteiID + "/" + wUserID + "/" + wSasizuID + "/" + wKouteiVer.ToString();
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    iRet = 1;
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return iRet;
        }

        public static int SendResultForFinish(int wUserID, int wSasizuID, int wKouteiID, int wSelLineID, int wKouteiVer, ref string strErr)
        {
            int iRet = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabprocessresult/" + clsGlobalVar.GetLanguageStr() + "/" + wSasizuID + "/" + wUserID + "/" + wKouteiID + "/" + wSelLineID + "/" + wKouteiVer.ToString();
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    iRet = 1;
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return iRet;
        }
        public static int SendResultForBack(int wUserID, int wSasizuID, int wKouteiID, int wPatternID, int wKouteiVer, ref string strErr)
        {
            int iRet = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabbackprocess/" + clsGlobalVar.GetLanguageStr() + "/" + wSasizuID + "/" + wUserID + "/" + wKouteiID + "/" + wPatternID + "/" + wKouteiVer.ToString();
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    iRet = 1;
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return iRet;
        }
        public static int SendResultForBack(int wUserID, int wSasizuID, int wKouteiID, int wPatternID, int wLotNum, int wKouteiVer, ref string strErr)
        {
            int iRet = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabbackprocess/" + clsGlobalVar.GetLanguageStr() + "/" + wSasizuID + "/" + wUserID + "/" + wKouteiID + "/" + wPatternID + "/" + wLotNum + "/" + wKouteiVer;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    iRet = 1;
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return iRet;
        }
        public static int SendProcessStartStop(int wUserID, string wSasizuNo, int wKouteiVer, ref string strErr)
        {
            int iRet = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabprocessstartstop/" + clsGlobalVar.GetLanguageStr() + "/" + wUserID + "/" + wSasizuNo + "/" + wKouteiVer;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {
                    iRet = 1;
                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    iRet = 0;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return iRet;
        }

        public static bool SendCheckSashizu(int wUserID, string wSasizuNo, int wKaisouNo, int wKouteiID, int wKouteiShousaiID, int wKensaBui, int wKensaBashoShousaiID, int wlastMode, int wVer, ref string srtErrMsg)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabworkprocesssearch/" + clsGlobalVar.GetLanguageStr() + "/" + wUserID + "/" + wSasizuNo + "/" + wKaisouNo + "/" + wKouteiID + "/" + wKouteiShousaiID + "/" + wKensaBui + "/" + wKensaBashoShousaiID + "/" + wlastMode + "/" + wVer;
            _QueURL = wURL;
            if (SendLog(wURL, ref srtErrMsg) == true)
            {
                string strRet = clsWebUpDown.GetWebResponce(wURL);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("NG") == 0|| strRet.IndexOf("#NG#") == 0)
                    {
                        bRet = false;
                        srtErrMsg = AddErrURL(wURL) + clsErrorMessage.GetErrMsg(strRet);
                    }
                    else
                    {
                        bRet = true;
                    }
                }
                else
                {
                    bRet = false;
                    srtErrMsg = AddErrURL(wURL) + "不明エラー発生";
                }
            }
            else
            {
                bRet = false;
                srtErrMsg = AddErrURL(wURL) + "ログ送信エラー発生";
            }
            return bRet;
        }
        public static string AddErrURL(string wURL)
        {
            string wRet = "";
            if (clsGlobalVar.g_logWrite == 1)
            {
                wRet = wURL + "\n\n";
            }
            return wRet;
        }

        public static bool SendAddDelSashizu(string wSasizuNo, string wSelectedSasizuNo, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabplaninstractrelation/" + wSasizuNo + "/" + wSelectedSasizuNo;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        public static bool SendReadCommand(int wUserID, string wSasizuNo, int wKouteiID, int wVer, int wLine, ref string srtErrMsg)
        {
            bool bRet = true;
            srtErrMsg = string.Empty;
            string strSend = string.Empty;
            if (wLine == -1)
            {
                strSend = clsGlobalVar.GetCurURL() + "users/tabiotinterface/" + clsGlobalVar.GetLanguageStr() + "/" + wUserID + "/" + wSasizuNo + "/" + wKouteiID + "/" + wVer;

            }
            else
            {
                strSend = clsGlobalVar.GetCurURL() + "users/tabiotinterface/" + clsGlobalVar.GetLanguageStr() + "/" + wUserID + "/" + wSasizuNo + "/" + wKouteiID + "/" + wVer + "/" + wLine;
            }
            try
            {
                SendLog(strSend, ref srtErrMsg);
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {
                        bRet = true;
                    }
                    else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                    {
                        srtErrMsg = clsErrorMessage.GetErrMsg(strRet);
                        bRet = false;
                    }
                }
            }
            catch (Exception)
            {
                //throw;
                bRet = false;
            }

            return bRet;
        }
        public static bool SendReadCommand2(int wUserID, string wSasizuNo, int wKouteiID, int wVer, ref string srtErrMsg)
        {
            bool bRet = true;
            srtErrMsg = string.Empty;
            string strSend = clsGlobalVar.GetCurURL() + "users/tabiotinterface2/" + clsGlobalVar.GetLanguageStr() + "/" + wUserID + "/" + wSasizuNo + "/" + wKouteiID + "/" + wVer;
            try
            {
                SendLog(strSend, ref srtErrMsg);
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {
                        bRet = true;
                    }
                    else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                    {
                        srtErrMsg = clsErrorMessage.GetErrMsg(strRet);
                        bRet = false;
                    }
                }
            }
            catch (Exception)
            {
                //throw;
                bRet = false;
            }

            return bRet;
        }

        public static string ConvPass2Str(int iPass)
        {
            string strRet = string.Empty;
            if (iPass == 1)
            {
                strRet = iPass.ToString();
            }
            else if (iPass == 0)
            {
                strRet = iPass.ToString();
            }
            else if (iPass == 2)
            {
                strRet = iPass.ToString();
            }
            else
            {
                strRet = "NULL";
            }
            return strRet;
        }
        public static string ConvdVal2Str(double dVal)
        {
            string strRet = string.Empty;
            if (dVal == -999999)
            {
                strRet = "NULL";
            }
            else
            {
                strRet = dVal.ToString();
            }
            return strRet;
        }
        public static string ConvdVal2Str(decimal dVal)
        {
            string strRet = string.Empty;
            if (dVal == -999999)
            {
                strRet = "NULL";
            }
            else
            {
                strRet = dVal.ToString();
            }
            return strRet;
        }
        public static string ConvstrVal2Str(string strVal)
        {
            string strRet = string.Empty;
            if (string.IsNullOrEmpty(strVal) == true)
            {
                strRet = "NULL";
            }
            else
            {
                strRet = strVal;
            }
            return strRet;
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
                _QueURL = wURL;
                string strRet = clsWebUpDown.GetWebResponce(wURL);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {

                    }
                    else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                    {
                        bRet = false;
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                    }
                }
            }
            return bRet;
        }
        public static bool SendOCR(string strPath, ref string strErr)
        {
            bool bRet = true;
            //            if (clsGlobalVar.g_logWrite == 1)
            //            {

            string wURL = clsGlobalVar.GetCurURL() + "users/useradd2";
            //string wURL = "http://drawing2.jazzclub.jp/users/useradd2";
                _QueURL = wURL;
                string strRet = clsWebUpDown.GetWebResponce(wURL);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {

                    }
                    else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                    {
                        bRet = false;
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                    }
                }
//            }
            return bRet;
        }
        public static int GetLoginVerify(string wID, string wPW, ref string strErr)
        {
            int iRet = 0;
            string strSend = "https://members.5ms.cloud/users/usercheck/" + wID + "/" + wPW + "/1";
            try
            {
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("NG:") > -1)
                    {
                        int iNo2 = strRet.IndexOf("<", strRet.IndexOf("NG:") + 3);
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                        clsGlobalVar.g_ComRegisterd = 0;
                        iRet = 0;
                    }
                    else
                    {
                        int iNo3 = strRet.IndexOf("http");
                        int iNo2 = strRet.IndexOf("<", strRet.IndexOf("http"));
                        if (iNo3 > -1)
                        {
                            clsGlobalVar.g_CompanyURL = strRet.Substring(iNo3, iNo2 - iNo3);
                            clsGlobalVar.g_CompanyID = wID;
                            clsGlobalVar.g_CompanyPW = wPW;
                            clsGlobalVar.g_ComRegisterd = 1;
                            iRet = 1;
                        }
                        else
                        {
                            iRet = 0;
                            clsGlobalVar.g_CompanyURL = "";
                            clsGlobalVar.g_ComRegisterd = 0;
                            strErr = "システムエラー\nサポートへお問い合わせ下さい。";
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
                iRet = -1;
                clsGlobalVar.g_Parmit = 0;
            }

            return iRet;
        }
        public static bool SendAddBeacon(string wSasizuNo, string wBeaconName, ref string strErr)
        {
            bool bRet = true;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabbeaconplaninstructadd/" + wBeaconName + "/" + wSasizuNo;
            _QueURL = wURL;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("OK") > -1)
                {

                }
                else if (strRet.IndexOf("NG") == 0 || strRet.IndexOf("#NG#") == 0)
                {
                    bRet = false;
                    strErr = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }

        public static int ConvVersion2Int(string strVer)
        {
            int iRet = 0;

            if (string.IsNullOrEmpty(strVer) == false)
            {

                int iNoD = strVer.IndexOf(".");
                if (iNoD > -1)
                {
                    string strNo1 = strVer.Substring(0, iNoD);
                    string strNo2 = strVer.Substring(iNoD + 1);
                    if (string.IsNullOrEmpty(strNo1) == false)
                    {
                        iRet = int.Parse(strNo1) * 1000;
                    }
                    if (string.IsNullOrEmpty(strNo2) == false)
                    {
                        iRet += int.Parse(strNo2);
                    }
                }
                else
                {
                    iRet = int.Parse(strVer) * 1000;
                }
            }

            return iRet;
        }
            public static int GetCurVersion(ref string strErr)
        {
            int iRet = 0;
            string strSend = "https://members.5ms.cloud/users/vercheck/" + "/1";
            try
            {
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("NG:") > -1)
                    {
                        // NGの場合
                        int iNo2 = strRet.IndexOf("<", strRet.IndexOf("NG:") + 3);
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                        iRet = 0;
                    }
                    else
                    {
                        // Versionの場合
                        int iNo2 = strRet.IndexOf("<");
                        string strVer = string.Empty;
                        if (iNo2 > -1)
                        {
                             strVer = strRet.Substring(0, iNo2).Trim();

                            iRet = ConvVersion2Int(strVer);
                        }
                        else
                        {
                            strVer = "0";
                            iRet = 0;
                            strErr = "システムエラー\nバージョン登録無し";
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw;
                strErr = "システムエラー\nバージョン登録例外エラー";
                iRet = -1;
            }

            return iRet;
        }
        public static List<clsSashizudat> GetListCommand(int wuid)
        {
            {
                List<clsSashizudat> lstRec = null;
                try
                {

                    string strSend = clsGlobalVar.GetCurURL() + "users/tabplaninstructsearch/" + wuid.ToString();
                    string srtErrMsg = string.Empty;
                    SendLog(strSend, ref srtErrMsg);
                    string strRet = clsWebUpDown.GetWebResponce(strSend);

                    if (string.IsNullOrEmpty(strRet) == false)
                    {
                        lstRec = GetListRec(strRet);
                    }
                    else
                    {
                        lstRec = null;
                    }
                }
                catch (Exception)
                {
                    //throw;
                    lstRec = null;
                }

                return lstRec;
            }
        }
        public static List<clsSashizudat> GetListRec(string strW)
        {
            List<clsSashizudat> lstRec = new List<clsSashizudat>();
            string strLeft;
            int iNo;
            strW = strW.Replace("<!--ダミー-->", "");
            strLeft = strW;
            while (strLeft.Length > 0)
            {
                int iNo2 = strLeft.IndexOf("<br />");
                if (iNo2 < -1)
                {
                    iNo2 = strLeft.IndexOf("\r\n");
                }
                string strW2 = string.Empty;
                if (iNo2 > -1)
                {
                    strW2 = strLeft.Substring(0, iNo2).Trim();
                    strLeft = strLeft.Substring(iNo2 + 6);
                }
                else
                {
                    strW2 = strLeft.Trim();
                    strLeft = "";
                }
                if (strW2.Length > 0)
                {
                    clsSashizudat wdat = new clsSashizudat(strW2);
                    lstRec.Add(wdat);
                }
            }
            return lstRec;
        }

    }
}
