using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VWG.Community.NetSqlAzMan.Helper
{
    public class Enums
    {
        public enum AzManItemType
        {
            Storage,
            Store,
            StoreGroups,
            StoreGroup,
            Application,
            ApplicationGroups,
            ApplicationGroup,
            ItemDefinitions,
            ItemAuthorizations,
            RoleDefinition,
            RoleDefinitions,
            RoleAuthorization,
            RoleAuthorizations,
            TaskDefinition,
            TaskDefinitions,
            TaskAuthorization,
            TaskAuthorizations,
            OperationDefinition,
            OperationDefinitions,
            OperationAuthorization,
            OperationAuthorizations
        }

        /// <summary>
        /// CRUD Modes
        /// </summary>
        public enum Mode
        {
            Create,
            Read,
            Update,
            Delete
        }
    }
}