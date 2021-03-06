﻿using DMP.DataModels.Commands;
using DMP.Resources;
using log4net;
using MissionPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using MissionPlanner.Controls;
using System.Timers;
using System.Threading;

namespace DMP.DataModels
{
    public class CalibrationDataModel : ViewModelBase , IDisposable ,IActivate 
    {
        #region Variables 
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        DispatcherTimer timer1 = new DispatcherTimer();
        /// <summary>
        ///  accel Calib 할때 쓴다...
        /// </summary>
        private byte count = 0;
        private int _mag1_cali_pct = 0;
        private int _mag2_cali_pct = 0;
        private int _mag3_cali_pct = 0;
        private int _accel_cali_pct = 0;
        private int _rc_cali_pct = 0;
        private string _acc_calibrate_msg = "";
        private string _rc_calibrate_msg = "";

        /// <summary>
        ///  이 것은 기체로 부터 날라오는 데이터를 보고 판단한다... 
        ///  화면에 보여 줄라고 Binding 변수로 한번더 선언 했다.... Public Set 은 없다. 
        /// </summary>
        private int _total_compass_count = 0;
        private int _total_compass_calibrate_complete = 0;

        public int Total_Compass_count { get => _total_compass_count; private set { _total_compass_count = value; OnPropertyChanged(); } }
        public int Total_Compass_Calibration_Complete_count { get => _total_compass_calibrate_complete; private set { _total_compass_calibrate_complete = value; OnPropertyChanged(); } }


        public int Mag1_cali_pct { get => _mag1_cali_pct; set { _mag1_cali_pct = value; OnPropertyChanged(); } }
        public int Mag2_cali_pct { get => _mag2_cali_pct; set { _mag2_cali_pct = value; OnPropertyChanged(); } }
        public int Mag3_cali_pct { get => _mag3_cali_pct; set { _mag3_cali_pct = value; OnPropertyChanged(); } }
        public int Accel_cali_pct { get => _accel_cali_pct; set { _accel_cali_pct = value; OnPropertyChanged(); } }
        public int Rc_cali_pct { get => _rc_cali_pct; set { _rc_cali_pct = value; OnPropertyChanged(); } }
        public string AccCalibrateMsg { get => _acc_calibrate_msg; set { _acc_calibrate_msg = value; OnPropertyChanged(); } }
        public string RCCalibrateMsg { get => _rc_calibrate_msg; set { _rc_calibrate_msg = value; OnPropertyChanged(); } }
        #endregion

        #region RC Calibration Variables 복잡하다. 
        //radio  이 변수는 CurrentState Class 의 chNin이 바뀌면 자동으로 바뀌게 되어 있다. 
        // 별도의 set을 하지는 않는다. 
        public static float ch1in { get; set; }
        public static float ch2in { get; set; }
        public static float ch3in { get; set; }
        public static float ch4in { get; set; }
        public static float ch5in { get; set; }
        public static float ch6in { get; set; }
        public static float ch7in { get; set; }
        public static float ch8in { get; set; }
 
        public static float ch9in { get; set; }
        public static float ch10in { get; set; }
        public static float ch11in { get; set; }
        public static float ch12in { get; set; }
        public static float ch13in { get; set; }
        public static float ch14in { get; set; }
        public static float ch15in { get; set; }
        public static float ch16in { get; set; }

        // motors 아직 구현이 않됨.. 
        public float ch1out { get; set; }
        public float ch2out { get; set; }
        public float ch3out { get; set; }
        public float ch4out { get; set; }
        public float ch5out { get; set; }
        public float ch6out { get; set; }
        public float ch7out { get; set; }
        public float ch8out { get; set; }

        public float ch9out { get; set; }
        public float ch10out { get; set; }
        public float ch11out { get; set; }
        public float ch12out { get; set; }
        public float ch13out { get; set; }
        public float ch14out { get; set; }
        public float ch15out { get; set; }
        public float ch16out { get; set; }

        private readonly float[] rcmax = new float[16];
        private readonly float[] rcmin = new float[16];
        private readonly float[] rctrim = new float[16];
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        private int chpitch = -1;
        private int chroll = -1;
        private int chthro = -1;
        private int chyaw = -1;
        private bool run;
        #endregion

        #region  Calibration variables 

        private const int THRESHOLD_OFS_RED = 600;
        private const int THRESHOLD_OFS_YELLOW = 400;
        private bool startup;

