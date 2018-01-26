using DMP.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMP.Controls
{
    /// <summary>
    /// WPListControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WPListControl : UserControl
    {
        public WPListControl()
        {
            InitializeComponent();
        }

        private void WPListItemControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selecteditem = ((WPListItemControl)sender).Tag as WayPointModel;

            /// 나머지 애들은 모두 비 활성화 체크 한다. 
            foreach (var item in GvarDesignModel.Instance.WPList)
            {
                if (item.Index == selecteditem.Index)
                {
                    item.IsActive = true;
                }
                else
                {
                    item.IsActive = false;
                }
            }
        }
    }
}
