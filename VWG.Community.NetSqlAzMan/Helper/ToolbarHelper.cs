using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VWG.Community.NetSqlAzMan.Helper
{
    public class ToolbarHelper
    {
        public static void AppendMenuItem_AppViews(ref ContextMenu ddlViews)
        {
            ddlViews.MenuItems.Add(new MenuItem(("Icon View"), string.Empty, "Icon"));
            ddlViews.MenuItems.Add(new MenuItem(("Tile View"), string.Empty, "Tile"));
            ddlViews.MenuItems.Add(new MenuItem(("List View"), string.Empty, "List"));
            ddlViews.MenuItems.Add(new MenuItem(("Details View"), string.Empty, "Details"));

            //ddlViews.MenuItems[0].Icon = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-view-grid-outline.png");
            //ddlViews.MenuItems[1].Icon = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-view-list.png");
            //ddlViews.MenuItems[2].Icon = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-view-column-outline.png");
            //ddlViews.MenuItems[3].Icon = new IconResourceHandle(GlobalVars.Theme + ".24.mdi-view-sequential-outline.png");
        }

        public static void AppendMenuItem_AppPref(ref ContextMenu ddlViews)
        {
            ddlViews.MenuItems.Add(new MenuItem(("Save"), string.Empty, "Save"));
            ddlViews.MenuItems.Add(new MenuItem(("Reset"), string.Empty, "Reset"));

            //ddlViews.MenuItems[0].Icon = new IconResourceHandle(GlobalVars.Theme + ".16.application_add.png");
            //ddlViews.MenuItems[1].Icon = new IconResourceHandle(GlobalVars.Theme + ".16.application_delete.png");
        }
    }
}