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
using Microsoft.Maps.MapControl.WPF;
using GMap.NET.MapProviders;
using System.Net;
using DMP.DataModels;
using MissionPlanner;

namespace DMP
{
    /// <summary>
    /// page_setting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageSetting : Page
    {

        // A collection of key/value pairs containing the event name 
        // and the text block to display the event to.
        Dictionary<string, TextBlock> eventBlocks = new Dictionary<string, TextBlock>();
        // A collection of key/value pairs containing the event name  
        // and the number of times the event fired.
        Dictionary<string, int> eventCount = new Dictionary<string, int>();

        /// <summary>
        ///  드래그 할 푸시핀을 저장한다. 
        /// </summary>
        public Pushpin SelectedPushpin { get; set; }

        /// <summary>
        /// 지금 푸시핀 드래그 중이냐?
        /// </summary>
        private bool _dragPin;

        public PageSetting()
        {
            InitializeComponent();
        }

        private void MapWithEvents_ViewChangeOnFrame(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_TargetViewChanged(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_ViewChangeStart(object sender, MapEventArgs e)
        {

        }

        private void MapWithEvents_ModeChanged(object sender, MapEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGetParamList_Click(object sender, RoutedEventArgs e)
        {

            MainV2.comPort.getParamList();
        }
    }
}
