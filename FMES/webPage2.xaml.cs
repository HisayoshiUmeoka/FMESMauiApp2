namespace FMES;

public partial class webPage2 : ContentPage
{
    private clsGyouretuList lstgyouretu;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
//    private WebView webB;
    private Button buttonEnd;
    private bool doingNow = false;
    private StackLayout layout1;
    private ScrollView sv;

    public webPage2()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
        this.BackgroundColor = Color.FromArgb("#D1D5DB");

        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 11;
        // ↓added for popupmeneu

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

        buttonEnd = new Button
        {
            //Text = AppResources.IDM032,
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
        layout1 = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 0, 0, 0),
        };
        layout1.Children.Add(ContentMenu);


        buttonEnd.Clicked += EndButtonClicked;


        string srtErrMsg = string.Empty;
        lstgyouretu = new clsGyouretuList();
        string wurl = clsGlobalVar.g_optionurl;
        if (lstgyouretu.GetList(wurl, ref srtErrMsg) == true)
        {
            int rowCount = lstgyouretu._Header._Gyou;
            int columnCount = lstgyouretu._Header._Retu;
            var grid = new Grid();

            for (int i = 0; i < rowCount; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Star
                });
            }

            for (int j = 0; j < columnCount; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Auto
                });
            }
            int imax = lstgyouretu._Datas.Count - 1;
            for (int i = 0; i <= imax; i++)
            {
                Label wlabel2 = new Label
                {
                    Text = lstgyouretu._Datas[i]._visible == true ? lstgyouretu._Datas[i]._text : "　　",
                    BackgroundColor = Colors.White,
                    TextColor = GetgyouretuColor(lstgyouretu._Datas[i]._fontcolor),
                    FontSize = lstgyouretu._Datas[i]._fontsize,
                    FontAttributes = lstgyouretu._Datas[i]._fontBOLD ? FontAttributes.Bold : FontAttributes.None,
                    WidthRequest = lstgyouretu._Datas[i]._width,
                    VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Start,

                };
                grid.Add(wlabel2, lstgyouretu._Datas[i]._retuno, lstgyouretu._Datas[i]._gyouno);
            }

            layout1.Children.Add(grid);


        }
        layout1.Children.Add(buttonEnd);
        sv = new ScrollView { Content = layout1 };
        Content = sv;



    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "webPage2";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu

    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {



            //ページをジャンプ
            //clsGlobalVar.g_JumpPage = "Page2";   //仕様変更に伴い
            string strJump = clsGlobalVar.g_JumpPage;
            clsGlobalVar.g_JumpPage = "SashizuPage";   //仕様変更に伴い
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
        freeThis();
        doingNow = false;

    }
    private Label MakeNewDataLabelTextColorParts(int id)
    {
        Label wlabel = new Label
        {
            Text = lstgyouretu._Datas[id]._visible==true? lstgyouretu._Datas[id]._text: "　　",
            BackgroundColor = Colors.White,
            TextColor = GetgyouretuColor(lstgyouretu._Datas[id]._fontcolor),
            FontSize = lstgyouretu._Datas[id]._fontsize,
            FontAttributes= lstgyouretu._Datas[id]._fontBOLD? FontAttributes.Bold: FontAttributes.None,
            WidthRequest = lstgyouretu._Datas[id]._width,
            VerticalOptions = LayoutOptions.Center,
            //            HorizontalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Center,

        };
        return (wlabel);

    }
    private Color GetgyouretuColor(string wcolor)
    {
        Color retCol = Colors.Black;
        if (wcolor == "Black") {
            retCol = Colors.Black;
        }
        else if(wcolor == "White"){
            retCol = Colors.White;

        }
        else if(wcolor == "Red"){
            retCol = Colors.Red;

        }
        else if(wcolor == "Green"){
            retCol = Colors.Green;

        }
        else if(wcolor == "Blue"){
            retCol = Colors.Blue;

        }
        else if(wcolor == "Yellow"){
            retCol = Colors.Yellow;

        }
        else if(wcolor == "Orange")
        {
            retCol = Colors.Orange;

        }
        else if(wcolor == "Purple")
        {
            retCol = Colors.Purple;

        }
        else if(wcolor == "Pink")
        {
            retCol = Colors.Pink;

        }
        else if(wcolor == "Gray")
        {
            retCol = Colors.Gray;

        }
        else if(wcolor == "LightGray")
        {
            retCol = Colors.LightGray;

        }
        else if(wcolor == "DarkGray")
        {
            retCol = Colors.DarkGray;

        }
        else if(wcolor == "LightBlue")
        {
            retCol = Colors.LightBlue;

        }
        else if(wcolor == "DarkBlue")
        {
            retCol = Colors.DarkBlue;

        }
        else if(wcolor == "LightGreen")
        {
            retCol = Colors.LightGreen;

        }
        else if(wcolor == "DarkGreen")
        {
            retCol = Colors.DarkGreen;

        }
        else if(wcolor == "Brown")
        {
            retCol = Colors.Brown;

        }
        else if(wcolor == "Cyan")
        {
            retCol = Colors.Cyan;

        }
        else if(wcolor == "Magenta")
        {
            retCol = Colors.Magenta;

        }
        else if(wcolor == "Lime")
        {
            retCol = Colors.Lime;

        }
        else if(wcolor == "Teal")
        {
            retCol = Colors.Teal;

        }
        else if(wcolor == "Navy")
        {
            retCol = Colors.Navy;

        }
        else if (wcolor == "Olive")
        {
            retCol = Colors.Olive;

        }
        return retCol;
    }

    private void freeThis()
    {
        Console.WriteLine("PageWeb free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        if (label1 != null)
        {
            label1 = null;
        }

        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        GC.Collect();
        Console.WriteLine("PageWeb free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }

}