using Gizmox.WebGUI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetSqlAzMan.Tester.NetSqlAzMan.Helper
{
    public class ActionMenu
    {
        /// <summary>
        /// 原本打算用嚟整右邊嘅 Action Menu，依家決定用 Toolbar
        /// </summary>
        public class ActionMenuItem
        {
            private int _id;
            private Object _tag;
            private string _text;
            private ResourceHandle _icon;

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public Object Tag
            {
                get { return _tag; }
                set { _tag = value; }
            }

            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            public ResourceHandle Icon
            {
                get { return _icon; }
                set { _icon = value; }
            }

            public ActionMenuItem(int id, Object tag, string text)
            {
                ID = id;
                Tag = tag;
                Text = text;
            }

            public ActionMenuItem(int id, string text, Object tag, ResourceHandle icon)
            {
                Icon = icon;
                ID = id;
                Tag = tag;
                Text = text;
            }
        }
    }
}