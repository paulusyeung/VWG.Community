using VWG.Community.Forms.Model.ECharts.style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts.axis
{
    public class AxisName
    {
        public bool? show { get; set; }

        public object formatter { get; set; }

        public TextStyle  textStyle { get; set; }

        public AxisName Formatter(object formatter)
        {
            this.formatter = formatter;
            return this;
        }

        public TextStyle TextStyle()
        {
            if (textStyle == null)
                textStyle = new style.TextStyle();
            return textStyle;
        }

        public AxisName Show(bool show)
        {
            this.show = show;
            return this;
        }
    }
}
