using DMP.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMP;

namespace DMP.DataModels
{
    public sealed class EnumListDictionary : ViewModelBase
    {
        #region Singleton
        /// <summary>
        /// A single instance (singleton) of the 데이터 입력하게.... 
        /// </summary>
        private static EnumListDictionary _instance;
        public static EnumListDictionary Instance {
            get
            {
                if( _instance == null)
                {
                    _instance = new EnumListDictionary();
                }
                return _instance;
            }
        }
    #endregion Singleton

        public Dictionary<int, string> DpdLandingStyle { get => dpdLandingStyle; set => dpdLandingStyle = value; }
        public Dictionary<int, string> DpdDoWhenError { get => dpdDoWhenError; set => dpdDoWhenError = value; }
        public Dictionary<int, string> DpdShowHeight { get => dpdShowHeight; set => dpdShowHeight = value; }
        public Dictionary<int, string> DpdShowDistance { get => dpdShowDistance; set => dpdShowDistance = value; }
        public Dictionary<int, string> DpdHeadingType { get => dpdHeadingType; set => dpdHeadingType = value; }
        public Dictionary<int, string> DpdInitialHeightType { get => dpdInitialHeightType; set => dpdInitialHeightType = value; }
        public Dictionary<int, string> DpdCornerType { get => dpdCornerType; set => dpdCornerType = value; }
        public Dictionary<int, string> DpdActionType { get => dpdActionType; set => dpdActionType = value; }

        public EnumListDictionary()
        {
            EnumHelperService svc = new EnumHelperService();

            dpdLandingStyle = svc.MapEnumToDictionary<LandingType>();
            dpdDoWhenError = svc.MapEnumToDictionary<DoWhenError>();
            dpdShowHeight = svc.MapEnumToDictionary<ONOFF>();
            dpdShowDistance = svc.MapEnumToDictionary<ONOFF>();
            dpdHeadingType = svc.MapEnumToDictionary<Heading>();
            dpdCornerType = svc.MapEnumToDictionary<CornerType>();
            dpdInitialHeightType = svc.MapEnumToDictionary<InitialHeightType>();
            dpdActionType = svc.MapEnumToDictionary<ACTIONTYPE>();
        }

        public static Dictionary<int, String> GetHeadingTypes()
        {
            return EnumListDictionary.Instance.DpdHeadingType;
        }

        public static Dictionary<int,String> GetActionTypes()
        {
            return EnumListDictionary.Instance.DpdActionType;
        }

        /*
        private Dictionary<int, String> dpdLandingStyle = null;
        private Dictionary<int, String> dpdDoWhenError = null;
        private Dictionary<int, String> dpdShowHeight = null;
        private Dictionary<int, String> dpdShowDistance = null;
        private Dictionary<int, String> dpdHeadingType = null;
        private Dictionary<int, String> dpdInitialHeightType = null;
        private Dictionary<int, String> dpdCornerType = null;
        private Dictionary<int, String> dpdActionType = null;
        */
        private Dictionary<int, String> dpdLandingStyle ;
        private Dictionary<int, String> dpdDoWhenError ;
        private Dictionary<int, String> dpdShowHeight ;
        private Dictionary<int, String> dpdShowDistance ;
        private Dictionary<int, String> dpdHeadingType ;
        private Dictionary<int, String> dpdInitialHeightType ;
        private Dictionary<int, String> dpdCornerType ;
        private Dictionary<int, String> dpdActionType ;
    }
}
