using 






























    
    
    
    
    
    ZXing;
using ZXing.Net.Maui;

namespace FMES;

public partial class QRPage : ContentPage
{
    public QRPage()
    {
        InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

    }
    private void QRReader_CodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            
            _QRReader.IsVisible = false;

            clsGlobalVar.g_QRRET = $"{e.Results[0].Value}";
            GoBackQR();
            return;

        });
    }
    private void GoBackQR()
    {

        if (clsGlobalVar.g_BackPage == "MainPage")
        {

            Application.Current.MainPage = new MainPage();
        }
        else if (clsGlobalVar.g_BackPage == "SashizuPage")
        {
            Application.Current.MainPage = new SashizuPage();
        }
        else if (clsGlobalVar.g_BackPage == "ComLogin")
        {
            Application.Current.MainPage = new ComLogin();
        }
        else if (clsGlobalVar.g_BackPage == "configPage")
        {
            Application.Current.MainPage = new configPage();
        }
        else if (clsGlobalVar.g_BackPage == "Page1")
        {
            Application.Current.MainPage = new Page1();
        }
        else if (clsGlobalVar.g_BackPage == "Page1_5")
        {
            Application.Current.MainPage = new Page1_5();
        }
        else if (clsGlobalVar.g_BackPage == "Page2")
        {
            Application.Current.MainPage = new Page2();
        }
        else if (clsGlobalVar.g_BackPage == "Page3")
        {
            Application.Current.MainPage = new Page3();
        }
        else if (clsGlobalVar.g_BackPage == "Page4")
        {
            Application.Current.MainPage = new Page4();
        }
        else if (clsGlobalVar.g_BackPage == "Page5")
        {
            Application.Current.MainPage = new Page5();
        }
        else if (clsGlobalVar.g_BackPage == "PageMaching")
        {
            Application.Current.MainPage = new PageMaching();
        }
        else if (clsGlobalVar.g_BackPage == "PageNo")
        {
            Application.Current.MainPage = new PageNo();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            Application.Current.MainPage = new PageWeb();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            Application.Current.MainPage = new PageWeb();
        }
        else if (clsGlobalVar.g_BackPage == "webPage2")
        {
            Application.Current.MainPage = new webPage2();
        }
        else if (clsGlobalVar.g_BackPage == "SashizuAdd")
        {
            Application.Current.MainPage = new SashizuAdd();
        }
        else
        {
            //ÉoÉOÇÃèÍçáÇ±Ç±Ç…óàÇÈ
            //Application.Current.MainPage = new MainPage();
        }
        return;
    }

    private void ScanStart_Clicked(object sender, EventArgs e)
    {
        //OK
        GoBackQR();

    }

    private void ScanCancel_Clicked(object sender, EventArgs e)
    {
        //cancel
        clsGlobalVar.g_QRRET = string.Empty;
        GoBackQR();
    }


}


