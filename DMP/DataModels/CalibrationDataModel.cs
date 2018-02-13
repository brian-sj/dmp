using DMP.DataModels.Commands;
using DMP.Resources;
using log4net;
using MissionPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMP.DataModels
{
    public class CalibrationDataModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  accel Calib 할때 쓴다...
        /// </summary>
        private byte count = 0;
        #region  Command  Declaration
        public RelayCommand AccelCaliCommand { get; private set; }
        public RelayCommand AccelCaliContinueCommand { get; private set; }
        #endregion

        public CalibrationDataModel()
        {
            AccelCaliCommand = new RelayCommand(AccelCali, AccelCaliCanUse);
            AccelCaliContinueCommand = new RelayCommand(AccelCaliContinue, AccelCaliContinueCanUse);
        }

        #region  Command  Function and CanUse  
        public void AccelCali(object sender)
        {
            doAccelCalibrate();
        }
        public bool AccelCaliCanUse(object sender)
        {
            if (_inAccelCali)
                return false;
            return true;
        }

        public void AccelCaliContinue(object sender)
        {
            DoAccCalibContinue();
        }
        /// <summary>
        /// Accel Cali Continue는 _inAccelCali 가 true일때 활성화 되며, Cali가 complete, or Fail 될때 끝난다. 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool AccelCaliContinueCanUse(object sender)
        {
            if (_inAccelCali)
                return true;
            else
                return false;
        }


        #endregion

        private int _mag1_cali_pct = 0;
        private int _mag2_cali_pct = 0;
        private int _mag3_cali_pct = 0;
        private int _accel_cali_pct = 0;
        private int _rc_cali_pct = 0;
        private string _acc_calibrate_msg = "";

        public int Mag1_cali_pct { get => _mag1_cali_pct; set { _mag1_cali_pct = value; OnPropertyChanged(); } }
        public int Mag2_cali_pct { get => _mag2_cali_pct; set { _mag2_cali_pct = value; OnPropertyChanged(); } }
        public int Mag3_cali_pct { get => _mag3_cali_pct; set{ _mag3_cali_pct = value; OnPropertyChanged(); } }
        public int Accel_cali_pct { get => _accel_cali_pct; set { _accel_cali_pct = value; OnPropertyChanged(); } }
        public int Rc_cali_pct { get => _rc_cali_pct; set { _rc_cali_pct = value; OnPropertyChanged(); } }
        public string AccCalibrateMsg { get=>_acc_calibrate_msg; set { _acc_calibrate_msg=value; OnPropertyChanged(); }}

        #region CompassCalibrate 
        /// <summary>
        /// Compass GPS Calibrate 이거좀 잘 검증해 줘라....   doCompassCalibrate 는될 때까지 루프 돌려야 한다. 
        /// </summary>
        private List<MAVLink.MAVLinkMessage> mprog = new List<MAVLink.MAVLinkMessage>();
        private List<MAVLink.MAVLinkMessage> mrep = new List<MAVLink.MAVLinkMessage>();
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsubCompassCali;
        public void doCompassCalibrate()
        {
            int compasscount = 0;
            int completecount = 0;

            lock (this.mprog)
            {
                Dictionary<byte, MAVLink.MAVLinkMessage> status = new Dictionary<byte, MAVLink.MAVLinkMessage>();

                //// 보통은 compass가 3개 정도 있다. GPS에도 있고, FC에도 있고, 기타 하나 더 있으면 3개다. 
                foreach (var item in mprog)
                {
                    status[((MAVLink.mavlink_mag_cal_progress_t)item.data).compass_id] = item;
                }

                foreach (var item in status)
                {
                    var obj = (MAVLink.mavlink_mag_cal_progress_t)item.Value.data;
                    // completion_pct == 100이면 완료다. 
                    try
                    {
                        if (item.Key == 0)
                        {
                            Mag1_cali_pct = obj.completion_pct;
                        }
                        if (item.Key == 1)
                        {
                            Mag2_cali_pct = obj.completion_pct;
                        }
                        if (item.Key == 2)
                        {
                            Mag3_cali_pct = obj.completion_pct;
                        }
                    }
                    catch { }
                    compasscount++;
                    log.InfoFormat("compass count detected {0} ", compasscount);
                }
            }

            lock (this.mrep)
            {
                Dictionary<byte, MAVLink.MAVLinkMessage> status = new Dictionary<byte, MAVLink.MAVLinkMessage>();
                foreach (var item in mrep)
                {
                    var obj = (MAVLink.mavlink_mag_cal_report_t)item.data;

                    if (obj.compass_id == 0 && obj.ofs_x == 0)
                        continue;

                    status[obj.compass_id] = item;
                }

                foreach (var item in status.Values)
                {
                    var obj = (MAVLink.mavlink_mag_cal_report_t)item.data;


                    if ((MAVLink.MAG_CAL_STATUS)obj.cal_status != MAVLink.MAG_CAL_STATUS.MAG_CAL_SUCCESS)
                    {
                        //CustomMessageBox.Show(Strings.CommandFailed);
                    }

                    if (obj.autosaved == 1)
                    {
                        completecount++;
                        log.InfoFormat(" {0}/{1} completed ", completecount, compasscount);
                        //timer1.Interval = 1000;
                    }
                }
            }

            if (compasscount == completecount && compasscount != 0)
            {
                Dialogs.CustomMessageBox.Show(" Compass Calibration Complete pls reboot the autopilot ");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private bool ReceivePacketCompassCalib(MAVLink.MAVLinkMessage packet)
        {
            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS)
            {
                var obj = packet.ToStructure<MAVLink.mavlink_mag_cal_progress_t>();

                return true;
            }
            return true;
        }

        #endregion


        #region AccelCalibrate 
        /// <summary>
        /// Accel Calibrate 
        /// </summary>
        private bool _inAccelCali = false;
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsubAccelCali;
        /// <summary>
        /// Accel Calibration을 시작한다. 
        /// </summary>
        public void doAccelCalibrate()
        {
            try
            {
                count = 0;
                _inAccelCali = true;
                //Log.Info("Sending accel command (mavlink 1.0)");
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.PREFLIGHT_CALIBRATION, 0, 0, 0, 0, 1, 0, 0);
                packetsubAccelCali = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.STATUSTEXT, receivedPacketAccCalib);
                //BUT_calib_accell.Text = Strings.Click_when_Done;
                AccCalibrateMsg = Strings.Click_when_Done;
                log.InfoFormat("doAccelCalibrate add receivedPacket");
            }
            catch (Exception)
            {
                _inAccelCali = false;
                // Log.Error("Exception on level", ex);
                Dialogs.CustomMessageBox.Show("Failed to level", Strings.ERROR);
            }
        }

        /// <summary>
        /// Data 받아 오는 부분..
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool receivedPacketAccCalib(MAVLink.MAVLinkMessage arg)
        {
            if (arg.msgid == (uint)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
            {
                var message = ASCIIEncoding.ASCII.GetString(arg.ToStructure<MAVLink.mavlink_statustext_t>().text);

                //UpdateUserMessage(message);
                /*
                Invoke((MethodInvoker)delegate
                {
                    if (message.ToLower().Contains("place vehicle") || message.ToLower().Contains("calibration"))
                        lblAccCalib.Text = message;
                });
                */
                if (message.ToLower().Contains("place vehicle") || message.ToLower().Contains("calibration"))
                {
                    Dialogs.CustomMessageBox.Show(message.ToLower(), "OK", Dialogs.MessageBoxType.Information);
                    AccCalibrateMsg = message; // message 가 계속 바뀜... 
                    log.InfoFormat(message, true);
                }
                
                if (message.ToLower().Contains("calibration successful") ||
                 message.ToLower().Contains("calibration failed"))
                {
                    try
                    {
                        AccCalibrateMsg = Strings.Done;
                        // cali 버튼좀 비활성화 해라...
                        _inAccelCali = false;
                        MainV2.comPort.UnSubscribeToPacketType(packetsubAccelCali);
                        //MainV2.comPort.UnSubscribeToPacketType( MAVLink.MAVLINK_MSG_ID.STATUSTEXT, ReceivedPacketAccCalib );
                    }
                    catch
                    {
                    }
                }
            }
            return true;
        }

        private void DoAccCalibContinue()
        {
            count++;
            try
            {
                MainV2.comPort.sendPacket(new MAVLink.mavlink_command_ack_t() { command = 1, result = count }, MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent);
            }
            catch (Exception ex)
            {
                Dialogs.CustomMessageBox.Show(Strings.CommandFailed + ex, Strings.ERROR);
            }
        }

        #endregion



        #region RCCalibrate 
        /// <summary>
        /// Calibrate
        /// </summary>
        private bool _inRcCalibrate = false;
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsubRCCali;
        public void doRCCalibrate()
        {
            if (_inRcCalibrate)
            {
                //MainV2
            }

        }
        #endregion
    }
}
