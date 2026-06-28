using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMES
{
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
            // セーフエリアを無効化
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            // ContentPageのパディングを0に
            this.Padding = new Thickness(0);

            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            // ページ全体は白背景（要求どおり）
            //            this.BackgroundColor = Color.FromArgb("#D1D5DB");
            App.Current.UserAppTheme = AppTheme.Light;
            Console.WriteLine($"Current Theme: {App.Current.UserAppTheme}");

            // モダンなグラデーション背景
            this.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#F0F4F8"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#E2E8F0"), Offset = 1.0f }
                }
            };


            // Version ラベル（小さめ）
            labelVer = new Label
            {
                Text = "Version Name: " + AppInfo.VersionString,
                FontSize = 12,
                TextColor = Color.FromArgb("#6B7280"), // muted gray
                HorizontalOptions = LayoutOptions.Center
            };

            // ボタン共通の設定（モダン化）
            buttonConfig = new Button
            {
                Text = "settings",
                FontSize = 16,
                HeightRequest = 48,
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0),
                Padding = new Thickness(12, 0),
                //BackgroundColor = GetBackColorParts(),
                //TextColor = GetTextColorParts(),
                                TextColor = Color.FromArgb("#3B82F6"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#3B82F6"),
                BorderWidth = 1.5,

            };

            buttonUserchange = new Button
            {
                Text = "ユーザー切替",
                FontSize = 16,
                HeightRequest = 48,
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0),
                Padding = new Thickness(12, 0),
                BackgroundColor = Colors.White,
                TextColor = Color.FromArgb("#111827"),
                BorderColor = Color.FromArgb("#E5E7EB"),
                BorderWidth = 1
            };

            buttonLogout = new Button
            {
                Text = "ログアウト",
                FontSize = 16,
                HeightRequest = 48,
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0),
                Padding = new Thickness(12, 0),
                BackgroundColor = Colors.White,
                TextColor = Color.FromArgb("#DC2626"),
                BorderColor = Color.FromArgb("#FEE2E2"),
                BorderWidth = 1
            };

            buttonback = new Button
            {
                Text = "戻る",
                FontSize = 16,
                HeightRequest = 44,
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0),
                Padding = new Thickness(12, 0),
                BackgroundColor = Colors.White,
                TextColor = Color.FromArgb("#374151"),
                BorderColor = Color.FromArgb("#E5E7EB"),
                BorderWidth = 1
            };

            // イベントはそのまま
            buttonConfig.Clicked += ConfigButtonClicked;
            buttonUserchange.Clicked += UserButtonClicked;
            buttonLogout.Clicked += LogoutButtonClicked;
            buttonback.Clicked += backButtonClicked;

            // モダンなカード風レイアウトを中央に配置
            var menuCard = new Frame
            {
                CornerRadius = 16,
                HasShadow = true,
                Padding = new Thickness(20),
                Margin = new Thickness(20),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#E6EEF8"),
                Content = new VerticalStackLayout
                {
                    Spacing = 12,
                    Children =
                    {
                        new HorizontalStackLayout
                        {
                            Spacing = 0,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
                            Children =
                            {
                                new Label
                                {
                                    Text = "メニュー",
                                    FontSize = 18,
                                    FontAttributes = FontAttributes.Bold,
                                    TextColor = Color.FromArgb("#0F172A"),
                                    HorizontalOptions = LayoutOptions.StartAndExpand,
                                    VerticalOptions = LayoutOptions.Center
                                },
                                // 空白スペースを取りつつ閉じる挙動は既存の戻るボタンで対応しているため省略
                            }
                        },
                        buttonConfig,
                        buttonUserchange,
                        buttonLogout,
                        buttonback
                    }
                }
            };

            // ページ全体の構成
            this.Content = new ScrollView
            {
                BackgroundColor = Colors.White,
                Content = new VerticalStackLayout
                {
                    Padding = new Thickness(20, 24),
                    Spacing = 16,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        // 上部に余白を取り、カードを中央寄せに見せる
                        new VerticalStackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Children =
                            {
                                menuCard
                            }
                        },

                        // フッター：バージョン表示と余白
                        new VerticalStackLayout
                        {
                            Spacing = 4,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.EndAndExpand,
                            Children =
                            {
                                labelVer,
                                new Label
                                {
                                    Text = "Copyright © Five Motion Systems, Inc.",
                                    FontSize = 11,
                                    TextColor = Color.FromArgb("#9CA3AF"),
                                    HorizontalOptions = LayoutOptions.Center
                                }
                            }
                        }
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
                //想定外のケースはメインへ
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
}