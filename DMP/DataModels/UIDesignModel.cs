using DMP.DataModels.Commands;
using DMP.util;
using Microsoft.Win32;
using MissionPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DMP.DataModels
{
    public class UIDesignModel : ViewModelBase
    {
        #region Singleton 
        /// <summary>
        /// A single instance (singleton) of the 데이터 입력하게.. d.. 
        /// </summary>
        private static UIDesignModel _instance;
        public static UIDesignModel Instance => _instance ?? (_instance = new UIDesignModel());
        
        #endregion Singleton
        /// <summary>
        ///  생성자.... 각종 변수 및 타겟 포인트 , 웨이포인트를 파일로 부터 읽어 와서 초기화 한다. 
        /// </summary>
        public UIDesignModel() 
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
            ChangeStatusCommand = new RelayCommand(ChangeStatus, ChangeStatusCanUse);
            //SaveXmlFileCommand = new RelayCommand(SaveXmlFile, SaveXmlFileCanUse);
        }


        private int _status;
        public int Status { get => _status; set { _status = value; OnPropertyChanged(); } }

        #region Button Command
        public RelayCommand ChangeStatusCommand { get; private set;}
        public void ChangeStatus(object parameters)
        {
            //if( parameters.Equals )
        }
        public bool ChangeStatusCanUse(object sender)
        {
            return true;
        }

        #endregion

    }
}
