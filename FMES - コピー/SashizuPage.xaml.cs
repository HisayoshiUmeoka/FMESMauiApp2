using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMES
{
    public partial class SashizuPage : ContentPage
    {
        private Label labelUser;
        private Button buttonMenu;
        private HorizontalStackLayout ContentMenu;

        private Label label1;
        private Entry user1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private HorizontalStackLayout Content2;
        private Picker dropdown2;

        private List<clsSashizudat> lstRec;
        private clsoptionList lstoption = new clsoptionList();

        private List<Button> Lstbutton = new List<Button>();
        private ScrollView sv;
        private VerticalStackLayout layout3;
        private bool doingNow = false;

        public SashizuPage()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);

            this.Padding = new Thickness(0);
            clsGlobalVar.g_NowForm = 2;

            // 背景を白色に変更
            this.BackgroundColor = Colors.White;

            // ヘッダー - ユーザー名とメニューボタン
            labelUser = new Label
            {
                Text = clsGlobalVar.g_Operator,
                TextColor = Color.FromArgb("#1E293B"),
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            buttonMenu = new Button
            {
                ImageSource = "icon80x80.png",
                WidthRequest = 40,
                HeightRequest = 40,
                CornerRadius = 20,
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#CBD5E1"),
                BorderWidth = 1,
                Padding = 4,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            buttonMenu.Clicked += MenuButtonClicked;

            ContentMenu = new HorizontalStackLayout
            {
                Spacing = 12,
                Padding = new Thickness(20, 16, 20, 16),
                BackgroundColor = Colors.White,
                Children = { labelUser, buttonMenu }
            };

            // オプションリスト取得
            string srtErrMsg = string.Empty;
            lstoption = new clsoptionList();
            if (lstoption.GetList(ref srtErrMsg) == true)
            {
                // 成功
            }

            // メインレイアウト
            layout3 = new VerticalStackLayout
            {
                Spacing = 0,
                BackgroundColor = Colors.White,
                Children = { ContentMenu }
            };

            // 指図番号入力セクション
            if (clsGlobalVar.g_SashizuMode == 1)
            {
                // QRモード
                var inputLabel = new Label
                {
                    Text = "指図番号",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#334155"),
                    Margin = new Thickness(0, 0, 0, 8)
                };

                user1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Color.FromArgb("#F8FAFC"),
                    TextColor = Color.FromArgb("#1E293B"),
                    FontSize = 15,
                    Placeholder = "指図番号を入力またはスキャン",
                    PlaceholderColor = Color.FromArgb("#94A3B8"),
                    HeightRequest = 48,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                // QR戻り値の復元（既存ロジック維持）
                if (clsGlobalVar.g_BackPage == "SashizuPage" && clsGlobalVar.g_QRRET != null)
                {
                    user1.Text = clsGlobalVar.g_QRRET;
                    clsGlobalVar.g_BackPage = string.Empty;
                    clsGlobalVar.g_QRRET = string.Empty;
                }

                button1 = new Button
                {
                    ImageSource = "Qr100x100.png",
                    WidthRequest = 48,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    BackgroundColor = Colors.White,
                    BorderColor = Color.FromArgb("#CBD5E1"),
                    BorderWidth = 1,
                    Padding = 8
                };
                button1.Clicked += ScanButtonClicked;

                Content2 = new HorizontalStackLayout
                {
                    Spacing = 10,
                    Children = { user1, button1 }
                };

                var inputCard = new Frame
                {
                    CornerRadius = 14,
                    HasShadow = true,
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(20, 16),
                    Margin = new Thickness(20, 16, 20, 16),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 8,
                        Children = { inputLabel, Content2 }
                    }
                };

                layout3.Children.Add(inputCard);
            }
            else
            {
                // セレクトモード
                lstRec = clsWebUpdate.GetListCommand(clsGlobalVar.g_UserID);
                if (lstRec != null)
                {
                    var inputLabel = new Label
                    {
                        Text = "指図番号",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromArgb("#334155"),
                        Margin = new Thickness(0, 0, 0, 8)
                    };

                    dropdown2 = new Picker
                    {
                        FontSize = 15,
                        BackgroundColor = Color.FromArgb("#F8FAFC"),
                        TextColor = Color.FromArgb("#1E293B"),
                        Title = "指図番号を選択してください",
                        HeightRequest = 48
                    };

                    foreach (clsSashizudat wwdat in lstRec)
                    {
                        dropdown2.Items.Add(wwdat._SashizuName);
                    }

                    var inputCard = new Frame
                    {
                        CornerRadius = 14,
                        HasShadow = true,
                        BackgroundColor = Colors.White,
                        Padding = new Thickness(20, 16),
                        Margin = new Thickness(20, 16, 20, 16),
                        Content = new VerticalStackLayout
                        {
                            Spacing = 8,
                            Children = { inputLabel, dropdown2 }
                        }
                    };

                    layout3.Children.Add(inputCard);
                }
            }

            // アクションボタンセクション
            var actionLabel = new Label
            {
                Text = "作業モード選択",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(20, 8, 20, 12)
            };
            layout3.Children.Add(actionLabel);

            // 通常作業ボタン（プライマリー）
            button2 = new Button
            {
                Text = "通常作業",
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                HeightRequest = 50,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12)
            };
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
            button2.Clicked += GoButtonClicked;
            layout3.Children.Add(button2);

            // 指図番号なし作業ボタン（セカンダリー）
            button5 = new Button
            {
                Text = "指図番号なし作業",
                FontSize = 14,
                TextColor = Color.FromArgb("#3B82F6"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#3B82F6"),
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12)
            };
            button5.Clicked += GoButtonClicked3;
            layout3.Children.Add(button5);

            // その他ボタン（セカンダリー）
            button3 = new Button
            {
                Text = "その他",
                FontSize = 14,
                TextColor = Color.FromArgb("#3B82F6"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#3B82F6"),
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12)
            };
            button3.Clicked += GoButtonClicked2;
            layout3.Children.Add(button3);

            // 作業確認ボタン（アウトライン）
            button4 = new Button
            {
                Text = "作業確認",
                FontSize = 14,
                TextColor = Color.FromArgb("#64748B"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#CBD5E1"),
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 16)
            };
            button4.Clicked += GoWebClicked;
            layout3.Children.Add(button4);

            // オプションボタン動的生成
            if (lstoption._Datas.Count > 0)
            {
                var optionLabel = new Label
                {
                    Text = "その他のオプション",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#334155"),
                    Margin = new Thickness(20, 8, 20, 12)
                };
                layout3.Children.Add(optionLabel);

                foreach (clsoptionData woption in lstoption._Datas)
                {
                    Button butn = new Button
                    {
                        Text = woption._optionName,
                        FontSize = 14,
                        TextColor = Color.FromArgb("#10B981"),
                        BackgroundColor = Colors.White,
                        BorderColor = Color.FromArgb("#10B981"),
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12)
                    };
                    butn.Clicked += ItemButtonClicked;
                    layout3.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
            }

            // 非表示フィールド（既存コードとの互換性維持）
            label1 = new Label { IsVisible = false };

            this.Content = new ScrollView
            {
                Padding = new Thickness(0),
                BackgroundColor = Colors.White,
                Content = layout3
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
            wCol = Colors.Blue;
#else
            wCol = Colors.DodgerBlue;
#endif
            return wCol;
        }

        private Color GetTextColorParts()
        {
            Color wCol = Colors.White;
#if IOS
            wCol = Colors.White;
#else
            wCol = Colors.Black;
#endif
            return wCol;
        }

        async void MenuButtonClicked(object sender, EventArgs s)
        {
            clsGlobalVar.g_BackPage = "SashizuPage";
            Application.Current.MainPage = new Pagepopupmenu();
        }

        async void GoButtonClicked(object sender, EventArgs s)
        {
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
                    await DisplayAlert("指図番号エラー", "指図番号の選択欄は入力してください", "OK");
                    return;
                }
            }

            if (string.IsNullOrEmpty(clsGlobalVar.g_SasizuNo) == false)
            {
                string srtErrMsg = string.Empty;
                clsKaisouList lstKaisou = new clsKaisouList();
                if (clsWebUpdate.SendCheckSashizu(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == false)
                {
                    await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
                }
                else
                {
                    freeThis();
                    clsGlobalVar.g_ActMode = 0;
                    Application.Current.MainPage = new Page1();
                }
            }
            else
            {
                await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
            }
        }

        async void GoButtonClicked2(object sender, EventArgs s)
        {
            clsGlobalVar.g_ActMode = -2;
            clsGlobalVar.g_SasizuNo = "-1";

            if (string.IsNullOrEmpty(clsGlobalVar.g_SasizuNo) == false)
            {
                string srtErrMsg = string.Empty;
                clsKaisouList lstKaisou = new clsKaisouList();
                if (clsWebUpdate.SendCheckSashizu(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == false)
                {
                    await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
                }
                else
                {
                    freeThis();
                    Application.Current.MainPage = new Page1();
                }
            }
            else
            {
                await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
            }
        }

        async void GoButtonClicked3(object sender, EventArgs s)
        {
            clsGlobalVar.g_SasizuNo = "-2";
            clsGlobalVar.g_ActMode = -2;

            if (string.IsNullOrEmpty(clsGlobalVar.g_SasizuNo) == false)
            {
                string srtErrMsg = string.Empty;
                clsKaisouList lstKaisou = new clsKaisouList();
                if (clsWebUpdate.SendCheckSashizu(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == false)
                {
                    await DisplayAlert("指図番号エラー", srtErrMsg, "OK");
                }
                else
                {
                    freeThis();
                    Application.Current.MainPage = new Page1();
                }
            }
            else
            {
                await DisplayAlert("指図番号エラー", "指図番号をスキャンしてください。", "OK");
            }
        }

        async void GoWebClicked(object sender, EventArgs s)
        {
            clsGlobalVar.g_SasizuNo = "-1";
            freeThis();
            clsGlobalVar.g_JumpPage = "SashizuPage";
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
            clsGlobalVar.g_BackPage = "SashizuPage";
            clsGlobalVar.g_QRRET = string.Empty;
            Application.Current.MainPage = new QRPage();
        }
    }
}

