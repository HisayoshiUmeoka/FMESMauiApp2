using System.Globalization;

namespace FMES;

public partial class Page1_5 : ContentPage
{
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu
    private clsVerList lstVer;

    private Label label1;
    private List<StackLayout> Lstlayout = new List<StackLayout>();
    private List<Button> Lstbutton = new List<Button>();
    private List<Button> Lstbutton2 = new List<Button>();
    private StackLayout layout1;
    private Button buttonEnd;
    private ScrollView sv;

    private bool doingNow = false;

    public Page1_5()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 4;

        lstVer = new clsVerList();
        string strError = string.Empty;
        if (lstVer.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, ref strError) == true)
        {
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

            //_SasizuID = lstVer._SashizuID;

            label1 = new Label
            {
                Text = "指図番号：" + clsGlobalVar.g_SasizuNo,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };

            layout1 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
            };
            foreach (clsVerData wVerData in lstVer._Datas)
            {
                Button butn = new Button
                {
                    Text = wVerData._KouteiName,
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.White,

                };
                Button butn2 = new Button
                {
                    Text = (wVerData._KouteiStatus == 0) ? "○" : "×",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = GetTextColor(wVerData._KouteiStatus),
                    BackgroundColor = GetBackColor(wVerData._KouteiStatus),
                };
                StackLayout layW = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            butn2,
                            butn,
                        }
                };
                butn.Clicked += ItemButtonClicked;
                butn2.Clicked += ItemButtonClicked2;

                layout1.Children.Add(ContentMenu);

                layout1.Children.Add(layW);
                Lstbutton.Add(butn);
                Lstbutton2.Add(butn2);
                Lstlayout.Add(layW);
            }
            buttonEnd = new Button
            {
                //Text = AppResources.IDM032,
                Text = "戻る",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(10, 10, 10, 10),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonEnd.Clicked += EndButtonClicked;
            layout1.Children.Add(buttonEnd);

            sv = new ScrollView { Content = layout1 };
            Content = sv;
        }
        else
        {
            label1 = new Label
            {
                //Text = "　" + AppResources.IDM027,
                Text = "　" + "データエラー（又は通信エラー）",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            buttonEnd = new Button
            {
                //Text = AppResources.IDM032,
                Text = "戻る",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(10, 10, 10, 10),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonEnd.Clicked += EndButtonClicked;
            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Children = {
                                            ContentMenu,
                label1,
                    buttonEnd,
                    }
            };
        }


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


    private Color GetBackColor(int wStatus)
    {
        Color wCol = Colors.White;
        if (wStatus == 0)
        {
            wCol = GetBackColorParts();
        }
        else
        {
            wCol = Colors.Red;
        }
        return wCol;
    }
    private Color GetTextColor(int wStatus)
    {
        Color wCol = Colors.Black;
        if (wStatus == 0)
        {
            wCol = GetTextColorParts();
        }
        else
        {
            wCol = Colors.White;
        }
        return wCol;
    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page1_5";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu


    async void ItemButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_KaisouNo = 2;
        int i = 0;
        if (doingNow == false)
        {
            doingNow = true;
            foreach (Button wBtn in Lstbutton)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    //実行可能な物
                    //_KouteiID = lstKaisou._Datas[i]._KouteiID;
                    clsGlobalVar.g_KouteiVer = lstVer._Datas[i]._KouteiVer;
                    freeThis();
                    //await Navigation.PushAsync(new Page2(yourData));
                    Application.Current.MainPage = new Page1();
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }
    async void ItemButtonClicked2(object sender, EventArgs s)
    {
        clsGlobalVar.g_KaisouNo = 2;
        int i = 0;
        if (doingNow == false)
        {
            doingNow = true;
            foreach (Button wBtn in Lstbutton2)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    string strErrMsg = "";
                    if (clsWebUpdate.SendProcessStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiVer, ref strErrMsg) == 1)
                    {
                        //await DisplayAlert(AppResources.IDM109, strErrMsg, "OK");
                        await DisplayAlert("工程バージョンエラー", strErrMsg, "OK");
                    }
                    lstVer._Datas[i]._KouteiStatus = (lstVer._Datas[i]._KouteiStatus == 0) ? 1 : 0;
                    wBtn.Text = (lstVer._Datas[i]._KouteiStatus == 0) ? "○" : "×";
                    wBtn.TextColor = GetTextColor(lstVer._Datas[i]._KouteiStatus);
                    wBtn.BackgroundColor = GetBackColor(lstVer._Datas[i]._KouteiStatus);
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            clsGlobalVar.g_KaisouNo = 1;
            //string[] yourData = { _UserID.ToString(), _SasizuNo, _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString() };
            freeThis();
            //await Navigation.PushAsync(new Page1(yourData));
            Application.Current.MainPage = new Page1();
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    private async void freeThis()
    {
        Console.WriteLine("Page1_5 free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
        if (Lstbutton != null)
        {
            int imax = Lstbutton.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstbutton[i].Clicked -= ItemButtonClicked;
                Lstbutton[i] = null;
            }
            Lstbutton.Clear();
            Lstbutton = null;
        }
        if (Lstbutton2 != null)
        {
            int imax = Lstbutton2.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstbutton2[i].Clicked -= ItemButtonClicked2;
                Lstbutton2[i] = null;
            }
            Lstbutton2.Clear();
            Lstbutton2 = null;
        }
        if (Lstlayout != null)
        {
            int imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }
        label1 = null;
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }

        if (lstVer != null)
        {
            lstVer.freeThis();
            lstVer = null;
        }
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

        layout1 = null;
        sv = null;
        Content = null;
        GC.Collect();
        Console.WriteLine("Page1_5free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }

}