using System.Globalization;
using ZXing.Net.Maui;

namespace FMES;

public partial class PageMaching : ContentPage
{
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Entry user1;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private Button button5;
    //private Button button5;

    private Button buttonEnd;

    private StackLayout Content2;

    private bool doingNow = false;

    public PageMaching()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 2;

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

        // StackLayoutで2つの Entryコントロールを並べる
        //dropdown1 = new Picker
        //{
        //    FontSize = 16,
        //    Title = AppResources.IDM103,
        //    VerticalOptions = LayoutOptions.Start
        //};
        //dropdown1.Items.Add(AppResources.IDM104);
        //dropdown1.Items.Add(AppResources.IDM105);
        //dropdown1.SelectedIndex = clsGlobalVar.g_lastSashizuKind - 1;
        user1 = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontSize = 20,
            //Placeholder = AppResources.IDM126,
            Placeholder = "ビーコン名称",
                        HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
        };
        // ↓added for QRScan
        if (clsGlobalVar.g_BackPage == "PageMaching" && clsGlobalVar.g_QRRET != null)
        {
            user1.Text = clsGlobalVar.g_QRRET;
            clsGlobalVar.g_BackPage = string.Empty;
            clsGlobalVar.g_QRRET = string.Empty;

        }
        // ↑added for QRScan

        button1 = new Button
        {
            //Text = "ＱＲスキャン",
            ImageSource = "qr100x100.png",
            FontSize = 20,
            BackgroundColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center//,//中央に配置する（横方向）
                                                    //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        Content2 = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            BackgroundColor = Colors.White,
            Children = {
                        user1,
                        button1,
                    }
        };
        button2 = new Button
        {
            //Text = AppResources.IDM119,//"　決定　",
            Text = "ビーコンマッチ登録",//"ビーコンマッチ登録",
            FontSize = 20,
            Margin = new Thickness(0, 5, 0, 5),
            Padding = new Thickness(10, 10, 10, 10),
            //BackgroundColor = Colors.DodgerBlue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
                        HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };

        //string wEnd = AppResources.IDM032;
        string wEnd = "戻る";

        buttonEnd = new Button
        {
            Text = wEnd,
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
                    //dropdown1,
                    Content2,
                    //user1,
                    //button1,
                    button2,
                    //button5,
                    //button3,
                    buttonEnd,
                    //button4,
                }
        };
        button1.Clicked += ScanButtonClicked;
        button2.Clicked += GoButtonClicked;
        //button3.Clicked += GoButtonClicked2;
        //button4.Clicked += GoWebClicked;
        //button5.Clicked += GoButtonClicked3;



    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "PageMaching";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu

    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            Application.Current.MainPage = new Page1();
            doingNow = false;
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
    async void ScanButtonClicked(object sender, EventArgs s)
    {
        //tako ここでQRスキャン実処理を入れる
        //ここにQRSCAN実処理を入れる。
        // ↓added for QRScan
        clsGlobalVar.g_BackPage = "PageMaching";
        clsGlobalVar.g_QRRET = string.Empty;
        Application.Current.MainPage = new QRPage();
        // ↑added for QRScan

        //var scanPage = new ZXing.Net.Maui.Views.CameraBarcodeReaderView();
        //var result = await scanPage.ScanAsync();

        //if (result != null)
        //{
        //    await DisplayAlert("スキャン完了", result.Text, "OK");
        //    user1.Text = result.Text;
        //}            //user1.Text = result.Text;
        //});

        //scanedData.Add(result.Text);
        //};
    }

    async void GoButtonClicked(object sender, EventArgs s)
    {
        //bool bChange = (clsGlobalVar.g_lastSashizuKind == dropdown1.SelectedIndex + 1)? false : true;
        //clsGlobalVar.g_SasizuNo = user1.Text;

        //clsGlobalVar.g_lastSashizuKind = dropdown1.SelectedIndex + 1;
        //            clsGlobalVar.g_lastSashizuKind = 0 + 1;


        //await clsGlobalVar.SaveTextAsync();

        if (string.IsNullOrEmpty(user1.Text) == false)
        {
            //await DisplayAlert(AppResources.IDM091, "0", "OK");
            string srtErrMsg = string.Empty;
            clsKaisouList lstKaisou = new clsKaisouList();
            if (clsWebUpdate.SendAddBeacon(clsGlobalVar.g_SasizuNo, user1.Text, ref srtErrMsg) == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM091, srtErrMsg, "OK");
                await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
            }
            else
            {
                //await DisplayAlert(AppResources.IDM125, "正常終了", "OK");
                await DisplayAlert("ビーコンマッチ登録完了", "正常終了", "OK");

                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString() };
                //if (bChange)
                //{
                //    await clsGlobalVar.SaveTextAsync();
                //    //System.Threading.Thread.Sleep(1000);
                //    await DisplayAlert(AppResources.IDM110, AppResources.IDM111, "OK");
                //}
                //await DisplayAlert(AppResources.IDM091, "2", "OK");
                freeThis();
                //await Navigation.PushAsync(new Page1(yourData));
                clsGlobalVar.g_ActMode = 0;
                Application.Current.MainPage = new Page1();
            }
        }
        else
        {
            //await Navigation.PopAsync();
            //await DisplayAlert(AppResources.IDM091, AppResources.IDM102, "OK");
        }
    }
    async void GoButtonClicked2(object sender, EventArgs s)
    {
        clsGlobalVar.g_SasizuNo = "-1";
        clsGlobalVar.g_ActMode = -1;

        //            clsGlobalVar.g_lastSashizuKind = 0 + 1;

        //await clsGlobalVar.SaveTextAsync();

        if (string.IsNullOrEmpty(clsGlobalVar.g_SasizuNo) == false)
        {
            //await DisplayAlert(AppResources.IDM091, "0", "OK");
            string srtErrMsg = string.Empty;
            clsKaisouList lstKaisou = new clsKaisouList();
            if (clsWebUpdate.SendCheckSashizu(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM091, srtErrMsg, "OK");
                await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
            }
            else
            {
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString() };
                //await DisplayAlert(AppResources.IDM091, "2", "OK");
                freeThis();
                //await Navigation.PushAsync(new Page1(yourData));
                Application.Current.MainPage = new Page1();
            }
        }
        else
        {
            //await Navigation.PopAsync();
            //await DisplayAlert(AppResources.IDM091, AppResources.IDM102, "OK");
            await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
        }
    }
    async void GoButtonClicked3(object sender, EventArgs s)
    {
        clsGlobalVar.g_SasizuNo = "-2";
        clsGlobalVar.g_ActMode = -2;

        //            clsGlobalVar.g_lastSashizuKind = 0 + 1;

        //await clsGlobalVar.SaveTextAsync();

        if (string.IsNullOrEmpty(clsGlobalVar.g_SasizuNo) == false)
        {
            //await DisplayAlert(AppResources.IDM091, "0", "OK");
            string srtErrMsg = string.Empty;
            clsKaisouList lstKaisou = new clsKaisouList();
            if (clsWebUpdate.SendCheckSashizu(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM091, srtErrMsg, "OK");
                await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
            }
            else
            {
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString() };
                //await DisplayAlert(AppResources.IDM091, "2", "OK");
                freeThis();
                //await Navigation.PushAsync(new Page1(yourData));
                Application.Current.MainPage = new Page1();
            }
        }
        else
        {
            //await Navigation.PopAsync();
            //await DisplayAlert(AppResources.IDM091, AppResources.IDM102, "OK");
            await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
        }
    }
    async void GoWebClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_SasizuNo = "-1";

        //await clsGlobalVar.SaveTextAsync();

        //await DisplayAlert(AppResources.IDM091, "0", "OK");
        freeThis();
        //await Navigation.PushAsync(new Page1(yourData));

        //neko
        //Application.Current.MainPage = new PageWeb();
        clsGlobalVar.g_optionurl = clsGlobalVar.GetCurURL() + "users/dailyreports/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID + "/" + DateTime.Now.ToString("yyyy-MM-dd");
        clsGlobalVar.g_JumpPage = "SashizuPage";
        Application.Current.MainPage = new webPage2();

    }
    private void freeThis()
    {
        label1 = null;
        user1 = null;
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


        //if (dropdown1 != null)
        //{
        //    dropdown1.Items.Clear();
        //    dropdown1 = null;
        //}
        button1.Clicked -= ScanButtonClicked;
        button2.Clicked -= GoButtonClicked;
        //button3.Clicked -= GoButtonClicked2;
        //button4.Clicked -= GoWebClicked;
        //button5.Clicked -= GoButtonClicked3;
        button1.ImageSource = null;
        button1 = null;
        button2 = null;
        button3 = null;
        button4 = null;
        button5 = null;
        Content2 = null;
        Content = null;
        GC.Collect();
    }





}