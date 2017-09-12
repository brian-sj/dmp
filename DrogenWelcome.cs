using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MissionPlanner.Comms;
using MissionPlanner.Utilities;
using MissionPlanner.Mavlink;
using System.Device.Location;

namespace MissionPlanner
{
    public partial class DrogenWelcome : Form
    {

        private bool skipconnectcheck = false;
        public DrogenWelcome()
        {
            InitializeComponent();
        }

        private void DrogenWelcome_Load(object sender, EventArgs e)
        {
            Program.Splash.Close();

        }

        private void connect()
        {
            MainV2.comPort.BaseStream = new SerialPort();

            MainV2.comPort.MAV.cs.ResetInternals();

            //cleanup any log being played
            MainV2.comPort.logreadmode = false;
            if (MainV2.comPort.logplaybackfile != null)
                MainV2.comPort.logplaybackfile.Close();
            MainV2.comPort.logplaybackfile = null;


            try
            {
                MainV2.comPort.BaseStream.BaudRate = 115200;
                MainV2.comPort.BaseStream.PortName = "COM3";
                MainV2.comPort.Open(false, skipconnectcheck);
            }
            catch (Exception ee)
            {
                Console.WriteLine("error " + ee.Message);
            }

            if (!MainV2.comPort.BaseStream.IsOpen)
            {

                Console.WriteLine("0000000   not opened ");

            }

            // get all the params
            foreach (var mavstate in MainV2.comPort.MAVlist)
            {
                MainV2.comPort.sysidcurrent = mavstate.sysid;
                MainV2.comPort.compidcurrent = mavstate.compid;
                MainV2.comPort.getParamList();
            }

            lblCompID.Text = MainV2.comPort.compidcurrent.ToString();
            lblSysID.Text = MainV2.comPort.sysidcurrent.ToString();

        }
        
        private void btngetHomePosition_Click(object sender, EventArgs e)
        {
            Locationwp wp = MainV2.comPort.getHomePosition();
            Console.WriteLine("############  Get Home Position   ....");
            doLog(   "WP :" + wp.p1 + ": lat:" + wp.lat + " lng:" + wp.lng   + " alt:" + wp.alt );
        }
        

        private void btngetWayPointCnt_Click(object sender, EventArgs e)
        {
            ushort cnt = MainV2.comPort.getWPCount();
            Console.WriteLine("############WayPoint cnt  ....");
            doLog( "############WayPoint cnt  ...." + cnt.ToString() );
        }

        private void doLog( object text )
        {
            txtLog.Text += text.ToString() + " \n ";
        }

        private void btnSetWP_Click(object sender, EventArgs e)
        {
            ushort cnt = MainV2.comPort.getWPCount();
            MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
            //frame = MAVLink.MAV_FRAME.GLOBAL;

            Locationwp wp = new Locationwp();
            wp.id = (ushort) MAVLink.MAV_CMD.WAYPOINT ;
                        
            wp.alt = (float)100.0;
            wp.lng = 126.4196777 ;
            wp.lat = 37.4675692  ;

            wp.p1 = 0;
            wp.p2 = 0;
            wp.p3 = 0;
            //cnt++;

            bool use_int = false ;
            ushort a = ++cnt; 

            //MAVLink.MAV_MISSION_RESULT result =  MainV2.comPort.setWP(   wp  , cnt , frame  );
            var homeans = MainV2.comPort.setWP( wp , a, frame, 0, 1, use_int);

            //MainV2.comPort.doCommand(MAVLink.MAV_CMD_DO)
        }

