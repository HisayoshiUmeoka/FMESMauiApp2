using System;
using System.Collections.Generic;
using System.Text;

namespace FMES
{
    class clsPartsData
    {
        public int partsID = 0;
        public string _PartsName = string.Empty;
        public int _ValType = 0;// ０:i or 1:f
        public int _iVal = 0;
        public decimal _dVal = -999999;
        //public List<clsLine> _PlaceLists = new List<clsLine>();
        public int _SelSelected = 0;
        public string _strunit = string.Empty;

        public bool _nullstring = false;

        public clsPartsData()
        {
            partsID = 0;
            _PartsName = string.Empty;
            _ValType = 0; // 0:int 1:float
            //_PlaceLists = new List<clsLine>();
            _SelSelected = 0;
        _iVal = 0;
         _dVal = -999999;
            _nullstring = false;
            _strunit = string.Empty;
            _dVal = -999999;

        }
        public clsPartsData(string wStr)
        {
            partsID = 0;

            _PartsName = string.Empty;
            _ValType = 0;
            //_PlaceLists = new List<clsLine>();
            _strunit = string.Empty;
            _SelSelected = 0;
            _iVal = 0;
            _dVal = -999999;
            _nullstring = false;
            int iIndex = 0;
            _strunit = string.Empty;
            if (string.IsNullOrEmpty(wStr) == false)
            {
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
                            partsID = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _PartsName = strW2;
                        }
                        else if (iIndex == 2)
                        {
                            _nullstring = false;

                            if (strW2.IndexOf(".", 0) == -1)
                            {
                                _ValType = 0;//i
                            }
                            else
                            {
                                _ValType = 1;//f
                            }
                            if (_ValType == 0)
                            {
                                _iVal = int.Parse(strW2);
                            }
                            else
                            {
                                _dVal = decimal.Parse(strW2);
                            }


                            //_ValType = int.Parse(strW2);
                        }
                        else if (iIndex == 3)
                        {
                            //_PlaceLists = ReadLines(strW2);
                        }
                        else if (iIndex == 4)
                        {
                            if (strW2.IndexOf(".", 0) == -1)
                            {
                                _ValType = 0;//i
                            }
                            else
                            {
                                _ValType = 1;//f
                            }

                            if (_ValType == 0)
                            {
                                _iVal = int.Parse(strW2);
                            }
                            else
                            {
                                _dVal = decimal.Parse(strW2);
                            }
                        }
                        else if (iIndex == 5)
                        {
                            _strunit = strW2;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {

                        if (iIndex == 2)
                        {
                            _nullstring = true;
                            _ValType = 0;//i
                            _iVal = 0;
                            _dVal = 0;
                            break;

                        }
                    }
                    iIndex += 1;
                }
                if (iIndex == 2)
                {
                    _nullstring = true;
                    _ValType = 0;//i
                    _iVal = 0;
                    _dVal = 0;

                }

            }
            else
            {
                if (iIndex == 2)
                {
                    _nullstring = true;
                    _ValType = 0;//i
                    _iVal = 0;
                    _dVal = 0;

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
//        public void freePlaceLists()
//        {
//            if (_PlaceLists != null)
//            {
//                int imax = _PlaceLists.Count;
//                for (int i = 0; i < imax; i++)
//                {
//                    _PlaceLists[i] = null;
//                }
//                _PlaceLists.Clear();
//                _PlaceLists = null;
//            }
//        }
        public void freeThis()
        {
            //freePlaceLists();
            _PartsName = string.Empty;
        _ValType = 0;// ０:i or 1:f
        _iVal = 0;
        _dVal = -999999;
        _strunit = string.Empty;
        }

    }
}
