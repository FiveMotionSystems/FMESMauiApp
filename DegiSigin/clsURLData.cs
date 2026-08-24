using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMESSignage
{
    class clsURLData
    {
        public int _interval = 0;
        public string _wurl = string.Empty;
        public clsURLData(string wStr)
        {
            int iIndex = 0;
            //int wPosX = 0;
            while (wStr.Length > 0)
            {
                int iNo1 = wStr.IndexOf(",");
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
                        _wurl = ConvURL2baseURL(strW2);
                    }
                    else if (iIndex == 1)
                    {
                        _interval = int.Parse(strW2);
                    }
                    else
                    {
                        break;
                    }
                }
                iIndex += 1;
            }
        }
        public static string ConvURL2Log(string strLog)
        {
            strLog = strLog.Replace("http://", "");
            strLog = strLog.Replace("https://", "");
            strLog = strLog.Replace("/", "ÅyÅbÅbÅz");
            return strLog;
        }
        public static string ConvURL2baseURL(string strLog)
        {
            strLog = strLog.Replace("Å^", "/");
            //strLog = strLog.Replace("/", "ÅyÅbÅbÅz");
            return strLog;
        }

        public void freeThis()
        {
            _interval = 0;
            _wurl = string.Empty;
        }


    }
}
