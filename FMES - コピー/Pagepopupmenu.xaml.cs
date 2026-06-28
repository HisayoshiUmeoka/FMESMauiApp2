using System.Globalization;

namespace FMES;

public partial class Pagepopupmenu : ContentPage
{
    private Label labelVer;
    private Button buttonConfig;
    private Button buttonUserchange;
    private Button buttonLogout;
    private Button buttonback;

    public Pagepopupmenu()
    {
        InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());

        labelVer = new Label
        {
            //Text = "Version :" + DependencyService.Get<IAssemblyService>().GetVersionName() + "  Version Code:" + DependencyService.Get<IAssemblyService>().GetVersionCode().ToString(),
            //Text = "Version :" + DependencyService.Get<IAssemblyService>().GetVersionCode().ToString(),
            Text = "Version Name:" + AppInfo.VersionString,

            FontSize = 15,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.End // 中央に配置する（縦方向
        };
        buttonConfig = new Button
        {
            Margin = new Thickness(0, 5, 0, 5),
            //Text = AppResources.IDM141,//"　環境設定　",
            Text = "　環境設定　",
            FontSize = 20,
            //TextColor = Colors.Black,
            //BackgroundColor = Colors.DodgerBlue,
            //BackgroundColor = Colors.Blue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),

            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        buttonUserchange = new Button
        {
            Margin = new Thickness(0, 5, 0, 5),            //Text = AppResources.IDM142,//"　ユーザー切替　",
            Text = "　ユーザー切替　",
            FontSize = 20,
            //TextColor = Colors.Black,
            //BackgroundColor = Colors.DodgerBlue,
            //BackgroundColor = Colors.Blue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),

            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        buttonLogout = new Button
        {
            Margin = new Thickness(0, 5, 0, 5),
            //Text = AppResources.IDM143,//"　ログアウト　",
            Text = "　ログアウト　",
            FontSize = 20,
            //TextColor = Colors.Black,
            //BackgroundColor = Colors.DodgerBlue,
            //BackgroundColor = Colors.Blue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),

            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        buttonback = new Button
        {
            Margin = new Thickness(0, 5, 0, 5),            //Text = AppResources.IDM032,//"　戻る　",
            Text = "　戻る　",
            FontSize = 20,
            //TextColor = Colors.Black,
            //BackgroundColor = Colors.DodgerBlue,
            //BackgroundColor = Colors.Blue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),

            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };

        buttonConfig.Clicked += ConfigButtonClicked;
        buttonUserchange.Clicked += UserButtonClicked;
        buttonLogout.Clicked += LogoutButtonClicked;
        buttonback.Clicked += backButtonClicked;




        this.Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),

                BackgroundColor = Colors.White,
                Children = {
                    labelVer,
                    //user1,
                    buttonConfig,
                    buttonUserchange,
                    buttonLogout,
                    buttonback,
                }
            }
        };

    }




    async void ConfigButtonClicked(object sender, EventArgs s)
    {
        freeThis();
        Application.Current.MainPage = new configPage();
    }
    async void UserButtonClicked(object sender, EventArgs s)
    {
        freeThis();
        Application.Current.MainPage = new MainPage();
    }
    async void LogoutButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_Operator=string.Empty;
        clsGlobalVar.g_UserID = 0;
        clsGlobalVar.g_SasizuNo = string.Empty;
        clsGlobalVar.g_KouteiID = 0;
        clsGlobalVar.g_lastSashizuKind = 0;
        clsGlobalVar.g_SasizuID = 0;
        clsGlobalVar.g_SasizuID = 0;
        clsGlobalVar.g_KouteiVer = 0;
        clsGlobalVar.g_lastSashizuKind = 0;

        freeThis();
        Application.Current.MainPage = new MainPage();
    }
    async void backButtonClicked(object sender, EventArgs s)
    {

        if (clsGlobalVar.g_BackPage == "MainPage")
        {

            freeThis();
            Application.Current.MainPage = new MainPage();
        }
        else if (clsGlobalVar.g_BackPage == "SashizuPage")
        {
            freeThis();
            Application.Current.MainPage = new SashizuPage();
        }
        else if (clsGlobalVar.g_BackPage == "ComLogin")
        {
            freeThis();
            Application.Current.MainPage = new ComLogin();
        }
        else if (clsGlobalVar.g_BackPage == "ConfigPage")
        {
            freeThis();
            Application.Current.MainPage = new configPage();
        }
        else if (clsGlobalVar.g_BackPage == "Page1")
        {
            freeThis();
            Application.Current.MainPage = new Page1();
        }
        else if (clsGlobalVar.g_BackPage == "Page1_5")
        {
            freeThis();
            Application.Current.MainPage = new Page1_5();
        }
        else if (clsGlobalVar.g_BackPage == "Page2")
        {
            freeThis();
            Application.Current.MainPage = new Page2();
        }
        else if (clsGlobalVar.g_BackPage == "Page3")
        {
            freeThis();
            Application.Current.MainPage = new Page3();
        }
        else if (clsGlobalVar.g_BackPage == "Page4")
        {
            freeThis();
            Application.Current.MainPage = new Page4();
        }
        else if (clsGlobalVar.g_BackPage == "Page5")
        {
            freeThis();
            Application.Current.MainPage = new Page5();
        }
        else if (clsGlobalVar.g_BackPage == "PageMaching")
        {
            freeThis();
            Application.Current.MainPage = new PageMaching();
        }
        else if (clsGlobalVar.g_BackPage == "PageNo")
        {
            freeThis();
            Application.Current.MainPage = new PageNo();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            freeThis();
            Application.Current.MainPage = new PageWeb();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            freeThis();
            Application.Current.MainPage = new PageWeb();
        }
        else if (clsGlobalVar.g_BackPage == "webPage2")
        {
            freeThis();
            Application.Current.MainPage = new webPage2();
        }
        else if (clsGlobalVar.g_BackPage == "SashizuAdd")
        {
            freeThis();
            Application.Current.MainPage = new SashizuAdd();
        }
        else
        {
            //バグの場合ここに来る
            freeThis();
            Application.Current.MainPage = new MainPage();
        }

    }
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
}