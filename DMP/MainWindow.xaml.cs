using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using DMP.pages;
using MissionPlanner.Mavlink;
using MissionPlanner;
using log4net;
using System.Reflection;
using MissionPlanner.Utilities;
using DMP.Dialogs;
using MissionPlanner.Controls;
using DMP.Resources;
using DMP.DataModels;
using System.Collections;
using ToastNotifications.Core;
using DMP.util;

namespace DMP
{
    /// <summary>
    /// 2017.10.31일 Brian 
    /// 첫 화면
    /// </summary>
    public partial class MainWindow 
    {
        /* ##################
         * Global Variable  
         * 
         */

        /* ############################## 
         *   Ready 설정 변수 
         */
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int _count = 0;
        private readonly ToastViewModel _vm;
        private string _lastMessage;
        public static MAVLinkInterface comPort
        {
            get
            {
                return _comPort;
            }
            set
            {
                if (_comPort == value)
                    return;
                _comPort = value;
                _comPort.MavChanged -=  MainV2.instance.comPort_MavChanged;
                _comPort.MavChanged += MainV2.instance.comPort_MavChanged;
                MainV2.instance.comPort_MavChanged(null, null);
            }
        }

        static MAVLinkInterface _comPort = new MAVLinkInterface();
        public static List<MAVLinkInterface> Comports = new List<MAVLinkInterface>();


        public MainWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            _vm = new ToastViewModel();
            Unloaded += OnUnload;

            //ShowMessage(_vm.ShowInformation, "Information");
        }

        public void ShowMessage(Action<string, MessageOptions> action, string name)
        {
            MessageOptions opts = new MessageOptions
            {
                CloseClickAction = CloseAction,
                Tag = $"[This is Tag Value ({++_count})]",
                //FreezeOnMouseEnter = cbFreezeOnMouseEnter.IsChecked.GetValueOrDefault(),
                //UnfreezeOnMouseLeave = cbUnfreezeOnMouseLeave.IsChecked.GetValueOrDefault(),
                //ShowCloseButton = cbShowCloseButton.IsChecked.GetValueOrDefault()
            };
            _lastMessage = $"{_count} {name}";
            action(_lastMessage, opts);
            //bClearLast.IsEnabled = true;
        }
        private void CloseAction(NotificationBase obj)
        {
            var opts = obj.DisplayPart.GetOptions();
            _vm.ShowInformation($"Notification close clicked, Tag: {opts.Tag}");
        }


        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _vm.OnUnloaded();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //Main.Content = new Page1();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Main.Content = new Page2();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Button_target(object sender, RoutedEventArgs e)
        {
            var pagename = Main.Content.GetType();
            GvarDesignModel.Instance.CurrentMenuName = (int)CurrentMenuName.TARGET;
            GvarDesignModel.Instance.PointType = (int)DMP.PointType.TARGET;
            if (pagename.Name != "PageWp")
            {
                Main.Content = new PageWp(); 
            }
        }

        private void Button_ready(object sender, RoutedEventArgs e)
        {
            GvarDesignModel.Instance.CurrentMenuName = (int)CurrentMenuName.READY;
            Main.Content = new PageReady();
        }

        private void Button_wp(object sender, RoutedEventArgs e)
        {
            var pagename = Main.Content.GetType();
            GvarDesignModel.Instance.CurrentMenuName = (int)CurrentMenuName.WAYPOINT;
            GvarDesignModel.Instance.PointType = (int)DMP.PointType.WAYPOINT;
            if (pagename.Name != "PageWp")
            {
                Main.Content = new PageWp( );
            }
            else
            {
                
            }
        }
        private void Button_review(object sender, RoutedEventArgs e)
        {
            var pagename = Main.Content.GetType();
            GvarDesignModel.Instance.CurrentMenuName = (int)CurrentMenuName.REVIEW;
            if (pagename.Name != "PageWp")
            {
                Main.Content = new PageWp();
            }
        }
        private void Button_play(object sender, RoutedEventArgs e)
        {
            _vm.ShowInformation("ddd");
            var pagename = Main.Content.GetType();
            GvarDesignModel.Instance.CurrentMenuName = (int)CurrentMenuName.PLAY;
            if (pagename.Name != "PageWp")
            {
                Main.Content = new PageWp();
            }
        }

