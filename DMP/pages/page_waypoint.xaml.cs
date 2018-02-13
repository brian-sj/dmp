using DMP.Controls;
using DMP.Controls.pushpin;
using DMP.DataModels;
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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMP
{
    /// <summary>
    /// Page2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageWp : Page
    {
        ObservableCollection<DMPPushpin> pushpins = new ObservableCollection<DMPPushpin>();



        private MapPolyline polyline = new MapPolyline();
        //private MapLayer DmapLayer = new MapLayer();
        private int pointType = (int)DMP.PointType.WAYPOINT;
        public PageWp( int pointType)
        {
            this.pointType = pointType;
            InitializeComponent();
            InitializePage();
        }
        public PageWp()
        {
            InitializeComponent();
            InitializePage();
        }
        private void InitializePage() { 
            var mapModel = MapDesignModel.Instance;
            mapModel.DMap = MapWithEvents;
            ControlTemplate ttemplate = (ControlTemplate)this.FindResource("CustomTPushpinTemplate");
            ControlTemplate wtemplate = (ControlTemplate)this.FindResource("CustomPushpinTemplate");
            ControlTemplate htemplate = (ControlTemplate)this.FindResource("CustomHPushpinTemplate");
            mapModel.Ttemplate = ttemplate;
            mapModel.Wtemplate = wtemplate;
            mapModel.Htemplate = htemplate;
            mapModel.DmlPolyline = DmapPolylineLayer; 
            mapModel.DmlPushpin = DmapPushPinLayer;
            mapModel.DmlFlight = DmapFlightLayer;
            /// 잠깐 멈춤 
            mapModel.LoadPushpinFromWaypointList();
        }

        private void MapWithEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            int tpCount = DMP.DataModels.GvarDesignModel.Instance.ITotalWPCount;
            tpCount++;

            // Click 시 위치를 가져 온다. 
            var l = e.GetPosition(MapWithEvents);// ViewportPointToLocation
            Location latlong = MapWithEvents.ViewportPointToLocation(l);

            // Template는 TargetPushpin 으로 가져 온다. 
            //ControlTemplate template = (ControlTemplate)this.FindResource("CustomPushpinTemplate");
            //ControlTemplate ttemplate = (ControlTemplate)this.FindResource("CustomTPushpinTemplate");

            WayPointModel wp = new WayPointModel();

            if( GvarDesignModel.Instance.PointType == (int) DMP.PointType.WAYPOINT)
            {
                wp.Index = (GvarDesignModel.Instance.WPList.Count + 1);
                wp.Latitude = latlong.Latitude;
                wp.Longitude = latlong.Longitude;
                wp.Name = wp.Index.ToString();
                GvarDesignModel.Instance.WPList.Add(wp);
            }
            else if(GvarDesignModel.Instance.PointType == (int)DMP.PointType.TARGET)
            {
                wp.Index = (GvarDesignModel.Instance.TPList.Count + 1);
                wp.Latitude = latlong.Latitude;
                wp.Longitude = latlong.Longitude;
                wp.Name = wp.Index.ToString();
                GvarDesignModel.Instance.TPList.Add(wp);
            }

            MapDesignModel.Instance.LoadPushpinFromWaypointList();
            MapDesignModel.Instance.DrawPolyLine();
            //var dist = MapDesignModel.Instance.CalculationEachDistance();
        }

        /// <summary>
        ///  tool Tip 처럼 정보를 보여준다.. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pushpinHighlight(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Pushpin clicked");
            var pin = sender as DMPPushpin;
            if (pin != null)
            {
                MapLayer.SetPosition(ContentPopup, pin.Location);
                MapLayer.SetPositionOffset(ContentPopup, new Point(15, -50));
                ContentPopupText.Text = String.Format("{0}-{1:0.00}" , pin.Index, pin.WPM.DistanceFromPrev);//"MyPush" + pin.Idx.ToString() + pin.WPM.DistanceFromPrev +":";
                ContentPopup.Visibility = Visibility.Visible;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
