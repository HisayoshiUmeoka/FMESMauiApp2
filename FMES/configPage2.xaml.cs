using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;

namespace FMES;

public partial class configPage2 : ContentPage
{
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Entry txturl;
    //↓認証用
    private Entry txtrmname;
    private Entry txtauth;
    //↑認証用
    private HorizontalStackLayout Content2;
    private HorizontalStackLayout Content4;
    private HorizontalStackLayout Content5;
    private VerticalStackLayout Content3;
    private Picker dropdown1;
    private Picker dropdown3;
    private Button Scanbutton;
    private Button buttonclear;
    private Button buttonUpd;
    private Button buttonEnd;

    public configPage2()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //this.Title = "環境設定";
        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 1;

        Color defBKCol = Colors.White;
        string wstr = DateTime.Now.ToString("yyyyMMddHHmmss");
        this.BackgroundColor = defBKCol;
        label1 = new Label
        {
            //                //Text = AppResources.IDM003,
            Text = "言語",
            FontSize = 22,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        dropdown1 = new Picker
        {
            FontSize = 16,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            //Title = AppResources.IDM004,
            Title = "未選択",
            VerticalOptions = LayoutOptions.Start
        };
        //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
        dropdown1.Items.Add("Japanese");
        dropdown1.Items.Add("English");
        dropdown1.Items.Add("Chinese");
        dropdown1.Items.Add("Korean");
        dropdown1.SelectedIndex = clsGlobalVar.g_language;
        label4 = new Label
        {


            //                //Text = AppResources.IDM003,
            Text = "URL",
            FontSize = 22,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        txtrmname = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            Text = "",
            FontSize = 22,
            Placeholder = "NAME",
            MaximumWidthRequest = 200,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
        };
        txtauth = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            Text = "",
            FontSize = 22,
            Placeholder = "AUTH",
            MaximumWidthRequest = 200,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
        };

        label5 = new Label
        {
            //                //Text = AppResources.IDM003,
            MaximumWidthRequest = 100,
            Text = "NAME:",
            FontSize = 22,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
        };
        label6 = new Label
        {
            //                //Text = AppResources.IDM003,
            MaximumWidthRequest = 100,
            Text = "AUTH:",
            FontSize = 22,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
        };
        label7 = new Label
        {
            //                //Text = AppResources.IDM003,
            MaximumWidthRequest = 100,
            Text = "URL",
            FontSize = 22,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
        };

