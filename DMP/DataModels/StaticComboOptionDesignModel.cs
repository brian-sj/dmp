using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class StaticComboOptionDesignModel  : StaticComboOptions 
    {
        #region Singleton
        /// <summary>
        /// A single instance (singleton) of the 콤보박스에 넣을 데이터들이다... 
        /// </summary>
        private static StaticComboOptionDesignModel _instance;
        public static StaticComboOptionDesignModel Instance => _instance ?? (_instance = new StaticComboOptionDesignModel());
        #endregion Singleton
    }
}
