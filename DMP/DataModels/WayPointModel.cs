using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DMP;
using DMP.DataModels.Commands;
using Microsoft.Maps.MapControl.WPF;
using MissionPlanner.Utilities;

namespace DMP.DataModels
{
    public class WayPointModel : ViewModelBase
    {
        #region 생성자 
        public WayPointModel()
        {
            Speed = DefaultValue.DefaultSpeed;
            Height = DefaultValue.DefaultHeight;
            Bearing = DefaultValue.DefaultBearing;

            createCommand();
        }

        /// <summary>
        /// Locationwp라는 구조체를 기체로 부터 받는다면 WaypointModel로 전환해 준다. 
        /// </summary>
        /// <param name="wp"></param>
        public WayPointModel(Locationwp wp)
        {
            SetLocationwp(wp);
        }
        #endregion

        private void createCommand()
        {
            /// Can Use 는 WP나 TP나 같이 사용한다. 
            WPUpCommand = new RelayCommand(WPUp, WPUpButtonCanUse);
            WPDownCommand = new RelayCommand(WPDown, WPDownButtonCanUse);
            WPDeleteCommand = new RelayCommand(WPDelete, WPDeleteButtonCanUse);
            TPDeleteCommand = new RelayCommand(TPDelete, TPDeleteButtonCanUse);
            TPUpCommand = new RelayCommand(TPUp, WPUpButtonCanUse);
            TPDownCommand = new RelayCommand(TPDown, WPDownButtonCanUse);


        }

        #region COMMAND 를 만든다. WPUP WPDown이 있다. 

        public RelayCommand WPUpCommand { get; private set; }
        public RelayCommand WPDownCommand { get; private set; }
        public RelayCommand WPDeleteCommand { get; private set; }
        public RelayCommand TPDeleteCommand { get; private set; }
        public RelayCommand TPUpCommand { get; private set; }
        public RelayCommand TPDownCommand { get; private set; }


        /// <summary>
        /// 순서 올리기...    PolyLine도 다시 그려야 한다..
        /// </summary>
        /// <param name="sender"></param>
        public void WPUp(object sender)
        {
            if (sender != null)
            {
                var wp = sender as WayPointModel;
                //Console.WriteLine("WayPointModel.WP Up : " + wp.Index);
                GvarDesignModel.Instance.SortAllKindWayPoint();
            }
            else
            {
                //Console.WriteLine("WayPointModel.WP Up");
            }
        }
        /// <summary>
        /// 순서 올리기...    PolyLine도 다시 그려야 한다..
        /// </summary>
        /// <param name="sender"></param>
        public void TPUp(object sender)
        {
            if (sender != null)
            {
                var p = sender as WayPointModel;
                //Console.WriteLine("WayPointModel.WP Up : " + p.Index);
                //SortingPoint(true, wp.Index);
                GvarDesignModel.Instance.ChangeTargetPointOrder(true, p.Index);
            }
            else
            {
                //Console.WriteLine("WayPointModel.WP Up");
            }
        }
        /// <summary>
        ///  순서 내리기....  PolyLine도 다시                      한다.. 
        /// </summary>
        /// <param name="sender"></param>
        public void WPDown(object sender)
        {
            if (sender != null)
            {
                var wp = sender as WayPointModel;
                Console.WriteLine("WayPointModel.WP Down : " + wp.Index);
                GvarDesignModel.Instance.ChangeWayPointOrder(false, wp.Index);
                //SortingWayPoint(false, wp.Index);
            }
            else
            {
                Console.WriteLine("ERROR .. sender가 없다. WPDOWN");
            }

        }
        /// <summary>
        ///  순서 내리기....  PolyLine도 다시                      한다.. 
        /// </summary>
        /// <param name="sender"></param>
        public void TPDown(object sender)
        {
            if (sender != null)
            {
                var p = sender as WayPointModel;
                GvarDesignModel.Instance.ChangeTargetPointOrder(false, p.Index);
            }
            else
            {
                Console.WriteLine("Error ,,sender가 없다. TPDown");
            }

        }

