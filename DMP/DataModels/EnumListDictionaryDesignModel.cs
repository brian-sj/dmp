using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public sealed class  EnumListDictionaryDesignModel
    {

        public static Dictionary<int, String> GetHeadingTypes()
        {
            return EnumListDictionary.Instance.DpdHeadingType;
        }

        public static Dictionary<int, String> GetActionTypes()
        {
            return EnumListDictionary.Instance.DpdActionType;
        }
    }
}
