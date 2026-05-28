using System.Globalization;
using System.Runtime.Intrinsics.X86;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ZXing.Net.Maui;

//using UKensa12cross.Resx;

namespace FMES
{
    public partial class configPage : ContentPage
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Entry txturl;
        private HorizontalStackLayout Content2;
        private VerticalStackLayout Content3;
        private Picker dropdown1;
        private Picker dropdown3;
        private Button Scanbutton;
        private Button buttonUpd;
        private Button buttonEnd;

        public configPage()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            this.BackgroundColor = Colors.White;
            this.BackgroundColor = Colors.White;

            //this.Title = "環境設定";
            //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
            clsGlobalVar.g_NowForm = 1;

            Color defBKCol = Colors.White;

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

            txturl = new Entry
            {
                Keyboard = Keyboard.Text,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                Text = clsGlobalVar.g_CompanyURL,
                FontSize = 22,
                Placeholder = "URL",
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
            };
            // ↓added for QRScan
            if (clsGlobalVar.g_BackPage == "configPage" && clsGlobalVar.g_QRRET != null)
            {
                txturl.Text = clsGlobalVar.g_QRRET;
                clsGlobalVar.g_BackPage= string.Empty;
                clsGlobalVar.g_QRRET = string.Empty;
            }
            else
            {
                if(clsGlobalVar.g_CompanyURL== null)
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
                HorizontalOptions = LayoutOptions.Start, //,//中央に配置する（横方向）
                //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            Content2 = new HorizontalStackLayout()
            {
                Spacing = 25,
                Padding = new Thickness(30, 0),

                //BackgroundColor = Colors.White,
                BackgroundColor = defBKCol,
                Children = {
                    //label1,
                        //user1,
                        //Scanbutton,
                        label4,
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
                        label1,
                        dropdown1,
                        //label2,
                        
                        Scanbutton,
                        Content2,
                        //txturl,
                        //label4,
                        label3,
                        dropdown3,
                        buttonUpd,
                        buttonEnd,
                    }


                }
            };
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
            };
        }
        async void UpdButtonClicked(object sender, EventArgs s)
        {
            if (clsGlobalVar.g_language != dropdown1.SelectedIndex)
            {
                //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
            }
            clsGlobalVar.g_language = dropdown1.SelectedIndex;
            clsGlobalVar.g_logWrite = dropdown3.SelectedIndex;
            clsGlobalVar.g_CompanyURL = txturl.Text;
            clsGlobalVar.g_svUrlTop = txturl.Text;
            //clsGlobalVar.g_CompanyURL = clsGlobalVar.g_CompanyURL;
            clsGlobalVar.g_CompanyID = "";
            clsGlobalVar.g_CompanyPW = "";
            clsGlobalVar.g_ComRegisterd = 1;

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
            Application.Current.MainPage = new MainPage();
            //shelを利用する様に変更
            //Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

            //await Shell.Current.GoToAsync("//MainPage");
            //Shell.FlyoutBehaviorProperty = FlyoutBehavior.Flyout;
            
        }


    }

}