        private void btnReadPacket_Click(object sender, EventArgs e)
        {
            MAVLink.MAVLinkMessage msg = MainV2.comPort.readPacket();
            String tomsg = "";

            uint a = msg.msgid;

            if (msg.Length > 5 && msg.sysid == MainV2.comPort.sysidcurrent && msg.compid == MainV2.comPort.compidcurrent)
            {

                ////// 압력을 넣는다.....

                if (msg.msgid == (byte) MAVLink.MAVLINK_MSG_ID.SCALED_PRESSURE)
                {
                    MAVLink.mavlink_scaled_pressure_t data = msg.ToStructure<MAVLink.mavlink_scaled_pressure_t>();

                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    lblPress.Text = data.press_abs.ToString()  ;
                    lblTemp.Text  = data.temperature.ToString() ;
                }



                if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.VFR_HUD)
                {
                    MAVLink.mavlink_vfr_hud_t data = msg.ToStructure<MAVLink.mavlink_vfr_hud_t>();

                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    lblAirSpeed.Text = data.airspeed.ToString();
                    lblHeading.Text = data.groundspeed.ToString();

                    lblAlt.Text = data.groundspeed.ToString();
                    lblThrottle.Text = data.groundspeed.ToString();
                    lblHeading.Text = data.groundspeed.ToString();

                }












            }
            MainV2.comPort.DebugPacket( msg , ref tomsg );

            //doLog("ddd" + Encoding.Default.GetString( msg.buffer) );
            doLog("ddd" + tomsg );
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var item = MainV2.comPort.MAVlist[MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent];
                //doLog( item.apname +":"+ item.ToString()  );
                // MAVlist[sysid,compid].param
                foreach (var it in item.param) {
                    doLog( it.Name +":"+ it.Value + " \n" );
                }
            
        }

        private void btnGetParam_Click(object sender, EventArgs e)
        {
            var item = MainV2.comPort.MAVlist[MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent];

            foreach ( var it in item.param  )
            {
                if( it.Name == txtParamName.Text  )
                {
                    txtParamValue.Text = it.Value.ToString();
                }
            }
        }

