using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VWG.Community.Forms.Model.ECharts.style;

namespace VWG.Community.Forms.Model.ECharts
{
    /// <summary>
    /// 2021.11.01 paulus:
    /// 原作冇嘅，我借 https://github.com/abel533/ECharts/blob/master/src/main/java/com/github/abel533/echarts/VisualMap.java 補上
    /// </summary>
    public class VisualMap : Basic<VisualMap>
    {
        #region Properties
        private VisualMapType type { get; set; }

        public bool calculable { get; set; }

        public bool realtime { get; set; }

        public int precision { get; set; }

        public int itemWidth { get; set; }

        public int itemHeight { get; set; }

        public SymbolType itemSymbol;

        public HorizontalType align;

        /// <summary>
        /// 好似冇啲咁嘅嘢，淨係有 HandleIcon, HandleSize, HandleStyle，暫時唔理住
        /// </summary>
        public HorizontalType handlePosition;

        public object dimension { get; set; }

        public int seriesIndex { get; set; }

        public VisualMapType inRange { get; set; }

        public VisualMapType outOfRange { get; set; }

        public string formatter { get; set; }

        public SelectedModeType selectedMode;

        public TextStyle textStyle;

        public object[] color { get; set; }

        public object[] text { get; set; }

        public object[] textGap { get; set; }

        public object[] pieces { get; set; }

        public object[] categories { get; set; }
        #endregion

        #region Methods
        public VisualMap Type(VisualMapType type)
        {
            this.type = type;
            return this;
        }

        public VisualMap Calculable(bool calculable)
        {
            this.calculable = calculable;
            return this;
        }

        public VisualMap Realtime(bool realtime)
        {
            this.realtime = realtime;
            return this;
        }

        public VisualMap Inverse(bool inverse)
        {
            this.inverse = inverse;
            return this;
        }

        public VisualMap Precision(int precision)
        {
            this.precision = precision;
            return this;
        }

        public VisualMap ItemWidth(int itemWidth)
        {
            this.itemWidth = itemWidth;
            return this;
        }

        public VisualMap ItemHeight(int itemHeight)
        {
            this.itemHeight = itemHeight;
            return this;
        }

        public VisualMap ItemSymbol(SymbolType itemSymbol)
        {
            this.itemSymbol = itemSymbol;
            return this;
        }

        public VisualMap Align(HorizontalType align)
        {
            this.align = align;
            return this;
        }

        public VisualMap HandlePosition(HorizontalType handlePosition)
        {
            this.handlePosition = handlePosition;
            return this;
        }

        public VisualMap Dimension(object dimension)
        {
            this.dimension = dimension;
            return this;
        }

        public VisualMap SeriesIndex(int seriesIndex)
        {
            this.seriesIndex = seriesIndex;
            return this;
        }

        public VisualMap InRange(VisualMapType inRange)
        {
            this.inRange = inRange;
            return this;
        }

        public VisualMap OutOfRange(VisualMapType outOfRange)
        {
            this.outOfRange = outOfRange;
            return this;
        }

        public VisualMap Formatter(string formatter)
        {
            this.formatter = formatter;
            return this;
        }

        public VisualMap SelectedMode(SelectedModeType selectedMode)
        {
            this.selectedMode = selectedMode;
            return this;
        }

        public VisualMap TextStyle(TextStyle textStyle)
        {
            //if (textStyle == null)
            //    textStyle = new style.TextStyle();
            //return this.textStyle;

            this.textStyle = textStyle;
            return this;
        }

        public VisualMap Text(object[] text)
        {
            this.text = text;
            return this;
        }

        public VisualMap TextGap(object[] textGap)
        {
            this.textGap = textGap;
            return this;
        }

        public VisualMap Pieces(object[] pieces)
        {
            this.pieces = pieces;
            return this;
        }

        public VisualMap Categories(object[] categories)
        {
            this.categories = categories;
            return this;
        }
        #endregion
    }
}
