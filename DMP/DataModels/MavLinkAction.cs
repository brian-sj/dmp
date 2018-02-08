using DMP.Resources;
using DMP.util;
using log4net;
using MissionPlanner;
using MissionPlanner.Mavlink;
using MissionPlanner.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class MavLinkAction
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void doReadWP()
        {

        }
        public static void doWriteWP()
        {

        }
        public static Locationwp ReadHomePosition()
        {
            Locationwp wp = MainV2.comPort.getHomePosition();
            return wp;
        }
        public static void WriteHomePosition()
        {
            Locationwp wp = new Locationwp();
            MAVLinkInterface port = MainV2.comPort;
            port.giveComport = true;

            wp = GvarDesignModel.Instance.HomePosition.GetLocationwp();
            wp.id = (ushort)MAVLink.MAV_CMD.DO_SET_HOME;
            
            //wp.lat = 37.444456789;// 37.379210;  //37.3865258,126.6460668,15.75z
            //wp.lng = 126.44445678;// 126.6723221;
            //wp.alt = 4f;

            int hometype = (int)PointType.HOME; // 총 8개중  param1 1 현재 위치를 홈으로 | 2 아래 주소지값을 홈으로

            bool ret = false;
            bool use_int = false;
            int a = 0;

            ret = MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_SET_HOME, hometype, 0, 0, 0, (float)wp.lat, (float)wp.lng, wp.alt);
            if (ret)
            {
                Dialogs.CustomMessageBox.Show(String.Format("Success -> {0} : {1}: {2}m", wp.lat, wp.lng, wp.alt));
            }
            else
            {
                Dialogs.CustomMessageBox.Show("Fail");
            }

            port.giveComport = false;
            port.setWPACK();
        }

        public static void WriteWPToDrone()
        {
            ushort totalCnt = 0;// GetCommandList().Count;
            MAVLinkInterface port = MainV2.comPort;

            try
            {
                MainV2.comPort.giveComport = true;
            }
            catch (Exception) { Console.WriteLine("getWPCount Timeout"); }

            //ushort a = (ushort) (cnt );

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
                var home = GvarDesignModel.Instance.HomePosition.GetLocationwp();
                /* Brian :: HomePosition을 먼저 넣어야 한다. */
                var homeans = port.setWP(home, (ushort)homeindex, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);

                for (int a = 1; a <= commandlist.Count; a++)
                {
                    if (a == 0)
                    {
                        Console.WriteLine("RequestNo를 가져 오지 못했네...");
                        a++;
                    }
                    var wp = commandlist[a - 1];
                    uploadwpno = (ushort)a;
                    var ans = port.setWP(wp, (ushort)uploadwpno, frame, 0, 1, use_int);
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                    {
                        // resend for partial upload
                        port.setWPPartialUpdate((ushort)(uploadwpno), totalCnt);
                        // reupload this point.
                        ans = port.setWP(wp, (ushort)(uploadwpno), frame, 0, 1, use_int);
                        log.InfoFormat("ERROR RETRY :{0} " , ans);
                    }
                    else if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                    {
                        a = port.getRequestedWPNo() - 1;
                        log.WarnFormat("@@@@@@@  invalid sequence retry: {0} requestedWpno:{1}", ans , a+1 );
                        continue;
                    }
                    else if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                    {
                        Dialogs.CustomMessageBox.Show("Error", "WP를 전송할 수 없습니다.");
                        log.InfoFormat("WP 전송실패 {0}번째 index:{1}", a, wp.lat);
                        break;
                    }
                }
                port.setWPACK();
                MainV2.comPort.giveComport = false;
            }
            catch (Exception ee) { Dialogs.CustomMessageBox.Show("Error", ee.ToString()); }
        }
    }
}
