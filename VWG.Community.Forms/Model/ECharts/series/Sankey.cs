///////////////////////////////////////////////////////////
//  Parallel.cs
//  Implementation of the Class Parallel
//  Generated by Enterprise Architect
//  Created on:      18-2��-2017 10:19:21
//  Original author: Doku
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using VWG.Community.Forms.Model.ECharts.series;
namespace VWG.Community.Forms.Model.ECharts.series {
	public class Sankey : ChartSeries<Sankey> {

        public string layout { get; set; }

        public object links { get; set; }

        public object categories { get; set; }


          public Sankey()
        {
            this.type = ChartType.sankey;
        }

          public Sankey(string name)
              : this()
          {
            this.name = name;
        }

          public Sankey Categories(object categories)
          {
              this.categories = categories;
              return this;
          }

          public Sankey Links(object links)
          {
              this.links = links;
              return this;
          }

          public Sankey Layout(string layout)
          {
              this.layout = layout;
              return this;
          }
         


		 

	}//end Parallel

}//end namespace series