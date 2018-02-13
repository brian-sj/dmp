using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MissionPlanner.Mavlink;
using MissionPlanner.Utilities;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Threading;
using MissionPlanner.Comms;
using System.Windows;
using DMP.DataModels;

namespace MissionPlanner
{
    public class MainV2
    {

        private static readonly ILog log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool skipconnectcheck = false;
        private static MainV2 _instance = null;

        public static MainV2 instance {get => _instance !=null ? _instance : new MainV2(); } 

        bool serialThread = false;
        bool pluginthreadrun = false;
        bool joystickthreadrun = false;

        bool readPacketloop = true;
        Thread readPacketThread;


        private DateTime heatbeatSend = DateTime.Now;
        Thread serialreaderthreead;
        Thread pluginthread;

        ManualResetEvent SerialThreadrunner = new ManualResetEvent(false);


        public MainV2()
        {
            _instance = this;
        }

        public static MAVLinkInterface comPort
        {
            get
            {
                return _comPort;
            }
            set
            {
                if (_comPort == value)
                    return;
                _comPort = value;
                _comPort.MavChanged -= instance.comPort_MavChanged;
                _comPort.MavChanged += instance.comPort_MavChanged;
                instance.comPort_MavChanged(null, null);
            }
        }

        static MAVLinkInterface _comPort = new MAVLinkInterface();

        public static List<MAVLinkInterface> Comports = new List<MAVLinkInterface>();

        public GCSViews.FlightPlanner FlightPlanner;

        /// <summary>
        /// other planes in the area from adsb
        /// </summary>
        public object adsblock = new object();

        public ConcurrentDictionary<string, adsb.PointLatLngAltHdg> adsbPlanes = new ConcurrentDictionary<string, adsb.PointLatLngAltHdg>();

        internal void doConnect(MAVLinkInterface comPort, string v1, string v2)
        {
            //throw new NotImplementedException();
            /*
            switch (portname)
            {
                case "preset":
                    break;
                case "TCP":
                    comPort.BaseStream = new TcpSerial();
                    break;
                case "UDP":
                    comPort.BaseStream = new UdpSerial();

                    break;
                case "UDPCl":
                    comPort.BaseStream = new UdpSerialConnect();

                    break;
                case "AUTO":
                // do autoscan
                
                Comms.CommsSerialScan.Scan(true);
                DateTime deadline = DateTime.Now.AddSeconds(50);
                while (Comms.CommsSerialScan.foundport == false)
                {
                    System.Threading.Thread.Sleep(100);

                    if (DateTime.Now > deadline || Comms.CommsSerialScan.run == 0)
                    {
                        CustomMessageBox.Show(Strings.Timeout);
                        _connectionControl.IsConnected(false);
                        return;
                    }
                }
                return;
                
                default:
                    comPort.BaseStream = new SerialPort();
                    break;
            }
            */
            comPort.BaseStream = new SerialPort();
            comPort.MAV.cs.ResetInternals();

            //cleanup any log being played
            comPort.logreadmode = false;
            if (comPort.logplaybackfile != null)
                comPort.logplaybackfile.Close();
            comPort.logplaybackfile = null;
            
            try
            {
                //comPort.BaseStream.BaudRate = 115200;
                //comPort.BaseStream.PortName = "COM3";

                comPort.BaseStream.BaudRate = 57600;
                comPort.BaseStream.PortName = "COM8";
                comPort.Open(false, skipconnectcheck);
            }
            catch (Exception ee)
            {
                Console.WriteLine("error " + ee.Message);
            }

            if (!comPort.BaseStream.IsOpen)
            {
                Console.WriteLine("Connection not opened ");
                return;
            }
            // get all the params
            foreach (var mavstate in MainV2.comPort.MAVlist)
            {
                comPort.sysidcurrent = mavstate.sysid;
                comPort.compidcurrent = mavstate.compid;
                comPort.getParamList();
            }
            connecttime = DateTime.Now;
        }

        /**** ###########  에러로 인해 주석 처리함.  나중에 군무 비행할때 꼭 필요함... ###########*/
        //public event WMDeviceChangeEventHandler DeviceChanged;
        //public delegate void WMDeviceChangeEventHandler(WM_DEVICECHANGE_enum cause);
        //public ConcurrentDictionary<string, adsb.PointLatLngAltHdg> adsbPlanes = new ConcurrentDictionary<string, adsb.PointLatLngAltHdg>();

        string titlebar;

