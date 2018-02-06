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
            ConnectCommand = new RelayCommand(Connect , ConnectCanUse);
            DisConnectCommand = new RelayCommand(DisConnect, DisConnectCanUse);


            StartMonitoringCommand = new RelayCommand(Connect, ConnectCanUse);
            EndMonitoringCommand = new RelayCommand(Connect, ConnectCanUse);

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
        // Way Point , TargetPoint를 지우는 일을 한다. 지우면 true , 못지우면 false ;
        public bool DeleteWayPoint(int pt , int index  )
        {
            if( pt ==(int)PointType.WAYPOINT)
            {
                DMP.DataModels.GvarDesignModel.Instance.WPList.RemoveAt( index );
                return true;
            }
            else if ( pt == (int) PointType.TARGET)
            {
                DMP.DataModels.GvarDesignModel.Instance.TPList.RemoveAt(index);
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
        /// <summary>
        /// 각 웨이포인트별로 거리를 구한다...  첫번째 것은 HomePoint별로 구해야 하는데 이것은 비행기를 띠워야 아는 것이다.
        /// </summary>
        public double CalculationEachDistance()
        {
            WayPointModel pwp = null;
            double totalDistance = 0.0;
            
            foreach (var wp in GvarDesignModel.Instance.WPList)
            {
                if (pwp == null)
                {  // 첫번째라면...
                    pwp = new WayPointModel();
                    pwp.Latitude = GvarDesignModel.Instance.HomePosition.Latitude;
                    pwp.Longitude = GvarDesignModel.Instance.HomePosition.Longitude;
                }
                wp.DistanceFromPrev = GeoCalculate.Distance(pwp.Latitude, pwp.Longitude, wp.Latitude, wp.Longitude);

                pwp.Bearing = (float)Math.Round(GeoCalculate.CalculateBearing( wp.Location , pwp.Location) ,2) ;
                pwp = wp;
                totalDistance += wp.DistanceFromPrev;
            }
            GvarDesignModel.Instance.TotalFlightDistance = totalDistance;
            return totalDistance;
        }

        #endregion

        #region Data Connection with Drone 
        public bool WriteWPCanUse(object sender)
        {
            return true;
        }
        public void WriteWP(object sender)
        {
            try
            {
                MAVLinkInterface port = MainV2.comPort;
                var wplist = GvarDesignModel.Instance.WPList;

                /// 여기서 wplist를   MainV2.comPort.MAV.wps.Values
                if (!port.BaseStream.IsOpen)
                {
                    throw new Exception("Pls connect first");
                }
                MainV2.comPort.giveComport = true;
                int a = 0;
                Locationwp home = new Locationwp();

                MavLinkAction.WriteHomePosition(); 

                /// 홈포지션은 waypoints에서 가지고 와서 타입이 home인 애를 가져온다. 
                try
                {
                    
                    var homeposition = MainV2.comPort.getHomePosition(); //GvarDesignModel.Instance.HomePosition;

                    home.lat = (double)homeposition.lat;// home 정보 넣어 주라..  ;
                    home.alt = (float)homeposition.alt;
                    home.lng = (double)homeposition.lng;


                    

                }
                catch {
                    Dialogs.CustomMessageBox.Show("ERROR", "HOMEPOSITION이 지정되지 않았습니다. 대신 임의로 47.4444 로 정합니다. ");

                    //****************테스트로 홈포지션을
                    home.id = (ushort)MAVLink.MAV_CMD.DO_SET_HOME;
                    home.lat = 47.4444444;
                    home.lng = 126.444444;
                    home.alt = 5f;
                }
                // log
                log.Info("wps values " + MainV2.comPort.MAV.wps.Values.Count);
                log.Info("cmd rows " + (wplist.Count + 1)); // + home

                // check for changes / future mod to send just changed wp's
                // 지금은 비교를 하고도 일단 다 집어 넣는 구조다.... 
                // 나중에 수정을 해 보자 테스트도 많이 필요하다...


                //#### 일단 받은 waypoint를 잘 정리해서 넣어 보자. 
                //#### 주의 해야한다... waypoint뿐아니라 
                //#### homeposition , targetpoint, action 까지 순차적으로 넣어야 한다. 
                WayPointConvertUtility.WayPointListToMavwps(); 
                if (true)  // 숫자를 맞출 필요가 없다... 전에 왜 이런 비교가 있었는지 궁금하다... 2018.1.31
                //if( MainV2.comPort.MAV.wps.Values.Count == (wplist.Count + 1))
                {
                    Hashtable wpstoupload = new Hashtable();
                    a = -1;
                    foreach (var item in MainV2.comPort.MAV.wps.Values)
                    {
                        // skip home
                        if (a == -1)
                        {
                            a++;
                            continue;
                        }

                        MAVLink.mavlink_mission_item_t temp = item;// WayPointConvertUtility.WPModeltoLocationwp(a);
                        //MAVLink.mavlink_mission_item_t temp = new MAVLink.mavlink_mission_item_t();

                        if (temp.command == item.command &&
                            temp.x == item.x &&
                            temp.y == item.y &&
                            temp.z == item.z &&
                            temp.param1 == item.param1 &&
                            temp.param2 == item.param2 &&
                            temp.param3 == item.param3 &&
                            temp.param4 == item.param4
                            )
                        {
                            log.Info("wp match " + (a + 1));
                        }
                        else
                        {
                            log.Info("wp no match" + (a + 1));
                            wpstoupload[a] = "1";
                        }

                        a++;
                    }
                }
                /// 일단 위에까지는 안쓸거지만... 다른것이 있는지 확은은 했다... 

                //int type으로 할지... 아니라면 float 타입니다... 
                bool use_int = (port.MAV.cs.capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

                // set wp total
                //((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

                ushort totalwpcountforupload = (ushort)(wplist.Count + 1);


                /// 일단 우리꺼는 ArduinoMEGA라고 나온다. 
                if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                {
                    totalwpcountforupload--;
                }

                port.setWPTotal(totalwpcountforupload); // + home

                // set home location - overwritten/ignored depending on firmware.
                //((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

                // upload from wp0
                a = 0;


                //####  이 부분은 왜 있는지 모르겠다.... 
                //일단 넘어가자 홈은 위에서 정해 주었으니 
                /*
                if (port.MAV.apname != MAVLink.MAV_AUTOPILOT.PX4)
                {
                    try
                    {
                        var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                Dialogs.CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
                                return;
                            }
                        }
                        a++;
                    }
                    catch (TimeoutException)
                    {
                        use_int = false;
                        // added here to prevent timeout errors
                        port.setWPTotal(totalwpcountforupload);
                        var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                Dialogs.CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
                                return;
                            }
                        }
                        a++;
                    }
                }
                else
                {
                    use_int = false;
                }
                */

                // define the default frame.
                MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

                // get the command list from the datagrid
                var commandlist = GetCommandList();

                // process commandlist to the mav
                for (a = 1; a <= commandlist.Count; a++)
                {
                    var temp = commandlist[a - 1];

                    //((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / wplist.Count, "Setting WP " + a);

                    // make sure we are using the correct frame for these commands
                    if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
                    {

                        /// 일단 relavive 로 하고 나중에 3가지 다 지원하자. 
                        /// 
                        /*
                        var mode = currentaltmode;

                        if (mode == altmode.Terrain)
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT;
                        }
                        else if (mode == altmode.Absolute)
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL;
                        }
                        else
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
                        }
                        */
                        ///#######################   일단.. relative 입니다. 
                        frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
                    }
                    // handle current wp upload number
                    int uploadwpno = a;
                    if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                        uploadwpno--;

                    // try send the wp
                    MAVLink.MAV_MISSION_RESULT ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

                    // we timed out while uploading wps/ command wasnt replaced/ command wasnt added
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                    {
                        // resend for partial upload
                        port.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
                        // reupload this point.
                        ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
                    }

                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
                    {
                        //e.ErrorMessage = ;
                        Dialogs.CustomMessageBox.Show("ERROR", "Upload failed, please reduce the number of wp's");
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
                    {
                        Dialogs.CustomMessageBox.Show("ERROR", "Upload failed, mission was rejected byt the Mav,\n item had a bad option wp# " + a + " " + ans);
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                    {
                        // invalid sequence can only occur if we failed to see a response from the apm when we sent the request.
                        // or there is io lag and we send 2 mission_items and get 2 responces, one valid, one a ack of the second send
                        // the ans is received via mission_ack, so we dont know for certain what our current request is for. as we may have lost the mission_request
                        // get requested wp no - 1;
                        a = port.getRequestedWPNo() - 1;

                        continue;
                    }
                    if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                    {
                        var ErrorMessage = "Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
                                         " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString());
                        Dialogs.CustomMessageBox.Show("ERROR" , ErrorMessage );
                        return;
                    }
                }

                port.setWPACK();
                //((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

                // m    이거 원래 화면에서 받는것인데... 없으니까. 일단 그냥넣어 봅시다. 
                port.setParam("WP_RADIUS", GvarDesignModel.Instance.FDRAD / CurrentState.multiplierdist);

                // cm's
                port.setParam("WPNAV_RADIUS", GvarDesignModel.Instance.FDRAD / CurrentState.multiplierdist * 100.0);

                try
                {
                    port.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
                        GvarDesignModel.Instance.FDLoiterRad / CurrentState.multiplierdist);
                }
                catch
                {

                }
                           //((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(100, "Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainV2.comPort.giveComport = false;
                Dialogs.CustomMessageBox.Show("ERROR", ex.ToString() );
            }
            MainV2.comPort.giveComport = false;
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

