namespace HealthCare
{
	partial class InbodyControl
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

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InbodyControl));
			this.gbSchool = new System.Windows.Forms.GroupBox();
			this.gbInbody = new System.Windows.Forms.GroupBox();
			this.btnInbodyComplete = new System.Windows.Forms.Button();
			this.btnSchoolMember = new System.Windows.Forms.Button();
			this.txtBMI = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.dgvMember = new System.Windows.Forms.DataGridView();
			this.lblBMI = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.lblHeight = new System.Windows.Forms.Label();
			this.cbbBan = new System.Windows.Forms.ComboBox();
			this.dgvMemberHistory = new System.Windows.Forms.DataGridView();
			this.lblBan = new System.Windows.Forms.Label();
			this.txtWeight = new System.Windows.Forms.TextBox();
			this.cbbGrade = new System.Windows.Forms.ComboBox();
			this.lblWeigth = new System.Windows.Forms.Label();
			this.lblGrade = new System.Windows.Forms.Label();
			this.lblInbodyPercent = new System.Windows.Forms.Label();
			this.btnCheckInbody = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.dgvSchool = new System.Windows.Forms.DataGridView();
			this.gbSmoke = new System.Windows.Forms.GroupBox();
			this.btnSmokeSchoolMember = new System.Windows.Forms.Button();
			this.dgvSmokeMemberHistory = new System.Windows.Forms.DataGridView();
			this.txtSmokeName = new System.Windows.Forms.TextBox();
			this.btnReuseSmoke = new System.Windows.Forms.Button();
			this.dgvSmokeMember = new System.Windows.Forms.DataGridView();
			this.lblSmokeName = new System.Windows.Forms.Label();
			this.btnComplete = new System.Windows.Forms.Button();
			this.cbbSmokeBan = new System.Windows.Forms.ComboBox();
			this.btnOpenPort = new System.Windows.Forms.Button();
			this.lblSmokeBan = new System.Windows.Forms.Label();
			this.txtCOHD = new System.Windows.Forms.TextBox();
			this.cbbSmokeGrade = new System.Windows.Forms.ComboBox();
			this.txtPPM = new System.Windows.Forms.TextBox();
			this.lblSmokeGrade = new System.Windows.Forms.Label();
			this.lblCOHD = new System.Windows.Forms.Label();
			this.lblPPM = new System.Windows.Forms.Label();
			this.btnSearchSchool = new System.Windows.Forms.Button();
			this.txtSearchSchool = new System.Windows.Forms.TextBox();
			this.lblSchool = new System.Windows.Forms.Label();
			this.axDataCompatiableCtrl = new AxDataCompatiableLib.AxDataCompatiable();
			this.spSmoke = new System.IO.Ports.SerialPort(this.components);
			this.gbSchool.SuspendLayout();
			this.gbInbody.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMember)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvMemberHistory)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvSchool)).BeginInit();
			this.gbSmoke.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSmokeMemberHistory)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvSmokeMember)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axDataCompatiableCtrl)).BeginInit();
			this.SuspendLayout();
			// 
			// gbSchool
			// 
			this.gbSchool.Controls.Add(this.gbInbody);
			this.gbSchool.Controls.Add(this.dgvSchool);
			this.gbSchool.Controls.Add(this.gbSmoke);
			this.gbSchool.Controls.Add(this.btnSearchSchool);
			this.gbSchool.Controls.Add(this.txtSearchSchool);
			this.gbSchool.Controls.Add(this.lblSchool);
			this.gbSchool.Location = new System.Drawing.Point(19, 16);
			this.gbSchool.Name = "gbSchool";
			this.gbSchool.Size = new System.Drawing.Size(1312, 521);
			this.gbSchool.TabIndex = 6;
			this.gbSchool.TabStop = false;
			this.gbSchool.Text = "학교조회";
			// 
			// gbInbody
			// 
			this.gbInbody.Controls.Add(this.btnInbodyComplete);
			this.gbInbody.Controls.Add(this.btnSchoolMember);
			this.gbInbody.Controls.Add(this.txtBMI);
			this.gbInbody.Controls.Add(this.txtName);
			this.gbInbody.Controls.Add(this.dgvMember);
			this.gbInbody.Controls.Add(this.lblBMI);
			this.gbInbody.Controls.Add(this.lblName);
			this.gbInbody.Controls.Add(this.txtHeight);
			this.gbInbody.Controls.Add(this.lblHeight);
			this.gbInbody.Controls.Add(this.cbbBan);
			this.gbInbody.Controls.Add(this.dgvMemberHistory);
			this.gbInbody.Controls.Add(this.lblBan);
			this.gbInbody.Controls.Add(this.txtWeight);
			this.gbInbody.Controls.Add(this.cbbGrade);
			this.gbInbody.Controls.Add(this.lblWeigth);
			this.gbInbody.Controls.Add(this.lblGrade);
			this.gbInbody.Controls.Add(this.lblInbodyPercent);
			this.gbInbody.Controls.Add(this.btnCheckInbody);
			this.gbInbody.Controls.Add(this.progressBar1);
			this.gbInbody.Location = new System.Drawing.Point(31, 122);
			this.gbInbody.Name = "gbInbody";
			this.gbInbody.Size = new System.Drawing.Size(1265, 205);
			this.gbInbody.TabIndex = 7;
			this.gbInbody.TabStop = false;
			this.gbInbody.Text = "인바디 측정";
			// 
			// btnInbodyComplete
			// 
			this.btnInbodyComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnInbodyComplete.Location = new System.Drawing.Point(1117, 171);
			this.btnInbodyComplete.Name = "btnInbodyComplete";
			this.btnInbodyComplete.Size = new System.Drawing.Size(132, 23);
			this.btnInbodyComplete.TabIndex = 21;
			this.btnInbodyComplete.Text = "저장";
			this.btnInbodyComplete.UseVisualStyleBackColor = true;
			this.btnInbodyComplete.Click += new System.EventHandler(this.btnInbodyComplete_Click);
			// 
			// btnSchoolMember
			// 
			this.btnSchoolMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSchoolMember.Location = new System.Drawing.Point(412, 25);
			this.btnSchoolMember.Name = "btnSchoolMember";
			this.btnSchoolMember.Size = new System.Drawing.Size(95, 21);
			this.btnSchoolMember.TabIndex = 20;
			this.btnSchoolMember.Text = "조회";
			this.btnSchoolMember.UseVisualStyleBackColor = true;
			this.btnSchoolMember.Click += new System.EventHandler(this.btnSchoolMember_Click);
			// 
			// txtBMI
			// 
			this.txtBMI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBMI.Enabled = false;
			this.txtBMI.Location = new System.Drawing.Point(991, 173);
			this.txtBMI.MaxLength = 20;
			this.txtBMI.Name = "txtBMI";
			this.txtBMI.Size = new System.Drawing.Size(104, 21);
			this.txtBMI.TabIndex = 18;
			// 
			// txtName
			// 
			this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtName.Location = new System.Drawing.Point(302, 25);
			this.txtName.MaxLength = 20;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(104, 21);
			this.txtName.TabIndex = 19;
			this.txtName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyUp);
			// 
			// dgvMember
			// 
			this.dgvMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMember.Location = new System.Drawing.Point(19, 52);
			this.dgvMember.Name = "dgvMember";
			this.dgvMember.RowTemplate.Height = 23;
			this.dgvMember.Size = new System.Drawing.Size(488, 141);
			this.dgvMember.TabIndex = 11;
			this.dgvMember.SelectionChanged += new System.EventHandler(this.dgvMember_SelectionChanged);
			// 
			// lblBMI
			// 
			this.lblBMI.AutoSize = true;
			this.lblBMI.Location = new System.Drawing.Point(944, 178);
			this.lblBMI.Name = "lblBMI";
			this.lblBMI.Size = new System.Drawing.Size(27, 12);
			this.lblBMI.TabIndex = 19;
			this.lblBMI.Text = "BMI";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(267, 29);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(29, 12);
			this.lblName.TabIndex = 18;
			this.lblName.Text = "이름";
			// 
			// txtHeight
			// 
			this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHeight.Enabled = false;
			this.txtHeight.Location = new System.Drawing.Point(790, 173);
			this.txtHeight.MaxLength = 20;
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(104, 21);
			this.txtHeight.TabIndex = 16;
			// 
			// lblHeight
			// 
			this.lblHeight.AutoSize = true;
			this.lblHeight.Location = new System.Drawing.Point(743, 178);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(29, 12);
			this.lblHeight.TabIndex = 17;
			this.lblHeight.Text = "신장";
			// 
			// cbbBan
			// 
			this.cbbBan.FormattingEnabled = true;
			this.cbbBan.Items.AddRange(new object[] {
            "전체",
            "1반",
            "2반",
            "3반",
            "4반",
            "5반",
            "6반",
            "7반",
            "8반",
            "9반",
            "10반",
            "11반",
            "12반",
            "13반",
            "14반",
            "15반"});
			this.cbbBan.Location = new System.Drawing.Point(179, 26);
			this.cbbBan.Name = "cbbBan";
			this.cbbBan.Size = new System.Drawing.Size(72, 20);
			this.cbbBan.TabIndex = 17;
			// 
			// dgvMemberHistory
			// 
			this.dgvMemberHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvMemberHistory.Location = new System.Drawing.Point(549, 25);
			this.dgvMemberHistory.Name = "dgvMemberHistory";
			this.dgvMemberHistory.RowTemplate.Height = 23;
			this.dgvMemberHistory.Size = new System.Drawing.Size(700, 95);
			this.dgvMemberHistory.TabIndex = 13;
			// 
			// lblBan
			// 
			this.lblBan.AutoSize = true;
			this.lblBan.Location = new System.Drawing.Point(144, 30);
			this.lblBan.Name = "lblBan";
			this.lblBan.Size = new System.Drawing.Size(29, 12);
			this.lblBan.TabIndex = 16;
			this.lblBan.Text = "학급";
			// 
			// txtWeight
			// 
			this.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWeight.Enabled = false;
			this.txtWeight.Location = new System.Drawing.Point(594, 173);
			this.txtWeight.MaxLength = 20;
			this.txtWeight.Name = "txtWeight";
			this.txtWeight.Size = new System.Drawing.Size(104, 21);
			this.txtWeight.TabIndex = 14;
			// 
			// cbbGrade
			// 
			this.cbbGrade.FormattingEnabled = true;
			this.cbbGrade.Items.AddRange(new object[] {
            "1학년"});
			this.cbbGrade.Location = new System.Drawing.Point(52, 26);
			this.cbbGrade.Name = "cbbGrade";
			this.cbbGrade.Size = new System.Drawing.Size(72, 20);
			this.cbbGrade.TabIndex = 15;
			// 
			// lblWeigth
			// 
			this.lblWeigth.AutoSize = true;
			this.lblWeigth.Location = new System.Drawing.Point(547, 178);
			this.lblWeigth.Name = "lblWeigth";
			this.lblWeigth.Size = new System.Drawing.Size(41, 12);
			this.lblWeigth.TabIndex = 15;
			this.lblWeigth.Text = "몸무게";
			// 
			// lblGrade
			// 
			this.lblGrade.AutoSize = true;
			this.lblGrade.Location = new System.Drawing.Point(17, 30);
			this.lblGrade.Name = "lblGrade";
			this.lblGrade.Size = new System.Drawing.Size(29, 12);
			this.lblGrade.TabIndex = 14;
			this.lblGrade.Text = "학년";
			// 
			// lblInbodyPercent
			// 
			this.lblInbodyPercent.AutoSize = true;
			this.lblInbodyPercent.Location = new System.Drawing.Point(1007, 142);
			this.lblInbodyPercent.Name = "lblInbodyPercent";
			this.lblInbodyPercent.Size = new System.Drawing.Size(21, 12);
			this.lblInbodyPercent.TabIndex = 14;
			this.lblInbodyPercent.Text = "0%";
			// 
			// btnCheckInbody
			// 
			this.btnCheckInbody.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCheckInbody.Location = new System.Drawing.Point(1117, 137);
			this.btnCheckInbody.Name = "btnCheckInbody";
			this.btnCheckInbody.Size = new System.Drawing.Size(132, 23);
			this.btnCheckInbody.TabIndex = 14;
			this.btnCheckInbody.Text = "인바디 검사";
			this.btnCheckInbody.UseVisualStyleBackColor = true;
			this.btnCheckInbody.Click += new System.EventHandler(this.btnCheckInbody_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(549, 137);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(438, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// dgvSchool
			// 
			this.dgvSchool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSchool.Location = new System.Drawing.Point(580, 25);
			this.dgvSchool.Name = "dgvSchool";
			this.dgvSchool.RowTemplate.Height = 23;
			this.dgvSchool.Size = new System.Drawing.Size(716, 87);
			this.dgvSchool.TabIndex = 10;
			this.dgvSchool.SelectionChanged += new System.EventHandler(this.dgvSchool_SelectionChanged);
			// 
			// gbSmoke
			// 
			this.gbSmoke.Controls.Add(this.axDataCompatiableCtrl);
			this.gbSmoke.Controls.Add(this.btnSmokeSchoolMember);
			this.gbSmoke.Controls.Add(this.dgvSmokeMemberHistory);
			this.gbSmoke.Controls.Add(this.txtSmokeName);
			this.gbSmoke.Controls.Add(this.btnReuseSmoke);
			this.gbSmoke.Controls.Add(this.dgvSmokeMember);
			this.gbSmoke.Controls.Add(this.lblSmokeName);
			this.gbSmoke.Controls.Add(this.btnComplete);
			this.gbSmoke.Controls.Add(this.cbbSmokeBan);
			this.gbSmoke.Controls.Add(this.btnOpenPort);
			this.gbSmoke.Controls.Add(this.lblSmokeBan);
			this.gbSmoke.Controls.Add(this.txtCOHD);
			this.gbSmoke.Controls.Add(this.cbbSmokeGrade);
			this.gbSmoke.Controls.Add(this.txtPPM);
			this.gbSmoke.Controls.Add(this.lblSmokeGrade);
			this.gbSmoke.Controls.Add(this.lblCOHD);
			this.gbSmoke.Controls.Add(this.lblPPM);
			this.gbSmoke.Location = new System.Drawing.Point(31, 333);
			this.gbSmoke.Name = "gbSmoke";
			this.gbSmoke.Size = new System.Drawing.Size(1265, 176);
			this.gbSmoke.TabIndex = 8;
			this.gbSmoke.TabStop = false;
			this.gbSmoke.Text = "흡연 측정";
			// 
			// btnSmokeSchoolMember
			// 
			this.btnSmokeSchoolMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSmokeSchoolMember.Location = new System.Drawing.Point(412, 14);
			this.btnSmokeSchoolMember.Name = "btnSmokeSchoolMember";
			this.btnSmokeSchoolMember.Size = new System.Drawing.Size(95, 21);
			this.btnSmokeSchoolMember.TabIndex = 29;
			this.btnSmokeSchoolMember.Text = "조회";
			this.btnSmokeSchoolMember.UseVisualStyleBackColor = true;
			this.btnSmokeSchoolMember.Click += new System.EventHandler(this.btnSmokeSchoolMember_Click);
			// 
			// dgvSmokeMemberHistory
			// 
			this.dgvSmokeMemberHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSmokeMemberHistory.Location = new System.Drawing.Point(549, 18);
			this.dgvSmokeMemberHistory.Name = "dgvSmokeMemberHistory";
			this.dgvSmokeMemberHistory.RowTemplate.Height = 23;
			this.dgvSmokeMemberHistory.Size = new System.Drawing.Size(700, 95);
			this.dgvSmokeMemberHistory.TabIndex = 22;
			// 
			// txtSmokeName
			// 
			this.txtSmokeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSmokeName.Location = new System.Drawing.Point(302, 14);
			this.txtSmokeName.MaxLength = 20;
			this.txtSmokeName.Name = "txtSmokeName";
			this.txtSmokeName.Size = new System.Drawing.Size(104, 21);
			this.txtSmokeName.TabIndex = 28;
			// 
			// btnReuseSmoke
			// 
			this.btnReuseSmoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReuseSmoke.Location = new System.Drawing.Point(814, 122);
			this.btnReuseSmoke.Name = "btnReuseSmoke";
			this.btnReuseSmoke.Size = new System.Drawing.Size(95, 48);
			this.btnReuseSmoke.TabIndex = 24;
			this.btnReuseSmoke.Text = "흡연 측정값\r\n재사용하기";
			this.btnReuseSmoke.UseVisualStyleBackColor = true;
			this.btnReuseSmoke.Click += new System.EventHandler(this.btnReuseSmoke_Click);
			// 
			// dgvSmokeMember
			// 
			this.dgvSmokeMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSmokeMember.Location = new System.Drawing.Point(19, 41);
			this.dgvSmokeMember.Name = "dgvSmokeMember";
			this.dgvSmokeMember.RowTemplate.Height = 23;
			this.dgvSmokeMember.Size = new System.Drawing.Size(488, 129);
			this.dgvSmokeMember.TabIndex = 22;
			this.dgvSmokeMember.SelectionChanged += new System.EventHandler(this.dgvSmokeMember_SelectionChanged);
			// 
			// lblSmokeName
			// 
			this.lblSmokeName.AutoSize = true;
			this.lblSmokeName.Location = new System.Drawing.Point(267, 18);
			this.lblSmokeName.Name = "lblSmokeName";
			this.lblSmokeName.Size = new System.Drawing.Size(29, 12);
			this.lblSmokeName.TabIndex = 27;
			this.lblSmokeName.Text = "이름";
			// 
			// btnComplete
			// 
			this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnComplete.Location = new System.Drawing.Point(1117, 122);
			this.btnComplete.Name = "btnComplete";
			this.btnComplete.Size = new System.Drawing.Size(132, 48);
			this.btnComplete.TabIndex = 14;
			this.btnComplete.Text = "저장";
			this.btnComplete.UseVisualStyleBackColor = true;
			this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
			// 
			// cbbSmokeBan
			// 
			this.cbbSmokeBan.FormattingEnabled = true;
			this.cbbSmokeBan.Items.AddRange(new object[] {
            "전체",
            "1반",
            "2반",
            "3반",
            "4반",
            "5반",
            "6반",
            "7반",
            "8반",
            "9반",
            "10반",
            "11반",
            "12반",
            "13반",
            "14반",
            "15반"});
			this.cbbSmokeBan.Location = new System.Drawing.Point(179, 15);
			this.cbbSmokeBan.Name = "cbbSmokeBan";
			this.cbbSmokeBan.Size = new System.Drawing.Size(72, 20);
			this.cbbSmokeBan.TabIndex = 26;
			// 
			// btnOpenPort
			// 
			this.btnOpenPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOpenPort.Location = new System.Drawing.Point(713, 122);
			this.btnOpenPort.Name = "btnOpenPort";
			this.btnOpenPort.Size = new System.Drawing.Size(95, 48);
			this.btnOpenPort.TabIndex = 20;
			this.btnOpenPort.Text = "포트 확인";
			this.btnOpenPort.UseVisualStyleBackColor = true;
			this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
			// 
			// lblSmokeBan
			// 
			this.lblSmokeBan.AutoSize = true;
			this.lblSmokeBan.Location = new System.Drawing.Point(144, 19);
			this.lblSmokeBan.Name = "lblSmokeBan";
			this.lblSmokeBan.Size = new System.Drawing.Size(29, 12);
			this.lblSmokeBan.TabIndex = 25;
			this.lblSmokeBan.Text = "학급";
			// 
			// txtCOHD
			// 
			this.txtCOHD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCOHD.Enabled = false;
			this.txtCOHD.Location = new System.Drawing.Point(600, 149);
			this.txtCOHD.MaxLength = 20;
			this.txtCOHD.Name = "txtCOHD";
			this.txtCOHD.Size = new System.Drawing.Size(105, 21);
			this.txtCOHD.TabIndex = 22;
			// 
			// cbbSmokeGrade
			// 
			this.cbbSmokeGrade.FormattingEnabled = true;
			this.cbbSmokeGrade.Items.AddRange(new object[] {
            "1학년"});
			this.cbbSmokeGrade.Location = new System.Drawing.Point(52, 15);
			this.cbbSmokeGrade.Name = "cbbSmokeGrade";
			this.cbbSmokeGrade.Size = new System.Drawing.Size(72, 20);
			this.cbbSmokeGrade.TabIndex = 24;
			// 
			// txtPPM
			// 
			this.txtPPM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPPM.Enabled = false;
			this.txtPPM.Location = new System.Drawing.Point(600, 122);
			this.txtPPM.MaxLength = 20;
			this.txtPPM.Name = "txtPPM";
			this.txtPPM.Size = new System.Drawing.Size(105, 21);
			this.txtPPM.TabIndex = 20;
			// 
			// lblSmokeGrade
			// 
			this.lblSmokeGrade.AutoSize = true;
			this.lblSmokeGrade.Location = new System.Drawing.Point(17, 19);
			this.lblSmokeGrade.Name = "lblSmokeGrade";
			this.lblSmokeGrade.Size = new System.Drawing.Size(29, 12);
			this.lblSmokeGrade.TabIndex = 23;
			this.lblSmokeGrade.Text = "학년";
			// 
			// lblCOHD
			// 
			this.lblCOHD.AutoSize = true;
			this.lblCOHD.Location = new System.Drawing.Point(547, 153);
			this.lblCOHD.Name = "lblCOHD";
			this.lblCOHD.Size = new System.Drawing.Size(39, 12);
			this.lblCOHD.TabIndex = 23;
			this.lblCOHD.Text = "COHD";
			// 
			// lblPPM
			// 
			this.lblPPM.AutoSize = true;
			this.lblPPM.Location = new System.Drawing.Point(547, 126);
			this.lblPPM.Name = "lblPPM";
			this.lblPPM.Size = new System.Drawing.Size(32, 12);
			this.lblPPM.TabIndex = 21;
			this.lblPPM.Text = "PPM";
			// 
			// btnSearchSchool
			// 
			this.btnSearchSchool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSearchSchool.Location = new System.Drawing.Point(443, 25);
			this.btnSearchSchool.Name = "btnSearchSchool";
			this.btnSearchSchool.Size = new System.Drawing.Size(91, 21);
			this.btnSearchSchool.TabIndex = 9;
			this.btnSearchSchool.Text = "조회";
			this.btnSearchSchool.UseVisualStyleBackColor = true;
			this.btnSearchSchool.Click += new System.EventHandler(this.btnSearchSchool_Click);
			// 
			// txtSearchSchool
			// 
			this.txtSearchSchool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchSchool.Location = new System.Drawing.Point(81, 25);
			this.txtSearchSchool.MaxLength = 20;
			this.txtSearchSchool.Name = "txtSearchSchool";
			this.txtSearchSchool.Size = new System.Drawing.Size(356, 21);
			this.txtSearchSchool.TabIndex = 8;
			this.txtSearchSchool.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchSchool_KeyUp);
			// 
			// lblSchool
			// 
			this.lblSchool.AutoSize = true;
			this.lblSchool.Location = new System.Drawing.Point(29, 28);
			this.lblSchool.Name = "lblSchool";
			this.lblSchool.Size = new System.Drawing.Size(29, 12);
			this.lblSchool.TabIndex = 7;
			this.lblSchool.Text = "학교";
			// 
			// axDataCompatiableCtrl
			// 
			this.axDataCompatiableCtrl.Enabled = true;
			this.axDataCompatiableCtrl.Location = new System.Drawing.Point(513, 0);
			this.axDataCompatiableCtrl.Name = "axDataCompatiableCtrl";
			this.axDataCompatiableCtrl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axDataCompatiableCtrl.OcxState")));
			this.axDataCompatiableCtrl.Size = new System.Drawing.Size(216, 23);
			this.axDataCompatiableCtrl.TabIndex = 21;
			this.axDataCompatiableCtrl.Visible = false;
			this.axDataCompatiableCtrl.EvtRcvData += new AxDataCompatiableLib._DDataCompatiableEvents_EvtRcvDataEventHandler(this.axDataCompatiableCtrl_EvtRcvData);
			// 
			// spSmoke
			// 
			this.spSmoke.BaudRate = 600;
			this.spSmoke.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.spSmoke_PinChanged);
			this.spSmoke.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.spSmoke_DataReceived);
			// 
			// InbodyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.gbSchool);
			this.Name = "InbodyControl";
			this.Size = new System.Drawing.Size(1352, 552);
			this.Load += new System.EventHandler(this.InbodyControl_Load);
			this.gbSchool.ResumeLayout(false);
			this.gbSchool.PerformLayout();
			this.gbInbody.ResumeLayout(false);
			this.gbInbody.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvMember)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvMemberHistory)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvSchool)).EndInit();
			this.gbSmoke.ResumeLayout(false);
			this.gbSmoke.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSmokeMemberHistory)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvSmokeMember)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axDataCompatiableCtrl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbSchool;
		private System.Windows.Forms.Button btnSearchSchool;
		private System.Windows.Forms.TextBox txtSearchSchool;
		private System.Windows.Forms.Label lblSchool;
		private System.Windows.Forms.DataGridView dgvSchool;
		private System.Windows.Forms.DataGridView dgvMemberHistory;
		private System.Windows.Forms.DataGridView dgvMember;
		private System.Windows.Forms.GroupBox gbInbody;
		private System.Windows.Forms.GroupBox gbSmoke;
		private System.Windows.Forms.TextBox txtWeight;
		private System.Windows.Forms.Label lblWeigth;
		private System.Windows.Forms.Label lblInbodyPercent;
		private System.Windows.Forms.Button btnCheckInbody;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox txtBMI;
		private System.Windows.Forms.Label lblBMI;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.Button btnComplete;
		private System.Windows.Forms.TextBox txtCOHD;
		private System.Windows.Forms.TextBox txtPPM;
		private System.Windows.Forms.Label lblCOHD;
		private System.Windows.Forms.Label lblPPM;
		private System.IO.Ports.SerialPort spSmoke;
		private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnReuseSmoke;
		private System.Windows.Forms.Button btnSchoolMember;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.ComboBox cbbBan;
		private System.Windows.Forms.Label lblBan;
		private System.Windows.Forms.ComboBox cbbGrade;
		private System.Windows.Forms.Label lblGrade;
		private AxDataCompatiableLib.AxDataCompatiable axDataCompatiableCtrl;
		private System.Windows.Forms.Button btnInbodyComplete;
		private System.Windows.Forms.DataGridView dgvSmokeMemberHistory;
		private System.Windows.Forms.Button btnSmokeSchoolMember;
		private System.Windows.Forms.TextBox txtSmokeName;
		private System.Windows.Forms.DataGridView dgvSmokeMember;
		private System.Windows.Forms.Label lblSmokeName;
		private System.Windows.Forms.ComboBox cbbSmokeBan;
		private System.Windows.Forms.Label lblSmokeBan;
		private System.Windows.Forms.ComboBox cbbSmokeGrade;
		private System.Windows.Forms.Label lblSmokeGrade;
	}
}
