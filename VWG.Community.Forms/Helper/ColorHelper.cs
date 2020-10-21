using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Helper
{
    public static class ColorHelper
    {
        public static String ToHex(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

        public static String SafeColorName(Color color)
        {
            var colorName = String.Empty;
            if (!color.IsEmpty)
            {
                if (color.IsNamedColor)
                {
                    colorName = color.Name;
                }
                else if (color.IsKnownColor)
                {
                    colorName = color.Name;
                }
                else if (color.IsSystemColor)
                {
                    colorName = color.Name;
                }
                else
                {
                    colorName = String.Format("#{0}", color.Name.Substring(2));
                }
            }
            return colorName;
        }
    }
}