        private enum CompassNumber
        {
            Compass1 = 0,
            Compass2,
            Compass3
        };


        #endregion

        #region  Command  Declaration
        public RelayCommand AccelCaliCommand { get; private set; }
        public RelayCommand AccelCaliContinueCommand { get; private set; }
        public RelayCommand AccelCaliCancelCommand { get; private set; }
        
        public RelayCommand CompassCaliCommand { get; private set; }
        public RelayCommand CompassCaliAcceptCommand { get; private set; }
        public RelayCommand CompassCaliCancelCommand { get; private set; }
        public RelayCommand RadioCaliCommand { get; private set; }
        public RelayCommand RebootCommand { get; private set; }

        #endregion

        /// <summary>
        /// 5개의 Command가 있다.. 
        /// </summary>
        public CalibrationDataModel()
        {
            AccelCaliCommand = new RelayCommand(AccelCali, AccelCaliCanUse);
            AccelCaliContinueCommand = new RelayCommand(AccelCaliContinue, AccelCaliContinueCanUse);
            AccelCaliCancelCommand = new RelayCommand(AccelCaliCancel, AccelCaliCancelCanUse);

            CompassCaliCommand = new RelayCommand(CompassCali, CompassCaliCanUse);
            CompassCaliAcceptCommand = new RelayCommand(CompassCaliAccept, CompassCaliAcceptCanUse);
            CompassCaliCancelCommand = new RelayCommand(CompassCaliCancel, CompassCaliCancelCanUse);
            RadioCaliCommand = new RelayCommand(RadioCali, RadioCaliCanUse);
            RebootCommand = new RelayCommand(Reboot, RebootCanUse);

            /// 결과값은 1초에 한번씩만 보면된다. 
            timer1.Interval = TimeSpan.FromMilliseconds(1000);
            timer1.Tick += Timer1_Tick_MagCali_check;

            /// 이게 맞는지 모르겠다. 여튼 페이지가 로딩되면 클라스를 생성하고 그때 _inAccelCali 를 true로 바꾼다.
            _inAccelCali = false;
        }



        public void Dispose()
        {
            /// code에서 dispose()를 Call 해줘야 한다... 
            timer1.Stop();
        }