        txturl = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            Text = clsGlobalVar.g_CompanyURL,
            FontSize = 22,
            Placeholder = "URL",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill,
        };
        // ↓added for QRScan
        if (clsGlobalVar.g_BackPage == "configPage" && clsGlobalVar.g_QRRET != null)
        {
            txturl.Text = clsGlobalVar.g_QRRET;
            clsGlobalVar.g_BackPage = string.Empty;
            clsGlobalVar.g_QRRET = string.Empty;
        }
        else
        {
            if (clsGlobalVar.g_CompanyURL == null)
            {
                txturl.Text = "https://";
            }
        }
        // ↑added for QRScan

        Scanbutton = new Button
        {
            //Text = "ＱＲスキャン",
            ImageSource = "qr100x100.png",
            FontSize = 20,
            //BackgroundColor = Colors.White,
            BackgroundColor = defBKCol,
            HorizontalOptions = LayoutOptions.End, //,//中央に配置する（横方向）
                                                   //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
        };
        Content2 = new HorizontalStackLayout()
        {
            Spacing = 25,
            Padding = new Thickness(30, 0),

            //BackgroundColor = Colors.White,
            BackgroundColor = defBKCol,
            Children = {
                    label5,
                        //user5,
                        //Scanbutton,
                        txtrmname,
                        //txturl,
                    }
        };
        Content4 = new HorizontalStackLayout()
        {
            Spacing = 25,
            Padding = new Thickness(30, 0),

            //BackgroundColor = Colors.White,
            BackgroundColor = defBKCol,
            Children = {
                    label6,
                        //user1,
                        //Scanbutton,
                        txtauth,
                        //txturl,
                    }
        };
        Content5 = new HorizontalStackLayout()
        {
            Spacing = 25,
            Padding = new Thickness(30, 0),

            //BackgroundColor = Colors.White,
            BackgroundColor = defBKCol,
            Children = {
                    label7,
                        //user1,
                        //Scanbutton,
                        //txtauth,
                        txturl,
                    }
        };
        Scanbutton.Clicked += ScanButtonClicked;

        label3 = new Label
        {
            //Text = AppResources.IDM005,
            Text = "ログ",
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontSize = 22,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Center,
        };
        dropdown3 = new Picker
        {
            FontSize = 16,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            //Title = AppResources.IDM004,
            Title = "未選択",
            VerticalOptions = LayoutOptions.Start
        };
        //dropdown3.Items.Add(AppResources.IDM006);
        //dropdown3.Items.Add(AppResources.IDM007);
        dropdown3.Items.Add("ログ送信無し");
        dropdown3.Items.Add("ログ送信有り");
        dropdown3.SelectedIndex = clsGlobalVar.g_logWrite;
        buttonclear = new Button
        {
            //Text = AppResources.IDM008,
            Text = "認証初期化",
            FontSize = 22,
            //VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
        };

        buttonUpd = new Button
        {
            //Text = AppResources.IDM008,
            Text = "ＯＫ",
            FontSize = 22,
            //VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = GetTextColorParts(),
            BackgroundColor = GetBackColorParts(),
        };
        buttonEnd = new Button
        {
            //Text = AppResources.IDM009,
            Text = "キャンセル",
            FontSize = 22,
            //VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = Colors.Black,
            BackgroundColor = Colors.LightGray,
        };
        buttonclear.Clicked += clearButtonClicked;
        buttonUpd.Clicked += UpdButtonClicked;
        buttonEnd.Clicked += EndButtonClicked;





        this.Content = new ScrollView
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,

            Content = new VerticalStackLayout
            {
                //縦方向
                Spacing = 25,
                Padding = new Thickness(30, 0),
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.White,
                Children = {
                        //label1,
                        //dropdown1,
                        //label2,
                        
                        
                        Content2,
                        Content4,
                        //Scanbutton,
                        //Content5,
                        //txturl,
                        //label4,
                        //label3,
                        //dropdown3,
                        //buttonclear,
                        buttonUpd,
                        buttonEnd,
                    }


            }
        };
    }
    private string Gettrmcode()
    {
        string str_base= DateTime.Now.ToString("yyyyMMddHHmmss");
        string str_ret = str_base.Substring(0, 10);
        //DateTime.Now.Month;
        try
        {
            str_ret = ComputeSHA256(str_base).Substring(0, 10);

        }
        catch (Exception)
        {
            str_ret = str_base.Substring(0, 10);

            //throw;
        }

        return str_ret;
    }
    static string ComputeSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
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
    async void ScanButtonClicked(object sender, EventArgs s)
    {
        //tako
        //QRSCAN実処理をここで行う


        //debugdummy
        //txturl.Text = "https://fmes.5ms.cloud/";

        // ↓added for QRScan
        clsGlobalVar.g_BackPage = "configPage";
        clsGlobalVar.g_QRRET = string.Empty;
        Application.Current.MainPage = new QRPage();
        // ↑added for QRScan


        //DefaultOverlayTopText = AppResources.IDM015,
        //DefaultOverlayBottomText = "ＱＲコードを読み取ります",

        // スキャナページを表示

        // PopAsyncで元のページに戻り、結果をダイアログで表示
        {
            //tako
            //ここにQRSCAN実処理を入れる。
            //var scanPage = new ZXing.Net.Maui.Views.CameraBarcodeReaderView();
            //var result = await scanPage.ScanAsync();

            //if (result != null)
            //{
            //    await DisplayAlert("スキャン完了", result.Text, "OK");
            //    txturl.Text = result.Text;
            //}

            //Application.Current.MainPage = this;
            //await DisplayAlert("スキャン完了", result.Text, "OK");

            //label4.Text = result.Text;

            //SCAN結果をtxturl.Textに代入する
            //txturl.Text = result.Text;

            //user1.Text = result.Text;
            //user2.Text = "";

            clsGlobalVar.g_svUrlTop = txturl.Text;
            clsGlobalVar.g_CompanyURL = txturl.Text;


            //scanedData.Add(result.Text);
        }
        ;
    }

    async void clearButtonClicked(object sender, EventArgs s)
    {
        //認証初期化

        string trmcode = "";

        if (clsGlobalVar.g_language != dropdown1.SelectedIndex)
        {
        }



        //↓認証用
        {
            //URL登録済みの場合
            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                //未認証の場合
                //trmcodeを取得する
                trmcode = Gettrmcode();
                if (string.IsNullOrEmpty(txtauth.Text) || txtauth.Text.Length < 5)
                {
                    //認証条件未達の場合
                    await DisplayAlert("環境設定エラー", "正しいAUTHを入力してください", "OK");
                    return;

                }
                else
                {
                    if (string.IsNullOrEmpty(txtrmname.Text))
                    {
                        await DisplayAlert("環境設定エラー", "NAMEを入力してください", "OK");
                        return;
                    }
                    else
                    {
                        //送信条件を満たした場合

                        //認証コマンドを送信する
                        string strErrMsg = "";
                        bool ret = clsWebUpdate.SendTrmData(txtauth.Text, trmcode, txtrmname.Text, ref strErrMsg);
                        //bool ret = clsWebUpdate.SendTrmData(txtrmname.Text, trmcode, txtauth.Text, ref strErrMsg);
                        if (ret == false)
                        {
                            await DisplayAlert("環境設定エラー", strErrMsg, "OK");
                            return;
                        }
                        else
                        {
                            await DisplayAlert("環境設定完了", "認証正常完了", "OK");
                        }

                    }
                }

            }
        }
        //↑認証用



        //clsGlobalVar.g_auth = txtauth.Text;
        //clsGlobalVar.g_trmcode = trmcode;
        clsGlobalVar.g_trmname = txtrmname.Text;

        clsGlobalVar.g_trmname = txtrmname.Text;
        clsGlobalVar.g_auth = txtauth.Text;
        clsGlobalVar.g_trmcode = trmcode;
        //環境設定保存
        clsGlobalVar.SaveConfig();
        string[] yourData = { clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), clsGlobalVar.g_lastSashizuKind.ToString() };
        //freeThis();
        //await Navigation.PushAsync(new MainPage(yourData));
        //Application.Current.MainPage = new MainPage(yourData);
        Application.Current.MainPage = new MainPage();
    }


    async void UpdButtonClicked(object sender, EventArgs s)
    {
        string trmcode = "";

        if (clsGlobalVar.g_language != dropdown1.SelectedIndex)
        {
        }



        //↓認証用
        {
            //URL登録済みの場合
            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                //未認証の場合
                //trmcodeを取得する
                trmcode = Gettrmcode();
                if (string.IsNullOrEmpty(txtauth.Text) || txtauth.Text.Length < 5)
                {
                    //認証条件未達の場合
                    await DisplayAlert("環境設定エラー", "正しいAUTHを入力してください", "OK");
                    return;

                }
                else
                {
                    if (string.IsNullOrEmpty(txtrmname.Text))
                    {
                        await DisplayAlert("環境設定エラー", "NAMEを入力してください", "OK");
                        return;
                    }
                    else
                    {
                        //送信条件を満たした場合
 
                        //認証コマンドを送信する
                        string strErrMsg = "";
                        bool ret = clsWebUpdate.SendTrmData(txtauth.Text, trmcode, txtrmname.Text, ref strErrMsg);
                        //bool ret = clsWebUpdate.SendTrmData(txtrmname.Text, trmcode, txtauth.Text, ref strErrMsg);
                        if (ret == false)
                        {
                            await DisplayAlert("環境設定エラー", strErrMsg, "OK");
                            return;
                        }
                        else
                        {
                            await DisplayAlert("環境設定完了", "認証正常完了", "OK");
                        }

                    }
                }

            }
        }
        //↑認証用



        //clsGlobalVar.g_auth = txtauth.Text;
        //clsGlobalVar.g_trmcode = trmcode;
        clsGlobalVar.g_trmname = txtrmname.Text;

        clsGlobalVar.g_trmname = txtrmname.Text;
        clsGlobalVar.g_auth = txtauth.Text;
        clsGlobalVar.g_trmcode = trmcode;
        //環境設定保存
        clsGlobalVar.SaveConfig();
        string[] yourData = { clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), clsGlobalVar.g_lastSashizuKind.ToString() };
        //freeThis();
        //await Navigation.PushAsync(new MainPage(yourData));
        //Application.Current.MainPage = new MainPage(yourData);
        Application.Current.MainPage = new MainPage();
    }

    async void EndButtonClicked(object sender, EventArgs s)
    {
        //await Navigation.PushAsync(new MainPage());
        Application.Current.MainPage = new configPage();
        //shelを利用する様に変更
        //Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

        //await Shell.Current.GoToAsync("//MainPage");
        //Shell.FlyoutBehaviorProperty = FlyoutBehavior.Flyout;

    }

}