        /// <summary>
        /// Comport name
        /// </summary>
        public static string comPortName = "";

        public static int comPortBaud = 115200;

        /// <summary>
        /// store the time we first connect
        /// </summary>
        DateTime connecttime = DateTime.Now;
        DateTime nodatawarning = DateTime.Now;
        DateTime OpenTime = DateTime.Now;

        /// <summary>
        /// mono detection
        /// </summary>
        public static bool MONO = false;

        /// <summary>
        /// speech engine enable
        /// </summary>
        public static bool speechEnable = false;

        /// <summary>
        /// spech engine static class
        /// </summary>
        public static Speech speechEngine { get; set; }

        /// <summary>
        /// joystick static class
        /// </summary>
        //public static Joystick.Joystick joystick { get; set; }

        /// <summary>
        /// track last joystick packet sent. used to control rate
        /// </summary>
        DateTime lastjoystick = DateTime.Now;

        /// <summary>
        /// determine if we are running sitl
        /// </summary>
        public static bool sitl
        {
            get;
            /*
            get
            {
                if (MissionPlanner.Controls.SITL.SITLSEND == null) return false;
                if (MissionPlanner.Controls.SITL.SITLSEND.Client.Connected) return true;
                return false;
            }
            */
        }
        /// <summary>
        /// hud background image grabber from a video stream - not realy that efficent. ie no hardware overlays etc.
        /// </summary>
        public static WebCamService.Capture cam { get; set; }

        public void comPort_MavChanged(object sender, EventArgs e)
        {
            log.Info("Mav Changed " + MainV2.comPort.MAV.sysid);
            /*
            HUD.Custom.src = MainV2.comPort.MAV.cs;

            CustomWarning.defaultsrc = MainV2.comPort.MAV.cs;

            MissionPlanner.Controls.PreFlight.CheckListItem.defaultsrc = MainV2.comPort.MAV.cs;

            // when uploading a firmware we dont want to reload this screen.
            if (instance.MyView.current.Control != null && instance.MyView.current.Control.GetType() == typeof(GCSViews.InitialSetup))
            {
                var page = ((GCSViews.InitialSetup)instance.MyView.current.Control).backstageView.SelectedPage;
                if (page != null && page.Text == "Install Firmware")
                {
                    return;
                }
            }

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    instance.MyView.Reload();
                });
            }
            else
            {
                instance.MyView.Reload();
            }
            */
        }


