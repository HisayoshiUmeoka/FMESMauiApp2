using System.Globalization;

namespace FMES;

public partial class Pageweboption : ContentPage
{
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private WebView webB;
    private Button buttonEnd;
    private bool doingNow = false;

    public Pageweboption()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());


        Application.Current.MainPage = new webPage2();
        return;




        clsGlobalVar.g_NowForm = 11;
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


        //string wURL = clsGlobalVar.GetCurURL() + "users/dailyreports/" + clsGlobalVar.GetLanguageStr() + "/" + wKouteiID + "/" + wUserID + "/" + wSasizuID + "/" + wKouteiVer;
        //string wURL = clsGlobalVar.GetCurURL() + "users/dailyreports/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID + "/" + DateTime.Now.ToString("yyyy-MM-dd");
        //            label1 = new Label
        //            {
        //                Text = "Loading...",
        //                FontSize = 22,
        //                VerticalOptions = LayoutOptions.Center,
        //                            HorizontalOptions = LayoutOptions.Fill,
        //            };
        webB = new WebView
        {
            HeightRequest = 1500,
            WidthRequest = 1000,
            Source = clsGlobalVar.g_optionurl,

            //VerticalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Fill,
        };
        buttonEnd = new Button
        {
            //Text = AppResources.IDM032,
            Text = "戻る",
            FontSize = 14,
            BorderColor = Colors.LightGray,
            BorderWidth = 1.5,
            HeightRequest = 48,
            CornerRadius = 12,
            Margin = new Thickness(20, 0, 20, 12),
            VerticalOptions = LayoutOptions.StartAndExpand,
            //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
            TextColor = Colors.Black,
            BackgroundColor = Colors.LightGreen,
        };
        Content = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 0, 0, 0),
            Children = {
                    ContentMenu,
//                                label1,
                                //buttonEnd,
                                webB,
                                buttonEnd,
                            }
        };
        webB.Navigated += webviewNavigated;
        webB.Navigating += webviewNavigating;
        buttonEnd.Clicked += EndButtonClicked;



    }

    void webviewNavigating(object sender, WebNavigatingEventArgs e)
    {
        //            label1.IsVisible = true;
    }

    void webviewNavigated(object sender, WebNavigatedEventArgs e)
    {
        //            label1.IsVisible = false;
    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Pageweboption";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu

    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {



            //ページをジャンプ
            //clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            string strJump = clsGlobalVar.g_JumpPage;
            clsGlobalVar.g_JumpPage = "SashizuPage";   //仕様変更に伴い
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
        freeThis();
        doingNow = false;

    }
    private void freeThis()
    {
        Console.WriteLine("PageWeb free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        if (label1 != null)
        {
            label1 = null;
        }

        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        if (webB != null)
        {
            webB.Navigated -= webviewNavigated;
            webB.Navigating -= webviewNavigating;
            webB = null;
        }
        GC.Collect();
        Console.WriteLine("PageWeb free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }


}