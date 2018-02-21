using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class GvarModel : ViewModelBase
    {

        #region     변수 선언 
        private float _initialHeight = Constants.HEIGHT_INITIAL;  // 초기 비행높이. 
        private float _speed = Constants.SPEED_DEFAULT;        // 항속도 
        private int _initialHeightType = 0;
        private int _landing_style = Constants.LANDING_TYPE_DEFAULT;
        private float _landing_height = Constants.LANDING_DEFAULT;

        private int _do_when_error = (int)DoWhenError.HOVER;
        private int _showHeight = 1;   // show 
        private int _showDistance = 1; // show 
        private int _cornerType = (int)DMP.CornerType.STRAIGHT;
        private static int _headingtype = (int)Heading.WAYPOINT; // true : Heading  false : RC 
        private WayPointModel _homePosition;
        private WayPointModel _langdingPosition;
        private int _pointType = (int)DMP.PointType.WAYPOINT;


        

        /// <summary>
        ///  현재 액션이 어느 메뉴에서 일어나고 있는지. 
        /// </summary>
        private int _currentMenuName;




        #region Calibration Data 
        private int _testThroattle = 7;
        #endregion


        // 절대압력값
        #region Flight Data 

        private string _flightFilename;
        private float _fdpress_abs;
        private float _fdtemperature;
        private float _fdspeed;
        private float _fdalt;
        private float _fdthroattle;
        private string _fdmsg;
        private float _fdheading;
        private float _fdbattery;
        private float _fdradius = 0; //roiter rad or spline rad 를 적는다. 
        private float _fd_loiter_radius = 0; //roiter rad or spline rad 를 적는다. 
        private float _fdLon = 0;
        private float _fdLat = 0;
        //Battery voltage of cells, in millivolts (1 = 1 millivolt).
        private int _fdvoltage = 0;

        private int _total_param_count = 0 ;
        private int _get_param_count = 0;
        private string _current_param_id = "";

        public float FDpressAbs { get { return _fdpress_abs; } set { _fdpress_abs = value; OnPropertyChanged(); } }
        public float FDtemperature { get => _fdtemperature; set { _fdtemperature = value; OnPropertyChanged(); } }
        public float FDspeed { get => _fdspeed; set { _fdspeed = value; OnPropertyChanged(); } }
        public float FDheading { get => _fdheading; set { _fdheading = value; OnPropertyChanged(); } }
        public float FDalt { get => _fdalt; set { _fdalt = value; OnPropertyChanged(); } }
        public float FDthroattle { get => _fdthroattle; set { _fdthroattle = value; OnPropertyChanged(); } }
        public string FDmsg { get => _fdmsg; set { _fdmsg = value; OnPropertyChanged(); } }
        public float FDbattery { get => _fdbattery; set { _fdbattery = value; OnPropertyChanged(); } }
        public float FDRAD { get => _fdradius; set { _fdradius = value; OnPropertyChanged(); } }
        public float FDLoiterRad { get => _fd_loiter_radius; set { _fd_loiter_radius = value; OnPropertyChanged(); } }
        public float FDLon { get => _fdLon; set { _fdLon = value; OnPropertyChanged(); } }
        public float FDLat { get => _fdLat; set { _fdLat = value; OnPropertyChanged(); } }
        public int FDvoltage { get => _fdvoltage; set { _fdvoltage = value; OnPropertyChanged(); }  }

        public int TotalParamCount { get => _total_param_count; set { _total_param_count = value; OnPropertyChanged(); } }
        public int GetParamCount { get => _get_param_count; set { _get_param_count = value; OnPropertyChanged(); } }
        public string CurrentParamId { get => _current_param_id; set { _current_param_id = value; OnPropertyChanged(); } }

        #endregion

        public string Name { get; set; }
        /// <summary>
        /// 드론이 날 수 있는 최대 거리. 
        /// </summary>
        private float _maxWPDistance = 0;
        /// <summary>
        /// 웨이 포인트 합산 거리 
        /// </summary>
        private double _totalFlightDistance = 0;
        /// <summary>
        /// 예상 빠떼리 소모 
        /// </summary>
        private float _expectedBatteryUsage = 0;

        /// <summary>
        /// 평균속도... 그럼 총 비행시간 나오네요. 
        /// </summary>
        private float _avgSpeed = 0;
        //private int _totalTargetCount = 0;    안쓴다. 
        //private int _totalWPCount = 0;        안쓴다. 

        /// <summary>
        /// Way Point 들 
        /// </summary>
        /// 
        private ObservableCollection<WayPointModel> wpList = new ObservableCollection<WayPointModel>();
        private ObservableCollection<WayPointModel> tpList = new ObservableCollection<WayPointModel>();
        //private List<WayPointModel> waypointList = new List<WayPointModel>();
        /// <summary> 
        /// Target Point 틀 
        /// </summary>
        //private List<TargetPointModel> targetPointList = new List<TargetPointModel>();


        #endregion

        #region 변수 처럼 보이지만 계산해서 보여 줘야 하는 부분 

        /// <summary>
        /// 이 부분은 통계를 내서 보여 줘야함. 
        /// </summary>
        public int ITotalWPCount { get { return wpList.Where(x => x.PointType == (int)DMP.PointType.WAYPOINT).Count<WayPointModel>(); } }  // set 은 없다. 
        public int ITotalTPCount { get { return wpList.Where(x => x.PointType == (int)DMP.PointType.TARGET).Count<WayPointModel>(); } }
        public float IAvgSpeed
        {
            get => _avgSpeed; set
            {
                //return _speed ;

                if (wpList == null || wpList.Count == 0)
                    _avgSpeed = 0;

                float _avgs = 0;
                foreach (var wp in wpList)
                {
                    _avgs += wp.Speed;
                }

                _avgSpeed = _avgs / wpList.Count;
            }
        }

        // 이부분은 꼭 처리해 줘라... 
        //public float MaxWPDistance { get { return _maxWPDistance; } }
        public double TotalFlightDistance
        {
            get => _totalFlightDistance;
            set { _totalFlightDistance = value; OnPropertyChanged(); }
            /*set
            {
                float val = 0;
                foreach (var wp in waypointList)
                {
                    val += wp.Speed;
                }
                _totalFlightDistance = val;
            }*/

        }
        public float ExpectedBatteryUsage { get => _expectedBatteryUsage; set => _expectedBatteryUsage = value; }


        #endregion

        #region 변수의 캡슐 부분
        public float InitialHeight { get => _initialHeight; set { _initialHeight = value; OnPropertyChanged(); } }
        public float Speed { get => _speed; set { _speed = value; OnPropertyChanged(); } }
        public int InitialHeightType { get => _initialHeightType; set { _initialHeightType = value; OnPropertyChanged(); } }
        public int Landing_style { get => _landing_style; set { _initialHeightType = value; OnPropertyChanged(); } }
        public float Landing_height { get => _landing_height; set { _landing_height = value; OnPropertyChanged(); } }
        public int Do_when_error { get => _do_when_error; set { _do_when_error = value; OnPropertyChanged(); } }
        public int ShowHeight { get => _showHeight; set { _showHeight = value; OnPropertyChanged(); } }
        public int ShowDistance { get => _showDistance; set { _showDistance = value; OnPropertyChanged(); } }
        public int CornerType { get => _cornerType; set { _cornerType = value; OnPropertyChanged(); } }
        public int Headingtype { get => _headingtype; set { _headingtype = value; OnPropertyChanged(); } }
        public float MaxWPDistance { get => _maxWPDistance; set { _maxWPDistance = value; OnPropertyChanged(); } }
        
        public float AvgSpeed { get => _avgSpeed; set { _avgSpeed = value; OnPropertyChanged(); } }

        public ObservableCollection<WayPointModel> WPList { get => wpList; set { wpList = value; OnPropertyChanged(); } }

        //public List<WayPointModel> WaypointList { get => waypointList; set { waypointList = value; OnPropertyChanged(); } }
        public ObservableCollection<WayPointModel> TPList { get => tpList; set { tpList = value; OnPropertyChanged(); } }
        public int TestThroattle { get => _testThroattle; set { _testThroattle = value; OnPropertyChanged(); } }
        public string FlightFilename { get => _flightFilename; set { _flightFilename = value; OnPropertyChanged(); } }
        public WayPointModel HomePosition { get => _homePosition; set { _homePosition = value; OnPropertyChanged(); } }
        public int PointType { get => _pointType; set { _pointType = value; OnPropertyChanged(); } }
        /*
        public WayPointModel LangdingPosition
        {
            get => _landingPosition; set { _landingPosition = value; OnPropertyChanged(); }
        }
        */
        /// <summary>
        /// 현재의 페이지가 waypoint인지 TargetPoint인지, Ready 등등인지.. 저장한다...
        /// </summary>
        public int CurrentMenuName
        {
            get=>_currentMenuName ; set { _currentMenuName = value; OnPropertyChanged(); }
        }

        
        #endregion


    }
}
