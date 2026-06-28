using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMES
{
    public partial class SashizuAdd : ContentPage
    {
        private clsKaisouList lstKaisou;

        private Label labelUser;
        private Button buttonMenu;
        private HorizontalStackLayout ContentMenu;

        private Label label1;
        private Label label2;
        private Label label3;
        private List<Label> LstlabSashizu = new List<Label>();
        private List<Button> LstbuttonDel = new List<Button>();
        private List<StackLayout> LstLayout = new List<StackLayout>();
        private Picker dropdown1;
        private Button buttonQR;
        private Button buttonSashizuGr;
        private Button buttonEnd;
        private StackLayout layout1;
        private ScrollView sv;

        private bool doingNow = false;

        public int _TotalTime { get; set; } = 0;
        public int _StartStop { get; set; } = 0;
        public bool _TimerStoped { get; set; } = false;

        public SashizuAdd()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);

            this.Padding = new Thickness(0);
            clsGlobalVar.g_NowForm = 9;
            
            string wwsashizuNo = clsGlobalVar.g_SasizuNo;
            if (clsGlobalVar.g_SasizuNo == "-2")
            {
                wwsashizuNo = "指図番号なし作業";
            }
            else if (clsGlobalVar.g_SasizuNo == "-1")
            {
                wwsashizuNo = "その他";
            }

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

            string srtErrMsg = string.Empty;
            lstKaisou = new clsKaisouList();
            if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
            {
                _TotalTime = lstKaisou._Header._TotalSec;
                _StartStop = lstKaisou._Header._StopWatch;
                _TimerStoped = (_StartStop == 1) ? false : true;
                clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;

                layout1 = new StackLayout
                {
                    Spacing = 0,
                    BackgroundColor = Colors.White,
                    Children = { ContentMenu }
                };

                // ヘッダー情報カード
                label1 = new Label
                {
                    Text = lstKaisou._Header._Title,
                    TextColor = Color.FromArgb("#1E293B"),
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 0, 0, 8)
                };

                label2 = new Label
                {
                    Text = "品名: " + lstKaisou._Header._ProductName,
                    TextColor = Color.FromArgb("#475569"),
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 4)
                };

                label3 = new Label
                {
                    Text = "指図番号: " + wwsashizuNo,
                    TextColor = Color.FromArgb("#475569"),
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 0)
                };

                var headerCard = new Frame
                {
                    CornerRadius = 14,
                    HasShadow = true,
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(20, 16),
                    Margin = new Thickness(20, 16, 20, 16),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 4,
                        Children = { label1, label2, label3 }
                    }
                };

                layout1.Children.Add(headerCard);

                // 選択済み指図番号リスト
                var selectedListLayout = new VerticalStackLayout
                {
                    Spacing = 8
                };

                var selectedLabel = new Label
                {
                    Text = "選択済み指図番号",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#334155"),
                    Margin = new Thickness(0, 0, 0, 8)
                };
                selectedListLayout.Children.Add(selectedLabel);

                foreach (clsLine wLine in lstKaisou._Header._SelLists)
                {
                    if (wLine._index == 1)
                    {
                        Label labelW = new Label
                        {
                            Text = wLine._LineName,
                            FontSize = 15,
                            TextColor = Color.FromArgb("#1E293B"),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        };
                        LstlabSashizu.Add(labelW);

                        Button butn = new Button
                        {
                            ImageSource = "batsu.png",
                            WidthRequest = 32,
                            HeightRequest = 32,
                            CornerRadius = 16,
                            BackgroundColor = Color.FromArgb("#FEE2E2"),
                            BorderColor = Color.FromArgb("#DC2626"),
                            BorderWidth = 1,
                            Padding = 4,
                            HorizontalOptions = LayoutOptions.End
                        };
                        butn.Clicked += ItemButtonDelClicked;
                        LstbuttonDel.Add(butn);

                        StackLayout Content2 = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 12,
                            Padding = new Thickness(12, 8),
                            BackgroundColor = Color.FromArgb("#F8FAFC"),
                            Children = { labelW, butn }
                        };

                        var itemFrame = new Frame
                        {
                            CornerRadius = 8,
                            HasShadow = false,
                            BackgroundColor = Color.FromArgb("#F8FAFC"),
                            Padding = 0,
                            Margin = new Thickness(0, 0, 0, 8),
                            Content = Content2
                        };

                        LstLayout.Add(Content2);
                        selectedListLayout.Children.Add(itemFrame);
                    }
                }

                if (LstlabSashizu.Count > 0)
                {
                    var selectedCard = new Frame
                    {
                        CornerRadius = 14,
                        HasShadow = true,
                        BackgroundColor = Colors.White,
                        Padding = new Thickness(20, 16),
                        Margin = new Thickness(20, 0, 20, 16),
                        Content = selectedListLayout
                    };
                    layout1.Children.Add(selectedCard);
                }

                // 追加セクション
                var addLabel = new Label
                {
                    Text = "指図番号を追加",
                    FontSize = 16,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#334155"),
                    Margin = new Thickness(0, 0, 0, 8)
                };

                dropdown1 = new Picker
                {
                    BackgroundColor = Color.FromArgb("#F8FAFC"),
                    TextColor = Color.FromArgb("#1E293B"),
                    FontSize = 15,
                    Title = "指図番号選択",
                    HeightRequest = 48,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                foreach (clsLine wLine in lstKaisou._Header._SelLists)
                {
                    if (wLine._index == 0)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                    }
                }

                // QR戻り値の復元（既存ロジック維持）
                if (clsGlobalVar.g_BackPage == "SashizuAdd" && clsGlobalVar.g_QRRET != null)
                {
                    int iMax = dropdown1.Items.Count;
                    for (int i = 0; i < iMax; i++)
                    {
                        if (dropdown1.Items[i].ToString() == clsGlobalVar.g_QRRET)
                        {
                            dropdown1.SelectedIndex = i;
                            break;
                        }
                    }
                    clsGlobalVar.g_BackPage = string.Empty;
                    clsGlobalVar.g_QRRET = string.Empty;
                }

                buttonQR = new Button
                {
                    ImageSource = "qr100x100.png",
                    WidthRequest = 48,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    BackgroundColor = Colors.White,
                    BorderColor = Color.FromArgb("#CBD5E1"),
                    BorderWidth = 1,
                    Padding = 8
                };
                buttonQR.Clicked += ScanButtonClicked;

                var pickerRow = new HorizontalStackLayout
                {
                    Spacing = 10,
                    Margin = new Thickness(0, 0, 0, 12),
                    Children = { dropdown1, buttonQR }
                };

                buttonSashizuGr = new Button
                {
                    Text = "追加",
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    HorizontalOptions = LayoutOptions.Fill
                };
                buttonSashizuGr.Background = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop { Color = Color.FromArgb("#10B981"), Offset = 0.0f },
                        new GradientStop { Color = Color.FromArgb("#059669"), Offset = 1.0f }
                    }
                };
                buttonSashizuGr.Clicked += AddButtonClicked;

                var addCard = new Frame
                {
                    CornerRadius = 14,
                    HasShadow = true,
                    BackgroundColor = Colors.White,
                    Padding = new Thickness(20, 16),
                    Margin = new Thickness(20, 0, 20, 16),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 8,
                        Children = { addLabel, pickerRow, buttonSashizuGr }
                    }
                };

                layout1.Children.Add(addCard);

                // 戻るボタン
                buttonEnd = new Button
                {
                    Text = "戻る",
                    FontSize = 14,
                    TextColor = Color.FromArgb("#64748B"),
                    BackgroundColor = Color.FromArgb("#F1F5F9"),
                    HeightRequest = 48,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 8, 20, 20),
                    HorizontalOptions = LayoutOptions.Fill
                };
                buttonEnd.Clicked += EndButtonClicked;
                layout1.Children.Add(buttonEnd);

                sv = new ScrollView 
                { 
                    Content = layout1,
                    Padding = new Thickness(0),
                    BackgroundColor = Colors.White
                };
                Content = sv;
            }
        }

        private Color GetBackColor(int index)
        {
            Color wCol = Colors.White;
            if (lstKaisou._Datas[index]._during == 0)
            {
                wCol = Colors.White;
            }
            else if (lstKaisou._Datas[index]._during == 1)
            {
                wCol = Colors.LightGreen;
            }
            else if (lstKaisou._Datas[index]._during == 2)
            {
                wCol = Colors.Gray;
            }
            else if (lstKaisou._Datas[index]._during == 3)
            {
                wCol = Colors.DarkGreen;
            }
            else if (lstKaisou._Datas[index]._during == 4)
            {
                wCol = Colors.Red;
            }
            else if (lstKaisou._Datas[index]._during == 5)
            {
                wCol = GetBackColorParts();
            }

            return wCol;
        }

        private Color GetTextColor(int index)
        {
            Color wCol;
            if (lstKaisou._Datas[index]._parmit == 1)
            {
                wCol = Colors.Black;
            }
            else
            {
                wCol = Colors.LightGray;
            }

            return wCol;
        }

        private Color GetBackColor(clsKaisou wKaisou)
        {
            Color wCol = Colors.White;
            if (wKaisou._during == 0)
            {
                wCol = Colors.White;
            }
            else if (wKaisou._during == 1)
            {
                wCol = Colors.LightGreen;
            }
            else if (wKaisou._during == 2)
            {
                wCol = Colors.Gray;
            }
            else if (wKaisou._during == 3)
            {
                wCol = Colors.DarkGreen;
            }
            else if (wKaisou._during == 4)
            {
                wCol = Colors.Red;
            }
            else if (wKaisou._during == 5)
            {
                wCol = GetBackColorParts();
            }
            return wCol;
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

        private Color GetTextColor(clsKaisou wKaisou)
        {
            Color wCol;
            if (wKaisou._parmit == 1)
            {
                wCol = Colors.Black;
            }
            else
            {
                wCol = Colors.Gray;
            }

            return wCol;
        }

        async void MenuButtonClicked(object sender, EventArgs s)
        {
            clsGlobalVar.g_BackPage = "SashizuAdd";
            freeThis();
            Application.Current.MainPage = new Pagepopupmenu();
        }

        async void ScanButtonClicked(object sender, EventArgs s)
        {
            clsGlobalVar.g_BackPage = "SashizuAdd";
            clsGlobalVar.g_QRRET = string.Empty;
            Application.Current.MainPage = new QRPage();
        }

        async void ItemButtonDelClicked(object sender, EventArgs s)
        {
            Button wBtn2 = (Button)sender;
            wBtn2.IsEnabled = false;
            if (doingNow == false)
            {
                doingNow = true;
                int i = 0;
                foreach (Button wBtn in LstbuttonDel)
                {
                    if (wBtn.GetHashCode() == sender.GetHashCode())
                    {
                        string wSashizuNo = clsGlobalVar.g_SasizuNo;
                        string wSelectedSasizuNo = LstlabSashizu[i].Text.Trim();
                        string strErrMsg = "";
                        bool bRet = clsWebUpdate.SendAddDelSashizu(wSashizuNo, wSelectedSasizuNo, ref strErrMsg);
                        if (bRet == false)
                        {
                            await DisplayAlert("指図番号追加・削除エラー", strErrMsg, "OK");
                        }
                        else
                        {
                            ReInit();
                        }
                        break;
                    }
                    i++;
                }
                doingNow = false;
            }
            wBtn2.IsEnabled = true;
        }

        private void freeThis()
        {
            Console.WriteLine("SashizuAddPage free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
            if (buttonMenu != null)
            {
                buttonMenu.Clicked -= MenuButtonClicked;
                buttonMenu.ImageSource = null;
                buttonMenu = null;
            }
            if (labelUser != null) labelUser = null;
            if (ContentMenu != null) ContentMenu = null;

            if (LstlabSashizu != null)
            {
                int imax = LstlabSashizu.Count;
                for (int i = 0; i < imax; i++)
                {
                    LstlabSashizu[i] = null;
                }
                LstlabSashizu.Clear();
                LstlabSashizu = null;
            }
            if (LstbuttonDel != null)
            {
                int imax = LstbuttonDel.Count;
                for (int i = 0; i < imax; i++)
                {
                    LstbuttonDel[i].ImageSource = null;
                    LstbuttonDel[i].Clicked -= ItemButtonDelClicked;
                    LstbuttonDel[i] = null;
                }
                LstbuttonDel.Clear();
                LstbuttonDel = null;
            }
            if (LstLayout != null)
            {
                int imax = LstLayout.Count;
                for (int i = 0; i < imax; i++)
                {
                    LstLayout[i] = null;
                }
                LstLayout.Clear();
                LstLayout = null;
            }
            label1 = null;
            label2 = null;
            label3 = null;
            if (dropdown1 != null)
            {
                dropdown1.Items.Clear();
                dropdown1 = null;
            }
            if (buttonEnd != null)
            {
                buttonEnd.Clicked -= EndButtonClicked;
                buttonEnd = null;
            }
            layout1 = null;
            sv = null;
            Content = null;
            if (lstKaisou != null)
            {
                lstKaisou.freeThis();
                lstKaisou = null;
            }
            GC.Collect();
            Console.WriteLine("SashizuAddPage free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
        }

        async void EndButtonClicked(object sender, EventArgs s)
        {
            Button wBtn = (Button)sender;
            wBtn.IsEnabled = false;
            if (doingNow == false)
            {
                doingNow = true;
                clsGlobalVar.g_KaisouNo = 2;
                freeThis();
                Application.Current.MainPage = new Page2();
                doingNow = false;
            }
            wBtn.IsEnabled = true;
        }

        private void ReInit()
        {
            clsGlobalVar.g_KaisouNo = 2;
            freeThis();
            Application.Current.MainPage = new SashizuAdd();
        }

        async void AddButtonClicked(object sender, EventArgs s)
        {
            Button wBtn = (Button)sender;
            wBtn.IsEnabled = false;
            if (doingNow == false)
            {
                if (dropdown1.SelectedIndex > -1)
                {
                    doingNow = true;
                    string wSashizuNo = clsGlobalVar.g_SasizuNo;
                    string wSelectedSasizuNo = dropdown1.SelectedItem.ToString();
                    string strErrMsg = "";
                    bool bRet = clsWebUpdate.SendAddDelSashizu(wSashizuNo, wSelectedSasizuNo, ref strErrMsg);
                    if (bRet == false)
                    {
                        await DisplayAlert("指図番号追加・削除エラー", strErrMsg, "OK");
                    }
                    else
                    {
                        ReInit();
                    }
                    doingNow = false;
                }
                else
                {
                    await DisplayAlert("指図番号追加・削除エラー", "追加する指図番号を選択してください。", "OK");
                }
            }
            wBtn.IsEnabled = true;
        }
    }
}