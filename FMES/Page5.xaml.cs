using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FMES;

public partial class Page5 : ContentPage
{
    private clsKaisouList lstKaisou;
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    //private Button buttonSS;
    private List<Button> Lstbutton = new List<Button>();
    private Picker dropdown1;
    private Entry txtVal1;
    private Button buttonPass;
    private Button buttonUpd;
    private Button buttonEnd;
    //private Button buttonOCR;
    private ActivityIndicator actIndOCR;
    private AbsoluteLayout absLay;
    private Image imgView;
    private StackLayout layout1;
    private ScrollView sv;
    //東レ用画面種別20用次へ　前へボタン
    private Button buttonnext;
    private Button buttonprev;
    private Label labelDummy;
    private StackLayout layout20;

    private bool doingNow = false;

    public Page5()
	{
		InitializeComponent();
        Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

        //        this.BackgroundColor = Color.FromArgb("#D1D5DB");
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


        //AppResources.Culture = new CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 8;
        string wwsashizuNo = clsGlobalVar.g_SasizuNo;
        if (clsGlobalVar.g_SasizuNo == "-2")
        {
            wwsashizuNo = "指図番号無し作業";
        }
        else if (clsGlobalVar.g_SasizuNo == "-1")
        {
            wwsashizuNo = "その他";
        }

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

        string srtErrMsg = string.Empty;
        lstKaisou = new clsKaisouList();
        if (lstKaisou.GetList(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuNo, 5, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, clsGlobalVar.g_lastSashizuKind, clsGlobalVar.g_KouteiVer, ref srtErrMsg) == true)
        {
            clsGlobalVar.g_KouteiKekkaID = lstKaisou._Header._KouteiKekkaID;

            //Padding = new Thickness(0, Device.OnPlatform(10, 0, 0), 0, 0);
            if (lstKaisou._Header._GamenKind == 1)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                buttonUpd,
                                buttonEnd,
                            }
                    };
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                dropdown1 = new Picker
                {
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM033,
                    Title = "ライン選択",
                    MinimumWidthRequest = 200,
                    Margin = new Thickness(20, 0, 20, 0),

                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsLine wLine in lstKaisou._Header._LineLists)
                {
                    dropdown1.Items.Add(wLine._LineName);
                    if (wLine._index == lstKaisou._Header._SelSelected)
                    {
                        dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                    }
                }

                if (lstKaisou._Header._done == 0)
                {
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                dropdown1,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                dropdown1,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {
                layout1 = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(10, 10, 10, 10),
                };

                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                layout1.Children.Add(ContentMenu);
                layout1.Children.Add(label1);
                layout1.Children.Add(label2);
                layout1.Children.Add(label3);
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        FontSize = 14,

                        BorderColor = GetBorderColor(wKaisou),
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Fill,
                        TextColor = GetTextColor(wKaisou),
                        BackgroundColor = GetBackColor(wKaisou),

                    };
                    butn.Clicked += ItemButtonClicked;
                    layout1.Children.Add(butn);
                    Lstbutton.Add(butn);
                }

                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    layout1.Children.Add(buttonUpd);
                    buttonUpd.Clicked += UpdButtonClicked;
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };
                layout1.Children.Add(buttonEnd);

                sv = new ScrollView { Content = layout1 };
                Content = sv;
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 14,
                    BorderColor = Colors.LightGray,
                    BorderWidth = 1.5,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 0, 20, 12),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    //buttonSS.Clicked += SSButtonClicked;
                    buttonPass.Clicked += PassButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };

                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 14,
                    BorderColor = Colors.LightGray,
                    BorderWidth = 1.5,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 0, 20, 12),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonPass.Clicked += PassButtonClicked;
                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                buttonPass,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 6)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    MinimumWidthRequest = 200,
                    Margin = new Thickness(20, 0, 20, 0),
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    dropdown1 = new Picker
                    {
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        TextColor = Colors.Black,
                        FontSize = 26,
                        //Title = AppResources.IDM039,
                        Title = "指数選択",
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                        if (wLine._index == lstKaisou._Header._SelSelected)
                        {
                            dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                        }
                    }
                }
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2;
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {
                        txtVal1,
                        dropdown1,
                        label6,
                        }
                    };
                }
                else
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                        txtVal1,
                        //dropdown1,
                        label6,
                        }
                    };
                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(30, 0, 20, 0),
                    //Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    //Placeholder = AppResources.IDM071,
                    MinimumWidthRequest = 300,
                    Placeholder = "文字列入力",
                    Text = ConvStr2Disp(lstKaisou._Header._strVal),
                };

                if (lstKaisou._Header._done == 0)
                {
                    layout1 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {
                                txtVal1,
                                //buttonOCR,
                            }
                    };
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    actIndOCR = new ActivityIndicator
                    {
                        //Color = Device.OnPlatform(Colors.Black, Colors.Default, Colors.Default),
                        IsRunning = false, // 回転中
                        VerticalOptions = LayoutOptions.Center // 中央に配置する
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                layout1,
                                actIndOCR,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                txtVal1,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                dropdown1 = new Picker
                {
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    //Title = AppResources.IDM040,
                    Title = "未選択",
                    MinimumWidthRequest = 200,
                    Margin = new Thickness(20, 0, 20, 0),
                    VerticalOptions = LayoutOptions.Start
                };
                //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    dropdown1.Items.Add(wKaisou._kaisouName);
                }
                dropdown1.SelectedIndex = GetCurSelectedDropDown(lstKaisou._Header._strCmb);
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                buttonPass = new Button
                {
                    Text = GetPassButtonStr(lstKaisou._Header._iPass),
                    FontSize = 14,
                    BorderColor = Colors.LightGray,
                    BorderWidth = 1.5,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 0, 20, 12),
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = GetPassButtonTColor(lstKaisou._Header._iPass),
                    BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass),
                };
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                dropdown1,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                dropdown1,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                
                
                
                
                
                
                var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

                double widthPx = displayInfo.Width;     // 画面幅（px）
                double heightPx = displayInfo.Height;   // 画面高さ（px）
                double density = displayInfo.Density;   // 画面密度

                double wimgWidth = widthPx / density;   // 画面幅（dp）
                double imgWidth = 0;
                double imgHeight = 0;
                double pos_rate = 7.0 / 16.0;
                int iwidth = (int)wimgWidth;
                string straddfile = "";
                imgWidth = 350;
                imgHeight = 226;
                straddfile = "_1";
