using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.NetSqlAzMan.Helper
{
    public class Config
    {
        public static string OutBox
        {
            get
            {
                string result = @"C:\Temp\OutBox";

                if (ConfigurationManager.AppSettings["OutBox"] != null)
                {
                    result = (string)ConfigurationManager.AppSettings["OutBox"];
                    if (!(Directory.Exists(result)))
                    {
                        Directory.CreateDirectory(result);
                    }
                }

                return result;
            }
        }
    }
}
