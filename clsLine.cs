using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsLine
    {
        public int _index = 0;
        public string _LineName;
        public clsLine()
        {
        }
        public clsLine(string wStr)
        {
            clsLine wLine = new clsLine();
            if (string.IsNullOrEmpty(wStr) == false)
            {
                int iNo1 = wStr.IndexOf("#", 0);
                string strW2;
                if (iNo1 > -1)
                {
                    strW2 = wStr.Substring(0, iNo1).Trim();
                    wStr = wStr.Substring(iNo1 + 1);
                    try
                    {
                        _index = int.Parse(strW2);
                        _LineName = wStr;
                    }
                    catch (Exception)
                    {
                        _index = 0;
                        _LineName = "DataErr";
                    }
                }
                else
                {
                    strW2 = wStr.Trim();
                    wStr = "";
                    _index = 0;
                    _LineName = strW2;
                }
            }
        }

    }
}