        public void Activate()
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                //Enabled = false;
                return;
            }
            // Enabled = true;
            startup = true;
        }
        public void DeActivate()
        {
            timer1.Stop();
        }

        private void RCActivate()
        {
            timer.Enabled = true;
            timer.Interval = 100;
            timer.Start();

            if (!MainV2.comPort.MAV.param.ContainsKey("RCMAP_ROLL"))
            {
                chroll = 1;
                chpitch = 2;
                chthro = 3;
                chyaw = 4;
            }
            else
            {
                //setup bindings
                chroll = (int)(float)MainV2.comPort.MAV.param["RCMAP_ROLL"];
                chpitch = (int)(float)MainV2.comPort.MAV.param["RCMAP_PITCH"];
                chthro = (int)(float)MainV2.comPort.MAV.param["RCMAP_THROTTLE"];
                chyaw = (int)(float)MainV2.comPort.MAV.param["RCMAP_YAW"];
            }

        }
        #region  Command  Function and CanUse  
        public void AccelCali(object sender)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            doAccelCalibrate();
        }
        public bool AccelCaliCanUse(object sender)
        {
            return true;
        }

        public void AccelCaliContinue(object sender)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            DoAccCalibContinue();
        }
        private bool CompassCaliCancelCanUse(object obj)
        {
            if (_inCompassCali)
                return true;
            return false;
        }
        private bool AccelCaliCancelCanUse(object obj)
        {
            if (_inAccelCali)
                return true;
            return false;
        }

        private void AccelCaliCancel(object obj)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            doAccelCaliCancel();
        }


        private bool CompassCaliAcceptCanUse(object obj)
        {
            if (_inCompassCali)
                return true;
            return false;
        }

        private void CompassCaliCancel(object obj)
        {
            doCancelCompassCalib();
        }

        private void CompassCaliAccept(object obj)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            doAcceptCompassCalib();
        }

        private bool CompassCaliCanUse(object obj)
        {
            return true;
        }

        private void CompassCali(object obj)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            doCompassCalibrate();
        }

        private void RadioCali(object sender)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
        }
        private bool RadioCaliCanUse(object sender)
        {
            return true;
        }

        private void Reboot(object sender)
        {
            if (!MainV2.comPort.BaseStream.IsOpen)
            {
                Dialogs.CustomMessageBox.Show("기체와 연결이 필요합니다.");
                return;
            }
            var result = Dialogs.CustomMessageBox.Show("기체를 재 부팅 하시겠습니까?", Dialogs.MessageBoxType.ConfirmationWithYesNo);
            if(result == MessageBoxResult.Yes || result == MessageBoxResult.OK )
            {
                MainV2.comPort.doReboot();
            }
            
        }
        private bool RebootCanUse(object sender)
        {
            return true;
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


        #region CompassCalibrate 
        /// <summary>
        /// Compass GPS Calibrate 이거좀 잘 검증해 줘라....   doCompassCalibrate 는될 때까지 루프 돌려야 한다. 
        /// </summary>
        private bool _inCompassCali = false;
        private List<MAVLink.MAVLinkMessage> mprog = new List<MAVLink.MAVLinkMessage>();
        private List<MAVLink.MAVLinkMessage> mrep = new List<MAVLink.MAVLinkMessage>();
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsubCompassProgCali;
        private KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> packetsubCompassRepCali;

        /// <summary>
        /// Loop 돌아가면서 Calibration 됬는지를 확인 한다... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick_MagCali_check(object sender, EventArgs e)
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
                    Total_Compass_count = compasscount;
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
                        _total_compass_calibrate_complete = completecount;
                        _inCompassCali = false;
                        log.InfoFormat(" {0}/{1} completed ", completecount, compasscount);
                        //timer1.Interval = 1000;
                    }
                }
            }

            if (compasscount == completecount && compasscount != 0)
            {

                _inCompassCali = false;
                Dialogs.CustomMessageBox.Show(" Compass Calibration Complete pls reboot the autopilot ");
            }

        }

        /// <summary>
        /// Compass Calibration하라고 명령을 한다... 응답은 ReceivePacketCompassCalib 여기서 받고
        ///                                                 이것을 전체 확인 하는 것은 
        ///                                                 Timer1_Tick_MagCali_check 에서 한다.. 
        /// </summary>
        public void doCompassCalibrate()
        {
            try
            {
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_START_MAG_CAL, 0, 1, 1, 0, 0, 0, 0);
                log.Info("CompassCalibration Start...");
            }
            catch (Exception ex)
            {
                log.Error("#####"+ex);
                Dialogs.CustomMessageBox.Show("Failed to start MAG CAL, check the autopilot is still responding.\n" + ex.ToString(), Strings.ERROR);
                return;
            }

            mprog.Clear();
            mrep.Clear();

            _inCompassCali = true;
            Mag1_cali_pct = 0;
            Mag2_cali_pct = 0;
            Mag3_cali_pct = 0;

            packetsubCompassProgCali = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS, ReceivePacketCompassCalib);
            packetsubCompassRepCali  = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MAG_CAL_REPORT, ReceivePacketCompassCalib);

            timer1.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private bool ReceivePacketCompassCalib(MAVLink.MAVLinkMessage packet)
        {
            log.Debug("MAG_CAL_PROGRESS");
            if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_PROGRESS)
            {
                //var obj = packet.ToStructure<MAVLink.mavlink_mag_cal_progress_t>();
                lock (this.mprog)
                {
                    mprog.Add( packet) ;
                }
                return true;
            }
            else if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.MAG_CAL_REPORT)
            {
                lock (this.mrep)
                {
                    this.mrep.Add(packet);
                }

                return true;
            }
            return true;
        }
        
        /// <summary>
        ///  OK 일때 버튼을 눌러준다. 
        /// </summary>
        private void doAcceptCompassCalib()
        {
            try
            {
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_ACCEPT_MAG_CAL, 0, 0, 1, 0, 0, 0, 0);

            }
            catch (Exception ex)
            {
                Dialogs.CustomMessageBox.Show(ex.ToString(), Strings.ERROR, Dialogs.MessageBoxType.Information);
            }

            MainV2.comPort.UnSubscribeToPacketType(packetsubCompassProgCali);
            MainV2.comPort.UnSubscribeToPacketType(packetsubCompassRepCali);
            _inCompassCali = false;
            timer1.Stop();
        }
        private void doCancelCompassCalib()
        {
            try
            {
              //MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_ACCEPT_MAG_CAL, 0, 0, 1, 0, 0, 0, 0);
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_CANCEL_MAG_CAL, 0, 0, 1, 0, 0, 0, 0);

            }
            catch (Exception ex)
            {
                Dialogs.CustomMessageBox.Show(ex.ToString(), Strings.ERROR, Dialogs.MessageBoxType.Information);
            }

            MainV2.comPort.UnSubscribeToPacketType(packetsubCompassProgCali);
            MainV2.comPort.UnSubscribeToPacketType(packetsubCompassRepCali);
            _inCompassCali = false;
            timer1.Stop();
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
              // 페이지 로딩될때 true 로 바뀌고, 버튼 눌릴때 true로 바뀐다.  
              // false로 바꾸는 것은 receive packet에서 바꾼다... 
            if (_inAccelCali)
            {
                count++;
                try
                {
                    MainV2.comPort.sendPacket(new MAVLink.mavlink_command_ack_t { command = 1, result = count },
                                MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent);

                    log.InfoFormat(" Accel Cali _inAccCali is true send  command_ack_t count :{0}", count);
                    
                }
                catch { Dialogs.CustomMessageBox.Show("Accel Cali에 실패를 했습니다.");
                    return;
                }
                
                return;
            }
            

            try
            {
                count = 0;
                log.Info("Sending Accel Command");
                //Log.Info("Sending accel command (mavlink 1.0)");
                MainV2.comPort.doCommand(MAVLink.MAV_CMD.PREFLIGHT_CALIBRATION, 0, 0, 0, 0, 1, 0, 0);
                packetsubAccelCali = MainV2.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.STATUSTEXT, receivedPacketAccCalib);
                //BUT_calib_accell.Text = Strings.Click_when_Done;
                _inAccelCali = true;
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
        /// 중간에 포기 한다. 
        /// </summary>
        private void doAccelCaliCancel()
        {
            _inAccelCali = false;
            MainV2.comPort.UnSubscribeToPacketType(packetsubAccelCali);
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

                log.DebugFormat( message );

                if (message.ToLower().Contains("place vehicle") || message.ToLower().Contains("calibration"))
                {
                    //Dialogs.CustomMessageBox.Show(message.ToLower(), "OK", Dialogs.MessageBoxType.Information);
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

        /// <summary>
        /// 이 함수는 쓰이지 않는다. 
        /// </summary>
        private void DoAccCalibContinue()
        {
            Dialogs.CustomMessageBox.Show(" 이 함수는 쓰이지 않는 함수이다...  ");
            /*
            count++;
            try
            {
                MainV2.comPort.sendPacket(new MAVLink.mavlink_command_ack_t() { command = 1, result = count }, MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent);
            }
            catch (Exception ex)
            {
                Dialogs.CustomMessageBox.Show(Strings.CommandFailed + ex, Strings.ERROR);
            }
            */
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
            RCActivate();
            /// Cali중인데 한번 더 누르면 종료된다. 
            if (_inRcCalibrate)
            {
                //MainV2
                RCCalibrateMsg = Strings.Done;
                _inRcCalibrate = false;
                return;
            }

            Dialogs.CustomMessageBox.Show("RC Transmitter와 Receiver가 정상적으로 연결되어 있는지 확인 바랍니다.");

            var oldrc = MainV2.comPort.MAV.cs.raterc;
            var oldatt = MainV2.comPort.MAV.cs.rateattitude;
            var oldpos = MainV2.comPort.MAV.cs.rateposition;
            var oldstatus = MainV2.comPort.MAV.cs.ratestatus;

            MainV2.comPort.MAV.cs.raterc = 10;
            MainV2.comPort.MAV.cs.rateattitude = 0;
            MainV2.comPort.MAV.cs.rateposition = 0;
            MainV2.comPort.MAV.cs.ratestatus = 0;

            try
            {
                MainV2.comPort.requestDatastream(MAVLink.MAV_DATA_STREAM.RC_CHANNELS, 10);
            }
            catch
            {
            }

            RCCalibrateMsg = Strings.Click_when_Done;
            Dialogs.CustomMessageBox.Show("OK를 누르기사고 모든 RC 스틱을 자유롭게 움직여 주세요.");


            _inRcCalibrate = true;

            while (run)
            {
                //Application.DoEvents();

                Thread.Sleep(5);

                MainV2.comPort.MAV.cs.UpdateCurrentSettings( );

                // check for non 0 values
                if (MainV2.comPort.MAV.cs.ch1in > 800 && MainV2.comPort.MAV.cs.ch1in < 2200)
                {
                    rcmin[0] = Math.Min(rcmin[0], MainV2.comPort.MAV.cs.ch1in);
                    rcmax[0] = Math.Max(rcmax[0], MainV2.comPort.MAV.cs.ch1in);

                    rcmin[1] = Math.Min(rcmin[1], MainV2.comPort.MAV.cs.ch2in);
                    rcmax[1] = Math.Max(rcmax[1], MainV2.comPort.MAV.cs.ch2in);

                    rcmin[2] = Math.Min(rcmin[2], MainV2.comPort.MAV.cs.ch3in);
                    rcmax[2] = Math.Max(rcmax[2], MainV2.comPort.MAV.cs.ch3in);

                    rcmin[3] = Math.Min(rcmin[3], MainV2.comPort.MAV.cs.ch4in);
                    rcmax[3] = Math.Max(rcmax[3], MainV2.comPort.MAV.cs.ch4in);

                    rcmin[4] = Math.Min(rcmin[4], MainV2.comPort.MAV.cs.ch5in);
                    rcmax[4] = Math.Max(rcmax[4], MainV2.comPort.MAV.cs.ch5in);

                    rcmin[5] = Math.Min(rcmin[5], MainV2.comPort.MAV.cs.ch6in);
                    rcmax[5] = Math.Max(rcmax[5], MainV2.comPort.MAV.cs.ch6in);

                    rcmin[6] = Math.Min(rcmin[6], MainV2.comPort.MAV.cs.ch7in);
                    rcmax[6] = Math.Max(rcmax[6], MainV2.comPort.MAV.cs.ch7in);

                    rcmin[7] = Math.Min(rcmin[7], MainV2.comPort.MAV.cs.ch8in);
                    rcmax[7] = Math.Max(rcmax[7], MainV2.comPort.MAV.cs.ch8in);

                    rcmin[8] = Math.Min(rcmin[8], MainV2.comPort.MAV.cs.ch9in);
                    rcmax[8] = Math.Max(rcmax[8], MainV2.comPort.MAV.cs.ch9in);

                    rcmin[9] = Math.Min(rcmin[9], MainV2.comPort.MAV.cs.ch10in);
                    rcmax[9] = Math.Max(rcmax[9], MainV2.comPort.MAV.cs.ch10in);

                    rcmin[10] = Math.Min(rcmin[10], MainV2.comPort.MAV.cs.ch11in);
                    rcmax[10] = Math.Max(rcmax[10], MainV2.comPort.MAV.cs.ch11in);

                    rcmin[11] = Math.Min(rcmin[11], MainV2.comPort.MAV.cs.ch12in);
                    rcmax[11] = Math.Max(rcmax[11], MainV2.comPort.MAV.cs.ch12in);

                    rcmin[12] = Math.Min(rcmin[12], MainV2.comPort.MAV.cs.ch13in);
                    rcmax[12] = Math.Max(rcmax[12], MainV2.comPort.MAV.cs.ch13in);

                    rcmin[13] = Math.Min(rcmin[13], MainV2.comPort.MAV.cs.ch14in);
                    rcmax[13] = Math.Max(rcmax[13], MainV2.comPort.MAV.cs.ch14in);

                    rcmin[14] = Math.Min(rcmin[14], MainV2.comPort.MAV.cs.ch15in);
                    rcmax[14] = Math.Max(rcmax[14], MainV2.comPort.MAV.cs.ch15in);

                    rcmin[15] = Math.Min(rcmin[15], MainV2.comPort.MAV.cs.ch16in);
                    rcmax[15] = Math.Max(rcmax[15], MainV2.comPort.MAV.cs.ch16in);
                }
            }

            if (rcmin[0] > 800 && rcmin[0] < 2200)
            {
            }
            else
            {
                Dialogs.CustomMessageBox.Show("Bad channel 1 input, canceling");
                return;
            }

        }
        private void RCCalibration_Timer_Tick( object sender , EventArgs e )
        {
            try
            {
                MainV2.comPort.MAV.cs.UpdateCurrentSettings();
            }
            catch(Exception ex) {
                log.ErrorFormat(ex.ToString());
            }
        }
        #endregion
    }
}
