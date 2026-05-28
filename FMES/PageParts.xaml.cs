namespace FMES;

public partial class PageParts : ContentPage
{
    private clshinnmokuList lsthinnmoku;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private StackLayout ContentMenu;
    // ↑added for popupmeneu
    private List<Button> Lstbuttongopart = new List<Button>();

    private List<StackLayout> Lstlayout = new List<StackLayout>();
    private List<Label> LstName = new List<Label>();
    private List<Picker> LstPlace = new List<Picker>();
    private List<Entry> LstVal = new List<Entry>();
    private List<Label> LstUnit = new List<Label>();
    private Button buttonUpd;
    private Button buttonEnd;
    private StackLayout layout1;
    private ScrollView sv;


    private bool doingNow = false;
    private bool sleepStarted = false;

    public int _TotalTime { get; set; } = 0;
    public int _StartStop { get; set; } = 0;
    public bool _TimerStoped { get; set; } = false;
    //public int _LineIndex { get; set; } = -1;

    public PageParts()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        //        InitializeComponent();
        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new System.Globalization.CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 5;


        string srtErrMsg = string.Empty;
        lsthinnmoku = new clshinnmokuList();
        //clsGlobalVar.g_KouteiID = 1;    // テスト用

        if (lsthinnmoku.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {
            Label wlabel3 = new Label
            {
                Text = "test",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };

            layout1 = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Orientation = StackOrientation.Vertical,
            };
            //                layout1.Children.Add(wlabel3);
            //               //sv = new ScrollView { Content = layout1 };
            //                Content = layout1;
            //return;


            foreach (clshinmokuData whinnmokuData in lsthinnmoku._Datas)
            {


                //Lstlayout.Add(wlayout);

                Button butn = new Button
                {
                    Text = whinnmokuData._hinnmokuName,
                    FontSize = 22,
                    Margin = new Thickness(15, 15, 15, 15),
                    Padding = new Thickness(0, 0, 0, 0),
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetTextColorParts(),
                    BackgroundColor = GetBackColorParts(),

                };
                butn.Clicked += ItemButtonClicked;
                layout1.Children.Add(butn);
                Lstbuttongopart.Add(butn);




                StackLayout wlayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Colors.White,
                    Children ={
                        },
                };

                Lstlayout.Add(wlayout);
                layout1.Children.Add(wlayout);


            }
            buttonEnd = new Button
            {
                //Text = AppResources.IDM032,
                Text = "戻る",
                FontSize = 22,
                Margin = new Thickness(15, 15, 15, 15),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                //            HorizontalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
        }
        buttonEnd.Clicked += EndButtonClicked;
        layout1.Children.Add(buttonEnd);







        sv = new ScrollView { Content = layout1 };
        Content = sv;


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


    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "PageParts";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    private async void freeThis()
    {
        Console.WriteLine("PageParts free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        int imax = 0;
        if (LstPlace != null)
        {
            imax = LstPlace.Count;
            for (int i = 0; i < imax; i++)
            {
                LstPlace[i].Items.Clear();
                LstPlace[i] = null;
            }
            LstPlace.Clear();
            LstPlace = null;
        }
        if (LstUnit != null)
        {
            imax = LstUnit.Count;
            for (int i = 0; i < imax; i++)
            {
                LstUnit[i] = null;
            }
            LstUnit.Clear();
            LstUnit = null;
        }
        if (LstVal != null)
        {
            imax = LstVal.Count;
            for (int i = 0; i < imax; i++)
            {
                LstVal[i] = null;
            }
            LstVal.Clear();
            LstVal = null;
        }
        if (Lstlayout != null)
        {
            imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }

        if (Lstlayout != null)
        {
            imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }

        sv = null;
        Content = null;
        if (lsthinnmoku != null)
        {
            lsthinnmoku.freeThis();
            lsthinnmoku = null;
        }
        GC.Collect();
        Console.WriteLine("PageParts free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }
    async void ItemButtonClicked(object sender, EventArgs s)
    {
        int i = 0;
        if (doingNow == false)
        {
            doingNow = true;
            foreach (Button wBtn in Lstbuttongopart)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    //実行可能な物
                    clsGlobalVar.g_hinnmokuID = lsthinnmoku._Datas[i].hinnmokuID;
                    freeThis();
                    Application.Current.MainPage = new PageParts2();
                    break;
                }
                i++;
            }
            doingNow = false;

        }
    }

    async void EndButtonClicked(object sender, EventArgs s)
    {
        //アップデート後 jump 設定ページへ
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {



            //ページをジャンプ
            //clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            string strJump = clsGlobalVar.g_JumpPage;
            clsGlobalVar.g_JumpPage = string.Empty;
            freeThis();
            if (strJump == "Page1")
            {
                Application.Current.MainPage = new Page1();
            }
            else if (strJump == "PageParts")
            {
                Application.Current.MainPage = new PageParts();

            }
            else if (strJump == "PageParts2")
            {
                Application.Current.MainPage = new PageParts2();

            }
            else if (strJump == "Page2")
            {
                Application.Current.MainPage = new Page2();

            }
            else if (strJump == "Page3")
            {
                Application.Current.MainPage = new Page3();

            }
            else if (strJump == "Page4")
            {
                Application.Current.MainPage = new Page4();

            }
            else if (strJump == "Page1_5")
            {
                Application.Current.MainPage = new Page1_5();

            }
            else if (strJump == "PageNo")
            {
                Application.Current.MainPage = new PageNo();

            }
            else if (strJump == "SashizuAdd")
            {
                Application.Current.MainPage = new SashizuAdd();

            }
            else if (strJump == "SashizuPage")
            {
                Application.Current.MainPage = new SashizuPage();

            }
            else
            {
                Application.Current.MainPage = new Page1();

            }

        }
    }

}