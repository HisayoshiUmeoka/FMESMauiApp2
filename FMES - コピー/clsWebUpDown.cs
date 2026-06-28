using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FMES
{
    class clsWebUpDown
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
                    if (aborted== true)
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
                        if (html.IndexOf("<!-- ロゴ -->") == -1)
                        {
                            //OKの場合
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

                    var response = client.GetAsync(wUrl).Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                err_message = ex.ToString();

                System.Diagnostics.Debug.WriteLine(
                    "GetWebResponce Error=" + ex.ToString());

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