        /// <summary>
        /// main serial reader thread
        /// controls
        /// serial reading
        /// link quality stats
        /// speech voltage - custom - alt warning - data lost
        /// heartbeat packet sending
        /// 
        /// and can't fall out
        /// </summary>
        private void SerialReader()
        {
            if (serialThread == true)
                return;
            serialThread = true;

            SerialThreadrunner.Reset();

            int minbytes = 0;

            int altwarningmax = 0;

            bool armedstatus = false;

            string lastmessagehigh = "";

            DateTime speechcustomtime = DateTime.Now;

            DateTime speechlowspeedtime = DateTime.Now;

            DateTime linkqualitytime = DateTime.Now;

            while (serialThread)
            {
                try
                {
                    //Thread.Sleep(1); // 나중에 1로 바꾸어 주어야 한다... 
                    Thread.Sleep(500); // 테스트를 위해서 500으로 바꾸었다. 

                    try
                    {
                        /*
                        if (GCSViews.Terminal.comPort is MAVLinkSerialPort)
                        {
                        }
                        else
                        {
                            if (GCSViews.Terminal.comPort != null && GCSViews.Terminal.comPort.IsOpen)
                                continue;
                        }
                        */
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }

                    // update connect/disconnect button and info stats
                    try
                    {
                        UpdateConnectIcon();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }

                    // 30 seconds interval speech options
                    if (speechEnable && speechEngine != null && (DateTime.Now - speechcustomtime).TotalSeconds > 30 &&
                        (MainV2.comPort.logreadmode || comPort.BaseStream.IsOpen))
                    {
                        if (MainV2.speechEngine.IsReady)
                        {
                            if (Settings.Instance.GetBoolean("speechcustomenabled"))
                            {
                                MainV2.speechEngine.SpeakAsync(Common.speechConversion("" + Settings.Instance["speechcustom"]));
                            }

                            speechcustomtime = DateTime.Now;
                        }

                        // speech for battery alerts
                        //speechbatteryvolt
                        float warnvolt = Settings.Instance.GetFloat("speechbatteryvolt");
                        float warnpercent = Settings.Instance.GetFloat("speechbatterypercent");

                        if (Settings.Instance.GetBoolean("speechbatteryenabled") == true &&
                            MainV2.comPort.MAV.cs.battery_voltage <= warnvolt &&
                            MainV2.comPort.MAV.cs.battery_voltage >= 5.0)
                        {
                            if (MainV2.speechEngine.IsReady)
                            {
                                MainV2.speechEngine.SpeakAsync(Common.speechConversion("" + Settings.Instance["speechbattery"]));
                            }
                        }
                        else if (Settings.Instance.GetBoolean("speechbatteryenabled") == true &&
                                 (MainV2.comPort.MAV.cs.battery_remaining) < warnpercent &&
                                 MainV2.comPort.MAV.cs.battery_voltage >= 5.0 &&
                                 MainV2.comPort.MAV.cs.battery_remaining != 0.0)
                        {
                            if (MainV2.speechEngine.IsReady)
                            {
                                MainV2.speechEngine.SpeakAsync(
                                    Common.speechConversion("" + Settings.Instance["speechbattery"]));
                            }
                        }
                    }

                    // speech for airspeed alerts
                    if (speechEnable && speechEngine != null && (DateTime.Now - speechlowspeedtime).TotalSeconds > 10 &&
                        (MainV2.comPort.logreadmode || comPort.BaseStream.IsOpen))
                    {
                        if (Settings.Instance.GetBoolean("speechlowspeedenabled") == true && MainV2.comPort.MAV.cs.armed)
                        {
                            float warngroundspeed = Settings.Instance.GetFloat("speechlowgroundspeedtrigger");
                            float warnairspeed = Settings.Instance.GetFloat("speechlowairspeedtrigger");

                            if (MainV2.comPort.MAV.cs.airspeed < warnairspeed)
                            {
                                if (MainV2.speechEngine.IsReady)
                                {
                                    MainV2.speechEngine.SpeakAsync(
                                        Common.speechConversion("" + Settings.Instance["speechlowairspeed"]));
                                    speechlowspeedtime = DateTime.Now;
                                }
                            }
                            else if (MainV2.comPort.MAV.cs.groundspeed < warngroundspeed)
                            {
                                if (MainV2.speechEngine.IsReady)
                                {
                                    MainV2.speechEngine.SpeakAsync(
                                        Common.speechConversion("" + Settings.Instance["speechlowgroundspeed"]));
                                    speechlowspeedtime = DateTime.Now;
                                }
                            }
                            else
                            {
                                speechlowspeedtime = DateTime.Now;
                            }
                        }
                    }

                    // speech altitude warning - message high warning
                    if (speechEnable && speechEngine != null &&
                        (MainV2.comPort.logreadmode || comPort.BaseStream.IsOpen))
                    {
                        float warnalt = float.MaxValue;
                        if (Settings.Instance.ContainsKey("speechaltheight"))
                        {
                            warnalt = Settings.Instance.GetFloat("speechaltheight");
                        }
                        try
                        {
                            altwarningmax = (int)Math.Max(MainV2.comPort.MAV.cs.alt, altwarningmax);

                            if (Settings.Instance.GetBoolean("speechaltenabled") == true && MainV2.comPort.MAV.cs.alt != 0.00 &&
                                (MainV2.comPort.MAV.cs.alt <= warnalt) && MainV2.comPort.MAV.cs.armed)
                            {
                                if (altwarningmax > warnalt)
                                {
                                    if (MainV2.speechEngine.IsReady)
                                        MainV2.speechEngine.SpeakAsync(
                                            Common.speechConversion("" + Settings.Instance["speechalt"]));
                                }
                            }
                        }
                        catch
                        {
                        } // silent fail


                        try
                        {
                            // say the latest high priority message
                            if (MainV2.speechEngine.IsReady &&
                                lastmessagehigh != MainV2.comPort.MAV.cs.messageHigh && MainV2.comPort.MAV.cs.messageHigh != null)
                            {
                                if (!MainV2.comPort.MAV.cs.messageHigh.StartsWith("PX4v2 "))
                                {
                                    MainV2.speechEngine.SpeakAsync(MainV2.comPort.MAV.cs.messageHigh);
                                    lastmessagehigh = MainV2.comPort.MAV.cs.messageHigh;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                    // not doing anything
                    if (!MainV2.comPort.logreadmode && !comPort.BaseStream.IsOpen)
                    {
                        altwarningmax = 0;
                    }

                    // attenuate the link qualty over time
                    // linkQuality time 이 1초를 넘지 않았는지 확인한다. 
                    if ((DateTime.Now - MainV2.comPort.MAV.lastvalidpacket).TotalSeconds >= 1)
                    {
                        if (linkqualitytime.Second != DateTime.Now.Second)
                        {
                            MainV2.comPort.MAV.cs.linkqualitygcs = (ushort)(MainV2.comPort.MAV.cs.linkqualitygcs * 0.8f);
                            linkqualitytime = DateTime.Now;

                            // force redraw if there are no other packets are being read
                            //GCSViews.FlightData.myhud.Invalidate();
                            //  화면 계속 바꾸어 주어야한다... 
                        }
                    }

                    // data loss warning - wait min of 10 seconds, ignore first 30 seconds of connect, repeat at 5 seconds interval
                    if ((DateTime.Now - MainV2.comPort.MAV.lastvalidpacket).TotalSeconds > 10
                        && (DateTime.Now - connecttime).TotalSeconds > 30
                        && (DateTime.Now - nodatawarning).TotalSeconds > 5
                        && (MainV2.comPort.logreadmode || comPort.BaseStream.IsOpen)
                        && MainV2.comPort.MAV.cs.armed)
                    {
                        if (speechEnable && speechEngine != null)
                        {
                            if (MainV2.speechEngine.IsReady)
                            {
                                MainV2.speechEngine.SpeakAsync("WARNING No Data for " +
                                                               (int)
                                                                   (DateTime.Now - MainV2.comPort.MAV.lastvalidpacket)
                                                                       .TotalSeconds + " Seconds");
                                nodatawarning = DateTime.Now;
                            }
                        }
                    }

                    // get home point on armed status change.
                    if (armedstatus != MainV2.comPort.MAV.cs.armed && comPort.BaseStream.IsOpen)
                    {
                        armedstatus = MainV2.comPort.MAV.cs.armed;
                        // status just changed to armed
                        if (MainV2.comPort.MAV.cs.armed == true && MainV2.comPort.MAV.aptype != MAVLink.MAV_TYPE.GIMBAL)
                        {
                            System.Threading.ThreadPool.QueueUserWorkItem(state =>
                            {
                                try
                                {
                                    //MainV2.comPort.getHomePosition();
                                    /*
                                    MainV2.comPort.MAV.cs.HomeLocation = new PointLatLngAlt(MainV2.comPort.getWP(0));
                                    if (MyView.current != null && MyView.current.Name == "FlightPlanner")
                                    {
                                        // update home if we are on flight data tab
                                        FlightPlanner.updateHome();
                                    }
                                    */

                                }
                                catch
                                {
                                    // dont hang this loop
                                    Application.Current.Dispatcher.BeginInvoke(new Action
                                        (delegate{
                                                DMP.Dialogs.CustomMessageBox.Show("Failed to update home location (" +
                                                                      MainV2.comPort.MAV.sysid + ")");
                                            }));
                                }
                            });
                        }

                        if (speechEnable && speechEngine != null)
                        {
                            if (Settings.Instance.GetBoolean("speecharmenabled"))
                            {
                                string speech = armedstatus ? Settings.Instance["speecharm"] : Settings.Instance["speechdisarm"];
                                if (!string.IsNullOrEmpty(speech))
                                {
                                    MainV2.speechEngine.SpeakAsync(Common.speechConversion(speech));
                                }
                            }
                        }
                    }

                    // send a hb every seconds from gcs to ap
                    if (heatbeatSend.Second != DateTime.Now.Second)
                    {
                        MAVLink.mavlink_heartbeat_t htb = new MAVLink.mavlink_heartbeat_t()
                        {
                            type = (byte)MAVLink.MAV_TYPE.GCS,
                            autopilot = (byte)MAVLink.MAV_AUTOPILOT.INVALID,
                            mavlink_version = 3 // MAVLink.MAVLINK_VERSION
                        };

                        // enumerate each link
                        foreach (var port in Comports)
                        {
                            if (!port.BaseStream.IsOpen)
                                continue;

                            // poll for params at heartbeat interval - primary mav on this port only
                            if (!port.giveComport)
                            {
                                try
                                {
                                    // poll only when not armed
                                    if (!port.MAV.cs.armed)
                                    {
                                        port.getParamPoll();
                                        port.getParamPoll();
                                    }
                                }
                                catch
                                {
                                }
                            }

                            // there are 3 hb types we can send, mavlink1, mavlink2 signed and unsigned
                            bool sentsigned = false;
                            bool sentmavlink1 = false;
                            bool sentmavlink2 = false;

                            // enumerate each mav
                            foreach (var MAV in port.MAVlist)
                            {
                                try
                                {
                                    // are we talking to a mavlink2 device
                                    if (MAV.mavlinkv2)
                                    {
                                        // is signing enabled
                                        if (MAV.signing)
                                        {
                                            // check if we have already sent
                                            if (sentsigned)
                                                continue;
                                            sentsigned = true;
                                        }
                                        else
                                        {
                                            // check if we have already sent
                                            if (sentmavlink2)
                                                continue;
                                            sentmavlink2 = true;
                                        }
                                    }
                                    else
                                    {
                                        // check if we have already sent
                                        if (sentmavlink1)
                                            continue;
                                        sentmavlink1 = true;
                                    }

                                    port.sendPacket(htb, MAV.sysid, MAV.compid);
                                }
                                catch (Exception ex)
                                {
                                    log.Error(ex);
                                    // close the bad port
                                    try
                                    {
                                        port.Close();
                                    }
                                    catch
                                    {
                                    }
                                    // refresh the screen if needed
                                    if (port == MainV2.comPort)
                                    {
                                        // refresh config window if needed
                                        /*
                                        if (MyView.current != null)
                                        {
                                            this.BeginInvoke((MethodInvoker)delegate ()
                                            {
                                                if (MyView.current.Name == "HWConfig")
                                                    MyView.ShowScreen("HWConfig");
                                                if (MyView.current.Name == "SWConfig")
                                                    MyView.ShowScreen("SWConfig");
                                            });
                                        }
                                        */
                                    }
                                }
                            }
                        }

                        heatbeatSend = DateTime.Now;
                    }

                    // if not connected or busy, sleep and loop
                    if (!comPort.BaseStream.IsOpen || comPort.giveComport == true)
                    {
                        if (!comPort.BaseStream.IsOpen)
                        {
                            // check if other ports are still open
                            foreach (var port in Comports)
                            {
                                if (port.BaseStream.IsOpen)
                                {
                                    Console.WriteLine("Main comport shut, swapping to other mav");
                                    comPort = port;
                                    break;
                                }
                            }
                        }

                        System.Threading.Thread.Sleep(100);
                    }

                    // read the interfaces
                    foreach (var port in Comports.ToArray())
                    {
                        if (!port.BaseStream.IsOpen)
                        {
                            // skip primary interface
                            if (port == comPort)
                                continue;

                            // modify array and drop out
                            Comports.Remove(port);
                            port.Dispose();
                            break;
                        }

                        while (port.BaseStream.IsOpen && port.BaseStream.BytesToRead > minbytes &&
                               port.giveComport == false && serialThread)
                        {
                            try
                            {
                                port.readPacket();
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        }
                        // update currentstate of sysids on the port
                        foreach (var MAV in port.MAVlist)
                        {
                            try
                            {
                                MAV.cs.UpdateCurrentSettings(null, false, port, MAV);
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //Tracking.AddException(e);
                    log.Error("Serial Reader fail :" + e.ToString());
                    try
                    {
                        comPort.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
            }

            Console.WriteLine("SerialReader Done");
            SerialThreadrunner.Set();
        }

        /// <summary>
        /// Used to fix the icon status for unexpected unplugins etc 
        /// </summary>
        DateTime connectButtonUpdate = DateTime.Now;
        private void UpdateConnectIcon()
        {
            if ((DateTime.Now - connectButtonUpdate).Milliseconds > 500)
            {
                //                        Console.WriteLine(DateTime.Now.Millisecond);
                if (comPort.BaseStream.IsOpen)
                {
                    //////############# 여기에 변경된 UI 바꾸어 넣어 주어라. 
/*
                    if ((string)this.MenuConnect.Image.Tag != "Disconnect")
                    {
                        Dispatcher.BeginInvoke((MethodInvoker)delegate
                        {
                            this.MenuConnect.Image = displayicons.disconnect;
                            this.MenuConnect.Image.Tag = "Disconnect";
                            this.MenuConnect.Text = Strings.DISCONNECTc;
                            _connectionControl.IsConnected(true);
                        });
                    }
*/
                }
                else
                {
/*
                    if (this.MenuConnect.Image != null && (string)this.MenuConnect.Image.Tag != "Connect")
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            this.MenuConnect.Image = displayicons.connect;
                            this.MenuConnect.Image.Tag = "Connect";
                            this.MenuConnect.Text = Strings.CONNECTc;
                            _connectionControl.IsConnected(false);
                            if (_connectionStats != null)
                            {
                                _connectionStats.StopUpdates();
                            }
                        });
*/
                    }

                    if (comPort.logreadmode)
                    {
                        //this.BeginInvoke((MethodInvoker)delegate { _connectionControl.IsConnected(true); });
                    }
                }
                connectButtonUpdate = DateTime.Now;
        }

        
        public void readPacketThreadRun()
        {
            readPacketThread = new Thread( readPacketThread_loop);
            readPacketloop = true;


            
            readPacketThread.Start();
        }
        public void readPacketThreadStop()
        {
            readPacketloop = false;
            if (readPacketThread != null)
            {
                readPacketThread.Abort();
            }
            
        }
        private void readPacketThread_loop()
        {
            var inst = GvarDesignModel.Instance;
            var satellite_count = 0;
            while (readPacketloop)
            {
                Thread.Sleep(500);
                MAVLink.MAVLinkMessage msg = MainV2.comPort.readPacket();

                String tomsg = "";
                uint a = msg.msgid;

                /// 비정상 패킷은 걸러 버리자...
                if (msg.Length < 6 || msg.sysid != MainV2.comPort.sysidcurrent || msg.compid != MainV2.comPort.compidcurrent)
                {
                    Console.WriteLine("비정상 패킷"+ msg.Length+":"+msg.sysid);
                    continue;
                }

                //압력과 온도. 
                if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.SCALED_PRESSURE)
                {
                    MAVLink.mavlink_scaled_pressure_t data = msg.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    GvarDesignModel.Instance.FDpressAbs = data.press_abs;
                    GvarDesignModel.Instance.FDtemperature = data.temperature;
                    Console.WriteLine(" 압력:온도" + GvarDesignModel.Instance.FDpressAbs +":"+ GvarDesignModel.Instance.FDtemperature);
                }
                else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.VFR_HUD)
                {
                    MAVLink.mavlink_vfr_hud_t data = msg.ToStructure<MAVLink.mavlink_vfr_hud_t>();
                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                    inst.FDspeed = data.airspeed;
                    inst.FDheading  = data.heading;
                    inst.FDalt = data.alt;
                    inst.FDthroattle = data.throttle;
                    //lblHeading.Text = data.groundspeed.ToString();
                }
                else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.STATUSTEXT)
                {
                    var message = ASCIIEncoding.ASCII.GetString(msg.ToStructure<MAVLink.mavlink_statustext_t>().text);
                    GvarDesignModel.Instance.FDmsg = message;
                    //var pres = mavLinkMessage.ToStructure<MAVLink.mavlink_scaled_pressure_t>();
                }
                else if (msg.msgid == (byte)MAVLink.MAVLINK_MSG_ID.BATTERY_STATUS)
                {
                    MAVLink.mavlink_battery_status_t data = msg.ToStructure<MAVLink.mavlink_battery_status_t>();
                    inst.FDbattery = data.battery_remaining;
                    inst.FDvoltage = Convert.ToInt32( data.voltages);
                }else if ( msg.msgid == (byte) MAVLink.MAVLINK_MSG_ID.GPS_RAW_INT )
                {
                    MAVLink.mavlink_gps_raw_int_t data = msg.ToStructure<MAVLink.mavlink_gps_raw_int_t>();
                    satellite_count = data.satellites_visible;

                    /// GPS 위성이 없다면 그냥... 정보를 보여 주지 말자... 
                    if ( satellite_count > 0 ){
                        inst.FDalt = data.alt;
                        inst.FDLon = data.lon;
                        inst.FDLat = data.lat;
                    }
                    log.InfoFormat("Lat:{0}, Lon:{1} , alt:{2}, satellites {3} ", data.lat , data.lon , data.alt , satellite_count );
                }
                //MainV2.comPort.DebugPacket( msg, ref tomsg );

            }
        }
#region ENUM 
        public enum Firmwares
        {
            ArduPlane,
            ArduCopter2,
            ArduRover,
            ArduSub ,
            Ateryx ,
            ArduTracker,
            Gymbal,
            PX4
        }
#endregion
    }
}
