namespace FMES;

public partial class logoutPage : ContentPage
{
	public logoutPage()
	{
		InitializeComponent();
		Image imglogo = new Image
		{
			Source = "logo.png"
		};


        this.Content = new ScrollView
		{

            Content = new VerticalStackLayout
			{
				Spacing = 25,
				Padding = new Thickness(30, 0),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Children = {
                    imglogo,
            }

			}
		};
         imglogo.RelScaleTo(2, 2000);
        imglogo.RelRotateTo(360, 2000);
        imglogo.Opacity = 1;
        imglogo.FadeTo(0, 4000);
    }
}