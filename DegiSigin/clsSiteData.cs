using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMESSignage
{
    class clsSiteData
    {
        public int _URLID = 0;
        public string _Title = string.Empty;
        public clsSiteData(string wStr)
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
                        _URLID = int.Parse(strW2);
                    }
                    else if (iIndex == 1)
                    {
                        _Title = strW2;
                    }
                    else
                    {
                        break;
                    }
                }
                iIndex += 1;
            }


        }
        public void freeThis()
        {
            _URLID = 0;
            _Title = string.Empty;
        }
    }
}
