using System.Globalization;
using System.Text.RegularExpressions;

namespace FMES;

public partial class PageNo : ContentPage
{
    //private clsKaisouList lstKaisou;
    //private readonly ITesseractApi _tesseract;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Entry txtVal1;
    private Button buttonFin;
    private StackLayout layout1;
    private ScrollView sv;

    private bool doingNow = false;

    public PageNo()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 10;

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
        //lstKaisou = new clsKaisouList();

        //if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, 0, 0, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        //{
        //    clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;

        // StackLayoutで2つの Entryコントロールを並べる
        //    label1 = new Label
        //    {
        //        Text = "　" + lstKaisou._Header._Title,
        //        FontSize = 22,
        //        VerticalOptions = LayoutOptions.Center,
        //                    HorizontalOptions = LayoutOptions.Fill,
        //    };
        //    label2 = new Label
        //    {
        //        Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
        //        FontSize = 16,
        //        VerticalOptions = LayoutOptions.Center,
        //                    HorizontalOptions = LayoutOptions.Fill,
        //    };
        //    label3 = new Label
        //    {
        //        Text = "　　" + AppResources.IDM030 + "：" + clsGlobalVar.g_SasizuNo,
        //        FontSize = 16,
        //        VerticalOptions = LayoutOptions.Center,
        //                    HorizontalOptions = LayoutOptions.Fill,
        //    };
        txtVal1 = new Entry
        {
            Keyboard = Keyboard.Text,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontSize = 26,
            //HorizontalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
            HorizontalTextAlignment = TextAlignment.End,
            Placeholder = "20",
            Text = "20",
        };
        label4 = new Label
        {
            Text = "台／20台",
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Fill,
            HorizontalTextAlignment = TextAlignment.Center,
        };

        layout1 = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            Children = {
                                txtVal1,
                                label4,
                            }
        };


        buttonFin = new Button
        {
            Text = "ＯＫ",
            FontSize = 22,
            Margin = new Thickness(15, 15, 15, 15),
            Padding = new Thickness(0, 0, 0, 0),
            //VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = Colors.Black,
            BackgroundColor = Colors.LightGreen,
        };
        buttonFin.Clicked += FinButtonClicked;
        Content = new StackLayout
        {
            Padding = new Thickness(10, 10, 10, 10),
            BackgroundColor = Colors.White,
            Children = {
                        ContentMenu,
                        //label1,
                        //label2,
                        //label3,
                        layout1,
                        buttonFin,
                    }
        };
        //}
        //else
        //{
        //    label1 = new Label
        //    {
        //        Text = "　" + AppResources.IDM027,
        //        FontSize = 22,
        //        VerticalOptions = LayoutOptions.Center,
        //                    HorizontalOptions = LayoutOptions.Fill,
        //    };
        //    Content = new StackLayout
        //    {
        //        Padding = new Thickness(10, 10, 10, 10),
        //        Children = {
        //        label1,
        //        }
        //    };
        //}

    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "PageNo";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu

    async void FinButtonClicked(object sender, EventArgs s)
    {
        //工程終了
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            int iPara = 0;
            int iMax = 20;
            if (string.IsNullOrEmpty(txtVal1.Text) == true)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                //txtVal1.Text = ;
                doingNow = false;
                wBtn.IsEnabled = true;
                return;
            }
            else if (CheckHankakuChar(txtVal1.Text) == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                //txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                doingNow = false;
                wBtn.IsEnabled = true;
                return;
            }
            else
            {
                try
                {
                    iPara = int.Parse(txtVal1.Text);
                }
                catch (Exception)
                {
                    //throw;
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                    await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                    //txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
            }
            if (iPara == 0 || iPara > iMax)
            {
                //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                //txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                doingNow = false;
                wBtn.IsEnabled = true;
                return;
            }

            doingNow = true;
            clsGlobalVar.g_KaisouNo = 1;
            freeThis();
            Application.Current.MainPage = new Page1();
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    private async void freeThis()
    {
        Console.WriteLine("PageNo free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());

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

        label1 = null;
        label2 = null;
        label3 = null;
        label4 = null;
        if (buttonFin != null)
        {
            //if (lstKaisou._Header._done == 0)
            //  buttonFin.Clicked -= FinButtonClicked;
            buttonFin = null;
        }

        txtVal1 = null;
        layout1 = null;
        sv = null;
        Content = null;
        //            if (lstKaisou != null)
        //            {
        //                lstKaisou.freeThis();
        //                lstKaisou = null;
        //            }
        GC.Collect();
        Console.WriteLine("PageNo free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }

    private bool CheckHankakuChar(string strW)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strW, "[0-9]") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }

}