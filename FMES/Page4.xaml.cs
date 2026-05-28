using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FMES;

public partial class Page4 : ContentPage
{
    private clsKaisouList lstKaisou;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    //private Button buttonSS;
    private List<Button> Lstbutton = new List<Button>();
    private Picker dropdown1;
    private Entry txtVal1;
    private Button buttonPass;
    private Button buttonUpd;
    private Button buttonUpd5;
    private Button buttonEnd;
    //        private Button buttonOCR;
    private ActivityIndicator actIndOCR;
    private AbsoluteLayout absLay;
    private Image imgView;
    private StackLayout layout1;
    private ScrollView sv;
    //東レ用画面種別20用次へ　前へボタン
    private Button buttonnext;
    private Button buttonprev;
    private Label labelDummy;
    private StackLayout layout20;

    private bool doingNow = false;


    public Page4()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //        AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 7;
        string wwsashizuNo = clsGlobalVar.g_SasizuNo;
        if (clsGlobalVar.g_SasizuNo == "-2")
        {
            wwsashizuNo = "指図番号無し作業";
        }
        else if (clsGlobalVar.g_SasizuNo == "-1")
        {
            wwsashizuNo = "その他";
        }


        string srtErrMsg = string.Empty;
        lstKaisou = new clsKaisouList();
        if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 4, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {
            clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;
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

            //            Padding = new Thickness(0, Device.OnPlatform(10, 0, 0), 0, 0);
            if (lstKaisou._Header._GamenKind == 1)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                buttonUpd,
                                buttonEnd,
                            }
                    };
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                dropdown1 = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM033,
                    Title = "ライン選択",
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsLine wLine in lstKaisou._Header._LineLists)
                {
                    dropdown1.Items.Add(wLine._LineName);
                    if (wLine._index == lstKaisou._Header._SelSelected)
                    {
                        dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                    }
                }

                if (lstKaisou._Header._done == 0)
                {
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                dropdown1,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                dropdown1,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {
                layout1 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(10, 10, 10, 10),
                };

                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                layout1.Children.Add(ContentMenu);
                layout1.Children.Add(label1);
                layout1.Children.Add(label2);
                layout1.Children.Add(label3);
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }

                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
