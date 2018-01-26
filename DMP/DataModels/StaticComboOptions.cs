using DMP.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class StaticComboOptions
    {
        public StaticComboOptions()
        {
            var svc = new EnumHelperService();
            dpdLandingStyle = svc.MapEnumToDictionary<LandingType>();
            dpdDoWhenError = svc.MapEnumToDictionary<DoWhenError>();
            dpdShowHeight = svc.MapEnumToDictionary<ONOFF>();
            dpdShowDistance = svc.MapEnumToDictionary<ONOFF>();
            dpdHeadingType = svc.MapEnumToDictionary<Heading>();
            dpdCornerType = svc.MapEnumToDictionary<CornerType>();
            dpdInitialHeightType = svc.MapEnumToDictionary<InitialHeightType>();
        }
 #region 콤보 박스에 보여줄 데이터 
        /// <summary>
        ///  랜딩 스타일 
        /// </summary>
        private Dictionary<int, String> dpdLandingStyle = null;
        /// <summary>
        /// 에러가 발생한경우 .네트웍 끊겼을때 
        /// </summary>
        private Dictionary<int, String> dpdDoWhenError = null;
        /// <summary>
        /// 고도 보여줄까?
        /// </summary>
        private Dictionary<int, String> dpdShowHeight = null;
        /// <summary>
        /// 거리 보여 줄까?
        /// </summary>
        private Dictionary<int, String> dpdShowDistance = null;
        /// <summary>
        /// 머리는 어떻게 할까?
        /// </summary>
        private Dictionary<int, String> dpdHeadingType = null;

        /// <summary>
        /// 초기높이는 어디를 따를까?
        /// </summary>
        private Dictionary<int, String> dpdInitialHeightType = null;
        /// <summary>
        /// 코너 돌아갈때 크게 돌까 작게 돌까 ?
        /// </summary>
        private Dictionary<int, String> dpdCornerType = null;
        #endregion

#region   public part
        public Dictionary<int, string> DpdLandingType { get => dpdLandingStyle; set => dpdLandingStyle = value; }
        public Dictionary<int, string> DpdDoWhenError { get => dpdDoWhenError; set => dpdDoWhenError = value; }
        public Dictionary<int, string> DpdShowHeight { get => dpdShowHeight; set => dpdShowHeight = value; }
        public Dictionary<int, string> DpdShowDistance { get => dpdShowDistance; set => dpdShowDistance = value; }
        public Dictionary<int, string> DpdHeadingType { get => dpdHeadingType; set => dpdHeadingType = value; }
        public Dictionary<int, string> DpdInitialHeightType { get => dpdInitialHeightType; set => dpdInitialHeightType = value; }
        public Dictionary<int, string> DpdCornerType { get => dpdCornerType; set => dpdCornerType = value; }
#endregion
    }
}
