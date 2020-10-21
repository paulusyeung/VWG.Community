using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VWG.Community.NetSqlAzMan.Helper
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

        public static String GetMemberTypeName(MemberType memberType, IAzManSid sid, IAzManItem item)
        {
            switch (memberType)
            {
                case MemberType.StoreGroup:
                    if (item.Application.Store.GetStoreGroup(sid).GroupType == GroupType.Basic)
                        return "Store Group";
                    else
                        return "LDAP Group";
                case MemberType.ApplicationGroup:
                    if (item.Application.GetApplicationGroup(sid).GroupType == GroupType.Basic)
                        return "Application Group";
                    else
                        return "LDAP Group";
                case MemberType.WindowsNTUser:
                    return "Windows User";
                case MemberType.WindowsNTGroup:
                    return "Windows Basic Group";
                case MemberType.DatabaseUser:
                    return "DB User";
                default:
                case MemberType.AnonymousSID:
                    return "SID Not Found";
            }
        }

        public static String GetAuthTypeName(AuthorizationType authorizationType)
        {
            switch (authorizationType)
            {
                case AuthorizationType.AllowWithDelegation:
                    return "Allow for Delegation";
                case AuthorizationType.Allow:
                    return "Allow";
                case AuthorizationType.Deny:
                    return "Deny";
                default:
                case AuthorizationType.Neutral:
                    return "Neutral";
            }
        }

        public static String GetWhereDefinedName(WhereDefined where)
        {
            switch (where)
            {
                case WhereDefined.Database:
                    return "DB User";
                case WhereDefined.LDAP:
                    return "LDAP User";
                case WhereDefined.Store:
                    return "Store User";
                case WhereDefined.Application:
                    return "Application User";
                case WhereDefined.Local:
                default:
                    return "Local User";

            }
        }
    }
}