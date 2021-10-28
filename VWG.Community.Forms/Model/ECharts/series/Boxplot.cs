using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts.series
{
    public class Boxplot : Rectangular<Boxplot>
    {
        public Boxplot()
        {
            this.type = ChartType.boxplot;
        }

    }
}
