using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
//using Windows.System;

namespace FMESSignage;

public partial class PageWeb : ContentPage
{
    private clsURLList lstURL = new clsURLList();
    private Label label1;
    private WebView webB;
    private Button buttonEnd;
    private bool doingNow = false;
    private ScrollView sv;
    private StackLayout layout1;
    private int CurURLindex = 0;
    private int changeSec = 0;

    public PageWeb()
	{
        //InitializeComponent();
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

        string srtErrMsg = string.Empty;
        string wURL = string.Empty;
        lstURL = new clsURLList();
        if (lstURL.GetList(clsGlobalVar.g_SiteID, ref srtErrMsg) == true)
        {
            if (lstURL.GetCount() > 0)
            {
                //                    stoptimer = false;

                wURL = lstURL._Datas[CurURLindex]._wurl;
                //totaltime = 0;
                changeSec = lstURL._Datas[CurURLindex]._interval;

            }
            else
            {
                //DisplayAlert(AppResources.IDM027, srtErrMsg, "OK");
                DisplayAlert("データエラー（又は通信エラー）", srtErrMsg, "OK");
            }
        }
        else
        {
            //await DisplayAlert(AppResources.IDM027, srtErrMsg, "OK");
            //DisplayAlert(AppResources.IDM027, srtErrMsg, "OK");

            DisplayAlert("データエラー（又は通信エラー）", srtErrMsg, "OK");
        }

        clsGlobalVar.g_NowForm = 11;













        //string wURL = "https://fmes.5ms.cloud/users/productionorderplaninstructreport";
        webB = new WebView
        {
            //Source = wURL,
            Source = wURL,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
        };

        buttonEnd = new Button
        {
            Text = "戻る",
            FontSize = 14,
            BorderColor = Colors.LightGray,
            BorderWidth = 1.5,
            HeightRequest = 48,
            CornerRadius = 12,
            Margin = new Thickness(20, 0, 20, 12),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Fill,
            TextColor = Colors.Black,
            BackgroundColor = Colors.LightGreen,
        };
        buttonEnd.Clicked += EndButtonClicked;
        // Grid をルートにして、WebView を * 行に配置することで確実に領域が確保される
        var rootGrid = new Grid
        {
            RowDefinitions = {
                    new RowDefinition(GridLength.Star), // WebView がここで伸びる
                    new RowDefinition(GridLength.Auto)  // ボタン等は自動高さ
                }
        };

        rootGrid.Add(webB, 0, 0);
        //rootGrid.Add(buttonEnd, 0, 1);

        Content = rootGrid;

        webB.Navigated += webviewNavigated;
        webB.Navigating += webviewNavigating;
    }

    void webviewNavigating(object sender, WebNavigatingEventArgs e)
    {
    }

    void webviewNavigated(object sender, WebNavigatedEventArgs e)
    {
        StartTimerAsync();
    }
    async void EndButtonClicked(object sender, EventArgs s)
    {
        if (doingNow == false)
        {
            doingNow = true;
            //                stoptimer = true;

            //freeThis();
            //await Navigation.PushAsync(new SashizuPage(yourData));
            Application.Current.MainPage = new Page1();
            doingNow = false;
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



                if (strRet2 == "OK")
                {
                    iRet = 1;
                    //clsGlobalVar.g_Parmit = 1;
                    if (strRet2 == "OK2")
                    {
                        iRet = 2;
                        //clsGlobalVar.g_Parmit = 2;
                    }
                }
                else if (strRet2 == "NG")
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

    private async void StartTimerAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(changeSec));

        string wURL = string.Empty;
        CurURLindex++;
        if (lstURL._Datas.Count > CurURLindex)
        {
            wURL = lstURL._Datas[CurURLindex]._wurl;
            changeSec = lstURL._Datas[CurURLindex]._interval;
        }
        else
        {
            //セッションキープ用にログインコマンドを発行する
            string ex_message = string.Empty;
            string strErrMsg = ""; ;
            int iRetLogin = 1;
            //定期送信の廃止
            // iRetLogin =GetLoginVerify(clsGlobalVar.g_CompanyID, clsGlobalVar.g_CompanyPW, ref ex_message);
            if (iRetLogin > 0)
            {
                //Application.Current.MainPage = new Page1();
            }
            else if (iRetLogin == 0)
            {
                await DisplayAlert("ログインエラー", "ログインID又はパスワードを確認してください", "OK");
                Application.Current.MainPage = new MainPage();
                return;
            }
            else if (iRetLogin == -1)
            {
                await DisplayAlert("環境設定エラー", "サーバURLを確認してください", "OK");
                Application.Current.MainPage = new MainPage();
                return;

            }
            else if (iRetLogin == -2)
            {
                await DisplayAlert("ログインエラー", ex_message, "OK");
                Application.Current.MainPage = new MainPage();
                return;

            }





            CurURLindex = 0;
            wURL = lstURL._Datas[CurURLindex]._wurl;
            changeSec = lstURL._Datas[CurURLindex]._interval;
        }
        if (webB != null)
        {
            webB.Source = wURL;
        }
    }
}

