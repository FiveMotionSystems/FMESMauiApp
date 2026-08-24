using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ZXing.Net.Maui;

namespace FMESSignage
{
    public partial class MainPage : ContentPage
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Entry user1;
        private Entry user2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Image imgLogo;
        private StackLayout Content2;
        private HorizontalStackLayout Content3;
        private HorizontalStackLayout Content4;
        private Button buttonOCR;


        public MainPage()
        {
            InitializeComponent();
            clsGlobalVar.LoadConfig();
            // セーフエリアを無効化
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            // ContentPageのパディングを0に
            this.Padding = new Thickness(0);

            App.Current.UserAppTheme = AppTheme.Light;
            Console.WriteLine($"Current Theme: {App.Current.UserAppTheme}");

            // 背景を白色に変更
            //            //this.BackgroundColor = Color.FromArgb("#D1D5DB"); // やや濃いライトグレー
            App.Current.UserAppTheme = AppTheme.Light;
            Console.WriteLine($"Current Theme: {App.Current.UserAppTheme}");

            // モダンなグラデーション背景
            this.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#F0F4F8"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#E2E8F0"), Offset = 1.0f }
                }
            };

            App.Current.UserAppTheme = AppTheme.Light;
            Console.WriteLine($"Current Theme: {App.Current.UserAppTheme}");

            // モダンなグラデーション背景
            this.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#F0F4F8"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#E2E8F0"), Offset = 1.0f }
                }
            };

            clsGlobalVar.g_NowForm = 0;

            // ロゴ画像
            imgLogo = new Image
            {
                Source = ImageSource.FromFile("logo.png"),
                HeightRequest = 140,
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                //BackgroundColor = Colors.White
                BackgroundColor = Colors.Transparent,          // ← 透過に変更
            };

            // ロゴフレーム
            var logoFrame = new Frame
            {
                CornerRadius = 0,
                HasShadow = false,
                Padding = new Thickness(20, 10, 20, 20),
                Margin = new Thickness(0, 0, 0, 0),
                BorderColor = Colors.Transparent,
                //BackgroundColor = Colors.White,
                BackgroundColor = Colors.Transparent,          // ← 透過に変更
                Content = imgLogo
            };

            // Login ID Entry - モダンスタイル（少し長く）
            user1 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 16,
                BackgroundColor = Colors.White,
                TextColor = Color.FromArgb("#1E293B"),
                Placeholder = "Login ID",
                PlaceholderColor = Color.FromArgb("#94A3B8"),
                HeightRequest = 50,
                MinimumWidthRequest = 200,
                Margin = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            // QR戻り値の復元（既存ロジック維持）
            if (clsGlobalVar.g_BackPage == "MainPage")
            {
                user1.Text = clsGlobalVar.g_QRRET;
            }

            // QRスキャンボタン - アイコンのみ、角丸
            button1 = new Button
            {
                ImageSource = "qr80.png",
                WidthRequest = 50,
                HeightRequest = 50,
                CornerRadius = 12,
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#CBD5E1"),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Padding = 8
            };
            button1.Clicked += OnQRScanClicked;

            // Login ID 行レイアウト
            Content3 = new HorizontalStackLayout
            {
                Spacing = 8,
                Margin = new Thickness(0, 0, 0, 12),
                HorizontalOptions = LayoutOptions.Fill,
                Children = { user1, button1 }
            };

            // スペーサー（既存のlabel1を維持するが非表示）
            label1 = new Label
            {
                Text = "",
                IsVisible = false
            };

            // Password Entry（Login IDと同じ幅に）
            user2 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 16,
                BackgroundColor = Colors.White,
                TextColor = Color.FromArgb("#1E293B"),
                Placeholder = "Password",
                PlaceholderColor = Color.FromArgb("#94A3B8"),
                IsPassword = true,
                HeightRequest = 50,
                MinimumWidthRequest = 200,
                Margin = new Thickness(0, 0, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Content4 = new HorizontalStackLayout
            {
                Spacing = 10,
                Margin = new Thickness(0, 0, 0, 20),
                HorizontalOptions = LayoutOptions.Fill,
                Children = { user2, label1 }
            };

            // Loginボタン - グラデーション背景
            button2 = new Button
            {
                Text = "Login",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                HeightRequest = 52,
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 0, 0, 12)
            };
            button2.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#3B82F6"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#2563EB"), Offset = 1.0f }
                }
            };
            button2.Clicked += CheckButtonClicked;

            // 環境設定ボタン - アウトライン
            button3 = new Button
            {
                Text = "Settings",
                FontSize = 14,
                TextColor = Color.FromArgb("#3B82F6"),
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#3B82F6"),
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 0, 0, 0)
            };
            button3.Clicked += ConfigButtonClicked;

            // バージョンラベル
            label3 = new Label
            {
                Text = "Version: " + AppInfo.VersionString,
                FontSize = 13,
                TextColor = Color.FromArgb("#64748B"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 16, 0, 4)
            };

            // コピーライト
            label2 = new Label
            {
                Text = "Copyright c Five Motion Systems, Inc. All rights reserved.",
                FontSize = 11,
                TextColor = Color.FromArgb("#94A3B8"),
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 16)
            };

            // 月別フッター画像（既存ロジック維持）
            int iMonth = DateTime.Now.Month;
            string imgFoot = "cal" + iMonth.ToString() + "m.png";
            Image imgFooter = new Image
            {
                Source = ImageSource.FromFile(imgFoot),
                Aspect = Aspect.AspectFit,
                HeightRequest = 120,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            // カード風フレーム（ログインフォーム）
            var loginCard = new Frame
            {
                CornerRadius = 16,
                HasShadow = true,
                BackgroundColor = Colors.White,
                Padding = new Thickness(24, 20),
                Margin = new Thickness(20, 0),
                Content = new VerticalStackLayout
                {
                    Spacing = 0,
                    Children =
                    {
                        Content3,
                        Content4,
                        button2,
                        button3
                    }
                }
            };

            // メインレイアウト
            this.Content = new ScrollView
            {
                Padding = new Thickness(0),
                BackgroundColor = Colors.Transparent,
                Content = new VerticalStackLayout
                {
                    Spacing = 12,
                    Padding = new Thickness(20, 0, 20, 20),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Colors.Transparent,
                    Children =
                    {
                        logoFrame,
                        loginCard,
                        label3,
                        label2,
                        imgFooter
                    }
                }
            };
        }

        private void OnQRScanClicked(object sender, EventArgs e)
        {
            clsGlobalVar.g_BackPage = "MainPage";
            clsGlobalVar.g_QRRET = string.Empty;
            Application.Current.MainPage = new QRPage();
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

        private void subInit()
        {
            // subInit でもページ背景を同じグラデーションに設定（背景のみ）
            this.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#F8FAFC"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#EFF6FF"), Offset = 0.6f },
                    new GradientStop { Color = Color.FromArgb("#E0F2FE"), Offset = 1.0f }
                }
            };

            user1 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                Placeholder = "Login ID",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };
            if (clsGlobalVar.g_BackPage == "MainPage")
            {
                user1.Text = clsGlobalVar.g_QRRET;
            }

            button1 = new Button
            {
                ImageSource = "QR100x100.png",
                FontSize = 20,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            };

            Content2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Colors.White,
                Children = { user1, button1 }
            };

            user2 = new Entry
            {
                Keyboard = Keyboard.Text,
                FontSize = 20,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                Placeholder = "Password",
                IsPassword = true,
            };

            button2 = new Button
            {
                Text = "Login",
                FontSize = 20,
                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),
                HorizontalOptions = LayoutOptions.Center,
            };

            button3 = new Button
            {
                Text = "環境設定",
                FontSize = 20,
                TextColor = GetTextColorParts(),
                BackgroundColor = GetBackColorParts(),
                HorizontalOptions = LayoutOptions.Fill,
            };
            button3.Clicked += ConfigButtonClicked;

            imgLogo = new Image
            {
                Source = ImageSource.FromFile("logo.png"),
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
            };

            label3 = new Label
            {
                Text = "Version Name:" + AppInfo.VersionString.ToString(),
                FontSize = 15,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            label2 = new Label
            {
                Text = "\n\nCopyright c Five Motion Systems,Inc. All rights reserved.",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            buttonOCR = new Button
            {
                Text = "　OCR　",
                FontSize = 20,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center
            };
        }

        async void ConfigButtonClicked(object sender, EventArgs s)
        {
            Application.Current.MainPage = new configPage();
        }

        async void CheckButtonClicked(object sender, EventArgs s)
        {
            string ex_message = string.Empty;
            string strErrMsg = "";
            //bool ret = clsWebUpdate.Sendchecklicense(clsGlobalVar.g_auth, clsGlobalVar.g_trmcode, ref strErrMsg);
            bool ret = true;
            if (!ret)
            {
                await DisplayAlert("ログインエラー", strErrMsg, "OK");
                return;
            }

            int iRetLogin = GetLoginVerify(user1.Text, user2.Text, ref ex_message);
            if (iRetLogin > 0)
            {
                Application.Current.MainPage = new Page1();
            }
            else if (iRetLogin == 0)
            {
                await DisplayAlert("ログインエラー", "ログインID又はパスワードを確認してください", "OK");
                return;
            }
            else if (iRetLogin == -1)
            {
                await DisplayAlert("環境設定エラー", "サーバURLを確認してください", "OK");
            }
            else if (iRetLogin == -2)
            {
                await DisplayAlert("ログインエラー", ex_message, "OK");
            }
        }
        private string Geaccess_Token(ref string wStr, ref string wStr2)
        {
            string accessToken = string.Empty;
            int iNo1 = wStr.IndexOf(",");

            string strW2 = string.Empty;
            string strLeft = wStr;
            if (iNo1 > -1)
            {
                wStr2 = strLeft.Substring(0, iNo1).Trim();

                strLeft = strLeft.Substring(iNo1 + 1);
                int iNo2 = strLeft.IndexOf("<br />");
                strW2 = strLeft.Substring(0, iNo2 + 6).Trim();
                if (iNo2 > -1)
                {
                    accessToken = strLeft.Substring(0, iNo2).Trim();
                    wStr = strLeft.Substring(iNo2 + 6);
                }
            }
            else
            {
                strW2 = wStr.Trim();
                wStr = "";
            }
            if (string.IsNullOrEmpty(strW2) == false)
            {
                return accessToken;
            }
            return accessToken;
        }
        private int GetLoginVerify(string wID, string wPW, ref string exmessage)
        {
            clsGlobalVar.g_CompanyID = wID;
            clsGlobalVar.g_CompanyPW = wPW;

            int iRet = 0;
            string strSend = clsGlobalVar.GetCurURL() + "users/tabgetplace/" + wID + "/" + wPW;
            try
            {
                string strRet = clsWebUpDown.GetWebResponce(strSend);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    strRet = strRet.Replace("<!--ダミー-->", "");
                    strRet = strRet.Replace("\r", "");
                    strRet = strRet.Replace("\n", "");
                    string strRet2 = string.Empty;
                    //トークンの取得
                    clsGlobalVar.g_AccessToken = Geaccess_Token(ref strRet, ref strRet2);



                    if (strRet2=="OK")
                    {
                        iRet = 1;
                        //clsGlobalVar.g_Parmit = 1;
                        if (strRet2=="OK2")
                        {
                            iRet = 2;
                            //clsGlobalVar.g_Parmit = 2;
                        }
                    }
                    else if (strRet2=="NG")
                    {
                        iRet = 0;
                        clsGlobalVar.g_Parmit = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                //iRet = -1;
                iRet = -2;
                clsGlobalVar.g_Parmit = 0;
                exmessage = ex.Message;
                //await DisplayAlert(ex.Message, "OK");

            }

            return iRet;
        }

    }
}
