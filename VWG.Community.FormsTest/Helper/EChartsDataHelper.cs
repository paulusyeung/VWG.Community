using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VWG.Community.Forms.Helper;
using VWG.Community.Forms.Model.ECharts;
using VWG.Community.Forms.Model.ECharts.axis;
using VWG.Community.Forms.Model.ECharts.feature;
using VWG.Community.Forms.Model.ECharts.series;
using VWG.Community.Forms.Model.ECharts.style;
using VWG.Community.FormsTest.ModelEx;

namespace VWG.Community.FormsTest.Helper
{
    public class EChartsDataHelper
    {
        /// <summary>
        /// refer: https://www.cnblogs.com/kulong995/p/5237796.html
        /// </summary>
        /// <returns></returns>
        public static string StdLine()
        {
            IList<string> weeks = ChartsUtil.Weeks();

            IList<int> datas1 = ChartsUtil.Datas(7, 10, 15);

            IList<int> datas2 = ChartsUtil.Datas(7, -2, 5);

            int min = datas2.Min();
            int index = datas2.IndexOf(min);

            ChartOption option = new ChartOption();

            #region option.title
            /** origional code
            option.title = new Title()
            {
                show = true,
                text = "未来一周天气变化",
                subtext = "纯虚构数据"
            };
            */
            option.title = new List<Title>()
            {
                new Title()
                {
                    show = true,
                    text = "未来一周天气变化",
                    subtext = "纯虚构数据"
                }
            };
            #endregion

            #region option.tooltip
            option.tooltip = new Forms.Model.ECharts.ToolTip()
            {
                trigger = TriggerType.axis
            };
            #endregion

            #region option.legend
            option.legend = new Legend()
            {
                data = new List<object>(){
                    "最高温度",
                    "最低温度"
                }
            };
            #endregion

            option.calculable = true;

            #region option.xAxis
            option.xAxis = new List<Forms.Model.ECharts.axis.Axis>()
            {
                new Forms.Model.ECharts.axis.CategoryAxis()
                {
                    type = AxisType.category,
                    boundaryGap= false,
                    data = new List<object>(weeks)
                }
            };
            #endregion

            #region option.yAxis
            option.yAxis = new List<Axis>()
            {
                new ValueAxis()
                {
                    type = AxisType.value,
                    axisLabel = new AxisLabel(){
                    formatter=new JRaw("{value} ℃").ToString()
                    }
                }
            };
            #endregion

            #region option.series
            option.series = new List<object>()
            {
                new Line()
                {
                    name = "最高温度",
                    type =  ChartType.line,
                    data =  datas1,
                    markPoint =  new MarkPoint()
                    {
                        data = new List<object>()
                        {
                            new MarkData()
                            {
                                name = "最大值",
                                type= MarkType.max,
                            },
                            new MarkData()
                            {
                                name = "最小值",
                                type= MarkType.min,
                            }
                        }
                        },
                        markLine = new MarkLine()
                        {
                        data = new List<object>()
                        {
                            new MarkData()
                            {
                                name = "平均值",
                                type = MarkType.average
                            }
                        }
                    }
                },
                new Line()
                {
                    name="最低温度",
                    type = ChartType.line,
                    data = datas2,
                    markPoint= new MarkPoint(){
                    data = new List<object>(){
                        new MarkData(){
                        name="周最低",
                        value = min,
                        xAxis = index,
                        yAxis = min+0.5,
                        }
                    }
                    },
                    markLine = new MarkLine(){
                        data = new List<object>(){
                            new MarkData(){
                                type = MarkType.average,
                                name = "平均值"
                            }
                        }
                    }
                }
            };
            #endregion

            var result = JsonHelper.ObjectToJson2(option);

            return result;
        }

        /// <summary>
        /// refer: https://www.cnblogs.com/kulong995/p/5237796.html
        /// </summary>
        /// <returns></returns>
        public static string TemperatureBar()
        {
            ChartOption option = new ChartOption();

            option.Title().Text("温度计式图表").Subtext("Form ExcelHome").
                Sublink("http://e.weibo.com/1341556070/AizJXrAEa");

            option.ToolTip().Trigger(TriggerType.axis)
                // 原作者嘅 code，好似有 bug，暫時取消
                //.Formatter(new JRaw(@"function (params){
                //    return params[0].name + '<br/>'
                //            + params[0].seriesName + ' : ' + params[0].value + '<br/>'
                //            + params[1].seriesName + ' : ' + (params[1].value + params[0].value);
                //    }"))
                .AxisPointer().Type(AxisPointType.shadow);

            option.Legend().Data("Acutal", "Forecast");

            Feature feature = new Feature();
            feature.Mark().Show(true);
            feature.DataView().Show(true).ReadOnly(false);
            feature.Restore().Show(true);
            feature.SaveAsImage().Show(true);
            option.ToolBox().Show(true).SetFeature(feature);

            option.Grid().Y(80).Y2(30);

            CategoryAxis x = new CategoryAxis();
            x.Data("Cosco", "CMA", "APL", "OOCL", "Wanhai", "Zim");
            option.XAxis(x);

            ValueAxis y = new ValueAxis();
            y.BoundaryGap(new List<double>() { 0, 0.1 });
            option.YAxis(y);

            var tomatoStyle = new ItemStyle();
            tomatoStyle.Normal().Color("tomato").BarBorderRadius(0)
                .BarBorderColor("tomato").BarBorderWidth(6)
                .Label().Show(true).Position(StyleLabelTyle.insideTop);
            Bar b1 = new Bar("Acutal");
            b1.Stack("sum");
            b1.SetItemStyle(tomatoStyle);
            b1.Data(260, 200, 220, 120, 100, 80);

            var forecastStyle = new ItemStyle();
            var pattern = @"(""(?:[^""\\]|\\.)*"")|\s+"; // remove space and CRLF
            var raw = @"function (params) {for (var i = 0, l = option.xAxis[0].data.length; i < l; i++) {if (option.xAxis[0].data[i] == params.name) {return option.series[0].data[i] + params.value;}}}";
            var formatter = string.Format(@"eval(""{0}"")", raw);
            var tmp = new JRaw(raw);
            //! HACK: Formatter 去到 EChartsBox.html script 會出 JSON.parse error
            forecastStyle.Normal().Color("#fff").BarBorderRadius(0)
                .BarBorderColor("tomato").BarBorderWidth(6)
                .Label().Show(true).Position(StyleLabelTyle.top)
                .Formatter(new JRaw(@"function (params) {
                        for (var i = 0, l = option.xAxis[0].data.length; i < l; i++) {
                            if (option.xAxis[0].data[i] == params.name) {
                                return option.series[0].data[i] + params.value;
                            }
                        }
                    }"))
                //.Formatter(new JRaw(formatter))
                .TextStyle().Color("tomato");

            Bar b2 = new Bar("Forecast");
            b2.Stack("sum");
            b2.SetItemStyle(forecastStyle);
            b2.Data(40, 80, 50, 80, 80, 70);

            option.Series(b1, b2);

            var result = JsonHelper.ObjectToJson2(option);

            return result;
        }
    }
}
