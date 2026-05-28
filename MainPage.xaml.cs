using System.Globalization;
using System.Runtime.Intrinsics.X86;
using ZXing.Net.Maui;

//using FMES.Resx;
//using UKensa12cross.Resx;

namespace FMES
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Entry user1;
        private Entry user2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Image imgLogo;
        private StackLayout Content2;
        private HorizontalStackLayout Content3;
        private HorizontalStackLayout Content4;
        private Button buttonOCR;


        public MainPage()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            //this.Title = "LOGIN";
            App.Current.UserAppTheme = AppTheme.Light;
            //App.Current.UserAppTheme = AppTheme.Light;
            Console.WriteLine($"Current Theme: {App.Current.UserAppTheme}");
            //App.Current.UserAppTheme = AppTheme.Dark;

            this.BackgroundColor = Colors.White;
            clsGlobalVar.g_NowForm = 0;
            // AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());

            //コントロール類の描画開始

            imgLogo = new Image
            {
                Source = ImageSource.FromFile("logo.png"),
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
            };


            // StackLayoutで2つの Entryコントロールを並べる
            this.BackgroundColor = Colors.White;

            user1 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                WidthRequest= 150,
                Placeholder = "Login ID",
                Margin = new Thickness(0, 2, 0, 2),

                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            // ↓added for QRScan
            if (clsGlobalVar.g_BackPage == "MainPage")
            {
                user1.Text = clsGlobalVar.g_QRRET;
            }
            // ↑added for QRScan

            button1 = new Button
            {
                //Text = "ＱＲスキャン",
                ImageSource = "qr100x100.png",
                FontSize = 20,
                //WidthRequest=80,
                //HeightRequest=80,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };
            button1.Clicked += OnQRScanClicked;

            Content3 = new HorizontalStackLayout
            {
                Spacing = 25,
                //                Padding = new Thickness(30, 0),
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(2, 2, 2, 2),
                VerticalOptions = LayoutOptions.Start,

                BackgroundColor = Colors.White,
                Children = {
                        user1,
                        button1,
                    }
            };
            label1 = new Label
            {
                //                //Text = AppResources.IDM003,
                MaximumWidthRequest = 100,
                Text = "　　",
                FontSize = 22,
                BackgroundColor = Colors.White,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            user2 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                WidthRequest = 150,
                //BackgroundColor = Colors.White,
                TextColor = Colors.Black,

                Placeholder = "Password",
                Margin = new Thickness(0, 2, 0, 2),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                IsPassword = true, // 入力された文字を隠す
            };
            Content4 = new HorizontalStackLayout
            {
                Spacing = 25,
                //                Padding = new Thickness(30, 0),
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(2, 2, 2, 2),

                VerticalOptions = LayoutOptions.Start,

                BackgroundColor = Colors.White,
                Children = {
                        user2,
                        label1,
                    }
            };


            button2 = new Button
            {
                Text = "Login",
                FontSize = 20,
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.DodgerBlue,
                //BackgroundColor = Colors.Blue,

                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),

                HorizontalOptions = LayoutOptions.Fill,
                //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            button1.Clicked += OnQRScanClicked;
            button2.Clicked += CheckButtonClicked;
            label3 = new Label
            {
                //Text = "Version Name:" + DependencyService.Get<IAssemblyService>().GetVersionName() + "  Version Code:" + DependencyService.Get<IAssemblyService>().GetVersionCode().ToString(),
                Text = "Version Name:" + AppInfo.VersionString.ToString(),
                FontSize = 15,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
            };
            button3 = new Button
            {
                Text = "環境設定",
                FontSize = 20,
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.DodgerBlue,
                //BackgroundColor = Colors.Blue,

                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),

                HorizontalOptions = LayoutOptions.Fill,
                //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            button3.Clicked += ConfigButtonClicked;


            label2 = new Label
            {
                //Text = "\n\nCopyright © ULVAC CRYOGENICS INC. All rights reserved.",
                Text = "Copyright © Five Motion Systems,Inc. All rights reserved.",
                //Text = "\n\n" + AppResources.FMESCPYL,//FMES用
                //Text =  "\n\n" + AppResources.UMESCPYL,//UMES用
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 10,
                HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
                VerticalOptions = LayoutOptions.End // 中央に配置する（縦方向
            };
            


            int iMonth = DateTime.Now.Month;
#if IOS
            string imgFoot = "cal" + iMonth.ToString() + "m.png";
#else
            string imgFoot = "cal" + iMonth.ToString() +   "m.png";
#endif
            Image imgFooter = new Image
            {
                Margin = new Thickness(20, 20, 20, 20),
                Source = ImageSource.FromFile(imgFoot),
                Aspect = Aspect.AspectFill,
                BackgroundColor = Colors.White,
                //HorizontalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.End,
            };




                this.Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    //縦方向
                    Spacing = 25,
                    Padding = new Thickness(30, 0),
                    VerticalOptions = LayoutOptions.Center,
                    Children ={
                        imgLogo,
                        Content3,
                        Content4,
                        //user2,
                        button2,
                        button3,
                        label2,
                        label3,
                        imgFooter,
                    }



                }
                };
            //user1.Visual = VisualMarker.Default;
            //user2.Visual = VisualMarker.Default;
            //user1.BackgroundColor = Colors.Gray;
            //user2.BackgroundColor = Colors.Gray;
            //user1.Opacity = 0;
            //user1.FadeTo(1, 2000);
            //user2.Opacity = 0;
            //user2.FadeTo(1, 2000);






        }
        private void OnQRScanClicked(object sender, EventArgs e)
        {
            //tako
            //ここにQRSCAN実処理を入れる。
            // ↓added for QRScan
            clsGlobalVar.g_BackPage = "MainPage";
            clsGlobalVar.g_QRRET = string.Empty;
            Application.Current.MainPage = new QRPage();
            // ↑added for QRScan




            //SCAN結果はuser1.textに入力する事

            //user1.Text = "001";
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
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
        private void subInit()
        {
//            AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
            // StackLayoutで2つの Entryコントロールを並べる
            this.BackgroundColor = Colors.White;

            user1 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,

                Placeholder = "Login ID",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };
            if (clsGlobalVar.g_BackPage == "MainPage")
            {
                user1.Text = clsGlobalVar.g_QRRET;
            }
            // ↑added for QRScan

            button1 = new Button
            {
                //Text = "ＱＲスキャン",
                ImageSource = "QR100x100.png",
                FontSize = 20,
                //WidthRequest=80,
                //HeightRequest=80,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
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
            user2 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,

                Placeholder = "Password",
                IsPassword = true, // 入力された文字を隠す
            };
            button2 = new Button
            {
                Text = "Login",
                FontSize = 20,
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.DodgerBlue,
                //BackgroundColor = Colors.Blue,

                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),

                HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            button3 = new Button
            {
                Text = "環境設定",
                FontSize = 20,
                //TextColor = Colors.Black,
                //BackgroundColor = Colors.DodgerBlue,
                //BackgroundColor = Colors.Blue,

                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),

                HorizontalOptions = LayoutOptions.Fill,
                //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
            button3.Clicked += ConfigButtonClicked;


            imgLogo = new Image
            {
                Source = ImageSource.FromFile("logo.png"),
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
            };
            label3 = new Label
            {
                //Text = "Version Name:" + DependencyService.Get<IAssemblyService>().GetVersionName() + "  Version Code:" + DependencyService.Get<IAssemblyService>().GetVersionCode().ToString(),
                Text = "Version Name:" + AppInfo.VersionString.ToString(),
                FontSize = 15,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
                VerticalOptions = LayoutOptions.End // 中央に配置する（縦方向
            };
            label2 = new Label
            {
                //Text = "\n\nCopyright © ULVAC CRYOGENICS INC. All rights reserved.",
                //Text = "\n\n" + AppResources.FMESCPYL,//FMES用
                Text = "\n\nCopyright © Five Motion Systems,Inc. All rights reserved.",//FMES用
                //Text =  "\n\n" + AppResources.UMESCPYL,//UMES用
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 10,
                HorizontalOptions = LayoutOptions.Center,//中央に配置する（横方向）
                VerticalOptions = LayoutOptions.End // 中央に配置する（縦方向
            };
            buttonOCR = new Button
            {
                Text = "　OCR　",
                FontSize = 20,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center//,//中央に配置する（横方向）
                                                        //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
            };
        }
        async void ConfigButtonClicked(object sender, EventArgs s)
        {
            //環境設定
            //freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            Application.Current.MainPage = new configPage();

        }
        async void CheckButtonClicked(object sender, EventArgs s)
        {
            string ex_message = string.Empty;
            int iRetLogin = GetLoginVerify(user1.Text, user2.Text, ref ex_message);
            if (iRetLogin > 0)
            {
                //_UserID = iRetLogin;
                //Navigation.PushAsync(SashizuPage);
                //string[] yourData = { _UserID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString() };
                //freeThis();
                //await Navigation.PushAsync(new SashizuPage(yourData));
                Application.Current.MainPage = new SashizuPage();

                //Application.Current.MainPage = new PageParts();
            }
            else if (iRetLogin == 0)
            {
                //await Navigation.PopAsync();
                //DisplayAlert(AppResources.IDM018, AppResources.IDM019, "OK");
                DisplayAlert("ログインエラー", "ログインID又はパスワードを確認してください", "OK");
            }
            else if (iRetLogin == -1)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM020, AppResources.IDM021, "OK");
                await DisplayAlert("環境設定エラー", "サーバURLを確認してください", "OK");
            }
            else if (iRetLogin == -2)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM018, ex_message, "OK");
                await DisplayAlert("ログインエラー", ex_message, "OK");
            }
        }
        private int GetLoginVerify(string wID, string wPW, ref string exmessage)
        {
            int iRet = 0;
            string strSend = clsGlobalVar.GetCurURL() + "users/tablogin2/" + wID + "/" + wPW;
            try
            {
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                //ver.32add
                if (clsWebUpDown.aborted == true)
                {
                    DisplayAlert("通信エラー", clsWebUpDown.err_message, "OK");
                }

                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {
                        iRet = 1;
                        clsGlobalVar.g_Parmit = 1;
                        if (strRet.IndexOf("OK2") > -1)
                        {
                            iRet = 2;
                            clsGlobalVar.g_Parmit = 2;
                        }
                        strRet = strRet.Replace("< !--ダミー-- >", "");
                        int iIndex = 0;
                        while (strRet.Length > 0)
                        {
                            int iNo1 = strRet.IndexOf(",");
                            string strW2;
                            if (iNo1 > -1)
                            {
                                strW2 = strRet.Substring(0, iNo1).Trim();
                                strRet = strRet.Substring(iNo1 + 1);
                            }
                            else
                            {
                                strW2 = strRet.Trim();
                                strRet = "";
                            }
                            if (string.IsNullOrEmpty(strW2) == false)
                            {
                                if (iIndex == 0)
                                {
                                    // OK or OK2 or NG
                                    // partsID = int.Parse(strW2);
                                }
                                else if (iIndex == 1)
                                {
                                    clsGlobalVar.g_UserID = int.Parse(strW2);
                                    //_PartsName = strW2;
                                }
                                else if (iIndex == 2)
                                {
                                    clsGlobalVar.g_SashizuMode = int.Parse(strW2);
                                }
                                else if (iIndex == 3)
                                {
                                    clsGlobalVar.g_Operator = strW2;

                                }
                                else if (iIndex == 4)
                                {
                                    clsGlobalVar.g_PartDisp = int.Parse(strW2);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            iIndex += 1;
                        }






                    }
                    else if (strRet.IndexOf("NG") > -1)
                    {
                        iRet = 0;
                        clsGlobalVar.g_Parmit = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                //iRet = -1;
                iRet = -2;
                clsGlobalVar.g_Parmit = 0;
                exmessage = ex.Message;
                //await DisplayAlert(ex.Message, "OK");

            }

            return iRet;
        }

    }
}
