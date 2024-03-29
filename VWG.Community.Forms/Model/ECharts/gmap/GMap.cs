///////////////////////////////////////////////////////////
//  GMap.cs use Google Map overlay
//  Implementation of the Class BMap
//  Generated by Enterprise Architect
//  Created on:      13-2��-2017 0:22:34
//  Original author: Doku
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VWG.Community.Forms.Model.ECharts.gmap
{
    public class GMap
    {
        #region Properties
        public IList<double> center { get; set; }

        public int zoom { get; set; }

        public bool roam { get; set; }

        public MapStyle mapStyle { get; set; }

        public bool renderOnMoving { get; set; }

        public int echartsLayerZIndex { get; set; }

        public StyleLabelType label { get; set; }
        #endregion

        #region Methods
        /// 
        /// <param name="center"></param>
        public GMap Center(IList<double> center)
        {
            this.center = center;
            return this;
        }

        /// 
        /// <param name="zoom"></param>
        public GMap Zoom(int zoom)
        {
            this.zoom = zoom;
            return this;
        }

        /// 
        /// <param name="roam"></param>
        public GMap Roam(bool roam)
        {
            this.roam = roam;
            return this;
        }

        /// 
        /// <param name="mapStyle"></param>
        public MapStyle MapStyle()
        {
            if (this.mapStyle == null)
                this.mapStyle = new MapStyle();
            return this.mapStyle;
        }

        public GMap RenderOnMoving(bool renderOnMoving)
        {
            this.renderOnMoving = renderOnMoving;
            return this;
        }

        public GMap EChartsLayerZIndex(int echartsLayerZIndex)
        {
            this.echartsLayerZIndex = echartsLayerZIndex;
            return this;
        }
        #endregion

    }//end BMap

}//end namespace bmap