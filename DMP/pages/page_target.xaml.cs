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
using DMP.util;
using DMP.Controls;
using DMP.DataModels;
using Microsoft.Maps.MapControl.WPF;
using System.Xml.Linq;
using Microsoft.Win32;
using DMP.Controls.pushpin;

namespace DMP
{
    /// <summary>
    /// Page2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageTarget : Page
    {


        private MapPolyline polyline = new MapPolyline();
        private MapLayer mapLayer = new MapLayer();
        public PageTarget()
        {
            InitializeComponent();

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



            //MapLayer myMapLayer = new MapLayer();
            //MapWithEvents.Children.Add( myMapLayer );

            LocationCollection myLocationColl = new LocationCollection();


            //PushpinOptions = new PushpinOptions();
            /// target Point를 찍어 주자.
            /// 
            foreach (var item in GvarDesignModel.Instance.TPList)
            {
                DMPPushpin pin = new DMPPushpin(ref MapWithEvents , ref polyline , ref mapLayer );
                pin.Location = new Location(item.Latitude, item.Longitude);
                myLocationColl.Add( pin.Location );
                pin.Template = ttemplate; 
                pin.Content = item.Name; 
                pin.Name = "_"+item.Index.ToString();
                pin.Idx = item.Index;
                
                /// 클릭하면 레이어 보여줌. 
                pin.MouseDown += new MouseButtonEventHandler( pushpinHighlight );
                MapWithEvents.Children.Add(pin);
                
                
            }
            
            var bounds = new LocationRect( myLocationColl);
            //MapWithEvents.SetView(bounds);
        }
        
        void pushpinHighlight( object sender , MouseEventArgs e )
        {
            Console.WriteLine("Pushpin clicked");

            var pin = sender as DMPPushpin;
            if(pin != null)
            {
                MapLayer.SetPosition( ContentPopup,pin.Location);
                MapLayer.SetPositionOffset(ContentPopup , new Point(15,-50) );
                ContentPopupText.Text = "MyPush" + pin.Idx.ToString() ;
                ContentPopup.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        ///  새로운 모델을 하나 만들고 UI 도 하나 만들어 준다....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            int tpCount = DMP.DataModels.GvarDesignModel.Instance.TargetPointList.Count;
            tpCount++;

            DMP.DataModels.GvarDesignModel.Instance.TargetPointList.Add(
                new TargetPointModel
                {
                    Index = tpCount ,
                    Latitude = 100 + tpCount,
                    Altitude = 100 + tpCount,
                    Longitude = 100 + tpCount,
                    Name = "Target Point" + tpCount.ToString(),
                }

                );
            */
        }

        private void MapWithEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /// Double Click 할때 맵이 줌업되지 않게 e.Handled = true 처리 한다. 
            e.Handled = true;

            int tpCount = DMP.DataModels.GvarDesignModel.Instance.TPList.Count;
            tpCount++;

            // Click 시 위치를 가져 온다. 
            var l = e.GetPosition(MapWithEvents);// ViewportPointToLocation
            Location latlong = MapWithEvents.ViewportPointToLocation(l);

            // Template는 TargetPushpin 으로 가져 온다. 
            ControlTemplate template = (ControlTemplate)this.FindResource("CustomTPushpinTemplate");

            Pushpin pin = new Pushpin();
            pin.Name = "TP" + tpCount.ToString();
            TargetPointModel tp = new TargetPointModel();

            tp.Index = (GvarDesignModel.Instance.ITotalTPCount + 1);
            tp.Latitude = latlong.Latitude;
            tp.Longitude = latlong.Longitude;
            tp.Height = DefaultValue.DefaultTargetAltitude;
            tp.Name = pin.Name;

            pin.MouseDown += new MouseButtonEventHandler(pin_MouseDown);
            pin.Location = latlong;

            pin.Template = template;
            //pin.Content = 10;  // 이내용이 들어 간다. 
            pin.Content = pin.Name;
            pin.ToolTip = "Altitude: " + tp.Height;

            //pin.LayoutTransform.Transform.Angle 


            GvarDesignModel.Instance.TPList.Add(tp);
            MapWithEvents.Children.Add(pin);
            //targetListControl.UpdateLayout(); 
        }

        private void pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

        }
    
    }
}
