using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.NetSqlAzMan.Helper
{
    public class GlobalVars
    {
        private static String _SqlConnectionString = String.Empty;
        public static String SqlConnectionString
        {
            get { return _SqlConnectionString; }
            set { _SqlConnectionString = value; }
        }

        private static String _Theme = "light";
        public static String Theme
        {
            get { return _Theme; }
            set { _Theme = value; }
        }
    }
}
