using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMESSignage
{
    class clsURLList
    {
        public List<clsURLData> _Datas = new List<clsURLData>();
        public clsURLList()
        {
            _Datas = new List<clsURLData>();
        }
        public bool GetList(int wUrlID, ref string srtErrMsg)
        {
            bool bRet = false;
            int iRow = 0;
            string wURL = clsGlobalVar.GetCurURL() + "users/tabsignage/" + wUrlID;
            string strErr = null;
            SendLog(wURL, ref strErr);
            string strRet = clsWebUpDown.GetWebResponce(wURL);
            if (string.IsNullOrEmpty(strRet) == false)
            {
                if (strRet.IndexOf("NG:") == -1)
                {
                    strRet = strRet.Replace("<!--ƒ_ƒ~[ -->", "");
                    strRet = strRet.Replace("\r", "");
                    strRet = strRet.Replace("\n", "");

                    List<string> lstRec = GetRecStart(strRet);
                    foreach (string wStr in lstRec)
                    {
                        if (iRow == 0)
                        {
                            if (wStr == "OK")
                            {
                                //‚È‚É‚à‚µ‚È‚¢
                            }
                            else
                            {
                                clsURLData wSiteData = new clsURLData(wStr);
                                _Datas.Add(wSiteData);

                            }
                        }
                        else
                        {
                            clsURLData wSiteData = new clsURLData(wStr);
                            _Datas.Add(wSiteData);
                        }
                        iRow += 1;
                    }
                    lstRec.Clear();
                    lstRec = null;
                    bRet = true;
                }
                else
                {
                    bRet = false;
                    srtErrMsg = clsErrorMessage.GetErrMsg(strRet);
                }
            }
            return bRet;
        }
        private List<string> GetRecStart(string strW)
        {
            List<string> lstRec = new List<string>();
            string strLeft;
            int iNo = strW.IndexOf("</div>");
            if (iNo > -1)
            {
                strLeft = strW.Substring(iNo + 6);
                while (strLeft.Length > 0)
                {
                    int iNo2 = strLeft.IndexOf("<br />");
                    if (iNo2 < -1)
                    {
                        iNo2 = strLeft.IndexOf("\r\n");
                    }
                    string strW2 = string.Empty;
                    if (iNo2 > -1)
                    {
                        strW2 = strLeft.Substring(0, iNo2).Trim();
                        strLeft = strLeft.Substring(iNo2 + 6);
                    }
                    else
                    {
                        strW2 = strLeft.Trim();
                        strLeft = "";
                    }
                    if (strW2.Length > 0)
                    {
                        lstRec.Add(strW2);
                    }
                }
            }
            else
            {
                iNo = 0;
                strLeft = strW.Substring(iNo);
                while (strLeft.Length > 0)
                {
                    int iNo2 = strLeft.IndexOf("<br />");
                    if (iNo2 < -1)
                    {
                        iNo2 = strLeft.IndexOf("\r\n");
                    }
                    string strW2 = string.Empty;
                    if (iNo2 > -1)
                    {
                        strW2 = strLeft.Substring(0, iNo2).Trim();
                        strLeft = strLeft.Substring(iNo2 + 6);
                    }
                    else
                    {
                        strW2 = strLeft.Trim();
                        strLeft = "";
                    }
                    if (strW2.Length > 0)
                    {
                        lstRec.Add(strW2);
                    }
                }

            }
            return lstRec;
        }
        public int GetCount()
        {
            int iret = 0;
            if (_Datas != null)
            {
                iret = _Datas.Count;
            }
            return (iret);
        }

        public void freeThis()
        {
            if (_Datas != null)
            {
                int imax = _Datas.Count;
                for (int i = 0; i < imax; i++)
                {
                    _Datas[i].freeThis();
                    _Datas[i] = null;
                }
                _Datas.Clear();
                _Datas = null;
            }
        }
        public static string ConvURL2Log(string strLog)
        {
            strLog = strLog.Replace("http://", "");
            strLog = strLog.Replace("https://", "");
            strLog = strLog.Replace("/", "ybbz");
            return strLog;
        }
        public static string ConvURL2baseURL(string strLog)
        {
            strLog = strLog.Replace("^", "/");
            //strLog = strLog.Replace("/", "ybbz");
            return strLog;
        }


        public static bool SendLog(string strLog, ref string strErr)
        {
            bool bRet = true;
            if (clsGlobalVar.g_logWrite == 1)
            {

                strLog = ConvURL2Log(strLog);
                string wURL = clsGlobalVar.GetCurURL() + "users/tabaccesslog/" + strLog;
                //_QueURL = wURL;
                string strRet = clsWebUpDown.GetWebResponce(wURL);
                if (string.IsNullOrEmpty(strRet) == false)
                {
                    if (strRet.IndexOf("OK") > -1)
                    {

                    }
                    else if (strRet.IndexOf("NG") > -1)
                    {
                        bRet = false;
                        strErr = clsErrorMessage.GetErrMsg(strRet);
                    }
                }
            }
            return bRet;
        }


    }
}
