using System;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            //string wstr = DateTime.Now.ToString("yyyyMMddHHmmss");
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
                Text = "NAME",
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
                Text = "AUTH",
                FontSize = 22,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
            };
            label7 = new Label
            {
                //                //Text = AppResources.IDM003,
                //MaximumWidthRequest = 100,
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
            if (clsGlobalVar.g_BackPage == "configPage" && !string.IsNullOrEmpty(clsGlobalVar.g_QRRET))
            {
                txturl.Text = clsGlobalVar.g_QRRET;
                clsGlobalVar.g_BackPage= string.Empty;
                clsGlobalVar.g_QRRET = string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(clsGlobalVar.g_CompanyURL))
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
                    label6,
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
                    label5,
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
                        label1,
                        dropdown1,
                        //label2,
                        
                        
                        //Content2,
                        //Content4,
                        Scanbutton,
                        Content5,
                        //txturl,
                        //label4,
                        label3,
                        dropdown3,
                        buttonclear,
                        buttonUpd,
                        buttonEnd,
                    }


                }
            };
        }

        private async Task<bool> checkURLAsync()
        {
            bool ret = false;
            string strurl = txturl.Text;
            ret = IsValidUrl(strurl);
            if (!ret)
            {
              //URLを自動修正する
              int i1= strurl.IndexOf("https://");
                if (i1 == -1)
                {
                    strurl =    "https://" + strurl;
                    //txturl.Text = strurl;
                    ret = IsValidUrl(strurl);

                }
                int i2 = strurl.LastIndexOf("/");
                if (i2 != strurl.Length-1)
                {
                    strurl = strurl + "/";
                    txturl.Text = strurl;
                    ret = IsValidUrl(strurl);

                }
                //実在するURLかを確認する
                //ret = await UrlExistsAsync(strurl);
            }
            if (ret)
            {
              txturl.Text= strurl;
            }
            return ret;
            {
                await DisplayAlert("環境設定エラー", "URLが不正です", "OK");
            }

            return ret;
            static async Task<bool> UrlExistsAsync(string url)
            {
                try
                {
                    using HttpClient client = new();
                    HttpResponseMessage response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }

        }
        static bool IsValidUrl(string url)
        {
            string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }

        private string Gettrmcode()
        {
            string str_ret;
            //DateTime.Now.Month;
            str_ret = ComputeSHA256(DateTime.Now.ToString("yyyyMMddHHmmss")).Substring(0,10);

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
            };
        }

        async void clearButtonClicked(object sender, EventArgs s)
        {
            //認証初期化

            clsGlobalVar.g_auth = string.Empty;
            clsGlobalVar.g_trmcode = string.Empty;
            clsGlobalVar.g_trmname = string.Empty;

            clsGlobalVar.g_trmname = string.Empty;
            clsGlobalVar.g_auth = string.Empty;
            clsGlobalVar.g_trmcode = string.Empty;
            //環境設定保存
            clsGlobalVar.SaveConfig();
            await DisplayAlert("            認証初期化", "            認証初期化完了", "OK");
            //freeThis();
            //await Navigation.PushAsync(new MainPage(yourData));
            //Application.Current.MainPage = new MainPage(yourData);
            //Application.Current.MainPage = new MainPage();
            return;
        }


        async void UpdButtonClicked(object sender, EventArgs s)
        {
            checkURLAsync();

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



            //↓認証用
            if (string.IsNullOrEmpty(clsGlobalVar.g_CompanyURL) || string.IsNullOrEmpty(txturl.Text))
            {
                //URL未登録の場合
                await DisplayAlert("環境設定エラー", "サーバURLを入力してください", "OK");
                return;


            }
            //↑認証用




            //環境設定保存
            clsGlobalVar.SaveConfig();
            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                freeThis();
                Application.Current.MainPage = new configPage2();
            }
            else
            {
                {
                    freeThis();
                    Application.Current.MainPage = new MainPage();
                }
            }
//↓認証用
            if (!string.IsNullOrEmpty(clsGlobalVar.g_CompanyURL) && string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                //Application.Current.MainPage = new configPage2();
                //freeThis();
                //Application.Current.MainPage = new configPage2();
                //Application.Current.MainPage = new MainPage();
            }
            //↑認証用
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
        private void freeThis()
        {
           GC.Collect();
        }


    }

}