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
            timer1.Interval = 50;
            timer1.Tick += new EventHandler(timer1_Tick);
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
            doLog("WP :" + wp.p1 + ": lat:" + wp.lat + " lng:" + wp.lng + " alt:" + wp.alt);
        }


        private void btngetWayPointCnt_Click(object sender, EventArgs e)
        {
            ushort cnt = MainV2.comPort.getWPCount();
            Console.WriteLine("############WayPoint cnt  ....");
            doLog("############WayPoint cnt  ...." + cnt.ToString());
        }

        private bool  mandatory = false;
        private void doLog(object text)
        {
            if (!mandatory)
            {
                txtLog.Text += text.ToString() + " \n ";
            }
            
        }

        private void doLog(object text , bool mandatory )
        {
            //txtLog.Text = "";
            txtLog.Text += text.ToString() + " \n ";
        }

        private void btnSetWP_Click(object sender, EventArgs e)
        {
            ushort cnt = MainV2.comPort.getWPCount();
            MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
            //frame = MAVLink.MAV_FRAME.GLOBAL;

            Locationwp wp = new Locationwp();
            wp.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;

            wp.alt = (float)100.0;
            wp.lng = 126.4196777;
            wp.lat = 37.4675692;

            wp.p1 = 0;
            wp.p2 = 0;
            wp.p3 = 0;
            //cnt++;

            bool use_int = false;


            ushort a = cnt;  // 들어가는 시퀀스를 정하고... 
            cnt++;   // 전체 숫자는 한개 늘리고... 

            //MAVLink.MAV_MISSION_RESULT result =  MainV2.comPort.setWP(   wp  , cnt , frame  );
            MainV2.comPort.setWPTotal(a);
            var homeans = MainV2.comPort.setWP(wp, a, frame, 0, 1, use_int);

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

                if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.SCALED_PRESSURE)
                {
                    MAVLink.mavlink_scaled_pressure_t data = msg.ToStructure<MAVLink.mavlink_scaled_pressure_t>();

                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    lblPress.Text = data.press_abs.ToString();
                    lblTemp.Text = data.temperature.ToString();
                }else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.VFR_HUD)
                {
                    MAVLink.mavlink_vfr_hud_t data = msg.ToStructure<MAVLink.mavlink_vfr_hud_t>();
                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    lblAirSpeed.Text = data.airspeed.ToString();
                    lblHeading.Text = data.heading.ToString();
                    lblAlt.Text = data.alt.ToString();
                    lblThrottle.Text = data.throttle.ToString();
                    //lblHeading.Text = data.groundspeed.ToString();

                }else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
                {
                    var message = ASCIIEncoding.ASCII.GetString(msg.ToStructure<MAVLink.mavlink_statustext_t>().text);
                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                }else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.BATTERY_STATUS)
                {
                    MAVLink.mavlink_battery_status_t data = msg.ToStructure<MAVLink.mavlink_battery_status_t>();
                    pgsBattery.Value = data.battery_remaining;
                }

            }
            MainV2.comPort.DebugPacket(msg, ref tomsg);

            //doLog("ddd" + Encoding.Default.GetString( msg.buffer) );
            doLog("ddd" + tomsg);
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
            foreach (var it in item.param)
            {
                doLog(it.Name + ":" + it.Value + " \n");
            }

        }

        private void btnGetParam_Click(object sender, EventArgs e)
        {
            var item = MainV2.comPort.MAVlist[MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent];

            foreach (var it in item.param)
            {
                if (it.Name == txtParamName.Text)
                {
                    txtParamValue.Text = it.Value.ToString();
                }
            }
        }

        private void btnSetParam_Click(object sender, EventArgs e)
        {
            String paramName = txtParamName.Text;
            Double paramValue = 0;
            try
            {
                paramValue = Double.Parse(txtParamValue.Text);

                bool result = MainV2.comPort.setParam(paramName, paramValue, false);

                if (result)
                {
                    CustomMessageBox.Show(" 저장이 성공적으로 진행되었습니다. .");
                }
                else
                {
                    CustomMessageBox.Show(" 저장이 문제가 있습니다. ");
                }

            }
            catch (Exception )
            {

                CustomMessageBox.Show(" 설정하려는 값에 문제가 있습니다. 숫자로 입력해 주세요.");
                return;
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

            bool ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, min_pitch, 0, 0, yaw_angle, lat, lng, alt);
            if (ret)
            {
                CustomMessageBox.Show("Success");
            }
            else
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
            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_CHANGE_SPEED, 0, speed, 0, 0, 0, 0, 0);
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

        /// <summary>
        /// 두 점간 거리는 ??
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_2(object sender, EventArgs e)
        {
            //
            // GeoCoordinate

            var sLatitude = 48.672309;
            var sLongitude = 15.695585;


            var eLatitude = 48.237867;
            var eLongitude = 16.389477;

            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            lblDistance.Text = sCoord.GetDistanceTo(eCoord).ToString();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            double theta1;
            Point p1;
            Point p2;

            p1 = new Point(5, 5);
            p2 = new Point(0, 10);
            theta1 = GetAngle(0.005, 0.005, 0, 0.010);  // -45도 나와야 하네... 

            p1 = new Point(0, 0);
            p2 = new Point(10, 10);
            theta1 = GetAngle(0, 0, 0.010, 0.010);   // 45도 나와야 하네. 

            p1 = new Point(5, 10);
            p2 = new Point(0, 10);
            theta1 = GetAngle(0.005, 0.010, 0, 0.010);    // 0도 나와야 하나? 

            p1 = new Point(5, 5);
            p2 = new Point(5, 10);
            theta1 = GetAngle(0.005, 0.005, 0.005, 0.010);  // 분모가 0 인경우는 어떻게 나오나?

        }

        /// <summary>
        /// 거리가 짧으니까 지구가 구체라는 가정은 하지 않는다. 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double GetAngle(double lat1, double lng1, double lat2, double lng2)
        {

            double dlat1 = DegreeToRadian(lat1);
            double dlat2 = DegreeToRadian(lat2);

            double dlng = DegreeToRadian(lng2) - DegreeToRadian(lng1);

            double y = Math.Sin(dlng) * Math.Cos(dlat2);
            double x = Math.Cos(dlat1) * Math.Sin(dlat2) - Math.Sin(dlat1) * Math.Cos(dlat2) * Math.Cos(dlng);
            double brng = Math.Atan2(y, x);
            return (RadianToDegree(brng) + 360) % 360;

        }


        private double DegreeToRadian(double angle) { return Math.PI * angle / 180.0; }
        private double RadianToDegree(double angle) { return 180.0 * angle / Math.PI; }

        /// <summary>
        /// SET HOME 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetHome_Click(object sender, EventArgs e)
        {
            /*
             * 
             * 이거는 한번 테스트 해본것임. 
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
            */

            Locationwp wp = new Locationwp();
            wp.id = (ushort)MAVLink.MAV_CMD.TAKEOFF;

            wp.alt = (float)5.0;
            wp.lng = 126.4196777;
            wp.lat = 37.4675692;



            bool ret = false;
            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_SET_HOME, 0, 0, 0, 0, (float)wp.lat, (float)wp.lng, wp.alt);
            if (ret)
            {
                CustomMessageBox.Show("Success -> 126.4196777 : 37.4675692: 5m");
            }
            else
            {
                CustomMessageBox.Show("Fail");
            }

        }



        private List<MAVLink.MAVLinkMessage> mprog = new List<MAVLink.MAVLinkMessage>();
        private List<MAVLink.MAVLinkMessage> mrep = new List<MAVLink.MAVLinkMessage>();

        /// <summary>
        ///  나침반 Calibration 할때 MAG_CAL_PROGRESS  하구 MAG_CAL_REPORT 값을 받아서는 두개의 리스트에 넣는다. ....
        ///  이는 나중에 갯수를 꺼내서 확인 해야 한다.  메세지에는 complete_pct 가 존재 한다. 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private bool ReceviedPacket(MAVLink.MAVLinkMessage packet)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                MainV2.comPort.DebugPacket(packet, true);

            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS)
            {
                var obj =packet.ToStructure<MAVLink.mavlink_mag_cal_progress_t>();
                lbl_obmagresult.Text += "_"+ obj.completion_pct.ToString() ;
                lock (this.mprog)
                {
                    this.mprog.Add(packet);
                }

                doLog("received Packet:"+obj.completion_pct.ToString(), true);

                if (obj.compass_id == 0)
                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = obj.completion_pct));//progressBar1.Value = obj.completion_pct;
                if (obj.compass_id == 1)
                    progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = obj.completion_pct));//progressBar2.Value = obj.completion_pct;
                if (obj.compass_id == 2)
                    progressBar3.Invoke((MethodInvoker)(() => progressBar3.Value = obj.completion_pct));//progressBar3.Value = obj.completion_pct;

                return true;
            }
            else if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_REPORT)
            {
                lock (this.mrep)
                {
                    this.mrep.Add(packet);
                }

                return true;
            }


            /// 이것은 여기에 있을것이 아닌데 테스트를 위해서 넣는다. 
            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
            {
                
            }

            return true;
        }


        /// <summary>
        ///  Mag Calibration 하는데 사용하는 변수 이다.... 
        /// </summary>
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsub1;
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsub2;
        Timer timer1 = new Timer();


        /// <summary>
        /// Mag Calibration 하는 버튼을 클릭한다...   나중에 ConfigHWCompass.cs 파일을 참조 한면 된다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMagCalib_Click(object sender, EventArgs e)
        {
            //MagCalib.DoGUIMagCalib();

            try
            {
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_START_MAG_CAL, 0, 1, 1, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                this.LogError(ex);
                CustomMessageBox.Show("Failed to start MAG CAL, check the autopilot is still responding.\n" + ex.ToString(), Strings.ERROR);
                return;
            }

            mprog.Clear();
            mrep.Clear();
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;

            packetsub1 = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS, ReceviedPacket);
            packetsub2 = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MAG_CAL_REPORT, ReceviedPacket);

            btnMagcali_Accept.Enabled = true;
            btnMagcali_cancel.Enabled = true;

            

            //timer1.Start();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {


            btnReadPacket_Click(sender , e );
            calcMagCali();
        }

        /// <summary>
        /// 원래 초당 1번 Timer 돌려야 하는데 화면 볼라고 버튼 이벤트에 걸었다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalcMagCali_Click(object sender, EventArgs e)
        {

            calcMagCali();
        }
        private void calcMagCali() { 
            lbl_obmagresult.Text = "";//.Clear();
            int compasscount = 0;
            int completecount = 0;
            lock (mprog)
            {
                // somewhere to save our %
                Dictionary<byte, MAVLink.MAVLinkMessage> status = new Dictionary<byte, MAVLink.MAVLinkMessage>();
                foreach (var item in mprog)
                {
                    status[((MAVLink.mavlink_mag_cal_progress_t)item.data).compass_id] = item;
                }

                // message for user
                string message = "";
                foreach (var item in status)
                {
                    var obj = (MAVLink.mavlink_mag_cal_progress_t)item.Value.data;


                    try
                    {
                        if (item.Key == 0)
                            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = obj.completion_pct));// progressBar1.Value = obj.completion_pct;
                        if (item.Key == 1)
                            progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = obj.completion_pct));// progressBar2.Value = obj.completion_pct;
                        if (item.Key == 2)
                            progressBar3.Invoke((MethodInvoker)(() => progressBar3.Value = obj.completion_pct));//progressBar3.Value = obj.completion_pct;
                    }
                    catch { }

                    message += "id:" + item.Key + " " + obj.completion_pct.ToString() + "% ";
                    compasscount++;
                }
                //lbl_obmagresult.AppendText(message + "\n");
                doLog("cali:"+message + "\n"   );

            }

            lock (mrep)
            {
                // somewhere to save our answer
                Dictionary<byte, MAVLink.MAVLinkMessage> status = new Dictionary<byte, MAVLink.MAVLinkMessage>();
                foreach (var item in mrep)
                {
                    var obj = (MAVLink.mavlink_mag_cal_report_t)item.data;

                    if (obj.compass_id == 0 && obj.ofs_x == 0)
                        continue;

                    status[obj.compass_id] = item;
                }

                // message for user
                foreach (var item in status.Values)
                {
                    var obj = (MAVLink.mavlink_mag_cal_report_t)item.data;

                    doLog("id:" + obj.compass_id + " x:" + obj.ofs_x.ToString("0.0") + " y:" +
                                               obj.ofs_y.ToString("0.0") + " z:" +
                                               obj.ofs_z.ToString("0.0") + " fit:" + obj.fitness.ToString("0.0") + " " +
                                               (MAVLink.MAG_CAL_STATUS)obj.cal_status + "\n"  , true )  ;

                    try
                    {
                        if (obj.compass_id == 0)
                            progressBar1.Value = 100;
                        if (obj.compass_id == 1)
                            progressBar2.Value = 100;
                        if (obj.compass_id == 2)
                            progressBar3.Value = 100;
                    }
                    catch
                    {
                    }

                    if ((MAVLink.MAG_CAL_STATUS)obj.cal_status != MAVLink.MAG_CAL_STATUS.MAG_CAL_SUCCESS)
                    {
                        //CustomMessageBox.Show(Strings.CommandFailed);
                    }

                    if (obj.autosaved == 1)
                    {
                        completecount++;
                        //timer1.Interval = 1000;
                    }
                }
            }

            if (compasscount == completecount && compasscount != 0)
            {
                btnMagcali_Accept.Enabled = false;
                btnMagcali_cancel.Enabled = false;
                timer1.Stop();
                CustomMessageBox.Show("Please reboot the autopilot");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnTimerStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnToggleLog_Click(object sender, EventArgs e)
        {
            mandatory = !mandatory;
        }



        /// <summary>
        /// Acc Calibration 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        bool _incalibrate = false;
        private byte count = 0;
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsub3;
        private void btnAccCalibrate_Click(object sender, EventArgs e)
        {
            if (_incalibrate)
            {
                count++;
                try
                {
                    MainV2.comPort.sendPacket(new MAVLink.mavlink_command_ack_t { command = 1, result = count },
                        MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent);

                    doLog("First packet  "+ count.ToString() , true );
                }
                catch
                {
                    CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
                    return;
                }

                return;
            }

            try
            {
                count = 0;

                //Log.Info("Sending accel command (mavlink 1.0)");

                MainV2.comPort.doCommand(MAVLink.MAV_CMD.PREFLIGHT_CALIBRATION, 0, 0, 0, 0, 1, 0, 0);

                _incalibrate = true;

                packetsub3 = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.STATUSTEXT, receivedPacketAccCalib);

                //BUT_calib_accell.Text = Strings.Click_when_Done;
                btnAccCalibrate.Text = Strings.Click_when_Done;

            }
            catch (Exception )
            {
                _incalibrate = false;
               // Log.Error("Exception on level", ex);
                CustomMessageBox.Show("Failed to level", Strings.ERROR);
            }
        }

        /// <summary>
        ///  Acc calibrate Packet received 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool receivedPacketAccCalib(MAVLink.MAVLinkMessage arg)
        {
            if (arg.msgid == (uint)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
            {
                var message = ASCIIEncoding.ASCII.GetString(arg.ToStructure<MAVLink.mavlink_statustext_t>().text);

                //UpdateUserMessage(message);
                /*
                Invoke((MethodInvoker)delegate
                {
                    if (message.ToLower().Contains("place vehicle") || message.ToLower().Contains("calibration"))
                        lblAccCalib.Text = message;
                });
                */
                if (message.ToLower().Contains("place vehicle") || message.ToLower().Contains("calibration"))
                {
                    CustomMessageBox.Show( message.ToLower(), "OK", MessageBoxButtons.OK);
                    lblAccCalib.Text = message;

                    doLog(message, true);
                }
                


                if (message.ToLower().Contains("calibration successful") ||
                 message.ToLower().Contains("calibration failed"))
                {
                    try
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            btnAccCalibrate.Text = Strings.Done;
                            btnAccCalibrate.Enabled = false;
                        });

                        _incalibrate = false;
                        MainV2.comPort.UnSubscribeToPacketType(packetsub3);
                        //MainV2.comPort.UnSubscribeToPacketType( MAVLink.MAVLINK_MSG_ID.STATUSTEXT, ReceivedPacketAccCalib );
                    }
                    catch
                    {
                    }
                }
            }

            return true;
        }

        private void btnAccCalibInit_Click(object sender, EventArgs e)
        {
            _incalibrate = false;
            btnAccCalibrate.Enabled = true;
        }

        /// <summary>
        ///  MagCalib를 취소한다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMagcali_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_CANCEL_MAG_CAL, 0, 0, 1, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.ToString(), Strings.ERROR, MessageBoxButtons.OK);
            }

            MainV2.comPort.UnSubscribeToPacketType(packetsub1);
            MainV2.comPort.UnSubscribeToPacketType(packetsub2);

            timer1.Stop();
        }
        /// <summary>
        /// MAg Calib 를 Accept 한다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMagcali_Accept_Click(object sender, EventArgs e)
        {
            try
            {
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_ACCEPT_MAG_CAL, 0, 0, 1, 0, 0, 0, 0);

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.ToString(), Strings.ERROR, MessageBoxButtons.OK);
            }

            MainV2.comPort.UnSubscribeToPacketType(packetsub1);
            MainV2.comPort.UnSubscribeToPacketType(packetsub2);

            timer1.Stop();
        }
        /// <summary>
        /// 모터 테스트를 한다.. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        int time = 2;   // motor test time 
        int speed = 6;  // motor speed -> percentage
        int motorcount = 4; // 4 copter


        
        private void motorTest( int motor , int speed , int time , int motorcount =4) { 

            

            try
            {

                
                if (
                !MainV2.comPort.doMotorTest( motor , MAVLink.MOTOR_TEST_THROTTLE_TYPE.MOTOR_TEST_THROTTLE_PERCENT,
                    speed, time, motorcount))
                {
                    CustomMessageBox.Show("Command was denied by the autopilot");
                }
                
                
            }
            catch
            {
                CustomMessageBox.Show(Strings.ErrorCommunicating + "\nMotor: " + motor, Strings.ERROR);
            }
            
        }
        private void btnMotorTest_Click(object sender, EventArgs e)
        {
            int motor = 1;
            speed =  Int32.Parse ( txtMotorSpeed.Text )    ;
            motorTest(motor, speed, time, motorcount);

        }
        private void btnMotorTest2_Click(object sender, EventArgs e)
        {
            int motor = 2;
            speed = Int32.Parse(txtMotorSpeed.Text);
            motorTest(motor, speed, time, motorcount);
        }

        private void btnMotorTest3_Click(object sender, EventArgs e)
        {
            int motor = 3;
            speed = Int32.Parse(txtMotorSpeed.Text);
            motorTest(motor, speed, time, motorcount);
        }

        private void btnMotorTest4_Click(object sender, EventArgs e)
        {
            int motor = 4;
            speed = Int32.Parse(txtMotorSpeed.Text);
            motorTest(motor, speed, time, motorcount);
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
