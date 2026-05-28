using System.Globalization;
using ZXing.Net.Maui;

namespace FMES;

public partial class SashizuAdd : ContentPage
{
    private clsKaisouList lstKaisou;

    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Label label2;
    private Label label3;
    private List<Label> LstlabSashizu = new List<Label>();
    private List<Button> LstbuttonDel = new List<Button>();
    private List<StackLayout> LstLayout = new List<StackLayout>();
    private Picker dropdown1;
    private Button buttonQR;
    private Button buttonSashizuGr;
    //private Button buttonUpd2;
    private Button buttonEnd;
    private StackLayout layout1;
    private ScrollView sv;

    private bool doingNow = false;

    public int _TotalTime { get; set; } = 0;
    public int _StartStop { get; set; } = 0;
    public bool _TimerStoped { get; set; } = false;



    public SashizuAdd()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        Color defBKCol = Colors.White;
        //        AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 9;
        // ↓added for popupmeneu

        labelUser = new Label
        {
            Text = clsGlobalVar.g_Operator,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontSize = 22,
            VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.End,
        };

        buttonMenu = new Button
        {
            //Text = "メニュー",
            ImageSource = "icon80x80.png",
            FontSize = 20,
            BackgroundColor = Colors.White,
            HorizontalOptions = LayoutOptions.End,
            //VerticalOptions = LayoutOptions.center // 中央に配置する（縦方向）
            VerticalOptions = LayoutOptions.Center // 中央に配置する（縦方向）
        };
        buttonMenu.Clicked += MenuButtonClicked;
        ContentMenu = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            BackgroundColor = Colors.White,
            Children = {
                        labelUser,
                        buttonMenu,
                    }
        };
        // ↑added for popupmeneu
        string srtErrMsg = string.Empty;
        lstKaisou = new clsKaisouList();
        if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {

            _TotalTime = lstKaisou._Header._TotalSec;
            _StartStop = lstKaisou._Header._StopWatch;
            _TimerStoped = (_StartStop == 1) ? false : true;
            clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;
            layout1 = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Orientation = StackOrientation.Vertical,
            };
            label1 = new Label
            {
                Text = "　" + lstKaisou._Header._Title,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            label2 = new Label
            {
                //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            label3 = new Label
            {
                //Text = "　　" + AppResources.IDM030 + "：" + clsGlobalVar.g_SasizuNo,
                Text = "　　" + "指図番号" + "：" + clsGlobalVar.g_SasizuNo,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            layout1.Children.Add(ContentMenu);
            layout1.Children.Add(label1);
            layout1.Children.Add(label2);
            layout1.Children.Add(label3);

            foreach (clsLine wLine in lstKaisou._Header._SelLists)
            {
                if (wLine._index == 1)
                {
                    Label labelW = new Label
                    {
                        Text = "　" + wLine._LineName,
                        FontSize = 22,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                    };
                    LstlabSashizu.Add(labelW);
                    Button butn = new Button
                    {
                        ImageSource = "batsu.png",
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.EndAndExpand//,//中央に配置する（横方向）
                    };
                    butn.Clicked += ItemButtonDelClicked;
                    LstbuttonDel.Add(butn);
                    StackLayout Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(10, 10, 10, 10),
                        Children = {
                                butn,
                                labelW,
                            }
                    };
                    LstLayout.Add(Content2);
                    layout1.Children.Add(Content2);
                }
            }

            dropdown1 = new Picker
            {
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 20,
                //Title = AppResources.IDM086,
                Title = "指図番号選択",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
            foreach (clsLine wLine in lstKaisou._Header._SelLists)
            {
                if (wLine._index == 0)
                {
                    dropdown1.Items.Add(wLine._LineName);
                }
            }
            // ↓added for QRScan
            if (clsGlobalVar.g_BackPage == "SashizuAdd" && clsGlobalVar.g_QRRET != null)
            {
                int iMax = dropdown1.Items.Count;
                int iSelectedIndex = -1;
                for (int i = 0; i < iMax; i++)
                {
                    if (dropdown1.Items[i].ToString() == clsGlobalVar.g_QRRET)
                    {
                        iSelectedIndex = i;
                        dropdown1.SelectedIndex = i;
                        break;
                    }
                }
                clsGlobalVar.g_BackPage = string.Empty;
                clsGlobalVar.g_QRRET = string.Empty;

            }
            // ↑added for QRScan

            buttonQR = new Button
            {
                //Text = "ＱＲスキャン",
                ImageSource = "qr100x100.png",
                FontSize = 20,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center//,//中央に配置する（横方向）
                                                        //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            buttonQR.Clicked += ScanButtonClicked;

            buttonSashizuGr = new Button
            {
                //Text = AppResources.IDM087,
                Text = "追加",
                FontSize = 22,
                //VerticalOptions = LayoutOptions.Center,
                //            HorizontalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonSashizuGr.Clicked += AddButtonClicked;
            //layout1.Children.Add(buttonSashizuGr);
            StackLayout Content3 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 5, 5, 5),
                BackgroundColor = Colors.White,
                Children = {
                                dropdown1,
                                buttonQR,
                                buttonSashizuGr,
                            }
            };
            layout1.Children.Add(Content3);

            //layout1.Children.Add(dropdown1);


            buttonEnd = new Button
            {
                //Text = AppResources.IDM032,
                Text = "戻る",
                FontSize = 22,
                //VerticalOptions = LayoutOptions.Center,
                //            HorizontalOptions = LayoutOptions.Fill,
                            HorizontalOptions = LayoutOptions.Fill,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            layout1.Children.Add(buttonEnd);

            sv = new ScrollView { Content = layout1 };
            Content = sv;
            buttonEnd.Clicked += EndButtonClicked;

        }

    }

    private Color GetBackColor(int index)
    {
        Color wCol = Colors.White;
        if (lstKaisou._Datas[index]._during == 0)
        {
            //進行中
            wCol = Colors.White;
        }
        else if (lstKaisou._Datas[index]._during == 1)
        {
            wCol = Colors.LightGreen;
        }
        else if (lstKaisou._Datas[index]._during == 2)
        {
            wCol = Colors.Gray;
        }
        else if (lstKaisou._Datas[index]._during == 3)
        {
            wCol = Colors.DarkGreen;
        }
        else if (lstKaisou._Datas[index]._during == 4)
        {
            wCol = Colors.Red;
        }
        else if (lstKaisou._Datas[index]._during == 5)
        {
            wCol = GetBackColorParts();
        }

        return wCol;
    }
    private Color GetTextColor(int index)
    {
        Color wCol;
        if (lstKaisou._Datas[index]._parmit == 1)
        {
            //権限あり
            wCol = Colors.Black;
        }
        else
        {
            wCol = Colors.LightGray;
        }

        return wCol;
    }
    private Color GetBackColor(clsKaisou wKaisou)
    {
        Color wCol = Colors.White;
        if (wKaisou._during == 0)
        {
            //進行中
            wCol = Colors.White;
        }
        else if (wKaisou._during == 1)
        {
            wCol = Colors.LightGreen;
        }
        else if (wKaisou._during == 2)
        {
            wCol = Colors.Gray;
        }
        else if (wKaisou._during == 3)
        {
            wCol = Colors.DarkGreen;
        }
        else if (wKaisou._during == 4)
        {
            wCol = Colors.Red;
        }
        else if (wKaisou._during == 5)
        {
            wCol = GetBackColorParts();
        }
        return wCol;
    }
    private Color GetBackColorParts()
    {
        Color wCol = Colors.White;
        if (Device.RuntimePlatform == Device.iOS)
        {
            //wCol = Colors.DodgerBlue;
            wCol = Colors.Blue;
        }
        else
        {
            wCol = Colors.DodgerBlue;
            //wCol = Colors.Blue;

        }
        return wCol;
    }

    private Color GetTextColorParts()
    {
        Color wCol = Colors.White;
        if (Device.RuntimePlatform == Device.iOS)
        {
            //wCol = Colors.Black;
            wCol = Colors.White;
        }
        else
        {
            //wCol = Colors.White;
            wCol = Colors.Black;


        }
        return wCol;
    }

    private Color GetTextColor(clsKaisou wKaisou)
    {
        Color wCol;
        if (wKaisou._parmit == 1)
        {
            //権限あり
            wCol = Colors.Black;
        }
        else
        {
            wCol = Colors.Gray;
        }

        return wCol;
    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "SashizuAdd";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu

    async void ScanButtonClicked(object sender, EventArgs s)
    {
        //TAKO ここにQRスキャン実処理を入れる。
        // ↓added for QRScan
        clsGlobalVar.g_BackPage = "SashizuAdd";
        clsGlobalVar.g_QRRET = string.Empty;
        Application.Current.MainPage = new QRPage();
        // ↑added for QRScan

    }
    async void ItemButtonDelClicked(object sender, EventArgs s)
    {
        Button wBtn2 = (Button)sender;
        wBtn2.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int i = 0;
            foreach (Button wBtn in LstbuttonDel)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    string wSashizuNo = clsGlobalVar.g_SasizuNo;
                    string wSelectedSasizuNo = LstlabSashizu[i].Text.Trim();
                    string strErrMsg = "";
                    bool bRet = clsWebUpdate.SendAddDelSashizu(wSashizuNo, wSelectedSasizuNo, ref strErrMsg);
                    if (bRet == false)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM094, strErrMsg, "OK");
                        await DisplayAlert("指図番号追加・削除エラー", strErrMsg, "OK");
                    }
                    else
                    {
                        ReInit();
                    }
                    break;
                }
                i++;
            }
            doingNow = false;
        }
        wBtn2.IsEnabled = true;
    }
    private void freeThis()
    {
        Console.WriteLine("SashizuAddPage free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
        // added for popupmeneu
        if (buttonMenu != null)
        {
            buttonMenu.Clicked -= MenuButtonClicked;
            buttonMenu.ImageSource = null;
            buttonMenu = null;
        }
        if (labelUser != null) labelUser = null;
        if (ContentMenu != null) ContentMenu = null;
        // ↑added for popupmeneu

        if (LstlabSashizu != null)
        {
            int imax = LstlabSashizu.Count;
            for (int i = 0; i < imax; i++)
            {
                LstlabSashizu[i] = null;
            }
            LstlabSashizu.Clear();
            LstlabSashizu = null;
        }
        if (LstbuttonDel != null)
        {
            int imax = LstbuttonDel.Count;
            for (int i = 0; i < imax; i++)
            {
                LstbuttonDel[i].ImageSource = null;
                LstbuttonDel[i].Clicked -= ItemButtonDelClicked;
                LstbuttonDel[i] = null;
            }
            LstbuttonDel.Clear();
            LstbuttonDel = null;
        }
        if (LstLayout != null)
        {
            int imax = LstLayout.Count;
            for (int i = 0; i < imax; i++)
            {
                LstLayout[i] = null;
            }
            LstLayout.Clear();
            LstLayout = null;
        }
        label1 = null;
        label2 = null;
        label3 = null;
        if (dropdown1 != null)
        {
            dropdown1.Items.Clear();
            dropdown1 = null;
        }
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        layout1 = null;
        sv = null;
        Content = null;
        if (lstKaisou != null)
        {
            lstKaisou.freeThis();
            lstKaisou = null;
        }
        GC.Collect();
        Console.WriteLine("SashizuAddPage free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            clsGlobalVar.g_KaisouNo = 2;
            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", _LineIndex.ToString() };
            freeThis();
            //await Navigation.PushAsync(new Page2(yourData));
            Application.Current.MainPage = new Page2();
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    private void ReInit()
    {
        clsGlobalVar.g_KaisouNo = 2;
        //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", _LineIndex.ToString() };
        freeThis();
        //await Navigation.PushAsync(new Page2(yourData));
        Application.Current.MainPage = new SashizuAdd();
    }
    async void AddButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            if (dropdown1.SelectedIndex > -1)
            {
                doingNow = true;
                string wSashizuNo = clsGlobalVar.g_SasizuNo;
                string wSelectedSasizuNo = dropdown1.SelectedItem.ToString();
                string strErrMsg = "";
                bool bRet = clsWebUpdate.SendAddDelSashizu(wSashizuNo, wSelectedSasizuNo, ref strErrMsg);
                if (bRet == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM094, strErrMsg, "OK");
                    await DisplayAlert("指図番号追加・削除エラー", strErrMsg, "OK");
                }
                else
                {
                    ReInit();
                }
                doingNow = false;
            }
            else
            {
                //await DisplayAlert(AppResources.IDM094, AppResources.IDM096, "OK");
                await DisplayAlert("指図番号追加・削除エラー", "追加する指図番号を選択してください。", "OK");
            }
        }
        wBtn.IsEnabled = true;
    }

}