using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsKaisouHeader
    {
        public int _SashizuID = 0;
        public string _SashizuNo = string.Empty;
        public string _ProductName = string.Empty;
        public int _KouteiID = 0;
        public int _KouteiShousaiID = 0;
        public int _KensaBashoID = 0;
        public int _GamenKind = 0;
        public List<clsLine> _LineLists;
        public string _InputSetsumei = string.Empty;
        public string _InputUnit = string.Empty;
        public int _KetaSei = 0;
        public int _KetaShou = 0;
        public List<clsLine> _SelLists;
        public int _SelSelected = 0;
        public string _Title = string.Empty;
        public string _ImageFile = string.Empty;
        public int _TotalSec = 0;
        public int _StopWatch = 0;
        public int _iPass = 0;
        public decimal _dVal = -999999;
        public string _strVal = string.Empty;
        public string _strCmb = string.Empty;
        public int _KouteiKekkaID = 0;
        public int _KensaBashoShousaiID = 0;
        public int _done = 0;
        public int _BackID = 0;
        public int _Agree = 0;
        public string _beconname = string.Empty;


        public int _LotKind = 1;
        public int _KouteiVer = 0;

        public int _LotMax = 0;

        public clsKaisouHeader()
        {
            _SashizuID = 0;
            _SashizuNo = string.Empty;
            _ProductName = string.Empty;
            _KouteiID = 0;
            _KouteiShousaiID = 0;
            _KensaBashoID = 0;
            _GamenKind = 0;
            _LineLists = new List<clsLine>();
            _InputSetsumei = string.Empty;
            _InputUnit = string.Empty;
            _KetaSei = 0;
            _KetaShou = 0;
            _SelLists = new List<clsLine>();
            _SelSelected = 0;
            _Title = string.Empty;
            _ImageFile = string.Empty;
            _TotalSec = 0;
            _StopWatch = 0;
            _iPass = 0;
            _dVal = -999999;
            _strVal = string.Empty;
            _strCmb = string.Empty;
            _KouteiKekkaID = 0;
            _KensaBashoShousaiID = 0;
            _done = 0;
            _BackID = 0;
            _Agree = 0;
            _LotKind = 1;
            _KouteiVer = 0;
            _LotMax = 0;

        }
        public clsKaisouHeader(string wStr)
        {
            _SashizuID = 0;
            _SashizuNo = string.Empty;
            _ProductName = string.Empty;
            _KouteiID = 0;
            _KouteiShousaiID = 0;
            _KensaBashoID = 0;
            _GamenKind = 0;
            _LineLists = new List<clsLine>();
            _InputSetsumei = string.Empty;
            _InputUnit = string.Empty;
            _KetaSei = 0;
            _KetaShou = 0;
            _SelLists = new List<clsLine>();
            _SelSelected = 0;
            _Title = string.Empty;
            _ImageFile = string.Empty;
            _TotalSec = 0;
            _StopWatch = 0;
            _iPass = -1;
            _dVal = -999999;
            _strVal = string.Empty;
            _strCmb = string.Empty;
            _KouteiKekkaID = 0;
            _KensaBashoShousaiID = 0;
            _done = 0;
            _BackID = 0;
            _Agree = 0;
            _LotKind = 1;
            _KouteiVer = 0;
            _LotMax = 0;
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
                            _SashizuID = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _SashizuNo = strW2;
                        }
                        else if (iIndex == 2)
                        {
                            _ProductName = strW2;
                        }
                        else if (iIndex == 3)
                        {
                            _KouteiID = int.Parse(strW2);
                        }
                        else if (iIndex == 4)
                        {
                            _KouteiShousaiID = int.Parse(strW2);
                        }
                        else if (iIndex == 5)
                        {
                            _KensaBashoID = int.Parse(strW2);
                        }
                        else if (iIndex == 6)
                        {
                            _GamenKind = int.Parse(strW2);
                        }
                        else if (iIndex == 7)
                        {
                            _LineLists = ReadLines(strW2);
                        }
                        else if (iIndex == 8)
                        {
                            _InputSetsumei = strW2;
                        }
                        else if (iIndex == 9)
                        {
                            _InputUnit = strW2;
                        }
                        else if (iIndex == 10)
                        {
                            _KetaSei = int.Parse(strW2);
                        }
                        else if (iIndex == 11)
                        {
                            _KetaShou = int.Parse(strW2);
                        }
                        else if (iIndex == 12)
                        {
                            _SelLists = ReadLines(strW2);
                            _SelSelected = 0;
                        }
                        else if (iIndex == 13)
                        {
                            _done = int.Parse(strW2);
                        }
                        else if (iIndex == 14)
                        {
                            _Title = strW2;
                        }
                        else if (iIndex == 15)
                        {
                            _ImageFile = strW2;
                        }
                        else if (iIndex == 16)
                        {
                            _TotalSec = int.Parse(strW2);
                        }
                        else if (iIndex == 17)
                        {
                            _StopWatch = int.Parse(strW2);
                        }
                        else if (iIndex == 18)
                        {
                            _iPass = int.Parse(strW2);
                        }
                        else if (iIndex == 19)
                        {
                            _dVal = decimal.Parse(strW2);
                        }
                        else if (iIndex == 20)
                        {
                            _strVal = strW2;
                        }
                        else if (iIndex == 21)
                        {
                            _strCmb = strW2;
                        }
                        else if (iIndex == 22)
                        {
                            _KouteiKekkaID = int.Parse(strW2);
                        }
                        else if (iIndex == 23)
                        {
                            _KensaBashoShousaiID = int.Parse(strW2);
                        }
                        else if (iIndex == 24)
                        {
                            _BackID = int.Parse(strW2);
                        }
                        else if (iIndex == 25)
                        {
                            _SelSelected = int.Parse(strW2);
                        }
                        else if (iIndex == 26)
                        {
                            _Agree = int.Parse(strW2);
                        }
                        else if (iIndex == 27)
                        {
                            _LotKind = int.Parse(strW2);
                        }
                        else if (iIndex == 28)
                        {
                            _KouteiVer = int.Parse(strW2);
                        }
                        else if (iIndex == 29)
                        {
                            _LotMax = int.Parse(strW2);
                        }
                        else if (iIndex == 30)
                        {
                            _beconname = strW2;
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
        public void freeLineLists()
        {
            if (_LineLists != null)
            {
                int imax = _LineLists.Count;
                for (int i = 0; i < imax; i++)
                {
                    _LineLists[i] = null;
                }
                _LineLists.Clear();
                _LineLists = null;
            }
        }
        public void freeSelLists()
        {
            if (_SelLists != null)
            {
                int imax = _SelLists.Count;
                for (int i = 0; i < imax; i++)
                {
                    _SelLists[i] = null;
                }
                _SelLists.Clear();
                _SelLists = null;
            }
        }
        public void freeThis()
        {
            freeLineLists();
            freeSelLists();
            _SashizuNo = string.Empty;
            _ProductName = string.Empty;
            _InputSetsumei = string.Empty;
            _InputUnit = string.Empty;
            _Title = string.Empty;
            _ImageFile = string.Empty;
            _strVal = string.Empty;
            _strCmb = string.Empty;
        }
    }
}
