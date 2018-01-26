using log4net;
using MissionPlanner;
using MissionPlanner.Comms;
using MissionPlanner.Mavlink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// page_connect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageConnect : Page
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool skipconnectcheck = false;
        public PageConnect()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button_Click
            
        }


        private void connect(MAVLinkInterface comPort , string portname , string baud )
        {
            Console.WriteLine("  Connect ... ");
            log.Debug("Start connect ");
            MainV2.instance.doConnect( comPort , portname , baud );
            lblCompID.Content = MainV2.comPort.compidcurrent.ToString(); 
            lblSysID.Content = MainV2.comPort.sysidcurrent.ToString(); 
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

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            connect(MainV2.comPort, "serial", "115200");
        }




        // 메세지 받으면 핸들러를  저장했다가 필요 없어지면 unsubscribe 한다. 
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsub1;

        private bool ReceviedPacket(MAVLink.MAVLinkMessage packet)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                MainV2.comPort.DebugPacket(packet, true);

            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS)
            {
                var obj = packet.ToStructure<MAVLink.mavlink_mag_cal_progress_t>();

                /*
                lock (this.mprog)
                {
                    this.mprog.Add(packet);
                }
                */

                Console.WriteLine("received Packet:" + obj.completion_pct.ToString(), true);


                lbl_obmagresult.Dispatcher.Invoke(new Action(delegate {
                    lbl_obmagresult.Content += "_" + obj.completion_pct.ToString();


                }));

                if (obj.compass_id == 0)
                    progressBar1.Dispatcher.Invoke(new Action(delegate { progressBar1.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;
                if (obj.compass_id == 1)
                    progressBar2.Dispatcher.Invoke(new Action(delegate { progressBar2.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;
                if (obj.compass_id == 2)
                    progressBar3.Dispatcher.Invoke(new Action(delegate { progressBar3.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;

                return true;
            }
            else if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_REPORT)
            {
                /*
                lock (this.mrep)
                {
                    this.mrep.Add(packet);
                }
                */

                return true;
            }
            /// 이것은 여기에 있을것이 아닌데 테스트를 위해서 넣는다. 
            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
            {

            }

            return true;
        }





        private void btnLogMonEnd_Click(object sender, RoutedEventArgs e)
        {
            MainV2.instance.readPacketThreadStop();
            MainV2.comPort.UnSubscribeToPacketType(packetsub1);  // 필요 없을때는 뺀다. 
        }

        private void btnLogMonStart_Click(object sender, RoutedEventArgs e)
        {
            MainV2.instance.readPacketThreadRun();
            //motor index , speed , int time , motorcount 
            //packetsub1 = MainV2.comPort.SubscribeToPacketType(ReceviedPacket);
        }

        private void btnMotorTest2_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(1, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, 10, 3, 0);
        }

        private void btnMotorTest1_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(0, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, 10, 3, 0);
        }

        private void btnMotorTest3_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(2, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, 10, 3, 0);
        }

        private void btnMotorTest4_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(3, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, 10, 3, 0);
        }

        private void Button_test_1(object sender, RoutedEventArgs e)
        {
            DMP.Dialogs.CustomMessageBox.Show( "Hello", "caption ", Dialogs.MessageBoxType.ConfirmationWithYesNo);

        }
    }
}
