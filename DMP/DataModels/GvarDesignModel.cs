using DMP.DataModels.Commands;
using DMP.Resources;
using DMP.util;
using log4net;
using Microsoft.Win32;
using MissionPlanner;
using MissionPlanner.Mavlink;
using MissionPlanner.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DMP.DataModels
{
    public class GvarDesignModel : GvarModel
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Singleton 
        /// <summary>
        /// A single instance (singleton) of the 데이터 입력하게.. d.. 
        /// </summary>
        private static GvarDesignModel _instance;
        public static GvarDesignModel Instance => _instance ?? (_instance = new GvarDesignModel());
        #endregion Singleton
        /// <summary>
        ///  생성자.... 각종 변수 및 타겟 포인트 , 웨이포인트를 파일로 부터 읽어 와서 초기화 한다. 
        /// </summary>
        public GvarDesignModel() 
        {
            #region test data 
            /*
            TargetPointList = new List<TargetPointModel>
            {
                new TargetPointModel
                {
                    Index = 1,
                    Latitude = 101,
                    Altitude = 101,
                    Longitude = 101,
                    Name = "101",
                },
                new TargetPointModel
                {
                    Index = 2,
                    Latitude =102,
                    Altitude = 102,
                    Longitude = 102,
                    Name = "102",
                },
                new TargetPointModel
                {
                    Index = 3,
                    Latitude =103,
                    Altitude = 103,
                    Longitude = 103,
                    Name = "103",
                },

            };

            WaypointList = new List<WayPointModel>
            {
                new WayPointModel
                {
                    Speed = 101,
                    Name = "WP 101",
                    Bearing = 180,
                    Height = 101,
                    Latitude = 37.3844168105,
                    Longitude = 126.65596369,
                },
                new WayPointModel
                {
                    Speed = 101,
                    Name = "WP 101",
                    Bearing = 180,
                    Height = 101,
                    Latitude = 37.3844169105,
                    Longitude = 126.65596369,
                },
            };
            */
            #endregion

            // TargetPointList = new List<TargetPointModel>();
            // WaypointList = new List<WayPointModel>();  

            LoadXmlFileCommand = new RelayCommand(LoadXmlFile, LoadXmlFileCanUse);
            SaveXmlFileCommand = new RelayCommand(SaveXmlFile, SaveXmlFileCanUse);

            ClearPointCommand = new RelayCommand(ClearPoint, ClearPointCanUse);

            ConnectCommand = new RelayCommand(Connect , ConnectCanUse);
            DisConnectCommand = new RelayCommand(DisConnect, DisConnectCanUse);


            StartMonitoringCommand = new RelayCommand(StartMonitoring, StartMonitoringCanUse);
            EndMonitoringCommand = new RelayCommand(EndMonitoring, EndMonitoringCanUse);

            ReadWPCommand = new RelayCommand(ReadWP, ReadWPCanUse);
            WriteWPCommand = new RelayCommand(WriteWP, WriteWPCanUse);

        }

        #region Button Command
        public RelayCommand LoadXmlFileCommand { get; private set; }
        public RelayCommand SaveXmlFileCommand { get; private set; }
        public RelayCommand ConnectCommand  { get; private set; }
        public RelayCommand ReadWPCommand { get; private set; }
        public RelayCommand WriteWPCommand { get; private set; }
        public RelayCommand DisConnectCommand { get; private set; }
        public RelayCommand StartMonitoringCommand { get; private set; }
        public RelayCommand EndMonitoringCommand { get; private set; }
        public RelayCommand ClearPointCommand { get; private set; }

        #endregion

        public void LoadXmlFile( object sender)
        {
            // 실제 구현은 DMP.util.ListToXML 에 있다.. 

            var a = new ToastViewModel();
            a.ShowInformation("dddd");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FlightFile files (*.xml)|*.xml|All files(*.*)|*.*";
            ListToXML ltx = new ListToXML();
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    XDocument doc = XDocument.Load(openFileDialog.FileName);
                    //tbFilename.Text = openFileDialog.FileName;

                    GvarDesignModel.Instance.FlightFilename = openFileDialog.FileName;
                    ltx.WPListFromXML(doc); // waypoint도 읽어오고
                    ltx.OptionsFromXML(doc);// option도 읽어오고 
                }
            }
            catch (Exception) { }
        }
        public void SaveXmlFile( object sender)
        {
            // 실제 구현은 DMP.util.ListToXML 에 있다.. 
            ListToXML xmlcon = new ListToXML();

            XDocument xml = new XDocument();
            XElement xmlele;
            //xml.Add( xmlcon.TPListToXML( GvarDesignModel.Instance.TargetPointList));
            //xml.Add( xmlcon.WPListToXML(GvarDesignModel.Instance.WaypointList));

            xmlele = xmlcon.ListAllToXML();
            //xmlele = xmlcon.TPListToXML(GvarDesignModel.Instance.TargetPointList);
            //xmlele.Add( xmlcon.WPListToXML(GvarDesignModel.Instance.WaypointList).Nodes());

            xml.Add(xmlele);
            //var list = xmlcon.TPListFromXML(xml);

            SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "dmp_"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "xml documents (.xml)|*.xml"; // Filter files by extension

            dlg.Filter = "XML-File | *.xml";
            if (dlg.ShowDialog() == true)
            {
                xml.Save(dlg.FileName);
            }
        }

        public void ClearPoint(object sender)
        {

            GvarDesignModel.Instance.WPList.Clear();
            ((Canvas)sender).Visibility = Visibility.Collapsed;
            GvarDesignModel.Instance.TPList.Clear();
            GvarDesignModel.Instance.HomePosition = null;
            MapDesignModel.Instance.DmlPushpin.Children.Clear();
            MapDesignModel.Instance.DmlPolyline.Children.Clear();
            //var ContentPopup = (Map)_map.FindName("MapWithEvents");
            //ContentPopup.Visibility = Visibility.Collapsed;
        }
        public bool ClearPointCanUse( object sender)
        {
            return true;
        }

        public void EndMonitoring( object sender)
        {

        }


        /// <summary>
        /// 일단 무조건 활성화 됨.... 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool LoadXmlFileCanUse( object sender)
        {
            return true;
        }

        /// <summary>
        /// 일단 무조건 활성화 됨.    //나중에 WP 갯수가 없으면 활성화 안되게 해야것다.... 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool SaveXmlFileCanUse( object sender  )
        {
            return true;
        }

        #region Connect and Monotoring  - ReadPacket 
        /// <summary>
        /// 지금은 그냥 comPort로만 연결 한다... 
        /// </summary>
        /// <param name="sender"></param>
        public void Connect(object sender)
        {
            Console.WriteLine("  Connect ... ");
            log.Debug("Start connect ");
            MainV2.instance.doConnect(MainV2.comPort, "serial", "115200"); //, portname, baud);
            MainV2.comPort.getParamList();
            int a =MainV2.comPort.MAV.param.Count ;
            log.DebugFormat(" 파람 갯수는" , a);
            //lblCompID.Content = MainV2.comPort.compidcurrent.ToString();
            //lblSysID.Content = MainV2.comPort.sysidcurrent.ToString();
        }
        /// <summary>
        /// 지금 열려 있으면 오픈않된다... 그런데 나중에 드론을 여러대를 붙일려면 수정해야한다. 
        /// </summary>
        /// <returns></returns>
        public bool ConnectCanUse(object sender)
        {
            if (MainV2.comPort.BaseStream.IsOpen) return false;
            return true;
        }
        public void DisConnect(object sender)
        {
            log.Debug("dis connect ");
            MainV2.comPort.Close();
        }
        public bool DisConnectCanUse(object sender)
        {
            if (MainV2.comPort.BaseStream.IsOpen) return true ;
            return false ;
        }

        public void StartMonitoring(object sender)
        {
            MainV2.instance.readPacketThreadRun();
            packetsub1 = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.STATUSTEXT,  TestReceivedDataFromDrone);
        }
        public void EndMonotoring(object sender)
        {
            MainV2.instance.readPacketThreadStop();
            MainV2.comPort.UnSubscribeToPacketType(packetsub1);  // 필요 없을때는 뺀다. 
        }
        public bool StartMonitoringCanUse(object sender)
        {
            if (MainV2.comPort.BaseStream.IsOpen)
                return true;
            else
                return false;
        }
        public bool EndMonitoringCanUse(object sender)
        {
            if (MainV2.comPort.BaseStream.IsOpen)
                return true;
            else
                return false;
        }
        #endregion




        #region Test입니다. 나중에 지워 주세요.
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsub1;
        private bool TestReceivedDataFromDrone(MAVLink.MAVLinkMessage packet)
        {

            ///이거 패킷 아이디별로 체크해 주세요. 

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

                /*
                lbl_obmagresult.Dispatcher.Invoke(new Action(delegate {
                    lbl_obmagresult.Content += "_" + obj.completion_pct.ToString();


                }));

                if (obj.compass_id == 0)
                    progressBar1.Dispatcher.Invoke(new Action(delegate { progressBar1.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;
                if (obj.compass_id == 1)
                    progressBar2.Dispatcher.Invoke(new Action(delegate { progressBar2.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;
                if (obj.compass_id == 2)
                    progressBar3.Dispatcher.Invoke(new Action(delegate { progressBar3.Value = obj.completion_pct; }));//progressBar1.Value = obj.completion_pct;
                    */
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
        #endregion

        #region Sorting and Change Order Function 
        /// <summary>
        /// Way Point , TargetPoint를 지우는 일을 한다. 지우면 true , 못지우면 false ;
        /// 지운다면 각자의 거리 계산 각도 계산을 다시 해야한다. 
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeleteWayPoint(int pt , int index  )
        {
            Dialogs.CustomMessageBox.Show("이창이 보이면 않됨.....GvarDesignmodel -> WayPointModel로 옮겼음. ");

            if( pt ==(int)DMP.PointType.WAYPOINT)
            {
                DMP.DataModels.GvarDesignModel.Instance.WPList.RemoveAt( index );
                return true;
            }
            else if ( pt == (int) DMP.PointType.TARGET) 
            {
                // 만약 Target 일경우 Target을 포함하는 Waypoint의 Target 정보를 삭제해야함
                var list = DMP.DataModels.GvarDesignModel.Instance.WPList;
                DMP.DataModels.GvarDesignModel.Instance.TPList.RemoveAt(index);
                for ( int i=0; i< list.Count; i++ )
                {
                    if(index == list[i].Target)
                    {
                        list[i].Target = 0;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 새로먼가가 추가 되거나, 삭제 되거나, 순서를 바꿀때 한번씩 소팅을 해준다. 
        /// </summary>
        public void SortAllKindWayPoint()
        {
            //Sorting 을 순서가 바뀔때 한번씩 해준다. 

            //var g = DMP.DataModels.GvarDesignModel.Instance;
            WPList
                = new ObservableCollection<WayPointModel>( WPList.OrderBy(i=>i.Index)); //(ObservableCollection<WayPointModel>)WPList.OrderBy(f => f);

            TPList = new ObservableCollection<WayPointModel>(TPList.OrderBy(i => i.Index)); // (ObservableCollection<WayPointModel>)TPList.OrderBy(f => f.Index);
            
            foreach (var item in WPList)  { item.Index = 1+WPList.IndexOf(item); }
            foreach (var item in TPList) { item.Index = 1+TPList.IndexOf(item); }
        }
        /// <summary>
        /// 특정 Index의 up true| false에 따라서 순서를 올리거나 내린다. 
        /// </summary>
        /// <param name="up"></param>
        /// <param name="index"></param>
        public void ChangeWayPointOrder(bool up , int selectedIndex)
        {
            // 업을 할때 1번째는 업이 안된다. 
            if (up && selectedIndex == 1) return;

            // 다운일때 맨 마지막은 다운이 않된다. 
            if (!up && selectedIndex == DMP.DataModels.GvarDesignModel.Instance.WPList.Count) return;
            /// 순서를 바꾸어 보자... 
            /// 
            var list = DMP.DataModels.GvarDesignModel.Instance.WPList;
            ChangePointOrder(list , up , selectedIndex);
        }
        /// <summary>
        /// TargetPoint의 정렬을 바꾼다...
        /// </summary>
        /// <param name="up"></param>
        /// <param name="selectedIndex"></param>
        public void ChangeTargetPointOrder(bool up, int selectedIndex)
        {
            // 업을 할때 1번째는 업이 안된다. 
            if (up && selectedIndex == 1) return;

            // 다운일때 맨 마지막은 다운이 않된다. 
            if (!up && selectedIndex == DMP.DataModels.GvarDesignModel.Instance.TPList.Count) return;
            /// 순서를 바꾸어 보자... 
            /// 
            var list = DMP.DataModels.GvarDesignModel.Instance.TPList;
            ChangePointOrder(list, up, selectedIndex);
        }

        /// <summary>
        /// WapPoint, TargetPoint인지 던져 주면 수정을 한다.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="up"></param>
        /// <param name="selectedIndex"></param>
        private void ChangePointOrder( ObservableCollection<WayPointModel> list , bool up , int selectedIndex ) { 
            int i = 0;
            //selectedIndex는 1로부터 시작하므로 하나 줄여준다. 
            selectedIndex--;
            
            foreach (var item in list )
            {
                i++;
                if (up)
                {
                    if (list.IndexOf(item) == selectedIndex - 1)
                    {
                        item.Index = i + 1;
                    }
                    else if (list.IndexOf(item) == selectedIndex)
                    {
                        item.Index = i - 1;
                    }
                }
                else   // Down 
                {
                    if (list.IndexOf(item) == selectedIndex)
                    {
                        item.Index = i + 1;
                    }
                    else if (list.IndexOf(item) == selectedIndex + 1)
                    {
                        item.Index = i - 1;
                    }
                }
            }
            /// 새로 바꾼 index로 소팅을 다시 한다. 
            SortAllKindWayPoint();
        }
        #endregion

        #region Data Connection with Drone 
        public bool WriteWPCanUse(object sender)
        {
            return true;
        }
        public void WriteWP(object sender)
        {
            
        }
        /// <summary>
        /// 가져 오기만 하고 아직 가져온것을 WayPointList에다가 담지는 않고 있다. ... 
        /// </summary>
        /// <param name="sender"></param>
        public bool ReadWPCanUse(object sender) { return true; }
        public void ReadWP(object sender)
        {
            MAVLinkInterface port = MainV2.comPort;
            Locationwp wp;

            try
            {
                int total = port.getWPCount();   // 신기하게도 Home position을 포함하는지 꼭 한개가 추가가 된다. 
                for (ushort i = 0; i < total; i++)
                {
                    wp = port.getWP(i);
                    Console.WriteLine( "######"+wp.lat +":" +wp.lng );
                }
                port.setWPACK();
            }
            catch (Exception ed) { }
        }
        private List<Locationwp> GetCommandList()
        {
            List<Locationwp> commands = new List<Locationwp>();
            foreach (var data in GvarDesignModel.Instance.WPList)
            {
                var temp = data.GetLocationwp();
                commands.Add(temp);
            }
            return commands;
        }
        /// <summary>
        /// WP 리스트에서 locationwp 구조체 형태를 가져오는 함수 이다. 
        /// WayPointConvertUtility로 옮겨 갔다. 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// 
        /*
        private Locationwp WPModeltoLocationwp(int a)
        {
        }
        */
#endregion


}
}

