using System;
using System.Collections.Generic;
using System.Text;
//using Xamarin.Forms;
namespace FMES
{
    class clsKaisou
    {
        //public int _No = 0;
        public int _during = 0;
        public int _done = 0;
        public int _parmit = 0;
        public int _kaisouNo = 0;
        public int _KouteiID = 0;
        public int _KouteiShousaiID = 0;
        public int _KensaBashoID = 0;
        public string _kaisouName;
        public Point _IconButton;
        public string _IconImg;
        public int _iPass = -1;
        public int _KensaBashoShousaiID = 0;
        public int _TotalTime = 0;

        public int _UnderGamenKind = 0; //下層の画面種別　但し画面種別13（チェックリスト）時はチェックボタンとして扱う
        public int _LotLeft = 0;    //ロット残数
        public int _Checked = 0;    //チェックボタンの状態


        public clsKaisou(string wStr)
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
                    if (string.IsNullOrEmpty(strW2) == false)
                    {

                        if (iIndex == 0)
                        {
                            _during = int.Parse(strW2);
                        }
                        else if (iIndex == 1)
                        {
                            _done = int.Parse(strW2);
                        }
                        else if (iIndex == 2)
                        {
                            _parmit = int.Parse(strW2);
                        }
                        else if (iIndex == 3)
                        {
                            _kaisouNo = int.Parse(strW2);
                        }
                        else if (iIndex == 4)
                        {
                            _KouteiID = int.Parse(strW2);
                        }
                        else if (iIndex == 5)
                        {
                            _KouteiShousaiID = int.Parse(strW2);
                        }
                        else if (iIndex == 6)
                        {
                            _KensaBashoID = int.Parse(strW2);
                        }
                        else if (iIndex == 7)
                        {
                            _kaisouName = strW2;
                        }
                        else if (iIndex == 8)
                        {
                            wPosX = int.Parse(strW2);
                        }
                        else if (iIndex == 9)
                        {
                            _IconButton = new Point(wPosX, int.Parse(strW2));
                        }
                        else if (iIndex == 10)
                        {
                            _IconImg = strW2;
                        }
                        else if (iIndex == 11)
                        {
                            _iPass = int.Parse(strW2);
                        }
                        else if (iIndex == 12)
                        {
                            _KensaBashoShousaiID = int.Parse(strW2);
                        }
                        else if (iIndex == 13)
                        {
                            _TotalTime = int.Parse(strW2);
                        }
                        else if (iIndex == 14)
                        {
                            _UnderGamenKind = int.Parse(strW2);
                        }
                        else if (iIndex == 15)
                        {
                            _Checked = int.Parse(strW2);
                        }
                        else if (iIndex == 16)
                        {
                            _LotLeft = int.Parse(strW2);
                        }
                        else if (iIndex == 17)
                        {
                            //_Checked = int.Parse(strW2);
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
            _kaisouName = string.Empty;
        }
    }
}
