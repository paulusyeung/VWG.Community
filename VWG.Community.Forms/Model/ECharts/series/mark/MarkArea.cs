using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VWG.Community.Forms.Model.ECharts.style;

namespace VWG.Community.Forms.Model.ECharts.series.mark
{
    public class MarkArea : AbstractData<MarkArea>
    {
        public ItemStyle itemStyle { get; set; }

        public ItemStyle ItemStyle()
        {
            if(this.itemStyle==null)
                this.itemStyle = new ItemStyle();
            return this.itemStyle;
        }


    }
}
