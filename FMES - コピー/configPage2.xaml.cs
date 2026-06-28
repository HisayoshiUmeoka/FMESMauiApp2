using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FMES
{
    public partial class configPage2 : ContentPage
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Entry txturl;
        private Entry txtrmname;
        private Entry txtauth;
        private HorizontalStackLayout Content2;
        private HorizontalStackLayout Content4;
        private HorizontalStackLayout Content5;
        private VerticalStackLayout Content3;
        private Picker dropdown1;
        private Picker dropdown3;
        private Button Scanbutton;
        private Button buttonclear;
        private Button buttonUpd;
        private Button buttonEnd;

        public configPage2()
        {
            InitializeComponent();
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);

            this.Padding = new Thickness(0);
            clsGlobalVar.g_NowForm = 1;

            // 背景を白色に変更
            this.BackgroundColor = Colors.White;

            // ヘッダー
            var headerLabel = new Label
            {
                Text = "認証設定",
                FontSize = 26,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#1E293B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 12)
            };

            var subHeaderLabel = new Label
            {
                Text = "初回利用時に認証情報を登録してください",
                FontSize = 14,
                TextColor = Color.FromArgb("#64748B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 24)
            };

            // NAME入力セクション
            label5 = new Label
            {
                Text = "NAME",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(0, 0, 0, 8)
            };

            txtrmname = new Entry
            {
                Keyboard = Keyboard.Text,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                TextColor = Color.FromArgb("#1E293B"),
                Text = "",
                FontSize = 15,
                Placeholder = "端末名を入力してください",
                PlaceholderColor = Color.FromArgb("#94A3B8"),
                HeightRequest = 48,
                HorizontalOptions = LayoutOptions.Fill
            };

            var nameCard = new Frame
            {
                CornerRadius = 14,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(20, 16),
                Margin = new Thickness(20, 0, 20, 16),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { label5, txtrmname }
                }
            };

            // AUTH入力セクション
            label6 = new Label
            {
                Text = "AUTH",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(0, 0, 0, 8)
            };

            txtauth = new Entry
            {
                Keyboard = Keyboard.Text,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                TextColor = Color.FromArgb("#1E293B"),
                Text = "",
                FontSize = 15,
                Placeholder = "認証コードを入力してください（5文字以上）",
                PlaceholderColor = Color.FromArgb("#94A3B8"),
                HeightRequest = 48,
                HorizontalOptions = LayoutOptions.Fill
            };

            var authCard = new Frame
            {
                CornerRadius = 14,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(20, 16),
                Margin = new Thickness(20, 0, 20, 16),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { label6, txtauth }
                }
            };

            // 注意書き
            var noticeLabel = new Label
            {
                Text = "※ 認証情報はサーバーに送信され、端末登録に使用されます",
                FontSize = 12,
                TextColor = Color.FromArgb("#64748B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20, 0, 20, 20)
            };

            // 登録ボタン（プライマリーグラデーション）
            buttonUpd = new Button
            {
                Text = "登録",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                HeightRequest = 52,
                CornerRadius = 12,
                Margin = new Thickness(20, 8, 20, 12)
            };
            buttonUpd.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#3B82F6"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#2563EB"), Offset = 1.0f }
                }
            };
            buttonUpd.Clicked += UpdButtonClicked;

            // キャンセルボタン（セカンダリー）
            buttonEnd = new Button
            {
                Text = "戻る",
                FontSize = 14,
                TextColor = Color.FromArgb("#64748B"),
                BackgroundColor = Color.FromArgb("#F1F5F9"),
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 20)
            };
            buttonEnd.Clicked += EndButtonClicked;

            // 非表示フィールド（既存コードとの互換性維持）
            label1 = new Label { IsVisible = false };
            label2 = new Label { IsVisible = false };
            label3 = new Label { IsVisible = false };
            label4 = new Label { IsVisible = false };
            label7 = new Label { IsVisible = false };
            txturl = new Entry { IsVisible = false };
            dropdown1 = new Picker { IsVisible = false };
            dropdown3 = new Picker { IsVisible = false };
            Scanbutton = new Button { IsVisible = false };
            buttonclear = new Button { IsVisible = false };
            Content2 = new HorizontalStackLayout { IsVisible = false };
            Content4 = new HorizontalStackLayout { IsVisible = false };
            Content5 = new HorizontalStackLayout { IsVisible = false };

            // メインレイアウト
            this.Content = new ScrollView
            {
                Padding = new Thickness(0),
                BackgroundColor = Colors.White,
                Content = new VerticalStackLayout
                {
                    Spacing = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Colors.White,
                    Children =
                    {
                        headerLabel,
                        subHeaderLabel,
                        nameCard,
                        authCard,
                        noticeLabel,
                        buttonUpd,
                        buttonEnd
                    }
                }
            };
        }

        private string Gettrmcode()
        {
            string str_base = DateTime.Now.ToString("yyyyMMddHHmmss");
            string str_ret = str_base.Substring(0, 10);
            try
            {
                str_ret = ComputeSHA256(str_base).Substring(0, 10);
            }
            catch (Exception)
            {
                str_ret = str_base.Substring(0, 10);
            }

            return str_ret;
        }

        static string ComputeSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
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

        async void ScanButtonClicked(object sender, EventArgs s)
        {
            clsGlobalVar.g_BackPage = "configPage";
            clsGlobalVar.g_QRRET = string.Empty;
            Application.Current.MainPage = new QRPage();

            clsGlobalVar.g_svUrlTop = txturl.Text;
            clsGlobalVar.g_CompanyURL = txturl.Text;
        }

        async void clearButtonClicked(object sender, EventArgs s)
        {
            string trmcode = "";

            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                trmcode = Gettrmcode();
                if (string.IsNullOrEmpty(txtauth.Text) || txtauth.Text.Length < 5)
                {
                    await DisplayAlert("環境設定エラー", "正しいAUTHを入力してください", "OK");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtrmname.Text))
                    {
                        await DisplayAlert("環境設定エラー", "NAMEを入力してください", "OK");
                        return;
                    }
                    else
                    {
                        string strErrMsg = "";
                        bool ret = clsWebUpdate.SendTrmData(txtauth.Text, trmcode, txtrmname.Text, ref strErrMsg);
                        if (ret == false)
                        {
                            await DisplayAlert("環境設定エラー", strErrMsg, "OK");
                            return;
                        }
                        else
                        {
                            await DisplayAlert("環境設定完了", "認証設定完了", "OK");
                        }
                    }
                }
            }

            clsGlobalVar.g_trmname = txtrmname.Text;
            clsGlobalVar.g_auth = txtauth.Text;
            clsGlobalVar.g_trmcode = trmcode;
            clsGlobalVar.SaveConfig();
            Application.Current.MainPage = new MainPage();
        }

        async void UpdButtonClicked(object sender, EventArgs s)
        {
            string trmcode = "";

            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                trmcode = Gettrmcode();
                if (string.IsNullOrEmpty(txtauth.Text) || txtauth.Text.Length < 5)
                {
                    await DisplayAlert("環境設定エラー", "正しいAUTHを入力してください（5文字以上）", "OK");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtrmname.Text))
                    {
                        await DisplayAlert("環境設定エラー", "NAMEを入力してください", "OK");
                        return;
                    }
                    else
                    {
                        string strErrMsg = "";
                        bool ret = clsWebUpdate.SendTrmData(txtauth.Text, trmcode, txtrmname.Text, ref strErrMsg);
                        if (ret == false)
                        {
                            await DisplayAlert("環境設定エラー", strErrMsg, "OK");
                            return;
                        }
                        else
                        {
                            await DisplayAlert("環境設定完了", "認証設定完了", "OK");
                        }
                    }
                }
            }

            clsGlobalVar.g_trmname = txtrmname.Text;
            clsGlobalVar.g_auth = txtauth.Text;
            clsGlobalVar.g_trmcode = trmcode;
            clsGlobalVar.SaveConfig();
            Application.Current.MainPage = new MainPage();
        }

        async void EndButtonClicked(object sender, EventArgs s)
        {
            Application.Current.MainPage = new configPage();
        }
    }
}