        /// <summary>
        /// WP를 지운다... 
        /// </summary>
        /// <param name="sender"></param>
        public void WPDelete(object sender)
        {
            var result = DMP.Dialogs.CustomMessageBox.Show("삭제하시겠습니까?", "주의", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
                return;

            if(sender != null)
            {
                var p = sender as WayPointModel;
                GvarDesignModel.Instance.WPList.Remove(p);
            }
            MapDesignModel.Instance.DmlPushpin.Children.Clear();
            MapDesignModel.Instance.DmlPolyline.Children.Clear();
            MapDesignModel.Instance.LoadPushpinFromWaypointList();
            GvarDesignModel.Instance.SortAllKindWayPoint();
        }
        /// <summary>
        /// WP를 지운다... 
        /// </summary>
        /// <param name="sender"></param>
        public void TPDelete(object sender)
        {
            var result = DMP.Dialogs.CustomMessageBox.Show("삭제하시겠습니까?", "주의", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
                return;
            if (sender != null)
            {
                var p = sender as WayPointModel;
                GvarDesignModel.Instance.TPList.Remove(p);
            }
            /// 모든 애들을 지운후 다시 그린다.... 
            MapDesignModel.Instance.DmlPushpin.Children.Clear();
            MapDesignModel.Instance.DmlPolyline.Children.Clear();
            MapDesignModel.Instance.LoadPushpinFromWaypointList();
            GvarDesignModel.Instance.SortAllKindWayPoint();
        }

        /// <summary>
        /// 이버튼은 항상 보인다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool WPDeleteButtonCanUse(object sender)
        {
            return true;
        }
        /// <summary>
        /// 이버튼은 항상 보인다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool TPDeleteButtonCanUse(object sender)
        {
            return true;
        }

        /// <summary>
        /// 맨처음 WP 이면 위로 버튼이 활성화 않됨.... 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool WPUpButtonCanUse(object sender)
        {
            if (sender != null)
            {
                var wp = sender as WayPointModel;
                //Console.WriteLine("WPUpButttonCanUse Index" + wp.Index + " Latitude : " + wp.Latitude);
                if (wp.Index == 1) return false;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 맨마지막 WP 이면 아래로 버튼이 활성화 않됨.... 
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool WPDownButtonCanUse(object sender)
        {
            if (sender != null)
            {
                var wp = sender as WayPointModel;
                //Console.WriteLine("WPDownButttonCanUse Index" + wp.Index + " Latitude : " + wp.Latitude);
                if (wp.Index == GvarDesignModel.Instance.WPList.Count) return false;
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region 변수 선언 부분        
        private int _index;
        private float _height;
        private float _bearing;
        private double _longitude;
        private double _latitude;
        private float _action;
        private string _name = "Model";
        private float _speed = 0;

        private double _distanceFromPrev = 0;
        private float _timeFromPrev = 0; // 이전 wp 로부터 비행하는데 필요한 시간이다.
                                         // 배터리 소요 예측은 시간으로 해야한다.
        public float p1;				// param 1
        public float p2;				// param 2
        public float p3;				// param 3
        public float p4;				// param 4
        public ushort command_id;
        private bool _isActive;

        private int _target;  // target 리스트중 1개 임.
        #endregion

        #region 변수 Setter , getter 
        /// <summary>
        /// Enum.PointType 나중에 WayPointModel , TargetModel합치자... 
        /// </summary>
        public int PointType {
            get;
            set; /// 타입을 바꾸어 주지는 않는다.
        }
        /// <summary>
        /// Min , Max 
        /// </summary>
        public float Height { get => _height; set {
                if (value > Constants.HEIGHT_MAX)
                {
                    _height = Constants.HEIGHT_MAX;
                } else if (value < Constants.HEIGHT_MIN)
                {
                    _height = Constants.HEIGHT_MIN;
                }
                else
                {
                    _height = value;
                }

                OnPropertyChanged();

            } }
        public float Bearing { get => _bearing; set
            {
                
                _bearing = value;
                OnPropertyChanged();
            }
        }
        public int Index { get => _index; set { _index = value; OnPropertyChanged(); } }
        public double Longitude { get => _longitude; set { _longitude = value; OnPropertyChanged(); } }
        public double Latitude { get => _latitude; set { _latitude = value; OnPropertyChanged(); } }
        public float Action { get => _action; set { _action = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public int   Target { get => _target; set { _target = value; OnPropertyChanged(); } }
        public float Speed { get => _speed; set { _speed = value; OnPropertyChanged(); } }
        public double DistanceFromPrev { get => _distanceFromPrev; set { _distanceFromPrev = value; OnPropertyChanged(); } }
        public float TimeFromPrev { get => _timeFromPrev; set { _timeFromPrev = value; OnPropertyChanged(); } }
        public bool IsActive { get => _isActive; set { _isActive = value; OnPropertyChanged(); } }

        public Location Location
        {
            get { return new Location(Latitude , Longitude); }
            set { Latitude = value.Latitude; Longitude = value.Longitude; OnPropertyChanged(); }
        }
        public Locationwp GetLocationwp()
        {
            
                Locationwp temp = new Locationwp();
                temp.lat = Latitude;
                temp.lng = Longitude;
                temp.alt = Height;

            if (PointType == (int)DMP.PointType.TARGET)
            {
                // 새로운 버전에서는 이것을 사용해야하지만 아직 아래것을 쓴다. 2018.2.7 일
                //temp.id = (ushort)MAVLink.MAV_CMD.MAV_CMD_DO_SET_ROI_LOCATION;
                temp.id = (ushort)MAVLink.MAV_CMD.DO_SET_ROI;
            }   
            else if (PointType == (int)DMP.PointType.HOME)
                temp.id = (ushort)MAVLink.MAV_CMD.DO_SET_HOME;
            else
                temp.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;



                temp.p1 = p1;
                temp.p2 = p2;
                temp.p3 = p3;
                temp.p4 = p4;
                temp.Tag = Name;
                return temp ;
               
        }
        public void SetLocationwp(Locationwp wp )
        {
            Index = GvarDesignModel.Instance.WPList.Count + 1; //Index는 1부터 시작한다. 그러니까 +1 해준다. 
            Height = wp.alt;
            Latitude = wp.lat;
            Longitude = wp.lng;
            command_id = wp.id;
            p1 = wp.p1;
            p2 = wp.p2;
            p3 = wp.p3;
            p4 = wp.p4;
        }

        #endregion
    }
}
