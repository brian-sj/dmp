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
using MissionPlanner.GCSViews.ConfigurationView;

namespace MissionPlanner.GCSViews.ConfigurationView
{
    /// <summary>
    /// ConfigFrameType.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfigFrameType : UserControl
    {
        public ConfigFrameType()
        {
            InitializeComponent();
        }

        public enum Frame 
        {
            

            Plus = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_PLUS,
            X = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_X,
            V = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_V,
            H = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_H,
            VTail = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_VTAIL,
            Y = ConfigFrameClassType.motor_frame_type.MOTOR_FRAME_TYPE_Y6B
        }
    }
}
