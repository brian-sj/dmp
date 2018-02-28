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
            _vm.ShowInformation("지도를 더블 클릭하면 관심 지점이 추가됩니다.");
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


            _vm.ShowInformation("지도를 더블 클릭하면  지점이 추가됩니다.");

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
            MapDesignModel.Instance.setMaxWPDistanceNavgSpeed();
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
    }
}
