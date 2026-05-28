using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
        internal class clshinmokuData
    {
        public int hinnmokuID = 0;
        public string _hinnmokuName = string.Empty;

        public clshinmokuData()
        {
            hinnmokuID = 0;
            _hinnmokuName = string.Empty;
            //_PlaceLists = new List<clsLine>();

        }
        public clshinmokuData(string wStr)
        {
            hinnmokuID = 0;
            _hinnmokuName = string.Empty;

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
                            hinnmokuID = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _hinnmokuName = strW2;
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

        private List<clsLine> ReadLines(string wStr)
        {
            List<clsLine> wLines = new List<clsLine>();
            if (string.IsNullOrEmpty(wStr) == false)
            {
                int iIndex = 0;
                while (wStr.Length > 0)
                {
                    int iNo1 = wStr.IndexOf("@");
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
                        clsLine wLine = new clsLine(strW2);
                        wLines.Add(wLine);
                    }
                    iIndex += 1;
                }
            }
            return wLines;
        }
        public void freeThis()
        {
            _hinnmokuName = string.Empty;
            hinnmokuID = 0;

            }

    }
}
