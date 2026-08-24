using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace FMESSignage;

public partial class Page1 : ContentPage
{
    // ↓added for popupmeneu
    private Label labelUser;
    private Button buttonMenu;
    private HorizontalStackLayout ContentMenu;
    // ↑added for popupmeneu
    private clsSiteList lstSite = new clsSiteList();

    private Label label1;
    private List<StackLayout> Lstlayout = new List<StackLayout>();
    private List<Button> Lstbutton = new List<Button>();
    private List<Label> LstTime = new List<Label>();
    private StackLayout layout1;
    private Button buttonEnd;
    private ScrollView sv;

    private bool doingNow = false;
    public Page1()
    {
        InitializeComponent();
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

        //AppResources.Culture = new System.Globalization.CultureInfo(clsGlobalVar.GetLanguageSetting());
        clsGlobalVar.g_NowForm = 3;
        // ↓added for popupmeneu

        labelUser = new Label
        {
            //Text = clsGlobalVar.g_Operator,
            Text = "　　 ",
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
            HorizontalOptions = LayoutOptions.Start,
            //            BackgroundColor = Color.FromArgb("#D1D5DB"),
            BackgroundColor = Colors.Transparent,          // ← 透過に変更
            Children = {
                        labelUser,
                        buttonMenu,
                    }
        };
        // ↑added for popupmeneu

        string srtErrMsg = string.Empty;
        //clsSiteList lstSite = new clsSiteList();
        if (lstSite.GetList(clsGlobalVar.g_CompanyID, clsGlobalVar.g_CompanyPW, ref srtErrMsg) == true)
        {

            layout1 = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
            };
            layout1.Children.Add(ContentMenu);
            foreach (clsSiteData wSiteData in lstSite._Datas)
            {
                Button butn = new Button
                {
                    Text = wSiteData._Title,
                    FontSize = 14,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.LightGreen,
                    //BorderColor = Colors.LightGreen,
                    BorderColor = Colors.LightGray,
                    BorderWidth = 1.5,
                    HeightRequest = 48,
                    CornerRadius = 12,
                    Margin = new Thickness(20, 0, 20, 12),
                    HorizontalOptions = LayoutOptions.Fill,
                };
                butn.Clicked += ItemButtonClicked;
                Lstbutton.Add(butn);
                //LstTime.Add(lbW);
                layout1.Children.Add(butn);
            }





            string wEnd;

            //wEnd = AppResources.IDM032;
            wEnd = "戻る";
            buttonEnd = new Button
            {
                Text = wEnd,
                //VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.LightGreen,
                //BorderColor = Colors.LightGreen,
                FontSize = 14,
                    TextColor = Colors.Black,
                BorderColor = Colors.LightGray,
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12),
                HorizontalOptions = LayoutOptions.Fill,
            };
            buttonEnd.Clicked += EndButtonClicked;
            layout1.Children.Add(buttonEnd);

            sv = new ScrollView { Content = layout1 };
            Content = sv;
        }
        else
        {
            label1 = new Label
            {
                //Text = "　" + AppResources.IDM027,
                Text = "　" + "データエラー（又は通信エラー）",
                BackgroundColor = Colors.Transparent,          // ← 透過に変更
                //BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 22,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            buttonEnd = new Button
            {
                //Text = AppResources.IDM026,
                Text = "場所変更",
                FontSize = 14,
                TextColor = Colors.Black,
                BorderColor = Colors.LightGray,
                BorderWidth = 1.5,
                HeightRequest = 48,
                CornerRadius = 12,
                Margin = new Thickness(20, 0, 20, 12),
                HorizontalOptions = LayoutOptions.Fill,
            };
            buttonEnd.Clicked += EndButtonClicked;
            Content = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = Colors.White,
                Children = {
                label1,
                    //buttonMaching,
                    buttonEnd,
                    }
            };
        }
    }
    void ItemButtonClicked(object sender, EventArgs s)
    {
        int i = 0;
        if (doingNow == false)
        {
            doingNow = true;
            foreach (Button wBtn in Lstbutton)
            {
                if (wBtn.GetHashCode() == sender.GetHashCode())
                {
                    //実行可能な物
                    //string[] yourData = { _UserID.ToString(), cnf._svUrl.ToString(), cnf._language.ToString(), cnf._logWrite.ToString(), cnf._urlMsg.ToString() };
                    clsGlobalVar.g_SiteID = lstSite._Datas[i]._URLID;
                    freeThis();
                    //await Navigation.PushAsync(new Page2(yourData));

                    Application.Current.MainPage = new PageWeb();
                    break;
                }
                i++;
            }
            doingNow = false;
        }
    }

    // ↓added for popupmeneu
    async void MenuButtonClicked(object sender, EventArgs s)
    {
        clsGlobalVar.g_BackPage = "Page1";
        freeThis();

        Application.Current.MainPage = new Pagepopupmenu();
    }
    // ↑added for popupmeneu
    private string GetDispTime(int iTotalSec)
    {
        string strRet = string.Empty;
        int hh = iTotalSec / 3600;
        int mm = (iTotalSec - (hh * 3600)) / 60;
        int ss = (iTotalSec - (hh * 3600) - (mm * 60));
        strRet = string.Format("（{0:D2}:{1:D2}:{2:D2}）", hh, mm, ss);
        return strRet;
    }

    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            Application.Current.MainPage =new MainPage();
            doingNow = false;
        }
    }
    private void freeThis()
    {
        Console.WriteLine("Page1 free before GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
        if (label1 != null)
        {
            label1 = null;
        }
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
        if (LstTime != null)
        {
            int imax = LstTime.Count;
            for (int i = 0; i < imax; i++)
            {
                LstTime[i] = null;
            }
            LstTime.Clear();
            LstTime = null;
        }
        if (Lstlayout != null)
        {
            int imax = Lstlayout.Count;
            for (int i = 0; i < imax; i++)
            {
                Lstlayout[i] = null;
            }
            Lstlayout.Clear();
            Lstlayout = null;
        }

        if (buttonEnd != null)
        {
            buttonEnd.Clicked -= EndButtonClicked;
            buttonEnd = null;
        }
        layout1 = null;
        sv = null;
        if (lstSite != null)
        {
            lstSite.freeThis();
            lstSite = null;
        }

        GC.Collect();
        Console.WriteLine("Page1 free after GC.GetTotalMemory:" + GC.GetTotalMemory(true).ToString());
    }

}