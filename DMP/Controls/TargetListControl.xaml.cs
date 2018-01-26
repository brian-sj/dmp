
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using DMP.DataModels;

namespace DMP.Controls
{
    /// <summary>
    /// TargetListControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TargetListControl : UserControl
    {
        public TargetListControl()
        {
            InitializeComponent();

        }



        /// <summary>
        /// TargetPoint에는 폴리라인이 필요 없다. 그런데... WayPoint에는 폴리라인이 필요하다..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetListItemControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selecteditem = ((TargetListItemControl)sender).Tag as TargetPointModel;
            
            /// 나머지 애들은 모두 비 활성화 체크 한다. 
            foreach (var item in GvarDesignModel.Instance.TPList)
            {
                if(  item.Index == selecteditem.Index)
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
