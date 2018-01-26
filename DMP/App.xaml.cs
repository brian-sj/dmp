using DMP.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DMP
{
#region ENUM 
    enum PointType : int
    {
        [Description("WAYPOINT")]
        WAYPOINT = 0,
        [Description("TARGET")]
        TARGET = 1,
        [Description("HOME")]
        HOME = 2,
        [Description("RELAY")]
        RELAY = 3
    }
    enum LandingType : int {
        [Description("RTH")]
        RTH = 0,
        [Description("HOVER")]
        HOVER = 1,
        [Description("LANDING")]
        LANDING = 2
    }
    enum DoWhenError : int
    {
        [Description("RTH")]
        RTH = 0,
        [Description("HOVER")]
        HOVER = 1,
        [Description("LANDING")]
        LANDING = 2
    }
    enum CornerType : int {
        [Description("STRAIGHT")]
        STRAIGHT = 0,
        [Description("SMALL")]
        SMALL = 1,
        [Description("MEDIUM")]
        MEDIUM = 2,
        [Description("LARGE")]
        LARGE = 3
    }
    enum Heading : int {
        [Description("WAYPOINT")]
        WAYPOINT = 0,
        [Description("RC")]
        RC = 1
    }

    enum InitialHeightType : int
    {
        [Description("STANDART")]
        WAYPOINT = 0,
        [Description("RTH")]
        RC = 1,
        [Description("First Point")]
        FIRST_POINT = 2
    }
    enum ONOFF : int
    {
        [Description("ON")]
        WAYPOINT = 1,
        [Description("OFF")]
        RC = 0,
    }

    /// <summary>
    /// 나중에 추가 해야한다.... 
    /// </summary>
    enum ACTIONTYPE : int
    {
        [Description("NOTHING")]
        NOTHING = 0,
        [Description("DO_JUMP")]
        DO_JUMP = 177,
    }


    public enum MAVLINK_MSG_ID
    {
        HEARTBEAT = 0,
        SYS_STATUS = 1,
        SYSTEM_TIME = 2,
        PING = 4,
        CHANGE_OPERATOR_CONTROL = 5,
        CHANGE_OPERATOR_CONTROL_ACK = 6,
        AUTH_KEY = 7,
        SET_MODE = 11,
        PARAM_REQUEST_READ = 20,
        PARAM_REQUEST_LIST = 21,
        PARAM_VALUE = 22,
        PARAM_SET = 23,
        GPS_RAW_INT = 24,
        GPS_STATUS = 25,
        SCALED_IMU = 26,
        RAW_IMU = 27,
        RAW_PRESSURE = 28,
        SCALED_PRESSURE = 29,
        ATTITUDE = 30,
        ATTITUDE_QUATERNION = 31,
        LOCAL_POSITION_NED = 32,
        GLOBAL_POSITION_INT = 33,
        RC_CHANNELS_SCALED = 34,
        RC_CHANNELS_RAW = 35,
        SERVO_OUTPUT_RAW = 36,
        MISSION_REQUEST_PARTIAL_LIST = 37,
        MISSION_WRITE_PARTIAL_LIST = 38,
        MISSION_ITEM = 39,
        MISSION_REQUEST = 40,
        MISSION_SET_CURRENT = 41,
        MISSION_CURRENT = 42,
        MISSION_REQUEST_LIST = 43,
        MISSION_COUNT = 44,
        MISSION_CLEAR_ALL = 45,
        MISSION_ITEM_REACHED = 46,
        MISSION_ACK = 47,
        SET_GPS_GLOBAL_ORIGIN = 48,
        GPS_GLOBAL_ORIGIN = 49,
        PARAM_MAP_RC = 50,
        MISSION_REQUEST_INT = 51,
        SAFETY_SET_ALLOWED_AREA = 54,
        SAFETY_ALLOWED_AREA = 55,
        ATTITUDE_QUATERNION_COV = 61,
        NAV_CONTROLLER_OUTPUT = 62,
        GLOBAL_POSITION_INT_COV = 63,
        LOCAL_POSITION_NED_COV = 64,
        RC_CHANNELS = 65,
        REQUEST_DATA_STREAM = 66,
        DATA_STREAM = 67,
        MANUAL_CONTROL = 69,
        RC_CHANNELS_OVERRIDE = 70,
        MISSION_ITEM_INT = 73,
        VFR_HUD = 74,
        COMMAND_INT = 75,
        COMMAND_LONG = 76,
        COMMAND_ACK = 77,
        MANUAL_SETPOINT = 81,
        SET_ATTITUDE_TARGET = 82,
        ATTITUDE_TARGET = 83,
        SET_POSITION_TARGET_LOCAL_NED = 84,
        POSITION_TARGET_LOCAL_NED = 85,
        SET_POSITION_TARGET_GLOBAL_INT = 86,
        POSITION_TARGET_GLOBAL_INT = 87,
        LOCAL_POSITION_NED_SYSTEM_GLOBAL_OFFSET = 89,
        HIL_STATE = 90,
        HIL_CONTROLS = 91,
        HIL_RC_INPUTS_RAW = 92,
        HIL_ACTUATOR_CONTROLS = 93,
        OPTICAL_FLOW = 100,
        GLOBAL_VISION_POSITION_ESTIMATE = 101,
        VISION_POSITION_ESTIMATE = 102,
        VISION_SPEED_ESTIMATE = 103,
        VICON_POSITION_ESTIMATE = 104,
        HIGHRES_IMU = 105,
        OPTICAL_FLOW_RAD = 106,
        HIL_SENSOR = 107,
        SIM_STATE = 108,
        RADIO_STATUS = 109,
        FILE_TRANSFER_PROTOCOL = 110,
        TIMESYNC = 111,
        CAMERA_TRIGGER = 112,
        HIL_GPS = 113,
        HIL_OPTICAL_FLOW = 114,
        HIL_STATE_QUATERNION = 115,
        SCALED_IMU2 = 116,
        LOG_REQUEST_LIST = 117,
        LOG_ENTRY = 118,
        LOG_REQUEST_DATA = 119,
        LOG_DATA = 120,
        LOG_ERASE = 121,
        LOG_REQUEST_END = 122,
        GPS_INJECT_DATA = 123,
        GPS2_RAW = 124,
        POWER_STATUS = 125,
        SERIAL_CONTROL = 126,
        GPS_RTK = 127,
        GPS2_RTK = 128,
        SCALED_IMU3 = 129,
        DATA_TRANSMISSION_HANDSHAKE = 130,
        ENCAPSULATED_DATA = 131,
        DISTANCE_SENSOR = 132,
        TERRAIN_REQUEST = 133,
        TERRAIN_DATA = 134,
        TERRAIN_CHECK = 135,
        TERRAIN_REPORT = 136,
        SCALED_PRESSURE2 = 137,
        ATT_POS_MOCAP = 138,
        SET_ACTUATOR_CONTROL_TARGET = 139,
        ACTUATOR_CONTROL_TARGET = 140,
        ALTITUDE = 141,
        RESOURCE_REQUEST = 142,
        SCALED_PRESSURE3 = 143,
        FOLLOW_TARGET = 144,
        CONTROL_SYSTEM_STATE = 146,
        BATTERY_STATUS = 147,
        AUTOPILOT_VERSION = 148,
        LANDING_TARGET = 149,
        SENSOR_OFFSETS = 150,
        SET_MAG_OFFSETS = 151,
        MEMINFO = 152,
        AP_ADC = 153,
        DIGICAM_CONFIGURE = 154,
        DIGICAM_CONTROL = 155,
        MOUNT_CONFIGURE = 156,
        MOUNT_CONTROL = 157,
        MOUNT_STATUS = 158,
        FENCE_POINT = 160,
        FENCE_FETCH_POINT = 161,
        FENCE_STATUS = 162,
        AHRS = 163,
        SIMSTATE = 164,
        HWSTATUS = 165,
        RADIO = 166,
        LIMITS_STATUS = 167,
        WIND = 168,
        DATA16 = 169,
        DATA32 = 170,
        DATA64 = 171,
        DATA96 = 172,
        RANGEFINDER = 173,
        AIRSPEED_AUTOCAL = 174,
        RALLY_POINT = 175,
        RALLY_FETCH_POINT = 176,
        COMPASSMOT_STATUS = 177,
        AHRS2 = 178,
        CAMERA_STATUS = 179,
        CAMERA_FEEDBACK = 180,
        BATTERY2 = 181,
        AHRS3 = 182,
        AUTOPILOT_VERSION_REQUEST = 183,
        REMOTE_LOG_DATA_BLOCK = 184,
        REMOTE_LOG_BLOCK_STATUS = 185,
        LED_CONTROL = 186,
        MAG_CAL_PROGRESS = 191,
        MAG_CAL_REPORT = 192,
        EKF_STATUS_REPORT = 193,
        PID_TUNING = 194,
        DEEPSTALL = 195,
        GIMBAL_REPORT = 200,
        GIMBAL_CONTROL = 201,
        GIMBAL_TORQUE_CMD_REPORT = 214,
        GOPRO_HEARTBEAT = 215,
        GOPRO_GET_REQUEST = 216,
        GOPRO_GET_RESPONSE = 217,
        GOPRO_SET_REQUEST = 218,
        GOPRO_SET_RESPONSE = 219,
        RPM = 226,
        ESTIMATOR_STATUS = 230,
        WIND_COV = 231,
        GPS_INPUT = 232,
        GPS_RTCM_DATA = 233,
        HIGH_LATENCY = 234,
        VIBRATION = 241,
        HOME_POSITION = 242,
        SET_HOME_POSITION = 243,
        MESSAGE_INTERVAL = 244,
        EXTENDED_SYS_STATE = 245,
        ADSB_VEHICLE = 246,
        COLLISION = 247,
        V2_EXTENSION = 248,
        MEMORY_VECT = 249,
        DEBUG_VECT = 250,
        NAMED_VALUE_FLOAT = 251,
        NAMED_VALUE_INT = 252,
        STATUSTEXT = 253,
        DEBUG = 254,
        SETUP_SIGNING = 256,
        BUTTON_CHANGE = 257,
        PLAY_TUNE = 258,
        CAMERA_INFORMATION = 259,
        CAMERA_SETTINGS = 260,
        STORAGE_INFORMATION = 261,
        CAMERA_CAPTURE_STATUS = 262,
        CAMERA_IMAGE_CAPTURED = 263,
        FLIGHT_INFORMATION = 264,
        MOUNT_ORIENTATION = 265,
        LOGGING_DATA = 266,
        LOGGING_DATA_ACKED = 267,
        LOGGING_ACK = 268,
        UAVIONIX_ADSB_OUT_CFG = 10001,
        UAVIONIX_ADSB_OUT_DYNAMIC = 10002,
        UAVIONIX_ADSB_TRANSCEIVER_HEALTH_REPORT = 10003,
        DEVICE_OP_READ = 11000,
        DEVICE_OP_READ_REPLY = 11001,
        DEVICE_OP_WRITE = 11002,
        DEVICE_OP_WRITE_REPLY = 11003,
        ADAP_TUNING = 11010,
        VISION_POSITION_DELTA = 11011,
        AOA_SSA = 11020,

    }


    ///<summary>  </summary>
    public enum ACCELCAL_VEHICLE_POS : int /*default*/
    {
        ///<summary>  | </summary>
        LEVEL = 1,
        ///<summary>  | </summary>
        LEFT = 2,
        ///<summary>  | </summary>
        RIGHT = 3,
        ///<summary>  | </summary>
        NOSEDOWN = 4,
        ///<summary>  | </summary>
        NOSEUP = 5,
        ///<summary>  | </summary>
        BACK = 6,
        ///<summary>  | </summary>
        SUCCESS = 16777215,
        ///<summary>  | </summary>
        FAILED = 16777216,

    };

    ///<summary> Commands to be executed by the MAV. They can be executed on user request, or as part of a mission script. If the action is used in a mission, the parameter mapping to the waypoint/mission message is as follows: Param 1, Param 2, Param 3, Param 4, X: Param 5, Y:Param 6, Z:Param 7. This command list is similar what ARINC 424 is for commercial aircraft: A data format how to interpret waypoint/mission data. </summary>
    public enum MAV_CMD : ushort
    {
        ///<summary> Navigate to MISSION. |Hold time in decimal seconds. (ignored by fixed wing, time to stay at MISSION for rotary wing)| Acceptance radius in meters (if the sphere with this radius is hit, the MISSION counts as reached)| 0 to pass through the WP, if > 0 radius in meters to pass by WP. Positive value for clockwise orbit, negative value for counter-clockwise orbit. Allows trajectory control.| Desired yaw angle at MISSION (rotary wing). NaN for unchanged.| Latitude| Longitude| Altitude|  </summary>
        WAYPOINT = 16,
        ///<summary> Loiter around this MISSION an unlimited amount of time |Empty| Empty| Radius around MISSION, in meters. If positive loiter clockwise, else counter-clockwise| Desired yaw angle.| Latitude| Longitude| Altitude|  </summary>
        LOITER_UNLIM = 17,
        ///<summary> Loiter around this MISSION for X turns |Turns| Empty| Radius around MISSION, in meters. If positive loiter clockwise, else counter-clockwise| Forward moving aircraft this sets exit xtrack location: 0 for center of loiter wp, 1 for exit location. Else, this is desired yaw angle| Latitude| Longitude| Altitude|  </summary>
        LOITER_TURNS = 18,
        ///<summary> Loiter around this MISSION for X seconds |Seconds (decimal)| Empty| Radius around MISSION, in meters. If positive loiter clockwise, else counter-clockwise| Forward moving aircraft this sets exit xtrack location: 0 for center of loiter wp, 1 for exit location. Else, this is desired yaw angle| Latitude| Longitude| Altitude|  </summary>
        LOITER_TIME = 19,
        ///<summary> Return to launch location |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        RETURN_TO_LAUNCH = 20,
        ///<summary> Land at location |Abort Alt| Empty| Empty| Desired yaw angle. NaN for unchanged.| Latitude| Longitude| Altitude|  </summary>
        LAND = 21,
        ///<summary> Takeoff from ground / hand |Minimum pitch (if airspeed sensor present), desired pitch without sensor| Empty| Empty| Yaw angle (if magnetometer present), ignored without magnetometer. NaN for unchanged.| Latitude| Longitude| Altitude|  </summary>
        TAKEOFF = 22,
        ///<summary> Land at local position (local frame only) |Landing target number (if available)| Maximum accepted offset from desired landing position [m] - computed magnitude from spherical coordinates: d = sqrt(x^2 + y^2 + z^2), which gives the maximum accepted distance between the desired landing position and the position where the vehicle is about to land| Landing descend rate [ms^-1]| Desired yaw angle [rad]| Y-axis position [m]| X-axis position [m]| Z-axis / ground level position [m]|  </summary>
        LAND_LOCAL = 23,
        ///<summary> Takeoff from local position (local frame only) |Minimum pitch (if airspeed sensor present), desired pitch without sensor [rad]| Empty| Takeoff ascend rate [ms^-1]| Yaw angle [rad] (if magnetometer or another yaw estimation source present), ignored without one of these| Y-axis position [m]| X-axis position [m]| Z-axis position [m]|  </summary>
        TAKEOFF_LOCAL = 24,
        ///<summary> Vehicle following, i.e. this waypoint represents the position of a moving vehicle |Following logic to use (e.g. loitering or sinusoidal following) - depends on specific autopilot implementation| Ground speed of vehicle to be followed| Radius around MISSION, in meters. If positive loiter clockwise, else counter-clockwise| Desired yaw angle.| Latitude| Longitude| Altitude|  </summary>
        FOLLOW = 25,
        ///<summary> Continue on the current course and climb/descend to specified altitude.  When the altitude is reached continue to the next command (i.e., don't proceed to the next command until the desired altitude is reached. |Climb or Descend (0 = Neutral, command completes when within 5m of this command's altitude, 1 = Climbing, command completes when at or above this command's altitude, 2 = Descending, command completes when at or below this command's altitude. | Empty| Empty| Empty| Empty| Empty| Desired altitude in meters|  </summary>
        CONTINUE_AND_CHANGE_ALT = 30,
        ///<summary> Begin loiter at the specified Latitude and Longitude.  If Lat=Lon=0, then loiter at the current position.  Don't consider the navigation command complete (don't leave loiter) until the altitude has been reached.  Additionally, if the Heading Required parameter is non-zero the  aircraft will not leave the loiter until heading toward the next waypoint.  |Heading Required (0 = False)| Radius in meters. If positive loiter clockwise, negative counter-clockwise, 0 means no change to standard loiter.| Empty| Forward moving aircraft this sets exit xtrack location: 0 for center of loiter wp, 1 for exit location| Latitude| Longitude| Altitude|  </summary>
        LOITER_TO_ALT = 31,
        ///<summary> Being following a target |System ID (the system ID of the FOLLOW_TARGET beacon). Send 0 to disable follow-me and return to the default position hold mode| RESERVED| RESERVED| altitude flag: 0: Keep current altitude, 1: keep altitude difference to target, 2: go to a fixed altitude above home| altitude| RESERVED| TTL in seconds in which the MAV should go to the default position hold mode after a message rx timeout|  </summary>
        DO_FOLLOW = 32,
        ///<summary> Reposition the MAV after a follow target command has been sent |Camera q1 (where 0 is on the ray from the camera to the tracking device)| Camera q2| Camera q3| Camera q4| altitude offset from target (m)| X offset from target (m)| Y offset from target (m)|  </summary>
        DO_FOLLOW_REPOSITION = 33,
        ///<summary> Sets the region of interest (ROI) for a sensor set or the vehicle itself. This can then be used by the vehicles control system to control the vehicle attitude and the attitude of various sensors such as cameras. |Region of intereset mode. (see MAV_ROI enum)| MISSION index/ target ID. (see MAV_ROI enum)| ROI index (allows a vehicle to manage multiple ROI's)| Empty| x the location of the fixed ROI (see MAV_FRAME)| y| z|  </summary>
        ROI = 80,
        ///<summary> Control autonomous path planning on the MAV. |0: Disable local obstacle avoidance / local path planning (without resetting map), 1: Enable local path planning, 2: Enable and reset local path planning| 0: Disable full path planning (without resetting map), 1: Enable, 2: Enable and reset map/occupancy grid, 3: Enable and reset planned route, but not occupancy grid| Empty| Yaw angle at goal, in compass degrees, [0..360]| Latitude/X of goal| Longitude/Y of goal| Altitude/Z of goal|  </summary>
        PATHPLANNING = 81,
        ///<summary> Navigate to MISSION using a spline path. |Hold time in decimal seconds. (ignored by fixed wing, time to stay at MISSION for rotary wing)| Empty| Empty| Empty| Latitude/X of goal| Longitude/Y of goal| Altitude/Z of goal|  </summary>
        SPLINE_WAYPOINT = 82,
        ///<summary> Mission command to wait for an altitude or downwards vertical speed. This is meant for high altitude balloon launches, allowing the aircraft to be idle until either an altitude is reached or a negative vertical speed is reached (indicating early balloon burst). The wiggle time is how often to wiggle the control surfaces to prevent them seizing up. |altitude (m)| descent speed (m/s)| Wiggle Time (s)| Empty| Empty| Empty| Empty|  </summary>
        ALTITUDE_WAIT = 83,
        ///<summary> Takeoff from ground using VTOL mode |Empty| Empty| Empty| Yaw angle in degrees. NaN for unchanged.| Latitude| Longitude| Altitude|  </summary>
        VTOL_TAKEOFF = 84,
        ///<summary> Land using VTOL mode |Empty| Empty| Empty| Yaw angle in degrees. NaN for unchanged.| Latitude| Longitude| Altitude|  </summary>
        VTOL_LAND = 85,
        ///<summary> hand control over to an external controller |On / Off (> 0.5f on)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        GUIDED_ENABLE = 92,
        ///<summary> Delay the next navigation command a number of seconds or until a specified time |Delay in seconds (decimal, -1 to enable time-of-day fields)| hour (24h format, UTC, -1 to ignore)| minute (24h format, UTC, -1 to ignore)| second (24h format, UTC)| Empty| Empty| Empty|  </summary>
        DELAY = 93,
        ///<summary> Descend and place payload.  Vehicle descends until it detects a hanging payload has reached the ground, the gripper is opened to release the payload |Maximum distance to descend (meters)| Empty| Empty| Empty| Latitude (deg * 1E7)| Longitude (deg * 1E7)| Altitude (meters)|  </summary>
        PAYLOAD_PLACE = 94,
        ///<summary> NOP - This command is only used to mark the upper limit of the NAV/ACTION commands in the enumeration |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        LAST = 95,
        ///<summary> Delay mission state machine. |Delay in seconds (decimal)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        CONDITION_DELAY = 112,
        ///<summary> Ascend/descend at rate.  Delay mission state machine until desired altitude reached. |Descent / Ascend rate (m/s)| Empty| Empty| Empty| Empty| Empty| Finish Altitude|  </summary>
        CONDITION_CHANGE_ALT = 113,
        ///<summary> Delay mission state machine until within desired distance of next NAV point. |Distance (meters)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        CONDITION_DISTANCE = 114,
        ///<summary> Reach a certain target angle. |target angle: [0-360], 0 is north| speed during yaw change:[deg per second]| direction: negative: counter clockwise, positive: clockwise [-1,1]| relative offset or absolute angle: [ 1,0]| Empty| Empty| Empty|  </summary>
        CONDITION_YAW = 115,
        ///<summary> NOP - This command is only used to mark the upper limit of the CONDITION commands in the enumeration |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        CONDITION_LAST = 159,
        ///<summary> Set system mode. |Mode, as defined by ENUM MAV_MODE| Custom mode - this is system specific, please refer to the individual autopilot specifications for details.| Custom sub mode - this is system specific, please refer to the individual autopilot specifications for details.| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_MODE = 176,
        ///<summary> Jump to the desired command in the mission list.  Repeat this action only the specified number of times |Sequence number| Repeat count| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_JUMP = 177,
        ///<summary> Change speed and/or throttle set points. |Speed type (0=Airspeed, 1=Ground Speed)| Speed  (m/s, -1 indicates no change)| Throttle  ( Percent, -1 indicates no change)| absolute or relative [0,1]| Empty| Empty| Empty|  </summary>
        DO_CHANGE_SPEED = 178,
        ///<summary> Changes the home location either to the current location or a specified location. |Use current (1=use current location, 0=use specified location)| Empty| Empty| Empty| Latitude| Longitude| Altitude|  </summary>
        DO_SET_HOME = 179,
        ///<summary> Set a system parameter.  Caution!  Use of this command requires knowledge of the numeric enumeration value of the parameter. |Parameter number| Parameter value| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_PARAMETER = 180,
        ///<summary> Set a relay to a condition. |Relay number| Setting (1=on, 0=off, others possible depending on system hardware)| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_RELAY = 181,
        ///<summary> Cycle a relay on and off for a desired number of cyles with a desired period. |Relay number| Cycle count| Cycle time (seconds, decimal)| Empty| Empty| Empty| Empty|  </summary>
        DO_REPEAT_RELAY = 182,
        ///<summary> Set a servo to a desired PWM value. |Servo number| PWM (microseconds, 1000 to 2000 typical)| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_SERVO = 183,
        ///<summary> Cycle a between its nominal setting and a desired PWM for a desired number of cycles with a desired period. |Servo number| PWM (microseconds, 1000 to 2000 typical)| Cycle count| Cycle time (seconds)| Empty| Empty| Empty|  </summary>
        DO_REPEAT_SERVO = 184,
        ///<summary> Terminate flight immediately |Flight termination activated if > 0.5| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_FLIGHTTERMINATION = 185,
        ///<summary> Change altitude set point. |Altitude in meters| Mav frame of new altitude (see MAV_FRAME)| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_CHANGE_ALTITUDE = 186,
        ///<summary> Mission command to perform a landing. This is used as a marker in a mission to tell the autopilot where a sequence of mission items that represents a landing starts. It may also be sent via a COMMAND_LONG to trigger a landing, in which case the nearest (geographically) landing sequence in the mission will be used. The Latitude/Longitude is optional, and may be set to 0/0 if not needed. If specified then it will be used to help find the closest landing sequence. |Empty| Empty| Empty| Empty| Latitude| Longitude| Empty|  </summary>
        DO_LAND_START = 189,
        ///<summary> Mission command to perform a landing from a rally point. |Break altitude (meters)| Landing speed (m/s)| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_RALLY_LAND = 190,
        ///<summary> Mission command to safely abort an autonmous landing. |Altitude (meters)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_GO_AROUND = 191,
        ///<summary> Reposition the vehicle to a specific WGS84 global position. |Ground speed, less than 0 (-1) for default| Bitmask of option flags, see the MAV_DO_REPOSITION_FLAGS enum.| Reserved| Yaw heading, NaN for unchanged. For planes indicates loiter direction (0: clockwise, 1: counter clockwise)| Latitude (deg * 1E7)| Longitude (deg * 1E7)| Altitude (meters)|  </summary>
        DO_REPOSITION = 192,
        ///<summary> If in a GPS controlled position mode, hold the current position or continue. |0: Pause current mission or reposition command, hold current position. 1: Continue mission. A VTOL capable vehicle should enter hover mode (multicopter and VTOL planes). A plane should loiter with the default loiter radius.| Reserved| Reserved| Reserved| Reserved| Reserved| Reserved|  </summary>
        DO_PAUSE_CONTINUE = 193,
        ///<summary> Set moving direction to forward or reverse. |Direction (0=Forward, 1=Reverse)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_REVERSE = 194,
        ///<summary> Control onboard camera system. |Camera ID (-1 for all)| Transmission: 0: disabled, 1: enabled compressed, 2: enabled raw| Transmission mode: 0: video stream, >0: single images every n seconds (decimal)| Recording: 0: disabled, 1: enabled compressed, 2: enabled raw| Empty| Empty| Empty|  </summary>
        DO_CONTROL_VIDEO = 200,
        ///<summary> Sets the region of interest (ROI) for a sensor set or the vehicle itself. This can then be used by the vehicles control system to control the vehicle attitude and the attitude of various sensors such as cameras. |Region of intereset mode. (see MAV_ROI enum)| MISSION index/ target ID. (see MAV_ROI enum)| ROI index (allows a vehicle to manage multiple ROI's)| Empty| x the location of the fixed ROI (see MAV_FRAME)| y| z|  </summary>
        DO_SET_ROI = 201,
        ///<summary> Mission command to configure an on-board camera controller system. |Modes: P, TV, AV, M, Etc| Shutter speed: Divisor number for one second| Aperture: F stop number| ISO number e.g. 80, 100, 200, Etc| Exposure type enumerator| Command Identity| Main engine cut-off time before camera trigger in seconds/10 (0 means no cut-off)|  </summary>
        DO_DIGICAM_CONFIGURE = 202,
        ///<summary> Mission command to control an on-board camera controller system. |Session control e.g. show/hide lens| Zoom's absolute position| Zooming step value to offset zoom from the current position| Focus Locking, Unlocking or Re-locking| Shooting Command| Command Identity| Empty|  </summary>
        DO_DIGICAM_CONTROL = 203,
        ///<summary> Mission command to configure a camera or antenna mount |Mount operation mode (see MAV_MOUNT_MODE enum)| stabilize roll? (1 = yes, 0 = no)| stabilize pitch? (1 = yes, 0 = no)| stabilize yaw? (1 = yes, 0 = no)| Empty| Empty| Empty|  </summary>
        DO_MOUNT_CONFIGURE = 204,
        ///<summary> Mission command to control a camera or antenna mount |pitch (WIP: DEPRECATED: or lat in degrees) depending on mount mode.| roll (WIP: DEPRECATED: or lon in degrees) depending on mount mode.| yaw (WIP: DEPRECATED: or alt in meters) depending on mount mode.| WIP: alt in meters depending on mount mode.| WIP: latitude in degrees * 1E7, set if appropriate mount mode.| WIP: longitude in degrees * 1E7, set if appropriate mount mode.| MAV_MOUNT_MODE enum value|  </summary>
        DO_MOUNT_CONTROL = 205,
        ///<summary> Mission command to set CAM_TRIGG_DIST for this flight |Camera trigger distance (meters)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SET_CAM_TRIGG_DIST = 206,
        ///<summary> Mission command to enable the geofence |enable? (0=disable, 1=enable, 2=disable_floor_only)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_FENCE_ENABLE = 207,
        ///<summary> Mission command to trigger a parachute |action (0=disable, 1=enable, 2=release, for some systems see PARACHUTE_ACTION enum, not in general message set.)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_PARACHUTE = 208,
        ///<summary> Mission command to perform motor test |motor number (a number from 1 to max number of motors on the vehicle)| throttle type (0=throttle percentage, 1=PWM, 2=pilot throttle channel pass-through. See MOTOR_TEST_THROTTLE_TYPE enum)| throttle| timeout (in seconds)| motor count (number of motors to test to test in sequence, waiting for the timeout above between them; 0=1 motor, 1=1 motor, 2=2 motors...)| motor test order (See MOTOR_TEST_ORDER enum)| Empty|  </summary>
        DO_MOTOR_TEST = 209,
        ///<summary> Change to/from inverted flight |inverted (0=normal, 1=inverted)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_INVERTED_FLIGHT = 210,
        ///<summary> Mission command to operate EPM gripper |gripper number (a number from 1 to max number of grippers on the vehicle)| gripper action (0=release, 1=grab. See GRIPPER_ACTIONS enum)| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_GRIPPER = 211,
        ///<summary> Enable/disable autotune |enable (1: enable, 0:disable)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_AUTOTUNE_ENABLE = 212,
        ///<summary> Sets a desired vehicle turn angle and speed change |yaw angle to adjust steering by in centidegress| speed - normalized to 0 .. 1| Empty| Empty| Empty| Empty| Empty|  </summary>
        SET_YAW_SPEED = 213,
        ///<summary> Mission command to control a camera or antenna mount, using a quaternion as reference. |q1 - quaternion param #1, w (1 in null-rotation)| q2 - quaternion param #2, x (0 in null-rotation)| q3 - quaternion param #3, y (0 in null-rotation)| q4 - quaternion param #4, z (0 in null-rotation)| Empty| Empty| Empty|  </summary>
        DO_MOUNT_CONTROL_QUAT = 220,
        ///<summary> set id of master controller |System ID| Component ID| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_GUIDED_MASTER = 221,
        ///<summary> set limits for external control |timeout - maximum time (in seconds) that external controller will be allowed to control vehicle. 0 means no timeout| absolute altitude min (in meters, AMSL) - if vehicle moves below this alt, the command will be aborted and the mission will continue.  0 means no lower altitude limit| absolute altitude max (in meters)- if vehicle moves above this alt, the command will be aborted and the mission will continue.  0 means no upper altitude limit| horizontal move limit (in meters, AMSL) - if vehicle moves more than this distance from it's location at the moment the command was executed, the command will be aborted and the mission will continue. 0 means no horizontal altitude limit| Empty| Empty| Empty|  </summary>
        DO_GUIDED_LIMITS = 222,
        ///<summary> Control vehicle engine. This is interpreted by the vehicles engine controller to change the target engine state. It is intended for vehicles with internal combustion engines |0: Stop engine, 1:Start Engine| 0: Warm start, 1:Cold start. Controls use of choke where applicable| Height delay (meters). This is for commanding engine start only after the vehicle has gained the specified height. Used in VTOL vehicles during takeoff to start engine after the aircraft is off the ground. Zero for no delay.| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_ENGINE_CONTROL = 223,
        ///<summary> NOP - This command is only used to mark the upper limit of the DO commands in the enumeration |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_LAST = 240,
        ///<summary> Trigger calibration. This command will be only accepted if in pre-flight mode. Except for Temperature Calibration, only one sensor should be set in a single message and all others should be zero. |1: gyro calibration, 3: gyro temperature calibration| 1: magnetometer calibration| 1: ground pressure calibration| 1: radio RC calibration, 2: RC trim calibration| 1: accelerometer calibration, 2: board level calibration, 3: accelerometer temperature calibration, 4: simple accelerometer calibration| 1: APM: compass/motor interference calibration (PX4: airspeed calibration, deprecated), 2: airspeed calibration| 1: ESC calibration, 3: barometer temperature calibration|  </summary>
        PREFLIGHT_CALIBRATION = 241,
        ///<summary> Set sensor offsets. This command will be only accepted if in pre-flight mode. |Sensor to adjust the offsets for: 0: gyros, 1: accelerometer, 2: magnetometer, 3: barometer, 4: optical flow, 5: second magnetometer, 6: third magnetometer| X axis offset (or generic dimension 1), in the sensor's raw units| Y axis offset (or generic dimension 2), in the sensor's raw units| Z axis offset (or generic dimension 3), in the sensor's raw units| Generic dimension 4, in the sensor's raw units| Generic dimension 5, in the sensor's raw units| Generic dimension 6, in the sensor's raw units|  </summary>
        PREFLIGHT_SET_SENSOR_OFFSETS = 242,
        ///<summary> Trigger UAVCAN config. This command will be only accepted if in pre-flight mode. |1: Trigger actuator ID assignment and direction mapping.| Reserved| Reserved| Reserved| Reserved| Reserved| Reserved|  </summary>
        PREFLIGHT_UAVCAN = 243,
        ///<summary> Request storage of different parameter values and logs. This command will be only accepted if in pre-flight mode. |Parameter storage: 0: READ FROM FLASH/EEPROM, 1: WRITE CURRENT TO FLASH/EEPROM, 2: Reset to defaults| Mission storage: 0: READ FROM FLASH/EEPROM, 1: WRITE CURRENT TO FLASH/EEPROM, 2: Reset to defaults| Onboard logging: 0: Ignore, 1: Start default rate logging, -1: Stop logging, > 1: start logging with rate of param 3 in Hz (e.g. set to 1000 for 1000 Hz logging)| Reserved| Empty| Empty| Empty|  </summary>
        PREFLIGHT_STORAGE = 245,
        ///<summary> Request the reboot or shutdown of system components. |0: Do nothing for autopilot, 1: Reboot autopilot, 2: Shutdown autopilot, 3: Reboot autopilot and keep it in the bootloader until upgraded.| 0: Do nothing for onboard computer, 1: Reboot onboard computer, 2: Shutdown onboard computer, 3: Reboot onboard computer and keep it in the bootloader until upgraded.| WIP: 0: Do nothing for camera, 1: Reboot onboard camera, 2: Shutdown onboard camera, 3: Reboot onboard camera and keep it in the bootloader until upgraded| WIP: 0: Do nothing for mount (e.g. gimbal), 1: Reboot mount, 2: Shutdown mount, 3: Reboot mount and keep it in the bootloader until upgraded| Reserved, send 0| Reserved, send 0| WIP: ID (e.g. camera ID -1 for all IDs)|  </summary>
        PREFLIGHT_REBOOT_SHUTDOWN = 246,
        ///<summary> Hold / continue the current action |MAV_GOTO_DO_HOLD: hold MAV_GOTO_DO_CONTINUE: continue with next item in mission plan| MAV_GOTO_HOLD_AT_CURRENT_POSITION: Hold at current position MAV_GOTO_HOLD_AT_SPECIFIED_POSITION: hold at specified position| MAV_FRAME coordinate frame of hold point| Desired yaw angle in degrees| Latitude / X position| Longitude / Y position| Altitude / Z position|  </summary>
        OVERRIDE_GOTO = 252,
        ///<summary> start running a mission |first_item: the first mission item to run| last_item:  the last mission item to run (after this item is run, the mission ends)|  </summary>
        MISSION_START = 300,
        ///<summary> Arms / Disarms a component |1 to arm, 0 to disarm|  </summary>
        COMPONENT_ARM_DISARM = 400,
        ///<summary> Request the home position from the vehicle. |Reserved| Reserved| Reserved| Reserved| Reserved| Reserved| Reserved|  </summary>
        GET_HOME_POSITION = 410,
        ///<summary> Starts receiver pairing |0:Spektrum| 0:Spektrum DSM2, 1:Spektrum DSMX|  </summary>
        START_RX_PAIR = 500,
        ///<summary> Request the interval between messages for a particular MAVLink message ID |The MAVLink message ID|  </summary>
        GET_MESSAGE_INTERVAL = 510,
        ///<summary> Request the interval between messages for a particular MAVLink message ID. This interface replaces REQUEST_DATA_STREAM |The MAVLink message ID| The interval between two messages, in microseconds. Set to -1 to disable and 0 to request default rate.|  </summary>
        SET_MESSAGE_INTERVAL = 511,
        ///<summary> Request autopilot capabilities |1: Request autopilot version| Reserved (all remaining params)|  </summary>
        REQUEST_AUTOPILOT_CAPABILITIES = 520,
        ///<summary> WIP: Request camera information (CAMERA_INFORMATION) |1: Request camera capabilities| Camera ID| Reserved (all remaining params)|  </summary>
        REQUEST_CAMERA_INFORMATION = 521,
        ///<summary> WIP: Request camera settings (CAMERA_SETTINGS) |1: Request camera settings| Camera ID| Reserved (all remaining params)|  </summary>
        REQUEST_CAMERA_SETTINGS = 522,
        ///<summary> WIP: Set the camera settings part 1 (CAMERA_SETTINGS) |Camera ID| Aperture (1/value)| Aperture locked (0: auto, 1: locked)| Shutter speed in s| Shutter speed locked (0: auto, 1: locked)| ISO sensitivity| ISO sensitivity locked (0: auto, 1: locked)|  </summary>
        SET_CAMERA_SETTINGS_1 = 523,
        ///<summary> WIP: Set the camera settings part 2 (CAMERA_SETTINGS) |Camera ID| White balance locked (0: auto, 1: locked)| White balance (color temperature in K)| Reserved for camera mode ID| Reserved for color mode ID| Reserved for image format ID| Reserved|  </summary>
        SET_CAMERA_SETTINGS_2 = 524,
        ///<summary> WIP: Request storage information (STORAGE_INFORMATION) |1: Request storage information| Storage ID| Reserved (all remaining params)|  </summary>
        REQUEST_STORAGE_INFORMATION = 525,
        ///<summary> WIP: Format a storage medium |1: Format storage| Storage ID| Reserved (all remaining params)|  </summary>
        STORAGE_FORMAT = 526,
        ///<summary> WIP: Request camera capture status (CAMERA_CAPTURE_STATUS) |1: Request camera capture status| Camera ID| Reserved (all remaining params)|  </summary>
        REQUEST_CAMERA_CAPTURE_STATUS = 527,
        ///<summary> WIP: Request flight information (FLIGHT_INFORMATION) |1: Request flight information| Reserved (all remaining params)|  </summary>
        REQUEST_FLIGHT_INFORMATION = 528,
        ///<summary> Start image capture sequence. Sends CAMERA_IMAGE_CAPTURED after each capture. |Duration between two consecutive pictures (in seconds)| Number of images to capture total - 0 for unlimited capture| Resolution in megapixels (0.3 for 640x480, 1.3 for 1280x720, etc), set to 0 if param 4/5 are used, set to -1 for highest resolution possible.| WIP: Resolution horizontal in pixels| WIP: Resolution horizontal in pixels| WIP: Camera ID|  </summary>
        IMAGE_START_CAPTURE = 2000,
        ///<summary> Stop image capture sequence |Camera ID| Reserved|  </summary>
        IMAGE_STOP_CAPTURE = 2001,
        ///<summary> Enable or disable on-board camera triggering system. |Trigger enable/disable (0 for disable, 1 for start)| Shutter integration time (in ms)| Reserved|  </summary>
        DO_TRIGGER_CONTROL = 2003,
        ///<summary> Starts video capture (recording) |Camera ID (0 for all cameras), 1 for first, 2 for second, etc.| Frames per second, set to -1 for highest framerate possible.| Resolution in megapixels (0.3 for 640x480, 1.3 for 1280x720, etc), set to 0 if param 4/5 are used, set to -1 for highest resolution possible.| WIP: Resolution horizontal in pixels| WIP: Resolution horizontal in pixels| WIP: Frequency CAMERA_CAPTURE_STATUS messages should be sent while recording (0 for no messages, otherwise time in Hz)|  </summary>
        VIDEO_START_CAPTURE = 2500,
        ///<summary> Stop the current video capture (recording) |WIP: Camera ID| Reserved|  </summary>
        VIDEO_STOP_CAPTURE = 2501,
        ///<summary> Request to start streaming logging data over MAVLink (see also LOGGING_DATA message) |Format: 0: ULog| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)|  </summary>
        LOGGING_START = 2510,
        ///<summary> Request to stop streaming log data over MAVLink |Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)| Reserved (set to 0)|  </summary>
        LOGGING_STOP = 2511,
        ///<summary>  |Landing gear ID (default: 0, -1 for all)| Landing gear position (Down: 0, Up: 1, NAN for no change)| Reserved, set to NAN| Reserved, set to NAN| Reserved, set to NAN| Reserved, set to NAN| Reserved, set to NAN|  </summary>
        AIRFRAME_CONFIGURATION = 2520,
        ///<summary> Create a panorama at the current position |Viewing angle horizontal of the panorama (in degrees, +- 0.5 the total angle)| Viewing angle vertical of panorama (in degrees)| Speed of the horizontal rotation (in degrees per second)| Speed of the vertical rotation (in degrees per second)|  </summary>
        PANORAMA_CREATE = 2800,
        ///<summary> Request VTOL transition |The target VTOL state, as defined by ENUM MAV_VTOL_STATE. Only MAV_VTOL_STATE_MC and MAV_VTOL_STATE_FW can be used.|  </summary>
        DO_VTOL_TRANSITION = 3000,
        ///<summary> This command sets the submode to standard guided when vehicle is in guided mode. The vehicle holds position and altitude and the user can input the desired velocites along all three axes.                    | </summary>
        SET_GUIDED_SUBMODE_STANDARD = 4000,
        ///<summary> This command sets submode circle when vehicle is in guided mode. Vehicle flies along a circle facing the center of the circle. The user can input the velocity along the circle and change the radius. If no input is given the vehicle will hold position.                    |Radius of desired circle in CIRCLE_MODE| User defined| User defined| User defined| Unscaled target latitude of center of circle in CIRCLE_MODE| Unscaled target longitude of center of circle in CIRCLE_MODE|  </summary>
        SET_GUIDED_SUBMODE_CIRCLE = 4001,
        ///<summary> Fence return point. There can only be one fence return point.          |Reserved| Reserved| Reserved| Reserved| Latitude| Longitude| Altitude|  </summary>
        FENCE_RETURN_POINT = 5000,
        ///<summary> Fence vertex for an inclusion polygon. The vehicle must stay within this area. Minimum of 3 vertices required.          |Polygon vertex count| Reserved| Reserved| Reserved| Latitude| Longitude| Reserved|  </summary>
        FENCE_POLYGON_VERTEX_INCLUSION = 5001,
        ///<summary> Fence vertex for an exclusion polygon. The vehicle must stay outside this area. Minimum of 3 vertices required.          |Polygon vertex count| Reserved| Reserved| Reserved| Latitude| Longitude| Reserved|  </summary>
        FENCE_POLYGON_VERTEX_EXCLUSION = 5002,
        ///<summary> Rally point. You can have multiple rally points defined.          |Reserved| Reserved| Reserved| Reserved| Latitude| Longitude| Altitude|  </summary>
        RALLY_POINT = 5100,
        ///<summary> Deploy payload on a Lat / Lon / Alt position. This includes the navigation to reach the required release position and velocity. |Operation mode. 0: prepare single payload deploy (overwriting previous requests), but do not execute it. 1: execute payload deploy immediately (rejecting further deploy commands during execution, but allowing abort). 2: add payload deploy to existing deployment list.| Desired approach vector in degrees compass heading (0..360). A negative value indicates the system can define the approach vector at will.| Desired ground speed at release time. This can be overriden by the airframe in case it needs to meet minimum airspeed. A negative value indicates the system can define the ground speed at will.| Minimum altitude clearance to the release position in meters. A negative value indicates the system can define the clearance at will.| Latitude unscaled for MISSION_ITEM or in 1e7 degrees for MISSION_ITEM_INT| Longitude unscaled for MISSION_ITEM or in 1e7 degrees for MISSION_ITEM_INT| Altitude, in meters AMSL|  </summary>
        PAYLOAD_PREPARE_DEPLOY = 30001,
        ///<summary> Control the payload deployment. |Operation mode. 0: Abort deployment, continue normal mission. 1: switch to payload deploment mode. 100: delete first payload deployment request. 101: delete all payload deployment requests.| Reserved| Reserved| Reserved| Reserved| Reserved| Reserved|  </summary>
        PAYLOAD_CONTROL_DEPLOY = 30002,
        ///<summary> User defined waypoint item. Ground Station will show the Vehicle as flying through this item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        WAYPOINT_USER_1 = 31000,
        ///<summary> User defined waypoint item. Ground Station will show the Vehicle as flying through this item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        WAYPOINT_USER_2 = 31001,
        ///<summary> User defined waypoint item. Ground Station will show the Vehicle as flying through this item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        WAYPOINT_USER_3 = 31002,
        ///<summary> User defined waypoint item. Ground Station will show the Vehicle as flying through this item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        WAYPOINT_USER_4 = 31003,
        ///<summary> User defined waypoint item. Ground Station will show the Vehicle as flying through this item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        WAYPOINT_USER_5 = 31004,
        ///<summary> User defined spatial item. Ground Station will not show the Vehicle as flying through this item. Example: ROI item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        SPATIAL_USER_1 = 31005,
        ///<summary> User defined spatial item. Ground Station will not show the Vehicle as flying through this item. Example: ROI item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        SPATIAL_USER_2 = 31006,
        ///<summary> User defined spatial item. Ground Station will not show the Vehicle as flying through this item. Example: ROI item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        SPATIAL_USER_3 = 31007,
        ///<summary> User defined spatial item. Ground Station will not show the Vehicle as flying through this item. Example: ROI item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        SPATIAL_USER_4 = 31008,
        ///<summary> User defined spatial item. Ground Station will not show the Vehicle as flying through this item. Example: ROI item. |User defined| User defined| User defined| User defined| Latitude unscaled| Longitude unscaled| Altitude, in meters AMSL|  </summary>
        SPATIAL_USER_5 = 31009,
        ///<summary> User defined command. Ground Station will not show the Vehicle as flying through this item. Example: MAV_CMD_DO_SET_PARAMETER item. |User defined| User defined| User defined| User defined| User defined| User defined| User defined|  </summary>
        USER_1 = 31010,
        ///<summary> User defined command. Ground Station will not show the Vehicle as flying through this item. Example: MAV_CMD_DO_SET_PARAMETER item. |User defined| User defined| User defined| User defined| User defined| User defined| User defined|  </summary>
        USER_2 = 31011,
        ///<summary> User defined command. Ground Station will not show the Vehicle as flying through this item. Example: MAV_CMD_DO_SET_PARAMETER item. |User defined| User defined| User defined| User defined| User defined| User defined| User defined|  </summary>
        USER_3 = 31012,
        ///<summary> User defined command. Ground Station will not show the Vehicle as flying through this item. Example: MAV_CMD_DO_SET_PARAMETER item. |User defined| User defined| User defined| User defined| User defined| User defined| User defined|  </summary>
        USER_4 = 31013,
        ///<summary> User defined command. Ground Station will not show the Vehicle as flying through this item. Example: MAV_CMD_DO_SET_PARAMETER item. |User defined| User defined| User defined| User defined| User defined| User defined| User defined|  </summary>
        USER_5 = 31014,
        ///<summary> A system wide power-off event has been initiated. |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        POWER_OFF_INITIATED = 42000,
        ///<summary> FLY button has been clicked. |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        SOLO_BTN_FLY_CLICK = 42001,
        ///<summary> FLY button has been held for 1.5 seconds. |Takeoff altitude| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        SOLO_BTN_FLY_HOLD = 42002,
        ///<summary> PAUSE button has been clicked. |1 if Solo is in a shot mode, 0 otherwise| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        SOLO_BTN_PAUSE_CLICK = 42003,
        ///<summary> Magnetometer calibration based on fixed position in earth field |MagDeclinationDegrees| MagInclinationDegrees| MagIntensityMilliGauss| YawDegrees| Empty| Empty| Empty|  </summary>
        FIXED_MAG_CAL = 42004,
        ///<summary> Initiate a magnetometer calibration |uint8_t bitmask of magnetometers (0 means all)| Automatically retry on failure (0=no retry, 1=retry).| Save without user input (0=require input, 1=autosave).| Delay (seconds)| Autoreboot (0=user reboot, 1=autoreboot)| Empty| Empty|  </summary>
        DO_START_MAG_CAL = 42424,
        ///<summary> Initiate a magnetometer calibration |uint8_t bitmask of magnetometers (0 means all)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_ACCEPT_MAG_CAL = 42425,
        ///<summary> Cancel a running magnetometer calibration |uint8_t bitmask of magnetometers (0 means all)| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_CANCEL_MAG_CAL = 42426,
        ///<summary> Command autopilot to get into factory test/diagnostic mode |0 means get out of test mode, 1 means get into test mode| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        SET_FACTORY_TEST_MODE = 42427,
        ///<summary> Reply with the version banner |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        DO_SEND_BANNER = 42428,
        ///<summary> Used when doing accelerometer calibration. When sent to the GCS tells it what position to put the vehicle in. When sent to the vehicle says what position the vehicle is in. |Position, one of the ACCELCAL_VEHICLE_POS enum values| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        ACCELCAL_VEHICLE_POS = 42429,
        ///<summary> Causes the gimbal to reset and boot as if it was just powered on |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        GIMBAL_RESET = 42501,
        ///<summary> Reports progress and success or failure of gimbal axis calibration procedure |Gimbal axis we're reporting calibration progress for| Current calibration progress for this axis, 0x64=100%| Status of the calibration| Empty| Empty| Empty| Empty|  </summary>
        GIMBAL_AXIS_CALIBRATION_STATUS = 42502,
        ///<summary> Starts commutation calibration on the gimbal |Empty| Empty| Empty| Empty| Empty| Empty| Empty|  </summary>
        GIMBAL_REQUEST_AXIS_CALIBRATION = 42503,
        ///<summary> Erases gimbal application and parameters |Magic number| Magic number| Magic number| Magic number| Magic number| Magic number| Magic number|  </summary>
        GIMBAL_FULL_RESET = 42505,

    };

    ///<summary>  </summary>
    public enum LIMITS_STATE : byte
    {
        ///<summary> pre-initialization | </summary>
        LIMITS_INIT = 0,
        ///<summary> disabled | </summary>
        LIMITS_DISABLED = 1,
        ///<summary> checking limits | </summary>
        LIMITS_ENABLED = 2,
        ///<summary> a limit has been breached | </summary>
        LIMITS_TRIGGERED = 3,
        ///<summary> taking action eg. RTL | </summary>
        LIMITS_RECOVERING = 4,
        ///<summary> we're no longer in breach of a limit | </summary>
        LIMITS_RECOVERED = 5,

    };

    ///<summary>  </summary>
    public enum LIMIT_MODULE : byte
    {
        ///<summary> pre-initialization | </summary>
        LIMIT_GPSLOCK = 1,
        ///<summary> disabled | </summary>
        LIMIT_GEOFENCE = 2,
        ///<summary> checking limits | </summary>
        LIMIT_ALTITUDE = 4,

    };

    ///<summary> Flags in RALLY_POINT message </summary>
    public enum RALLY_FLAGS : byte
    {
        ///<summary> Flag set when requiring favorable winds for landing. | </summary>
        FAVORABLE_WIND = 1,
        ///<summary> Flag set when plane is to immediately descend to break altitude and land without GCS intervention. Flag not set when plane is to loiter at Rally point until commanded to land. | </summary>
        LAND_IMMEDIATELY = 2,

    };

    ///<summary>  </summary>
    public enum PARACHUTE_ACTION : int /*default*/
    {
        ///<summary> Disable parachute release | </summary>
        PARACHUTE_DISABLE = 0,
        ///<summary> Enable parachute release | </summary>
        PARACHUTE_ENABLE = 1,
        ///<summary> Release parachute | </summary>
        PARACHUTE_RELEASE = 2,

    };

    ///<summary> Gripper actions. </summary>
    public enum GRIPPER_ACTIONS : int /*default*/
    {
        ///<summary> gripper release of cargo | </summary>
        GRIPPER_ACTION_RELEASE = 0,
        ///<summary> gripper grabs onto cargo | </summary>
        GRIPPER_ACTION_GRAB = 1,

    };

    ///<summary>  </summary>
    public enum CAMERA_STATUS_TYPES : byte
    {
        ///<summary> Camera heartbeat, announce camera component ID at 1hz | </summary>
        CAMERA_STATUS_TYPE_HEARTBEAT = 0,
        ///<summary> Camera image triggered | </summary>
        CAMERA_STATUS_TYPE_TRIGGER = 1,
        ///<summary> Camera connection lost | </summary>
        CAMERA_STATUS_TYPE_DISCONNECT = 2,
        ///<summary> Camera unknown error | </summary>
        CAMERA_STATUS_TYPE_ERROR = 3,
        ///<summary> Camera battery low. Parameter p1 shows reported voltage | </summary>
        CAMERA_STATUS_TYPE_LOWBATT = 4,
        ///<summary> Camera storage low. Parameter p1 shows reported shots remaining | </summary>
        CAMERA_STATUS_TYPE_LOWSTORE = 5,
        ///<summary> Camera storage low. Parameter p1 shows reported video minutes remaining | </summary>
        CAMERA_STATUS_TYPE_LOWSTOREV = 6,

    };

    ///<summary>  </summary>
    public enum CAMERA_FEEDBACK_FLAGS : byte
    {
        ///<summary> Shooting photos, not video | </summary>
        CAMERA_FEEDBACK_PHOTO = 0,
        ///<summary> Shooting video, not stills | </summary>
        CAMERA_FEEDBACK_VIDEO = 1,
        ///<summary> Unable to achieve requested exposure (e.g. shutter speed too low) | </summary>
        CAMERA_FEEDBACK_BADEXPOSURE = 2,
        ///<summary> Closed loop feedback from camera, we know for sure it has successfully taken a picture | </summary>
        CAMERA_FEEDBACK_CLOSEDLOOP = 3,
        ///<summary> Open loop camera, an image trigger has been requested but we can't know for sure it has successfully taken a picture | </summary>
        CAMERA_FEEDBACK_OPENLOOP = 4,

    };

    ///<summary>  </summary>
    public enum MAV_MODE_GIMBAL : int /*default*/
    {
        ///<summary> Gimbal is powered on but has not started initializing yet | </summary>
        UNINITIALIZED = 0,
        ///<summary> Gimbal is currently running calibration on the pitch axis | </summary>
        CALIBRATING_PITCH = 1,
        ///<summary> Gimbal is currently running calibration on the roll axis | </summary>
        CALIBRATING_ROLL = 2,
        ///<summary> Gimbal is currently running calibration on the yaw axis | </summary>
        CALIBRATING_YAW = 3,
        ///<summary> Gimbal has finished calibrating and initializing, but is relaxed pending reception of first rate command from copter | </summary>
        INITIALIZED = 4,
        ///<summary> Gimbal is actively stabilizing | </summary>
        ACTIVE = 5,
        ///<summary> Gimbal is relaxed because it missed more than 10 expected rate command messages in a row. Gimbal will move back to active mode when it receives a new rate command | </summary>
        RATE_CMD_TIMEOUT = 6,

    };

    ///<summary>  </summary>
    public enum GIMBAL_AXIS : int /*default*/
    {
        ///<summary> Gimbal yaw axis | </summary>
        YAW = 0,
        ///<summary> Gimbal pitch axis | </summary>
        PITCH = 1,
        ///<summary> Gimbal roll axis | </summary>
        ROLL = 2,

    };

    ///<summary>  </summary>
    public enum GIMBAL_AXIS_CALIBRATION_STATUS : int /*default*/
    {
        ///<summary> Axis calibration is in progress | </summary>
        IN_PROGRESS = 0,
        ///<summary> Axis calibration succeeded | </summary>
        SUCCEEDED = 1,
        ///<summary> Axis calibration failed | </summary>
        FAILED = 2,

    };

    ///<summary>  </summary>
    public enum GIMBAL_AXIS_CALIBRATION_REQUIRED : int /*default*/
    {
        ///<summary> Whether or not this axis requires calibration is unknown at this time | </summary>
        UNKNOWN = 0,
        ///<summary> This axis requires calibration | </summary>
        TRUE = 1,
        ///<summary> This axis does not require calibration | </summary>
        FALSE = 2,

    };

    ///<summary>  </summary>
    public enum GOPRO_HEARTBEAT_STATUS : byte
    {
        ///<summary> No GoPro connected | </summary>
        DISCONNECTED = 0,
        ///<summary> The detected GoPro is not HeroBus compatible | </summary>
        INCOMPATIBLE = 1,
        ///<summary> A HeroBus compatible GoPro is connected | </summary>
        CONNECTED = 2,
        ///<summary> An unrecoverable error was encountered with the connected GoPro, it may require a power cycle | </summary>
        ERROR = 3,

    };

    ///<summary>  </summary>
    public enum GOPRO_HEARTBEAT_FLAGS : byte
    {
        ///<summary> GoPro is currently recording | </summary>
        GOPRO_FLAG_RECORDING = 1,

    };

    ///<summary>  </summary>
    public enum GOPRO_REQUEST_STATUS : byte
    {
        ///<summary> The write message with ID indicated succeeded | </summary>
        GOPRO_REQUEST_SUCCESS = 0,
        ///<summary> The write message with ID indicated failed | </summary>
        GOPRO_REQUEST_FAILED = 1,

    };

    ///<summary>  </summary>
    public enum GOPRO_COMMAND : byte
    {
        ///<summary> (Get/Set) | </summary>
        POWER = 0,
        ///<summary> (Get/Set) | </summary>
        CAPTURE_MODE = 1,
        ///<summary> (___/Set) | </summary>
        SHUTTER = 2,
        ///<summary> (Get/___) | </summary>
        BATTERY = 3,
        ///<summary> (Get/___) | </summary>
        MODEL = 4,
        ///<summary> (Get/Set) | </summary>
        VIDEO_SETTINGS = 5,
        ///<summary> (Get/Set) | </summary>
        LOW_LIGHT = 6,
        ///<summary> (Get/Set) | </summary>
        PHOTO_RESOLUTION = 7,
        ///<summary> (Get/Set) | </summary>
        PHOTO_BURST_RATE = 8,
        ///<summary> (Get/Set) | </summary>
        PROTUNE = 9,
        ///<summary> (Get/Set) Hero 3+ Only | </summary>
        PROTUNE_WHITE_BALANCE = 10,
        ///<summary> (Get/Set) Hero 3+ Only | </summary>
        PROTUNE_COLOUR = 11,
        ///<summary> (Get/Set) Hero 3+ Only | </summary>
        PROTUNE_GAIN = 12,
        ///<summary> (Get/Set) Hero 3+ Only | </summary>
        PROTUNE_SHARPNESS = 13,
        ///<summary> (Get/Set) Hero 3+ Only | </summary>
        PROTUNE_EXPOSURE = 14,
        ///<summary> (Get/Set) | </summary>
        TIME = 15,
        ///<summary> (Get/Set) | </summary>
        CHARGING = 16,

    };

    ///<summary>  </summary>
    public enum GOPRO_CAPTURE_MODE : byte
    {
        ///<summary> Video mode | </summary>
        VIDEO = 0,
        ///<summary> Photo mode | </summary>
        PHOTO = 1,
        ///<summary> Burst mode, hero 3+ only | </summary>
        BURST = 2,
        ///<summary> Time lapse mode, hero 3+ only | </summary>
        TIME_LAPSE = 3,
        ///<summary> Multi shot mode, hero 4 only | </summary>
        MULTI_SHOT = 4,
        ///<summary> Playback mode, hero 4 only, silver only except when LCD or HDMI is connected to black | </summary>
        PLAYBACK = 5,
        ///<summary> Playback mode, hero 4 only | </summary>
        SETUP = 6,
        ///<summary> Mode not yet known | </summary>
        UNKNOWN = 255,

    };

    ///<summary>  </summary>
    public enum GOPRO_RESOLUTION : int /*default*/
    {
        ///<summary> 848 x 480 (480p) | </summary>
        _480p = 0,
        ///<summary> 1280 x 720 (720p) | </summary>
        _720p = 1,
        ///<summary> 1280 x 960 (960p) | </summary>
        _960p = 2,
        ///<summary> 1920 x 1080 (1080p) | </summary>
        _1080p = 3,
        ///<summary> 1920 x 1440 (1440p) | </summary>
        _1440p = 4,
        ///<summary> 2704 x 1440 (2.7k-17:9) | </summary>
        _2_7k_17_9 = 5,
        ///<summary> 2704 x 1524 (2.7k-16:9) | </summary>
        _2_7k_16_9 = 6,
        ///<summary> 2704 x 2028 (2.7k-4:3) | </summary>
        _2_7k_4_3 = 7,
        ///<summary> 3840 x 2160 (4k-16:9) | </summary>
        _4k_16_9 = 8,
        ///<summary> 4096 x 2160 (4k-17:9) | </summary>
        _4k_17_9 = 9,
        ///<summary> 1280 x 720 (720p-SuperView) | </summary>
        _720p_SUPERVIEW = 10,
        ///<summary> 1920 x 1080 (1080p-SuperView) | </summary>
        _1080p_SUPERVIEW = 11,
        ///<summary> 2704 x 1520 (2.7k-SuperView) | </summary>
        _2_7k_SUPERVIEW = 12,
        ///<summary> 3840 x 2160 (4k-SuperView) | </summary>
        _4k_SUPERVIEW = 13,

    };

    ///<summary>  </summary>
    public enum GOPRO_FRAME_RATE : int /*default*/
    {
        ///<summary> 12 FPS | </summary>
        _12 = 0,
        ///<summary> 15 FPS | </summary>
        _15 = 1,
        ///<summary> 24 FPS | </summary>
        _24 = 2,
        ///<summary> 25 FPS | </summary>
        _25 = 3,
        ///<summary> 30 FPS | </summary>
        _30 = 4,
        ///<summary> 48 FPS | </summary>
        _48 = 5,
        ///<summary> 50 FPS | </summary>
        _50 = 6,
        ///<summary> 60 FPS | </summary>
        _60 = 7,
        ///<summary> 80 FPS | </summary>
        _80 = 8,
        ///<summary> 90 FPS | </summary>
        _90 = 9,
        ///<summary> 100 FPS | </summary>
        _100 = 10,
        ///<summary> 120 FPS | </summary>
        _120 = 11,
        ///<summary> 240 FPS | </summary>
        _240 = 12,
        ///<summary> 12.5 FPS | </summary>
        _12_5 = 13,

    };

    ///<summary>  </summary>
    public enum GOPRO_FIELD_OF_VIEW : int /*default*/
    {
        ///<summary> 0x00: Wide | </summary>
        WIDE = 0,
        ///<summary> 0x01: Medium | </summary>
        MEDIUM = 1,
        ///<summary> 0x02: Narrow | </summary>
        NARROW = 2,

    };

    ///<summary>  </summary>
    public enum GOPRO_VIDEO_SETTINGS_FLAGS : int /*default*/
    {
        ///<summary> 0=NTSC, 1=PAL | </summary>
        GOPRO_VIDEO_SETTINGS_TV_MODE = 1,

    };

    ///<summary>  </summary>
    public enum GOPRO_PHOTO_RESOLUTION : int /*default*/
    {
        ///<summary> 5MP Medium | </summary>
        _5MP_MEDIUM = 0,
        ///<summary> 7MP Medium | </summary>
        _7MP_MEDIUM = 1,
        ///<summary> 7MP Wide | </summary>
        _7MP_WIDE = 2,
        ///<summary> 10MP Wide | </summary>
        _10MP_WIDE = 3,
        ///<summary> 12MP Wide | </summary>
        _12MP_WIDE = 4,

    };

    ///<summary>  </summary>
    public enum GOPRO_PROTUNE_WHITE_BALANCE : int /*default*/
    {
        ///<summary> Auto | </summary>
        AUTO = 0,
        ///<summary> 3000K | </summary>
        _3000K = 1,
        ///<summary> 5500K | </summary>
        _5500K = 2,
        ///<summary> 6500K | </summary>
        _6500K = 3,
        ///<summary> Camera Raw | </summary>
        RAW = 4,

    };

    ///<summary>  </summary>
    public enum GOPRO_PROTUNE_COLOUR : int /*default*/
    {
        ///<summary> Auto | </summary>
        STANDARD = 0,
        ///<summary> Neutral | </summary>
        NEUTRAL = 1,

    };

    ///<summary>  </summary>
    public enum GOPRO_PROTUNE_GAIN : int /*default*/
    {
        ///<summary> ISO 400 | </summary>
        _400 = 0,
        ///<summary> ISO 800 (Only Hero 4) | </summary>
        _800 = 1,
        ///<summary> ISO 1600 | </summary>
        _1600 = 2,
        ///<summary> ISO 3200 (Only Hero 4) | </summary>
        _3200 = 3,
        ///<summary> ISO 6400 | </summary>
        _6400 = 4,

    };

    ///<summary>  </summary>
    public enum GOPRO_PROTUNE_SHARPNESS : int /*default*/
    {
        ///<summary> Low Sharpness | </summary>
        LOW = 0,
        ///<summary> Medium Sharpness | </summary>
        MEDIUM = 1,
        ///<summary> High Sharpness | </summary>
        HIGH = 2,

    };

    ///<summary>  </summary>
    public enum GOPRO_PROTUNE_EXPOSURE : int /*default*/
    {
        ///<summary> -5.0 EV (Hero 3+ Only) | </summary>
        NEG_5_0 = 0,
        ///<summary> -4.5 EV (Hero 3+ Only) | </summary>
        NEG_4_5 = 1,
        ///<summary> -4.0 EV (Hero 3+ Only) | </summary>
        NEG_4_0 = 2,
        ///<summary> -3.5 EV (Hero 3+ Only) | </summary>
        NEG_3_5 = 3,
        ///<summary> -3.0 EV (Hero 3+ Only) | </summary>
        NEG_3_0 = 4,
        ///<summary> -2.5 EV (Hero 3+ Only) | </summary>
        NEG_2_5 = 5,
        ///<summary> -2.0 EV | </summary>
        NEG_2_0 = 6,
        ///<summary> -1.5 EV | </summary>
        NEG_1_5 = 7,
        ///<summary> -1.0 EV | </summary>
        NEG_1_0 = 8,
        ///<summary> -0.5 EV | </summary>
        NEG_0_5 = 9,
        ///<summary> 0.0 EV | </summary>
        ZERO = 10,
        ///<summary> +0.5 EV | </summary>
        POS_0_5 = 11,
        ///<summary> +1.0 EV | </summary>
        POS_1_0 = 12,
        ///<summary> +1.5 EV | </summary>
        POS_1_5 = 13,
        ///<summary> +2.0 EV | </summary>
        POS_2_0 = 14,
        ///<summary> +2.5 EV (Hero 3+ Only) | </summary>
        POS_2_5 = 15,
        ///<summary> +3.0 EV (Hero 3+ Only) | </summary>
        POS_3_0 = 16,
        ///<summary> +3.5 EV (Hero 3+ Only) | </summary>
        POS_3_5 = 17,
        ///<summary> +4.0 EV (Hero 3+ Only) | </summary>
        POS_4_0 = 18,
        ///<summary> +4.5 EV (Hero 3+ Only) | </summary>
        POS_4_5 = 19,
        ///<summary> +5.0 EV (Hero 3+ Only) | </summary>
        POS_5_0 = 20,

    };

    ///<summary>  </summary>
    public enum GOPRO_CHARGING : int /*default*/
    {
        ///<summary> Charging disabled | </summary>
        DISABLED = 0,
        ///<summary> Charging enabled | </summary>
        ENABLED = 1,

    };

    ///<summary>  </summary>
    public enum GOPRO_MODEL : int /*default*/
    {
        ///<summary> Unknown gopro model | </summary>
        UNKNOWN = 0,
        ///<summary> Hero 3+ Silver (HeroBus not supported by GoPro) | </summary>
        HERO_3_PLUS_SILVER = 1,
        ///<summary> Hero 3+ Black | </summary>
        HERO_3_PLUS_BLACK = 2,
        ///<summary> Hero 4 Silver | </summary>
        HERO_4_SILVER = 3,
        ///<summary> Hero 4 Black | </summary>
        HERO_4_BLACK = 4,

    };

    ///<summary>  </summary>
    public enum GOPRO_BURST_RATE : int /*default*/
    {
        ///<summary> 3 Shots / 1 Second | </summary>
        _3_IN_1_SECOND = 0,
        ///<summary> 5 Shots / 1 Second | </summary>
        _5_IN_1_SECOND = 1,
        ///<summary> 10 Shots / 1 Second | </summary>
        _10_IN_1_SECOND = 2,
        ///<summary> 10 Shots / 2 Second | </summary>
        _10_IN_2_SECOND = 3,
        ///<summary> 10 Shots / 3 Second (Hero 4 Only) | </summary>
        _10_IN_3_SECOND = 4,
        ///<summary> 30 Shots / 1 Second | </summary>
        _30_IN_1_SECOND = 5,
        ///<summary> 30 Shots / 2 Second | </summary>
        _30_IN_2_SECOND = 6,
        ///<summary> 30 Shots / 3 Second | </summary>
        _30_IN_3_SECOND = 7,
        ///<summary> 30 Shots / 6 Second | </summary>
        _30_IN_6_SECOND = 8,

    };

    ///<summary>  </summary>
    public enum LED_CONTROL_PATTERN : int /*default*/
    {
        ///<summary> LED patterns off (return control to regular vehicle control) | </summary>
        OFF = 0,
        ///<summary> LEDs show pattern during firmware update | </summary>
        FIRMWAREUPDATE = 1,
        ///<summary> Custom Pattern using custom bytes fields | </summary>
        CUSTOM = 255,

    };

    ///<summary> Flags in EKF_STATUS message </summary>
    public enum EKF_STATUS_FLAGS : ushort
    {
        ///<summary> set if EKF's attitude estimate is good | </summary>
        EKF_ATTITUDE = 1,
        ///<summary> set if EKF's horizontal velocity estimate is good | </summary>
        EKF_VELOCITY_HORIZ = 2,
        ///<summary> set if EKF's vertical velocity estimate is good | </summary>
        EKF_VELOCITY_VERT = 4,
        ///<summary> set if EKF's horizontal position (relative) estimate is good | </summary>
        EKF_POS_HORIZ_REL = 8,
        ///<summary> set if EKF's horizontal position (absolute) estimate is good | </summary>
        EKF_POS_HORIZ_ABS = 16,
        ///<summary> set if EKF's vertical position (absolute) estimate is good | </summary>
        EKF_POS_VERT_ABS = 32,
        ///<summary> set if EKF's vertical position (above ground) estimate is good | </summary>
        EKF_POS_VERT_AGL = 64,
        ///<summary> EKF is in constant position mode and does not know it's absolute or relative position | </summary>
        EKF_CONST_POS_MODE = 128,
        ///<summary> set if EKF's predicted horizontal position (relative) estimate is good | </summary>
        EKF_PRED_POS_HORIZ_REL = 256,
        ///<summary> set if EKF's predicted horizontal position (absolute) estimate is good | </summary>
        EKF_PRED_POS_HORIZ_ABS = 512,

    };

    ///<summary>  </summary>
    public enum PID_TUNING_AXIS : byte
    {
        ///<summary>  | </summary>
        PID_TUNING_ROLL = 1,
        ///<summary>  | </summary>
        PID_TUNING_PITCH = 2,
        ///<summary>  | </summary>
        PID_TUNING_YAW = 3,
        ///<summary>  | </summary>
        PID_TUNING_ACCZ = 4,
        ///<summary>  | </summary>
        PID_TUNING_STEER = 5,
        ///<summary>  | </summary>
        PID_TUNING_LANDING = 6,

    };

    ///<summary>  </summary>
    public enum MAG_CAL_STATUS : byte
    {
        ///<summary>  | </summary>
        MAG_CAL_NOT_STARTED = 0,
        ///<summary>  | </summary>
        MAG_CAL_WAITING_TO_START = 1,
        ///<summary>  | </summary>
        MAG_CAL_RUNNING_STEP_ONE = 2,
        ///<summary>  | </summary>
        MAG_CAL_RUNNING_STEP_TWO = 3,
        ///<summary>  | </summary>
        MAG_CAL_SUCCESS = 4,
        ///<summary>  | </summary>
        MAG_CAL_FAILED = 5,

    };

    ///<summary> Special ACK block numbers control activation of dataflash log streaming </summary>
    public enum MAV_REMOTE_LOG_DATA_BLOCK_COMMANDS : uint
    {
        ///<summary> UAV to stop sending DataFlash blocks | </summary>
        MAV_REMOTE_LOG_DATA_BLOCK_STOP = 2147483645,
        ///<summary> UAV to start sending DataFlash blocks | </summary>
        MAV_REMOTE_LOG_DATA_BLOCK_START = 2147483646,

    };

    ///<summary> Possible remote log data block statuses </summary>
    public enum MAV_REMOTE_LOG_DATA_BLOCK_STATUSES : byte
    {
        ///<summary> This block has NOT been received | </summary>
        MAV_REMOTE_LOG_DATA_BLOCK_NACK = 0,
        ///<summary> This block has been received | </summary>
        MAV_REMOTE_LOG_DATA_BLOCK_ACK = 1,

    };

    ///<summary> Bus types for device operations </summary>
    public enum DEVICE_OP_BUSTYPE : byte
    {
        ///<summary> I2C Device operation | </summary>
        I2C = 0,
        ///<summary> SPI Device operation | </summary>
        SPI = 1,

    };

    ///<summary> Deepstall flight stage </summary>
    public enum DEEPSTALL_STAGE : byte
    {
        ///<summary> Flying to the landing point | </summary>
        FLY_TO_LANDING = 0,
        ///<summary> Building an estimate of the wind | </summary>
        ESTIMATE_WIND = 1,
        ///<summary> Waiting to breakout of the loiter to fly the approach | </summary>
        WAIT_FOR_BREAKOUT = 2,
        ///<summary> Flying to the first arc point to turn around to the landing point | </summary>
        FLY_TO_ARC = 3,
        ///<summary> Turning around back to the deepstall landing point | </summary>
        ARC = 4,
        ///<summary> Approaching the landing point | </summary>
        APPROACH = 5,
        ///<summary> Stalling and steering towards the land point | </summary>
        LAND = 6,

    };


    ///<summary> Micro air vehicle / autopilot classes. This identifies the individual model. </summary>
    public enum MAV_AUTOPILOT : byte
    {
        ///<summary> Generic autopilot, full support for everything | </summary>
        GENERIC = 0,
        ///<summary> Reserved for future use. | </summary>
        RESERVED = 1,
        ///<summary> SLUGS autopilot, http://slugsuav.soe.ucsc.edu | </summary>
        SLUGS = 2,
        ///<summary> ArduPilotMega / ArduCopter, http://diydrones.com | </summary>
        ARDUPILOTMEGA = 3,
        ///<summary> OpenPilot, http://openpilot.org | </summary>
        OPENPILOT = 4,
        ///<summary> Generic autopilot only supporting simple waypoints | </summary>
        GENERIC_WAYPOINTS_ONLY = 5,
        ///<summary> Generic autopilot supporting waypoints and other simple navigation commands | </summary>
        GENERIC_WAYPOINTS_AND_SIMPLE_NAVIGATION_ONLY = 6,
        ///<summary> Generic autopilot supporting the full mission command set | </summary>
        GENERIC_MISSION_FULL = 7,
        ///<summary> No valid autopilot, e.g. a GCS or other MAVLink component | </summary>
        INVALID = 8,
        ///<summary> PPZ UAV - http://nongnu.org/paparazzi | </summary>
        PPZ = 9,
        ///<summary> UAV Dev Board | </summary>
        UDB = 10,
        ///<summary> FlexiPilot | </summary>
        FP = 11,
        ///<summary> PX4 Autopilot - http://pixhawk.ethz.ch/px4/ | </summary>
        PX4 = 12,
        ///<summary> SMACCMPilot - http://smaccmpilot.org | </summary>
        SMACCMPILOT = 13,
        ///<summary> AutoQuad -- http://autoquad.org | </summary>
        AUTOQUAD = 14,
        ///<summary> Armazila -- http://armazila.com | </summary>
        ARMAZILA = 15,
        ///<summary> Aerob -- http://aerob.ru | </summary>
        AEROB = 16,
        ///<summary> ASLUAV autopilot -- http://www.asl.ethz.ch | </summary>
        ASLUAV = 17,
        ///<summary> SmartAP Autopilot - http://sky-drones.com | </summary>
        SMARTAP = 18,

    };

    ///<summary>  </summary>
    public enum MAV_TYPE : byte
    {
        ///<summary> Generic micro air vehicle. | </summary>
        GENERIC = 0,
        ///<summary> Fixed wing aircraft. | </summary>
        FIXED_WING = 1,
        ///<summary> Quadrotor | </summary>
        QUADROTOR = 2,
        ///<summary> Coaxial helicopter | </summary>
        COAXIAL = 3,
        ///<summary> Normal helicopter with tail rotor. | </summary>
        HELICOPTER = 4,
        ///<summary> Ground installation | </summary>
        ANTENNA_TRACKER = 5,
        ///<summary> Operator control unit / ground control station | </summary>
        GCS = 6,
        ///<summary> Airship, controlled | </summary>
        AIRSHIP = 7,
        ///<summary> Free balloon, uncontrolled | </summary>
        FREE_BALLOON = 8,
        ///<summary> Rocket | </summary>
        ROCKET = 9,
        ///<summary> Ground rover | </summary>
        GROUND_ROVER = 10,
        ///<summary> Surface vessel, boat, ship | </summary>
        SURFACE_BOAT = 11,
        ///<summary> Submarine | </summary>
        SUBMARINE = 12,
        ///<summary> Hexarotor | </summary>
        HEXAROTOR = 13,
        ///<summary> Octorotor | </summary>
        OCTOROTOR = 14,
        ///<summary> Tricopter | </summary>
        TRICOPTER = 15,
        ///<summary> Flapping wing | </summary>
        FLAPPING_WING = 16,
        ///<summary> Kite | </summary>
        KITE = 17,
        ///<summary> Onboard companion controller | </summary>
        ONBOARD_CONTROLLER = 18,
        ///<summary> Two-rotor VTOL using control surfaces in vertical operation in addition. Tailsitter. | </summary>
        VTOL_DUOROTOR = 19,
        ///<summary> Quad-rotor VTOL using a V-shaped quad config in vertical operation. Tailsitter. | </summary>
        VTOL_QUADROTOR = 20,
        ///<summary> Tiltrotor VTOL | </summary>
        VTOL_TILTROTOR = 21,
        ///<summary> VTOL reserved 2 | </summary>
        VTOL_RESERVED2 = 22,
        ///<summary> VTOL reserved 3 | </summary>
        VTOL_RESERVED3 = 23,
        ///<summary> VTOL reserved 4 | </summary>
        VTOL_RESERVED4 = 24,
        ///<summary> VTOL reserved 5 | </summary>
        VTOL_RESERVED5 = 25,
        ///<summary> Onboard gimbal | </summary>
        GIMBAL = 26,
        ///<summary> Onboard ADSB peripheral | </summary>
        ADSB = 27,
        ///<summary> Dodecarotor | </summary>
        DODECAROTOR = 28,

    };

    ///<summary> These values define the type of firmware release.  These values indicate the first version or release of this type.  For example the first alpha release would be 64, the second would be 65. </summary>
    public enum FIRMWARE_VERSION_TYPE : int /*default*/
    {
        ///<summary> development release | </summary>
        DEV = 0,
        ///<summary> alpha release | </summary>
        ALPHA = 64,
        ///<summary> beta release | </summary>
        BETA = 128,
        ///<summary> release candidate | </summary>
        RC = 192,
        ///<summary> official stable release | </summary>
        OFFICIAL = 255,

    };

    ///<summary> These flags encode the MAV mode. </summary>
    public enum MAV_MODE_FLAG : byte
    {
        ///<summary> 0b00000001 Reserved for future use. | </summary>
        CUSTOM_MODE_ENABLED = 1,
        ///<summary> 0b00000010 system has a test mode enabled. This flag is intended for temporary system tests and should not be used for stable implementations. | </summary>
        TEST_ENABLED = 2,
        ///<summary> 0b00000100 autonomous mode enabled, system finds its own goal positions. Guided flag can be set or not, depends on the actual implementation. | </summary>
        AUTO_ENABLED = 4,
        ///<summary> 0b00001000 guided mode enabled, system flies MISSIONs / mission items. | </summary>
        GUIDED_ENABLED = 8,
        ///<summary> 0b00010000 system stabilizes electronically its attitude (and optionally position). It needs however further control inputs to move around. | </summary>
        STABILIZE_ENABLED = 16,
        ///<summary> 0b00100000 hardware in the loop simulation. All motors / actuators are blocked, but internal software is full operational. | </summary>
        HIL_ENABLED = 32,
        ///<summary> 0b01000000 remote control input is enabled. | </summary>
        MANUAL_INPUT_ENABLED = 64,
        ///<summary> 0b10000000 MAV safety set to armed. Motors are enabled / running / can start. Ready to fly. Additional note: this flag is to be ignore when sent in the command MAV_CMD_DO_SET_MODE and MAV_CMD_COMPONENT_ARM_DISARM shall be used instead. The flag can still be used to report the armed state. | </summary>
        SAFETY_ARMED = 128,

    };

    ///<summary> These values encode the bit positions of the decode position. These values can be used to read the value of a flag bit by combining the base_mode variable with AND with the flag position value. The result will be either 0 or 1, depending on if the flag is set or not. </summary>
    public enum MAV_MODE_FLAG_DECODE_POSITION : int /*default*/
    {
        ///<summary> Eighth bit: 00000001 | </summary>
        CUSTOM_MODE = 1,
        ///<summary> Seventh bit: 00000010 | </summary>
        TEST = 2,
        ///<summary> Sixt bit:   00000100 | </summary>
        AUTO = 4,
        ///<summary> Fifth bit:  00001000 | </summary>
        GUIDED = 8,
        ///<summary> Fourth bit: 00010000 | </summary>
        STABILIZE = 16,
        ///<summary> Third bit:  00100000 | </summary>
        HIL = 32,
        ///<summary> Second bit: 01000000 | </summary>
        MANUAL = 64,
        ///<summary> First bit:  10000000 | </summary>
        SAFETY = 128,

    };

    ///<summary> Override command, pauses current mission execution and moves immediately to a position </summary>
    public enum MAV_GOTO : int /*default*/
    {
        ///<summary> Hold at the current position. | </summary>
        DO_HOLD = 0,
        ///<summary> Continue with the next item in mission execution. | </summary>
        DO_CONTINUE = 1,
        ///<summary> Hold at the current position of the system | </summary>
        HOLD_AT_CURRENT_POSITION = 2,
        ///<summary> Hold at the position specified in the parameters of the DO_HOLD action | </summary>
        HOLD_AT_SPECIFIED_POSITION = 3,

    };

    ///<summary> These defines are predefined OR-combined mode flags. There is no need to use values from this enum, but it                simplifies the use of the mode flags. Note that manual input is enabled in all modes as a safety override. </summary>
    public enum MAV_MODE : byte
    {
        ///<summary> System is not ready to fly, booting, calibrating, etc. No flag is set. | </summary>
        PREFLIGHT = 0,
        ///<summary> System is allowed to be active, under manual (RC) control, no stabilization | </summary>
        MANUAL_DISARMED = 64,
        ///<summary> UNDEFINED mode. This solely depends on the autopilot - use with caution, intended for developers only. | </summary>
        TEST_DISARMED = 66,
        ///<summary> System is allowed to be active, under assisted RC control. | </summary>
        STABILIZE_DISARMED = 80,
        ///<summary> System is allowed to be active, under autonomous control, manual setpoint | </summary>
        GUIDED_DISARMED = 88,
        ///<summary> System is allowed to be active, under autonomous control and navigation (the trajectory is decided onboard and not pre-programmed by MISSIONs) | </summary>
        AUTO_DISARMED = 92,
        ///<summary> System is allowed to be active, under manual (RC) control, no stabilization | </summary>
        MANUAL_ARMED = 192,
        ///<summary> UNDEFINED mode. This solely depends on the autopilot - use with caution, intended for developers only. | </summary>
        TEST_ARMED = 194,
        ///<summary> System is allowed to be active, under assisted RC control. | </summary>
        STABILIZE_ARMED = 208,
        ///<summary> System is allowed to be active, under autonomous control, manual setpoint | </summary>
        GUIDED_ARMED = 216,
        ///<summary> System is allowed to be active, under autonomous control and navigation (the trajectory is decided onboard and not pre-programmed by MISSIONs) | </summary>
        AUTO_ARMED = 220,

    };

    ///<summary>  </summary>
    public enum MAV_STATE : byte
    {
        ///<summary> Uninitialized system, state is unknown. | </summary>
        UNINIT = 0,
        ///<summary> System is booting up. | </summary>
        BOOT = 1,
        ///<summary> System is calibrating and not flight-ready. | </summary>
        CALIBRATING = 2,
        ///<summary> System is grounded and on standby. It can be launched any time. | </summary>
        STANDBY = 3,
        ///<summary> System is active and might be already airborne. Motors are engaged. | </summary>
        ACTIVE = 4,
        ///<summary> System is in a non-normal flight mode. It can however still navigate. | </summary>
        CRITICAL = 5,
        ///<summary> System is in a non-normal flight mode. It lost control over parts or over the whole airframe. It is in mayday and going down. | </summary>
        EMERGENCY = 6,
        ///<summary> System just initialized its power-down sequence, will shut down now. | </summary>
        POWEROFF = 7,

    };

    ///<summary>  </summary>
    public enum MAV_COMPONENT : int /*default*/
    {
        ///<summary>  | </summary>
        MAV_COMP_ID_ALL = 0,
        ///<summary>  | </summary>
        MAV_COMP_ID_CAMERA = 100,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO1 = 140,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO2 = 141,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO3 = 142,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO4 = 143,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO5 = 144,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO6 = 145,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO7 = 146,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO8 = 147,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO9 = 148,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO10 = 149,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO11 = 150,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO12 = 151,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO13 = 152,
        ///<summary>  | </summary>
        MAV_COMP_ID_SERVO14 = 153,
        ///<summary>  | </summary>
        MAV_COMP_ID_GIMBAL = 154,
        ///<summary>  | </summary>
        MAV_COMP_ID_LOG = 155,
        ///<summary>  | </summary>
        MAV_COMP_ID_ADSB = 156,
        ///<summary> On Screen Display (OSD) devices for video links | </summary>
        MAV_COMP_ID_OSD = 157,
        ///<summary> Generic autopilot peripheral component ID. Meant for devices that do not implement the parameter sub-protocol | </summary>
        MAV_COMP_ID_PERIPHERAL = 158,
        ///<summary>  | </summary>
        MAV_COMP_ID_QX1_GIMBAL = 159,
        ///<summary>  | </summary>
        MAV_COMP_ID_MAPPER = 180,
        ///<summary>  | </summary>
        MAV_COMP_ID_MISSIONPLANNER = 190,
        ///<summary>  | </summary>
        MAV_COMP_ID_PATHPLANNER = 195,
        ///<summary>  | </summary>
        MAV_COMP_ID_IMU = 200,
        ///<summary>  | </summary>
        MAV_COMP_ID_IMU_2 = 201,
        ///<summary>  | </summary>
        MAV_COMP_ID_IMU_3 = 202,
        ///<summary>  | </summary>
        MAV_COMP_ID_GPS = 220,
        ///<summary>  | </summary>
        MAV_COMP_ID_UDP_BRIDGE = 240,
        ///<summary>  | </summary>
        MAV_COMP_ID_UART_BRIDGE = 241,
        ///<summary>  | </summary>
        MAV_COMP_ID_SYSTEM_CONTROL = 250,

    };

    ///<summary> These encode the sensors whose status is sent as part of the SYS_STATUS message. </summary>
    public enum MAV_SYS_STATUS_SENSOR : uint
    {
        ///<summary> 0x01 3D gyro | </summary>
        _3D_GYRO = 1,
        ///<summary> 0x02 3D accelerometer | </summary>
        _3D_ACCEL = 2,
        ///<summary> 0x04 3D magnetometer | </summary>
        _3D_MAG = 4,
        ///<summary> 0x08 absolute pressure | </summary>
        ABSOLUTE_PRESSURE = 8,
        ///<summary> 0x10 differential pressure | </summary>
        DIFFERENTIAL_PRESSURE = 16,
        ///<summary> 0x20 GPS | </summary>
        GPS = 32,
        ///<summary> 0x40 optical flow | </summary>
        OPTICAL_FLOW = 64,
        ///<summary> 0x80 computer vision position | </summary>
        VISION_POSITION = 128,
        ///<summary> 0x100 laser based position | </summary>
        LASER_POSITION = 256,
        ///<summary> 0x200 external ground truth (Vicon or Leica) | </summary>
        EXTERNAL_GROUND_TRUTH = 512,
        ///<summary> 0x400 3D angular rate control | </summary>
        ANGULAR_RATE_CONTROL = 1024,
        ///<summary> 0x800 attitude stabilization | </summary>
        ATTITUDE_STABILIZATION = 2048,
        ///<summary> 0x1000 yaw position | </summary>
        YAW_POSITION = 4096,
        ///<summary> 0x2000 z/altitude control | </summary>
        Z_ALTITUDE_CONTROL = 8192,
        ///<summary> 0x4000 x/y position control | </summary>
        XY_POSITION_CONTROL = 16384,
        ///<summary> 0x8000 motor outputs / control | </summary>
        MOTOR_OUTPUTS = 32768,
        ///<summary> 0x10000 rc receiver | </summary>
        RC_RECEIVER = 65536,
        ///<summary> 0x20000 2nd 3D gyro | </summary>
        _3D_GYRO2 = 131072,
        ///<summary> 0x40000 2nd 3D accelerometer | </summary>
        _3D_ACCEL2 = 262144,
        ///<summary> 0x80000 2nd 3D magnetometer | </summary>
        _3D_MAG2 = 524288,
        ///<summary> 0x100000 geofence | </summary>
        MAV_SYS_STATUS_GEOFENCE = 1048576,
        ///<summary> 0x200000 AHRS subsystem health | </summary>
        MAV_SYS_STATUS_AHRS = 2097152,
        ///<summary> 0x400000 Terrain subsystem health | </summary>
        MAV_SYS_STATUS_TERRAIN = 4194304,
        ///<summary> 0x800000 Motors are reversed | </summary>
        MAV_SYS_STATUS_REVERSE_MOTOR = 8388608,
        ///<summary> 0x1000000 Logging | </summary>
        MAV_SYS_STATUS_LOGGING = 16777216,
        ///<summary> 0x2000000 Battery | </summary>
        BATTERY = 33554432,

    };

    ///<summary>  </summary>
    public enum MAV_FRAME : byte
    {
        ///<summary> Global coordinate frame, WGS84 coordinate system. First value / x: latitude, second value / y: longitude, third value / z: positive altitude over mean sea level (MSL) | </summary>
        GLOBAL = 0,
        ///<summary> Local coordinate frame, Z-up (x: north, y: east, z: down). | </summary>
        LOCAL_NED = 1,
        ///<summary> NOT a coordinate frame, indicates a mission command. | </summary>
        MISSION = 2,
        ///<summary> Global coordinate frame, WGS84 coordinate system, relative altitude over ground with respect to the home position. First value / x: latitude, second value / y: longitude, third value / z: positive altitude with 0 being at the altitude of the home location. | </summary>
        GLOBAL_RELATIVE_ALT = 3,
        ///<summary> Local coordinate frame, Z-down (x: east, y: north, z: up) | </summary>
        LOCAL_ENU = 4,
        ///<summary> Global coordinate frame, WGS84 coordinate system. First value / x: latitude in degrees*1.0e-7, second value / y: longitude in degrees*1.0e-7, third value / z: positive altitude over mean sea level (MSL) | </summary>
        GLOBAL_INT = 5,
        ///<summary> Global coordinate frame, WGS84 coordinate system, relative altitude over ground with respect to the home position. First value / x: latitude in degrees*10e-7, second value / y: longitude in degrees*10e-7, third value / z: positive altitude with 0 being at the altitude of the home location. | </summary>
        GLOBAL_RELATIVE_ALT_INT = 6,
        ///<summary> Offset to the current local frame. Anything expressed in this frame should be added to the current local frame position. | </summary>
        LOCAL_OFFSET_NED = 7,
        ///<summary> Setpoint in body NED frame. This makes sense if all position control is externalized - e.g. useful to command 2 m/s^2 acceleration to the right. | </summary>
        BODY_NED = 8,
        ///<summary> Offset in body NED frame. This makes sense if adding setpoints to the current flight path, to avoid an obstacle - e.g. useful to command 2 m/s^2 acceleration to the east. | </summary>
        BODY_OFFSET_NED = 9,
        ///<summary> Global coordinate frame with above terrain level altitude. WGS84 coordinate system, relative altitude over terrain with respect to the waypoint coordinate. First value / x: latitude in degrees, second value / y: longitude in degrees, third value / z: positive altitude in meters with 0 being at ground level in terrain model. | </summary>
        GLOBAL_TERRAIN_ALT = 10,
        ///<summary> Global coordinate frame with above terrain level altitude. WGS84 coordinate system, relative altitude over terrain with respect to the waypoint coordinate. First value / x: latitude in degrees*10e-7, second value / y: longitude in degrees*10e-7, third value / z: positive altitude in meters with 0 being at ground level in terrain model. | </summary>
        GLOBAL_TERRAIN_ALT_INT = 11,

    };

    ///<summary>  </summary>
    public enum MAVLINK_DATA_STREAM_TYPE : int /*default*/
    {
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_JPEG = 1,
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_BMP = 2,
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_RAW8U = 3,
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_RAW32U = 4,
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_PGM = 5,
        ///<summary>  | </summary>
        MAVLINK_DATA_STREAM_IMG_PNG = 6,

    };

    ///<summary>  </summary>
    public enum FENCE_ACTION : int /*default*/
    {
        ///<summary> Disable fenced mode | </summary>
        NONE = 0,
        ///<summary> Switched to guided mode to return point (fence point 0) | </summary>
        GUIDED = 1,
        ///<summary> Report fence breach, but don't take action | </summary>
        REPORT = 2,
        ///<summary> Switched to guided mode to return point (fence point 0) with manual throttle control | </summary>
        GUIDED_THR_PASS = 3,
        ///<summary> Switch to RTL (return to launch) mode and head for the return point. | </summary>
        RTL = 4,

    };

    ///<summary>  </summary>
    public enum FENCE_BREACH : byte
    {
        ///<summary> No last fence breach | </summary>
        NONE = 0,
        ///<summary> Breached minimum altitude | </summary>
        MINALT = 1,
        ///<summary> Breached maximum altitude | </summary>
        MAXALT = 2,
        ///<summary> Breached fence boundary | </summary>
        BOUNDARY = 3,

    };

    ///<summary> Enumeration of possible mount operation modes </summary>
    public enum MAV_MOUNT_MODE : byte
    {
        ///<summary> Load and keep safe position (Roll,Pitch,Yaw) from permant memory and stop stabilization | </summary>
        RETRACT = 0,
        ///<summary> Load and keep neutral position (Roll,Pitch,Yaw) from permanent memory. | </summary>
        NEUTRAL = 1,
        ///<summary> Load neutral position and start MAVLink Roll,Pitch,Yaw control with stabilization | </summary>
        MAVLINK_TARGETING = 2,
        ///<summary> Load neutral position and start RC Roll,Pitch,Yaw control with stabilization | </summary>
        RC_TARGETING = 3,
        ///<summary> Load neutral position and start to point to Lat,Lon,Alt | </summary>
        GPS_POINT = 4,

    };

    ///<summary> THIS INTERFACE IS DEPRECATED AS OF JULY 2015. Please use MESSAGE_INTERVAL instead. A data stream is not a fixed set of messages, but rather a      recommendation to the autopilot software. Individual autopilots may or may not obey      the recommended messages. </summary>
    public enum MAV_DATA_STREAM : int /*default*/
    {
        ///<summary> Enable all data streams | </summary>
        ALL = 0,
        ///<summary> Enable IMU_RAW, GPS_RAW, GPS_STATUS packets. | </summary>
        RAW_SENSORS = 1,
        ///<summary> Enable GPS_STATUS, CONTROL_STATUS, AUX_STATUS | </summary>
        EXTENDED_STATUS = 2,
        ///<summary> Enable RC_CHANNELS_SCALED, RC_CHANNELS_RAW, SERVO_OUTPUT_RAW | </summary>
        RC_CHANNELS = 3,
        ///<summary> Enable ATTITUDE_CONTROLLER_OUTPUT, POSITION_CONTROLLER_OUTPUT, NAV_CONTROLLER_OUTPUT. | </summary>
        RAW_CONTROLLER = 4,
        ///<summary> Enable LOCAL_POSITION, GLOBAL_POSITION/GLOBAL_POSITION_INT messages. | </summary>
        POSITION = 6,
        ///<summary> Dependent on the autopilot | </summary>
        EXTRA1 = 10,
        ///<summary> Dependent on the autopilot | </summary>
        EXTRA2 = 11,
        ///<summary> Dependent on the autopilot | </summary>
        EXTRA3 = 12,

    };

    ///<summary>  The ROI (region of interest) for the vehicle. This can be                 be used by the vehicle for camera/vehicle attitude alignment (see                 MAV_CMD_NAV_ROI). </summary>
    public enum MAV_ROI : int /*default*/
    {
        ///<summary> No region of interest. | </summary>
        NONE = 0,
        ///<summary> Point toward next MISSION. | </summary>
        WPNEXT = 1,
        ///<summary> Point toward given MISSION. | </summary>
        WPINDEX = 2,
        ///<summary> Point toward fixed location. | </summary>
        LOCATION = 3,
        ///<summary> Point toward of given id. | </summary>
        TARGET = 4,

    };

    ///<summary> ACK / NACK / ERROR values as a result of MAV_CMDs and for mission item transmission. </summary>
    public enum MAV_CMD_ACK : int /*default*/
    {
        ///<summary> Command / mission item is ok. | </summary>
        OK = 1,
        ///<summary> Generic error message if none of the other reasons fails or if no detailed error reporting is implemented. | </summary>
        ERR_FAIL = 2,
        ///<summary> The system is refusing to accept this command from this source / communication partner. | </summary>
        ERR_ACCESS_DENIED = 3,
        ///<summary> Command or mission item is not supported, other commands would be accepted. | </summary>
        ERR_NOT_SUPPORTED = 4,
        ///<summary> The coordinate frame of this command / mission item is not supported. | </summary>
        ERR_COORDINATE_FRAME_NOT_SUPPORTED = 5,
        ///<summary> The coordinate frame of this command is ok, but he coordinate values exceed the safety limits of this system. This is a generic error, please use the more specific error messages below if possible. | </summary>
        ERR_COORDINATES_OUT_OF_RANGE = 6,
        ///<summary> The X or latitude value is out of range. | </summary>
        ERR_X_LAT_OUT_OF_RANGE = 7,
        ///<summary> The Y or longitude value is out of range. | </summary>
        ERR_Y_LON_OUT_OF_RANGE = 8,
        ///<summary> The Z or altitude value is out of range. | </summary>
        ERR_Z_ALT_OUT_OF_RANGE = 9,

    };

    ///<summary> Specifies the datatype of a MAVLink parameter. </summary>
    public enum MAV_PARAM_TYPE : byte
    {
        ///<summary> 8-bit unsigned integer | </summary>
        UINT8 = 1,
        ///<summary> 8-bit signed integer | </summary>
        INT8 = 2,
        ///<summary> 16-bit unsigned integer | </summary>
        UINT16 = 3,
        ///<summary> 16-bit signed integer | </summary>
        INT16 = 4,
        ///<summary> 32-bit unsigned integer | </summary>
        UINT32 = 5,
        ///<summary> 32-bit signed integer | </summary>
        INT32 = 6,
        ///<summary> 64-bit unsigned integer | </summary>
        UINT64 = 7,
        ///<summary> 64-bit signed integer | </summary>
        INT64 = 8,
        ///<summary> 32-bit floating-point | </summary>
        REAL32 = 9,
        ///<summary> 64-bit floating-point | </summary>
        REAL64 = 10,

    };

    ///<summary> result from a mavlink command </summary>
    public enum MAV_RESULT : byte
    {
        ///<summary> Command ACCEPTED and EXECUTED | </summary>
        ACCEPTED = 0,
        ///<summary> Command TEMPORARY REJECTED/DENIED | </summary>
        TEMPORARILY_REJECTED = 1,
        ///<summary> Command PERMANENTLY DENIED | </summary>
        DENIED = 2,
        ///<summary> Command UNKNOWN/UNSUPPORTED | </summary>
        UNSUPPORTED = 3,
        ///<summary> Command executed, but failed | </summary>
        FAILED = 4,

    };

    ///<summary> result in a mavlink mission ack </summary>
    public enum MAV_MISSION_RESULT : byte
    {
        ///<summary> mission accepted OK | </summary>
        MAV_MISSION_ACCEPTED = 0,
        ///<summary> generic error / not accepting mission commands at all right now | </summary>
        MAV_MISSION_ERROR = 1,
        ///<summary> coordinate frame is not supported | </summary>
        MAV_MISSION_UNSUPPORTED_FRAME = 2,
        ///<summary> command is not supported | </summary>
        MAV_MISSION_UNSUPPORTED = 3,
        ///<summary> mission item exceeds storage space | </summary>
        MAV_MISSION_NO_SPACE = 4,
        ///<summary> one of the parameters has an invalid value | </summary>
        MAV_MISSION_INVALID = 5,
        ///<summary> param1 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM1 = 6,
        ///<summary> param2 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM2 = 7,
        ///<summary> param3 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM3 = 8,
        ///<summary> param4 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM4 = 9,
        ///<summary> x/param5 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM5_X = 10,
        ///<summary> y/param6 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM6_Y = 11,
        ///<summary> param7 has an invalid value | </summary>
        MAV_MISSION_INVALID_PARAM7 = 12,
        ///<summary> received waypoint out of sequence | </summary>
        MAV_MISSION_INVALID_SEQUENCE = 13,
        ///<summary> not accepting any mission commands from this communication partner | </summary>
        MAV_MISSION_DENIED = 14,

    };

    ///<summary> Indicates the severity level, generally used for status messages to indicate their relative urgency. Based on RFC-5424 using expanded definitions at: http://www.kiwisyslog.com/kb/info:-syslog-message-levels/. </summary>
    public enum MAV_SEVERITY : byte
    {
        ///<summary> System is unusable. This is a "panic" condition. | </summary>
        EMERGENCY = 0,
        ///<summary> Action should be taken immediately. Indicates error in non-critical systems. | </summary>
        ALERT = 1,
        ///<summary> Action must be taken immediately. Indicates failure in a primary system. | </summary>
        CRITICAL = 2,
        ///<summary> Indicates an error in secondary/redundant systems. | </summary>
        ERROR = 3,
        ///<summary> Indicates about a possible future error if this is not resolved within a given timeframe. Example would be a low battery warning. | </summary>
        WARNING = 4,
        ///<summary> An unusual event has occured, though not an error condition. This should be investigated for the root cause. | </summary>
        NOTICE = 5,
        ///<summary> Normal operational messages. Useful for logging. No action is required for these messages. | </summary>
        INFO = 6,
        ///<summary> Useful non-operational messages that can assist in debugging. These should not occur during normal operation. | </summary>
        DEBUG = 7,

    };

    ///<summary> Power supply status flags (bitmask) </summary>
    public enum MAV_POWER_STATUS : ushort
    {
        ///<summary> main brick power supply valid | </summary>
        BRICK_VALID = 1,
        ///<summary> main servo power supply valid for FMU | </summary>
        SERVO_VALID = 2,
        ///<summary> USB power is connected | </summary>
        USB_CONNECTED = 4,
        ///<summary> peripheral supply is in over-current state | </summary>
        PERIPH_OVERCURRENT = 8,
        ///<summary> hi-power peripheral supply is in over-current state | </summary>
        PERIPH_HIPOWER_OVERCURRENT = 16,
        ///<summary> Power status has changed since boot | </summary>
        CHANGED = 32,

    };

    ///<summary> SERIAL_CONTROL device types </summary>
    public enum SERIAL_CONTROL_DEV : byte
    {
        ///<summary> First telemetry port | </summary>
        TELEM1 = 0,
        ///<summary> Second telemetry port | </summary>
        TELEM2 = 1,
        ///<summary> First GPS port | </summary>
        GPS1 = 2,
        ///<summary> Second GPS port | </summary>
        GPS2 = 3,
        ///<summary> system shell | </summary>
        SHELL = 10,

    };

    ///<summary> SERIAL_CONTROL flags (bitmask) </summary>
    public enum SERIAL_CONTROL_FLAG : byte
    {
        ///<summary> Set if this is a reply | </summary>
        REPLY = 1,
        ///<summary> Set if the sender wants the receiver to send a response as another SERIAL_CONTROL message | </summary>
        RESPOND = 2,
        ///<summary> Set if access to the serial port should be removed from whatever driver is currently using it, giving exclusive access to the SERIAL_CONTROL protocol. The port can be handed back by sending a request without this flag set | </summary>
        EXCLUSIVE = 4,
        ///<summary> Block on writes to the serial port | </summary>
        BLOCKING = 8,
        ///<summary> Send multiple replies until port is drained | </summary>
        MULTI = 16,

    };

    ///<summary> Enumeration of distance sensor types </summary>
    public enum MAV_DISTANCE_SENSOR : byte
    {
        ///<summary> Laser rangefinder, e.g. LightWare SF02/F or PulsedLight units | </summary>
        LASER = 0,
        ///<summary> Ultrasound rangefinder, e.g. MaxBotix units | </summary>
        ULTRASOUND = 1,
        ///<summary> Infrared rangefinder, e.g. Sharp units | </summary>
        INFRARED = 2,
        ///<summary> Radar type, e.g. uLanding units | </summary>
        RADAR = 3,
        ///<summary> Broken or unknown type, e.g. analog units | </summary>
        UNKNOWN = 4,

    };

    ///<summary> Enumeration of sensor orientation, according to its rotations </summary>
    public enum MAV_SENSOR_ORIENTATION : byte
    {
        ///<summary> Roll: 0, Pitch: 0, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_NONE = 0,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 45 | </summary>
        MAV_SENSOR_ROTATION_YAW_45 = 1,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_YAW_90 = 2,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 135 | </summary>
        MAV_SENSOR_ROTATION_YAW_135 = 3,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 180 | </summary>
        MAV_SENSOR_ROTATION_YAW_180 = 4,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 225 | </summary>
        MAV_SENSOR_ROTATION_YAW_225 = 5,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 270 | </summary>
        MAV_SENSOR_ROTATION_YAW_270 = 6,
        ///<summary> Roll: 0, Pitch: 0, Yaw: 315 | </summary>
        MAV_SENSOR_ROTATION_YAW_315 = 7,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180 = 8,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 45 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_45 = 9,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_90 = 10,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 135 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_135 = 11,
        ///<summary> Roll: 0, Pitch: 180, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_PITCH_180 = 12,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 225 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_225 = 13,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 270 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_270 = 14,
        ///<summary> Roll: 180, Pitch: 0, Yaw: 315 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_YAW_315 = 15,
        ///<summary> Roll: 90, Pitch: 0, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90 = 16,
        ///<summary> Roll: 90, Pitch: 0, Yaw: 45 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_YAW_45 = 17,
        ///<summary> Roll: 90, Pitch: 0, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_YAW_90 = 18,
        ///<summary> Roll: 90, Pitch: 0, Yaw: 135 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_YAW_135 = 19,
        ///<summary> Roll: 270, Pitch: 0, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270 = 20,
        ///<summary> Roll: 270, Pitch: 0, Yaw: 45 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_YAW_45 = 21,
        ///<summary> Roll: 270, Pitch: 0, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_YAW_90 = 22,
        ///<summary> Roll: 270, Pitch: 0, Yaw: 135 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_YAW_135 = 23,
        ///<summary> Roll: 0, Pitch: 90, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_PITCH_90 = 24,
        ///<summary> Roll: 0, Pitch: 270, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_PITCH_270 = 25,
        ///<summary> Roll: 0, Pitch: 180, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_PITCH_180_YAW_90 = 26,
        ///<summary> Roll: 0, Pitch: 180, Yaw: 270 | </summary>
        MAV_SENSOR_ROTATION_PITCH_180_YAW_270 = 27,
        ///<summary> Roll: 90, Pitch: 90, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_PITCH_90 = 28,
        ///<summary> Roll: 180, Pitch: 90, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_PITCH_90 = 29,
        ///<summary> Roll: 270, Pitch: 90, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_PITCH_90 = 30,
        ///<summary> Roll: 90, Pitch: 180, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_PITCH_180 = 31,
        ///<summary> Roll: 270, Pitch: 180, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_PITCH_180 = 32,
        ///<summary> Roll: 90, Pitch: 270, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_PITCH_270 = 33,
        ///<summary> Roll: 180, Pitch: 270, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_180_PITCH_270 = 34,
        ///<summary> Roll: 270, Pitch: 270, Yaw: 0 | </summary>
        MAV_SENSOR_ROTATION_ROLL_270_PITCH_270 = 35,
        ///<summary> Roll: 90, Pitch: 180, Yaw: 90 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_PITCH_180_YAW_90 = 36,
        ///<summary> Roll: 90, Pitch: 0, Yaw: 270 | </summary>
        MAV_SENSOR_ROTATION_ROLL_90_YAW_270 = 37,
        ///<summary> Roll: 315, Pitch: 315, Yaw: 315 | </summary>
        MAV_SENSOR_ROTATION_ROLL_315_PITCH_315_YAW_315 = 38,

    };

    ///<summary> Bitmask of (optional) autopilot capabilities (64 bit). If a bit is set, the autopilot supports this capability. </summary>
    public enum MAV_PROTOCOL_CAPABILITY : ulong
    {
        ///<summary> Autopilot supports MISSION float message type. | </summary>
        MISSION_FLOAT = 1,
        ///<summary> Autopilot supports the new param float message type. | </summary>
        PARAM_FLOAT = 2,
        ///<summary> Autopilot supports MISSION_INT scaled integer message type. | </summary>
        MISSION_INT = 4,
        ///<summary> Autopilot supports COMMAND_INT scaled integer message type. | </summary>
        COMMAND_INT = 8,
        ///<summary> Autopilot supports the new param union message type. | </summary>
        PARAM_UNION = 16,
        ///<summary> Autopilot supports the new FILE_TRANSFER_PROTOCOL message type. | </summary>
        FTP = 32,
        ///<summary> Autopilot supports commanding attitude offboard. | </summary>
        SET_ATTITUDE_TARGET = 64,
        ///<summary> Autopilot supports commanding position and velocity targets in local NED frame. | </summary>
        SET_POSITION_TARGET_LOCAL_NED = 128,
        ///<summary> Autopilot supports commanding position and velocity targets in global scaled integers. | </summary>
        SET_POSITION_TARGET_GLOBAL_INT = 256,
        ///<summary> Autopilot supports terrain protocol / data handling. | </summary>
        TERRAIN = 512,
        ///<summary> Autopilot supports direct actuator control. | </summary>
        SET_ACTUATOR_TARGET = 1024,
        ///<summary> Autopilot supports the flight termination command. | </summary>
        FLIGHT_TERMINATION = 2048,
        ///<summary> Autopilot supports onboard compass calibration. | </summary>
        COMPASS_CALIBRATION = 4096,
        ///<summary> Autopilot supports mavlink version 2. | </summary>
        MAVLINK2 = 8192,
        ///<summary> Autopilot supports mission fence protocol. | </summary>
        MISSION_FENCE = 16384,
        ///<summary> Autopilot supports mission rally point protocol. | </summary>
        MISSION_RALLY = 32768,

    };

    ///<summary> Type of mission items being requested/sent in mission protocol. </summary>
    public enum MAV_MISSION_TYPE : byte
    {
        ///<summary> Items are mission commands for main mission. | </summary>
        MISSION = 0,
        ///<summary> Specifies GeoFence area(s). Items are MAV_CMD_FENCE_ GeoFence items. | </summary>
        FENCE = 1,
        ///<summary> Specifies the rally points for the vehicle. Rally points are alternative RTL points. Items are MAV_CMD_RALLY_POINT rally point items. | </summary>
        RALLY = 2,
        ///<summary> Only used in MISSION_CLEAR_ALL to clear all mission types. | </summary>
        ALL = 255,

    };

    ///<summary> Enumeration of estimator types </summary>
    public enum MAV_ESTIMATOR_TYPE : byte
    {
        ///<summary> This is a naive estimator without any real covariance feedback. | </summary>
        NAIVE = 1,
        ///<summary> Computer vision based estimate. Might be up to scale. | </summary>
        VISION = 2,
        ///<summary> Visual-inertial estimate. | </summary>
        VIO = 3,
        ///<summary> Plain GPS estimate. | </summary>
        GPS = 4,
        ///<summary> Estimator integrating GPS and inertial sensing. | </summary>
        GPS_INS = 5,

    };

    ///<summary> Enumeration of battery types </summary>
    public enum MAV_BATTERY_TYPE : byte
    {
        ///<summary> Not specified. | </summary>
        UNKNOWN = 0,
        ///<summary> Lithium polymer battery | </summary>
        LIPO = 1,
        ///<summary> Lithium-iron-phosphate battery | </summary>
        LIFE = 2,
        ///<summary> Lithium-ION battery | </summary>
        LION = 3,
        ///<summary> Nickel metal hydride battery | </summary>
        NIMH = 4,

    };

    ///<summary> Enumeration of battery functions </summary>
    public enum MAV_BATTERY_FUNCTION : byte
    {
        ///<summary> Battery function is unknown | </summary>
        UNKNOWN = 0,
        ///<summary> Battery supports all flight systems | </summary>
        ALL = 1,
        ///<summary> Battery for the propulsion system | </summary>
        PROPULSION = 2,
        ///<summary> Avionics battery | </summary>
        AVIONICS = 3,
        ///<summary> Payload battery | </summary>
        MAV_BATTERY_TYPE_PAYLOAD = 4,

    };

    ///<summary> Enumeration of VTOL states </summary>
    public enum MAV_VTOL_STATE : byte
    {
        ///<summary> MAV is not configured as VTOL | </summary>
        UNDEFINED = 0,
        ///<summary> VTOL is in transition from multicopter to fixed-wing | </summary>
        TRANSITION_TO_FW = 1,
        ///<summary> VTOL is in transition from fixed-wing to multicopter | </summary>
        TRANSITION_TO_MC = 2,
        ///<summary> VTOL is in multicopter state | </summary>
        MC = 3,
        ///<summary> VTOL is in fixed-wing state | </summary>
        FW = 4,

    };

    ///<summary> Enumeration of landed detector states </summary>
    public enum MAV_LANDED_STATE : byte
    {
        ///<summary> MAV landed state is unknown | </summary>
        UNDEFINED = 0,
        ///<summary> MAV is landed (on ground) | </summary>
        ON_GROUND = 1,
        ///<summary> MAV is in air | </summary>
        IN_AIR = 2,
        ///<summary> MAV currently taking off | </summary>
        TAKEOFF = 3,
        ///<summary> MAV currently landing | </summary>
        LANDING = 4,

    };

    ///<summary> Enumeration of the ADSB altimeter types </summary>
    public enum ADSB_ALTITUDE_TYPE : byte
    {
        ///<summary> Altitude reported from a Baro source using QNH reference | </summary>
        PRESSURE_QNH = 0,
        ///<summary> Altitude reported from a GNSS source | </summary>
        GEOMETRIC = 1,

    };

    ///<summary> ADSB classification for the type of vehicle emitting the transponder signal </summary>
    public enum ADSB_EMITTER_TYPE : byte
    {
        ///<summary>  | </summary>
        NO_INFO = 0,
        ///<summary>  | </summary>
        LIGHT = 1,
        ///<summary>  | </summary>
        SMALL = 2,
        ///<summary>  | </summary>
        LARGE = 3,
        ///<summary>  | </summary>
        HIGH_VORTEX_LARGE = 4,
        ///<summary>  | </summary>
        HEAVY = 5,
        ///<summary>  | </summary>
        HIGHLY_MANUV = 6,
        ///<summary>  | </summary>
        ROTOCRAFT = 7,
        ///<summary>  | </summary>
        UNASSIGNED = 8,
        ///<summary>  | </summary>
        GLIDER = 9,
        ///<summary>  | </summary>
        LIGHTER_AIR = 10,
        ///<summary>  | </summary>
        PARACHUTE = 11,
        ///<summary>  | </summary>
        ULTRA_LIGHT = 12,
        ///<summary>  | </summary>
        UNASSIGNED2 = 13,
        ///<summary>  | </summary>
        UAV = 14,
        ///<summary>  | </summary>
        SPACE = 15,
        ///<summary>  | </summary>
        UNASSGINED3 = 16,
        ///<summary>  | </summary>
        EMERGENCY_SURFACE = 17,
        ///<summary>  | </summary>
        SERVICE_SURFACE = 18,
        ///<summary>  | </summary>
        POINT_OBSTACLE = 19,

    };

    ///<summary> These flags indicate status such as data validity of each data source. Set = data valid </summary>
    public enum ADSB_FLAGS : ushort
    {
        ///<summary>  | </summary>
        VALID_COORDS = 1,
        ///<summary>  | </summary>
        VALID_ALTITUDE = 2,
        ///<summary>  | </summary>
        VALID_HEADING = 4,
        ///<summary>  | </summary>
        VALID_VELOCITY = 8,
        ///<summary>  | </summary>
        VALID_CALLSIGN = 16,
        ///<summary>  | </summary>
        VALID_SQUAWK = 32,
        ///<summary>  | </summary>
        SIMULATED = 64,

    };

    ///<summary> Bitmask of options for the MAV_CMD_DO_REPOSITION </summary>
    public enum MAV_DO_REPOSITION_FLAGS : int /*default*/
    {
        ///<summary> The aircraft should immediately transition into guided. This should not be set for follow me applications | </summary>
        CHANGE_MODE = 1,

    };

    ///<summary> Flags in EKF_STATUS message </summary>
    public enum ESTIMATOR_STATUS_FLAGS : ushort
    {
        ///<summary> True if the attitude estimate is good | </summary>
        ESTIMATOR_ATTITUDE = 1,
        ///<summary> True if the horizontal velocity estimate is good | </summary>
        ESTIMATOR_VELOCITY_HORIZ = 2,
        ///<summary> True if the  vertical velocity estimate is good | </summary>
        ESTIMATOR_VELOCITY_VERT = 4,
        ///<summary> True if the horizontal position (relative) estimate is good | </summary>
        ESTIMATOR_POS_HORIZ_REL = 8,
        ///<summary> True if the horizontal position (absolute) estimate is good | </summary>
        ESTIMATOR_POS_HORIZ_ABS = 16,
        ///<summary> True if the vertical position (absolute) estimate is good | </summary>
        ESTIMATOR_POS_VERT_ABS = 32,
        ///<summary> True if the vertical position (above ground) estimate is good | </summary>
        ESTIMATOR_POS_VERT_AGL = 64,
        ///<summary> True if the EKF is in a constant position mode and is not using external measurements (eg GPS or optical flow) | </summary>
        ESTIMATOR_CONST_POS_MODE = 128,
        ///<summary> True if the EKF has sufficient data to enter a mode that will provide a (relative) position estimate | </summary>
        ESTIMATOR_PRED_POS_HORIZ_REL = 256,
        ///<summary> True if the EKF has sufficient data to enter a mode that will provide a (absolute) position estimate | </summary>
        ESTIMATOR_PRED_POS_HORIZ_ABS = 512,
        ///<summary> True if the EKF has detected a GPS glitch | </summary>
        ESTIMATOR_GPS_GLITCH = 1024,

    };

    ///<summary>  </summary>
    public enum MOTOR_TEST_ORDER : int /*default*/
    {
        ///<summary> default autopilot motor test method | </summary>
        DEFAULT = 0,
        ///<summary> motor numbers are specified as their index in a predefined vehicle-specific sequence | </summary>
        SEQUENCE = 1,
        ///<summary> motor numbers are specified as the output as labeled on the board | </summary>
        BOARD = 2,

    };

    ///<summary>  </summary>
    public enum MOTOR_TEST_THROTTLE_TYPE : int /*default*/
    {
        ///<summary> throttle as a percentage from 0 ~ 100 | </summary>
        MOTOR_TEST_THROTTLE_PERCENT = 0,
        ///<summary> throttle as an absolute PWM value (normally in range of 1000~2000) | </summary>
        MOTOR_TEST_THROTTLE_PWM = 1,
        ///<summary> throttle pass-through from pilot's transmitter | </summary>
        MOTOR_TEST_THROTTLE_PILOT = 2,
        ///<summary> per-motor compass calibration test | </summary>
        MOTOR_TEST_COMPASS_CAL = 3,

    };

    ///<summary>  </summary>
    public enum GPS_INPUT_IGNORE_FLAGS : ushort
    {
        ///<summary> ignore altitude field | </summary>
        GPS_INPUT_IGNORE_FLAG_ALT = 1,
        ///<summary> ignore hdop field | </summary>
        GPS_INPUT_IGNORE_FLAG_HDOP = 2,
        ///<summary> ignore vdop field | </summary>
        GPS_INPUT_IGNORE_FLAG_VDOP = 4,
        ///<summary> ignore horizontal velocity field (vn and ve) | </summary>
        GPS_INPUT_IGNORE_FLAG_VEL_HORIZ = 8,
        ///<summary> ignore vertical velocity field (vd) | </summary>
        GPS_INPUT_IGNORE_FLAG_VEL_VERT = 16,
        ///<summary> ignore speed accuracy field | </summary>
        GPS_INPUT_IGNORE_FLAG_SPEED_ACCURACY = 32,
        ///<summary> ignore horizontal accuracy field | </summary>
        GPS_INPUT_IGNORE_FLAG_HORIZONTAL_ACCURACY = 64,
        ///<summary> ignore vertical accuracy field | </summary>
        GPS_INPUT_IGNORE_FLAG_VERTICAL_ACCURACY = 128,

    };

    ///<summary> Possible actions an aircraft can take to avoid a collision. </summary>
    public enum MAV_COLLISION_ACTION : byte
    {
        ///<summary> Ignore any potential collisions | </summary>
        NONE = 0,
        ///<summary> Report potential collision | </summary>
        REPORT = 1,
        ///<summary> Ascend or Descend to avoid threat | </summary>
        ASCEND_OR_DESCEND = 2,
        ///<summary> Move horizontally to avoid threat | </summary>
        MOVE_HORIZONTALLY = 3,
        ///<summary> Aircraft to move perpendicular to the collision's velocity vector | </summary>
        MOVE_PERPENDICULAR = 4,
        ///<summary> Aircraft to fly directly back to its launch point | </summary>
        RTL = 5,
        ///<summary> Aircraft to stop in place | </summary>
        HOVER = 6,

    };

    ///<summary> Aircraft-rated danger from this threat. </summary>
    public enum MAV_COLLISION_THREAT_LEVEL : byte
    {
        ///<summary> Not a threat | </summary>
        NONE = 0,
        ///<summary> Craft is mildly concerned about this threat | </summary>
        LOW = 1,
        ///<summary> Craft is panicing, and may take actions to avoid threat | </summary>
        HIGH = 2,

    };

    ///<summary> Source of information about this collision. </summary>
    public enum MAV_COLLISION_SRC : byte
    {
        ///<summary> ID field references ADSB_VEHICLE packets | </summary>
        ADSB = 0,
        ///<summary> ID field references MAVLink SRC ID | </summary>
        MAVLINK_GPS_GLOBAL_INT = 1,

    };

    ///<summary> Type of GPS fix </summary>
    public enum GPS_FIX_TYPE : byte
    {
        ///<summary> No GPS connected | </summary>
        NO_GPS = 0,
        ///<summary> No position information, GPS is connected | </summary>
        NO_FIX = 1,
        ///<summary> 2D position | </summary>
        _2D_FIX = 2,
        ///<summary> 3D position | </summary>
        _3D_FIX = 3,
        ///<summary> DGPS/SBAS aided 3D position | </summary>
        DGPS = 4,
        ///<summary> RTK float, 3D position | </summary>
        RTK_FLOAT = 5,
        ///<summary> RTK Fixed, 3D position | </summary>
        RTK_FIXED = 6,
        ///<summary> Static fixed, typically used for base stations | </summary>
        STATIC = 7,

    };


    ///<summary> State flags for ADS-B transponder dynamic report </summary>
    public enum UAVIONIX_ADSB_OUT_DYNAMIC_STATE : ushort
    {
        ///<summary>  | </summary>
        INTENT_CHANGE = 1,
        ///<summary>  | </summary>
        AUTOPILOT_ENABLED = 2,
        ///<summary>  | </summary>
        NICBARO_CROSSCHECKED = 4,
        ///<summary>  | </summary>
        ON_GROUND = 8,
        ///<summary>  | </summary>
        IDENT = 16,

    };

    ///<summary> Transceiver RF control flags for ADS-B transponder dynamic reports </summary>
    public enum UAVIONIX_ADSB_OUT_RF_SELECT : byte
    {
        ///<summary>  | </summary>
        STANDBY = 0,
        ///<summary>  | </summary>
        RX_ENABLED = 1,
        ///<summary>  | </summary>
        TX_ENABLED = 2,

    };

    ///<summary> Status for ADS-B transponder dynamic input </summary>
    public enum UAVIONIX_ADSB_OUT_DYNAMIC_GPS_FIX : byte
    {
        ///<summary>  | </summary>
        NONE_0 = 0,
        ///<summary>  | </summary>
        NONE_1 = 1,
        ///<summary>  | </summary>
        _2D = 2,
        ///<summary>  | </summary>
        _3D = 3,
        ///<summary>  | </summary>
        DGPS = 4,
        ///<summary>  | </summary>
        RTK = 5,

    };

    ///<summary> Status flags for ADS-B transponder dynamic output </summary>
    public enum UAVIONIX_ADSB_RF_HEALTH : byte
    {
        ///<summary>  | </summary>
        INITIALIZING = 0,
        ///<summary>  | </summary>
        OK = 1,
        ///<summary>  | </summary>
        FAIL_TX = 2,
        ///<summary>  | </summary>
        FAIL_RX = 16,

    };

    ///<summary> Definitions for aircraft size </summary>
    public enum UAVIONIX_ADSB_OUT_CFG_AIRCRAFT_SIZE : byte
    {
        ///<summary>  | </summary>
        NO_DATA = 0,
        ///<summary>  | </summary>
        L15M_W23M = 1,
        ///<summary>  | </summary>
        L25M_W28P5M = 2,
        ///<summary>  | </summary>
        L25_34M = 3,
        ///<summary>  | </summary>
        L35_33M = 4,
        ///<summary>  | </summary>
        L35_38M = 5,
        ///<summary>  | </summary>
        L45_39P5M = 6,
        ///<summary>  | </summary>
        L45_45M = 7,
        ///<summary>  | </summary>
        L55_45M = 8,
        ///<summary>  | </summary>
        L55_52M = 9,
        ///<summary>  | </summary>
        L65_59P5M = 10,
        ///<summary>  | </summary>
        L65_67M = 11,
        ///<summary>  | </summary>
        L75_W72P5M = 12,
        ///<summary>  | </summary>
        L75_W80M = 13,
        ///<summary>  | </summary>
        L85_W80M = 14,
        ///<summary>  | </summary>
        L85_W90M = 15,

    };

    ///<summary> GPS lataral offset encoding </summary>
    public enum UAVIONIX_ADSB_OUT_CFG_GPS_OFFSET_LAT : byte
    {
        ///<summary>  | </summary>
        NO_DATA = 0,
        ///<summary>  | </summary>
        LEFT_2M = 1,
        ///<summary>  | </summary>
        LEFT_4M = 2,
        ///<summary>  | </summary>
        LEFT_6M = 3,
        ///<summary>  | </summary>
        RIGHT_0M = 4,
        ///<summary>  | </summary>
        RIGHT_2M = 5,
        ///<summary>  | </summary>
        RIGHT_4M = 6,
        ///<summary>  | </summary>
        RIGHT_6M = 7,

    };

    ///<summary> GPS longitudinal offset encoding </summary>
    public enum UAVIONIX_ADSB_OUT_CFG_GPS_OFFSET_LON : byte
    {
        ///<summary>  | </summary>
        NO_DATA = 0,
        ///<summary>  | </summary>
        APPLIED_BY_SENSOR = 1,

    };

    ///<summary> Emergency status encoding </summary>
    public enum UAVIONIX_ADSB_EMERGENCY_STATUS : byte
    {
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_NO_EMERGENCY = 0,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_GENERAL_EMERGENCY = 1,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_LIFEGUARD_EMERGENCY = 2,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_MINIMUM_FUEL_EMERGENCY = 3,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_NO_COMM_EMERGENCY = 4,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_UNLAWFUL_INTERFERANCE_EMERGENCY = 5,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_DOWNED_AIRCRAFT_EMERGENCY = 6,
        ///<summary>  | </summary>
        UAVIONIX_ADSB_OUT_RESERVED = 7,

    };


#endregion
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class App : Application
    {
        public App()
        {
            // DMPGVarsModel.getInstance().IInitialHeight = DMP.Properties.Settings.Default.InitialHeight; 

        }
    }

}
