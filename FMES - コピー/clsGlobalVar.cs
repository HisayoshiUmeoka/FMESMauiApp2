using Microsoft.Maui.Storage;
using System;

namespace FMES
{
    public static class clsGlobalVar
    {
        private const string SubFolderName = "FMesConfData";
        private const string TextFileName = "FMesConf.txt";

        private const int DefCnfVer = 10100;    //Ver.1.01.00

        static int _svUrl  = 0;
        static string _svUrlTop = string.Empty;
        static string _trmcode = string.Empty;
        static string _trmname = string.Empty;
        static string _auth = string.Empty;
        static int _language = 0;
        static int _logWrite = 1;
        static int _urlMsg = 0;
        static string _optionurl = string.Empty;
        static int _lastSashizuKind = 1;
        static int _CnfExist = 0;
        static int _CnfRed = 0;

        static int _CnfVer = DefCnfVer;
        static int _PartDisp = 0;

        static int _UserID = 0;
        static int _SasizuID = 0;
        static string _SasizuNo = string.Empty;
        static int _KaisouNo = 0;
        static int _KouteiID = 0;
        static int _KouteiShousaiID = 0;
        static int _KensaBashoID = 0;
        static int _Parmit = 0;
        static int _KouteiKekkaID = 0;
        static int _KensaBashoShousaiID = 0;
        static int _hinnmokuID = 0;

        // ログイン情報用フィールド追加
        static string _loginID = string.Empty;
        static string _loginName = string.Empty;

        static int _LineIndex = -1;
        static int _KouteiVer = 0;

        static int _BackKouteiID = -1;

        static int _ActMode = 0;
        // 0：initial(指図番号モード）  -1：その他　-2：指図番号無し作業モード

        static int _NowForm = 0;
        // 0:MainPage これはリドロー不可
        // 1:ConfigPage
        // 2:SashizuPage
        // 3:Page1
        // 4:Page1_5
        // 5:Page2
        // 6:Page3
        // 7:Page4
        // 8:Page5
        // 9:SashizuAdd
        // 10:PageNo
        // 11:PageWeb

        //Configの内容
        static string _CompanyID = string.Empty;
        static string _CompanyPW = string.Empty;
        static string _CompanyURL = string.Empty;

        static string _BackPage = string.Empty;
        static string _QRBackPage = string.Empty;
        static string _QRRET = string.Empty;
        public static string g_BackPage
        {
            get { return _BackPage; }
            set { _BackPage = value; }
        }
        public static string g_QRBackPage
        {
            get { return _QRBackPage; }
            set { _QRBackPage = value; }
        }
        public static string g_QRRET
        {
            get { return _QRRET; }
            set { _QRRET = value; }
        }
        static string _JumpPage = string.Empty;
        public static string g_JumpPage
        {
            get { return _JumpPage; }
            set { _JumpPage = value; }
        }

        static int _ComRegisterd = 0;

        static int _SashizuMode = 1;    //1:QRコードモード 2:セレクトモード
        static string _Operator = string.Empty; //オペレター名

        // ログイン情報用プロパティ追加
        public static string g_loginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }

