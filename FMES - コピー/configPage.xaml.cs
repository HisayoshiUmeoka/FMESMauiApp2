using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ZXing.Net.Maui;

namespace FMES
{
    public partial class configPage : ContentPage
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

        public configPage()
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
                Text = "環境設定",
                FontSize = 26,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#1E293B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 12)
            };

            var subHeaderLabel = new Label
            {
                Text = "アプリケーションの設定を管理します",
                FontSize = 14,
                TextColor = Color.FromArgb("#64748B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 24)
            };

            // 言語セクション - カード
            label1 = new Label
            {
                Text = "言語",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(0, 0, 0, 8)
            };

            dropdown1 = new Picker
            {
                FontSize = 15,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                TextColor = Color.FromArgb("#1E293B"),
                Title = "選択してください",
                HeightRequest = 48
            };
            dropdown1.Items.Add("Japanese");
            dropdown1.Items.Add("English");
            dropdown1.Items.Add("Chinese");
            dropdown1.Items.Add("Korean");
            dropdown1.SelectedIndex = clsGlobalVar.g_language;

            var languageCard = new Frame
            {
                CornerRadius = 14,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(20, 16),
                Margin = new Thickness(20, 0, 20, 16),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { label1, dropdown1 }
                }
            };

            // URLセクション - カード
            label7 = new Label
            {
                Text = "サーバーURL",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(0, 0, 0, 8)
            };

            txturl = new Entry
            {
                Keyboard = Keyboard.Url,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                TextColor = Color.FromArgb("#1E293B"),
                Text = clsGlobalVar.g_CompanyURL,
                FontSize = 15,
                Placeholder = "https://example.com/",
                PlaceholderColor = Color.FromArgb("#94A3B8"),
                HeightRequest = 48,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // QR戻り値の復元（既存ロジック維持）
            if (clsGlobalVar.g_BackPage == "configPage" && !string.IsNullOrEmpty(clsGlobalVar.g_QRRET))
            {
                txturl.Text = clsGlobalVar.g_QRRET;
                clsGlobalVar.g_BackPage = string.Empty;
                clsGlobalVar.g_QRRET = string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(clsGlobalVar.g_CompanyURL))
                {
                    txturl.Text = "https://";
                }
            }

            Scanbutton = new Button
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
            Scanbutton.Clicked += ScanButtonClicked;

            Content5 = new HorizontalStackLayout
            {
                Spacing = 10,
                Children = { txturl, Scanbutton }
            };

            var urlCard = new Frame
            {
                CornerRadius = 14,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(20, 16),
                Margin = new Thickness(20, 0, 20, 16),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { label7, Content5 }
                }
            };

            // ログセクション - カード
            label3 = new Label
            {
                Text = "ログ出力",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#334155"),
                Margin = new Thickness(0, 0, 0, 8)
            };

            dropdown3 = new Picker
            {
                FontSize = 15,
                BackgroundColor = Color.FromArgb("#F8FAFC"),
                TextColor = Color.FromArgb("#1E293B"),
                Title = "選択してください",
                HeightRequest = 48
            };
            dropdown3.Items.Add("ログ送信する");
            dropdown3.Items.Add("ログ送信無し");
            dropdown3.SelectedIndex = clsGlobalVar.g_logWrite;

            var logCard = new Frame
            {
                CornerRadius = 14,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(20, 16),
                Margin = new Thickness(20, 0, 20, 16),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { label3, dropdown3 }
                }
            };

            // 認証クリアボタン（アウトライン）
            buttonclear = new Button
            {
                Text = "認証情報クリア",
                FontSize = 14,
                TextColor = Color.FromArgb("#DC2626"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#DC2626"),
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12)
            };
            buttonclear.Clicked += clearButtonClicked;

            // 保存ボタン（プライマリーグラデーション）
            buttonUpd = new Button
            {
                Text = "保存",
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
                Text = "キャンセル",
                FontSize = 14,
                TextColor = Color.FromArgb("#64748B"),
                BackgroundColor = Color.FromArgb("#F1F5F9"),
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 20)
            };
            buttonEnd.Clicked += EndButtonClicked;

            // 非表示フィールド（既存コードとの互換性維持）
            label2 = new Label { IsVisible = false };
            label4 = new Label { IsVisible = false };
            label5 = new Label { IsVisible = false };
            label6 = new Label { IsVisible = false };
            txtrmname = new Entry { IsVisible = false };
            txtauth = new Entry { IsVisible = false };
            Content2 = new HorizontalStackLayout { IsVisible = false };
            Content4 = new HorizontalStackLayout { IsVisible = false };

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
                        languageCard,
                        urlCard,
                        logCard,
                        buttonclear,
                        buttonUpd,
                        buttonEnd
                    }
                }
            };
        }

        private async Task<bool> checkURLAsync()
        {
            bool ret = false;
            string strurl = txturl.Text;
            ret = IsValidUrl(strurl);
            if (!ret)
            {
                int i1 = strurl.IndexOf("https://");
                if (i1 == -1)
                {
                    strurl = "https://" + strurl;
                    ret = IsValidUrl(strurl);
                }
                int i2 = strurl.LastIndexOf("/");
                if (i2 != strurl.Length - 1)
                {
                    strurl = strurl + "/";
                    txturl.Text = strurl;
                    ret = IsValidUrl(strurl);
                }
            }
            if (ret)
            {
                txturl.Text = strurl;
            }
            else
            {
                await DisplayAlert("環境設定エラー", "URLが不正です", "OK");
            }

            return ret;

            static async Task<bool> UrlExistsAsync(string url)
            {
                try
                {
                    using HttpClient client = new();
                    HttpResponseMessage response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }
        }

        static bool IsValidUrl(string url)
        {
            string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }

        private string Gettrmcode()
        {
            string str_ret;
            str_ret = ComputeSHA256(DateTime.Now.ToString("yyyyMMddHHmmss")).Substring(0, 10);
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
            clsGlobalVar.g_auth = string.Empty;
            clsGlobalVar.g_trmcode = string.Empty;
            clsGlobalVar.g_trmname = string.Empty;
            clsGlobalVar.SaveConfig();
            await DisplayAlert("認証情報クリア", "認証情報をクリアしました", "OK");
            return;
        }

        async void UpdButtonClicked(object sender, EventArgs s)
        {
            await checkURLAsync();

            if (clsGlobalVar.g_language != dropdown1.SelectedIndex)
            {
                // 言語変更処理
            }
            clsGlobalVar.g_language = dropdown1.SelectedIndex;
            clsGlobalVar.g_logWrite = dropdown3.SelectedIndex;
            clsGlobalVar.g_CompanyURL = txturl.Text;
            clsGlobalVar.g_svUrlTop = txturl.Text;
            clsGlobalVar.g_CompanyID = "";
            clsGlobalVar.g_CompanyPW = "";
            clsGlobalVar.g_ComRegisterd = 1;

            if (string.IsNullOrEmpty(clsGlobalVar.g_CompanyURL) || string.IsNullOrEmpty(txturl.Text))
            {
                await DisplayAlert("環境設定エラー", "サーバURLを入力してください", "OK");
                return;
            }

            clsGlobalVar.SaveConfig();
            if (string.IsNullOrEmpty(clsGlobalVar.g_trmcode))
            {
                freeThis();
                Application.Current.MainPage = new configPage2();
            }
            else
            {
                freeThis();
                Application.Current.MainPage = new MainPage();
            }
        }

        async void EndButtonClicked(object sender, EventArgs s)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void freeThis()
        {
            GC.Collect();
        }
    }
}