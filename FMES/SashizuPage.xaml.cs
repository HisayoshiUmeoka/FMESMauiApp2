using System.Globalization;
using ZXing.Net.Maui;


namespace FMES;

public partial class SashizuPage : ContentPage
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
    private HorizontalStackLayout Content2;
    // private Picker dropdown1;
    private Picker dropdown2;

    private List<clsSashizudat> lstRec;
    private clsoptionList lstoption = new clsoptionList();

    private List<Button> Lstbutton = new List<Button>();
    //private HorizontalStackLayout ContentMenu;
    private ScrollView sv;
    private VerticalStackLayout layout3;
    private bool doingNow = false;

    public SashizuPage()
    {
        InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        //clsGlobalVar.g_SashizuMode = 0;//debug用

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
        layout3 = new VerticalStackLayout()
        {
            Margin = new Thickness(0, 30, 0, 0),
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Colors.White,
            Padding = new Thickness(10, 10, 10, 10),

        };
        string srtErrMsg = string.Empty;
        lstoption = new clsoptionList();
        //clsGlobalVar.g_KouteiID = 1;    // テスト用

        if (lstoption.GetList(ref srtErrMsg) == true)
        {

        }


        if (clsGlobalVar.g_SashizuMode == 1)
        {
            //QRモードの場合

            user1 = new Entry
            {
                Keyboard = Keyboard.Text,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 20,
                WidthRequest = 250,
                //Placeholder = AppResources.IDM098,
                Placeholder = "指図番号",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            // ↓added for QRScan
            if (clsGlobalVar.g_BackPage == "SashizuPage" && clsGlobalVar.g_QRRET != null)
            {
                user1.Text = clsGlobalVar.g_QRRET;
                clsGlobalVar.g_BackPage = string.Empty;
                clsGlobalVar.g_QRRET = string.Empty;

            }
            // ↑added for QRScan

            button1 = new Button
            {
                //Text = "ＱＲスキャン",
                ImageSource = "Qr100x100.png",
                FontSize = 20,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.End//,//中央に配置する（横方向）
                                                        //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            Content2 = new HorizontalStackLayout()
            {
                BackgroundColor = Colors.White,
                Children = {
                        user1,
                        button1,
                    }
            };
        }
        else
        {
            //セレクトモードの場合
            lstRec = clsWebUpdate.GetListCommand(clsGlobalVar.g_UserID);
            if (lstRec != null)
            {
                dropdown2 = new Picker
                {
                    FontSize = 20,
                    //Title = "選択又はQRを読み込んでください",
                    //Title = AppResources.IDM098,
                    Title = "指図番号",
                    //Title = AppResources.IDM103,
                    VerticalOptions = LayoutOptions.Start
                };
                foreach (clsSashizudat wwdat in lstRec)
                {
                    dropdown2.Items.Add(wwdat._SashizuName);
                }

            }





        }


        button2 = new Button
        {
            Margin = new Thickness(0, 30, 0, 0),
            //Text = AppResources.IDM099,//"　決定　",
            Text = "決定",
            FontSize = 20,
            //BackgroundColor = Colors.DodgerBlue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),

            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        button5 = new Button
        {
            Margin = new Thickness(0, 30, 0, 0),
            //Text = AppResources.IDM117,//"　指図番号無し作業　",
            Text = "指図番号無し作業",
            FontSize = 20,
            //BackgroundColor = Colors.DodgerBlue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };

        button3 = new Button
        {
            Margin = new Thickness(0, 30, 0, 0),
            //Text = AppResources.IDM112,//"　その他　",
            Text = "その他",//"その他",
            FontSize = 20,
            //BackgroundColor = Colors.DodgerBlue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        button4 = new Button
        {
            Margin = new Thickness(0, 30, 0, 0),
            //Text = AppResources.IDM113,//"　日報確認　",
            Text = "日報確認",//"　日報確認　",
            FontSize = 20,
            //BackgroundColor = Colors.DodgerBlue,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
            HorizontalOptions = LayoutOptions.Fill,
            //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };

        if (clsGlobalVar.g_SashizuMode == 1)
        {
            layout3.Children.Add(ContentMenu);
            layout3.Children.Add(Content2);
            layout3.Children.Add(button2);
            layout3.Children.Add(button5);
            layout3.Children.Add(button3);
            layout3.Children.Add(button4);
            //layout3.Children.Add(button2);
            //layout3.Children.Add(button2);
            foreach (clsoptionData woption in lstoption._Datas)
            {
                Button butn = new Button
                {
                    Margin = new Thickness(0, 30, 0, 0),
                    Text = woption._optionName,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    TextColor = GetTextColorParts(),
                    BackgroundColor = GetBackColorParts(),

                };
                butn.Clicked += ItemButtonClicked;
                layout3.Children.Add(butn);
                Lstbutton.Add(butn);

            }


            button1.Clicked += ScanButtonClicked;
        }
        else
        {
            layout3.Children.Add(ContentMenu);
            //                layout3.Children.Add(Content2);
            layout3.Children.Add(dropdown2);
            layout3.Children.Add(button2);
            layout3.Children.Add(button5);
            layout3.Children.Add(button3);
            layout3.Children.Add(button4);
            //layout3.Children.Add(button2);
            //layout3.Children.Add(button2);
            foreach (clsoptionData woption in lstoption._Datas)
            {
                Button butn = new Button
                {
                    Margin = new Thickness(0, 30, 0, 0),
                    Text = woption._optionName,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = GetTextColorParts(),
                    BackgroundColor = GetBackColorParts(),

                };
                butn.Clicked += ItemButtonClicked;
                layout3.Children.Add(butn);
                Lstbutton.Add(butn);

            }
        }
        button2.Clicked += GoButtonClicked;
        button3.Clicked += GoButtonClicked2;
        button4.Clicked += GoWebClicked;
        button5.Clicked += GoButtonClicked3;











        this.Content = new ScrollView
        {
            Content = layout3,


        };
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

    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "SashizuPage";
        //freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu




    async void GoButtonClicked(object sender, EventArgs s)
    {
        //bool bChange = (clsGlobalVar.g_lastSashizuKind == dropdown1.SelectedIndex + 1)? false : true;
        if (clsGlobalVar.g_SashizuMode == 1)
        {
            clsGlobalVar.g_SasizuNo = user1.Text;
        }
        else
        {
            if (dropdown2.SelectedIndex != -1)
            {
                clsGlobalVar.g_SasizuNo = lstRec[dropdown2.SelectedIndex]._SashizuName;
            }
            else
            {
                //await DisplayAlert(AppResources.IDM091, AppResources.IDM148, "OK");
                await DisplayAlert("指図番号エラー", "指図番号の選択又は入力してください", "OK");
                return;
            }
            //clsGlobalVar.g_SasizuNo = lstRec[dropdown2.SelectedIndex]._SashizuName;



        }

        //clsGlobalVar.g_lastSashizuKind = dropdown1.SelectedIndex + 1;
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
            await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
        }
    }
    async void GoButtonClicked2(object sender, EventArgs s)
    {
        //-1と-2が逆になってた。
        //clsGlobalVar.g_SasizuNo = "-2";
        clsGlobalVar.g_ActMode = -2;
        clsGlobalVar.g_SasizuNo = "-1"; //その他
        //clsGlobalVar.g_ActMode = -1;

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
        //-1と-2が逆になってた。
        //clsGlobalVar.g_SasizuNo = "-1";
        //clsGlobalVar.g_ActMode = -1;
        clsGlobalVar.g_SasizuNo = "-2"; //指図番号無し作業
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
        clsGlobalVar.g_JumpPage = "SashizuPage";
        //        Application.Current.MainPage = new PageWeb();

        clsGlobalVar.g_optionurl = clsGlobalVar.GetCurURL() + "users/dailyreports/" + clsGlobalVar.GetLanguageStr() + "/" + clsGlobalVar.g_UserID + "/" + DateTime.Now.ToString("yyyy-MM-dd");
        clsGlobalVar.g_JumpPage = "SashizuPage";
        Application.Current.MainPage = new webPage2();


    }
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
                    if (string.IsNullOrEmpty(lstoption._Datas[i]._optionURL) == false)
                    {
                        //実行可能な物
                        clsGlobalVar.g_optionurl = lstoption._Datas[i]._optionURL;
                        freeThis();
                        clsGlobalVar.g_JumpPage = "SashizuPage";
                        Application.Current.MainPage = new webPage2();
                    }
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }









    async void ScanButtonClicked(object sender, EventArgs s)
    {
        //tako
        // ↓added for QRScan
        clsGlobalVar.g_BackPage = "SashizuPage";
        clsGlobalVar.g_QRRET = string.Empty;
        Application.Current.MainPage = new QRPage();
        // ↑added for QRScan

        //tako
        //ここにQRSCAN実処理を入れる。
        //var scanPage = new ZXing.Net.Maui.Views.CameraBarcodeReaderView();
        //var result = await scanPage.ScanAsync();

        //if (result != null)
        //{
        //    await DisplayAlert("スキャン完了", result.Text, "OK");
        //    user1.Text = result.Text;


        //}
    }
}

