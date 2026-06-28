using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMES
{
    internal class clsGyouretu
    {
        public int _gyouno = 0;
        public int _retuno = 0;
        public int _fontsize = 0;
        public string _fontattr = string.Empty;
        public bool _fontBOLD = false;
        public string _fontcolor = string.Empty;
        //public colors _fontcolors = Colors.Black;

        public string _text = string.Empty;
        public int _width = 0;
        public bool _visible = false;

        public clsGyouretu(string wStr)
        {
            if (string.IsNullOrEmpty(wStr) == false)
            {
                int iIndex = 0;
                int wPosX = 0;
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
                    //if (string.IsNullOrEmpty(strW2) == false)
                    //{

                        if (iIndex == 0)
                        {
                            _gyouno = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _retuno = int.Parse(strW2);
                        }
                        else if (iIndex == 2)
                        {
                            _fontsize = int.Parse(strW2);
                        }
                        else if (iIndex == 3)
                        {
                            _fontattr = strW2;
                            _fontBOLD = (strW2=="B")? true: false;
                        }
                        else if (iIndex == 4)
                        {
                            _fontcolor = strW2.Replace("\"","");
                        }
                        else if (iIndex == 5)
                        {
                            _text = strW2.Replace("\"", "");
                        _visible= (string.IsNullOrEmpty(_text) == true)? false: true;

                        }
                        else if (iIndex == 6)
                        {
                            _width = int.Parse(strW2);
                        }
                        else
                        {
                            break;
                        }
                    //}
                    iIndex += 1;
                }
            }
        }
        public void freeThis()
        {
            _text = string.Empty;
            _fontattr = string.Empty;
            _fontcolor = string.Empty;
        }
    }
}
