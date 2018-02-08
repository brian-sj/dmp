using DMP.DataModels;
using MissionPlanner;
using MissionPlanner.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAVLink;

namespace DMP.util
{
    class WayPointConvertUtility
    {

        public static WayPointModel LocationwpToWayPoint( Locationwp data  )
        {

            /// null 체크를 적절히 하지 못했다. 나중에 추가해 주라. 2018.1.31
            if ( false )
            {
                //DMP.Dialogs.CustomMessageBox.Show("ERROR", "기체 정보를 WayPoint형식으로 변환하는데 실패 했습니다. ");
            }
            WayPointModel wp = new WayPointModel();
            wp.Height = data.alt;
            wp.Latitude = data.lat;
            wp.Longitude = data.lng;
            wp.p1 = data.p1;
            wp.p2 = data.p2;
            wp.p3 = data.p3;
            wp.p4 = data.p4;
            return wp;
        }
        public static Locationwp WPModeltoLocationwp( WayPointModel data )
        {
            Locationwp temp = new Locationwp();
            temp.lat = data.Latitude;
            temp.lng = data.Longitude;
            temp.alt = data.Height;
            temp.id = data.command_id;
            temp.p1 = data.p1;
            temp.p2 = data.p2;
            temp.p3 = data.p3;
            temp.p4 = data.p4;  /// 총 7개 파라메터가 있는데 나머지 세개가 lat , lng , alt 이다. 
            temp.Tag = data.Name;

            return temp;
        }
        public static  Locationwp WPModeltoLocationwp(int a)
        {
            WayPointModel data = null;//= GvarDesignModel.Instance.WaypointList.Where(wp => ((WayPointModel)wp).Index == a );
            try
            {
                foreach (var item in GvarDesignModel.Instance.WPList)
                {
                    if (item.Index == a)
                    {
                        data = item;
                        break;
                    }
                }
                if (data == null)
                {
                    //throw new FormatException("invalid number of wp's list " + (a + 1).ToString(), null);
                    Dialogs.CustomMessageBox.Show("ERROR", "해당 Waypoint가 존재 하지 않습니다. :"+(a+1).ToString() );
                }
            }
            catch (Exception e)
            {
                Dialogs.CustomMessageBox.Show("ERROR", "해당 Waypoint가 존재 하지 않습니다. :" + (a + 1).ToString());
                return null;
            }
            return WPModeltoLocationwp(data);
        }

        /// <summary>
        /// WayPointList 를 MainV2.comPort.MAV.wps에 넣는다... 2018년 2월 7일 꼭 수정해야 한다. GetCommandList해서 넣어라...
        /// </summary>
        public static void WayPointListToMavwps()
        {
            ConcurrentDictionary<int, mavlink_mission_item_t> wps = new ConcurrentDictionary<int, mavlink_mission_item_t>();
            int i = 0;

            List<Locationwp> list = GetCommandList();
            foreach (var item in list)
            {
                //Locationwp data = WayPointConvertUtility.WPModeltoLocationwp( item );
                wps.TryAdd( i, (MAVLink.mavlink_mission_item_t)item);
                i++;
            }
            MainV2.comPort.MAV.wps = wps;
        }

        /// <summary>
        /// 이 부분은 WayPoint뿐아니라 Target, Home 등을 모두 합해야 한다. 
        /// </summary>
        /// <returns></returns>
        public static  List<Locationwp> GetCommandList()
        {
            List<Locationwp> commands = new List<Locationwp>();
            var inst = GvarDesignModel.Instance;
            int temp_target_index = 0;
            // ## 1. HomePosition을 생략한다. 이것은 필요하지가 않다.  
            //Locationwp home = inst.HomePosition.GetLocationwp();
            //commands.Add(home);

            // ## 2. WayPoint를 입력한다.  만약 TargetPoint를 가지고 있다면 먼저 입력해 줘야 한다. 
            foreach (var data in inst.WPList)
            {
                // 만약 Target이 0 이 아님서 앞서서 입력한 Target이 아닌경우는 command 목록에 입력해 줘야한다.
                if( data.Target != 0 && data.Target != temp_target_index )
                {
                    var targetPoint = inst.TPList.Single( i => i.Index == data.Target  );
                    commands.Add( targetPoint.GetLocationwp());
                    temp_target_index = data.Target; // 최근 Target포인트를 기록한다. 
                }
                var temp = data.GetLocationwp();
                commands.Add(temp);
            }

            //  ## 아직액션은 없다. 생기면 넣어 줘야지...


            return commands;
        }
    }
}
