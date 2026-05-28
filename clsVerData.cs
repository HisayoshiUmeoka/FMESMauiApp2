using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsVerData
    {
        public int _LotKind = 1;
        public int _KouteiVer = 0;
        public string _KouteiName = string.Empty;
        public int _KouteiStatus = 1;

        public clsVerData(string wStr)
        {
            if (string.IsNullOrEmpty(wStr) == false)
            {
                int iIndex = 0;
                while (wStr.Length > 0)
                {
                    int iNo1 = wStr.IndexOf(",");
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
                            _LotKind = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _KouteiVer = int.Parse(strW2);
                        }
                        else if (iIndex == 2)
                        {
                            _KouteiName = strW2;
                        }
                        else if (iIndex == 3)
                        {
                            _KouteiStatus = int.Parse(strW2);
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
        }

    }
}
