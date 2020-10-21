using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Xml.Serialization;

namespace NetSqlAzMan.Tester.NetSqlAzMan.Helper
{
    public class ListViewHelper
    {
        public class DisplayPreference
        {
            public static void Save(ListView lvwList)
            {
                /*
                // 把每個 ColumnHeader 的資料保存在 MetadataXml 中
                String sql = String.Format("UserSid = '{0}'", Common.Config.CurrentUserId.ToString());
                xPort5.DAL.UserProfile user = xPort5.DAL.UserProfile.LoadWhere(sql);
                if (user != null)
                {
                    sql = String.Format("UserId = '{0}' AND PreferenceObjectId = '{1}'", user.UserId.ToString(), ((Guid)lvwList.Tag).ToString());

                    UserDisplayPreference userPref = UserDisplayPreference.LoadWhere(sql);

                    if (userPref == null)
                    {
                        userPref = new UserDisplayPreference();
                        userPref.UserId = user.UserId;
                        userPref.PreferenceObjectId = (Guid)lvwList.Tag;
                    }

                    userPref.MetadataXml = new Dictionary<string, UserDisplayPreference.MetadataAttributes>();     // 首先清空舊的 Metadata.

                    foreach (ColumnHeader col in lvwList.Columns)
                    {
                        UserDisplayPreference.MetadataAttributes attrs = new UserDisplayPreference.MetadataAttributes();

                        attrs.Add(new UserDisplayPreference.MetadataAttribute("Name", col.Name));
                        //attrs.Add(new UserDisplayPreference.MetadataAttribute("Position", col.Position.ToString()));
                        attrs.Add(new UserDisplayPreference.MetadataAttribute("SortOrder", col.SortOrder.ToString()));
                        attrs.Add(new UserDisplayPreference.MetadataAttribute("SortPosition", col.SortPosition.ToString()));
                        attrs.Add(new UserDisplayPreference.MetadataAttribute("Text", col.Text));                  // 為咗方便睇 SQL 紀錄，Text 會 save 不過不需要 load
                        attrs.Add(new UserDisplayPreference.MetadataAttribute("Visible", col.Visible.ToString()));
                        attrs.Add(new UserDisplayPreference.MetadataAttribute("Width", col.Width.ToString()));
                        if (col.Image != null)
                            attrs.Add(new UserDisplayPreference.MetadataAttribute("ImageFile", col.Image.File));
                        else
                            attrs.Add(new UserDisplayPreference.MetadataAttribute("ImageFile", String.Empty));

                        userPref.SetMetadata(col.Index.ToString(), attrs);                                  // 採用 ColumnHeader.Index 作為 key
                    }

                    userPref.Save();
                }
                */
            }

            public static void Delete(ListView lvwList)
            {
                //String sql = String.Format("UserSid = '{0}'", Common.Config.CurrentUserId.ToString());
                //xPort5.DAL.UserProfile user = xPort5.DAL.UserProfile.LoadWhere(sql);
                //if (user != null)
                //{
                //    sql = String.Format("UserId = '{0}' AND PreferenceObjectId = '{1}'", user.UserId.ToString(), ((Guid)lvwList.Tag).ToString());

                //    UserDisplayPreference userPref = UserDisplayPreference.LoadWhere(sql);

                //    if (userPref != null)
                //    {
                //        userPref.Delete();
                //    }
                //}
            }

