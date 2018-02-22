
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using DMP.Dialogs;
using System;
using System.ComponentModel;

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

        private void btnTestProgress_Click(object sender, RoutedEventArgs e)
        {
            //ProgressDialogTestWithCancelButtonAndProgressDisplay();
            int millisecondsTimeout = 250;

            ProgressDialogResult result = ProgressDialog.Execute( Application.Current.MainWindow , "Loading data", (bw, we) => {
                
                for (int _i = 1; _i <= 200; _i++)
                {
                    if (ProgressDialog.ReportWithCancellationCheck(bw, we, i, "Executing step {0}/20...", i))
                        return;

                    Thread.Sleep(millisecondsTimeout);
                }
                
                //bool a =ProgressDialog.ReportWithCancellationCheck(bw, we, i * 5, "Executing step {0}/20...", i);

                _bw = bw;
                _we = we;

                // So this check in order to avoid default processing after the Cancel button has been pressed.
                // This call will set the Cancelled flag on the result structure.
                ProgressDialog.CheckForPendingCancellation(bw, we);

            }, new ProgressDialogSettings(true, true, false));


            // ProgressDialogResult result = ProgressDialog.Execute(this, "Loading data" ,());

            if (result.Cancelled)
                MessageBox.Show("ProgressDialog cancelled.");
            else if (result.OperationFailed)
                MessageBox.Show("ProgressDialog failed.");
            else
                MessageBox.Show("ProgressDialog successfully executed.");
        }

        int i = 0;
        BackgroundWorker _bw;
        DoWorkEventArgs _we;

        private void btnTestAddProgress_Click(object sender, RoutedEventArgs e)
        {
            i += 5;
            //ProgressDialog.ReportWithCancellationCheck( _bw, _we , i ,"Loading data {0}", i  );
        }
    }
}
