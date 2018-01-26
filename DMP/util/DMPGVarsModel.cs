

namespace DMP.util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using DMP.DataModels;
    using DMP;
    

    public class DMPGVarsModel : INotifyPropertyChanged
    {

        private static DMPGVarsModel instance;
        
        public DMPGVarsModel()
        {
            //instance ?? (instance = new DMPGVarsModel());
            var svc = new EnumHelperService();
            dpdLandingStyle = svc.MapEnumToDictionary<LandingType>();
            dpdDoWhenError = svc.MapEnumToDictionary<DoWhenError>();
            dpdShowHeight = svc.MapEnumToDictionary<ONOFF>();
            dpdShowDistance = svc.MapEnumToDictionary<ONOFF>();
            dpdHeadingType = svc.MapEnumToDictionary<Heading>();
            dpdCornerType = svc.MapEnumToDictionary<CornerType>();
            dpdInitialHeightType = svc.MapEnumToDictionary<InitialHeightType>();

            TargetPointList = new List<TargetPointModel>();
            WaypointList = new List<WayPointModel>();
        }
        public DMPGVarsModel Instance
        {
            get { return instance ?? (instance = new DMPGVarsModel()); }
        }

        public static DMPGVarsModel getInstance()
        {
           return instance ?? (instance = new DMPGVarsModel()); 
        }


        private float _initialHeight = Constants.HEIGHT_INITIAL;  // 초기 비행높이. 
        private float _speed = Constants.SPEED_DEFAULT ;        // 항속도 
        private int _initialHeightType = 0;
        private int _landing_style =  Constants.LANDING_TYPE_DEFAULT ;
        private float _landing_height = Constants.LANDING_DEFAULT;

        private int _do_when_error = (int)DoWhenError.HOVER;
        private int _showHeight =   1;   // show 
        private int _showDistance = 1; // show 
        private int _cornerType = (int) CornerType.STRAIGHT;
        private static int _headingtype = (int)Heading.WAYPOINT; // true : Heading  false : RC 


        private float _maxWPDistance = 0;
        private float _totalFlightDistance = 0;
        private float _expectedBatteryUsage = 0;
        private float _avgSpeed = 0;
        //private int _totalTargetCount = 0;    안쓴다. 
        //private int _totalWPCount = 0;        안쓴다. 



        private List<WayPointModel> waypointList;
        private List<TargetPointModel> targetPointList;

        private Dictionary<int, String> dpdLandingStyle = null;
        private Dictionary<int, String> dpdDoWhenError = null;
        private Dictionary<int, String> dpdShowHeight = null;
        private Dictionary<int, String> dpdShowDistance = null;
        private Dictionary<int, String> dpdHeadingType = null;
        private Dictionary<int, String> dpdInitialHeightType = null;
        private Dictionary<int, String> dpdCornerType = null;
        // private List<KeyValuePair(int , String)> dplLandingStyle = null;

        public int ILanding_style { get => _landing_style; set { _landing_style = value; OnPropertyChanged("ILanding_style"); } }
        public int IDowhenerror { get => _do_when_error; set { _do_when_error = value; OnPropertyChanged("IDowhenerror"); } }
        public int IShowHeight { get => _showHeight; set { _showHeight = value; OnPropertyChanged("IShowHeight"); } }
        public int IShowDistance { get => _showDistance; set { _showDistance = value; OnPropertyChanged("IShowDistance"); } }
        public int IHeadingType { get => _headingtype; set { _headingtype = value; OnPropertyChanged("IHeadingType"); } }

        public float ISpeed { get => _speed; set { _speed = value; OnPropertyChanged("ISpeed"); } }
        public float IInitialHeight { get => _initialHeight; set { _initialHeight = value; OnPropertyChanged("IInitialHeight"); } }
        public int IInitialHeightType { get => _initialHeightType; set { _initialHeightType = value; OnPropertyChanged("IInitialHeightType"); } }
        public float ILanding_height { get => _landing_height; set { _landing_height = value; OnPropertyChanged("ILanding_height"); } }
        public int ICornerType { get => _cornerType; set { _cornerType = value; OnPropertyChanged("ICornerType"); } }   

        /// <summary>
        /// 이 부분은 통계를 내서 보여 줘야함. 
        /// </summary>
        public int ITotalWPCount { get { return waypointList.Count; } }  // set 은 없다. 
        public int ITotalTPCount { get { return targetPointList.Count; } } 
        public float IAvgSpeed { get => _avgSpeed; set  {
                //return _speed ;

                if (waypointList == null || waypointList.Count == 0)
                    _avgSpeed = 0;

                float _avgs = 0 ;
                foreach ( var wp in waypointList )
                {
                    _avgs += wp.Speed;
                }

                _avgSpeed = _avgs / waypointList.Count;
        }  }

        // 이부분은 꼭 처리해 줘라... 
        public float MaxWPDistance { get { return  _maxWPDistance;} }   
        public float TotalFlightDistance { get => _totalFlightDistance; set {
                float val = 0;
                foreach (var wp in waypointList)
                {
                    val += wp.Speed;
                }
                _totalFlightDistance = val;
            } }
        public float ExpectedBatteryUsage { get => _expectedBatteryUsage; set => _expectedBatteryUsage = value; }


        public Dictionary<int, String> DpdLandingStyle { get => dpdLandingStyle; set { dpdLandingStyle = value; OnPropertyChanged("DpdLandingStyle"); } }
        public Dictionary<int, string> DpdDoWhenError { get => dpdDoWhenError; set { dpdDoWhenError = value; OnPropertyChanged("DpdDoWhenError"); } }
        public Dictionary<int, string> DpdShowHeight { get => dpdShowHeight; set { dpdShowHeight = value; OnPropertyChanged("DpdShowHeight"); } }
        public Dictionary<int, string> DpdShowDistance { get => dpdShowDistance; set { dpdShowDistance = value; OnPropertyChanged("DpdShowDistance"); } }
        public Dictionary<int, string> DpdHeadingType { get => dpdHeadingType; set { dpdHeadingType = value; OnPropertyChanged("DpdHeadingType"); } }
        public Dictionary<int, string> DpdCornerType { get => dpdCornerType; set { dpdCornerType = value; OnPropertyChanged("DpdCornerType");} }
        public Dictionary<int, string> DpdInitialHeightType { get => dpdInitialHeightType; set { dpdInitialHeightType = value; OnPropertyChanged("DpdInitialHeightType"); } }

        public List<WayPointModel> WaypointList { get => waypointList; set => waypointList = value; }
        public List<TargetPointModel> TargetPointList { get => targetPointList; set => targetPointList = value; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        //public static double CalculateDistanceWithGPS(double sLatitude, double sLongitude
        //    , double eLatitude, double eLongitude )
        public static double CalculateDistanceWithGPS(double lat1, double lon1 , double lon2, double lat2 , string unit )
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == "K")
            {
                dist = dist * 1.609344;
            }
            else if (unit == "N")
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private static  double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static  double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
