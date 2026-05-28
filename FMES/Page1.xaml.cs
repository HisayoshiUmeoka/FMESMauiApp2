using System.Diagnostics;
using System.Globalization;

namespace FMES;

public partial class Page1 : ContentPage
{
    private clsKaisouList lstKaisou;

    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Label label3;
    private List<HorizontalStackLayout> Lstlayout = new List<HorizontalStackLayout>();
    private List<Button> Lstbutton = new List<Button>();
    private List<Label> LstTime = new List<Label>();
    private VerticalStackLayout layout1;
    private Label lbBeacon;
    private Button buttonNext;
    private Button buttonMaching;
    private Button buttonEnd;
    private Button buttonpreview;
    //    private ScrollView sv;

    private bool doingNow = false;

    public Page1()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //        AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 3;
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
        string srtErrMsg = string.Empty;
        lstKaisou = new clsKaisouList();

        if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 1, clsGlobalVar.g_KouteiID, 0, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {
            clsGlobalVar.g_SasizuID = lstKaisou._Header._SashizuID;
            clsGlobalVar.g_KouteiVer = lstKaisou._Header._KouteiVer;
            clsGlobalVar.g_lastSashizuKind = lstKaisou._Header._LotKind;
            label1 = new Label
            {
                Text = "　" + lstKaisou._Header._Title,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            string wsashizuNo = clsGlobalVar.g_SasizuNo;
            if (clsGlobalVar.g_SasizuNo == "-2")
            {
                wsashizuNo = "指図番号無し作業";
            }
            else if (clsGlobalVar.g_SasizuNo == "-1")
            {
                wsashizuNo = "その他";
            }

                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                layout1 = new VerticalStackLayout
            {
                //Orientation = StackOrientation.Vertical,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),

                BackgroundColor = Colors.White,
            };
                //layout1.Children.Add(ContentMenu);
            layout1.Children.Add(label3);

            foreach (clsKaisou wKaisou in lstKaisou._Datas)
            {
                Button butn = new Button
                {
                    //Text = wKaisou._kaisouName,
                    Text = (wKaisou._done == 1) ? wKaisou._kaisouName + "　"+ GetDispTime(wKaisou._TotalTime): wKaisou._kaisouName,
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetTextColor(wKaisou),
                    BackgroundColor = GetBackColor(wKaisou),
                    //WidthRequest=150,

                };
                butn.Clicked += ItemButtonClicked;
            //    //layout1.Children.Add(ContentMenu);
                layout1.Children.Add(butn);
                Lstbutton.Add(butn);
                //LstTime.Add(lbW);
                //Lstlayout.Add(layW);
            }
            buttonNext = new Button
            {
                //Text = AppResources.IDM107,
                Text = "別工程へ",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonNext.Clicked += NextButtonClicked;

            lbBeacon = new Label
            {
                //Text = AppResources.IDM130 + lstKaisou._Header._beconname,
                Text = "beacon：" + lstKaisou._Header._beconname,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            };

            buttonMaching = new Button
            {
                //Text = AppResources.IDM119,
                Text = "ビーコンマッチ登録",
                FontSize = 16,
                //Margin = new Thickness(0, 5, 0, 5),Margin = new Thickness(0, 5, 0, 5),,
                //Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonMaching.Clicked += BeaconUpDateButtonClicked;

            StackLayout layW2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                //Margin = new Thickness(0, 15, 0, 15),
                //Padding = new Thickness(10, 10, 10, 10),

                Children = {
                            buttonMaching,
                            lbBeacon,
                        }
            };
            //Lstlayout.Add(layW2);


            string wEnd;
            if (clsGlobalVar.g_ActMode == 0)
            {
                //layout1.Children.Add(ContentMenu);
                layout1.Children.Add(buttonNext);
                //layout1.Children.Add(buttonMaching);
                layout1.Children.Add(layW2);

                //wEnd = AppResources.IDM026;
                wEnd = "次の指図番号へ";
            }
            else
            {
                //wEnd = AppResources.IDM032;
                wEnd = "戻る";
            }
            buttonEnd = new Button
            {
                Text = wEnd,
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonEnd.Clicked += EndButtonClicked;
            //layout1.Children.Add(ContentMenu);
            layout1.Children.Add(buttonEnd);
            buttonpreview = new Button
            {
                //Text = AppResources.IDM149,
                Text = "プレビュー",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonpreview.Clicked += PreviewButtonClicked;
            if (clsGlobalVar.g_SasizuNo != "-2" && clsGlobalVar.g_SasizuNo != "-1")
            {
                layout1.Children.Add(buttonpreview);
            }

            //sv = new ScrollView { Content = layout1 };
            //Content = sv;
            this.Content = new ScrollView
            {
                Content = layout1,


            };


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
            buttonNext = new Button
            {
                //Text = AppResources.IDM107,
                Text = "別工程へ",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonNext.Clicked += NextButtonClicked;
            layout1.Children.Add(buttonMaching);

            buttonEnd = new Button
            {
                //Text = AppResources.IDM026,
                Text = "次の指図番号へ",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonEnd.Clicked += EndButtonClicked;
            buttonpreview = new Button
            {
                //Text = AppResources.IDM149,
                Text = "プレビュー",
                FontSize = 22,
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(0, 0, 0, 0),
                //VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                //HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonpreview.Clicked += PreviewButtonClicked;


            layout1 = new VerticalStackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Children = {
                                            ContentMenu,
                label1,
                    //buttonMaching,
                    buttonEnd,
                    buttonpreview,
                    }
            };
            this.Content = new ScrollView
            {
                Content = layout1,


            };

        }

    }

    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page1";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    private string GetDispTime(int iTotalSec)
    {
        string strRet = string.Empty;
        int hh = iTotalSec / 3600;
        int mm = (iTotalSec - (hh * 3600)) / 60;
        int ss = (iTotalSec - (hh * 3600) - (mm * 60));
        strRet = string.Format("（{0:D2}:{1:D2}:{2:D2}）", hh, mm, ss);
        return strRet;
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
            wCol = Colors.LightGray;
        }

        return wCol;
    }

    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            Application.Current.MainPage = new SashizuPage();
            doingNow = false;
        }
    }
    async void PreviewButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            clsGlobalVar.g_JumpPage = "Page1";
            clsGlobalVar.g_optionurl = clsGlobalVar.GetCurURL() + "users/tabreport1/" + +clsGlobalVar.g_SasizuID;
            //Application.Current.MainPage = new Pageweboption();
            //Application.Current.MainPage = new Pageweboption();
            Application.Current.MainPage = new webPage2();
            return;

            doingNow = false;
        }
    }

    async void BeaconUpDateButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            Application.Current.MainPage = new PageMaching();
            doingNow = false;
        }
    }
    async void NextButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            //await Navigation.PushAsync(new Page1_5(yourData));
            Application.Current.MainPage = new Page1_5();
            doingNow = false;
        }
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
                    if ((lstKaisou._Datas[i]._during == 1 && lstKaisou._Datas[i]._parmit == 1) || (lstKaisou._Datas[i]._done == 1))
                    {
                        //実行可能な物
                        //string[] yourData = { _UserID.ToString(), cnf._svUrl.ToString(), cnf._language.ToString(), cnf._logWrite.ToString(), cnf._urlMsg.ToString() };
                        clsGlobalVar.g_KouteiID = lstKaisou._Datas[i]._KouteiID;
                        freeThis();
                        //await Navigation.PushAsync(new Page2(yourData));
                        Application.Current.MainPage = new Page2();
                    }
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }






}