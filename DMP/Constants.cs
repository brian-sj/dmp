using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP
{
    class Constants 
    {
        public const float HEIGHT_MAX = 500  ;
        public const float HEIGHT_MIN = -200 ;
        public const float HEIGHT_INITIAL = 10; // m 

        public const float SPEED_DEFAULT = 18;// m/s

        public const float LANDING_DEFAULT = 10; // m 
        public const int LANDING_TYPE_DEFAULT = (int)LandingType.RTH;

        public const float BEARING_MAX = 270; // Bearing Max 값 
        public const float BEARING_MIN = 0;   // Bearing Min 값 
    }
}
