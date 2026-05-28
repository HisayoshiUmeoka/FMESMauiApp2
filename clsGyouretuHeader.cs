using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace FMES
{
    internal class clsGyouretuHeader
    {
        public int _Gyou = 0;
        public int _Retu = 0;
        public clsGyouretuHeader()
        {
           _Gyou = 0;
           _Retu = 0;
    
        }
        public clsGyouretuHeader(string wStr)
        {
            _Gyou = 0;
            _Retu = 0;
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
                            _Gyou = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _Retu = int.Parse(strW2);
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
            _Gyou = 0;
            _Retu = 0;
        }
    }
}
