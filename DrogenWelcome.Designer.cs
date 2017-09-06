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
            this.serialPort1 = new MissionPlanner.Comms.SerialPort();
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
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnLand = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.btnLand);
            this.groupBox2.Controls.Add(this.btnReboot);
            this.groupBox2.Controls.Add(this.btnLaunch);
            this.groupBox2.Location = new System.Drawing.Point(1, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 85);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "command";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(6, 13);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(84, 28);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
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
            // btnLand
            // 
            this.btnLand.Location = new System.Drawing.Point(96, 13);
            this.btnLand.Name = "btnLand";
            this.btnLand.Size = new System.Drawing.Size(84, 28);
            this.btnLand.TabIndex = 2;
            this.btnLand.Text = "Landing";
            this.btnLand.UseVisualStyleBackColor = true;
            this.btnLand.Click += new System.EventHandler(this.btnLand_Click);
            // 
            // DrogenWelcome
            // 
            this.ClientSize = new System.Drawing.Size(1418, 323);
            this.Controls.Add(this.groupBox2);
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
    }
}
