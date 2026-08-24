using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FMESSignage
{
    internal class clsWebUpDown
    {
        public static bool aborted = false;
        public static string err_message = string.Empty;
        public static string GetWebResponce(string wUrl)
        {
            string html = string.Empty;
            if (string.IsNullOrEmpty(wUrl) == false)
            {
                {
                    Task.Delay(500);
                    aborted = false;
                    err_message = string.Empty;
                    html = GetWebResponce2(wUrl);
                    if (aborted == true)
                    {
                        //aborted = false;
                        html = GetWebResponceKep(wUrl);
                    }
                }
            }
            return (html);
        }
        public static string GetWebResponcekeepuwp(string wUrl)
        {
            string html = string.Empty;
            if (string.IsNullOrEmpty(wUrl) == false)
            {
                for (int iRetry = 0; iRetry < 2; iRetry++)
                {
                    html = GetWebResponce2(wUrl);
                    if (string.IsNullOrEmpty(html) == false)
                    {
                        if (html.IndexOf("<!-- ÉçÉS -->") == -1)
                        {
                            //OKÇÃèÍçá
                            break;
                        }
                        else
                        {
                            html = string.Empty;
                        }
                    }
                }
            }
            return (html);
        }
        public static string GetWebResponce2(string wUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    if (string.IsNullOrEmpty(clsGlobalVar.g_AccessToken) == false)
                    {
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", clsGlobalVar.g_AccessToken);
                    }

                    var response = client.GetAsync(wUrl).Result;
                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                err_message = ex.ToString();
                aborted = true;
                return string.Empty;
            }
        }
        public static string GetWebResponceKep(string wUrl)
        {
            string html = String.Empty;
            int iRetry = 0;
            if (string.IsNullOrEmpty(wUrl) == false)
            {
                WebClient wClient = new WebClient();
                try
                {
                    //RetryHere:
                    System.IO.Stream stream = wClient.OpenRead(wUrl);
                    StreamReader red = new StreamReader(stream);
                    html = red.ReadToEnd();
                    red.Close();
                    stream.Close();
                    stream.Dispose();
                }
                catch (Exception)
                {
                    html = String.Empty;
                    // throw;
                }
                finally
                {
                    if (wClient != null)
                    {
                        wClient.Dispose();
                        wClient = null;
                    }
                }
            }

            return (html);
        }


        public static bool GetImageFile(string wUrl, string wSave)
        {
            bool bRet = false;
            WebClient wClient = new WebClient();
            try
            {
                wClient.DownloadFile(wUrl, wSave);
                bRet = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (wClient != null)
                {
                    wClient.Dispose();
                    wClient = null;
                }
            }
            return bRet;
        }
    }
}
