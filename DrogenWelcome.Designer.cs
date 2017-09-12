namespace MissionPlanner
{
    partial class DrogenWelcome
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnGetHomePosition = new System.Windows.Forms.Button();
            this.btngetWayPointCnt = new System.Windows.Forms.Button();
            this.btnSetWP = new System.Windows.Forms.Button();
            this.btnReadPacket = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSysID = new System.Windows.Forms.Label();
            this.lblCompID = new System.Windows.Forms.Label();
            this.btnViewParameters = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtParamValue = new System.Windows.Forms.TextBox();
            this.btnSetParam = new System.Windows.Forms.Button();
            this.btnGetParam = new System.Windows.Forms.Button();
            this.txtParamName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRTL = new System.Windows.Forms.Button();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.btnLand = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnCameraShot = new System.Windows.Forms.Button();
            this.serialPort1 = new MissionPlanner.Comms.SerialPort();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCalcBearing = new System.Windows.Forms.Button();
            this.lblDistance = new System.Windows.Forms.Label();
            this.btnCalcDistance = new System.Windows.Forms.Button();
            this.btnYaw = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAlt = new System.Windows.Forms.Label();
            this.lblGroundSpeed = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblAirSpeed = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblThrottle = new System.Windows.Forms.Label();
            this.btnSetHome = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(1, 1);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1302, 149);
            this.txtLog.TabIndex = 0;
            // 
            // btnGetHomePosition
            // 
            this.btnGetHomePosition.Location = new System.Drawing.Point(57, 157);
            this.btnGetHomePosition.Name = "btnGetHomePosition";
            this.btnGetHomePosition.Size = new System.Drawing.Size(123, 34);
            this.btnGetHomePosition.TabIndex = 1;
            this.btnGetHomePosition.Text = "GetHomePosition";
            this.btnGetHomePosition.UseVisualStyleBackColor = true;
            this.btnGetHomePosition.Click += new System.EventHandler(this.btngetHomePosition_Click);
            // 
            // btngetWayPointCnt
            // 
            this.btngetWayPointCnt.Location = new System.Drawing.Point(186, 157);
            this.btngetWayPointCnt.Name = "btngetWayPointCnt";
            this.btngetWayPointCnt.Size = new System.Drawing.Size(123, 34);
            this.btngetWayPointCnt.TabIndex = 2;
            this.btngetWayPointCnt.Text = "GetWaypointCnt";
            this.btngetWayPointCnt.UseVisualStyleBackColor = true;
            this.btngetWayPointCnt.Click += new System.EventHandler(this.btngetWayPointCnt_Click);
            // 
            // btnSetWP
            // 
            this.btnSetWP.Location = new System.Drawing.Point(316, 157);
            this.btnSetWP.Name = "btnSetWP";
            this.btnSetWP.Size = new System.Drawing.Size(114, 34);
            this.btnSetWP.TabIndex = 3;
            this.btnSetWP.Text = "set Way Point";
            this.btnSetWP.UseVisualStyleBackColor = true;
            this.btnSetWP.Click += new System.EventHandler(this.btnSetWP_Click);
            // 
            // btnReadPacket
            // 
            this.btnReadPacket.Location = new System.Drawing.Point(436, 157);
            this.btnReadPacket.Name = "btnReadPacket";
            this.btnReadPacket.Size = new System.Drawing.Size(114, 34);
            this.btnReadPacket.TabIndex = 4;
            this.btnReadPacket.Text = "ReadPacket";
            this.btnReadPacket.UseVisualStyleBackColor = true;
            this.btnReadPacket.Click += new System.EventHandler(this.btnReadPacket_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(1, 157);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(50, 34);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "연결";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1309, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "SYSID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1309, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "CompID";
            // 
            // lblSysID
            // 
            this.lblSysID.AutoSize = true;
            this.lblSysID.Location = new System.Drawing.Point(1367, 13);
            this.lblSysID.Name = "lblSysID";
            this.lblSysID.Size = new System.Drawing.Size(11, 12);
            this.lblSysID.TabIndex = 8;
            this.lblSysID.Text = "0";
            // 
            // lblCompID
            // 
            this.lblCompID.AutoSize = true;
            this.lblCompID.Location = new System.Drawing.Point(1368, 35);
            this.lblCompID.Name = "lblCompID";
            this.lblCompID.Size = new System.Drawing.Size(11, 12);
            this.lblCompID.TabIndex = 9;
            this.lblCompID.Text = "0";
            // 
            // btnViewParameters
            // 
            this.btnViewParameters.Location = new System.Drawing.Point(556, 157);
            this.btnViewParameters.Name = "btnViewParameters";
            this.btnViewParameters.Size = new System.Drawing.Size(126, 34);
            this.btnViewParameters.TabIndex = 10;
            this.btnViewParameters.Text = "ViewAllParameters";
            this.btnViewParameters.UseVisualStyleBackColor = true;
            this.btnViewParameters.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtParamValue);
            this.groupBox1.Controls.Add(this.btnSetParam);
            this.groupBox1.Controls.Add(this.btnGetParam);
            this.groupBox1.Controls.Add(this.txtParamName);
            this.groupBox1.Location = new System.Drawing.Point(712, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "Param Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-2, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Param Name";
            // 
            // txtParamValue
            // 
            this.txtParamValue.Location = new System.Drawing.Point(113, 41);
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(109, 21);
            this.txtParamValue.TabIndex = 3;
            // 
            // btnSetParam
            // 
            this.btnSetParam.Location = new System.Drawing.Point(113, 71);
            this.btnSetParam.Name = "btnSetParam";
            this.btnSetParam.Size = new System.Drawing.Size(109, 23);
            this.btnSetParam.TabIndex = 2;
            this.btnSetParam.Text = "세팅하기";
            this.btnSetParam.UseVisualStyleBackColor = true;
            this.btnSetParam.Click += new System.EventHandler(this.btnSetParam_Click);
            // 
            // btnGetParam
            // 
            this.btnGetParam.Location = new System.Drawing.Point(0, 71);
            this.btnGetParam.Name = "btnGetParam";
            this.btnGetParam.Size = new System.Drawing.Size(109, 23);
            this.btnGetParam.TabIndex = 1;
            this.btnGetParam.Text = "가져오기";
            this.btnGetParam.UseVisualStyleBackColor = true;
            this.btnGetParam.Click += new System.EventHandler(this.btnGetParam_Click);
            // 
            // txtParamName
            // 
            this.txtParamName.Location = new System.Drawing.Point(0, 43);
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.Size = new System.Drawing.Size(109, 21);
            this.txtParamName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRTL);
            this.groupBox2.Controls.Add(this.btnSetSpeed);
            this.groupBox2.Controls.Add(this.btnLand);
            this.groupBox2.Controls.Add(this.btnReboot);
            this.groupBox2.Controls.Add(this.btnLaunch);
            this.groupBox2.Location = new System.Drawing.Point(1, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 145);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "command";
            // 
            // btnRTL
            // 
            this.btnRTL.Location = new System.Drawing.Point(6, 47);
            this.btnRTL.Name = "btnRTL";
            this.btnRTL.Size = new System.Drawing.Size(309, 23);
            this.btnRTL.TabIndex = 5;
            this.btnRTL.Text = "긴급회항 : MAV_CMD_NAV_RETURN_TO_LAUNCH";
            this.btnRTL.UseVisualStyleBackColor = true;
            this.btnRTL.Click += new System.EventHandler(this.btnRTL_Click);
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.Location = new System.Drawing.Point(11, 117);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(116, 23);
            this.btnSetSpeed.TabIndex = 4;
            this.btnSetSpeed.Text = "1. 속도를 30KM/h";
            this.btnSetSpeed.UseVisualStyleBackColor = true;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // btnLand
            // 
            this.btnLand.Location = new System.Drawing.Point(6, 13);
            this.btnLand.Name = "btnLand";
            this.btnLand.Size = new System.Drawing.Size(212, 28);
            this.btnLand.TabIndex = 2;
            this.btnLand.Text = "Landing : Lng lat 두개를 지정";
            this.btnLand.UseVisualStyleBackColor = true;
            this.btnLand.Click += new System.EventHandler(this.btnLand_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnReboot.Location = new System.Drawing.Point(579, 13);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(84, 28);
            this.btnReboot.TabIndex = 1;
            this.btnReboot.Text = "Reboot";
            this.btnReboot.UseVisualStyleBackColor = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(489, 13);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(84, 28);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnCameraShot
            // 
            this.btnCameraShot.Location = new System.Drawing.Point(712, 274);
            this.btnCameraShot.Name = "btnCameraShot";
            this.btnCameraShot.Size = new System.Drawing.Size(95, 23);
            this.btnCameraShot.TabIndex = 3;
            this.btnCameraShot.Text = "찰칵";
            this.btnCameraShot.UseVisualStyleBackColor = true;
            this.btnCameraShot.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(712, 334);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 205);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnCalcBearing);
            this.tabPage1.Controls.Add(this.lblDistance);
            this.tabPage1.Controls.Add(this.btnCalcDistance);
            this.tabPage1.Controls.Add(this.btnYaw);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(355, 179);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "방향전환";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "00-> 11 각도는?";
            // 
            // btnCalcBearing
            // 
            this.btnCalcBearing.Location = new System.Drawing.Point(7, 67);
            this.btnCalcBearing.Name = "btnCalcBearing";
            this.btnCalcBearing.Size = new System.Drawing.Size(115, 23);
            this.btnCalcBearing.TabIndex = 9;
            this.btnCalcBearing.Text = "한점에서 한점의 각도는";
            this.btnCalcBearing.UseVisualStyleBackColor = true;
            this.btnCalcBearing.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(128, 43);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(121, 12);
            this.lblDistance.TabIndex = 8;
            this.lblDistance.Text = "00-> 11 간의 거리는?";
            // 
            // btnCalcDistance
            // 
            this.btnCalcDistance.Location = new System.Drawing.Point(7, 38);
            this.btnCalcDistance.Name = "btnCalcDistance";
            this.btnCalcDistance.Size = new System.Drawing.Size(115, 23);
            this.btnCalcDistance.TabIndex = 7;
            this.btnCalcDistance.Text = "두점간의 거리를 구해";
            this.btnCalcDistance.UseVisualStyleBackColor = true;
            this.btnCalcDistance.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btnYaw
            // 
            this.btnYaw.Location = new System.Drawing.Point(6, 8);
            this.btnYaw.Name = "btnYaw";
            this.btnYaw.Size = new System.Drawing.Size(116, 23);
            this.btnYaw.TabIndex = 6;
            this.btnYaw.Text = "머리돌려라 YAW";
            this.btnYaw.UseVisualStyleBackColor = true;
            this.btnYaw.Click += new System.EventHandler(this.btnYaw_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(355, 179);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1260, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Yaw (deg)";
            // 
            // lblAlt
            // 
            this.lblAlt.AutoSize = true;
            this.lblAlt.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAlt.Location = new System.Drawing.Point(1063, 179);
            this.lblAlt.Name = "lblAlt";
            this.lblAlt.Size = new System.Drawing.Size(103, 40);
            this.lblAlt.TabIndex = 14;
            this.lblAlt.Text = "0.00";
            // 
            // lblGroundSpeed
            // 
            this.lblGroundSpeed.AutoSize = true;
            this.lblGroundSpeed.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblGroundSpeed.Location = new System.Drawing.Point(1250, 177);
            this.lblGroundSpeed.Name = "lblGroundSpeed";
            this.lblGroundSpeed.Size = new System.Drawing.Size(103, 40);
            this.lblGroundSpeed.TabIndex = 15;
            this.lblGroundSpeed.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(1063, 236);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 40);
            this.label9.TabIndex = 16;
            this.label9.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(1250, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 40);
            this.label10.TabIndex = 17;
            this.label10.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(1063, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 40);
            this.label11.TabIndex = 18;
            this.label11.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(1250, 291);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 40);
            this.label12.TabIndex = 19;
            this.label12.Text = "0.00";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1097, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "altitude";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1260, 167);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "ground speed m/s";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1080, 224);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 12);
            this.label15.TabIndex = 22;
            this.label15.Text = "Dist to WP (m)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1260, 273);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 12);
            this.label16.TabIndex = 23;
            this.label16.Text = "dist to Mav";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1080, 276);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(121, 12);
            this.label17.TabIndex = 24;
            this.label17.Text = "Virtical Speed (m/s)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1313, 376);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 12);
            this.label18.TabIndex = 27;
            this.label18.Text = "Temperature";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1133, 379);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 12);
            this.label19.TabIndex = 28;
            this.label19.Text = "press";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTemp.Location = new System.Drawing.Point(1303, 394);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(103, 40);
            this.lblTemp.TabIndex = 26;
            this.lblTemp.Text = "0.00";
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPress.Location = new System.Drawing.Point(1116, 394);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(103, 40);
            this.lblPress.TabIndex = 25;
            this.lblPress.Text = "0.00";
            // 
            // lblAirSpeed
            // 
            this.lblAirSpeed.AutoSize = true;
            this.lblAirSpeed.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAirSpeed.Location = new System.Drawing.Point(1233, 457);
            this.lblAirSpeed.Name = "lblAirSpeed";
            this.lblAirSpeed.Size = new System.Drawing.Size(36, 14);
            this.lblAirSpeed.TabIndex = 29;
            this.lblAirSpeed.Text = "0.00";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.Location = new System.Drawing.Point(1096, 457);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 14);
            this.label20.TabIndex = 30;
            this.label20.Text = "AirSpeed";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.Location = new System.Drawing.Point(1096, 480);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 14);
            this.label21.TabIndex = 32;
            this.label21.Text = "Heading";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblHeading.Location = new System.Drawing.Point(1233, 480);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(36, 14);
            this.lblHeading.TabIndex = 31;
            this.lblHeading.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(1096, 506);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 34;
            this.label7.Text = "Throttle";
            // 
            // lblThrottle
            // 
            this.lblThrottle.AutoSize = true;
            this.lblThrottle.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblThrottle.Location = new System.Drawing.Point(1233, 506);
            this.lblThrottle.Name = "lblThrottle";
            this.lblThrottle.Size = new System.Drawing.Size(36, 14);
            this.lblThrottle.TabIndex = 33;
            this.lblThrottle.Text = "0.00";
            // 
            // btnSetHome
            // 
            this.btnSetHome.Location = new System.Drawing.Point(57, 192);
            this.btnSetHome.Name = "btnSetHome";
            this.btnSetHome.Size = new System.Drawing.Size(123, 34);
            this.btnSetHome.TabIndex = 35;
            this.btnSetHome.Text = "SET HOME";
            this.btnSetHome.UseVisualStyleBackColor = true;
            this.btnSetHome.Click += new System.EventHandler(this.btnSetHome_Click);
            // 
            // DrogenWelcome
            // 
            this.ClientSize = new System.Drawing.Size(1418, 569);
            this.Controls.Add(this.btnSetHome);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblThrottle);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblAirSpeed);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.lblPress);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblGroundSpeed);
            this.Controls.Add(this.lblAlt);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCameraShot);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnViewParameters);
            this.Controls.Add(this.lblCompID);
            this.Controls.Add(this.lblSysID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnReadPacket);
            this.Controls.Add(this.btnSetWP);
            this.Controls.Add(this.btngetWayPointCnt);
            this.Controls.Add(this.btnGetHomePosition);
            this.Controls.Add(this.txtLog);
            this.Name = "DrogenWelcome";
            this.Load += new System.EventHandler(this.DrogenWelcome_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnGetHomePosition;
        private System.Windows.Forms.Button btngetWayPointCnt;
        private Comms.SerialPort serialPort1;
        private System.Windows.Forms.Button btnSetWP;
        private System.Windows.Forms.Button btnReadPacket;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSysID;
        private System.Windows.Forms.Label lblCompID;
        private System.Windows.Forms.Button btnViewParameters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtParamValue;
        private System.Windows.Forms.Button btnSetParam;
        private System.Windows.Forms.Button btnGetParam;
        private System.Windows.Forms.TextBox txtParamName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLand;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnCameraShot;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.Button btnRTL;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnYaw;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCalcDistance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCalcBearing;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAlt;
        private System.Windows.Forms.Label lblGroundSpeed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblPress;
        private System.Windows.Forms.Label lblAirSpeed;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblThrottle;
        private System.Windows.Forms.Button btnSetHome;
    }
}
