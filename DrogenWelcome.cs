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
            ushort a = cnt ;

            //MAVLink.MAV_MISSION_RESULT result =  MainV2.comPort.setWP(   wp  , cnt , frame  );
            var homeans = MainV2.comPort.setWP( wp , (ushort)a, frame, 0, 1, use_int);

            //MainV2.comPort.doCommand(MAVLink.MAV_CMD_DO)
        }

        private void btnReadPacket_Click(object sender, EventArgs e)
        {
            MAVLink.MAVLinkMessage msg = MainV2.comPort.readPacket();
            String tomsg = "";
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
    }
}
