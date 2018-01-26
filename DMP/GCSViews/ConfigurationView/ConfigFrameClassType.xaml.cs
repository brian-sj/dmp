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

namespace MissionPlanner.GCSViews.ConfigurationView
{
    /// <summary>
    /// ConfigFrameClassType.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfigFrameClassType : UserControl
    {

        private motor_frame_class work_frame_class;
        private motor_frame_type work_frame_type;

        // from https://github.com/ArduPilot/ardupilot/blob/master/libraries/AP_Motors/AP_Motors_Class.h
        public enum motor_frame_class
        {
            MOTOR_FRAME_UNDEFINED = 0,
            MOTOR_FRAME_QUAD = 1,
            MOTOR_FRAME_HEXA = 2,
            MOTOR_FRAME_OCTA = 3,
            MOTOR_FRAME_OCTAQUAD = 4,
            MOTOR_FRAME_Y6 = 5,
            MOTOR_FRAME_HELI = 6,
            MOTOR_FRAME_TRI = 7,
            MOTOR_FRAME_SINGLE = 8,
            MOTOR_FRAME_COAX = 9
        };

        public enum motor_frame_type
        {
            MOTOR_FRAME_TYPE_PLUS = 0,
            MOTOR_FRAME_TYPE_X = 1,
            MOTOR_FRAME_TYPE_V = 2,
            MOTOR_FRAME_TYPE_H = 3,
            MOTOR_FRAME_TYPE_VTAIL = 4,
            MOTOR_FRAME_TYPE_ATAIL = 5,
            MOTOR_FRAME_TYPE_Y6B = 10

        };

        // list of valid options enterd from https://github.com/ArduPilot/ardupilot/blob/master/libraries/AP_Motors/AP_MotorsMatrix.cpp#L378
        public List<Tuple<motor_frame_class, motor_frame_type?>> ValidList =
            new List<Tuple<motor_frame_class, motor_frame_type?>>()
            {
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_PLUS),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_X),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_V),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_H),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_VTAIL),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_QUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_ATAIL),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_HEXA,
                    motor_frame_type.MOTOR_FRAME_TYPE_PLUS),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_HEXA,
                    motor_frame_type.MOTOR_FRAME_TYPE_X),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTA,
                    motor_frame_type.MOTOR_FRAME_TYPE_PLUS),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTA,
                    motor_frame_type.MOTOR_FRAME_TYPE_X),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTA,
                    motor_frame_type.MOTOR_FRAME_TYPE_V),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTAQUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_PLUS),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTAQUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_X),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTAQUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_V),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_OCTAQUAD,
                    motor_frame_type.MOTOR_FRAME_TYPE_H),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_Y6,
                    motor_frame_type.MOTOR_FRAME_TYPE_Y6B),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_Y6,
                    motor_frame_type.MOTOR_FRAME_TYPE_X),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_HELI,
                    null),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_TRI,
                    null),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_SINGLE,
                    null),
                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_COAX,
                    null),

                new Tuple<motor_frame_class, motor_frame_type?>(motor_frame_class.MOTOR_FRAME_UNDEFINED,
                    null),
            };
        public ConfigFrameClassType()
        {
            InitializeComponent();
        }
    }
}
