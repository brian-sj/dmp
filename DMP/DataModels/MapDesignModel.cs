using DMP.Controls.pushpin;
using DMP.util;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DMP.DataModels
{
    /// <summary>
    ///   Map 페이지가 생길때 DMap 확인해주라...
    /// </summary>
    class MapDesignModel : MapModel
    {
        #region Singleton 
        /// <summary>
        /// A single instance (singleton) of the 데이터 입력하게.. d.. 
        /// </summary>
        private static MapDesignModel _instance;
        public static MapDesignModel Instance => _instance ?? (_instance = new MapDesignModel());
        #endregion Singleton
        
        public MapDesignModel()
        {
            DPushpins = new ObservableCollection<DMPPushpin>();
        }

        public bool DrawPolyLine()
        {
            if (DMap == null)
                return false;

            if (DmlPolyline.Children.Count > 0) DmlPolyline.Children.RemoveAt(0);
            var polyline = new MapPolyline();
            
            polyline.Stroke = new SolidColorBrush(System.Windows.Media.Colors.Brown);
            polyline.Opacity = 0.7;
            polyline.Locations = new LocationCollection();
            foreach (var item in GvarDesignModel.Instance.WPList)
            {
                polyline.Locations.Add(new Location(item.Latitude, item.Longitude));
            }
            DmlPolyline.Children.Add(polyline);
            return true;
        }

        public bool DrawToolTipMapLayer()
        {
            if (DMap == null)
                return false;

            return true;
        }

        public bool DrawFlightPolyLine()
        {
            if (DMap == null)
                return false;


            return true;
        }


        public void LoadPushpinFromWaypointList()
        {
            if (Ttemplate == null)
            {
                DMP.Dialogs.CustomMessageBox.Show("오류: Style이 정해지지 않았습니다. (MapDesignModel.LoadPushpinFromWaypoint)");
                return;
            }
            DPushpins.Clear();
            DmlPushpin.Children.Clear();
            var inst = GvarDesignModel.Instance;
            //// HomePosition이 Null 이면 WPList로 첫것을 택한다. 
            if (GvarDesignModel.Instance.HomePosition == null)
            {

                var temp = inst.WPList.FirstOrDefault();
                // WayPoint 한개라도있으면 복사해 넣고... 
                if (temp != null)
                {
                    WayPointModel hwp = new WayPointModel();   // 눈에 보이게 조금 숫자를 보탠다. 
                    hwp.Latitude = temp.Latitude + 0.0003;
                    hwp.Longitude = temp.Longitude + 0.0002;
                    hwp.PointType = (int)DMP.PointType.HOME;
                    inst.HomePosition = hwp;
                }

            }
            if (  GvarDesignModel.Instance.HomePosition != null )
            {
                var hpin = new DMPPushpin(DMap);
                hpin.Location = new Location(GvarDesignModel.Instance.HomePosition.Latitude, GvarDesignModel.Instance.HomePosition.Longitude);
                hpin.Template = Htemplate;

                hpin.PositionOrigin = PositionOrigin.Center;
                hpin.Content = "";

                hpin.WPM = GvarDesignModel.Instance.HomePosition;
                DmlPushpin.Children.Add(hpin);
                DPushpins.Add(hpin);
            }


            /// way point 를 찍어 주자... 
            foreach (var item in GvarDesignModel.Instance.WPList)
            {
                var pin = new DMPPushpin( DMap ); // ref MapWithEvents, ref polyline, ref DmapPushPinLayer);
                
                Binding lbinding = new Binding("Location");
                Binding hbinding = new Binding("Height");
                Binding ibinding = new Binding("Index");
                Binding bbinding = new Binding("Bearing");
                lbinding.Source = item;
                lbinding.Mode = BindingMode.TwoWay;
                
                hbinding.Source = item;
                ibinding.Source = item;
                bbinding.Source = item;

                pin.SetBinding(Pushpin.LocationDependencyProperty, lbinding);
                pin.SetBinding(DMPPushpin.IndexProperty, ibinding);
                pin.SetBinding(DMPPushpin.AltProperty, hbinding);
                pin.SetBinding(DMPPushpin.BearingProperty, bbinding);
                //pin.Location = new Location(item.Latitude, item.Longitude);


                pin.Template = Wtemplate;
                pin.Content = String.Format("{0}m",  item.Height);
                pin.PointType = (int)PointType.WAYPOINT;
                pin.WPM = item;

                pin.PositionOrigin = PositionOrigin.Center;
                //pin.MouseDown += new MouseButtonEventHandler(pushpinHighlight);
                DmlPushpin.Children.Add(pin);
                DPushpins.Add(pin);
            }
            /// target Point를 찍어 주자.
            foreach (var item in GvarDesignModel.Instance.TPList)
            {
                var pin = new DMPPushpin(DMap); // ref MapWithEvents, ref polyline, ref DmapPushPinLayer);
                
                pin.Template = Ttemplate;
                pin.Content = String.Format("{0}({1})", item.Index, item.Height);
                Binding lbinding = new Binding("Location");
                Binding hbinding = new Binding("Height");
                Binding ibinding = new Binding("Index");
                lbinding.Source = item;
                lbinding.Mode = BindingMode.TwoWay;
                hbinding.Source = item;
                ibinding.Source = item;

                pin.SetBinding(Pushpin.LocationDependencyProperty, lbinding);
                pin.SetBinding(DMPPushpin.IndexProperty, ibinding);
                pin.SetBinding(DMPPushpin.AltProperty, hbinding);

                pin.PointType = (int)PointType.TARGET;
                pin.PositionOrigin = PositionOrigin.Center;
                pin.WPM = item;
                DmlPushpin.Children.Add(pin);// MapWithEvents.Children.Add(pin);
                DPushpins.Add(pin);
            }
            /// DrawPolyLine 과 CalculationEachDistance는 항상 같이 다닌다. 
            DrawPolyLine();
            MapDesignModel.Instance.CalculationEachDistance();
        }

        public void BindPushpinWaypointModel( WayPointModel wp ,DMPPushpin pin )
        {
            Binding lbinding = new Binding("Location");
            Binding hbinding = new Binding("Height");
            Binding ibinding = new Binding("Index");
            Binding bbinding = new Binding("Bearing");
            lbinding.Source = wp;
            lbinding.Mode = BindingMode.TwoWay;
            hbinding.Source = wp;
            ibinding.Source = wp;
            bbinding.Source = wp;
            pin.SetBinding(Pushpin.LocationDependencyProperty, lbinding);
            pin.SetBinding(DMPPushpin.IndexProperty, ibinding);
            pin.SetBinding(DMPPushpin.AltProperty, hbinding);
            pin.SetBinding(DMPPushpin.BearingProperty, bbinding);
        }
        /// <summary>
        /// 각 웨이포인트별로 거리를 구한다...  첫번째 것은 HomePoint별로 구해야 하는데 이것은 비행기를 띠워야 아는 것이다.
        /// </summary>
        public double CalculationEachDistance()
        {

            double totalDistance = 0.0;
            var inst = GvarDesignModel.Instance;
            WayPointModel pwp = null;
            WayPointModel hwp = inst.HomePosition;
            if (hwp == null)
            {
                var temp = inst.WPList.FirstOrDefault();
                if (temp == null)
                    return 0;
                hwp = new WayPointModel();
                hwp.Latitude = temp.Latitude + 0.0003;
                hwp.Longitude = temp.Longitude + 0.0002;
                hwp.PointType = (int)DMP.PointType.HOME;
                inst.HomePosition = hwp;
            }

            int totalcount = inst.WPList.Count;
            int idx = 0;

            foreach (var wp in inst.WPList)
            {
                idx++;
                if (pwp == null)
                {  // 첫번째라면...
                    pwp = new WayPointModel();
                    pwp.Latitude = hwp.Latitude;
                    pwp.Longitude = hwp.Longitude;
                }
                wp.DistanceFromPrev = GeoCalculate.Distance(pwp.Latitude, pwp.Longitude, wp.Latitude, wp.Longitude);



                if (pwp.Target == 0)
                {
                    pwp.Bearing = (float)Math.Round(GeoCalculate.CalculateBearing(wp.Location, pwp.Location), 2);
                }
                else
                {
                    WayPointModel tp = inst.TPList.Single<WayPointModel>(i => i.Index == pwp.Target);
                    pwp.Bearing = (float)Math.Round(GeoCalculate.CalculateBearing(tp.Location, pwp.Location), 2);
                }
                //마지막꺼라면 내꺼도 채워 줘야지.... 
                if (idx == totalcount)
                {
                    if (wp.Target == 0)
                    {
                        wp.Bearing = (float)Math.Round(GeoCalculate.CalculateBearing(hwp.Location, wp.Location), 2);
                    }
                    else
                    {
                        WayPointModel tp = inst.TPList.Single<WayPointModel>(i => i.Index == pwp.Target);
                        wp.Bearing = (float)Math.Round(GeoCalculate.CalculateBearing(tp.Location, wp.Location), 2);
                    }
                }

                pwp = wp;
                totalDistance += wp.DistanceFromPrev;
            }
            GvarDesignModel.Instance.TotalFlightDistance = totalDistance;
            return totalDistance;
        }

    }
}
