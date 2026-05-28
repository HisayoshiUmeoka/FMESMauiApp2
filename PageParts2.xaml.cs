namespace FMES;

public partial class PageParts2 : ContentPage
{
    //private ScrollView sv;
    private clsPartsList lstParts;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private List<StackLayout> Lstlayout = new List<StackLayout>();
    private List<Label> LstName = new List<Label>();
    //        private List<Picker> LstPlace = new List<Picker>();
    private List<Entry> LstVal = new List<Entry>();
    //private List<Label> LstUnit = new List<Label>();
    private Button buttonUpd;
    private Button buttonEnd;
    private StackLayout layout1;
    private ScrollView sv;


    private bool doingNow = false;
    private bool sleepStarted = false;

    public int _TotalTime { get; set; } = 0;
    public int _StartStop { get; set; } = 0;
    public bool _TimerStoped { get; set; } = false;
    //public int _LineIndex { get; set; } = -1;


    public PageParts2()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        this.BackgroundColor = Colors.White;

        //AppResources.Culture = new System.Globalization.CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 5;


        string srtErrMsg = string.Empty;
        lstParts = new clsPartsList();
        //clsGlobalVar.g_KouteiID = 1;    // テスト用

        if (lstParts.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 2, clsGlobalVar.g_KouteiID, 0, 0, clsGlobalVar.g_hinnmokuID, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {
            Label wlabel3 = new Label
            {
                Text = "test",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };

            layout1 = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Orientation = StackOrientation.Vertical,
            };
            //                layout1.Children.Add(wlabel3);
            //               //sv = new ScrollView { Content = layout1 };
            //                Content = layout1;
            //return;


            foreach (clsPartsData wParts in lstParts._Datas)
            {


                //Lstlayout.Add(wlayout);

                Label wlabel1 = new Label
                {
                    Text = wParts._PartsName,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                LstName.Add(wlabel1);
                //wlayout.Children.Add(wlabel1);
                Picker wplace = new Picker
                {
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    Title = "－－－",
                    VerticalOptions = LayoutOptions.Start
                };
                //                    LstPlace.Add(wplace);
                //                    //wlayout.Children.Add(wplace);
                //
                //                    foreach (clsLine wLine in wParts._PlaceLists)
                //                    {
                //                        wplace.Items.Add(wLine._LineName);
                //
                //                  }
                //                    //wlayout.Children.Add(wplace);


                Entry wtxtVal = new Entry
                {
                    Keyboard = Keyboard.Text,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.End,
                    //Placeholder = (wParts._ValType == 0)? "整数を入力" : "小数を入力",
                    Text = (wParts._nullstring == true) ? "" : (wParts._ValType == 0) ? wParts._iVal.ToString() : wParts._dVal.ToString(),
                };
                LstVal.Add(wtxtVal);
                //wlayout.Children.Add(wtxtVal);

                //                    Label wUnit = new Label
                //                  {
                //                        Text = wParts._strunit,
                //                        BackgroundColor = Colors.White,
                //                        TextColor = Colors.Black,
                //                        FontSize = 22,
                //                        VerticalOptions = LayoutOptions.Center,
                //                                    HorizontalOptions = LayoutOptions.Fill,
                //                  };
                //                    LstUnit.Add(wUnit);
                //                    //wlayout.Children.Add(wUnit);


                StackLayout wlayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Colors.White,
                    Children ={
                            wlabel1,
                            //wplace,
                            wtxtVal,
                            //wUnit,
                        },
                };

                Lstlayout.Add(wlayout);
                layout1.Children.Add(wlayout);


            }
            buttonUpd = new Button
            {
                //Text = AppResources.IDM038,
                Text = "更新",
                FontSize = 22,
                //VerticalOptions = LayoutOptions.Center,
                //            HorizontalOptions = LayoutOptions.Fill,
                            HorizontalOptions = LayoutOptions.Fill,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
            buttonUpd.Clicked += UpdButtonClicked;
            layout1.Children.Add(buttonUpd);

            buttonEnd = new Button
            {
                //Text = AppResources.IDM032,
                Text = "戻る",
                FontSize = 22,
                //VerticalOptions = LayoutOptions.Center,
                //            HorizontalOptions = LayoutOptions.Fill,
                            HorizontalOptions = LayoutOptions.Fill,
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightGreen,
            };
        }
        buttonEnd.Clicked += EndButtonClicked;
        layout1.Children.Add(buttonEnd);







        sv = new ScrollView { Content = layout1 };
        Content = sv;


    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "PageParts2";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    private async void freeThis()
    {
        Console.WriteLine("PageParts free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        int imax = 0;
        //            if (LstPlace != null)
        //            {
        //                imax = LstPlace.Count;
        //                for (int i = 0; i < imax; i++)
        //                {
        //                    LstPlace[i].Items.Clear();
        //                    LstPlace[i] = null;
        //                }
        //                LstPlace.Clear();
        //                LstPlace = null;
        //            }
        //            if (LstUnit != null)
        //            {
        //                imax = LstUnit.Count;
        //                for (int i = 0; i < imax; i++)
        //                {
        //                    LstUnit[i] = null;
        //                }
        //                LstUnit.Clear();
        //               LstUnit = null;
        //            }
        if (LstVal != null)
        {
            imax = LstVal.Count;
            for (int i = 0; i < imax; i++)
            {
                LstVal[i] = null;
            }
            LstVal.Clear();
            LstVal = null;
        }
        if (Lstlayout != null)
        {
            imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }

        if (Lstlayout != null)
        {
            imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }
        if (buttonUpd != null)
        {
            buttonUpd.Clicked -= UpdButtonClicked;
            buttonUpd = null;
        }
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }

        sv = null;
        Content = null;
        if (lstParts != null)
        {
            lstParts.freeThis();
            lstParts = null;
        }
        GC.Collect();
        Console.WriteLine("PageParts free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }
    private bool checkPartsval(ref int idx)
    {
        bool b_ret = true;
        int imax = LstVal.Count;
        string strval = string.Empty;
        for (int i = 0; i < imax; i++)
        {

            strval = LstVal[i].Text;
            //int
            try
            {
                if (string.IsNullOrEmpty(strval) == false)
                {

                    if (lstParts._Datas[i]._ValType == 1)
                    {

                        lstParts._Datas[i]._iVal = int.Parse(strval);
                    }
                    else
                    {
                        //float
                        lstParts._Datas[i]._dVal = decimal.Parse(strval);

                    }
                    LstVal[i].BackgroundColor = Colors.White;
                }
                else
                {
                    //nullでもOKにする
                    LstVal[i].BackgroundColor = Colors.White;
                    b_ret = true;
                }
            }
            catch (Exception)
            {
                idx = i;
                b_ret = false;
                LstVal[i].BackgroundColor = Colors.Red;
                //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                //throw;
                break;
            }



        }
        return b_ret;


    }

    async void EndButtonClicked(object sender, EventArgs s)
    {
        //アップデート後 jump 設定ページへ
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {



            //ページをジャンプ
            clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            clsGlobalVar.g_JumpPage = "PageParts";   //仕様変更に伴い
            string strJump = clsGlobalVar.g_JumpPage;
            clsGlobalVar.g_JumpPage = string.Empty;
            freeThis();
            if (strJump == "Page1")
            {
                Application.Current.MainPage = new Page1();
            }
            else if (strJump == "PageParts")
            {
                Application.Current.MainPage = new PageParts();

            }
            else if (strJump == "PageParts2")
            {
                Application.Current.MainPage = new PageParts2();

            }
            else if (strJump == "Page2")
            {
                Application.Current.MainPage = new Page2();

            }
            else if (strJump == "Page3")
            {
                Application.Current.MainPage = new Page3();

            }
            else if (strJump == "Page4")
            {
                Application.Current.MainPage = new Page4();

            }
            else if (strJump == "Page1_5")
            {
                Application.Current.MainPage = new Page1_5();

            }
            else if (strJump == "PageNo")
            {
                Application.Current.MainPage = new PageNo();

            }
            else if (strJump == "SashizuAdd")
            {
                Application.Current.MainPage = new SashizuAdd();

            }
            else if (strJump == "SashizuPage")
            {
                Application.Current.MainPage = new SashizuPage();

            }
            else
            {
                Application.Current.MainPage = new Page1();

            }

        }
    }




    async void UpdButtonClicked(object sender, EventArgs s)
    {
        //アップデート後 jump 設定ページへ
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            int err_idx = 0;
            bool b_ret = checkPartsval(ref err_idx);
            if (b_ret == false)
            {
                //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                wBtn.IsEnabled = true;
                return;
            }

            //更新
            string strErrMsg = "";

            string strPara = string.Empty;
            int imax = 0;
            int idx = 0;
            string strval = string.Empty;
            imax = LstVal.Count;
            for (int i = 0; i < imax; i++)
            {
                strval = LstVal[i].Text;
            }
            int ii = 0;
            string strval2 = string.Empty;
            foreach (clsPartsData wParts in lstParts._Datas)
            {
                strval = LstVal[ii].Text;
                try
                {
                    if (string.IsNullOrEmpty(strval) == false)
                    {
                        if (lstParts._Datas[ii]._ValType == 1)
                        {
                            lstParts._Datas[ii]._iVal = int.Parse(strval);
                            strval2 = lstParts._Datas[ii]._iVal.ToString();
                        }
                        else
                        {
                            //float
                            lstParts._Datas[ii]._dVal = decimal.Parse(strval);
                            strval2 = lstParts._Datas[ii]._dVal.ToString();


                        }
                    }
                    else
                    {
                        strval2 = "";
                    }

                }
                catch (Exception)
                {

                    //throw;
                }
                //品目ID!保管場所ID!数量@品目ID!保管場所ID!数量
                if (string.IsNullOrEmpty(strPara) == false)
                {
                    strPara += "@";
                }
                //strPara += wParts.partsID + "!" + wParts._SelSelected + "!" + strval2;
                strPara += wParts.partsID + "!" + strval2;
                ii++;
            }



            bool bRet = clsWebUpdate.SendPartsDataVal(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_hinnmokuID, strPara, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
                wBtn.IsEnabled = true;
                return;

            }



            //ページをジャンプ
            //clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            clsGlobalVar.g_JumpPage = "PageParts";   //仕様変更に伴い
            string strJump = clsGlobalVar.g_JumpPage;
            clsGlobalVar.g_JumpPage = string.Empty;
            freeThis();
            if (strJump == "Page1")
            {
                Application.Current.MainPage = new Page1();
            }
            else if (strJump == "PageParts")
            {
                Application.Current.MainPage = new PageParts();

            }
            else if (strJump == "PageParts2")
            {
                Application.Current.MainPage = new PageParts2();

            }
            else if (strJump == "Page2")
            {
                Application.Current.MainPage = new Page2();

            }
            else if (strJump == "Page3")
            {
                Application.Current.MainPage = new Page3();

            }
            else if (strJump == "Page4")
            {
                Application.Current.MainPage = new Page4();

            }
            else if (strJump == "Page1_5")
            {
                Application.Current.MainPage = new Page1_5();

            }
            else if (strJump == "PageNo")
            {
                Application.Current.MainPage = new PageNo();

            }
            else if (strJump == "SashizuAdd")
            {
                Application.Current.MainPage = new SashizuAdd();

            }
            else if (strJump == "SashizuPage")
            {
                Application.Current.MainPage = new SashizuPage();

            }
            else
            {
                Application.Current.MainPage = new Page1();

            }

        }
    }


}