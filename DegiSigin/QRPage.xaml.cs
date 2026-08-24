using System.Formats.Tar;
using ZXing.Net.Maui;


namespace FMESSignage;

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

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        // ページがロードされた時の処理
        //DisplayAlert("Loaded", "ページが表示されました", "OK");
        _QRReader.WidthRequest = this.Width;
        _QRReader.HeightRequest = this.Height;
        _QRReader.IsVisible = true;
    }

    private void GoBackQR()
    {

        if (clsGlobalVar.g_BackPage == "MainPage")
        {

            Application.Current.MainPage = new MainPage();
        }
        else if (clsGlobalVar.g_BackPage == "configPage")
        {
            Application.Current.MainPage = new configPage();
        }
        else if (clsGlobalVar.g_BackPage == "Page1")
        {
            Application.Current.MainPage = new Page1();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            Application.Current.MainPage = new PageWeb();
        }
        else if (clsGlobalVar.g_BackPage == "PageWeb")
        {
            Application.Current.MainPage = new PageWeb();
        }
        else
        {
            //バグの場合ここに来る
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


