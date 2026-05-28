namespace FMES
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
            //環境設定読込            
            clsGlobalVar.LoadConfig();
            if (clsGlobalVar.g_CompanyURL == "")
            {
                //環境設定に行くべき
                //Application.Current.MainPage = new configPage();
            }

        }
    }
}
