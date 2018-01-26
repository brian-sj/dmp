using DMP.Controls.pushpin;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

            if (  GvarDesignModel.Instance.HomePosition != null )
            {
                var hpin = new DMPPushpin(DMap);
                hpin.Location = new Location(GvarDesignModel.Instance.HomePosition.Latitude, GvarDesignModel.Instance.HomePosition.Longitude);
                hpin.Template = Htemplate;
                hpin.Idx = 0;
                hpin.Content = "";
                hpin.WPM = GvarDesignModel.Instance.HomePosition;
                DmlPushpin.Children.Add(hpin);
                DPushpins.Add(hpin);
            }

            /// way point 를 찍어 주자... 
            foreach (var item in GvarDesignModel.Instance.WPList)
            {
                var pin = new DMPPushpin( DMap ); // ref MapWithEvents, ref polyline, ref DmapPushPinLayer);
                pin.Location = new Location(item.Latitude, item.Longitude);
                pin.Template = Wtemplate;
                pin.Content = String.Format("{0}({1})", item.Index, item.Height);
                pin.PointType = (int)PointType.WAYPOINT;
                pin.Idx = item.Index;
                pin.Alt = item.Height;
                pin.WPM = item;
                //pin.MouseDown += new MouseButtonEventHandler(pushpinHighlight);
                DmlPushpin.Children.Add(pin);
                DPushpins.Add(pin);
            }
            /// target Point를 찍어 주자.
            foreach (var item in GvarDesignModel.Instance.TPList)
            {
                var pin = new DMPPushpin(DMap); // ref MapWithEvents, ref polyline, ref DmapPushPinLayer);
                pin.Location = new Location(item.Latitude, item.Longitude);
                pin.Template = Ttemplate;
                pin.Content = String.Format("{0}({1})", item.Index, item.Height);
                pin.Alt = item.Height;
                pin.Idx = item.Index;
                pin.PointType = (int)PointType.TARGET;
                pin.WPM = item;
                DmlPushpin.Children.Add(pin);// MapWithEvents.Children.Add(pin);
                DPushpins.Add(pin);
            }
            /// DrawPolyLine 과 CalculationEachDistance는 항상 같이 다닌다. 
            DrawPolyLine();
            GvarDesignModel.Instance.CalculationEachDistance();
        }
    }
}
