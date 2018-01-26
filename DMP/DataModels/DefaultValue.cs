using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class DefaultValue
    {
        //private static DefaultValue _instance;
        //public static DefaultValue Instance => _instance ?? (_instance = new DefaultValue());

        public static float DefaultHeight = 10         ;
        public static float DefaultSpeed = 10          ;
        public static float DefaultBearing = 10        ;
        public static float DefaultTargetAltitude = 0  ;
        public static float DefaultWayPointHeight = 15 ;

        /// <summary>
        /// 사용자 설정을 받아다가 여기에다가 적어 준다. 
        /// </summary>
        public DefaultValue()
        {
            
        }
        

    }
}
