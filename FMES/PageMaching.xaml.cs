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

        // ページ背景は変更しない（指定の濃いライトグレー）
        //        this.BackgroundColor = Color.FromArgb("#D1D5DB");
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


        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 2;

        // ↓added for popupmeneu (小さなユーザー表示 + メニューボタン)
        labelUser = new Label
        {
            Text = clsGlobalVar.g_Operator,
            //            BackgroundColor = Color.FromArgb("#D1D5DB"),
            BackgroundColor = Colors.Transparent,          // ← 透過に変更

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
            //            BackgroundColor = Color.FromArgb("#D1D5DB"),
            BackgroundColor = Colors.Transparent,          // ← 透過に変更
            HorizontalOptions = LayoutOptions.End,
            //VerticalOptions = LayoutOptions.center // 中央に配置する（縦方向）
            VerticalOptions = LayoutOptions.Center // 中央に配置する（縦方向）
        };
        buttonMenu.Clicked += MenuButtonClicked;
        ContentMenu = new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            //            BackgroundColor = Color.FromArgb("#D1D5DB"),
            BackgroundColor = Colors.Transparent,          // ← 透過に変更
            Children = {
                        labelUser,
                        buttonMenu,
                    }
        };
        // ↑added for popupmeneu

        // Entry - ビーコン名
        user1 = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Color.FromArgb("#F8FAFC"), // subtle input bg
            TextColor = Color.FromArgb("#0F172A"),
            FontSize = 16,
            Placeholder = "ビーコン名称",
            PlaceholderColor = Color.FromArgb("#94A3B8"),
            HeightRequest = 48,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            //CornerRadius = 10,
            Margin = new Thickness(0)
        };
        // ↓added for QRScan
        if (clsGlobalVar.g_BackPage == "PageMaching" && clsGlobalVar.g_QRRET != null)
        {
            user1.Text = clsGlobalVar.g_QRRET;
            clsGlobalVar.g_BackPage = string.Empty;
            clsGlobalVar.g_QRRET = string.Empty;

        }
        // ↑added for QRScan

        // QRボタン（小型）
        button1 = new Button
        {
            ImageSource = "qr100x100.png",
            WidthRequest = 48,
            HeightRequest = 48,
            CornerRadius = 12,
            BackgroundColor = Colors.White,
            BorderColor = Color.FromArgb("#E6EEF8"),
            BorderWidth = 1,
            Padding = 8,
            HorizontalOptions = LayoutOptions.End
        };
        // Content2 は入力行（Entry + QR）
        Content2 = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Fill,
            Children = { user1, button1 }
        };

        // アクションボタン（主操作）
        button2 = new Button
        {
            Text = "ビーコンマッチ登録",
            FontSize = 14,
            BorderColor = Colors.LightGray,
            BorderWidth = 1.5,
            HeightRequest = 48,
            CornerRadius = 12,
            Margin = new Thickness(20, 0, 20, 12),
            HorizontalOptions = LayoutOptions.Fill
        };
        // プラットフォームに応じたアクセントカラーを利用してグラデーション風に
        button2.Background = new LinearGradientBrush
        {
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0),
            GradientStops = new GradientStopCollection
            {
                new GradientStop { Color = Color.FromArgb("#3B82F6"), Offset = 0.0f },
                new GradientStop { Color = Color.FromArgb("#2563EB"), Offset = 1.0f }
            }
        };

        // 戻るボタン（セカンダリ）
        buttonEnd = new Button
        {
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
        buttonEnd.Clicked += EndButtonClicked;

        // 全体カード — 中央の白いカードにまとめて洗練された見た目に
        var mainCard = new Frame
        {
            CornerRadius = 14,
            HasShadow = true,
            Padding = new Thickness(16),
            Margin = new Thickness(20, 12, 20, 20),
            BackgroundColor = Colors.White,
            BorderColor = Color.FromArgb("#E6EEF8"),
            Content = new VerticalStackLayout
            {
                Spacing = 12,
                Children =
                {
                    // ヘッダー行（タイトル + メニュー）
                    new HorizontalStackLayout
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        Children =
                        {
                            new Label
                            {
                                Text = "ビーコンマッチ",
                                FontSize = 20,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromArgb("#0F172A"),
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.StartAndExpand
                            },
                            ContentMenu
                        }
                    },

                    // 説明（小さめテキスト）
                    new Label
                    {
                        Text = "ビーコン名を入力して登録します。QRで簡単に入力できます。",
                        FontSize = 13,
                        TextColor = Color.FromArgb("#64748B"),
                        HorizontalOptions = LayoutOptions.Fill,
                        LineBreakMode = LineBreakMode.WordWrap
                    },

                    // 入力行
                    Content2,

                    // 主ボタン・戻るボタン
                    new VerticalStackLayout
                    {
                        Spacing = 10,
                        Children =
                        {
                            button2,
                            buttonEnd
                        }
                    }
                }
            }
        };

        // ページ全体のレイアウト（背景色は保持）
        this.Content = new ScrollView
        {
            Padding = new Thickness(0),
            BackgroundColor = Color.FromArgb("#D1D5DB"), // ページ背景は変えない
            Content = new VerticalStackLayout
            {
                Spacing = 0,
                Padding = new Thickness(0, 12),
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Colors.Transparent,
                Children =
                {
                    // 上部に小さな余白を作り、カードを中心に表示
                    mainCard
                }
            }
        };

        // イベントは既存ハンドラへ（元のロジックを維持）
        button1.Clicked += ScanButtonClicked;
        button2.Clicked += GoButtonClicked;
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
    }

    async void GoButtonClicked(object sender, EventArgs s)
    {
        if (string.IsNullOrEmpty(user1.Text) == false)
        {
            string srtErrMsg = string.Empty;
            clsKaisouList lstKaisou = new clsKaisouList();
            if (clsWebUpdate.SendAddBeacon(clsGlobalVar.g_SasizuNo, user1.Text, ref srtErrMsg) == false)
            {
                await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
            }
            else
            {
                await DisplayAlert("ビーコンマッチ登録完了", "正常終了", "OK");
                freeThis();
                clsGlobalVar.g_ActMode = 0;
                Application.Current.MainPage = new Page1();
            }
        }
        else
        {
            // 空白入力時の挙動は既存のまま
        }
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

        button1.Clicked -= ScanButtonClicked;
        button2.Clicked -= GoButtonClicked;
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