using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsSashizudat
    {
        public int _SashizuID = 0;
        public string _SashizuName;
        public clsSashizudat()
        {
        }
        public clsSashizudat(string wStr)
        {
            clsLine wLine = new clsLine();
            if (string.IsNullOrEmpty(wStr) == false)
            {
                int iNo1 = wStr.IndexOf(",");
                string strW2;
                if (iNo1 > -1)
                {
                    strW2 = wStr.Substring(0, iNo1).Trim();
                    wStr = wStr.Substring(iNo1 + 1);
                    try
                    {
                        _SashizuID = int.Parse(strW2);
                        _SashizuName = wStr;
                    }
                    catch (Exception)
                    {
                        _SashizuID = 0;
                        _SashizuName = "DataErr";
                    }
                }
                else
                {
                    strW2 = wStr.Trim();
                    wStr = "";
                    _SashizuID = 0;
                    _SashizuName = strW2;
                }
            }
        }

    }
}
