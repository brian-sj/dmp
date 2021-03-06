﻿using DMP.DataModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DMP.Controls.pushpin
{
    class DMPPushpin : Pushpin 
    {
        private Map _map;
        private MapPolyline polyline;
        private MapLayer mapLayer;
        private bool isDragging = false;
        Location _center;


        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(DMPPushpin)
                , new UIPropertyMetadata(0));

        public static readonly DependencyProperty AltProperty =
            DependencyProperty.Register("Alt", typeof(float), typeof(DMPPushpin)
        , new UIPropertyMetadata(0f));

        public static readonly DependencyProperty BearingProperty =
            DependencyProperty.Register("Bearing", typeof(float), typeof(DMPPushpin)
        , new UIPropertyMetadata(0f));

        public DMPPushpin(Map map){
            _map = map;
            DataContext = this;
        }

        public DMPPushpin(ref Map map ,ref MapPolyline mpl , ref MapLayer ml )
        {
            _map = map;
            polyline = mpl;
            mapLayer = ml;
        }

#region 다른 정보들 ... 
        /// <summary>
        /// PushPin에 해당하는 것들... 근데...
        /// </summary>
        public WayPointModel WPM
        {
            set;get;
        }
        public string Description
        {
            get; set;
        }
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
        public int PointType
        {
            get; set;
        }
        public float Alt
        {
            get { return (float)GetValue(AltProperty); }
            set { SetValue(AltProperty, value); }
        }

        public float Bearing
        {
            get { return (float)GetValue(BearingProperty); }
        }

        #endregion

        #region 드래거블 

        protected override void OnMouseDown( MouseButtonEventArgs e)
        {
            Console.WriteLine("MouseDown... : heigh light");


            //base.OnMouseDown(e);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(_map != null)
            {
                _center = _map.Center;
                _map.ViewChangeOnFrame += _map_ViewChangeOnFrame;
                _map.MouseUp += ParentMap_MouseLeftButtonUp;
                _map.MouseMove += ParentMap_MouseMove;
                _map.TouchMove += _map_TouchMove;

                ///선택된애 색을 밝게 해준다. 
                if(WPM != null)WPM.IsActive = true; 
            }
            this.isDragging = true;
            base.OnMouseLeftButtonDown(e);
        }
        /// <summary>
        /// pc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ParentMap_MouseMove(object sender, MouseEventArgs e)
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            if (this.isDragging)
            {
                var mouseMapPosition = e.GetPosition(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition);
                _map_MoveAction(mouseGeocode);
            }
        }
        /// <summary>
        /// TABLET
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _map_TouchMove( object sender , TouchEventArgs e )
        {
            var map = sender as Microsoft.Maps.MapControl.WPF.Map;
            if (this.isDragging)
            {
                var mouseMapPosition = e.GetTouchPoint(map);
                var mouseGeocode = map.ViewportPointToLocation(mouseMapPosition.Position);
                _map_MoveAction( mouseGeocode);
            }
        }
        /// <summary>
        /// Touch 건 마우스건 Move는 여기서 한다.. 
        /// </summary>
        /// <param name="mouseGeocode"></param>
        void _map_MoveAction(Location mouseGeocode)
        {
            this.Location = mouseGeocode;
            if(WPM != null)
            {
                WPM.Latitude = this.Location.Latitude;
                WPM.Longitude = this.Location.Longitude;
            }
            //Console.WriteLine("Move :" + this.Idx + ":" + this.Location.Latitude + ":" + this.Location.Longitude);
        }

        void _map_ViewChangeOnFrame( object sender , MapEventArgs e )
        {
            if (isDragging)
            {
                _map.Center = _center;
            }
        }

        void ParentMap_MouseLeftButtonUp(Object sender , MouseButtonEventArgs e)
        {
            if( _map != null)
            {
                _map.ViewChangeOnFrame -= _map_ViewChangeOnFrame;
                _map.MouseUp -= ParentMap_MouseLeftButtonUp;
                _map.MouseMove -= ParentMap_MouseMove;
                _map.TouchMove -= _map_TouchMove;

                /// MapPolyLine을 다시 그린다. WAYPoint 일때만 상로가 그려진다. 
                if(WPM.PointType == (int)DMP.PointType.WAYPOINT)
                {
                    MapDesignModel.Instance.DrawPolyLine();
                }


                /// 거리뿐 아니라 각도도 다시 계산해야 하므로 HomePosition, Waypoint
                

                // Home 과 WayPoint일때만 거리를 재 측정한다. 
                if (WPM.PointType == (int) DMP.PointType.WAYPOINT 
                    || WPM.PointType == (int)DMP.PointType.HOME
                    || WPM.PointType == (int)DMP.PointType.TARGET)
                {
                    MapDesignModel.Instance.CalculationEachDistance();
                    //MapDesignModel.Instance.
                }
                
                
                //ReDrawMapPolyLine();

                if (WPM.PointType == (int)DMP.PointType.WAYPOINT) {
                    var ContentPopup = (Canvas)_map.FindName("ContentPopup");
                    var ContentPopupText = (TextBlock)_map.FindName("ContentPopupText");
                    MapLayer.SetPosition(ContentPopup, Location);
                    MapLayer.SetPositionOffset(ContentPopup, new Point(15, -50));
                    ToolTip = String.Format("{0:0.00}", WPM.DistanceFromPrev);
                    ContentPopupText.Text = String.Format("{0:0.00}", WPM.DistanceFromPrev);//"MyPush" + pin.Idx.ToString() + pin.WPM.DistanceFromPrev +":";
                    ContentPopup.Visibility = Visibility.Visible;
                }
            }

            this.isDragging = false;
        }

        /// <summary>
        /// 이것좀 어디로 보내고 싶따...
        /// </summary>
        public void ReDrawMapPolyLine()
        {
            /*
            if(mapLayer.Children.Count>0) mapLayer.Children.RemoveAt(0);
            polyline = new MapPolyline();
            polyline.Stroke = new SolidColorBrush(System.Windows.Media.Colors.Brown);
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();
            foreach (var item in GvarDesignModel.Instance.WPList)
            {
                polyline.Locations.Add(new Location(item.Latitude, item.Longitude));
            }
            mapLayer.Children.Add(polyline);
            */
        }
        #endregion

    }
}
