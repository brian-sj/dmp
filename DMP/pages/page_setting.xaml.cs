using System;
using System.Collections.Generic;
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
using Microsoft.Maps.MapControl.WPF;
using GMap.NET.MapProviders;
using System.Net;
using DMP.DataModels;

namespace DMP
{
    /// <summary>
    /// page_setting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageSetting : Page
    {

        // A collection of key/value pairs containing the event name 
        // and the text block to display the event to.
        Dictionary<string, TextBlock> eventBlocks = new Dictionary<string, TextBlock>();
        // A collection of key/value pairs containing the event name  
        // and the number of times the event fired.
        Dictionary<string, int> eventCount = new Dictionary<string, int>();

        /// <summary>
        ///  드래그 할 푸시핀을 저장한다. 
        /// </summary>
        public Pushpin SelectedPushpin { get; set; }

        /// <summary>
        /// 지금 푸시핀 드래그 중이냐?
        /// </summary>
        private bool _dragPin;


        private Vector _mouseToMarker;

        public PageSetting()
        {
            InitializeComponent();

            ControlTemplate template = (ControlTemplate)this.FindResource("CustomPushpinTemplate");
            foreach ( var wp in GvarDesignModel.Instance.WPList )
            {
                Pushpin pin = new Pushpin();
                pin.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
                pin.Location = new Location( wp.Latitude , wp.Longitude );
                pin.Template = template;
                //pin.Content = 10;  // 이내용이 들어 간다. 
                pin.Content = pin.Name;

                pin.ToolTip = "Speed: " + wp.Speed + " , Bearing:" + wp.Bearing;
            }
            //MapWithEvents.MouseDoubleClick  += new EventHandler<Microsoft.Maps.MapControl    MapMouseEventArgs>(MyMap_MouseClick);
        }

        private void MapWithEvents_ViewChangeOnFrame(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_TargetViewChanged(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_ViewChangeStart(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_ModeChanged(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ControlTemplate template = (ControlTemplate)this.FindResource("CustomPushpinTemplate");
            var l = e.GetPosition( MapWithEvents );// ViewportPointToLocation
            Location latlong = MapWithEvents.ViewportPointToLocation(  l );
            //MessageBox.Show(latlong.Latitude + ":::::::" + latlong.Longitude);
            
            Pushpin pin = new Pushpin();
            pin.Name = "WP" + (GvarDesignModel.Instance.ITotalWPCount + 1);
            WayPointModel wp = new WayPointModel();

            wp.Index = (GvarDesignModel.Instance.ITotalWPCount + 1);
            wp.Latitude = latlong.Latitude;
            wp.Longitude = latlong.Longitude;
            wp.Height = DefaultValue.DefaultHeight;
            wp.Name = pin.Name;
            wp.Speed = DefaultValue.DefaultSpeed;
            wp.Bearing = DefaultValue.DefaultBearing; 

            pin.MouseDown += new MouseButtonEventHandler( pin_MouseDown );
            pin.Location = latlong;

            pin.Template = template;
            //pin.Content = 10;  // 이내용이 들어 간다. 
            pin.Content = pin.Name;
            pin.ToolTip = "Speed: " + wp.Speed + " , Bearing:"+wp.Bearing ;

            GvarDesignModel.Instance.WPList.Add( wp );
            
            MapWithEvents.Children.Add( pin );
            //TextBlock tb = new TextBlock();
            //tb.Text = "Test";
            //MapWithEvents.Children.Add( tb  );

            ShowEvent("MapWithEvents_MouseDoubleClick : Lat-"+latlong.Latitude +": log"+ latlong.Longitude );
        }

        void ShowEvent(string eventName)
        {
            // Updates the display box showing the number of times 
            // the wired events occured.
            if (!eventBlocks.ContainsKey(eventName))
            {
                TextBlock tb = new TextBlock();
                tb.Foreground = new SolidColorBrush(
                    Color.FromArgb(255, 128, 255, 128));
                tb.Margin = new Thickness(5);
                eventBlocks.Add(eventName, tb);
                eventCount.Add(eventName, 0);
                eventsPanel.Children.Add(tb);
            }
                eventCount[eventName]++;
                eventBlocks[eventName].Text = String.Format(
                "{0}: [{1} times] {2} (HH:mm:ss:ffff)",
                eventName, eventCount[eventName].ToString(), DateTime.Now.ToString());
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {

            /*
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance.APIKey = "AIzaSyA_QA - AdTPdN7EOW7NIORSb8WZjnhoPWWM";
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;



            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;

            mapView.MinZoom = 2;
            mapView.MaxZoom = 17;
            // whole world zoom
            mapView.Zoom = 2;
            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            mapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;
            */
        }

        private void MapWithEvents_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point p = e.GetPosition( MapWithEvents );
        }


        /// <summary>
        ///  푸시핀 드래그 시작한다... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pin_MouseDown( object sender , MouseButtonEventArgs e )
        {
            e.Handled = true;
            SelectedPushpin = sender as Pushpin;
            _dragPin = true;
            _mouseToMarker = Point.Subtract(
                       MapWithEvents.LocationToViewportPoint(SelectedPushpin.Location),
                       e.GetPosition(MapWithEvents));
        }
        /// <summary>
        ///  푸시핀 마우스 드래그 하고 있는 중간이다.... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapWithEvents_MouseMove(object sender, MouseEventArgs e)
        {
            if ( e.LeftButton == MouseButtonState.Pressed )
            {
                if( _dragPin && SelectedPushpin != null ){
                    SelectedPushpin.Location = MapWithEvents.ViewportPointToLocation(Point.Add( e.GetPosition(MapWithEvents) , _mouseToMarker ));
                    e.Handled = true;

                }
            }
        }
    }
}
