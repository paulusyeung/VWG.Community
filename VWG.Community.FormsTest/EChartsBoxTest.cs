#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using VWG.Community.Forms;
using System.Text.RegularExpressions;
using VWG.Community.Forms.Model.ECharts;
using VWG.Community.FormsTest.ModelEx;
using VWG.Community.Forms.Model.ECharts.axis;
using Newtonsoft.Json.Linq;
using VWG.Community.Forms.Model.ECharts.series;
using VWG.Community.Forms.Helper;
using Gizmox.WebGUI.Common.Resources;
using VWG.Community.Forms.Skins;

#endregion

namespace VWG.Community.FormsTest
{
    public partial class EChartsBoxTest : Form
    {
        public EChartsBoxTest()
        {
            InitializeComponent();
        }

        private void EChartsBoxTest_Load(object sender, EventArgs e)
        {
            var blockSize = new Size(640, 480);

            #region Simple Bar
            var pattern = @"(""(?:[^""\\]|\\.)*"")|\s+";
            var simpleBarChart = Regex.Replace(@"{
    ""title"": {
        ""text"": ""第 2 个 ECharts 实例""
    },
    ""tooltip"": {},
    ""legend"": {
        ""data"":[""销量""]
    },
    ""xAxis"": {
        ""data"": [""衬衫"",""羊毛衫"",""雪纺衫"",""裤子"",""高跟鞋"",""袜子""]
    },
    ""yAxis"": {},
    ""series"": [{
        ""name"": ""销量"",
        ""type"": ""bar"",
        ""data"": [5, 20, 36, 10, 10, 20]
    }]
}", pattern, "$1");   // Use RegEx to remove spaces CRLF

            var simpleBar = Regex.Replace(simpleBarChart, pattern, "$1");
            var simpleBarBox = new EChartsBox()
            {
                Dock = DockStyle.Fill,
                EChartsOption = simpleBar,
                Size = blockSize,
                Text = "Simple Bar Chart"
            };
            #endregion

            #region standard line
            var stdLine = Regex.Replace(Helper.EChartsDataHelper.StdLine(), pattern, "$1");
            var stdLineBox = new EChartsBox()
            {
                Dock = DockStyle.Fill,
                EChartsOption = stdLine,
                Size = blockSize
            };
            #endregion

            #region temperature bar
            var tempBar = Regex.Replace(Helper.EChartsDataHelper.TemperatureBar(), pattern, "$1");
            var tempBarBox = new EChartsBox()
            {
                Dock = DockStyle.Fill,
                EChartsOption = tempBar,
                Size = blockSize
            };
            #endregion

            #region Simple Calendar
            //var pattern = @"(""(?:[^""\\]|\\.)*"")|\s+";
            var simpleScript = @"
function getVirtulData(year) {
    year = year || '2017';
    var date = +echarts.number.parseDate(year + '-01-01');
    var end = +echarts.number.parseDate(year + '-12-31');
    var dayTime = 3600 * 24 * 1000;
    var data = [];
    for (var time = date; time <= end; time += dayTime) {
        data.push([
            echarts.format.formatTime('yyyy-MM-dd', time),
            Math.floor(Math.random() * 10000)
        ]);
    }
    return data;
}";

            var simpleCalendarData = Regex.Replace(@"{
    visualMap: {
        show: false,
        min: 0,
        max: 10000
    },
    calendar: {
        range: '2017'
    },
    series: {
        type: 'heatmap',
        coordinateSystem: 'calendar',
        data: getVirtulData(2017)
    }
}", pattern, "$1");   // Use RegEx to remove spaces CRLF

            var simpleCalendar = Regex.Replace(simpleCalendarData, pattern, "$1");
            var simpleCalBox = new EChartsBox()
            {
                Dock = DockStyle.Fill,
                EChartsExtraScript = simpleScript,
                EChartsOption = simpleCalendar,
                Size = blockSize,
                Text = "Simple Calendar Chart"
            };
            #endregion

            var flp = new FlowLayoutPanel()
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };
            flp.Controls.Add(simpleBarBox);
            flp.Controls.Add(stdLineBox);
            flp.Controls.Add(tempBarBox);
            flp.Controls.Add(simpleCalBox);

            this.Controls.Add(flp);
        }
    }
}