            public static void Load(ref ListView lvwList)
            {
                /*
                String sql = String.Format("UserSid = '{0}'", Common.Config.CurrentUserId.ToString());
                xPort5.DAL.UserProfile user = xPort5.DAL.UserProfile.LoadWhere(sql);
                if (user != null)
                {
                    // 2012.04.18 paulus:
                    // 首先用 SuperUser 個 Id 試下，搵唔到才用自己個 Id，於是 SuperUser 可以設定 ListView 的 Layout 給所有用戶
                    sql = String.Format("UserId = '{0}' AND PreferenceObjectId = '{1}'", xPort5.Controls.Utility.Staff.GetSuperUserId().ToString(), ((Guid)lvwList.Tag).ToString());
                    UserDisplayPreference userPref = UserDisplayPreference.LoadWhere(sql);
                    if (userPref == null)
                    {
                        sql = String.Format("UserId = '{0}' AND PreferenceObjectId = '{1}'", user.UserId.ToString(), ((Guid)lvwList.Tag).ToString());
                        userPref = UserDisplayPreference.LoadWhere(sql);
                    }

                    #region 搵到就根據 UserDisplayPreference 的資料更改 ColumnHeader
                    if (userPref != null)
                    {
                        Dictionary<string, xPort5.DAL.UserDisplayPreference.MetadataAttributes> metadata = userPref.MetadataXml;
                        foreach (KeyValuePair<string, xPort5.DAL.UserDisplayPreference.MetadataAttributes> col in metadata)
                        {
                            int colIndex = int.Parse(col.Key);      // col.Key 等於 ColumnHeader.Index

                            foreach (xPort5.DAL.UserDisplayPreference.MetadataAttribute item in col.Value)
                            {
                                int position = 0, sortPosition = 0, width = 0;
                                bool visible = false;

                                switch (item.Key)
                                {
                                    case "Name":
                                        lvwList.Columns[colIndex].Name = item.Value;
                                        break;
                                    case "Position":
                                        int.TryParse(item.Value, out position);
                                        //lvwList.Columns[colIndex].Position = position;
                                        break;
                                    case "SortOrder":
                                        if (item.Value == Gizmox.WebGUI.Forms.SortOrder.Ascending.ToString("g"))
                                            lvwList.Columns[colIndex].SortOrder = Gizmox.WebGUI.Forms.SortOrder.Ascending;
                                        else if (item.Value == Gizmox.WebGUI.Forms.SortOrder.Descending.ToString("g"))
                                            lvwList.Columns[colIndex].SortOrder = Gizmox.WebGUI.Forms.SortOrder.Descending;
                                        else if (item.Value == Gizmox.WebGUI.Forms.SortOrder.None.ToString("g"))
                                            lvwList.Columns[colIndex].SortOrder = Gizmox.WebGUI.Forms.SortOrder.None;
                                        break;
                                    case "SortPosition":
                                        int.TryParse(item.Value, out sortPosition);
                                        lvwList.Columns[colIndex].SortPosition = sortPosition;
                                        break;
                                    case "Visible":
                                        bool.TryParse(item.Value, out visible);
                                        lvwList.Columns[colIndex].Visible = visible;
                                        break;
                                    case "Width":
                                        int.TryParse(item.Value, out width);
                                        lvwList.Columns[colIndex].Width = width;
                                        break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                */
            }

            #region 2012.04.18 paulus: 已放棄這段 Serialization code
            // HACK: Serialize 最好的保存方法是用 SQL Data Type VARBINARY 保存
            public Byte[] Serialize(ListView lvwList)
            {
                CustomColumn[] ccArray = CustomizeListView(lvwList);

                XmlSerializer xmlSerialer = new XmlSerializer(typeof(CustomColumn));

                BinaryFormatter objBinaryFormatter = new BinaryFormatter();
                MemoryStream objMemoryStream = new MemoryStream();
                StringWriter writer = new StringWriter();

                objBinaryFormatter.Serialize(objMemoryStream, ccArray);

                objMemoryStream.Seek(0, 0);
                Byte[] content = objMemoryStream.ToArray();

                String s = System.Text.Encoding.Default.GetString(objMemoryStream.ToArray());

                return content;
            }

            // HACK: 未完成
            public void Deserialize(ListView lvwList)
            {
                CustomColumn[] ccArray = CustomizeListView(lvwList);
                BinaryFormatter objBinaryFormatter = new BinaryFormatter();
                MemoryStream objMemoryStream = new MemoryStream();
                objBinaryFormatter.Serialize(objMemoryStream, ccArray);
                objMemoryStream.Seek(0, 0);
                Byte[] content = objMemoryStream.ToArray();
            }

            [Serializable()]
            public class CustomColumn
            {
                public string ImageFile;
                public int Index;
                public string Name;
                public int Position;
                public string Text;
                public bool Visible;
                public int Width;
            }

            public CustomColumn[] CustomizeListView(ListView L)
            {
                CustomColumn[] cColArray = new CustomColumn[L.Columns.Count];

                for (int i = 0; i < L.Columns.Count; i++)
                {
                    ColumnHeader col = L.Columns[i];
                    CustomColumn cc = new CustomColumn();

                    cc.ImageFile = String.Empty;
                    if (col.Image != null)
                        cc.ImageFile = col.Image.File;
                    cc.Index = col.Index;
                    cc.Name = col.Name;
                    //cc.Position = col.Position;
                    cc.Text = col.Text;
                    cc.Visible = col.Visible;
                    cc.Width = col.Width;

                    cColArray[i] = cc;
                }

                return cColArray;
            }
            #endregion
        }
    }
}