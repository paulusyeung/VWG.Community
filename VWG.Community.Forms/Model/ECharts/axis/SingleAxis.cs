﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts.axis
{
    public class SingleAxis:ChartAxis<SingleAxis>
    {

        public SingleAxis()
        { 
        }

        public SingleAxis(AxisType type)
        {
            this.type = type;
        }

    }
}