        public static string g_loginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        public static int g_SashizuMode
        {
            get { return _SashizuMode; }
            set { _SashizuMode = value; }
        }
        public static string g_Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }
        public static int g_hinnmokuID
        {
            get { return _hinnmokuID; }
            set { _hinnmokuID = value; }
        }

        public static string g_CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public static string g_CompanyPW
        {
            get { return _CompanyPW; }
            set { _CompanyPW = value; }
        }
        public static int g_ComRegisterd
        {
            get { return _ComRegisterd; }
            set { _ComRegisterd = value; }
        }
        public static string g_CompanyURL
        {
            get { return _CompanyURL; }
            set { _CompanyURL = value; }
        }

        public static string g_trmcode
        {
            get { return _trmcode; }
            set { _trmcode = value; }
        }
        public static string g_trmname
        {
            get { return _trmname; }
            set { _trmname = value; }
        }
        public static string g_auth
        {
            get { return _auth; }
            set { _auth = value; }
        }
        public static int g_svUrl
        {
            get { return _svUrl; }
            set { _svUrl = value; }
        }
        public static string g_svUrlTop
        {
            get { return _svUrlTop; }
            set { _svUrlTop = value; }
        }
        public static string g_optionurl
        {
            get { return _optionurl; }
            set { _optionurl = value; }
        }

        public static int g_language
        {
            get { return _language; }
            set { _language = value; }
        }
        public static int g_logWrite
        {
            get { return _logWrite; }
            set { _logWrite = value; }
        }
        public static int g_urlMsg
        {
            get { return _urlMsg; }
            set { _urlMsg = value; }
        }
        public static int g_lastSashizuKind
        {
            get { return _lastSashizuKind; }
            set { _lastSashizuKind = value; }
        }


        public static int g_CnfExist
        {
            get { return _CnfExist; }
            set { _CnfExist = value; }
        }
        public static int g_CnfRed
        {
            get { return _CnfRed; }
            set { _CnfRed = value; }
        }

        public static int g_CnfVer
        {
            get { return _CnfVer; }
            set { _CnfVer = value; }
        }

        //各画面呼び出し時に必要な変数
        public static int g_UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public static int g_PartDisp
        {
            get { return _PartDisp; }
            set { _PartDisp = value; }
        }

        public static int g_SasizuID
        {
            get { return _SasizuID; }
            set { _SasizuID = value; }
        }

        public static string g_SasizuNo
        {
            get { return _SasizuNo; }
            set { _SasizuNo = value; }
        }

        public static int g_KaisouNo
        {
            get { return _KaisouNo; }
            set { _KaisouNo = value; }
        }

        public static int g_KouteiID
        {
            get { return _KouteiID; }
            set { _KouteiID = value; }
        }

        public static int g_KouteiShousaiID
        {
            get { return _KouteiShousaiID; }
            set { _KouteiShousaiID = value; }
        }

        public static int g_KensaBashoID
        {
            get { return _KensaBashoID; }
            set { _KensaBashoID = value; }
        }

        public static int g_Parmit
        {
            get { return _Parmit; }
            set { _Parmit = value; }
        }

        public static int g_KouteiKekkaID
        {
            get { return _KouteiKekkaID; }
            set { _KouteiKekkaID = value; }
        }

        public static int g_KensaBashoShousaiID
        {
            get { return _KensaBashoShousaiID; }
            set { _KensaBashoShousaiID = value; }
        }

        public static int g_LineIndex
        {
            get { return _LineIndex; }
            set { _LineIndex = value; }
        }

        public static int g_KouteiVer
        {
            get { return _KouteiVer; }
            set { _KouteiVer = value; }
        }

        public static int g_BackKouteiID
        {
            get { return _BackKouteiID; }
            set { _BackKouteiID = value; }
        }
        public static int g_ActMode
        {
            get { return _ActMode; }
            set { _ActMode = value; }
        }
        public static int g_NowForm
        {
            get { return _NowForm; }
            set { _NowForm = value; }
        }

        public static string GetLanguageStr()
        {
            string strRet = "ja";
            if (g_language == 0)
            {
                strRet = "ja";
            }
            else if (g_language == 1)
            {
                strRet = "en";
            }
            else if (g_language == 2)
            {
                strRet = "zh";
            }
            else if (g_language == 3)
            {
                strRet = "ko";
            }
            return strRet;
        }
        public static string GetLanguageSetting()
        {
            string strRet = "ja-JP";
            if (g_language == 0)
            {
                strRet = "ja-JP";
            }
            else if (g_language == 1)
            {
                strRet = "en_US";
            }
            else if (g_language == 2)
            {
                strRet = "zh-CN";
            }
            else if (g_language == 3)
            {
                strRet = "ko-KR";
            }
            return strRet;
        }
        public static string GetCurURL()
        {
            return _CompanyURL;
        }
        public static string GetCurURL_keep2()
        {
            string strRet = "http://192.168.120.94/";
            if (g_svUrl == 1)
            {
                strRet = "https://ulvacdev01.5ms.cloud/";
            }
            else if (g_svUrl == 2)
            {
                strRet = "https://ulvacdev02.5ms.cloud/";
            }
            else if (g_svUrl == 3)
            {
                strRet = "https://ulvac01.5ms.cloud/";
            }
            else if (g_svUrl == 4)
            {
                strRet = "https://ulvac02.5ms.cloud/";
            }
            else if (g_svUrl == 5)
            {
                strRet = "https://ulvac03.5ms.cloud/";
            }
            else if (g_svUrl == 6)
            {
                strRet = "https://ulvac04.5ms.cloud/";
            }
            else if (g_svUrl == 7)
            {
                strRet = "https://ulvac05.5ms.cloud/";
            }
            else if (g_svUrl == 8)
            {
                strRet = "https://ulvac06.5ms.cloud/";
            }
            else if (g_svUrl == 9)
            {
                strRet = "https://ulvac07.5ms.cloud/";
            }
            else if (g_svUrl == 10)
            {
                strRet = "https://ulvac08.5ms.cloud/";
            }
            else if (g_svUrl == 11)
            {
                strRet = "https://ulvac09.5ms.cloud/";
            }
            else if (g_svUrl == 12)
            {
                strRet = "https://ulvac10.5ms.cloud/";
            }


            return strRet;
        }
        public static string GetCurURLKeep()
        {
            string strRet = "http://192.168.120.94/";
            if (g_svUrl == 1)
            {
                strRet = "http://drawing.jazzclub.jp/";
            }
            else if (g_svUrl == 2)
            {
                strRet = "http://drawing2.jazzclub.jp/";
            }

            else if (g_svUrl == 3)
            {
                strRet = "http://ulvacdev01.5ms.cloud/";
            }
            else if (g_svUrl == 4)
            {
                strRet = "http://ulvacdev02.5ms.cloud/";
            }
            else if (g_svUrl == 5)
            {
                strRet = "http://ulvacdev03.5ms.cloud/";
            }
            else if (g_svUrl == 6)
            {
                strRet = "http://ulvacdev04.5ms.cloud/";
            }
            else if (g_svUrl == 7)
            {
                strRet = "http://ulvacdev05.5ms.cloud/";
            }
            else if (g_svUrl == 8)
            {
                strRet = "http://ulvac01.5ms.cloud/";
            }
            else if (g_svUrl == 9)
            {
                strRet = "http://ulvac02.5ms.cloud/";
            }
            else if (g_svUrl == 10)
            {
                strRet = "http://ulvac03.5ms.cloud/";
            }
            else if (g_svUrl == 11)
            {
                strRet = "http://ulvac04.5ms.cloud/";
            }
            else if (g_svUrl == 12)
            {
                strRet = "http://ulvac05.5ms.cloud/";
            }
            else if (g_svUrl == 13)
            {
                strRet = "http://ulvac06.5ms.cloud/";
            }
            else if (g_svUrl == 14)
            {
                strRet = "http://ulvac07.5ms.cloud/";
            }
            else if (g_svUrl == 15)
            {
                strRet = "http://ulvac08.5ms.cloud/";
            }
            else if (g_svUrl == 16)
            {
                strRet = "http://ulvac09.5ms.cloud/";
            }
            else if (g_svUrl == 17)
            {
                strRet = "http://ulvac10.5ms.cloud/";
            }


            return strRet;
        }
        public static string IsRegisterd()
        {
            return _CompanyURL;
        }


        public static bool LoadConfig()
        {
            bool bRet = true;
            try
            {
                _language = (int)Preferences.Default.Get("_language", 0);
                _logWrite = (int)Preferences.Default.Get("_logWrite", 1);
                _urlMsg = (int)Preferences.Default.Get("_urlMsg", 1);
                _svUrl = (int)Preferences.Default.Get("_svUrl", 0);
                _lastSashizuKind = (int)Preferences.Default.Get("_lastSashizuKind", 0);
                _CnfVer = (int)Preferences.Default.Get("_CnfVer", 0);
                _CnfExist = 1;
                _CnfRed = 1;
                _CompanyID = (string)Preferences.Default.Get("_CompanyID", "");
                _CompanyPW = (string)Preferences.Default.Get("_CompanyPW", "");
                _ComRegisterd = (int)Preferences.Default.Get("_ComRegisterd", 1);
                _CompanyURL = (string)Preferences.Default.Get("_CompanyURL", "");

                //↓認証用
                _trmcode = (string)Preferences.Default.Get("_trmcode", "");
                _trmname = (string)Preferences.Default.Get("_trmname", "");
                _auth = (string)Preferences.Default.Get("_auth", "");
                //↑認証用

                // ログイン情報の読み込み追加
                _loginID = (string)Preferences.Default.Get("_loginID", "");
                _loginName = (string)Preferences.Default.Get("_loginName", "");

                _svUrlTop = _CompanyURL;
                _CnfExist = 1;
                _CnfRed = 1;
                SaveConfig();
            }
            catch (Exception)
            {
                SaveConfig();
                _CnfExist = 1;
                _CnfRed = 1;
                bRet = true;
            }
            return bRet;
        }

        public static bool SaveConfig()
        {
            bool bRet = true;
            Preferences.Default.Set("_language", _language);
            Preferences.Default.Set("_logWrite", _logWrite);
            Preferences.Default.Set("_urlMsg", _urlMsg);
            Preferences.Default.Set("_svUrl", _svUrl);
            Preferences.Default.Set("_lastSashizuKind", _lastSashizuKind);
            Preferences.Default.Set("_CnfVer" , _CnfVer);
            Preferences.Default.Set("_CompanyID", _CompanyID);
            Preferences.Default.Set("_CompanyPW", _CompanyPW);
            Preferences.Default.Set("_ComRegisterd", _ComRegisterd);
            Preferences.Default.Set("_CompanyURL", _CompanyURL);
            //↓認証用
            Preferences.Default.Set("_trmcode", _trmcode);
            Preferences.Default.Set("_trmname", _trmname);
            Preferences.Default.Set("_auth", _auth);
            //↑認証用

            // ログイン情報の保存追加
            Preferences.Default.Set("_loginID", _loginID);
            Preferences.Default.Set("_loginName", _loginName);

            return bRet;
        }
    }
}
