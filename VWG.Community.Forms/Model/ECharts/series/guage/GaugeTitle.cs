using VWG.Community.Forms.Model.ECharts.style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts
{
    public class GaugeTitle
    {
        public bool? show { get; set; }
       
        public IList<string> offsetCenter { get; set; }
      
        public TextStyle textStyle { get; set; }


        public TextStyle TextStyle()
        {
            if (this.textStyle == null)
                textStyle = new style.TextStyle();
            return textStyle;
        }

        public GaugeTitle OffsetCenter(IList<string> offsetCenter)
        {
            this.offsetCenter = offsetCenter;
            return this;
        }

        public GaugeTitle Show(bool show)
        {
            this.show = show;
            return this;
        }
    }
}