//                    layout1.Children.Add(buttonUpd);
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                layout1.Children.Add(buttonEnd);

                sv = new ScrollView { Content = layout1 };
                Content = sv;
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    //buttonSS.Clicked += SSButtonClicked;
                    buttonPass.Clicked += PassButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        Text = GetUpdButtonStr(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                else
                {
                    buttonUpd5 = new Button
                    {
                        Text = GetUpdButtonStr(lstKaisou._Header._iPass),
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    buttonUpd5.Clicked += UpdButtonClicked5;
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonPass.Clicked += PassButtonClicked2;
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                buttonUpd5,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 6)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.End,
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    dropdown1 = new Picker
                    {
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 26,
                        //Title = AppResources.IDM039,
                        Title = "指数選択",
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                        if (wLine._index == lstKaisou._Header._SelSelected)
                        {
                            dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                        }
                    }
                }
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2;
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                        txtVal1,
                        dropdown1,
                        label6,
                        }
                    };
                }
                else
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                        txtVal1,
                        //dropdown1,
                        label6,
                        }
                    };

                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                                HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(20, 0, 20, 0),
                    //Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    //Placeholder = AppResources.IDM071,
                    Placeholder = "文字列入力",
                    Text = ConvStr2Disp(lstKaisou._Header._strVal),
                };

                if (lstKaisou._Header._done == 0)
                {
                    layout1 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                                txtVal1,
                                //buttonOCR,
                            }
                    };
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    actIndOCR = new ActivityIndicator
                    {
                        //Color = Device.OnPlatform(Colors.Black, Colors.Default, Colors.Default),
                        IsRunning = false, // 回転中
                        VerticalOptions = LayoutOptions.Center // 中央に配置する
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                layout1,
                                actIndOCR,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                txtVal1,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                dropdown1 = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM040,
                    Title = "未選択",
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    dropdown1.Items.Add(wKaisou._kaisouName);
                }
                dropdown1.SelectedIndex = GetCurSelectedDropDown(lstKaisou._Header._strCmb);
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                dropdown1,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                dropdown1,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                absLay = new AbsoluteLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,

                };
                string cacheBuster = $"?t={DateTime.UtcNow.Ticks}";
                var uri = clsGlobalVar.GetCurURL() + "img/instruction/" + lstKaisou._Header._ImageFile;
                Debug.WriteLine(uri);
                Trace.WriteLine(uri);

                imgView = new Image
                {
                    Source = ImageSource.FromUri(new Uri(uri + cacheBuster)),

                    HorizontalOptions = LayoutOptions.Center,

                };
                int z = 0;
                absLay.Children.Add(imgView);
                //imgView.ZIndex = 0;
                z++;
                absLay.SetLayoutFlags(imgView, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
                absLay.SetLayoutBounds(imgView, new Rect(0 / 2, 0 / 2, 1500, 1500));

                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    double baseFontsize = 22;
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = baseFontsize,
                        //WidthRequest = 50,
                        ZIndex = ++z,
                        WidthRequest = baseFontsize * 1.7 * wKaisou._kaisouName.Length,

                        BackgroundColor = GetPassButtonBColor9(wKaisou._during),
                        TextColor = GetPassButtonTColor(wKaisou._iPass),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,

                        //TextColor = GetTextColor(wKaisou),
                        //BackgroundColor = GetBackColor(wKaisou),
                    };
                    butn.Clicked += ItemButtonClicked;
                    Lstbutton.Add(butn);
                    absLay.SetLayoutFlags(butn, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
                    absLay.SetLayoutBounds(butn, new Rect((wKaisou._IconButton.X + 700) / 1772, (wKaisou._IconButton.Y + 500) / 1772, (baseFontsize * 1.7 * wKaisou._kaisouName.Length), 70));
                    //absLay.SetLayoutBounds(butn, new Rect((wKaisou._IconButton.X + 700) / 1772, (wKaisou._IconButton.Y + 500) / 1772, (12* wKaisou._kaisouName.Length), 70));
                    //absLay.SetLayoutBounds(imgView, new Rect(0 / 2, 0 / 2, 1500, 1500));
                    absLay.Children.Add(butn);
                    //butn.ZIndex = ++z ;
                    //butn.TranslateTo(0, 0);
                    //                    butn.Opacity = 0;
                    //                  butn.FadeTo(1, 4000);
                    //butn.RelScaleTo(250);
                    //butn.TranslateTo(0, 0);
                    //butn.TranslateTo(wKaisou._IconButton.X / 2, wKaisou._IconButton.Y / 2);

                    //absLay.Children.Add(butn, new Point(wKaisou._IconButton.X / 2, wKaisou._IconButton.Y / 2));
                }

                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                    ZIndex = ++z,

                };

                layout1 = new StackLayout
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    Children = {
                            ContentMenu,
                            label1,
                            label2,
                            label3,
                            buttonEnd,
                            absLay,
                            //buttonEnd,
                        }
                };
                sv = new ScrollView { Content = layout1 };
                Content = sv;

                //imgView.Clicked += 
                buttonEnd.Clicked += EndButtonClicked;
                //アニメーションを入れてみる
                foreach (Button wbutn in Lstbutton)
                {
                    wbutn.Opacity = 0;
                    wbutn.FadeTo(1, 4000);
                    //wbutn.RelScaleTo(2);
                    //                    wbutn.RelScaleTo(250);
                    //wbutn.RelRotateTo(90);

                    //  string stwork = wbutn.Text;
                    //  wbutn.Text = " " + stwork + "　";
                    //  wbutn.Text = stwork;
                }


            }
            else if (lstKaisou._Header._GamenKind == 13)
            {
                //チェックボタンリスト分
                layout1 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(10, 10, 10, 10),
                };

                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                layout1.Children.Add(ContentMenu);
                layout1.Children.Add(label1);
                layout1.Children.Add(label2);
                layout1.Children.Add(label3);
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor14(wKaisou),
                        BackgroundColor = GetBackColor14(wKaisou),
                    };
                    butn.Clicked += ItemButtonClicked2;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                layout1.Children.Add(buttonEnd);

                sv = new ScrollView { Content = layout1 };
                Content = sv;
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 20)
            {

                labelDummy = new Label
                {
                    Text = "　　",
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    //HorizontalOptions = LayoutOptions.End,
                };

                buttonnext = new Button
                {
                    //Text = "次へ",
                    ImageSource = "next.png",
                    FontSize = 20,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    //HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.End,
                    //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
                    VerticalOptions = LayoutOptions.EndAndExpand // 中央に配置する（縦方向）
                };
                buttonnext.Clicked += MenuButtonNextClicked;


                buttonprev = new Button
                {
                    //Text = "前へ",
                    ImageSource = "prev.png",
                    FontSize = 20,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    BackgroundColor = Colors.White,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    //HorizontalOptions = LayoutOptions.Center//,//中央に配置する（横方向）
                    //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
                    VerticalOptions = LayoutOptions.EndAndExpand // 中央に配置する（縦方向）
                };
                buttonprev.Clicked += MenuButtonPrevClicked;

                layout20 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Colors.White,
                    Children = {
                        buttonprev,
                        labelDummy,
                        buttonnext,
                    }
                };

                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.End,
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    dropdown1 = new Picker
                    {
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        FontSize = 26,
                        //Title = AppResources.IDM039,
                        Title = "指数選択",
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                        if (wLine._index == lstKaisou._Header._SelSelected)
                        {
                            dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                        }
                    }
                }
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2;
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                        txtVal1,
                        dropdown1,
                        label6,
                        }
                    };
                }
                else
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                        txtVal1,
                        //dropdown1,
                        label6,
                        }
                    };

                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 22,
                        Margin = new Thickness(0, 5, 0, 5),
                        Padding = new Thickness(10, 10, 10, 10),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
                buttonEnd = new Button
                {
                    //Text = AppResources.IDM032,
                    Text = "戻る",
                    FontSize = 22,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10, 10, 10, 10),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                layout20,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                        BackgroundColor = Colors.White,
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                layout20,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }

        }
        else
        {
            label1 = new Label
            {
                //Text = "　" + AppResources.IDM027,
                Text = "　" + "データエラー（又は通信エラー）",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Children = {
                    label1,
                    }
            };
        }



    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page4";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    async void ItemButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            int i = 0;
            foreach (Button wBtn in Lstbutton)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    clsGlobalVar.g_KaisouNo = 5;
                    clsGlobalVar.g_KouteiID = lstKaisou._Datas[i]._KouteiID;
                    clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                    clsGlobalVar.g_KensaBashoID = lstKaisou._Datas[i]._KensaBashoID;

                    clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[i]._KensaBashoShousaiID;

                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), _KensaBashoShousaiID.ToString(), GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();
                    freeThis();
                    //await Navigation.PushAsync(new Page4(yourData));
                    Application.Current.MainPage = new Page5();
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }
    async void ItemButtonClicked2(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            int i = 0;
            foreach (Button wBtn in Lstbutton)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    if (lstKaisou._Datas[i]._UnderGamenKind == 14)
                    {
                        if (lstKaisou._Header._done == 0)
                        {
                            if (lstKaisou._Datas[i]._Checked == 0)
                            {
                                lstKaisou._Datas[i]._Checked = 1;
                            }
                            else
                            {
                                lstKaisou._Datas[i]._Checked = 0;
                            }
                            wBtn.BackgroundColor = GetBackColor14(lstKaisou._Datas[i]);
                            wBtn.TextColor = GetTextColor14(lstKaisou._Datas[i]);
                            //                                string wSendPara = "＠" + lstKaisou._Datas[i]._KensaBashoID + "＃" + lstKaisou._Datas[i]._Checked.ToString();
                            string wSendPara = lstKaisou._Datas[i]._KensaBashoID + "＃" + lstKaisou._Datas[i]._Checked.ToString();
                            string strErrMsg = "";
                            bool bRet = clsWebUpdate.SendCheckData(clsGlobalVar.g_UserID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KouteiKekkaID, clsGlobalVar.g_SasizuNo, wSendPara, ref strErrMsg);

                            //bool bRet = clsWebUpdate.SendCheckData(clsGlobalVar.g_UserID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KouteiKekkaID, clsGlobalVar.g_SasizuNo, lstKaisou._Datas[i]._KensaBashoID, lstKaisou._Datas[i]._Checked, ref strErrMsg);
                            if (bRet == false)
                            {
                                //未入力
                                //await Navigation.PopAsync();
                                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                                await DisplayAlert("更新エラー", strErrMsg, "OK");
                            }
                            i++;
                            break;
                        }
                    }
                    else
                    {
                        if (lstKaisou._Datas[i]._parmit == 1)
                        {
                            //実行可能な物
                            clsGlobalVar.g_KaisouNo = 4;
                            clsGlobalVar.g_KouteiID = lstKaisou._Datas[i]._KouteiID;
                            clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                            clsGlobalVar.g_KensaBashoID = lstKaisou._Datas[i]._KensaBashoID;
                            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                            clsGlobalVar.g_LineIndex = GetSelectedLineID();
                            freeThis();
                            //await Navigation.PushAsync(new Page4(yourData));
                            Application.Current.MainPage = new Page4();
                        }
                    }
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }
    async void MenuButtonNextClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");

                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                if (lstKaisou._Header._GamenKind == 5 && lstKaisou._Header._iPass == 0)
                {
                    clsGlobalVar.g_KaisouNo = 5;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), lstKaisou._Datas[0]._KensaBashoShousaiID.ToString(), GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    //サーバー側のポカで　本来在るべき第4階層に2行目のデータ無くておちるケースがあるので、ワーニングを出す事にした 2022/0405
                    //if(lstKaisou._Datas.Count<= 0)
                    //{

                    //}
                    //else { }
                    clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;
                    freeThis();
                    //await Navigation.PushAsync(new Page4(yourData));
                    Application.Current.MainPage = new Page5();
                }
                else
                {
                    clsGlobalVar.g_KaisouNo = 3;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    clsGlobalVar.g_KensaBashoID = 0;
                    clsGlobalVar.g_KensaBashoShousaiID = 0;

                   // clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[1]._KensaBashoShousaiID;//次へ

                    freeThis();
                    //await Navigation.PushAsync(new Page3(yourData));
                    Application.Current.MainPage = new Page3();
                }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void MenuButtonPrevClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                if (lstKaisou._Header._GamenKind == 5 && lstKaisou._Header._iPass == 0)
                {
                    clsGlobalVar.g_KaisouNo = 5;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), lstKaisou._Datas[0]._KensaBashoShousaiID.ToString(), GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    //サーバー側のポカで　本来在るべき第4階層に2行目のデータ無くておちるケースがあるので、ワーニングを出す事にした 2022/0405
                    //if(lstKaisou._Datas.Count<= 0)
                    //{

                    //}
                    //else { }
                    clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;
                    freeThis();
                    //await Navigation.PushAsync(new Page4(yourData));
                    Application.Current.MainPage = new Page5();
                }
                else
                {
                    clsGlobalVar.g_KaisouNo = 3;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    clsGlobalVar.g_KensaBashoID = 0;
                    clsGlobalVar.g_KensaBashoShousaiID = 0;

                    //clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;//前への検査場所

                    freeThis();
                    //await Navigation.PushAsync(new Page3(yourData));
                    Application.Current.MainPage = new Page3();
                }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    async void UpdButtonClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 6 || lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                if (lstKaisou._Header._GamenKind == 5 && lstKaisou._Header._iPass == 0)
                {
                    clsGlobalVar.g_KaisouNo = 5;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), lstKaisou._Datas[0]._KensaBashoShousaiID.ToString(), GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    //サーバー側のポカで　本来在るべき第4階層に2行目のデータ無くておちるケースがあるので、ワーニングを出す事にした 2022/0405
                    //if(lstKaisou._Datas.Count<= 0)
                    //{

                    //}
                    //else { }
                    clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;
                    freeThis();
                    //await Navigation.PushAsync(new Page4(yourData));
                    Application.Current.MainPage = new Page5();
                }
                else
                {
                    clsGlobalVar.g_KaisouNo = 3;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();

                    clsGlobalVar.g_KensaBashoID = 0;
                    clsGlobalVar.g_KensaBashoShousaiID = 0;
                    freeThis();
                    //await Navigation.PushAsync(new Page3(yourData));
                    Application.Current.MainPage = new Page3();
                }
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void UpdButtonClicked5(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            if (lstKaisou._Header._GamenKind == 5)
            {
                clsGlobalVar.g_KaisouNo = 5;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), lstKaisou._Datas[0]._KensaBashoShousaiID.ToString(), GetSelectedLineID().ToString() };
                clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;

                clsGlobalVar.g_LineIndex = GetSelectedLineID();
                freeThis();
                //await Navigation.PushAsync(new Page4(yourData));
                Application.Current.MainPage = new Page5();
            }
            else if (lstKaisou._Header._GamenKind == 10)
            {
                clsGlobalVar.g_KaisouNo = 3;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), lstKaisou._Header._BackID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_LineIndex = GetSelectedLineID();

                clsGlobalVar.g_KensaBashoID = 0;
                clsGlobalVar.g_KensaBashoShousaiID = 0;
                freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                Application.Current.MainPage = new Page3();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            clsGlobalVar.g_KaisouNo = 3;
            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), "0", clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
            clsGlobalVar.g_LineIndex = GetSelectedLineID();

            clsGlobalVar.g_KensaBashoID = 0;
            clsGlobalVar.g_KensaBashoShousaiID = 0;
            freeThis();
            //await Navigation.PushAsync(new Page3(yourData));
            Application.Current.MainPage = new Page3();
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void PassButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._iPass == 1)
            {
                lstKaisou._Header._iPass = 0;
            }
            else if (lstKaisou._Header._iPass == 0)
            {
                lstKaisou._Header._iPass = -1;
            }
            else if (lstKaisou._Header._iPass == -1)
            {
                lstKaisou._Header._iPass = 1;
            }
            buttonPass.Text = GetPassButtonStr(lstKaisou._Header._iPass);
            buttonPass.BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass);
            buttonPass.TextColor = GetPassButtonTColor(lstKaisou._Header._iPass);
            doingNow = false;
        }
    }
    async void PassButtonClicked2(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._iPass == 1)
            {
                lstKaisou._Header._iPass = 0;
            }
            else if (lstKaisou._Header._iPass == 0)
            {
                lstKaisou._Header._iPass = 2;
            }
            else if (lstKaisou._Header._iPass == 2)
            {
                lstKaisou._Header._iPass = -1;
            }
            else if (lstKaisou._Header._iPass == -1)
            {
                lstKaisou._Header._iPass = 1;
            }
            buttonPass.Text = GetPassButtonStr(lstKaisou._Header._iPass);
            buttonPass.BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass);
            buttonPass.TextColor = GetPassButtonTColor(lstKaisou._Header._iPass);
            buttonUpd.Text = GetUpdButtonStr(lstKaisou._Header._iPass);
            doingNow = false;
        }
    }
    private string GetPassButtonStr(int iPass)
    {
        //string strRet = "　" + AppResources.IDM059 + "　";
        string strRet = "　" + "未入力" + "　";
        if (iPass == 1)
        {
            //strRet = "　" + AppResources.IDM060 + "　";
            strRet = "　" + "合格" + "　";
        }
        else if (iPass == 0)
        {
            //strRet = "　" + AppResources.IDM061 + "　";
            strRet = "　" + "不合格" + "　";
        }
        else if (iPass == 2)
        {
            //strRet = "　" + AppResources.IDM062 + "　";
            strRet = "　" + "不要" + "　";
        }
        return strRet;
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

    private Color GetPassButtonBColor9(int iPass)
    {
        Color retCol = Colors.LightGray;
        if (iPass == 1)
        {
            retCol = GetBackColorParts();
        }
        else if (iPass == 0)
        {
            retCol = Colors.White;
        }
        else if (iPass == 4)
        {
            retCol = Colors.Red;
        }
        else if (iPass == 5)
        {
            retCol = Colors.Blue;
        }
        return retCol;
    }





    private Color GetPassButtonBColor(int iPass)
    {
        Color retCol = Colors.LightGray;
        if (iPass == 1)
        {
            retCol = GetBackColorParts();
        }
        else if (iPass == 0)
        {
            retCol = Colors.Red;
        }
        else if (iPass == 2)
        {
            retCol = Colors.LightGray;
        }
        return retCol;
    }
    private Color GetPassButtonTColor(int iPass)
    {
        Color retCol = Colors.Black;
        if (iPass == 1)
        {
            retCol = Colors.White;
        }
        else if (iPass == 0)
        {
            retCol = Colors.White;
        }
        else if (iPass == 2)
        {
            retCol = Colors.Black;
        }
        return retCol;
    }
    private string GetUpdButtonStr(int iPass)
    {
        //string strRet = AppResources.IDM038;
        string strRet = "更新";
        if (iPass == 1)
        {
            //strRet = AppResources.IDM038;
            strRet = "更新";
        }
        else if (iPass == 0)
        {
            //strRet = AppResources.IDM065;
            strRet = "詳細入力";
        }
        return strRet;
    }

    private Color GetBackColor(int index)
    {
        Color wCol = Colors.White;
        if (lstKaisou._Datas[index]._during == 0)
        {
            //進行中
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
    private Color GetBackColor(clsKaisou wKaisou)
    {
        Color wCol = Colors.White;
        if (wKaisou._during == 0)
        {
            //進行中
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

    private Color GetTextColor(int index)
    {
        Color wCol;
        if (lstKaisou._Datas[index]._parmit == 1)
        {
            //権限あり
            wCol = Colors.Black;
        }
        else
        {
            wCol = Colors.LightGray;
        }

        return wCol;
    }
    private Color GetTextColor(clsKaisou wKaisou)
    {
        Color wCol;
        if (wKaisou._parmit == 1)
        {
            //権限あり
            wCol = Colors.Black;
        }
        else
        {
            wCol = Colors.Gray;
        }

        return wCol;
    }
    private string GetKetaStr(int iSei, int iShou)
    {
        string strKetaW = string.Empty;
        for (int i = 0; i < iSei; i++)
        {
            strKetaW += "x";
        }
        if (iSei == 0)
        {
            strKetaW += "x";
        }
        if (iShou > 0)
        {
            strKetaW += ".";
            for (int i = 0; i < iShou; i++)
            {
                strKetaW += "x";
            }
        }
        return strKetaW;
    }
    private bool CheckNumberChar3(string strNo)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strNo, "^[-]?[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*.[0-9]*$") == true || Regex.IsMatch(strNo, "^[-]?[0-9]*.[0-9]*$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }
    private Color GetBackColor14(clsKaisou wKaisou)
    {
        Color wCol = Colors.White;
        if (wKaisou._UnderGamenKind == 14)
        {
            if (wKaisou._Checked == 1)
            {
                wCol = Colors.LightBlue;
            }
            else
            {
                wCol = Colors.LightYellow;
            }
        }
        else
        {
            if (wKaisou._during == 0)
            {
                //進行中
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
        }
        return wCol;
    }
    private Color GetTextColor14(clsKaisou wKaisou)
    {
        Color wCol;
        if (wKaisou._UnderGamenKind == 14)
        {
            wCol = Colors.Black;
        }
        else
        {
            if (wKaisou._parmit == 1)
            {
                //権限あり
                wCol = Colors.Black;
            }
            else
            {
                wCol = Colors.Gray;
            }
        }

        return wCol;
    }

    private string GetFormatedStrByKeta(int iSei, int iShou, decimal dVal)
    {
        string strRet = string.Empty;
        if (dVal == -999999)
        {

        }
        else
        {
            string strF = "F" + iShou.ToString();
            strRet = dVal.ToString(strF);
        }
        return strRet;
    }
    private int GetCurSelectedDropDown(string strVal)
    {
        int iRet = -1;
        if (string.IsNullOrEmpty(strVal) == false)
        {
            string wStr = strVal;
            int iIndex = 0;
            int wKouteiID = -1;
            int wKouteiShousaiID = -1;
            int wKensaBashoID = -1;

            while (wStr.Length > 0)
            {
                int iNo1 = wStr.IndexOf("-");
                string strW2;
                if (iNo1 > -1)
                {
                    strW2 = wStr.Substring(0, iNo1).Trim();
                    wStr = wStr.Substring(iNo1 + 1);
                }
                else
                {
                    strW2 = wStr.Trim();
                    wStr = "";
                }
                if (string.IsNullOrEmpty(strW2) == false)
                {
                    if (iIndex == 0)
                    {
                        wKouteiID = int.Parse(strW2);
                    }
                    else if (iIndex == 1)
                    {
                        wKouteiShousaiID = int.Parse(strW2);
                    }
                    else if (iIndex == 2)
                    {
                        wKensaBashoID = int.Parse(strW2);
                    }
                    else
                    {
                        break;
                    }
                }
                iIndex += 1;
            }
            if (wKouteiID > -1)
            {
                iIndex = 0;
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    if (wKaisou._KouteiID == wKouteiID && wKaisou._KouteiShousaiID == wKouteiShousaiID && wKaisou._KensaBashoID == wKensaBashoID)
                    {
                        iRet = iIndex;
                        break;
                    }
                    iIndex++;
                }
            }
        }
        return iRet;
    }
    private void freeThis()
    {
        Console.WriteLine("Page4 free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        if (Lstbutton != null)
        {
            int imax = Lstbutton.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstbutton[i].Clicked -= ItemButtonClicked;
                Lstbutton[i] = null;
            }
            Lstbutton.Clear();
            Lstbutton = null;
        }
        label1 = null;
        label2 = null;
        label3 = null;
        label4 = null;
        label5 = null;
        label6 = null;
        if (dropdown1 != null)
        {
            dropdown1.Items.Clear();
            dropdown1 = null;
        }
        txtVal1 = null;
        if (buttonPass != null)
        {
            if (lstKaisou._Header._done == 0)
            {
                if (lstKaisou._Header._GamenKind == 5)
                {
                    buttonPass.Clicked -= PassButtonClicked2;
                }
                else
                {
                    buttonPass.Clicked -= PassButtonClicked;
                }
            }
            buttonPass = null;
        }
        if (buttonnext != null)
        {
            buttonnext.Clicked -= MenuButtonNextClicked;
            buttonnext.ImageSource = null;
            buttonnext = null;
        }
        if (buttonprev != null)
        {
            buttonprev.Clicked -= MenuButtonPrevClicked;
            buttonprev.ImageSource = null;
            buttonprev = null;
        }
        labelDummy = null;
        layout20 = null;

        if (actIndOCR != null)
        {
            actIndOCR = null;
        }
        if (buttonUpd != null)
        {
            if (lstKaisou._Header._done == 0)
                buttonUpd.Clicked -= UpdButtonClicked;
            buttonUpd = null;
        }
        if (buttonUpd5 != null)
        {
            if (lstKaisou._Header._done == 0)
                buttonUpd5.Clicked -= UpdButtonClicked5;
            buttonUpd5 = null;
        }
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        if (imgView != null)
        {
            imgView.Source = null;
            imgView = null;
        }
        layout1 = null;
        absLay = null;
        sv = null;
        Content = null;
        if (lstKaisou != null)
        {
            lstKaisou.freeThis();
            lstKaisou = null;
        }
        GC.Collect();
        Console.WriteLine("Page4 free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }
    private bool CheckHankakuChar(string strW)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strW, "^[a-zA-Z0-9!-/:-@[-`{-~]+$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }
    private string ConvStr2Disp(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListFrom = { "＃", "＆", "？", "％", "￥" };
        string[] strsListTo = { "#", "&", "?", "%", "\\" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }
    private string ConvStr2Webserver(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListTo = { "＃", "＆", "？", "％", "￥" };
        string[] strsListFrom = { "#", "&", "?", "%", "\\" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }
    private int GetSelectedLineID()
    {
        int iRet = -1;
        if (lstKaisou._Header._GamenKind == 2 && (dropdown1 != null) && lstKaisou._Header._done == 0)
        {
            iRet = dropdown1.SelectedIndex;
        }
        else
        {
            iRet = clsGlobalVar.g_LineIndex;
        }
        return iRet;
    }

}