//                if (iwidth >= 350 && iwidth > 0)
//                {
//                    imgWidth = 350;
//                    imgHeight = 226;
//                    straddfile = "_1";
//                }
                if (iwidth >= 400 && iwidth > 350)
                {
                    imgWidth = 400;
                    imgHeight = 258;
                    straddfile = "_2";
                    pos_rate = 1.0 / 2.0;
                }
                else if (iwidth >= 450 && iwidth > 400)
                {
                    imgWidth = 450;
                    imgHeight = 290;
                    straddfile = "_3";
                    pos_rate = 9.0 / 16.0;
                }
                else if (iwidth >= 500 && iwidth > 450)
                {
                    imgWidth = 500;
                    imgHeight = 322;
                    straddfile = "_4";
                    pos_rate = 5.0 / 8.0;
                }
                else if (iwidth >= 550 && iwidth > 500)
                {
                    imgWidth = 550;
                    imgHeight = 354;
                    straddfile = "_5";
                    pos_rate = 11.0 / 16.0;
                }
                else if (iwidth >= 600 && iwidth > 550)
                {
                    imgWidth = 600;
                    imgHeight = 387;
                    straddfile = "_6";
                    pos_rate = 3.0 / 4.0;
                }
                else if (iwidth >= 650 && iwidth > 600)
                {
                    imgWidth = 650;
                    imgHeight = 419;
                    straddfile = "_7";
                    pos_rate = 13.0 / 16.0;
                }
                else if (iwidth >= 700 && iwidth > 650)
                {
                    imgWidth = 700;
                    imgHeight = 451;
                    straddfile = "_8";
                    pos_rate = 7.0 / 8.0;
                }
                else if (iwidth >= 750 && iwidth > 700)
                {
                    imgWidth = 750;
                    imgHeight = 484;
                    straddfile = "_9";
                    pos_rate = 1.0;
                }
                else
                {
                    imgWidth = 800;
                    imgHeight = 516;
                    straddfile = "";
                }

                // 元画像の実ピクセルサイズ（正しい値に置換）
                double origW_px = imgWidth;
                double origH_px = imgHeight;
                absLay = new AbsoluteLayout
                    {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = new Thickness(0),
                        ZIndex = 0,
                    };
                string cacheBuster = $"?t={DateTime.UtcNow.Ticks}";
                var uri = clsGlobalVar.GetCurURL() + "img/instruction/" + lstKaisou._Header._ImageFile+ straddfile;
                int ino = lstKaisou._Header._ImageFile.IndexOf(".",0);
                if (ino > -1)
                {
                    uri = clsGlobalVar.GetCurURL() + "img/instruction/" + lstKaisou._Header._ImageFile.Substring(0, ino) + straddfile + lstKaisou._Header._ImageFile.Substring(ino);
                }










                Debug.WriteLine(uri);
                Trace.WriteLine(uri);

                imgView = new Image
                {
                    Source = ImageSource.FromUri(new Uri(uri + cacheBuster)),

                    //Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    HeightRequest = imgHeight,
                    WidthRequest = imgWidth,
                    MinimumHeightRequest = imgHeight,
                    MinimumWidthRequest = imgWidth,
                };
                int z = 0;
                absLay.Children.Add(imgView);

                //imgView.ZIndex = 0;
                z++;
                absLay.SetLayoutFlags(imgView, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.None);
                absLay.SetLayoutBounds(imgView, new Rect(0, 0, imgWidth, imgHeight));

                // 画像・ボタン配置の作成・配置ロジック（置換用）
                double origW = imgWidth; // 元画像の実ピクセル幅（正しい値に置き換えてください）
                double origH = imgHeight; // 元画像の実ピクセル高さ
                                          // 一時リストにボタンと元座標(px)を保存
                var buttonInfos = new List<(Button btn, double srcX_px, double srcY_px, double btnW_dp, double btnH_dp)>();
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    double baseFontsize = 14;//22
                    Button butn = new Button
                    {
                        Text = wKaisou._kaisouName,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        FontSize = baseFontsize,
                        ZIndex = ++z,
                        WidthRequest = baseFontsize * wKaisou._kaisouName.Length,
                        BackgroundColor = GetPassButtonBColor9(wKaisou._during),
                        TextColor = GetPassButtonTColor(wKaisou._iPass),
                    };
                    butn.Clicked += ItemButtonClicked;
                    Lstbutton.Add(butn);

                    // 元座標は px 単位と仮定（必要ならここで調整）
                    double srcX_px = wKaisou._IconButton.X* pos_rate + (-16);
                    //double srcX_px = wKaisou._IconButton.X / 2 + (-16 - 28.6) - (baseFontsize * wKaisou._kaisouName.Length);
                    //double srcY_px = wKaisou._IconButton.Y / 2 + (-17 - 98.4);
                    double srcY_px = wKaisou._IconButton.Y* pos_rate + (-17);

                    // 一旦追加（位置は SizeChanged で設定）
                    absLay.Children.Add(butn);
                    double btnW_dp = butn.WidthRequest > 0 ? butn.WidthRequest : baseFontsize * wKaisou._kaisouName.Length*2;
                    double btnH_dp = butn.HeightRequest > 0 ? butn.HeightRequest : 48;
                    buttonInfos.Add((butn, srcX_px, srcY_px, btnW_dp, btnH_dp));
                }

                // 画像の実表示サイズが確定したらボタンを配置
                imgView.SizeChanged += (s, e) =>
                {
                    double dispW_dp = imgView.Width;
                    double dispH_dp = imgView.Height;
                    if (dispW_dp <= 0 || dispH_dp <= 0) return;

                    // px -> dp
                    double origW_dp = origW_px / density;
                    double origH_dp = origH_px / density;

                    // AspectFit のスケール
                    double scale = Math.Min(dispW_dp / origW_dp, dispH_dp / origH_dp);

                    // 表示される画像サイズと余白（letterbox）
                    double displayedImageW = origW_dp * scale;
                    double displayedImageH = origH_dp * scale;
                    double offsetX = (dispW_dp - displayedImageW) / 2.0;
                    double offsetY = (dispH_dp - displayedImageH) / 2.0;

                    // imgView が absLay 内で置かれた左上座標（SetLayoutBounds で置いているなら通常 0,0）
                    var imgBounds = AbsoluteLayout.GetLayoutBounds(imgView);
                    double imgLeft = imgBounds.X;
                    double imgTop = imgBounds.Y;

                    foreach (var info in buttonInfos)
                    {
                        var btn = info.btn;
                        // 元座標(px) -> dp に変換してスケール適用
                        double srcX_dp = info.srcX_px / density;
                        double srcY_dp = info.srcY_px / density;

                        double placedX = imgLeft + offsetX + srcX_dp * scale;
                        double placedY = imgTop + offsetY + srcY_dp * scale;

                        AbsoluteLayout.SetLayoutFlags(btn, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.None);
                        AbsoluteLayout.SetLayoutBounds(btn, new Rect(placedX, placedY, info.btnW_dp, info.btnH_dp));
                    }
                };
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                    ZIndex = ++z,

                };

                layout1 = new StackLayout
                {
                    //Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(0),
                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    Children = {
                            ContentMenu,
                            label1,
                            label2,
                            label3,
                            buttonEnd,
                            absLay,
                            //buttonEnd,
                        }
                };
                sv = new ScrollView {
                    //Orientation = ScrollOrientation.Both,
                    Content = layout1 
                };
                Content = sv;

                //imgView.Clicked += 
                buttonEnd.Clicked += EndButtonClicked;
                //アニメーションを入れてみる
                foreach (Button wbutn in Lstbutton)
                {
                    wbutn.Opacity = 0;
                    wbutn.FadeTo(1, 4000);
                    //wbutn.RelScaleTo(2);
                    //                    wbutn.RelScaleTo(250);
                    //wbutn.RelRotateTo(90);

                    //  string stwork = wbutn.Text;
                    //  wbutn.Text = " " + stwork + "　";
                    //  wbutn.Text = stwork;
                }



            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                labelDummy = new Label
                {
                    Text = "　　",
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    //HorizontalOptions = LayoutOptions.End,
                };

                buttonnext = new Button
                {
                    //Text = "次へ",
                    ImageSource = "next.png",
                    FontSize = 20,
                    //Margin = new Thickness(0, 5, 0, 5),
                    Margin = new Thickness(0, 40, 0, 20),
                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    //HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.End,
                    //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
                    VerticalOptions = LayoutOptions.EndAndExpand // 中央に配置する（縦方向）
                };
                buttonnext.Clicked += MenuButtonNextClicked;


                buttonprev = new Button
                {
                    //Text = "前へ",
                    ImageSource = "prev.png",
                    FontSize = 20,
                    Margin = new Thickness(0, 15, 0, 15),
                    Padding = new Thickness(5, 10, 10, 15),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    //HorizontalOptions = LayoutOptions.Center//,//中央に配置する（横方向）
                    //VerticalOptions = LayoutOptions.CenterAndExpand // 中央に配置する（縦方向）
                    VerticalOptions = LayoutOptions.EndAndExpand // 中央に配置する（縦方向）
                };
                buttonprev.Clicked += MenuButtonPrevClicked;

                layout20 = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    Children = {
                        buttonprev,
                        labelDummy,
                        buttonnext,
                    }
                };


                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    //HorizontalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.End,
                    MinimumWidthRequest = 200,
                    Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal),
                };
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    dropdown1 = new Picker
                    {
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        TextColor = Colors.Black,
                        FontSize = 26,
                        //Title = AppResources.IDM039,
                        Title = "指数選択",
                        VerticalOptions = LayoutOptions.Center
                    };
                    //var ar = Enumerable.Range(0, 100).Select(n => string.Format("item-{0}", n)).ToList();
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        dropdown1.Items.Add(wLine._LineName);
                        if (wLine._index == lstKaisou._Header._SelSelected)
                        {
                            dropdown1.SelectedIndex = dropdown1.Items.Count - 1;
                        }
                    }
                }
                label6 = new Label
                {
                    Text = lstKaisou._Header._InputUnit,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                StackLayout Content2;
                if (lstKaisou._Header._LineLists.Count > 0)
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {
                        txtVal1,
                        dropdown1,
                        label6,
                        }
                    };
                }
                else
                {
                    Content2 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {
                        txtVal1,
                        //dropdown1,
                        label6,
                        }
                    };
                }
                if (lstKaisou._Header._done == 0)
                {
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                layout20,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                Content2,
                                layout20,

                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                // StackLayoutで2つの Entryコントロールを並べる
                label1 = new Label
                {
                    Text = "　" + lstKaisou._Header._Title,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 22,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label2 = new Label
                {
                    //Text = "　　" + AppResources.IDM029 + "：" + lstKaisou._Header._ProductName,
                    Text = "　　" + "機種" + "：" + lstKaisou._Header._ProductName,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                };
                label3 = new Label
                {
                    //Text = "　　" + AppResources.IDM030 + "：" + wwsashizuNo,
                    Text = "　　" + "指図番号" + "：" + wwsashizuNo,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0, 0, 0, 0),
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                label5 = new Label
                {
                    Text = lstKaisou._Header._InputSetsumei,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Fill,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                txtVal1 = new Entry
                {
                    Keyboard = Keyboard.Text,
                                    BackgroundColor = Colors.Transparent,          // ← 透過に変更
                    TextColor = Colors.Black,
                    FontSize = 26,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(30, 0, 20, 0),
                    //Placeholder = GetKetaStr(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou),
                    //Placeholder = AppResources.IDM071,
                    MinimumWidthRequest = 300,
                    Placeholder = "文字列入力",
                    Text = ConvStr2Disp(lstKaisou._Header._strVal),
                };

                if (lstKaisou._Header._done == 0)
                {
                    layout1 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Center,
                        Children = {
                                txtVal1,
                                //buttonOCR,
                            }
                    };
                    buttonUpd = new Button
                    {
                        //Text = AppResources.IDM038,
                        Text = "更新",
                        FontSize = 14,
                        BorderColor = Colors.LightGray,
                        BorderWidth = 1.5,
                        HeightRequest = 48,
                        CornerRadius = 12,
                        Margin = new Thickness(20, 0, 20, 12),
                        //VerticalOptions = LayoutOptions.Center,
                        //            HorizontalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.LightGreen,
                    };
                    actIndOCR = new ActivityIndicator
                    {
                        //Color = Device.OnPlatform(Colors.Black, Colors.Default, Colors.Default),
                        IsRunning = false, // 回転中
                        VerticalOptions = LayoutOptions.Center // 中央に配置する
                    };
                }
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
                    //VerticalOptions = LayoutOptions.Center,
                    //            HorizontalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                };

                if (lstKaisou._Header._done == 0)
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                layout1,
                                actIndOCR,
                                buttonUpd,
                                buttonEnd,
                            }
                    };

                    buttonUpd.Clicked += UpdButtonClicked;
                }
                else
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(10, 10, 10, 10),
                                        BackgroundColor = Colors.Transparent,          // ← 透過に変更
                        Children = {
                                ContentMenu,
                                label1,
                                label2,
                                label3,
                                label5,
                                txtVal1,
                                //buttonUpd,
                                buttonEnd,
                            }
                    };
                }
                buttonEnd.Clicked += EndButtonClicked;
            }

        }
        else
        {
            label1 = new Label
            {
                //Text = "　" + AppResources.IDM027,
                Text = "　" + "データエラー（又は通信エラー）",
                                BackgroundColor = Colors.Transparent,          // ← 透過に変更
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Fill,
            };
            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                                BackgroundColor = Colors.Transparent,          // ← 透過に変更
                Children = {
                    label1,
                    }
            };
        }


    }
    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page5";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    async void ItemButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            int i = 0;
            foreach (Button wBtn in Lstbutton)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    clsGlobalVar.g_KaisouNo = 4;
                    clsGlobalVar.g_KouteiID = lstKaisou._Datas[i]._KouteiID;
                    clsGlobalVar.g_KouteiShousaiID = lstKaisou._Datas[i]._KouteiShousaiID;
                    clsGlobalVar.g_KensaBashoID = lstKaisou._Datas[i]._KensaBashoID;

                    clsGlobalVar.g_KensaBashoShousaiID = 0;
                    //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                    clsGlobalVar.g_LineIndex = GetSelectedLineID();
                    //freeThis();
                    //await Navigation.PushAsync(new Page4(yourData));
                    clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[1]._KensaBashoShousaiID;
                    freeThis();
                    Application.Current.MainPage = new Page5();
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }
    async void MenuButtonPrevClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                //clsGlobalVar.g_KaisouNo = 4;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_LineIndex = GetSelectedLineID();

                clsGlobalVar.g_KensaBashoShousaiID = 0;
                //freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                clsGlobalVar.g_KaisouNo = 5;
                clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[0]._KensaBashoShousaiID;//前への検査場所
                freeThis();
                Application.Current.MainPage = new Page5();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }

    async void MenuButtonNextClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                //clsGlobalVar.g_KaisouNo = 4;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_LineIndex = GetSelectedLineID();

                clsGlobalVar.g_KensaBashoShousaiID = 0;
                //freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                //Application.Current.MainPage = new Page4();
                clsGlobalVar.g_KaisouNo = 5;
                clsGlobalVar.g_KensaBashoShousaiID = lstKaisou._Datas[1]._KensaBashoShousaiID;//次へ
                freeThis();
                Application.Current.MainPage = new Page5();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    private Color GetPassButtonBColor9(int iPass)
    {
        Color retCol = Colors.LightGray;
        if (iPass == 1)
        {
            retCol = GetBackColorParts();
        }
        else if (iPass == 0)
        {
            retCol = Colors.White;
        }
        else if (iPass == 4)
        {
            retCol = Colors.Red;
        }
        else if (iPass == 5)
        {
            retCol = Colors.Blue;
        }
        return retCol;
    }

    async void UpdButtonClicked(object sender, EventArgs s)
    {
        //アップデート後1階層へ　主に画面種別４，６，７専用
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            int iPass = -1;
            decimal dPara = -999999;
            string strPara = string.Empty;
            string strCombo = string.Empty;
            int iSelectedID = 0;
            if (lstKaisou._Header._GamenKind == 1)
            {

            }
            else if (lstKaisou._Header._GamenKind == 2)
            {
                if (dropdown1.SelectedIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM043, "OK");
                    await DisplayAlert("更新エラー", "ラインが選択されていません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    int iWkNo = 0;
                    foreach (clsLine wLine in lstKaisou._Header._LineLists)
                    {
                        if (iWkNo == dropdown1.SelectedIndex)
                        {
                            iSelectedID = wLine._index;
                            break;
                        }
                        iWkNo++;
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 3)
            {

            }
            else if (lstKaisou._Header._GamenKind == 4)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 5)
            {
                iPass = lstKaisou._Header._iPass;
            }
            else if (lstKaisou._Header._GamenKind == 6)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }
            else if (lstKaisou._Header._GamenKind == 7)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM047, "OK");
                    await DisplayAlert("更新エラー", "文字が入力されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckHankakuChar(txtVal1.Text) == false)
                {
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM048, "OK");
                    await DisplayAlert("更新エラー", "許可されない文字が含まれています。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strPara = ConvStr2Webserver(txtVal1.Text);
                }
            }
            else if (lstKaisou._Header._GamenKind == 8)
            {
                int iIndex = dropdown1.SelectedIndex;
                if (iIndex == -1)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM049, "OK");
                    await DisplayAlert("更新エラー", "選択項目が選択されていません。", "OK");
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    strCombo = lstKaisou._Datas[iIndex]._KouteiID + "-" + lstKaisou._Datas[iIndex]._KouteiShousaiID + "-" + lstKaisou._Datas[iIndex]._KensaBashoID;
                }
            }
            else if (lstKaisou._Header._GamenKind == 9)
            {

            }
            else if (lstKaisou._Header._GamenKind == 20)
            {
                if (string.IsNullOrEmpty(txtVal1.Text) == true)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else if (CheckNumberChar3(txtVal1.Text) == false)
                {
                    //await Navigation.PopAsync();
                    //await DisplayAlert(AppResources.IDM042, AppResources.IDM044, "OK");
                    await DisplayAlert("更新エラー", "入力値が正しくありません。", "OK");
                    txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                    doingNow = false;
                    wBtn.IsEnabled = true;
                    return;
                }
                else
                {
                    try
                    {
                        dPara = decimal.Parse(txtVal1.Text);
                    }
                    catch (Exception)
                    {
                        //throw;
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM045, "OK");
                        await DisplayAlert("更新エラー", "入力値の数値化で例外エラー発生。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                }
                if (dropdown1 != null)
                {
                    if (dropdown1.SelectedIndex == -1)
                    {
                        //await Navigation.PopAsync();
                        //await DisplayAlert(AppResources.IDM042, AppResources.IDM046, "OK");
                        await DisplayAlert("更新エラー", "入力値の指数が選択されていません。", "OK");
                        txtVal1.Text = GetFormatedStrByKeta(lstKaisou._Header._KetaSei, lstKaisou._Header._KetaShou, lstKaisou._Header._dVal);
                        doingNow = false;
                        wBtn.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        int iWkNo = 0;
                        foreach (clsLine wLine in lstKaisou._Header._LineLists)
                        {
                            if (iWkNo == dropdown1.SelectedIndex)
                            {
                                iSelectedID = wLine._index;
                                break;
                            }
                            iWkNo++;
                        }
                    }
                }
            }






            string strErrMsg = "";
            bool bRet = clsWebUpdate.SendResultData(clsGlobalVar.g_UserID, clsGlobalVar.g_SasizuID, clsGlobalVar.g_KouteiID, clsGlobalVar.g_KouteiShousaiID, clsGlobalVar.g_KensaBashoID, clsGlobalVar.g_KensaBashoShousaiID, lstKaisou._Header._KouteiKekkaID, iPass, dPara, strPara, strCombo, iSelectedID, clsGlobalVar.g_KouteiVer, ref strErrMsg);
            if (bRet == false)
            {
                //await Navigation.PopAsync();
                //await DisplayAlert(AppResources.IDM042, strErrMsg, "OK");
                await DisplayAlert("更新エラー", strErrMsg, "OK");
            }
            else
            {
                clsGlobalVar.g_KaisouNo = 4;
                //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
                clsGlobalVar.g_LineIndex = GetSelectedLineID();

                clsGlobalVar.g_KensaBashoShousaiID = 0;
                clsGlobalVar.g_KensaBashoShousaiID = 0;
               freeThis();
                //await Navigation.PushAsync(new Page3(yourData));
                Application.Current.MainPage = new Page4();
                //Application.Current.MainPage = new Page3();
            }
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        Button wBtn = (Button)sender;
        wBtn.IsEnabled = false;
        if (doingNow == false)
        {
            doingNow = true;
            clsGlobalVar.g_KaisouNo = 4;
            //string[] yourData = { _UserID.ToString(), _SasizuNo, _SasizuID.ToString(), _KaisouNo.ToString(), _KouteiID.ToString(), _KouteiShousaiID.ToString(), _KensaBashoID.ToString(), clsGlobalVar.g_svUrl.ToString(), clsGlobalVar.g_language.ToString(), clsGlobalVar.g_logWrite.ToString(), clsGlobalVar.g_urlMsg.ToString(), "0", GetSelectedLineID().ToString() };
            clsGlobalVar.g_LineIndex = GetSelectedLineID();
            freeThis();
            //await Navigation.PushAsync(new Page3(yourData));

            clsGlobalVar.g_KensaBashoShousaiID = 0;
            Application.Current.MainPage = new Page4();
            doingNow = false;
        }
        wBtn.IsEnabled = true;
    }
    async void PassButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            if (lstKaisou._Header._iPass == 1)
            {
                lstKaisou._Header._iPass = 0;
            }
            else if (lstKaisou._Header._iPass == 0)
            {
                lstKaisou._Header._iPass = 2;
            }
            else if (lstKaisou._Header._iPass == 2)
            {
                lstKaisou._Header._iPass = -1;
            }
            else if (lstKaisou._Header._iPass == -1)
            {
                lstKaisou._Header._iPass = 1;
            }
            buttonPass.Text = GetPassButtonStr(lstKaisou._Header._iPass);
            buttonPass.BackgroundColor = GetPassButtonBColor(lstKaisou._Header._iPass);
            buttonPass.TextColor = GetPassButtonTColor(lstKaisou._Header._iPass);
            doingNow = false;
        }
    }
    private string GetPassButtonStr(int iPass)
    {
        //string strRet = "　" + AppResources.IDM059 + "　";
        string strRet = "　" + "未入力" + "　";
        if (iPass == 1)
        {
            //strRet = "　" + AppResources.IDM060 + "　";
            strRet = "　" + "合格" + "　";
        }
        else if (iPass == 0)
        {
            //strRet = "　" + AppResources.IDM061 + "　";
            strRet = "　" + "不合格" + "　";
        }
        else if (iPass == 2)
        {
            //strRet = "　" + AppResources.IDM062 + "　";
            strRet = "　" + "不要" + "　";
        }
        return strRet;
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

    private Color GetPassButtonBColor(int iPass)
    {
        Color retCol = Colors.LightGray;
        if (iPass == 1)
        {
            retCol = GetBackColorParts();
        }
        else if (iPass == 0)
        {
            retCol = Colors.Red;
        }
        else if (iPass == 2)
        {
            retCol = Colors.LightGray;
        }
        return retCol;
    }
    private Color GetPassButtonTColor(int iPass)
    {
        Color retCol = Colors.Black;
        if (iPass == 1)
        {
            retCol = Colors.White;
        }
        else if (iPass == 0)
        {
            retCol = Colors.White;
        }
        else if (iPass == 2)
        {
            retCol = Colors.Black;
        }
        return retCol;
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
            wCol = Colors.Blue;
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
    private Color GetBorderColor(clsKaisou wKaisou)
    {
        Color wCol = Colors.LightGray;
        if (wKaisou._during == 0)
        {
            //進行中
            wCol = Colors.LightGray;
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
            wCol = Colors.Gray;
        }

        return wCol;
    }
    private string GetKetaStr(int iSei, int iShou)
    {
        string strKetaW = string.Empty;
        for (int i = 0; i < iSei; i++)
        {
            strKetaW += "x";
        }
        if (iSei == 0)
        {
            strKetaW += "x";
        }
        if (iShou > 0)
        {
            strKetaW += ".";
            for (int i = 0; i < iShou; i++)
            {
                strKetaW += "x";
            }
        }
        return strKetaW;
    }
    private bool CheckNumberChar3(string strNo)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strNo, "^[-]?[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*$") == true || Regex.IsMatch(strNo, "^[0-9]*.[0-9]*$") == true || Regex.IsMatch(strNo, "^[-]?[0-9]*.[0-9]*$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }

    private string GetFormatedStrByKeta(int iSei, int iShou, decimal dVal)
    {
        string strRet = string.Empty;
        if (dVal == -999999)
        {

        }
        else
        {
            string strF = "F" + iShou.ToString();
            strRet = dVal.ToString(strF);
        }
        return strRet;
    }
    private int GetCurSelectedDropDown(string strVal)
    {
        int iRet = -1;
        if (string.IsNullOrEmpty(strVal) == false)
        {
            string wStr = strVal;
            int iIndex = 0;
            int wKouteiID = -1;
            int wKouteiShousaiID = -1;
            int wKensaBashoID = -1;

            while (wStr.Length > 0)
            {
                int iNo1 = wStr.IndexOf("-");
                string strW2;
                if (iNo1 > -1)
                {
                    strW2 = wStr.Substring(0, iNo1).Trim();
                    wStr = wStr.Substring(iNo1 + 1);
                }
                else
                {
                    strW2 = wStr.Trim();
                    wStr = "";
                }
                if (string.IsNullOrEmpty(strW2) == false)
                {
                    if (iIndex == 0)
                    {
                        wKouteiID = int.Parse(strW2);
                    }
                    else if (iIndex == 1)
                    {
                        wKouteiShousaiID = int.Parse(strW2);
                    }
                    else if (iIndex == 2)
                    {
                        wKensaBashoID = int.Parse(strW2);
                    }
                    else
                    {
                        break;
                    }
                }
                iIndex += 1;
            }
            if (wKouteiID > -1)
            {
                iIndex = 0;
                foreach (clsKaisou wKaisou in lstKaisou._Datas)
                {
                    if (wKaisou._KouteiID == wKouteiID && wKaisou._KouteiShousaiID == wKouteiShousaiID && wKaisou._KensaBashoID == wKensaBashoID)
                    {
                        iRet = iIndex;
                        break;
                    }
                    iIndex++;
                }
            }
        }
        return iRet;
    }
    private void freeThis()
    {
        Console.WriteLine("Page4 free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
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

        if (Lstbutton != null)
        {
            int imax = Lstbutton.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstbutton[i].Clicked -= ItemButtonClicked;
                Lstbutton[i] = null;
            }
            Lstbutton.Clear();
            Lstbutton = null;
        }
        label1 = null;
        label2 = null;
        label3 = null;
        label4 = null;
        label5 = null;
        label6 = null;
        if (dropdown1 != null)
        {
            dropdown1.Items.Clear();
            dropdown1 = null;
        }
        txtVal1 = null;

        if (buttonnext != null)
        {
            buttonnext.Clicked -= MenuButtonNextClicked;
            buttonnext.ImageSource = null;
            buttonnext = null;
        }
        if (buttonprev != null)
        {
            buttonprev.Clicked -= MenuButtonPrevClicked;
            buttonprev.ImageSource = null;
            buttonprev = null;
        }
        labelDummy = null;
        layout20 = null;


        if (actIndOCR != null)
        {
            actIndOCR = null;
        }
        if (buttonPass != null)
        {
            if (lstKaisou._Header._done == 0)
                buttonPass.Clicked -= PassButtonClicked;
            buttonPass = null;
        }
        if (buttonUpd != null)
        {
            if (lstKaisou._Header._done == 0)
                buttonUpd.Clicked -= UpdButtonClicked;
            buttonUpd = null;
        }
        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        if (imgView != null)
        {
            imgView.Source = null;
            imgView = null;
        }
        layout1 = null;
        absLay = null;
        sv = null;
        Content = null;
        if (lstKaisou != null)
        {
            lstKaisou.freeThis();
            lstKaisou = null;
        }
        GC.Collect();
        Console.WriteLine("Page4 free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }
    private bool CheckHankakuChar(string strW)
    {
        //return true;
        bool bRet = true;
        if (Regex.IsMatch(strW, "^[a-zA-Z0-9!-/:-@[-`{-~]+$") == true)
        {
            bRet = true;
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }
    private string ConvStr2Disp(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListFrom = { "＃", "＆", "？", "％", "￥", "／" };
        string[] strsListTo = { "#", "&", "?", "%", "\\", "/" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }
    private string ConvStr2Webserver(string strW)
    {
        //return true;
        string strRet = strW;
        string[] strsListTo = { "＃", "＆", "？", "％", "￥", "／" };
        string[] strsListFrom = { "#", "&", "?", "%", "\\", "/" };
        int i = 0;
        foreach (string strW2 in strsListFrom)
        {
            if (strRet.IndexOf(strW2) > -1)
            {
                strRet = strRet.Replace(strW2, strsListTo[i]);
            }
            i++;
        }
        return strRet;
    }
    private int GetSelectedLineID()
    {
        int iRet = -1;
        if (lstKaisou._Header._GamenKind == 2 && (dropdown1 != null) && lstKaisou._Header._done == 0)
        {
            iRet = dropdown1.SelectedIndex;
        }
        else
        {
            iRet = clsGlobalVar.g_LineIndex;
        }
        return iRet;
    }

}