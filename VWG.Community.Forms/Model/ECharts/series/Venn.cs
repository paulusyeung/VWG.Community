﻿using VWG.Community.Forms.Model.ECharts.style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts.series
{
    public class Venn : ChartSeries<Venn>
    {

        public Venn()
        {
            this.type = ChartType.venn;
        }

        public Venn(string name) : this() {
            this.name = name;
        }

    }
}