        private void btnSetParam_Click(object sender, EventArgs e)
        {
            String paramName = txtParamName.Text;
            Double  paramValue = 0;
            try
            {
                paramValue = Double.Parse(txtParamValue.Text);

                bool result = MainV2.comPort.setParam(  paramName , paramValue , false );

                if (result )
                {
                    CustomMessageBox.Show(" 저장이 성공적으로 진행되었습니다. .");
                }
                else
                {
                    CustomMessageBox.Show(" 저장이 문제가 있습니다. ");
                }

            }
            catch (Exception ee ) {

                CustomMessageBox.Show(" 설정하려는 값에 문제가 있습니다. 숫자로 입력해 주세요.");
                return ;
            }


        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            // doCommand(MAV_CMD actionid, float p1, float p2, float p3, float p4, float p5, float p6, float p7, bool requireack = true)
            float yaw_angle = 10;
            float min_pitch = 10;

            float lat = 0;
            float lng = 0;
            float alt = 10;
            
            bool ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, min_pitch  , 0,0, yaw_angle ,  lat , lng , alt  );
            if(ret)
            {
                CustomMessageBox.Show("Success");
            }else
            {
                CustomMessageBox.Show("Fail");
            }

        }

        private void btnLand_Click(object sender, EventArgs e)
        {
            float yaw_angle = 10;
            float min_pitch = 10;

            float lat = 0;
            float lng = 0;
            float alt = 0;
            bool ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.LAND, 0, 0, 0, yaw_angle, lat, lng, alt);
            
            
            if (ret)
            {
                CustomMessageBox.Show("Success");
            }
            else
            {
                CustomMessageBox.Show("Fail");
            }
        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            MainV2.comPort.doReboot();
        }


        /// <summary>
        /// 찰칵.. 카메라를 이용해 찍는다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {



            MainV2.comPort.setDigicamControl(true); 




        }

        /// <summary>
        ///  속도를 정합시다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            /// MAV_CMD_DO_CHANGE_SPEED
            /// param1 SpeedType ( 0 : air speed , 1: ground speed )
            /// param2 speed m/s    나머지 파라미터들은 다 0 입니다. 
            /// 

            bool ret = false;
            float speed = (float)20;
            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_CHANGE_SPEED , 0 , speed ,0,0,0,0,0 );
            if (ret)
            {
                CustomMessageBox.Show("Success -> 20 m/s");
            }
            else
            {
                CustomMessageBox.Show("Fail");
            }
        }
        /// <summary>
        /// 긴급회항 ... 가까운 Rally Point 또는 Home Location 으로 온다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRTL_Click(object sender, EventArgs e)
        {
            //MAV_CMD_NAV_RETURN_TO_LAUNCH
            /// param 없음. 
            bool ret = false;
            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);
            if (ret)
            {
                CustomMessageBox.Show("Success -> 긴급회항");
            }
            else
            {
                CustomMessageBox.Show("Fail");
            }
        }
        /// <summary>
        ///  머리를 돌려라 Yaw 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYaw_Click(object sender, EventArgs e)
        {
            /// MAV_CMD_CONDITION_YAW
            /// param1 : Deg   param4 번째가 0 이면 절대 각도(북쪽이 0이다.) 1이면 상대각도 
            /// param2 는 방향 전환 속도 인제 지원하지 않는다. 
            /// param3 은 방향 , param4는 절대,상대 


            bool ret = false;

            float deg = 30;
            float dir = 1; //   1=> cW 시계방향   ccw => -1  시계 반대 방향 
            float rel_abs = 0; // 0 절대  1상대 


            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.CONDITION_YAW, deg, 0, dir, rel_abs, 0, 0, 0);
            if (ret)
            {
                CustomMessageBox.Show("Success ->머리를 틀어라 각도를 30도로 ");
            }
            else
            {
                CustomMessageBox.Show("Fail");
            }



        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //
            // GeoCoordinate

            var sLatitude = 48.672309;
            var sLongitude = 15.695585;


            var eLatitude = 48.237867 ;
            var eLongitude = 16.389477;

            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            lblDistance.Text = sCoord.GetDistanceTo(eCoord) .ToString() ;
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            double theta1;
            Point p1;
            Point p2;     

            p1 = new Point(5 , 5) ;
            p2 = new Point(0 , 10) ;
            theta1 = GetAngle( 0.005,0.005 ,   0,0.010  );  // -45도 나와야 하네... 

            p1 = new Point(0, 0);
            p2 = new Point(10, 10);
            theta1 = GetAngle(0,0   ,0.010,0.010);   // 45도 나와야 하네. 

            p1 = new Point(5, 10);
            p2 = new Point(0, 10);       
            theta1 = GetAngle(0.005,0.010,    0,0.010);    // 0도 나와야 하나? 

            p1 = new Point(5, 5);
            p2 = new Point(5, 10);
            theta1 = GetAngle(0.005,0.005,    0.005,0.010);  // 분모가 0 인경우는 어떻게 나오나?
            
        }

        /// <summary>
        /// 거리가 짧으니까 지구가 구체라는 가정은 하지 않는다. 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double GetAngle( double  lat1 , double lng1 , double lat2 , double lng2  )
        {

            double dlat1 = DegreeToRadian(lat1);
            double dlat2 = DegreeToRadian(lat2);

            double dlng = DegreeToRadian( lng2) -DegreeToRadian(lng1);

            double y = Math.Sin(dlng) * Math.Cos(dlat2);
            double x = Math.Cos(dlat1) * Math.Sin(dlat2) - Math.Sin(dlat1) * Math.Cos(dlat2) * Math.Cos(dlng);
            double brng = Math.Atan2(y, x);
            return (RadianToDegree(brng) + 360) % 360;

        }


        private double DegreeToRadian(double angle) { return Math.PI * angle / 180.0;}
        private double RadianToDegree(double angle) { return 180.0 * angle / Math.PI; }

        private void btnSetHome_Click(object sender, EventArgs e)
        {
            ushort cnt = MainV2.comPort.getWPCount();
            MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
            //frame = MAVLink.MAV_FRAME.GLOBAL;

            Locationwp wp = new Locationwp();
            wp.id = (ushort)MAVLink.MAV_CMD.TAKEOFF;

            wp.alt = (float)100.0;
            wp.lng = 126.4196777;
            wp.lat = 37.4675692;

            wp.p1 = 0;
            wp.p2 = 0;
            wp.p3 = 0;
            //cnt++;

            bool use_int = false;
            ushort a = ++cnt;

            //MAVLink.MAV_MISSION_RESULT result =  MainV2.comPort.setWP(   wp  , cnt , frame  );
            var homeans = MainV2.comPort.setWP(wp, a, frame, 0, 1, use_int);
        }
    }
}