        private void Button_help(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageHelp();
        }
        private void Button_setting(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageSetting();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            /**** ####  홈정보를 입력한후   */
            Locationwp home = new Locationwp();
            try
            {
                home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                home.alt = 10;
                home.lat = 37.3795214;
                home.lng = 126.6758602;
            }
            catch {
            }

            ///foreach() 여기다가는 WAYPoint를 저장하고 정보가 문제가 없는지 확인한다.
            ///

            DMP.Dialogs.ProgressReporterDialogue frmProgressReporter = new DMP.Dialogs.ProgressReporterDialogue();

            frmProgressReporter.DoWork += saveWPs;
            frmProgressReporter.UpdateProgressAndStatus( -1 , "Sending WP's ");
            frmProgressReporter.RunBackgroundOperationAsync();
            //frmProgressReporter.Dispose();
        }
        /// <summary>
        /// 드론에 WPS를 저장을 한다...  이 함수는 GvarDesignModel에 집어 넣는다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="passdata"></param>
        private void saveWPs(object sender, ProgressWorkerEventArgs e, object passdata = null)
        {
            try
            {
                MAVLinkInterface port = MainV2.comPort;
                var wplist = GvarDesignModel.Instance.WPList;

                if (!port.BaseStream.IsOpen)
                {
                    throw new Exception("Pls connect first");
                }
                MainV2.comPort.giveComport = true;
                int a = 0;

                Locationwp home = new Locationwp();
                /// 홈포지션은 waypoints에서 가지고 와서 타입이 home인 애를 가져온다. 
                try
                {
                    home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                    home.lat = (double)37.3795214;// home 정보 넣어 주라..  ;
                    home.alt = (float)100;
                    home.lng = (double)126.6758602;
                }
                catch { throw new Exception("Your Home location is invalid "); }

                // log
                log.Info("wps values " + MainV2.comPort.MAV.wps.Values.Count);
                log.Info("cmd rows " + (wplist.Count + 1)); // + home

                // check for changes / future mod to send just changed wp's
                if (MainV2.comPort.MAV.wps.Values.Count == (wplist.Count + 1))
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
                        //MAVLink.mavlink_mission_item_t temp = DataViewtoLocationwp(a);
                        MAVLink.mavlink_mission_item_t temp = WPModeltoLocationwp(a);


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
                            wpstoupload[a] = "";
                        }

                        a++;
                    }
                }

                bool use_int = (port.MAV.cs.capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

                // set wp total
                ((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

                ushort totalwpcountforupload = (ushort)(wplist.Count + 1);

                if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                {
                    totalwpcountforupload--;
                }

                port.setWPTotal(totalwpcountforupload); // + home

                // set home location - overwritten/ignored depending on firmware.
                ((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

                // upload from wp0
                a = 0;

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

                // define the default frame.
                MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

                // get the command list from the datagrid
                var commandlist = WayPointConvertUtility.GetCommandList(); //GetCommandList();

                // process commandlist to the mav
                for (a = 1; a <= commandlist.Count; a++)
                {
                    var temp = commandlist[a - 1];

                    ((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / wplist.Count,
                        "Setting WP " + a);

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
                        e.ErrorMessage = "Upload failed, please reduce the number of wp's";
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
                    {
                        e.ErrorMessage =
                            "Upload failed, mission was rejected byt the Mav,\n item had a bad option wp# " + a + " " +
                            ans;
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
                        e.ErrorMessage = "Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
                                         " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString());
                        return;
                    }
                }

                port.setWPACK();
                ((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

                // m    이거 원래 화면에서 받는것인데... 없으니까. 일단 그냥넣어 봅시다. 
                port.setParam("WP_RADIUS",  GvarDesignModel.Instance.FDRAD / CurrentState.multiplierdist);

                // cm's
                port.setParam("WPNAV_RADIUS", GvarDesignModel.Instance.FDRAD / CurrentState.multiplierdist * 100.0);

                try
                {
                    port.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
                        GvarDesignModel.Instance.FDLoiterRad  / CurrentState.multiplierdist);
                }
                catch
                {

                }
                ((Dialogs.ProgressReporterDialogue)sender).UpdateProgressAndStatus(100, "Done.");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MainV2.comPort.giveComport = false;
                throw;
            }

            MainV2.comPort.giveComport = false;
        }

        /// <summary>
        /// WP 리스트에서 locationwp 구조체 형태를 가져오는 함수 이다. 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private Locationwp WPModeltoLocationwp(int a)
        {
            try
            {
                Locationwp temp = new Locationwp();
                WayPointModel data =null;//= GvarDesignModel.Instance.WaypointList.Where(wp => ((WayPointModel)wp).Index == a );
                foreach (var item in GvarDesignModel.Instance.WPList)
                {
                    if (item.Index == a)
                    {
                        data = item;
                        break;
                    }
                } 
                if(data == null)
                    throw new FormatException("invalid number of wp's list " + (a + 1).ToString(), null);

                temp.lat = data.Latitude;
                temp.lng =  data.Longitude;
                temp.alt = data.Height;
                temp.id = data.command_id;
                temp.p1 = data.p1;
                temp.p2 = data.p2;
                temp.p3 = data.p3;                          
                temp.p4 = data.p4;  /// 총 7개 파라메터가 있는데 나머지 세개가 lat , lng , alt 이다. 
                temp.Tag = data.Name;

                return temp;
            }
            catch(Exception e )
            {
                throw new FormatException("invalid number of wp's list "+ (a+1).ToString() , e );
            }
        }

        private void getWP_Click(object sender, RoutedEventArgs e)
        {
            MAVLinkInterface port = MainV2.comPort;
            Locationwp wp;

            try
            {
                int total = port.getWPCount();   // 신기하게도 Home position을 포함하는지 꼭 한개가 추가가 된다. 
                for (ushort i = 1; i < total; i++)
                {
                    wp = port.getWP(i);
                }
                port.setWPACK();
            }
            catch (Exception ed) {  }
        }
        private void temp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                Locationwp home = MainV2.comPort.getHomePosition();
                WayPointModel wp = WayPointConvertUtility.LocationwpToWayPoint(home);
                GvarDesignModel.Instance.HomePosition = wp;
            }
            catch (Exception ee ) { Dialogs.CustomMessageBox.Show("ERROR", "HomePoint를 읽을 수 없습니다."); }
            
        }
    }
}
