using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DMP.DataModels;

namespace DMP.Controls
{
    /// <summary>
    /// TargetListItemControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TargetListItemControl : UserControl
    {
        public TargetListItemControl()
        {
            InitializeComponent();
        }
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            var selecteditem = ((Button)sender).Tag as WayPointModel;
            int selectedi = selecteditem.Index;
            SortingTargetPoint( true , selectedi );
        }

        private void btnDown_Click(object sender, RoutedEventArgs e )
        {
            var selecteditem = ((Button)sender).Tag as WayPointModel;
            int selectedi = selecteditem.Index;
            SortingTargetPoint(false , selectedi);
        }
        private void SortingTargetPoint( bool up , int selectedIndex )
        {
            GvarDesignModel.Instance.ChangeTargetPointOrder(up, selectedIndex);
            GvarDesignModel.Instance.SortAllKindWayPoint();
        }
    }
}
