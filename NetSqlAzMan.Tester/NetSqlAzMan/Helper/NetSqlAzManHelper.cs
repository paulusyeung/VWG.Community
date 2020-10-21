using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetSqlAzMan.Tester.NetSqlAzMan.Helper
{
    public class NetSqlAzManHelper
    {
        public static String getName(string source, int index)
        {
            String result = "";

            var list = source.Trim().Split(' ');
            result = list[index].Trim();

            return result;
        }
    }
}