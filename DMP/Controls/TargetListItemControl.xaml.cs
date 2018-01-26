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
            var selecteditem = ((Button)sender).Tag as TargetPointModel;
            int selectedi = selecteditem.Index;
            SortingTargetPoint( true , selectedi );
        }

        private void btnDown_Click(object sender, RoutedEventArgs e )
        {
            var selecteditem = ((Button)sender).Tag as TargetPointModel;
            int selectedi = selecteditem.Index;
            SortingTargetPoint(false , selectedi);
        }
        private void SortingTargetPoint( bool up , int selectedIndex )
        {
            //Sorting 시작과 끝에 한번씩 해주자... 
            GvarDesignModel.Instance.TPList 
                = (ObservableCollection<WayPointModel>)GvarDesignModel.Instance.TPList.OrderBy(f => f.Index);

            // 업을 할때 1번째는 업이 안된다. 
            if (up && selectedIndex == 1) return;

            // 다운일때 맨 마지막은 다운이 않된다. 
            if (!up && selectedIndex == DMP.DataModels.GvarDesignModel.Instance.TPList.Count) return;

            /// 순서를 바꾸어 보자... 
            int i = 0;
            foreach (var item in DMP.DataModels.GvarDesignModel.Instance.TPList)
            {
                i++;
                if ( up )
                {
                    if (item.Index == selectedIndex - 1)
                    {
                        item.Index = i + 1;
                    }
                    else if (item.Index == selectedIndex)
                    {
                        item.Index = i - 1;
                    }
                }
                else   // Down 
                {
                    if (item.Index == selectedIndex )
                    {
                        item.Index = i + 1;
                    }
                    else if (item.Index == selectedIndex +1 )
                    {
                        item.Index = i - 1;
                    }
                }
            }
            DMP.DataModels.GvarDesignModel.Instance.TPList 
                = (ObservableCollection<WayPointModel>)DMP.DataModels.GvarDesignModel.Instance.TPList.OrderBy(f => f.Index) ;
        }
    }
}
