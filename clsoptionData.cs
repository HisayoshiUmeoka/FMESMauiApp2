using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    internal class clsoptionData
    {
        public string _optionName = string.Empty;
        public string _optionURL = string.Empty;
        
        public clsoptionData()
        {
            _optionName = string.Empty;
            _optionURL = string.Empty;

        }
        public clsoptionData(string wStr,int ibuttonID)
        {
            _optionName = string.Empty;
            _optionURL = string.Empty;
            int iIndex = 0;
            if (string.IsNullOrEmpty(wStr) == false)
            {
                while (wStr.Length > 0)
                {
                    int iNo1 = wStr.IndexOf("<br />");
                    string strW2;
                    if (iNo1 > -1)
                    {
                        strW2 = wStr.Substring(0, iNo1).Trim();
                        wStr = wStr.Substring(iNo1 + 1);
                    }
                    else
                    {
                        strW2 = wStr.Trim();
                        wStr = "";
                    }
                    if (string.IsNullOrEmpty(strW2) == false)
                    {
                        if (iIndex == 0)
                        {
                            _optionName = strW2;
                            if (ibuttonID == 1)
                            {
                                _optionURL = clsGlobalVar.GetCurURL() + "users/taboption"+ ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;
                            }
                            else if (ibuttonID == 2)
                            {
                                _optionURL = clsGlobalVar.GetCurURL() + "users/taboption" + ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;
                            }
                            else if (ibuttonID == 3)
                            {
                                _optionURL = clsGlobalVar.GetCurURL() + "users/taboption" + ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;

                            }
                            else if (ibuttonID == 4)
                            {
                                _optionURL = clsGlobalVar.GetCurURL() + "users/taboption" + ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;

                            }
                            else if (ibuttonID == 5)
                            {
                                _optionURL = clsGlobalVar.GetCurURL() + "users/taboption" + ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;
                            }
                            else if (ibuttonID == 6)
                            {
                                //もし拡張する場合は下記を有効にする
                                //_optionURL = clsGlobalVar.GetCurURL() + "users/taboption" + ibuttonID.ToString() + "/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID;

                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    iIndex += 1;
                }

            }
        }

        public void freeThis()
        {
            //freePlaceLists();
            _optionName = string.Empty;
            _optionURL = string.Empty;
        }

    }
}
