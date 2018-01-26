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
using GMap.NET.WindowsPresentation;

namespace DMP.pages
{
    /// <summary>
    /// page_help.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageHelp : Page
    {
        public PageHelp()
        {
            InitializeComponent();
            //var gmap = new GMap.NET.WindowsPresentation.GMapControl();
            //this.AddVisualChild(gmap );
            
        }
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // choose your provider here
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
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
    }


}
