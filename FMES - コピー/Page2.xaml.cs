using System.Globalization;
using System.Text.RegularExpressions;

namespace FMES;

public partial class Page2 : ContentPage
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
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label labelSashizu;
    private Button buttonAddSashizu;
    private Button buttonSS;
    private List<Button> Lstbutton = new List<Button>();
    private Picker dropdown1;
    private Button buttonPass;
    private Button buttonPass3;
    private Button buttonUpd;
    //private Button buttonUpd2;
    private Button buttonUpd3;
    private Button buttonUpd5;
    private Button buttonEnd;
    private Button buttonParts;
    private Button buttonRead;
    private Button buttonRead2;
    private Button buttonFin;
    private Button buttonFin2;
    //        private Button buttonOCR;
    private Entry txtVal1;
    private AbsoluteLayout absLay;
    private Image imgView;
    private StackLayout layout1;
    private ScrollView sv;
    private Button buttonProStart;

    private List<Entry> LstVal = new List<Entry>();

    private bool doingNow = false;
    private bool sleepStarted = false;

    public int _TotalTime { get; set; } = 0;
    public int _StartStop { get; set; } = 0;
    public bool _TimerStoped { get; set; } = false;
    //public int _LineIndex { get; set; } = -1;

    public Page2()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 5;
        string wwsashizuNo = clsGlobalVar.g_SasizuNo;
        if (clsGlobalVar.g_SasizuNo == "-2")
        {
            wwsashizuNo = "指図番号無し作業";
        }
        else if (clsGlobalVar.g_SasizuNo == "-1")
        {
            wwsashizuNo = "その他";
        }
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

        //テスト用
        //clsGlobalVar.g_KouteiID = 1;    // テスト用
        //freeThis();
        //Application.Current.MainPage = new PageParts2();
        //Application.Current.MainPage = new Page1();
        //DispPartsPage("Page1");
        //return;
        //テスト用ここまで

        string srtErrMsg = string.Empty;
        lstKaisou = new clsKaisouList();
        if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {

            _TotalTime = lstKaisou._Header._TotalSec;
            _StartStop = lstKaisou._Header._StopWatch;
            _TimerStoped = (_StartStop == 1) ? false : true;
            clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;
            if (lstKaisou._Header._GamenKind == 1)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };

                if (clsGlobalVar.g_ActMode == 0)
                {
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
                        //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                        Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(0, 0, 0, 0),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 16,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
                    };
                }
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    //string strFinsh = (clsGlobalVar.g_SasizuNo.CompareTo("-1") != 0) ? AppResources.IDM031 : AppResources.IDM116;
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    //layout1.Children.Add(buttonParts);

                    buttonFin = new Button
                    {
                        //Text = (clsGlobalVar.g_ActMode != -1) ? AppResources.IDM031 : AppResources.IDM116,
                        Text = (clsGlobalVar.g_ActMode != -1) ? "工程終了" : "終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                }

                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };


                buttonParts = new Button
                {
                    //Text = AppResources.IDM147, //（旧）部品ページへ
                    Text = "部品／材料", //（旧）部品ページへ
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                buttonParts.Clicked += PartsButtonClicked;


                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_ActMode == 0)
                    {
                        if (clsGlobalVar.g_PartDisp == 1)
                        {

                            Content = new StackLayout
                            {
                                Padding = new Thickness(10, 10, 10, 10),
                                BackgroundColor = Colors.White,
                                Children = {
                                    ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    label4,
                                    buttonSS,
                                    buttonProStart,
                                buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                            };
                        }
                        else
                        {
                            Content = new StackLayout
                            {
                                Padding = new Thickness(10, 10, 10, 10),
                                BackgroundColor = Colors.White,
                                Children = {
                                    ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    label4,
                                    buttonSS,
                                    //buttonProStart,
                                buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                            };

                        }
                    }
                    else
                    {
                        if (clsGlobalVar.g_PartDisp == 1)
                        {
                            Content = new StackLayout
                            {
                                Padding = new Thickness(10, 10, 10, 10),
                                BackgroundColor = Colors.White,
                                Children = {
                                    ContentMenu,
               label1,
                                    //label2,
                                    //label3,
                                    label4,
                                    buttonSS,
                                    buttonProStart,
                                    buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                            };
                        }
                        else
                        {
                            Content = new StackLayout
                            {
                                Padding = new Thickness(10, 10, 10, 10),
                                BackgroundColor = Colors.White,
                                Children = {
                                    ContentMenu,
               label1,
                                    //label2,
                                    //label3,
                                    label4,
                                    buttonSS,
                                    buttonProStart,
                                    //buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                            };

                        }
                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    if (clsGlobalVar.g_ActMode == 0)
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                    ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    //label4,
                                    //buttonSS,
                                    //buttonFin,
                                    buttonEnd,
                                }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                    ContentMenu,
                                    label1,
                                    //label2,
                                    //label3,
                                    //label4,
                                    //buttonSS,
                                    //buttonFin,
                                    buttonEnd,
                                }
                        };
                    }
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    layout1.Children.Add(label4);
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    layout1.Children.Add(buttonSS);
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                dropdown1 = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM033,
                    Title = "ライン選択",
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsLine wLine in lstKaisou._Header._LineLists)
                {
                    dropdown1.Items.Add(wLine._LineName);
                    if (wLine._index == lstKaisou._Header._SelSelected)
                    {
                        dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                    }
                }
                if (clsGlobalVar.g_LineIndex > -1)
                {
                    dropdown1.SelectedIndex = clsGlobalVar.g_LineIndex;
                }
                layout1.Children.Add(dropdown1);

                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
                //20220130部品／材料ボタン追加 18
                buttonParts = new Button
                {
                    //Text = AppResources.IDM147,
                    Text = "部品／材料",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                buttonParts.Clicked += PartsButtonClicked;

                layout1.Children.Add(buttonParts);

                label7 = new Label
                {
                    //Text = AppResources.IDM129, //登録済指図番号："
                    Text = "登録済指図番号：",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.End,
                };
                string wLst = string.Empty;
                foreach (clsLine wLine in lstKaisou._Header._SelLists)
                {
                    if (wLine._index == 1)
                    {
                        wLst += wLine._LineName + "\n";
                    }
                }
                if (string.IsNullOrEmpty(wLst) == true)
                {
                    wLst = "未登録";
                }
                labelSashizu = new Label
                {
                    Text = wLst,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };
                //layout1.Children.Add(ContentMenu);

                StackLayout Content2 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            label7,
                            labelSashizu,
                        }
                };
                layout1.Children.Add(Content2);


                if (lstKaisou._Header._done == 0)
                {
                    buttonAddSashizu = new Button
                    {
                        //Text = AppResources.IDM128,
                        Text = "指図番号追加・削除",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonAddSashizu);
                    buttonAddSashizu.Clicked += AddButtonClicked;
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };


                    buttonProStart.Clicked += ProStartButtonClicked;
                    layout1.Children.Add(buttonProStart);

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    layout1.Children.Add(buttonParts);

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonFin);
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };


                layout1.Children.Add(buttonEnd);

                if (lstKaisou._Header._done == 0)
                {
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                }
                sv = new ScrollView { Content = layout1 };
                Content = sv;
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {
                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Vertical,
                };

                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 20,
                        //HeightRequest = 220,
                        //WidthRequest = 220,

                        //ImageSource = "START.png",
                        VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.White,
                        //BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    layout1.Children.Add(label4);
                    layout1.Children.Add(buttonSS);
                }
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;
                    layout1.Children.Add(buttonProStart);


                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    layout1.Children.Add(buttonParts);


                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonFin);
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
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
            else if (lstKaisou._Header._GamenKind == 4)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        //Image = "START.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    //Text = "　" + AppResources.IDM060 + "　",
                    Text = "　" + "合格" + "　",
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    FontSize = 22,
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                                HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetTextColorParts(),
                    BackgroundColor = GetBackColorParts(),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    FontSize = 22,
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                                HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {

                    if (clsGlobalVar.g_PartDisp == 1)
                    {

                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonProStart,
                                buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonProStart,
                                //buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };

                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonPass.Clicked += PassButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //label4,
                                //buttonSS,
                                label5,
                                buttonPass,
                                //buttonUpd,
                                //buttonFin,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //Image = "START.png",
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        Text = GetUpdButtonStr(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    buttonUpd5 = new Button
                    {
                        Text = GetUpdButtonStr(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonUpd5.Clicked += UpdButtonClicked5;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_PartDisp == 1)
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    label4,
                                    buttonSS,
                                    label5,
                                    buttonPass,
                                    buttonUpd,
                                    buttonProStart,
                                    buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    label4,
                                    buttonSS,
                                    label5,
                                    buttonPass,
                                    buttonUpd,
                                    buttonProStart,
                                    //buttonParts,
                                    buttonFin,
                                    buttonEnd,
                                }
                        };

                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonPass.Clicked += PassButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                    label1,
                                    label2,
                                    label3,
                                    //label4,
                                    //buttonSS,
                                    label5,
                                    buttonPass,
                                    //buttonUpd,
                                    //buttonFin,
                                    buttonEnd,
                                }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 6)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.End,
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    dropdown1 = new Picker
                    {
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 26,
                        Title = "指数選択",
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                        if (wLine._index == lstKaisou._Header._SelSelected)
                        {
                            dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                        }
                    }
                }
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2;
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                                ContentMenu,
                                txtVal1,
                                dropdown1,
                                label6,
                            }
                    };
                }
                else
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                                ContentMenu,
                                txtVal1,
                                //dropdown1,
                                label6,
                            }
                    };
                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_PartDisp == 1)
                    {

                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                Content2,
                                buttonUpd,
                                buttonProStart,
                                buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                Content2,
                                buttonUpd,
                                buttonProStart,
                                //buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };

                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //label4,
                                //buttonSS,
                                label5,
                                Content2,
                                //buttonUpd,
                                //buttonFin,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        //Image = "START.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                                HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(20, 0, 20, 0),
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    //Text = lstKaisou._Header._strVal,
                    Text = ConvStr2Disp(lstKaisou._Header._strVal),
                };

                if (lstKaisou._Header._done == 0)
                {
                    layout1 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                                txtVal1,
//                                buttonOCR,
                            }
                    };
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;


                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_PartDisp == 1)
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                layout1,
                                buttonUpd,
                                buttonProStart,
                                buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                layout1,
                                buttonUpd,
                                buttonProStart,
                                //buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };

                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //label4,
                                //buttonSS,
                                label5,
                                txtVal1,
                                //buttonUpd,
                                //buttonFin,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        //Image = "START.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                dropdown1 = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM040,
                    Title = "未選択",
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    dropdown1.Items.Add(wKaisou._kaisouName);
                }
                dropdown1.SelectedIndex = GetCurSelectedDropDown(lstKaisou._Header._strCmb);
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    //Text = "　" + AppResources.IDM060 + "　",
                    Text = "　" + "合格" + "　",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetTextColorParts(),
                    BackgroundColor = GetBackColorParts(),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_PartDisp == 1)
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                dropdown1,
                                buttonUpd,
                                buttonProStart,
                                buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label4,
                                buttonSS,
                                label5,
                                dropdown1,
                                buttonUpd,
                                buttonProStart,
                                //buttonParts,
                                buttonFin,
                                buttonEnd,
                            }
                        };

                    }
                    buttonSS.Clicked += SSButtonClicked;
                    buttonPass.Clicked += PassButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //label4,
                                //buttonSS,
                                label5,
                                dropdown1,
                                //buttonUpd,
                                //buttonFin,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                absLay = new AbsoluteLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,

                };
                var uri = clsGlobalVar.GetCurURL() + "img/instruction/" + lstKaisou._Header._ImageFile;
                imgView = new Image
                {
                    Source = ImageSource.FromUri(new Uri(uri)),
                    HorizontalOptions = LayoutOptions.Center,
                };
                int z = 0;
                absLay.Children.Add(imgView);
                //imgView.ZIndex = 0;
                z++;
                absLay.SetLayoutFlags(imgView, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
                absLay.SetLayoutBounds(imgView, new Rect(0 / 2, 0 / 2, 1500, 1500));
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 12,
                        //WidthRequest = 50,
                        ZIndex = ++z,
                        WidthRequest = 12 * wKaisou._kaisouName.Length,

                        BackgroundColor = GetPassButtonBColor(wKaisou._iPass),
                        TextColor = GetPassButtonTColor(wKaisou._iPass),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,

                        //TextColor = GetTextColor(wKaisou),
                        //BackgroundColor = GetBackColor(wKaisou),
                    };
                    butn.Clicked += ItemButtonClicked;
                    Lstbutton.Add(butn);
                    //absLay.SetLayoutFlags(butn, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
                    absLay.SetLayoutFlags(butn, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.None);
                    absLay.SetLayoutBounds(butn, new Rect((wKaisou._IconButton.X + 700) / 1772, (wKaisou._IconButton.Y + 500) / 1772, (12 * wKaisou._kaisouName.Length), 70));
                    //absLay.SetLayoutBounds(imgView, new Rect(0 / 2, 0 / 2, 1500, 1500));
                    absLay.Children.Add(butn);
                    //butn.ZIndex = ++z ;
                    //butn.TranslateTo(0, 0);
                    //                    butn.Opacity = 0;
                    //                  butn.FadeTo(1, 4000);
                    //butn.RelScaleTo(250);
                    //butn.TranslateTo(0, 0);
                    //butn.TranslateTo(wKaisou._IconButton.X / 2, wKaisou._IconButton.Y / 2);
                    //一応これで表示出来る筈　後で要調整！




                    //absLay.Children.Add(butn, new Point(wKaisou._IconButton.X / 2, wKaisou._IconButton.Y / 2));
                }

                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                    ZIndex = ++z,

                };

                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Children = {
                            ContentMenu,
                            label1,
                            label2,
                            label3,
                            buttonEnd,
                            absLay,
                            //buttonEnd,
                        }
                };
                sv = new ScrollView { Content = layout1 };
                Content = sv;
                //imgView.Clicked += 
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 10)
            {
                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Vertical,
                };

                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                //layout1.Children.Add(ContentMenu);
                layout1.Children.Add(label1);
                layout1.Children.Add(label2);
                layout1.Children.Add(label3);
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //Image = "START.png",
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    layout1.Children.Add(ContentMenu);
                    layout1.Children.Add(label4);
                    layout1.Children.Add(buttonSS);
                }
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
                buttonPass3 = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                layout1.Children.Add(buttonPass3);
                if (lstKaisou._Header._done == 0)
                {
                    buttonPass3.Clicked += PassButtonClicked3;

                    buttonUpd3 = new Button
                    {
                        Text = GetUpdButtonStr3(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonUpd3.IsEnabled = (lstKaisou._Header._iPass == -1) ? false : true;
                    layout1.Children.Add(buttonUpd3);
                    buttonUpd3.Clicked += UpdButtonClicked3;
                    buttonSS.Clicked += SSButtonClicked;
                    //buttonFin.Clicked += FinButtonClicked;
                }
                else if (lstKaisou._Header._iPass == 0)
                {
                    buttonUpd5 = new Button
                    {
                        Text = GetUpdButtonStr3(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonUpd5);
                    buttonUpd5.Clicked += UpdButtonClicked5;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
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
            else if (lstKaisou._Header._GamenKind == 12)
            {
                //ロット分
                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Vertical,
                };

                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //Image = "START.png",
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    layout1.Children.Add(label4);
                    layout1.Children.Add(buttonSS);
                }
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    StackLayout layoutRow = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Orientation = StackOrientation.Horizontal,
                    };
                    Label lbName = new Label
                    {
                        Text = "　" + wKaisou._kaisouName,
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 16,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                    };
                    layoutRow.Children.Add(lbName);
                    if (lstKaisou._Header._done == 0)
                    {
                        Entry entLot = new Entry
                        {
                            Keyboard = Keyboard.Text,
                            BackgroundColor = Colors.White,
                            TextColor = Colors.Black,
                            FontSize = 26,
                            //HorizontalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Fill,
                            HorizontalTextAlignment = TextAlignment.End,
                            Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                            Text = "0",
                        };
                        layoutRow.Children.Add(entLot);
                        LstVal.Add(entLot);
                    }
                    else
                    {
                        Label lbVal = new Label
                        {
                            Text = wKaisou._LotLeft.ToString(),
                            BackgroundColor = Colors.White,
                            TextColor = Colors.Black,
                            FontSize = 16,
                            VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Fill,
                        };
                        layoutRow.Children.Add(lbVal);
                    }

                    Label lbTotal = new Label
                    {
                        Text = " / " + wKaisou._LotLeft.ToString(),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 16,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                    };
                    layoutRow.Children.Add(lbTotal);
                    layout1.Children.Add(layoutRow);
                }

                if (lstKaisou._Header._done == 0)
                {
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;
                    layout1.Children.Add(buttonProStart);

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    layout1.Children.Add(buttonParts);

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonFin);
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked2;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
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
            else if (lstKaisou._Header._GamenKind == 15)
            {
                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Vertical,
                };

                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //Image = "START.png",
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    layout1.Children.Add(label4);
                    layout1.Children.Add(buttonSS);
                }
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
                buttonRead = new Button
                {
                    //Text = AppResources.IDM114,
                    Text = "読み込み",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                layout1.Children.Add(buttonRead);
                buttonRead.Clicked += ReadButtonClicked;


                if (lstKaisou._Header._done == 0)
                {
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;
                    layout1.Children.Add(buttonProStart);

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    layout1.Children.Add(buttonParts);

                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonFin);
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
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
            else if (lstKaisou._Header._GamenKind == 16)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                //                    label2 = new Label
                //                    {
                //                        Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                //                        FontSize = 16,
                //                        VerticalOptions = LayoutOptions.Center,
                //                                    HorizontalOptions = LayoutOptions.Fill,
                //                    };
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.End,
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                        txtVal1,
                        label6,
                    }
                };

                if (lstKaisou._Header._done == 0)
                {
                    //                        buttonUpd = new Button
                    //                        {
                    //                            Text = AppResources.IDM038,
                    //                            FontSize = 22,
                    //                            //VerticalOptions = LayoutOptions.Center,
                    //                            //            HorizontalOptions = LayoutOptions.Fill,
                    //                                        HorizontalOptions = LayoutOptions.Fill,
                    //                            TextColor = Colors.Black,
                    //                            BackgroundColor = Colors.LightGreen,
                    //                        };
                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    //layout1.Children.Add(buttonParts);

                    buttonFin2 = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    if (clsGlobalVar.g_PartDisp == 1)
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                //label2,
                                //label3,
                                label4,
                                buttonSS,
                                label5,
                                Content2,
                                //buttonUpd,
                                buttonProStart,

                                buttonParts,
                                buttonFin2,
                                buttonEnd,
                            }
                        };
                    }
                    else
                    {
                        Content = new StackLayout
                        {
                            Padding = new Thickness(10, 10, 10, 10),
                            BackgroundColor = Colors.White,
                            Children = {
                                ContentMenu,
                                label1,
                                //label2,
                                //label3,
                                label4,
                                buttonSS,
                                label5,
                                Content2,
                                //buttonUpd,
                                buttonProStart,

                                //buttonParts,
                                buttonFin2,
                                buttonEnd,
                            }
                        };

                    }

                    buttonSS.Clicked += SSButtonClicked;
                    //buttonUpd.Clicked += UpdButtonClicked;
                    buttonFin2.Clicked += FinButtonClicked3;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                //label2,
                                //label3,
                                //label4,
                                //buttonSS,
                                label5,
                                Content2,
                                //buttonUpd,
                                //buttonFin,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 17)
            {
                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    layout1.Children.Add(label4);
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    layout1.Children.Add(buttonSS);
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                }
                dropdown1 = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM033,
                    Title = "ライン選択",
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsLine wLine in lstKaisou._Header._LineLists)
                {
                    dropdown1.Items.Add(wLine._LineName);
                    if (wLine._index == lstKaisou._Header._SelSelected)
                    {
                        dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                    }
                }
                if (clsGlobalVar.g_LineIndex > -1)
                {
                    dropdown1.SelectedIndex = clsGlobalVar.g_LineIndex;
                }
                layout1.Children.Add(dropdown1);

                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }

                label7 = new Label
                {
                    Text = "　登録済指図番号：",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.End,
                };
                string wLst = string.Empty;
                foreach (clsLine wLine in lstKaisou._Header._SelLists)
                {
                    if (wLine._index == 1)
                    {
                        wLst += wLine._LineName + "\n";
                    }
                }
                if (string.IsNullOrEmpty(wLst) == true)
                {
                    wLst = "未登録";
                }
                labelSashizu = new Label
                {
                    Text = wLst,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };

                StackLayout Content2 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            label7,
                            labelSashizu,
                        }
                };
                layout1.Children.Add(Content2);


                if (lstKaisou._Header._done == 0)
                {
                    buttonAddSashizu = new Button
                    {
                        //Text = AppResources.IDM128,
                        Text = "指図番号追加・削除",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonAddSashizu);
                    buttonAddSashizu.Clicked += AddButtonClicked;

                    buttonRead2 = new Button
                    {
                        //Text = AppResources.IDM114,
                        Text = "読み込み",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonRead2);
                    buttonRead2.Clicked += ReadButtonClicked2;

                    buttonProStart = new Button
                    {
                        //Text = AppResources.IDM144,
                        Text = "工程開始",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };

                    buttonProStart.Clicked += ProStartButtonClicked;
                    layout1.Children.Add(buttonProStart);

                    buttonParts = new Button
                    {
                        //Text = AppResources.IDM147,
                        Text = "部品／材料",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonParts.Clicked += PartsButtonClicked;

                    layout1.Children.Add(buttonParts);


                    buttonFin = new Button
                    {
                        //Text = AppResources.IDM031,
                        Text = "工程終了",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonFin);
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                layout1.Children.Add(buttonEnd);

                if (lstKaisou._Header._done == 0)
                {
                    buttonSS.Clicked += SSButtonClicked;
                    buttonFin.Clicked += FinButtonClicked;
                }
                else
                {
                }
                sv = new ScrollView { Content = layout1 };
                Content = sv;
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 18)
            {
                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Vertical,
                };

                // StackLayoutで2つの Entryコントロールを並べる
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
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
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
                if (lstKaisou._Header._done == 0)
                {
                    label4 = new Label
                    {
                        //Text = "00:00",
                        Text = GetDispTime(lstKaisou._Header._TotalSec),
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 25,
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    buttonSS = new Button
                    {
                        //Text = (lstKaisou._Header._StopWatch == 0) ? "START" : "STOP",
                        ImageSource = (lstKaisou._Header._StopWatch == 0) ? "START.png" : "STOP.png",
                        FontSize = 22,
                        //Image = "START.png",
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White,
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.White,
                        //BackgroundColor = Colors.White,
                        //BackgroundColor = (lstKaisou._Header._StopWatch == 0) ? GetBackColorParts() : Colors.Red,
                    };
                    if (lstKaisou._Header._StopWatch == 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
                    }
                    layout1.Children.Add(label4);
                    layout1.Children.Add(buttonSS);
                }
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }

                if (lstKaisou._Header._done == 0)
                {
                    buttonRead = new Button
                    {
                        //Text = AppResources.IDM114,
                        Text = "読み込み",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonRead);
                    buttonRead.Clicked += ReadButtonClicked;
                }
                buttonPass3 = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                layout1.Children.Add(buttonPass3);
                //20220130部品／材料ボタン追加 18
                buttonParts = new Button
                {
                    //Text = AppResources.IDM147,
                    Text = "部品／材料",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                buttonParts.Clicked += PartsButtonClicked;

                layout1.Children.Add(buttonParts);

                if (lstKaisou._Header._done == 0)
                {
                    buttonPass3.Clicked += PassButtonClicked3;

                    buttonUpd3 = new Button
                    {
                        Text = GetUpdButtonStr3(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonUpd3.IsEnabled = (lstKaisou._Header._iPass == -1) ? false : true;
                    layout1.Children.Add(buttonUpd3);
                    buttonUpd3.Clicked += UpdButtonClicked3;
                    buttonSS.Clicked += SSButtonClicked;
                    //buttonFin.Clicked += FinButtonClicked;
                }
                else if (lstKaisou._Header._iPass == 0)
                {
                    buttonUpd5 = new Button
                    {
                        Text = GetUpdButtonStr3(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonUpd5);
                    buttonUpd5.Clicked += UpdButtonClicked5;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
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
            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Children = {
                    label1,
                    }
            };
        }





    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page2";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    private void freeThis()
    {
        GC.Collect();
    }
    private Color GetBackColorParts()
    {
        Color wCol = Colors.White;
#if IOS
        //wCol = Colors.DodgerBlue;
        wCol = Colors.Blue;
#else
        wCol = Colors.DodgerBlue;
        //wCol = Colors.Blue;
#endif

        return wCol;
    }

    private Color GetTextColorParts()
    {
        Color wCol = Colors.White;
#if IOS
        //wCol = Colors.Black;
        wCol = Colors.White;
#else
        //wCol = Colors.White;
        wCol = Colors.Black;

#endif

        return wCol;
    }

    async void SSButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (_StartStop == 0)
            {
                //buttonSS.Text = "STOP";
                buttonSS.ImageSource = "STOP.png";
                //buttonSS.BackgroundColor = Colors.Red;
                _StartStop = 1;
                _TimerStoped = false;
                clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
                Device.StartTimer(TimeSpan.FromSeconds(1), TimerEvent);
            }
            else
            {
                //buttonSS.Text = "START";
                buttonSS.ImageSource = "START.png";
                //buttonSS.BackgroundColor = Colors.Blue;
                _StartStop = 0;
                clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
            }
            doingNow = false;
        }
    }
    private bool TimerEvent()
    {
        if (_StartStop == 0 || sleepStarted == true)
        {
            _TimerStoped = true;
            sleepStarted = false;
            return false;
        }
        // カウントをインクリメント
        _TotalTime++;

        // カウントをラベルのテキストに設定
        if (label4 != null)
        {
            label4.Text = GetDispTime(_TotalTime);
        }
        return true;
    }

    async void ItemButtonClicked(object sender, EventArgs s)
    {
        int i = 0;
        if (doingNow == false)
        {
            if ((_StartStop == 1 && lstKaisou._Header._done == 0) || lstKaisou._Header._done == 1)
            {
                doingNow = true;
                foreach (Button wBtn in Lstbutton)
                {
                    if (wBtn.GetHashCode() == sender.GetHashCode())
                    {
                        if (lstKaisou._Datas[i]._parmit == 1)
                        {
                            //実行可能な物
                            clsGlobalVar.g_KaisouNo = 3;
                            clsGlobalVar.g_KouteiID = lstKaisou._Datas[i]._KouteiID;
                            clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                            //clsGlobalVar.g_KensaBashoID = 0;
                            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), cnf._svUrl.ToString(), cnf._language.ToString(), cnf._logWrite.ToString(), cnf._urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                            freeThis();
                            //await Navigation.PushAsync(new Page3(yourData));
                            Application.Current.MainPage = new Page3();
                        }
                        break;
                    }
                    i++;
                }
                doingNow = false;
            }
        }
    }
    async void UpdButtonClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            //if (_StartStop == 1)
            if ((_StartStop == 1 && lstKaisou._Header._done == 0) || (clsGlobalVar.g_KouteiKekkaID > 0 && lstKaisou._Header._done == 0))
            {
                int iPass = -1;
                decimal dPara = -999999;
                string strPara = string.Empty;
                string strCombo = string.Empty;
                int iSelectedID = 0;
                doingNow = true;
                if (lstKaisou._Header._GamenKind == 1)
                {

                }
                else if (lstKaisou._Header._GamenKind == 2 || lstKaisou._Header._GamenKind == 17)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                        await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
                else if (lstKaisou._Header._GamenKind == 3)
                {

                }
                else if (lstKaisou._Header._GamenKind == 4)
                {
                    iPass = lstKaisou._Header._iPass;
                }
                else if (lstKaisou._Header._GamenKind == 5)
                {
                    iPass = lstKaisou._Header._iPass;
                }
                else if (lstKaisou._Header._GamenKind == 6)
                {
                    if (string.IsNullOrEmpty(txtVal1.Text) == true)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                        await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else if (CheckNumberChar3(txtVal1.Text) == false)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                        await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        try
                        {
                            dPara = decimal.Parse(txtVal1.Text);
                        }
                        catch (Exception)
                        {
                            //throw;
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                            await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                            txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                    }
                    if (dropdown1 != null)
                    {
                        if (dropdown1.SelectedIndex == -1)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                            await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                            txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else
                        {
                            int iWkNo = 0;
                            foreach (clsLine wLine in lstKaisou._Header._LineLists)
                            {
                                if (iWkNo == dropdown1.SelectedIndex)
                                {
                                    iSelectedID = wLine._index;
                                    break;
                                }
                                iWkNo++;
                            }
                        }
                    }
                }
                else if (lstKaisou._Header._GamenKind == 7)
                {
                    if (string.IsNullOrEmpty(txtVal1.Text) == true)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                        await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else if (CheckHankakuChar(txtVal1.Text) == false)
                    {
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                        await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        strPara = ConvStr2Webserver(txtVal1.Text);
                    }
                }
                else if (lstKaisou._Header._GamenKind == 8)
                {
                    int iIndex = dropdown1.SelectedIndex;
                    if (iIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                        await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + clsGlobalVar.g_KensaBashoID;
                    }
                }
                else if (lstKaisou._Header._GamenKind == 9)
                {

                }

                string strErrMsg = "";
                bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, clsGlobalVar.g_KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
                if (bRet == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                    await DisplayAlert("更新エラー", strErrMsg, "OK");
                }
                else
                {
                    if (lstKaisou._Header._GamenKind == 5 && lstKaisou._Header._iPass == 0)
                    {
                        clsGlobalVar.g_KaisouNo = 3;
                        //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Datas[0]._KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                        clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[0]._KouteiShousaiID;
                        clsGlobalVar.g_KensaBashoID = 0;
                        clsGlobalVar.g_LineIndex = GetSelectedLineID();
                        freeThis();
                        //await Navigation.PushAsync(new Page3(yourData));
                        Application.Current.MainPage = new Page3();
                    }
                    else
                    {
                        //_KaisouNo = 1;
                        ////string[] yourData = { _UserID.ToString(), _SasizuNo, _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString() };
                        //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0" };
                        //await Navigation.PushAsync(new Page1(yourData));
                    }
                }
                doingNow = false;
            }
        }
        wBtn.IsEnabled = true;
    }
    private int GetSelectedLineID()
    {
        int iRet = -1;
        if ((lstKaisou._Header._GamenKind == 2 || lstKaisou._Header._GamenKind == 17) && (dropdown1 != null))
        {
            if (lstKaisou._Header._done == 0)
            {
                iRet = dropdown1.SelectedIndex;
            }
            else
            {
                iRet = lstKaisou._Header._SelSelected;
            }
        }
        return iRet;
    }
    async void AddButtonClicked(object sender, EventArgs s)
    {
        int i = 0;
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            if ((_StartStop == 1 && lstKaisou._Header._done == 0) || lstKaisou._Header._done == 1)
            {
                doingNow = true;
                //実行可能な物
                clsGlobalVar.g_KaisouNo = 2;
                //_KouteiID = lstKaisou._Datas[i]._KouteiID;
                //_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                //clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                Application.Current.MainPage = new SashizuAdd();
                doingNow = false;
            }
        }
        wBtn.IsEnabled = true;
    }
    async void UpdButtonClicked5(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;

        if (doingNow == false)
        {
            if (lstKaisou._Header._GamenKind == 5)
            {
                clsGlobalVar.g_KaisouNo = 3;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Datas[0]._KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[0]._KouteiShousaiID;
                clsGlobalVar.g_KensaBashoID = 0;
                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                Application.Current.MainPage = new Page3();
            }
            else if (lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18)
            {
                clsGlobalVar.g_KaisouNo = 3;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Header._BackID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_KouteiShousaiID = lstKaisou._Header._BackID;
                clsGlobalVar.g_KensaBashoID = 0;
                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                Application.Current.MainPage = new Page3();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void UpdButtonClicked3(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別11専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            //if (_StartStop == 1 && lstKaisou._Header._done == 0)
            if ((_StartStop == 1 && lstKaisou._Header._done == 0) || (clsGlobalVar.g_KouteiKekkaID > 0 && lstKaisou._Header._done == 0))
            {
                //                if ((_StartStop == 1 && lstKaisou._Header._done == 0) || lstKaisou._Header._done == 1 || clsGlobalVar.g_KouteiKekkaID > 0)
                //                {
                int iPass = -1;
                decimal dPara = -999999;
                string strPara = string.Empty;
                string strCombo = string.Empty;
                int iSelectedID = 0;
                doingNow = true;
                if (lstKaisou._Header._done == 1)
                {
                    if ((lstKaisou._Header._GamenKind == 5 || lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18) && lstKaisou._Header._iPass == 0)
                    {
                        clsGlobalVar.g_KaisouNo = 3;
                        //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Header._BackID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                        clsGlobalVar.g_KouteiShousaiID = lstKaisou._Header._BackID;
                        clsGlobalVar.g_KensaBashoID = 0;
                        clsGlobalVar.g_LineIndex = GetSelectedLineID();
                        freeThis();
                        //await Navigation.PushAsync(new Page3(yourData));
                        Application.Current.MainPage = new Page3();
                    }
                }
                else
                {
                    if (lstKaisou._Header._GamenKind == 1)
                    {

                    }
                    else if (lstKaisou._Header._GamenKind == 2 || lstKaisou._Header._GamenKind == 17)
                    {
                        if (dropdown1.SelectedIndex == -1)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                            await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                            txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                            doingNow = false;
                            return;
                        }
                        else
                        {
                            int iWkNo = 0;
                            foreach (clsLine wLine in lstKaisou._Header._LineLists)
                            {
                                if (iWkNo == dropdown1.SelectedIndex)
                                {
                                    iSelectedID = wLine._index;
                                    break;
                                }
                                iWkNo++;
                            }
                        }
                    }
                    else if (lstKaisou._Header._GamenKind == 3)
                    {

                    }
                    else if (lstKaisou._Header._GamenKind == 4)
                    {
                        iPass = lstKaisou._Header._iPass;
                    }
                    else if (lstKaisou._Header._GamenKind == 5)
                    {
                        iPass = lstKaisou._Header._iPass;
                    }
                    else if (lstKaisou._Header._GamenKind == 6)
                    {
                        if (string.IsNullOrEmpty(txtVal1.Text) == true)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                            await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                            txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else if (CheckNumberChar3(txtVal1.Text) == false)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                            await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                            txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else
                        {
                            try
                            {
                                dPara = decimal.Parse(txtVal1.Text);
                            }
                            catch (Exception)
                            {
                                //throw;
                                //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                                await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                                txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                                doingNow = false;
                                wBtn.IsEnabled = true;
                                return;
                            }
                        }
                        if (dropdown1 != null)
                        {
                            if (dropdown1.SelectedIndex == -1)
                            {
                                //await Navigation.PopAsync();
                                //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                                await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                                txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                                doingNow = false;
                                wBtn.IsEnabled = true;
                                return;
                            }
                            else
                            {
                                int iWkNo = 0;
                                foreach (clsLine wLine in lstKaisou._Header._LineLists)
                                {
                                    if (iWkNo == dropdown1.SelectedIndex)
                                    {
                                        iSelectedID = wLine._index;
                                        break;
                                    }
                                    iWkNo++;
                                }
                            }
                        }
                    }
                    else if (lstKaisou._Header._GamenKind == 7)
                    {
                        if (string.IsNullOrEmpty(txtVal1.Text) == true)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                            await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else if (CheckHankakuChar(txtVal1.Text) == false)
                        {
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                            await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else
                        {
                            strPara = ConvStr2Webserver(txtVal1.Text);
                        }
                    }
                    else if (lstKaisou._Header._GamenKind == 8)
                    {
                        int iIndex = dropdown1.SelectedIndex;
                        if (iIndex == -1)
                        {
                            //await Navigation.PopAsync();
                            //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                            await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                            doingNow = false;
                            wBtn.IsEnabled = true;
                            return;
                        }
                        else
                        {
                            strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + clsGlobalVar.g_KensaBashoID;
                        }
                    }
                    else if (lstKaisou._Header._GamenKind == 9)
                    {

                    }
                    else if (lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18)
                    {
                        iPass = lstKaisou._Header._iPass;
                    }
                    string strErrMsg = "";
                    bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, clsGlobalVar.g_KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
                    if (bRet == false)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                        await DisplayAlert("更新エラー", strErrMsg, "OK");
                        doingNow = false;
                    }
                    else
                    {
                        if (lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18)
                        {
                            if (_StartStop == 0 && clsGlobalVar.g_KouteiKekkaID == 0)
                            {
                                strErrMsg = clsErrorMessage.GetErrMsg("NG03");
                                //await DisplayAlert(AppResources.IDM051, strErrMsg, "OK");
                                await DisplayAlert("工程終了エラー", strErrMsg, "OK");
                                doingNow = false;
                                wBtn.IsEnabled = true;
                                return;
                            }
                        }
                        if ((lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18) && lstKaisou._Header._iPass == 1)
                        {
                            if (_StartStop == 1)
                            {
                                //計測中の場合は一気にStopさせる
                                //buttonSS.Text = "START";
                                buttonSS.ImageSource = "START.png";
                                //buttonSS.BackgroundColor = Colors.Blue;
                                _StartStop = 0;
                                clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
                            }
                            int wSelLineID = 0;
                            int iRet = clsWebUpdate.SendResultForFinish(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, wSelLineID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
                            if (iRet == 0)
                            {
                                //未入力
                                //await Navigation.PopAsync();
                                //await DisplayAlert(AppResources.IDM051, strErrMsg, "OK");
                                await DisplayAlert("工程終了エラー", strErrMsg, "OK");
                            }
                            else
                            {
                                clsGlobalVar.g_KaisouNo = 1;
                                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                                //                    clsGlobalVar.g_KouteiID = 0; //Partsページジャンプの為に不要になった

                                clsGlobalVar.g_KouteiShousaiID = 0;
                                clsGlobalVar.g_KensaBashoID = 0;
                                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                                freeThis();
                                //await Navigation.PushAsync(new Page1(yourData));
                                Application.Current.MainPage = new Page1();
                            }

                        }
                        else
                        {
                            if ((lstKaisou._Header._GamenKind == 5 || lstKaisou._Header._GamenKind == 10 || lstKaisou._Header._GamenKind == 18) && lstKaisou._Header._iPass == 0)
                            {
                                clsGlobalVar.g_KaisouNo = 3;
                                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Header._BackID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                                clsGlobalVar.g_KouteiShousaiID = lstKaisou._Header._BackID;
                                clsGlobalVar.g_KensaBashoID = 0;
                                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                                freeThis();
                                //await Navigation.PushAsync(new Page3(yourData));
                                Application.Current.MainPage = new Page3();
                            }
                            else
                            {
                                //_KaisouNo = 1;
                                ////string[] yourData = { _UserID.ToString(), _SasizuNo, _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString() };
                                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0" };
                                //await Navigation.PushAsync(new Page1(yourData));
                            }
                        }
                    }
                }
                doingNow = false;
            }
        }
        wBtn.IsEnabled = true;
    }

    async void ProStartButtonClicked(object sender, EventArgs s)
    {
        //buttonProStart用工程開始 コマンド未定の為着手済み
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            string strErrMsg = "";
            int iRet = clsWebUpdate.SendProStart(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (iRet == 0)
            {
                //工程開始完了エラー
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM145, strErrMsg, "OK");
                await DisplayAlert("工程開始エラー", strErrMsg, "OK");
            }
            else
            {
                //正常　工程開始完了
                //await DisplayAlert(AppResources.IDM146, strErrMsg, "OK");
                await DisplayAlert("工程開始完了", strErrMsg, "OK");
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    private async void DispPartsPage(string wpage)
    {
        //            if (clsGlobalVar.g_PartDisp == 1)
        //            {
        //                clsGlobalVar.g_JumpPage = wpage;
        //                //freeThis();
        //                //Application.Current.MainPage = new PageParts();
        //                //freeThis();
        //
        //                //Application.Current.MainPage = new PageParts2();
        //                //return ;
        //           }
        //            else
        //          {
        freeThis();
        if (wpage == "Page1")
        {
            Application.Current.MainPage = new Page1();
        }
        else if (wpage == "Page2")
        {
            Application.Current.MainPage = new Page2();

        }
        else if (wpage == "Page3")
        {
            Application.Current.MainPage = new Page3();

        }
        else if (wpage == "Page4")
        {
            Application.Current.MainPage = new Page4();

        }
        else if (wpage == "Page1_5")
        {
            Application.Current.MainPage = new Page1_5();

        }
        else if (wpage == "PageNo")
        {
            Application.Current.MainPage = new PageNo();

        }
        else if (wpage == "SashizuAdd")
        {
            Application.Current.MainPage = new SashizuAdd();

        }
        else if (wpage == "SashizuPage")
        {
            Application.Current.MainPage = new SashizuPage();

        }
        //            }

    }
    async void FinButtonClicked(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._Agree == 1)
            {
                //bool resultA = await DisplayAlert(AppResources.IDM053, AppResources.IDM054, "OK", AppResources.IDM009);
                bool resultA = await DisplayAlert("注意", "工程終了時に未入力の項目を全て合格に致します。", "OK", "キャンセル");
                //戻り値をボタンテキストにセットする
                if (resultA == false)
                {
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
            }
            int wSelLineID = 0;
            if (lstKaisou._Header._GamenKind == 2 || lstKaisou._Header._GamenKind == 17)
            {
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex > -1)
                    {
                        wSelLineID = lstKaisou._Header._LineLists[dropdown1.SelectedIndex]._index;
                    }
                    else
                    {
                        //await DisplayAlert(AppResources.IDM051, AppResources.IDM043, "OK");
                        await DisplayAlert("工程終了エラー", "ラインが選択されていません。", "OK");
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                else
                {
                    //await DisplayAlert(AppResources.IDM051, AppResources.IDM055, "OK");
                    await DisplayAlert("工程終了エラー", "ラインのリストが登録されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
            }
            if (_StartStop == 1)
            {
                //計測中の場合は一気にStopさせる
                //buttonSS.Text = "START";
                buttonSS.ImageSource = "START.png";
                //buttonSS.BackgroundColor = Colors.Blue;
                _StartStop = 0;
                clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
            }
            string strErrMsg = "";
            int iRet = clsWebUpdate.SendResultForFinish(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, wSelLineID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (iRet == 0)
            {
                //未入力
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM051, strErrMsg, "OK");
                await DisplayAlert("工程終了エラー", strErrMsg, "OK");
            }
            else
            {
                clsGlobalVar.g_KaisouNo = 1;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                //                    clsGlobalVar.g_KouteiID = 0; //Partsページジャンプの為に不要になった
                clsGlobalVar.g_KouteiShousaiID = 0;
                clsGlobalVar.g_KensaBashoID = 0;
                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                freeThis();
                //await Navigation.PushAsync(new Page1(yourData));
                //                    if (clsGlobalVar.g_lastSashizuKind == 2)
                //                    {
                //                        Application.Current.MainPage = new PageNo();
                //                    }
                //                    else
                //                    {
                //Application.Current.MainPage = new Page1();
                DispPartsPage("Page1");

                //                    }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    async void ReadButtonClicked(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            //clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);

            string strErrMsg = "";

            clsGlobalVar.g_LineIndex = GetSelectedLineID();

            bool bRet = clsWebUpdate.SendReadCommand(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, clsGlobalVar.g_LineIndex, ref strErrMsg);
            //bool bRet = clsWebUpdate.SendReadCommand(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //未入力
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM115, strErrMsg, "OK");
                await DisplayAlert("読み込みエラー", strErrMsg, "OK");
            }
            else
            {
                freeThis();
                Application.Current.MainPage = new Page2();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void ReadButtonClicked2(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            //clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);

            string strErrMsg = "";
            //                clsGlobalVar.g_LineIndex = GetSelectedLineID2();

            //bool bRet = clsWebUpdate.SendReadCommand2(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, ref strErrMsg);

            clsGlobalVar.g_LineIndex = GetSelectedLineID();
            //bool bRet = clsWebUpdate.SendReadCommand(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, clsGlobalVar.g_LineIndex, ref strErrMsg);
            bool bRet = clsWebUpdate.SendReadCommand(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer, clsGlobalVar.g_LineIndex, ref strErrMsg);
            if (bRet == false)
            {
                //未入力
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM115, strErrMsg, "OK");
                await DisplayAlert("読み込みエラー", strErrMsg, "OK");
            }
            else
            {
                freeThis();
                Application.Current.MainPage = new Page2();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    async void FinButtonClicked2(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int wSelLineID = 0;
            int i = 0;
            foreach (Entry entW in LstVal)
            {
                //entW.BackgroundColor = Colors.Default;
                entW.BackgroundColor = Colors.White;
            }
            string wSendPara = "";
            foreach (Entry entW in LstVal)
            {
                int iwNo = 0;
                //該当発見
                if (entW.Text.Length == 0)
                {

                }
                else
                {
                    if (CheckNumberChar3(entW.Text) == false)
                    {
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM057, "OK");
                        await DisplayAlert("更新エラー", "ロット数が不正です。", "OK");
                        entW.BackgroundColor = Colors.LightPink;
                        wBtn.IsEnabled = true;
                        doingNow = false;
                        return;
                    }
                    try
                    {
                        iwNo = int.Parse(entW.Text);
                    }
                    catch (Exception)
                    {
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM057, "OK");
                        await DisplayAlert("更新エラー", "ロット数が不正です。", "OK");
                        entW.BackgroundColor = Colors.LightPink;
                        wBtn.IsEnabled = true;
                        doingNow = false;
                        return;
                    }
                    if (iwNo < 0 || iwNo > lstKaisou._Datas[i]._LotLeft)
                    {
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM057, "OK");
                        await DisplayAlert("更新エラー", "ロット数が不正です。", "OK");
                        entW.BackgroundColor = Colors.LightPink;
                        wBtn.IsEnabled = true;
                        doingNow = false;
                        return;
                    }
                    if (iwNo > 0)
                    {
                        string wStr = lstKaisou._Datas[i]._kaisouName + @"＃" + iwNo.ToString();
                        if (wSendPara.Length > 0)
                        {
                            wSendPara += @"@";
                        }
                        wSendPara += wStr;
                    }
                }
                i++;
            }
            if (wSendPara.Length > 0)
            {
                if (_StartStop == 1)
                {
                    //計測中の場合は一気にStopさせる
                    //buttonSS.Text = "START";
                    buttonSS.ImageSource = "START.png";
                    //buttonSS.BackgroundColor = Colors.Blue;
                    _StartStop = 0;
                    clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
                }
                //await DisplayAlert("確認", wSendPara, "OK");
                string strErrMsg = "";
                bool bRet = clsWebUpdate.SendLotData(clsGlobalVar.g_UserID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KouteiKekkaID, wSendPara, ref strErrMsg);
                if (bRet == false)
                {
                    //未入力
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                    await DisplayAlert("更新エラー", strErrMsg, "OK");
                }
                else
                {
                    clsGlobalVar.g_KaisouNo = 1;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                    //                    clsGlobalVar.g_KouteiID = 0; //Partsページジャンプの為に不要になった
                    clsGlobalVar.g_KouteiShousaiID = 0;
                    clsGlobalVar.g_KensaBashoID = 0;
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();
                    freeThis();
                    //await Navigation.PushAsync(new Page1(yourData));
                    //                        if (clsGlobalVar.g_lastSashizuKind == 2)
                    //                        {
                    //                            Application.Current.MainPage = new PageNo();
                    //                        }
                    //                        else
                    //                        {
                    //Application.Current.MainPage = new Page1();
                    DispPartsPage("Page1");

                    //                        }
                }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void FinButtonClicked3(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._Agree == 1)
            {
                //bool resultA = await DisplayAlert(AppResources.IDM053, AppResources.IDM054, "OK", AppResources.IDM009);
                bool resultA = await DisplayAlert("注意", "工程終了時に未入力の項目を全て合格に致します。", "OK", "キャンセル");
                //戻り値をボタンテキストにセットする
                if (resultA == false)
                {
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
            }
            int wSelLineID = 0;
            if (lstKaisou._Header._GamenKind == 16)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        wSelLineID = int.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        //txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
            }
            if (_StartStop == 1)
            {
                //計測中の場合は一気にStopさせる
                //buttonSS.Text = "START";
                buttonSS.ImageSource = "START.png";
                //buttonSS.BackgroundColor = Colors.Blue;
                _StartStop = 0;
                clsGlobalVar.g_KouteiKekkaID = clsWebUpdate.SendStartStop(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiVer);
            }
            string strErrMsg = "";
            int iRet = clsWebUpdate.SendResultForFinish(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, wSelLineID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (iRet == 0)
            {
                //未入力
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM051, strErrMsg, "OK");
                await DisplayAlert("工程終了エラー", strErrMsg, "OK");
            }
            else
            {
                clsGlobalVar.g_KaisouNo = 1;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                //                    clsGlobalVar.g_KouteiID = 0; //Partsページジャンプの為に不要になった
                clsGlobalVar.g_KouteiShousaiID = 0;
                clsGlobalVar.g_KensaBashoID = 0;
                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                freeThis();
                //await Navigation.PushAsync(new Page1(yourData));
                //                    if (clsGlobalVar.g_lastSashizuKind == 2)
                //                    {
                //                        Application.Current.MainPage = new PageNo();
                //                    }
                //                    else
                //                    {
                //Application.Current.MainPage = new Page1();
                DispPartsPage("Page1");

                //                    }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    async void PartsButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            //clsGlobalVar.g_KaisouNo = 1;
            clsGlobalVar.g_KouteiShousaiID = 0;
            clsGlobalVar.g_KensaBashoID = 0;
            clsGlobalVar.g_LineIndex = GetSelectedLineID();
            freeThis();
            Application.Current.MainPage = new PageParts(); //テスト用
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            clsGlobalVar.g_KaisouNo = 1;
            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), "0", "0", "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
            //                    clsGlobalVar.g_KouteiID = 0; //Partsページジャンプの為に不要になった
            clsGlobalVar.g_KouteiShousaiID = 0;
            clsGlobalVar.g_KensaBashoID = 0;
            clsGlobalVar.g_LineIndex = GetSelectedLineID();
            freeThis();
            //await Navigation.PushAsync(new Page1(yourData));
            Application.Current.MainPage = new Page1(); //テスト用
                                                        //Application.Current.MainPage = new PageParts(); //テスト用
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void PassButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._iPass == 1)
            {
                lstKaisou._Header._iPass = 0;
            }
            else if (lstKaisou._Header._iPass == 0)
            {
                lstKaisou._Header._iPass = -1;
            }
            else if (lstKaisou._Header._iPass == -1)
            {
                lstKaisou._Header._iPass = 1;
            }
            buttonPass.Text = GetPassButtonStr(lstKaisou._Header._iPass);
            buttonPass.BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass);
            buttonPass.TextColor = GetPassButtonTColor(lstKaisou._Header._iPass);

            doingNow = false;
        }
    }
    async void PassButtonClicked3(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._iPass == 1)
            {
                lstKaisou._Header._iPass = 0;
            }
            else if (lstKaisou._Header._iPass == 0)
            {
                lstKaisou._Header._iPass = 2;
            }
            else if (lstKaisou._Header._iPass == 2)
            {
                lstKaisou._Header._iPass = -1;
            }
            else if (lstKaisou._Header._iPass == -1)
            {
                lstKaisou._Header._iPass = 1;
            }
            buttonPass3.Text = GetPassButtonStr(lstKaisou._Header._iPass);
            buttonPass3.BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass);
            buttonPass3.TextColor = GetPassButtonTColor(lstKaisou._Header._iPass);
            if (buttonUpd3 != null) buttonUpd3.IsEnabled = (lstKaisou._Header._iPass == -1) ? false : true;
            buttonUpd3.Text = GetUpdButtonStr3(lstKaisou._Header._iPass);
            doingNow = false;
        }
    }
    private string GetPassButtonStr(int iPass)
    {
        //string strRet = "　" + AppResources.IDM059 + "　";
        string strRet = "　" + "未入力" + "　";
        if (iPass == 1)
        {
            //strRet = "　" + AppResources.IDM060 + "　";
            strRet = "　" + "合格" + "　";
        }
        else if (iPass == 0)
        {
            //strRet = "　" + AppResources.IDM061 + "　";
            strRet = "　" + "不合格" + "　";
        }
        else if (iPass == 2)
        {
            //strRet = "　" + AppResources.IDM062 + "　";
            strRet = "　" + "不要" + "　";
        }
        return strRet;
    }
    private Color GetPassButtonBColor(int iPass)
    {
        Color retCol = Colors.LightGray;
        if (iPass == 1)
        {
            retCol = Colors.Blue;
        }
        else if (iPass == 0)
        {
            retCol = Colors.Red;
        }
        else if (iPass == 2)
        {
            retCol = Colors.LightGray;
        }
        return retCol;
    }
    private Color GetPassButtonTColor(int iPass)
    {
        Color retCol = Colors.Black;
        if (iPass == 1)
        {
            retCol = Colors.White;
        }
        else if (iPass == 0)
        {
            retCol = Colors.White;
        }
        else if (iPass == 2)
        {
            retCol = Colors.Black;
        }
        return retCol;
    }
    private string GetUpdButtonStr(int iPass)
    {
        //string strRet = AppResources.IDM038;
        string strRet = "更新";
        if (iPass == 1)
        {
            //strRet = AppResources.IDM038;
            strRet = "更新";
        }
        else if (iPass == 0)
        {
            //strRet = AppResources.IDM065;
            strRet = "詳細入力";
        }
        return strRet;
    }

    private string GetUpdButtonStr3(int iPass)
    {
        //string strRet = AppResources.IDM038;
        string strRet = "更新";
        if (iPass == 1)
        {
            //strRet = AppResources.IDM031;
            strRet = "工程終了";
        }
        else if (iPass == 0)
        {
            //strRet = AppResources.IDM069;
            strRet = "戻り工程指定";
        }
        return strRet;
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
            wCol = GetBackColorParts();//Colors.Blue;
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
            wCol = GetBackColorParts();//Colors.Blue;
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
    private string GetKetaStr(int iSei, int iShou)
    {
        string strKetaW = string.Empty;
        for (int i = 0; i < iSei; i++)
        {
            strKetaW += "x";
        }
        if (iSei == 0)
        {
            strKetaW += "x";
        }
        if (iShou > 0)
        {
            strKetaW += ".";
            for (int i = 0; i < iShou; i++)
            {
                strKetaW += "x";
            }
        }
        return strKetaW;
    }
    private string GetDispTime(int iTotalSec)
    {
        string strRet = string.Empty;
        int hh = iTotalSec / 3600;
        int mm = (iTotalSec - (hh * 3600)) / 60;
        int ss = (iTotalSec - (hh * 3600) - (mm * 60));
        strRet = string.Format("{0:D2}:{1:D2}:{2:D2}", hh, mm, ss);
        return strRet;
    }
    private bool CheckNumberChar3(string strNo)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strNo, "^[-]?[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*.[0-9]*$") == true || Regex.IsMatch(strNo, "^[-]?[0-9]*.[0-9]*$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }

    private string GetFormatedStrByKeta(int iSei, int iShou, decimal dVal)
    {
        string strRet = string.Empty;
        if (dVal == -999999)
        {

        }
        else
        {
            string strF = "F" + iShou.ToString();
            strRet = dVal.ToString(strF);
        }
        return strRet;
    }
    private int GetCurSelectedDropDown(string strVal)
    {
        int iRet = -1;
        if (string.IsNullOrEmpty(strVal) == false)
        {
            string wStr = strVal;
            int iIndex = 0;
            int wNo = -1;
            int wKouteiID = -1;
            int wKouteiShousaiID = -1;
            int wKensaBashoID = -1;

            while (wStr.Length > 0)
            {
                int iNo1 = wStr.IndexOf("-");
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
                        wKouteiID = int.Parse(strW2);
                    }
                    else if (iIndex == 1)
                    {
                        wKouteiShousaiID = int.Parse(strW2);
                    }
                    else if (iIndex == 2)
                    {
                        wKensaBashoID = int.Parse(strW2);
                    }
                    else
                    {
                        break;
                    }
                }
                iIndex += 1;
            }
            if (wKouteiID > -1)
            {
                iIndex = 0;
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    if (wKaisou._KouteiID == wKouteiID && wKaisou._KouteiShousaiID == wKouteiShousaiID && wKaisou._KensaBashoID == wKensaBashoID)
                    {
                        iRet = iIndex;
                        break;
                    }
                    iIndex++;
                }
            }
        }
        return iRet;
    }

    private bool CheckHankakuChar(string strW)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strW, "^[a-zA-Z0-9!-/:-@[-`{-~]+$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }
    private string ConvStr2Disp(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListFrom = { "＃", "＆", "？", "％", "￥", "／" };
        string[] strsListTo = { "#", "&", "?", "%", "\\", "/" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }
    private string ConvStr2Webserver(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListTo = { "＃", "＆", "？", "％", "￥", "／" };
        string[] strsListFrom = { "#", "&", "?", "%", "\\", "/" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }





}