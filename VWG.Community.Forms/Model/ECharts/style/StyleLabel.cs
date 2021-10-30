using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VWG.Community.Forms.Model.ECharts.style
{
    public class StyleLabel
    {
        #region Properties
        public bool? show { get; set; }

        public StyleLabelType? position { get; set; }

        public bool? rotate { get; set; }

        public int? distance { get; set; }

        public object color { get; set; }

        public object formatter { get; set; }

        public TextStyle textStyle { get; set; }

        public int? x { get; set; }

        public int? y { get; set; }
        #endregion

        #region Methods
        public StyleLabel Y(int y)
        {
            this.y = y;
            return this;
        }

        public StyleLabel X(int x)
        {
            this.x = x;
            return this;
        }

        public StyleLabel Formatter(object formatter)
        {
            this.formatter = formatter;
            return this;
        }

        public StyleLabel Rotate(bool rotate)
        {
            this.rotate = rotate;
            return this;
        }

        public StyleLabel Color(string color)
        {
            this.color = color;
            return this;
        }

        public StyleLabel Distance(int distance)
        {
            this.distance = distance;
            return this;
        }

        public StyleLabel Show(bool show)
        {
            this.show = show;
            return this;
        }

        public TextStyle TextStyle()
        {
            if (this.textStyle == null)
                this.textStyle = new style.TextStyle();
            return this.textStyle;
        }

        public StyleLabel Position(StyleLabelType position)
        {
            this.position = position;
            return this;
        }
        #endregion
    }
}
