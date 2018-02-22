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
using Microsoft.Win32;
using DMP.util;
using DMP;
using System.Xml.Linq;
using DMP.DataModels;
using MissionPlanner.Comms;
using MissionPlanner;
using log4net;
using System.Reflection;
using MissionPlanner.Utilities;
using MissionPlanner.Mavlink;
using DMP.Resources;

namespace DMP
{
    /// <summary>
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageReady : Page
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private bool skipconnectcheck = false;
        EnumListDictionary vars = new EnumListDictionary();
        public PageReady()
        {
            InitializeComponent();
            
            //cmbLandingStyle.ItemsSource = vars.DpdLandingStyle;
            //cmbDoWhenError.ItemsSource = vars.DpdDoWhenError; 
            //cmbCornerStyle.ItemsSource = vars.DpdCornerType;
            //cmbInitialHeightType.ItemsSource = vars.DpdInitialHeightType;
        }

        private void speedChangedEventHanlder(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            //DMPGVarsModel gvars = new DMPGVarsModel().Instance;
            //gvars.ISpeed = 11;
            //Console.Write("ddd" + gvars.ISpeed );
        }

        private void cornerStyle_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //int j = 0;
        }

        private void cornerStyle_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int i = 0;
        }

        private void cornerStyle_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //int i = 0;
            
        }

        private void landingStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int j = sender.SelectedValue;
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

        #region Button Click Event 
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
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
        #endregion 

        #region MOTORTEST 
        private void btnMotorTest2_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(1, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, GvarDesignModel.Instance.TestThroattle, 3, 0);
        }

        private void btnMotorTest1_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(0, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, GvarDesignModel.Instance.TestThroattle, 3, 0);
        }

        private void btnMotorTest3_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(2, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, GvarDesignModel.Instance.TestThroattle, 3, 0);
        }

        private void btnMotorTest4_Click(object sender, RoutedEventArgs e)
        {
            MainV2.comPort.doMotorTest(3, MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT, GvarDesignModel.Instance.TestThroattle, 3, 0);
        }
        #endregion

        private void SetHome_Click(object sender, RoutedEventArgs e)
        {
            MavLinkAction.WriteHomePosition();
        }

        private void GetHome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Locationwp wp = MainV2.comPort.getHomePosition();
                Dialogs.CustomMessageBox.Show("lat=" + wp.lat + " : lng=" + wp.lng + " : id=" + wp.id + " : alt=" + wp.alt);
            }
            catch (Exception ee ) { DMP.Dialogs.CustomMessageBox.Show("Error", "[GetHomePosition] HomePosition을 읽어 올 수가 없습니다."); }

        }

        private void SetWP1_Click(object sender, RoutedEventArgs e)
        {
            ushort totalCnt = 0;// GetCommandList().Count;
            ushort cnt = 0;
            MAVLinkInterface port = MainV2.comPort;

            try
            {
                MainV2.comPort.giveComport = true;
                cnt = port.getWPCount();
            }catch(Exception) { Console.WriteLine("getWPCount Timeout");  }


            log.InfoFormat("########## 현재 가지고 있는  wp 수는 {0}개", cnt );
            //ushort a = (ushort) (cnt );

            //a = 3; // 일단 3개를 집어 넣는다. 

            bool use_int = false;

            MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

            //MainV2.comPort.setWPTotal(2);
            ushort uploadwpno = 0;

            var commandlist = WayPointConvertUtility.GetCommandList();// GetCommandList();
            totalCnt = (ushort)commandlist.Count;
            totalCnt++;


            try
            {
                port.setWPTotal(totalCnt);

                // home은 0번에다가 넣는다. 
                int homeindex = 0;
                var home = getHomeLocationwp();
                /* Brian :: HomePosition을 먼저 넣어야 한다. */
            var homeans = port.setWP(home, (ushort)homeindex, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                
                for (int a = 1; a <= commandlist.Count ; a++ )
                {
                    if(a == 0)
                    {
                        Console.WriteLine("RequestNo를 가져 오지 못했네...");
                        a++;
                    }
                    var wp = commandlist[a-1];
                    uploadwpno = (ushort)a;
                    var ans = port.setWP(wp, (ushort)uploadwpno, frame, 0, 1, use_int);
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                    {
                        // resend for partial upload
                        port.setWPPartialUpdate((ushort)(uploadwpno), totalCnt );
                        // reupload this point.
                        ans = port.setWP(wp, (ushort)(uploadwpno), frame, 0, 1, use_int);
                        Console.WriteLine(" ERROR RETRY " + ans);
                    }
                    else if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                    {
                        a = port.getRequestedWPNo() - 1;
                    Console.WriteLine("@@@@@@@  invalid sequence retry ");
                        continue;
                    }else if ( ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED )
                    {
                        Dialogs.CustomMessageBox.Show("Error", "WP를 전송할 수 없습니다.");
                        log.InfoFormat("WP 전송실패 {0}번째 index:{1}",a , wp.lat );
                        break;
                    }
                }
                port.setWPACK();
                MainV2.comPort.giveComport = false;
            }catch (Exception ee) { Dialogs.CustomMessageBox.Show("Error" , ee.ToString() ); }
        }





        private Locationwp getHomeLocationwp()
        {
            Locationwp wp = new Locationwp();
            wp.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
            wp.alt = (float)100.0;
            wp.lng = (double)126.44444123456;
            wp.lat = (double)37.44444123456;
            wp.p1 = 0;
            wp.p2 = 0;
            wp.p3 = 0;
            return wp;
        }
        
    }
}
