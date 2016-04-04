using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace HealthCare
{
    public partial class InbodyControl : UserControl
    {
        private frmMain mainForm = null;
        private AdoUtil adoUtil = null;
        private VoStudent student = null;
        private VoStudent studentSmoke = null;
        private VoInbody inbodyInfo = null;
        private VoSmokeInfo smokeInfo = null;

        private int read = 0;
        private int[] portOutputBuf = new int[10];

        private int iPpm = -1;
        private float fCohb = -1.0f;
        private bool isFirstShowInbodyNotice = true;

        public InbodyControl()
        {
            InitializeComponent();
        }

        private void InbodyControl_Load(object sender, EventArgs e)
        {
            txtPPM.Text = "1";
            txtCOHD.Text = "1";
        }

        public void InitData()
        {
            mainForm = this.ParentForm as frmMain;
            adoUtil = new AdoUtil(mainForm);
            student = new VoStudent();
            studentSmoke = new VoStudent();
            inbodyInfo = new VoInbody();
            smokeInfo = new VoSmokeInfo();

            // 로그인 여부 체크
            if (mainForm._isLogin == false)
            {
                mainForm.btnLogout.Visible = false;
                mainForm.btnLogout.Hide();

                mainForm.LoadLoginControl();

                return;
            }

            InitializeDataGridView();

            // 학교 검색
            GetSearchSchoolInfo(String.Empty);

            // 시리얼 포트 오픈
            OpenSerialPort(false);
        }

        private void InitializeDataGridView()
        {
            DataGridViewColumn column = null;

            InitializeDataGridViewSetting(this.dgvSchool);
            InitializeDataGridViewSetting(this.dgvMember);
            InitializeDataGridViewSetting(this.dgvMemberHistory);
            InitializeDataGridViewSetting(this.dgvSmokeMember);
            InitializeDataGridViewSetting(this.dgvSmokeMemberHistory);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "NO";
            column.HeaderText = "NO";
            column.Width = 60;
            this.dgvSchool.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_name";
            column.HeaderText = "학교명";
            column.Width = 160;
            this.dgvSchool.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "address";
            column.HeaderText = "주소";
            column.Width = 300;
            this.dgvSchool.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_id";
            column.HeaderText = "school_id";
            column.Width = 200;
            column.Visible = false;
            this.dgvSchool.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "NO";
            column.HeaderText = "NO";
            column.Width = 60;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.HeaderText = "학생이름";
            column.Width = 120;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_grade";
            column.HeaderText = "학년";
            column.Width = 60;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_class";
            column.HeaderText = "반";
            column.Width = 60;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "sex";
            column.HeaderText = "성별";
            column.Width = 60;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "member_id";
            column.HeaderText = "member_id";
            column.Width = 60;
            column.Visible = false;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "birth_date";
            column.HeaderText = "생년월일";
            column.Width = 100;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "age";
            column.HeaderText = "age";
            column.Width = 60;
            column.Visible = false;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_id";
            column.HeaderText = "school_id";
            column.Width = 60;
            column.Visible = false;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "gubun2";
            column.HeaderText = "gubun2";
            column.Width = 60;
            column.Visible = false;
            this.dgvMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "measure_date";
            column.HeaderText = "측정일";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.HeaderText = "학생이름";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_name";
            column.HeaderText = "학교명";
            column.Width = 200;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_grade";
            column.HeaderText = "학년";
            column.Width = 60;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_ban";
            column.HeaderText = "반";
            column.Width = 60;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Height";
            column.HeaderText = "키";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Weight";
            column.HeaderText = "몸무게";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BMI";
            column.HeaderText = "BMI";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PPM";
            column.HeaderText = "PPM";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "COHD";
            column.HeaderText = "COHD";
            column.Width = 120;
            this.dgvMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "NO";
            column.HeaderText = "NO";
            column.Width = 60;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.HeaderText = "학생이름";
            column.Width = 120;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_grade";
            column.HeaderText = "학년";
            column.Width = 60;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_class";
            column.HeaderText = "반";
            column.Width = 60;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "sex";
            column.HeaderText = "성별";
            column.Width = 60;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "member_id";
            column.HeaderText = "member_id";
            column.Width = 60;
            column.Visible = false;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "birth_date";
            column.HeaderText = "생년월일";
            column.Width = 100;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "age";
            column.HeaderText = "age";
            column.Width = 60;
            column.Visible = false;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_id";
            column.HeaderText = "school_id";
            column.Width = 60;
            column.Visible = false;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "gubun2";
            column.HeaderText = "gubun2";
            column.Width = 60;
            column.Visible = false;
            this.dgvSmokeMember.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "measure_date";
            column.HeaderText = "측정일";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.HeaderText = "학생이름";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_name";
            column.HeaderText = "학교명";
            column.Width = 200;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_grade";
            column.HeaderText = "학년";
            column.Width = 60;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "school_ban";
            column.HeaderText = "반";
            column.Width = 60;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Height";
            column.HeaderText = "키";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Weight";
            column.HeaderText = "몸무게";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BMI";
            column.HeaderText = "BMI";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PPM";
            column.HeaderText = "PPM";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "COHD";
            column.HeaderText = "COHD";
            column.Width = 120;
            this.dgvSmokeMemberHistory.Columns.Add(column);
        }

        /// <summary>
        /// DataGridView Setting
        /// </summary>
        /// <param name="dgv"></param>
        private void InitializeDataGridViewSetting(DataGridView dgv)
        {
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoGenerateColumns = false;
            dgv.MultiSelect = false;
            dgv.CurrentCell = null;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.ReadOnly = true;
        }

        private void InitSchoolMember(bool is_cho)
        {
            cbbGrade.Items.Clear();
            cbbGrade.Items.Add("전체");
            cbbGrade.Items.Add("1학년");
            cbbGrade.Items.Add("2학년");
            cbbGrade.Items.Add("3학년");

            cbbSmokeGrade.Items.Clear();
            cbbSmokeGrade.Items.Add("전체");
            cbbSmokeGrade.Items.Add("1학년");
            cbbSmokeGrade.Items.Add("2학년");
            cbbSmokeGrade.Items.Add("3학년");

            if (is_cho)
            {
                cbbGrade.Items.Add("4학년");
                cbbGrade.Items.Add("5학년");
                cbbGrade.Items.Add("6학년");
                cbbSmokeGrade.Items.Add("4학년");
                cbbSmokeGrade.Items.Add("5학년");
                cbbSmokeGrade.Items.Add("6학년");
            }

            cbbGrade.SelectedIndex = 0;
            cbbBan.SelectedIndex = 0;
            txtName.Text = "";

            cbbSmokeGrade.SelectedIndex = 0;
            cbbSmokeBan.SelectedIndex = 0;
            txtSmokeName.Text = "";
        }

        /// <summary>
        /// 학교 검색
        /// </summary>
        /// <param name="search_school"></param>
        private void GetSearchSchoolInfo(string search_school)
        {
            dgvSchool.DataSource = null;
            dgvMember.DataSource = null;
            dgvMemberHistory.DataSource = null;
            dgvSmokeMember.DataSource = null;
            dgvSmokeMemberHistory.DataSource = null;

            DataTable dt = adoUtil.SelectSchoolInfo(search_school);

            if (dt != null && dt.Rows.Count > 0)
            {
                // 순번 필드 추가
                dt.Columns.Add("NO", typeof(int));

                int dt_count = dt.Rows.Count;

                for (int i = 1; i <= dt_count; i++)
                {
                    dt.Rows[i - 1]["NO"] = i;
                }

                dgvSchool.DataSource = dt;
            }
            else
            {
                if (mainForm._isError == false)
                {
                    mainForm.ShowNotice("검색된 학교 정보가 없습니다.");
                }
                else
                {
                    mainForm._isError = false;
                }
            }
        }

        /// <summary>
        /// 학교 검색 텍스트 박스 엔터 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchSchool_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSearchSchoolInfo(txtSearchSchool.Text);
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSchoolMember_Click(null, null);
            }
        }

        /// <summary>
        /// 학교 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchSchool_Click(object sender, EventArgs e)
        {
            GetSearchSchoolInfo(txtSearchSchool.Text);
        }

        /// <summary>
        /// 측정완료 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplete_Click(object sender, EventArgs e)
        {
            portOutputBuf = new int[10];
            read = 0;

            iPpm = -1;
            fCohb = -1.0f;

            if (String.IsNullOrWhiteSpace(studentSmoke.ID))
            {
                mainForm.ShowNotice("학생이 선택되지 않았습니다.");
                return;
            }

            if (String.IsNullOrWhiteSpace(this.txtPPM.Text))
            {
                mainForm.ShowNotice("흡연 측정이 진행되지 않았습니다.");
                return;
            }

            bool isSuccess = adoUtil.InsertSmokeMeasureInfo(studentSmoke, smokeInfo);

            if (isSuccess)
            {
                mainForm.ShowNotice("정상적으로 처리되었습니다.");

                txtPPM.Text = "-1";
                txtCOHD.Text = "-1";

                smokeInfo = new VoSmokeInfo();

                // 학생 정보 재조회
                dgvSmokeMember_SelectionChanged(dgvSmokeMember, null);
            }
        }

        private void btnInbodyComplete_Click(object sender, EventArgs e)
        {
            isFirstShowInbodyNotice = true;

            if (String.IsNullOrWhiteSpace(student.ID))
            {
                mainForm.ShowNotice("학생이 선택되지 않았습니다.");
                return;
            }

            // Inbody
            if (String.IsNullOrWhiteSpace(this.txtWeight.Text))
            {
                mainForm.ShowNotice("인바디 측정이 진행되지 않았습니다.");
                return;
            }

            bool isSuccess = adoUtil.InsertInbodyMeasureInfo(student, inbodyInfo);

            if (isSuccess)
            {
                mainForm.ShowNotice("정상적으로 처리되었습니다.");

                progressBar1.Value = 0;
                lblInbodyPercent.Text = "0%";
                txtHeight.Text = "";
                txtWeight.Text = "";
                txtBMI.Text = "";

                inbodyInfo = new VoInbody();

                // 학생 정보 재조회
                dgvMember_SelectionChanged(dgvMember, null);
            }
        }

        /// <summary>
        /// 학교 선택 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSchool_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = dgvSchool.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                dgvMember.DataSource = null;
                dgvMemberHistory.DataSource = null;
                dgvSmokeMember.DataSource = null;
                dgvSmokeMemberHistory.DataSource = null;

                int school_id = Int32.Parse(dgvSchool.Rows[selectedIndex].Cells[3].Value.ToString());
                string school_name = dgvSchool.Rows[selectedIndex].Cells[1].Value.ToString();

                InitSchoolMember((school_name.IndexOf("초등") >= 0) ? true : false);

                GetSchoolMemberList(school_id);
                GetSmokeSchoolMemberList(school_id);
            }
        }

        private void btnSchoolMember_Click(object sender, EventArgs e)
        {
            int selectedIndex = dgvSchool.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                dgvMember.DataSource = null;
                dgvMemberHistory.DataSource = null;

                int school_id = Int32.Parse(dgvSchool.Rows[selectedIndex].Cells[3].Value.ToString());

                GetSchoolMemberList(school_id);
            }
        }

        private void btnSmokeSchoolMember_Click(object sender, EventArgs e)
        {
            int selectedIndex = dgvSchool.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                dgvSmokeMember.DataSource = null;
                dgvSmokeMemberHistory.DataSource = null;

                int school_id = Int32.Parse(dgvSchool.Rows[selectedIndex].Cells[3].Value.ToString());

                GetSmokeSchoolMemberList(school_id);
            }
        }

        private void GetSchoolMemberList(int school_id)
        {
            string grade_value = cbbGrade.SelectedItem.ToString().Replace("학년", "");
            string ban_value = cbbBan.SelectedItem.ToString().Replace("반", "");

            // 학생 정보 조회
            DataTable dt = adoUtil.SelectSchoolMemberInfo(school_id, grade_value, ban_value, txtName.Text);

            if (dt != null && dt.Rows.Count > 0)
            {
                // 순번 필드 추가
                dt.Columns.Add("NO", typeof(int));

                int dt_count = dt.Rows.Count;

                for (int i = 1; i <= dt_count; i++)
                {
                    dt.Rows[i - 1]["NO"] = i;
                }

                dgvMember.DataSource = dt;
            }
            else
            {
                dgvMember.DataSource = null;

                if (mainForm._isError == false)
                {
                    mainForm.ShowNotice("검색된 학생 정보가 없습니다.");
                }
                else
                {
                    mainForm._isError = false;
                }
            }
        }

        private void GetSmokeSchoolMemberList(int school_id)
        {
            string grade_value = cbbSmokeGrade.SelectedItem.ToString().Replace("학년", "");
            string ban_value = cbbSmokeBan.SelectedItem.ToString().Replace("반", "");

            // 학생 정보 조회
            DataTable dt = adoUtil.SelectSchoolMemberInfo(school_id, grade_value, ban_value, txtSmokeName.Text);

            if (dt != null && dt.Rows.Count > 0)
            {
                // 순번 필드 추가
                dt.Columns.Add("NO", typeof(int));

                int dt_count = dt.Rows.Count;

                for (int i = 1; i <= dt_count; i++)
                {
                    dt.Rows[i - 1]["NO"] = i;
                }

                dgvSmokeMember.DataSource = dt;
            }
            else
            {
                dgvSmokeMember.DataSource = null;

                if (mainForm._isError == false)
                {
                    mainForm.ShowNotice("검색된 학생 정보가 없습니다.");
                }
                else
                {
                    mainForm._isError = false;
                }
            }
        }

        /// <summary>
        /// 학생 선택 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMember_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = dgvMember.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                dgvMemberHistory.DataSource = null;

                int member_id = Int32.Parse(dgvMember.Rows[selectedIndex].Cells[5].Value.ToString());
                string last_measure_date = "";
                string last_measure_id = "";

                // 학생 측정 이력 정보 조회
                DataTable dt = adoUtil.SelectSchoolMemberHistoryInfo(member_id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvMemberHistory.DataSource = dt;

                    // 가장 마지막 측정일 정보를 가져온다.
                    last_measure_date = Convert.ToDateTime(dt.Rows[0]["measure_date"].ToString()).ToString("yyyy-MM-dd");
                    last_measure_id = dt.Rows[0]["measure_id"].ToString();
                }
                else
                {
                    dgvMemberHistory.DataSource = null;

                    if (mainForm._isError)
                    {
                        mainForm._isError = false;
                    }
                }

                txtHeight.Text = "";
                txtWeight.Text = "";
                txtBMI.Text = "";

                student.ID = member_id.ToString();
                student.Name = dgvMember.Rows[selectedIndex].Cells[1].Value.ToString();
                student.SchoolID = dgvMember.Rows[selectedIndex].Cells[8].Value.ToString();
                student.Grade = dgvMember.Rows[selectedIndex].Cells[2].Value.ToString();
                student.Grade_ID = GetGradeId(student.Grade, dgvMember.Rows[selectedIndex].Cells[9].Value.ToString());
                student.ClassNumber = dgvMember.Rows[selectedIndex].Cells[3].Value.ToString();
                student.Measure_Date = DateTime.Now.ToString("yyyy-MM-dd");
                student.LastMeasureDate = last_measure_date;
                student.UpdateMeasureSeq = last_measure_id;
            }
        }

        private void dgvSmokeMember_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = dgvSmokeMember.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                dgvSmokeMemberHistory.DataSource = null;

                int member_id = Int32.Parse(dgvSmokeMember.Rows[selectedIndex].Cells[5].Value.ToString());
                string last_measure_date = "";
                string last_measure_id = "";

                // 학생 측정 이력 정보 조회
                DataTable dt = adoUtil.SelectSchoolMemberHistoryInfo(member_id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvSmokeMemberHistory.DataSource = dt;

                    // 가장 마지막 측정일 정보를 가져온다.
                    last_measure_date = Convert.ToDateTime(dt.Rows[0]["measure_date"].ToString()).ToString("yyyy-MM-dd");
                    last_measure_id = dt.Rows[0]["measure_id"].ToString();
                }
                else
                {
                    dgvSmokeMemberHistory.DataSource = null;

                    if (mainForm._isError)
                    {
                        mainForm._isError = false;
                    }
                }

                txtCOHD.Text = "";
                txtPPM.Text = "";

                studentSmoke.ID = member_id.ToString();
                studentSmoke.Name = dgvSmokeMember.Rows[selectedIndex].Cells[1].Value.ToString();
                studentSmoke.SchoolID = dgvSmokeMember.Rows[selectedIndex].Cells[8].Value.ToString();
                studentSmoke.Grade = dgvSmokeMember.Rows[selectedIndex].Cells[2].Value.ToString();
                studentSmoke.Grade_ID = GetGradeId(studentSmoke.Grade, dgvSmokeMember.Rows[selectedIndex].Cells[9].Value.ToString());
                studentSmoke.ClassNumber = dgvSmokeMember.Rows[selectedIndex].Cells[3].Value.ToString();
                studentSmoke.Measure_Date = DateTime.Now.ToString("yyyy-MM-dd");
                studentSmoke.LastMeasureDate = last_measure_date;
                studentSmoke.UpdateMeasureSeq = last_measure_id;
            }
        }

        /// <summary>
        /// 포트 오픈 클릭 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            OpenSerialPort(true);
        }

        /// <summary>
        /// 시리얼 포트 오픈
        /// </summary>
        /// <param name="isClickButton"></param>
        private void OpenSerialPort(bool isClickButton)
        {
            iPpm = -1;
            fCohb = -1.0f;

            if (spSmoke.IsOpen)
            {
                spSmoke.Close();
            }

            spSmoke.PortName = "COM2";

            txtPPM.Text = iPpm.ToString();
            txtCOHD.Text = fCohb.ToString();

            try
            {
                spSmoke.Open();
                //btnOpenPort.Visible = false;
            }
            catch
            {
                if (isClickButton)
                {
                    mainForm.ShowNotice("시리얼 포트 " + spSmoke.PortName + " 를 열 수 없습니다.\r\n연결 상태를 확인하세요.");
                }
                else
                {
                    btnOpenPort.Visible = true;
                }
            }
        }

        /// <summary>
        /// 시리얼 포트를 통해 데이터 전송
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spSmoke_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            while (sp.BytesToRead > 0)
            {
                portOutputBuf[read++] = sp.ReadByte();
            }

            if (read == 5)
            {
                byte low = (byte)portOutputBuf[2];
                byte high = (byte)portOutputBuf[3];

                // Data 변화 처리
                int value = high;
                value = value << 8;
                value += low;

                float fCohb = value * 0.01f;
                int iPpm = (int)portOutputBuf[1];

                if (fCohb > this.fCohb || iPpm > this.iPpm)
                {
                    if (fCohb > this.fCohb)
                        this.fCohb = fCohb;

                    if (iPpm > this.iPpm)
                        this.iPpm = iPpm;

                    this.Invoke(new EventHandler(DisplayPPMCOHD));
                }

                portOutputBuf = new int[10];
                read = 0;
            }
        }

        /// <summary>
        /// 흡연 측정 결과 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayPPMCOHD(object sender, EventArgs e)
        {
            txtPPM.Text = this.iPpm.ToString();
            txtCOHD.Text = this.fCohb.ToString();

            smokeInfo.PPM = (txtPPM.Text == "-1") ? "" : txtPPM.Text;
            smokeInfo.COHD = (txtCOHD.Text == "-1") ? "" : txtCOHD.Text;
        }

        /// <summary>
        /// Check Inbody 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckInbody_Click(object sender, EventArgs e)
        {
            int selectedIndex = dgvMember.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                if (String.IsNullOrWhiteSpace(this.dgvMember.Rows[selectedIndex].Cells[6].Value.ToString()))
                {
                    mainForm.ShowNotice("선택된 학생에 대한 생년월일 정보가 없기에, 오류가 발생할 수 있습니다.", 500);
                }

                // Send Profile Data
                // Profile Format [ ID + $ " + NAME + "$" + BirthDay + "$" + Age + "$" + Height + "$" + Sex(M or F) + "$" + "" + "$" +""+ "$"]
                String userInfo = this.dgvMember.Rows[selectedIndex].Cells[5].Value.ToString() + "$"
                   + this.dgvMember.Rows[selectedIndex].Cells[1].Value.ToString() + "$"
                   + this.dgvMember.Rows[selectedIndex].Cells[6].Value.ToString() + "$"
                   + this.dgvMember.Rows[selectedIndex].Cells[7].Value.ToString() + "$"
                   + "177" + "$"
                   + this.dgvMember.Rows[selectedIndex].Cells[4].Value.ToString() + "$"
                   + "Human" + "$"
                   + "0" + "$";

                // Handle을 통해서 
                axDataCompatiableCtrl.InitCom(this.Handle.ToString());
                axDataCompatiableCtrl.DataSend("110", userInfo);
            }
            else
            {
                mainForm.ShowNotice("선택된 학생 정보가 없습니다.");
            }
        }

        private void axDataCompatiableCtrl_EvtRcvData(object sender, AxDataCompatiableLib._DDataCompatiableEvents_EvtRcvDataEvent e)
        {
            //Console.WriteLine("[{0}] e.nFlag:{1} e.strData:{2}",
            //	  DateTime.Now.ToString("hh:mm:ss.fff"),
            //	  e.nFlag.ToString(),
            //	  e.strData.ToString());

            ///
            /// Recived InBody Data
            ///  
            switch (e.nFlag)
            {
                case "720":
                    // InBody 720					
                    InBody720_520_Data(e.strData);
                    break;
                case "520":
                    // InBody 520
                    InBody720_520_Data(e.strData);
                    break;
                case "370":
                    // InBody 370
                    this.InBody370_Data(e.strData);
                    break;
                case "10":
                    // InBody J10
                    InBodyJ10Data(e.strData);
                    break;
                case "5":
                    // InBody J05
                    this.InBodyJ05Data(e.strData);
                    break;
                case "330":
                    // InBody 330
                    InBody330_Data(e.strData);
                    break;
                case "320":
                    // InBody 330
                    InBody320_Data(e.strData);
                    break;
                case "230":
                    // InBody 230
                    this.InBody230_Data(e.strData);
                    break;
                case "220":
                    // InBody 220
                    InBody220_Data(e.strData);
                    break;
                case "430":
                    // InBody 430
                    this.InBody430_Data(e.strData);
                    break;
                // 상태코드 확인 : log 상 STATUS
                case "1000":
                    this.lblInbodyPercent.Text = e.strData.ToString() + "%";

                    if (e.strData == "SU" && progressBar1.Value == 90)
                    {
                        progressBar1.Value = 100;
                        lblInbodyPercent.Text = "100%";

                        mainForm.ShowNotice("측정을 완료하였습니다.", 500);
                    }
                    else if (e.strData == "ST")
                    {
                        if (isFirstShowInbodyNotice)
                        {
                            mainForm.ShowNotice("측정을 시작합니다.", 500);
                            isFirstShowInbodyNotice = false;

                            lblInbodyPercent.Text = "0%";
                        }
                    }
                    else if (e.strData == "SF")
                    {
                        mainForm.ShowNotice("인바디 측정 중 통신 오류가 발생하였습니다.");

                        lblInbodyPercent.Text = "";
                    }

                    break;
                // 측정 진행율 전송 처리
                case "1100":
                    this.lblInbodyPercent.Text = e.strData.ToString() + "%";
                    progressBar1.Value = Convert.ToInt32(e.strData);
                    break;
                default:
                    //MessageBox.Show(e.nFlag);
                    //MessageBox.Show(e.strData.ToString());
                    break;
            }
        }

        #region ParsingInBodyData

        /// <summary>
        /// InbodyJ10 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBodyJ10Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 300; i++)
            {
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);

                if (intPos2 > 0)
                {
                    if (i > 2)
                    {
                        m_InbodyData[i - 1] = "0";
                        m_InbodyData[i - 1] = strData.Substring(intPos1, intPos2 - intPos1);
                    }
                    else
                    {
                        m_InbodyData[i] = "0";
                        m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);
                    }
                }
            }

            string Weight = String.Format("{0:F2}", float.Parse(m_InbodyData[1]));
            string Height = String.Format("{0:F2}", float.Parse(m_InbodyData[189]));
            string BMI = String.Format("{0:F2}", float.Parse(m_InbodyData[24]));

            inbodyInfo.DateTimes = m_InbodyData[0];
            inbodyInfo.Weight = m_InbodyData[1];
            inbodyInfo.Protein_Mass = m_InbodyData[4];
            inbodyInfo.Mineral_Mass = m_InbodyData[5];
            inbodyInfo.Body_Fat_Mass = m_InbodyData[6];
            inbodyInfo.Total_Body_Water = m_InbodyData[7];
            inbodyInfo.Soft_Lean_Mass = m_InbodyData[8];
            inbodyInfo.Fat_Free_Mass = m_InbodyData[9];
            inbodyInfo.OSSEOUS = m_InbodyData[10];
            inbodyInfo.Skeletal_Muscle_Mass = m_InbodyData[20];
            inbodyInfo.BMI = m_InbodyData[24];
            inbodyInfo.Percent_Body_Fat = m_InbodyData[26];
            inbodyInfo.Waist_Hip_Ratio = m_InbodyData[27];
            inbodyInfo.Target_Weight = m_InbodyData[60];
            inbodyInfo.Weight_Control = m_InbodyData[61];
            inbodyInfo.Fat_Control = m_InbodyData[62];
            inbodyInfo.Muscle_Control = m_InbodyData[63];
            inbodyInfo.Fitness_Score = m_InbodyData[64];
            inbodyInfo.BMR = m_InbodyData[66];
            inbodyInfo.Neck = m_InbodyData[68];
            inbodyInfo.Chest = m_InbodyData[69];
            inbodyInfo.ABD = m_InbodyData[70];
            inbodyInfo.THIGHL = m_InbodyData[71];
            inbodyInfo.ACL = m_InbodyData[72];
            inbodyInfo.HIP = m_InbodyData[73];
            inbodyInfo.THIGHR = m_InbodyData[74];
            inbodyInfo.ACR = m_InbodyData[77];
            inbodyInfo.Protein_Max = m_InbodyData[139];
            inbodyInfo.Protein_Min = m_InbodyData[140];
            inbodyInfo.Mineral_Max = m_InbodyData[141];
            inbodyInfo.Mineral_Min = m_InbodyData[142];
            inbodyInfo.Body_Fat_Mass_Max = m_InbodyData[143];
            inbodyInfo.Body_Fat_Mass_Min = m_InbodyData[144];
            inbodyInfo.Weight_Max = m_InbodyData[145];
            inbodyInfo.Weight_Min = m_InbodyData[146];
            inbodyInfo.Skeletal_Muscle_Mass_Max = m_InbodyData[147];
            inbodyInfo.Skeletal_Muscle_Mass_Min = m_InbodyData[148];
            inbodyInfo.BMI_Max = m_InbodyData[149];
            inbodyInfo.BMI_Min = m_InbodyData[150];
            inbodyInfo.Percent_Body_Fat_Max = m_InbodyData[151];
            inbodyInfo.Percent_Body_Fat_Min = m_InbodyData[152];
            inbodyInfo.Waist_Hip_Ratio_Max = m_InbodyData[153];
            inbodyInfo.Waist_Hip_Ratio_Min = m_InbodyData[154];
            inbodyInfo.BMR_MAX = m_InbodyData[163];
            inbodyInfo.BMR_MIN = m_InbodyData[164];
            inbodyInfo.FCHEST = m_InbodyData[174];
            inbodyInfo.FABD = m_InbodyData[175];
            inbodyInfo.FACR = m_InbodyData[176];
            inbodyInfo.FACL = m_InbodyData[177];
            inbodyInfo.FTHIGHR = m_InbodyData[178];
            inbodyInfo.FTHIGHL = m_InbodyData[179];
            inbodyInfo.HEIGHT = m_InbodyData[189];

            strTemp = "DATETIMES ==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight ==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Protein Mass ==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Mineral Mass ==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "Body Fat Mass ==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "Total Body Water ==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Soft Lean Mass ==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "Fat Free Mass ==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "OSSEOUS ==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass ==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "BMI ==> " + m_InbodyData[24] + "\r\n";
            strTemp = strTemp + "Percent Body Fat ==> " + m_InbodyData[26] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio ==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "Target Weight ==> " + m_InbodyData[60] + "\r\n";
            strTemp = strTemp + "Weight Control ==> " + m_InbodyData[61] + "\r\n";
            strTemp = strTemp + "Fat Control ==> " + m_InbodyData[62] + "\r\n";
            strTemp = strTemp + "Muscle Control ==> " + m_InbodyData[63] + "\r\n";
            strTemp = strTemp + "Fitness Score ==> " + m_InbodyData[64] + "\r\n";
            strTemp = strTemp + "BMR ==> " + m_InbodyData[66] + "\r\n";
            strTemp = strTemp + "Neck ==> " + m_InbodyData[68] + "\r\n";
            strTemp = strTemp + "Chest ==> " + m_InbodyData[69] + "\r\n";
            strTemp = strTemp + "ABD ==> " + m_InbodyData[70] + "\r\n";
            strTemp = strTemp + "THIGHL ==> " + m_InbodyData[71] + "\r\n";
            strTemp = strTemp + "ACL ==> " + m_InbodyData[72] + "\r\n";
            strTemp = strTemp + "HIP ==> " + m_InbodyData[73] + "\r\n";
            strTemp = strTemp + "THIGHR ==> " + m_InbodyData[74] + "\r\n";
            strTemp = strTemp + "ACR ==> " + m_InbodyData[77] + "\r\n";
            strTemp = strTemp + "Protein Max ==> " + m_InbodyData[139] + "\r\n";
            strTemp = strTemp + "Protein Min ==> " + m_InbodyData[140] + "\r\n";
            strTemp = strTemp + "Mineral Max ==> " + m_InbodyData[141] + "\r\n";
            strTemp = strTemp + "Mineral Min ==> " + m_InbodyData[142] + "\r\n";
            strTemp = strTemp + "Body Fat Mass Max ==> " + m_InbodyData[143] + "\r\n";
            strTemp = strTemp + "Body Fat Mass Min ==> " + m_InbodyData[144] + "\r\n";
            strTemp = strTemp + "Weight Max ==> " + m_InbodyData[145] + "\r\n";
            strTemp = strTemp + "Weight Min ==> " + m_InbodyData[146] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass Max ==> " + m_InbodyData[147] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass Min ==> " + m_InbodyData[148] + "\r\n";
            strTemp = strTemp + "BMI Max ==> " + m_InbodyData[149] + "\r\n";
            strTemp = strTemp + "BMI Min ==> " + m_InbodyData[150] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Max ==> " + m_InbodyData[151] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Min ==> " + m_InbodyData[152] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio Max ==> " + m_InbodyData[153] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio Min ==> " + m_InbodyData[154] + "\r\n";
            strTemp = strTemp + "BMR MAX ==> " + m_InbodyData[163] + "\r\n";
            strTemp = strTemp + "BMR MIN ==> " + m_InbodyData[164] + "\r\n";
            strTemp = strTemp + "FCHEST ==> " + m_InbodyData[174] + "\r\n";
            strTemp = strTemp + "FABD ==> " + m_InbodyData[175] + "\r\n";
            strTemp = strTemp + "FACR ==> " + m_InbodyData[176] + "\r\n";
            strTemp = strTemp + "FACL ==> " + m_InbodyData[177] + "\r\n";
            strTemp = strTemp + "FTHIGHR ==> " + m_InbodyData[178] + "\r\n";
            strTemp = strTemp + "FTHIGHL ==> " + m_InbodyData[179] + "\r\n";
            strTemp = strTemp + "HEIGHT ==> " + m_InbodyData[189] + "\r\n";

            //textBox2.Text = strTemp;

            txtWeight.Text = Weight;
            txtHeight.Text = Height;
            txtBMI.Text = BMI;
        }

        /// <summary>
        /// Inbody 720 , 520 Data
        /// </summary>
        /// <param name="strData"></param> 
        private void InBody720_520_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 300; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES ==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight ==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Evaluation Data ==> " + m_InbodyData[2] + "\r\n";
            strTemp = strTemp + "Inter cellular Water ==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Extra cellular Water ==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Protein Mass ==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "Mineral Mass ==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "Body Fat Mass ==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "Soft Lean Mass==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "Fat Free Mass==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Born Mineral Contents==> " + m_InbodyData[11] + "\r\n";
            strTemp = strTemp + "Ideal Inter cellular Water==> " + m_InbodyData[12] + "\r\n";
            strTemp = strTemp + "Ideal Extra cellular Water==> " + m_InbodyData[13] + "\r\n";
            strTemp = strTemp + "IPROTEIN==> " + m_InbodyData[14] + "\r\n";
            strTemp = strTemp + "IMINERAL==> " + m_InbodyData[15] + "\r\n";
            strTemp = strTemp + "Ideal Body Fat Mass==> " + m_InbodyData[16] + "\r\n";
            strTemp = strTemp + "Ideal Total Body Water==> " + m_InbodyData[17] + "\r\n";
            strTemp = strTemp + "Ideal Born Mineral Contents==> " + m_InbodyData[18] + "\r\n";
            strTemp = strTemp + "Ideal Weight==> " + m_InbodyData[19] + "\r\n";
            strTemp = strTemp + "Percent Weight==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass==> " + m_InbodyData[21] + "\r\n";
            strTemp = strTemp + "Ideal Skeletal Muscle Mass==> " + m_InbodyData[22] + "\r\n";
            strTemp = strTemp + "Percent Skeletal Muscle Mass==> " + m_InbodyData[23] + "\r\n";
            strTemp = strTemp + "PFATNEW==> " + m_InbodyData[24] + "\r\n";
            strTemp = strTemp + "BMI==> " + m_InbodyData[25] + "\r\n";
            strTemp = strTemp + "Ideal BMI==> " + m_InbodyData[26] + "\r\n";
            strTemp = strTemp + "Percent Body Fat==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "Waist-Hip-Ratio==> " + m_InbodyData[28] + "\r\n";
            strTemp = strTemp + "Ideal WHR==> " + m_InbodyData[29] + "\r\n";
            strTemp = strTemp + "Lean Right Arm==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "Lean Left Arm==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Lean Trunk==> " + m_InbodyData[32] + "\r\n";
            strTemp = strTemp + "Lean Right Leg==> " + m_InbodyData[33] + "\r\n";
            strTemp = strTemp + "Lean Left Leg==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "Percent Lean Right Arm==> " + m_InbodyData[35] + "\r\n";
            strTemp = strTemp + "Percent Lean Left Arm==> " + m_InbodyData[36] + "\r\n";
            strTemp = strTemp + "Percent Lean Trunk==> " + m_InbodyData[37] + "\r\n";
            strTemp = strTemp + "Percent Lean Right Leg==> " + m_InbodyData[38] + "\r\n";
            strTemp = strTemp + "Percent Lean Left Leg==> " + m_InbodyData[39] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Right Arm==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Left Arm==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Trunk==> " + m_InbodyData[42] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Right Leg==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Left Leg==> " + m_InbodyData[44] + "\r\n";
            strTemp = strTemp + "ECF/TBF Right Arm EDEMA ==> " + m_InbodyData[45] + "\r\n";
            strTemp = strTemp + "ECF/TBF Left Arm EDEMA==> " + m_InbodyData[46] + "\r\n";
            strTemp = strTemp + "ECF/TBF Trunk EDEMA==> " + m_InbodyData[47] + "\r\n";
            strTemp = strTemp + "ECF/TBF Right Leg Edema==> " + m_InbodyData[48] + "\r\n";
            strTemp = strTemp + "ECF/TBF Left Leg Edema==> " + m_InbodyData[49] + "\r\n";
            strTemp = strTemp + "ECW/TBW Right Arm EDEMA==> " + m_InbodyData[50] + "\r\n";
            strTemp = strTemp + "ECW/TBW Left Arm EDEMA==> " + m_InbodyData[51] + "\r\n";
            strTemp = strTemp + "ECW/TBW Trunk EDEMA==> " + m_InbodyData[52] + "\r\n";
            strTemp = strTemp + "ECW/TBW Right Leg Edema==> " + m_InbodyData[53] + "\r\n";
            strTemp = strTemp + "ECW/TBW Left Leg Edema==> " + m_InbodyData[54] + "\r\n";
            strTemp = strTemp + "ECF/TBF Edema==> " + m_InbodyData[55] + "\r\n";
            strTemp = strTemp + "ECW/TBW Edema==> " + m_InbodyData[56] + "\r\n";
            strTemp = strTemp + "Percent PROTEIN==> " + m_InbodyData[57] + "\r\n";
            strTemp = strTemp + "Percent MINERAL==> " + m_InbodyData[58] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Mass==> " + m_InbodyData[59] + "\r\n";
            strTemp = strTemp + "PTBW==> " + m_InbodyData[60] + "\r\n";
            strTemp = strTemp + "Target Weight==> " + m_InbodyData[61] + "\r\n";
            strTemp = strTemp + "Weight Control==> " + m_InbodyData[62] + "\r\n";
            strTemp = strTemp + "Fat Control==> " + m_InbodyData[63] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[64] + "\r\n";
            strTemp = strTemp + "Fitness Score==> " + m_InbodyData[65] + "\r\n";
            strTemp = strTemp + "BCM==> " + m_InbodyData[66] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[67] + "\r\n";
            strTemp = strTemp + "Ideal BCM==> " + m_InbodyData[68] + "\r\n";
            strTemp = strTemp + "NECK==> " + m_InbodyData[69] + "\r\n";
            strTemp = strTemp + "CHEST==> " + m_InbodyData[70] + "\r\n";
            strTemp = strTemp + "Abdomen==> " + m_InbodyData[71] + "\r\n";
            strTemp = strTemp + "Right THIGH==> " + m_InbodyData[72] + "\r\n";
            strTemp = strTemp + "Arm Circumference Leg==> " + m_InbodyData[73] + "\r\n";
            strTemp = strTemp + "HIP==> " + m_InbodyData[74] + "\r\n";
            strTemp = strTemp + "Left THIGH==> " + m_InbodyData[75] + "\r\n";
            strTemp = strTemp + "AMC==> " + m_InbodyData[76] + "\r\n";
            strTemp = strTemp + "Visceral Fat Area==> " + m_InbodyData[77] + "\r\n";
            strTemp = strTemp + "Arm Circumference Right==> " + m_InbodyData[78] + "\r\n";
            strTemp = strTemp + "BSD==> " + m_InbodyData[79] + "\r\n";
            strTemp = strTemp + "Body Density==> " + m_InbodyData[80] + "\r\n";
            //			strTemp = strTemp + "Fluid Right Arm ==> " + m_InbodyData[81] + "\r\n";
            //			strTemp = strTemp + "Fluid Left Arm==> " + m_InbodyData[82] + "\r\n";
            //			strTemp = strTemp + "Fluid Trunk==> " + m_InbodyData[83] + "\r\n";
            //			strTemp = strTemp + "Fluid Right Leg==> " + m_InbodyData[84] + "\r\n";
            //			strTemp = strTemp + "Fluid Left Leg ==> " + m_InbodyData[85] + "\r\n";

            strTemp = strTemp + "Fluid Right Arm ==> " + m_InbodyData[221] + "\r\n";
            strTemp = strTemp + "Fluid Left Arm==> " + m_InbodyData[222] + "\r\n";
            strTemp = strTemp + "Fluid Trunk==> " + m_InbodyData[223] + "\r\n";
            strTemp = strTemp + "Fluid Right Leg==> " + m_InbodyData[224] + "\r\n";
            strTemp = strTemp + "Fluid Left Leg ==> " + m_InbodyData[225] + "\r\n";
            strTemp = strTemp + "Ideal Percent Body Fat==> " + m_InbodyData[226] + "\r\n";

            strTemp = strTemp + "IRA1==> " + m_InbodyData[87] + "\r\n";
            strTemp = strTemp + "ILA1==> " + m_InbodyData[88] + "\r\n";
            strTemp = strTemp + "IT1==> " + m_InbodyData[89] + "\r\n";
            strTemp = strTemp + "IRL1==> " + m_InbodyData[90] + "\r\n";
            strTemp = strTemp + "ILL1==> " + m_InbodyData[91] + "\r\n";
            strTemp = strTemp + "IRA5==> " + m_InbodyData[92] + "\r\n";
            strTemp = strTemp + "ILA5==> " + m_InbodyData[93] + "\r\n";
            strTemp = strTemp + "IT5==> " + m_InbodyData[94] + "\r\n";
            strTemp = strTemp + "IRL5==> " + m_InbodyData[95] + "\r\n";
            strTemp = strTemp + "ILL5==> " + m_InbodyData[96] + "\r\n";
            strTemp = strTemp + "IRA50==> " + m_InbodyData[97] + "\r\n";
            strTemp = strTemp + "ILA50==> " + m_InbodyData[98] + "\r\n";
            strTemp = strTemp + "IT50==> " + m_InbodyData[99] + "\r\n";
            strTemp = strTemp + "IRL50==> " + m_InbodyData[100] + "\r\n";
            strTemp = strTemp + "ILL50==> " + m_InbodyData[101] + "\r\n";
            strTemp = strTemp + "IRA250==> " + m_InbodyData[102] + "\r\n";
            strTemp = strTemp + "ILA250==> " + m_InbodyData[103] + "\r\n";
            strTemp = strTemp + "IT250==> " + m_InbodyData[104] + "\r\n";
            strTemp = strTemp + "IRL250==> " + m_InbodyData[105] + "\r\n";
            strTemp = strTemp + "ILL250==> " + m_InbodyData[106] + "\r\n";
            strTemp = strTemp + "IRA500==> " + m_InbodyData[107] + "\r\n";
            strTemp = strTemp + "ILA500==> " + m_InbodyData[108] + "\r\n";
            strTemp = strTemp + "IT500==> " + m_InbodyData[109] + "\r\n";
            strTemp = strTemp + "IRL500==> " + m_InbodyData[110] + "\r\n";
            strTemp = strTemp + "ILL500==> " + m_InbodyData[111] + "\r\n";
            strTemp = strTemp + "IRA1M==> " + m_InbodyData[112] + "\r\n";
            strTemp = strTemp + "ILA1M==> " + m_InbodyData[113] + "\r\n";
            strTemp = strTemp + "IT1M==> " + m_InbodyData[114] + "\r\n";
            strTemp = strTemp + "IRL1M==> " + m_InbodyData[115] + "\r\n";
            strTemp = strTemp + "ILL1M==> " + m_InbodyData[116] + "\r\n";
            strTemp = strTemp + "XRA5==> " + m_InbodyData[117] + "\r\n";
            strTemp = strTemp + "XLA5==> " + m_InbodyData[118] + "\r\n";
            strTemp = strTemp + "XTR5==> " + m_InbodyData[119] + "\r\n";
            strTemp = strTemp + "XRL5==> " + m_InbodyData[120] + "\r\n";
            strTemp = strTemp + "XLL5==> " + m_InbodyData[121] + "\r\n";
            strTemp = strTemp + "XRA50==> " + m_InbodyData[122] + "\r\n";
            strTemp = strTemp + "XLA50==> " + m_InbodyData[123] + "\r\n";
            strTemp = strTemp + "XTR50==> " + m_InbodyData[124] + "\r\n";
            strTemp = strTemp + "XRL50==> " + m_InbodyData[125] + "\r\n";
            strTemp = strTemp + "XLL50==> " + m_InbodyData[126] + "\r\n";
            strTemp = strTemp + "XRA250==> " + m_InbodyData[127] + "\r\n";
            strTemp = strTemp + "XLA250==> " + m_InbodyData[128] + "\r\n";
            strTemp = strTemp + "XTR250==> " + m_InbodyData[129] + "\r\n";
            strTemp = strTemp + "XRL250==> " + m_InbodyData[130] + "\r\n";
            strTemp = strTemp + "XLL250==> " + m_InbodyData[131] + "\r\n";
            strTemp = strTemp + "Ideal Soft Lean Mass==> " + m_InbodyData[132] + "\r\n";
            strTemp = strTemp + "Ideal Fat Free Mass==> " + m_InbodyData[133] + "\r\n";
            strTemp = strTemp + "Percent Fat Free Mass==> " + m_InbodyData[134] + "\r\n";
            strTemp = strTemp + "Ideal Height==> " + m_InbodyData[135] + "\r\n";
            strTemp = strTemp + "ICW_MAX==> " + m_InbodyData[136] + "\r\n";
            strTemp = strTemp + "ICW_MIN==> " + m_InbodyData[137] + "\r\n";
            strTemp = strTemp + "ECW_MAX==> " + m_InbodyData[138] + "\r\n";
            strTemp = strTemp + "ECW_MIN==> " + m_InbodyData[139] + "\r\n";
            strTemp = strTemp + "PROTEIN_MAX==> " + m_InbodyData[140] + "\r\n";
            strTemp = strTemp + "PROTEIN_MIN==> " + m_InbodyData[141] + "\r\n";
            strTemp = strTemp + "MINERAL_MAX==> " + m_InbodyData[142] + "\r\n";
            strTemp = strTemp + "MINERAL_MIN==> " + m_InbodyData[143] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MAX==> " + m_InbodyData[144] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MIN==> " + m_InbodyData[145] + "\r\n";
            strTemp = strTemp + "Weight MAX==> " + m_InbodyData[146] + "\r\n";
            strTemp = strTemp + "Weight MIN==> " + m_InbodyData[147] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MAX==> " + m_InbodyData[148] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MIN==> " + m_InbodyData[149] + "\r\n";
            strTemp = strTemp + "BMI_MAX==> " + m_InbodyData[150] + "\r\n";
            strTemp = strTemp + "BMI_MIN==> " + m_InbodyData[151] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MAX==> " + m_InbodyData[152] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MIN==> " + m_InbodyData[153] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MAX==> " + m_InbodyData[154] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MIN==> " + m_InbodyData[155] + "\r\n";
            strTemp = strTemp + "Obesity MAX==> " + m_InbodyData[156] + "\r\n";
            strTemp = strTemp + "Obesity MIN==> " + m_InbodyData[157] + "\r\n";
            strTemp = strTemp + "Body Cell Mass MAX==> " + m_InbodyData[158] + "\r\n";
            strTemp = strTemp + "Body Cell Mass MIN==> " + m_InbodyData[159] + "\r\n";
            strTemp = strTemp + "Born Mineral Contents MAX==> " + m_InbodyData[160] + "\r\n";
            strTemp = strTemp + "Born Mineral Contents MIN==> " + m_InbodyData[161] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Mass MAX==> " + m_InbodyData[163] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Mass MIN==> " + m_InbodyData[162] + "\r\n";
            strTemp = strTemp + "BMR MAX==> " + m_InbodyData[165] + "\r\n";
            strTemp = strTemp + "BMR MIN==> " + m_InbodyData[164] + "\r\n";

            strTemp = strTemp + "CCHEST==> " + m_InbodyData[167] + "\r\n";
            strTemp = strTemp + "CABD==> " + m_InbodyData[168] + "\r\n";
            strTemp = strTemp + "CACR==> " + m_InbodyData[169] + "\r\n";
            strTemp = strTemp + "CACL==> " + m_InbodyData[170] + "\r\n";
            strTemp = strTemp + "CTHIGHR==> " + m_InbodyData[171] + "\r\n";
            strTemp = strTemp + "CTHIGHL==> " + m_InbodyData[172] + "\r\n";

            strTemp = strTemp + "FCHEST==> " + m_InbodyData[173] + "\r\n";
            strTemp = strTemp + "FABD==> " + m_InbodyData[174] + "\r\n";
            strTemp = strTemp + "FACR==> " + m_InbodyData[175] + "\r\n";
            strTemp = strTemp + "FACL==> " + m_InbodyData[176] + "\r\n";
            strTemp = strTemp + "FTHIGHR==> " + m_InbodyData[177] + "\r\n";
            strTemp = strTemp + "FTHIGHL==> " + m_InbodyData[178] + "\r\n";

            strTemp = strTemp + "INECK==> " + m_InbodyData[179] + "\r\n";
            strTemp = strTemp + "ICHEST==> " + m_InbodyData[180] + "\r\n";
            strTemp = strTemp + "IABD==> " + m_InbodyData[181] + "\r\n";
            strTemp = strTemp + "IHIP==> " + m_InbodyData[182] + "\r\n";
            strTemp = strTemp + "IACR==> " + m_InbodyData[183] + "\r\n";
            strTemp = strTemp + "IACL==> " + m_InbodyData[184] + "\r\n";
            strTemp = strTemp + "ITHIGHR==> " + m_InbodyData[185] + "\r\n";
            strTemp = strTemp + "ITHIGHL==> " + m_InbodyData[186] + "\r\n";

            strTemp = strTemp + "ICCHEST==> " + m_InbodyData[187] + "\r\n";
            strTemp = strTemp + "ICABD==> " + m_InbodyData[188] + "\r\n";
            strTemp = strTemp + "ICACR==> " + m_InbodyData[189] + "\r\n";
            strTemp = strTemp + "ICACL==> " + m_InbodyData[190] + "\r\n";
            strTemp = strTemp + "ICTHIGHR==> " + m_InbodyData[191] + "\r\n";
            strTemp = strTemp + "ICTHIGHL==> " + m_InbodyData[192] + "\r\n";

            strTemp = strTemp + "IFCHEST==> " + m_InbodyData[193] + "\r\n";
            strTemp = strTemp + "IFABD==> " + m_InbodyData[194] + "\r\n";
            strTemp = strTemp + "IFACR==> " + m_InbodyData[195] + "\r\n";
            strTemp = strTemp + "IFACL==> " + m_InbodyData[196] + "\r\n";
            strTemp = strTemp + "IFTHIGHR==> " + m_InbodyData[197] + "\r\n";
            strTemp = strTemp + "IFTHIGHL==> " + m_InbodyData[198] + "\r\n";

            strTemp = strTemp + "DNECK==> " + m_InbodyData[199] + "\r\n";
            strTemp = strTemp + "DCHEST==> " + m_InbodyData[200] + "\r\n";
            strTemp = strTemp + "DABD==> " + m_InbodyData[201] + "\r\n";
            strTemp = strTemp + "DHIP==> " + m_InbodyData[202] + "\r\n";
            strTemp = strTemp + "DACR==> " + m_InbodyData[203] + "\r\n";
            strTemp = strTemp + "DACL==> " + m_InbodyData[204] + "\r\n";
            strTemp = strTemp + "DTHIGHR==> " + m_InbodyData[205] + "\r\n";
            strTemp = strTemp + "DTHIGHL==> " + m_InbodyData[206] + "\r\n";

            strTemp = strTemp + "DCCHEST==> " + m_InbodyData[207] + "\r\n";
            strTemp = strTemp + "DCABD==> " + m_InbodyData[208] + "\r\n";
            strTemp = strTemp + "DCACR==> " + m_InbodyData[209] + "\r\n";
            strTemp = strTemp + "DCACL==> " + m_InbodyData[210] + "\r\n";
            strTemp = strTemp + "DCTHIGHR==> " + m_InbodyData[211] + "\r\n";
            strTemp = strTemp + "DCTHIGHL==> " + m_InbodyData[212] + "\r\n";

            strTemp = strTemp + "DFCHEST==> " + m_InbodyData[213] + "\r\n";
            strTemp = strTemp + "DFABD==> " + m_InbodyData[214] + "\r\n";
            strTemp = strTemp + "DFACR==> " + m_InbodyData[215] + "\r\n";
            strTemp = strTemp + "DFACL==> " + m_InbodyData[216] + "\r\n";
            strTemp = strTemp + "DFTHIGHR==> " + m_InbodyData[217] + "\r\n";
            strTemp = strTemp + "DFTHIGHL==> " + m_InbodyData[218] + "\r\n";

            strTemp = strTemp + "Evaluation Data FAT==> " + m_InbodyData[229] + "\r\n";
            //부위별 지방
            strTemp = strTemp + "Fat mass of Right Arm==> " + m_InbodyData[81] + "\r\n";
            strTemp = strTemp + "Fat mass of Left Arm ==> " + m_InbodyData[82] + "\r\n";
            strTemp = strTemp + "Fat mass of Trunk ==> " + m_InbodyData[83] + "\r\n";
            strTemp = strTemp + "Fat mass of Right Leg ==> " + m_InbodyData[84] + "\r\n";
            strTemp = strTemp + "Fat mass of Left Leg==> " + m_InbodyData[85] + "\r\n";

            //STHYG
            strTemp = strTemp + "STHYG1==> " + m_InbodyData[226] + "\r\n";
            strTemp = strTemp + "STHYG2==> " + m_InbodyData[227] + "\r\n";
            strTemp = strTemp + "STHYG3==> " + m_InbodyData[228] + "\r\n";

            //Stadiometer
            strTemp = strTemp + "HEIGHT==> " + m_InbodyData[229] + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Only American - USA 
        /// </summary>
        /// <param name="strData"></param> 
        private void InBody520_US_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 300; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES ==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight ==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Inter cellular Water ==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Extra cellular Water ==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Body Fat Mass ==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "Lean Body Mass==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "BMI==> " + m_InbodyData[25] + "\r\n";
            strTemp = strTemp + "Percent Body Fat==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "Lean Right Arm==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "Lean Left Arm==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Lean Trunk==> " + m_InbodyData[32] + "\r\n";
            strTemp = strTemp + "Lean Right Leg==> " + m_InbodyData[33] + "\r\n";
            strTemp = strTemp + "Lean Left Leg==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Right Arm==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Left Arm==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Trunk==> " + m_InbodyData[42] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Right Leg==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "Percent Ideal Lean Left Leg==> " + m_InbodyData[44] + "\r\n";
            strTemp = strTemp + "ECW/TBW Edema==> " + m_InbodyData[56] + "\r\n";
            strTemp = strTemp + "Fat Control==> " + m_InbodyData[63] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[64] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[67] + "\r\n";
            strTemp = strTemp + "BMI_MAX==> " + m_InbodyData[150] + "\r\n";
            strTemp = strTemp + "BMI_MIN==> " + m_InbodyData[151] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MAX==> " + m_InbodyData[152] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MIN==> " + m_InbodyData[153] + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// InbodyJ05 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBodyJ05Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp = string.Empty;

            int length = 69;

            for (int i = 0; i < length; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            for (int i = 0; i < length; i++)
            {
                strTemp = strTemp + "Value[" + i.ToString() + "]==> " + m_InbodyData[i] + "\r\n";
            }

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody320 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody320_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 123; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight==> " + ConvertKgTolb(m_InbodyData[1], 1) + "\r\n";
            strTemp = strTemp + "Body Fat Mass==> " + ConvertKgTolb(m_InbodyData[5], 1) + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + ConvertKgTolb(m_InbodyData[6], 1) + "\r\n";

            strTemp = strTemp + "Lean Body Free Mass==> " + ConvertKgTolb(m_InbodyData[8], 1) + "\r\n";
            strTemp = strTemp + "PBF==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "Target Weight==> " + ConvertKgTolb(m_InbodyData[11], 1) + "\r\n";
            strTemp = strTemp + "Weight Control==> " + ConvertKgTolb(m_InbodyData[12], 1) + "\r\n";
            strTemp = strTemp + "Fat Control==> " + ConvertKgTolb(m_InbodyData[13], 1) + "\r\n";
            strTemp = strTemp + "LBM Control==> " + ConvertKgTolb(m_InbodyData[14], 1) + "\r\n";

            strTemp = strTemp + "BMI==> " + m_InbodyData[16] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[17] + "\r\n";

            strTemp = strTemp + "Dry Lean Mass==> " + ConvertKgTolb(m_InbodyData[106], 1) + "\r\n";
            strTemp = strTemp + "Right Arm==> " + ConvertCmToInNoUnit(m_InbodyData[107], 1) + "\r\n";
            strTemp = strTemp + "Left Arm==> " + ConvertCmToInNoUnit(m_InbodyData[108], 1) + "\r\n";
            strTemp = strTemp + "Trunk==> " + ConvertCmToInNoUnit(m_InbodyData[109], 1) + "\r\n";
            strTemp = strTemp + "Right Leg==> " + ConvertCmToInNoUnit(m_InbodyData[110], 1) + "\r\n";
            strTemp = strTemp + "Left Leg==> " + ConvertCmToInNoUnit(m_InbodyData[111], 1) + "\r\n";
            strTemp = strTemp + "Intracellular Water==>" + ConvertKgTolb(m_InbodyData[117], 1) + "\r\n";
            strTemp = strTemp + "Extracellular Water==>" + ConvertKgTolb(m_InbodyData[120], 1) + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody330 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody330_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 105; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Evaluation Data==> " + m_InbodyData[2] + "\r\n";
            strTemp = strTemp + "Protein Mass==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Mineral Mass==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Body Fat Mass==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "Soft Lean Mass==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Fat Free Mass==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "PBF==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "WHR==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Target Weight==> " + m_InbodyData[11] + "\r\n";
            strTemp = strTemp + "Weight Control==> " + m_InbodyData[12] + "\r\n";
            strTemp = strTemp + "Fat Control==> " + m_InbodyData[13] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[14] + "\r\n";
            strTemp = strTemp + "Fitness Score==> " + m_InbodyData[15] + "\r\n";
            strTemp = strTemp + "BMI==> " + m_InbodyData[16] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[17] + "\r\n";

            strTemp = strTemp + "Skeletal Muscle Mass==> " + m_InbodyData[18] + "\r\n";
            strTemp = strTemp + "Percent Weight==> " + m_InbodyData[19] + "\r\n";
            strTemp = strTemp + "Percent Skeletal Muscle Mass==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Mass==> " + m_InbodyData[21] + "\r\n";
            strTemp = strTemp + "Percent Fat Free Mass==> " + m_InbodyData[22] + "\r\n";

            strTemp = strTemp + "OSSEOUS==> " + m_InbodyData[23] + "\r\n";
            strTemp = strTemp + "Total Body Water MIN==> " + m_InbodyData[24] + "\r\n";
            strTemp = strTemp + "Total Body Water MAX==> " + m_InbodyData[25] + "\r\n";
            strTemp = strTemp + "PROTEIN MIN==> " + m_InbodyData[26] + "\r\n";
            strTemp = strTemp + "PROTEIN MAX==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "MINERAL MIN==> " + m_InbodyData[28] + "\r\n";
            strTemp = strTemp + "MINERAL MAX==> " + m_InbodyData[29] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MIN==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MAX==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Weight MIN==> " + m_InbodyData[32] + "\r\n";
            strTemp = strTemp + "Weight MAX==> " + m_InbodyData[33] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MIN==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MAX==> " + m_InbodyData[35] + "\r\n";
            strTemp = strTemp + "BMI MIN==> " + m_InbodyData[36] + "\r\n";
            strTemp = strTemp + "BMI MAX==> " + m_InbodyData[37] + "\r\n";
            strTemp = strTemp + "PBF MAX==> " + m_InbodyData[38] + "\r\n";
            strTemp = strTemp + "PBF MAX==> " + m_InbodyData[39] + "\r\n";
            strTemp = strTemp + "WHR_MIN==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "WHR_MAX==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "STHYG1==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "STHYG2==> " + m_InbodyData[44] + "\r\n";
            strTemp = strTemp + "STHYG3==> " + m_InbodyData[45] + "\r\n";
            strTemp = strTemp + "NECK==> " + m_InbodyData[46] + "\r\n";
            strTemp = strTemp + "CHEST==> " + m_InbodyData[47] + "\r\n";
            strTemp = strTemp + "Abdomen==> " + m_InbodyData[48] + "\r\n";
            strTemp = strTemp + "HIP==> " + m_InbodyData[49] + "\r\n";
            strTemp = strTemp + "Arm Circumference Right==> " + m_InbodyData[50] + "\r\n";
            strTemp = strTemp + "Arm Circumference Left==> " + m_InbodyData[51] + "\r\n";
            strTemp = strTemp + "Right THIGH==> " + m_InbodyData[52] + "\r\n";
            strTemp = strTemp + "Left THIGH==> " + m_InbodyData[53] + "\r\n";
            strTemp = strTemp + "CCHEST(Measured muscle circumference value of chest)==> " + m_InbodyData[54] + "\r\n";
            strTemp = strTemp + "CABD(Measured muscle circumference value of abdomen)==> " + m_InbodyData[55] + "\r\n";
            strTemp = strTemp + "CACR(Measured muscle circumference value of right arm)==> " + m_InbodyData[56] + "\r\n";
            strTemp = strTemp + "CACL(Measured muscle circumference value of left arm)==> " + m_InbodyData[57] + "\r\n";
            strTemp = strTemp + "CTHIGHR(Measured muscle circumference value of right thigh)==> " + m_InbodyData[58] + "\r\n";
            strTemp = strTemp + "CTHIGHL(Measured muscle circumference value of left thigh)==> " + m_InbodyData[59] + "\r\n";
            strTemp = strTemp + "FCHEST(Measured fat thickness of chest)==> " + m_InbodyData[60] + "\r\n";
            strTemp = strTemp + "FABD	(Measured fat thickness of abdomen)==> " + m_InbodyData[61] + "\r\n";
            strTemp = strTemp + "FACR(Measured fat thickness of right arm)==> " + m_InbodyData[62] + "\r\n";
            strTemp = strTemp + "FACL(Measured fat thickness of left arm)==> " + m_InbodyData[63] + "\r\n";
            strTemp = strTemp + "FTHIGHR(Measured fat thickness of right thigh)==> " + m_InbodyData[64] + "\r\n";
            strTemp = strTemp + "FTHIGHL(Measured fat thickness of left thigh)==> " + m_InbodyData[65] + "\r\n";
            strTemp = strTemp + "INECK(Standard  outer  circumference value of neck)==> " + m_InbodyData[66] + "\r\n";
            strTemp = strTemp + "ICHEST(Standard  outer  circumference value of chest)==> " + m_InbodyData[67] + "\r\n";
            strTemp = strTemp + "IABD(Standard  outer  circumference value of abdomen)==> " + m_InbodyData[68] + "\r\n";
            strTemp = strTemp + "IHIP	(Standard  outer  circumference value of hip)==> " + m_InbodyData[69] + "\r\n";
            strTemp = strTemp + "IACR	(Standard  outer  circumference value of right arm)==> " + m_InbodyData[70] + "\r\n";
            strTemp = strTemp + "IACL(Standard  outer  circumference value of left arm)==> " + m_InbodyData[71] + "\r\n";
            strTemp = strTemp + "ITHIGHR(Standard  outer  circumference value of right thigh)==> " + m_InbodyData[72] + "\r\n";
            strTemp = strTemp + "ITHIGHL(Standard  outer  circumference value of left thigh)==> " + m_InbodyData[73] + "\r\n";
            strTemp = strTemp + "ICCHEST(Standard  muscle circumference value of chest)==> " + m_InbodyData[74] + "\r\n";
            strTemp = strTemp + "ICABD(Standard  muscle circumference value of abdomen)==> " + m_InbodyData[75] + "\r\n";
            strTemp = strTemp + "ICACR(Standard  muscle circumference value of right arm)==> " + m_InbodyData[76] + "\r\n";
            strTemp = strTemp + "ICACL(Standard  muscle circumference value of left arm)==> " + m_InbodyData[77] + "\r\n";
            strTemp = strTemp + "ICTHIGHR(Standard  muscle circumference value of right thigh)==> " + m_InbodyData[78] + "\r\n";
            strTemp = strTemp + "ICTHIGHL Standard  muscle circumference value of left thigh)==> " + m_InbodyData[79] + "\r\n";
            strTemp = strTemp + "IFCHEST	(Standard  fat thickness of chest)==> " + m_InbodyData[80] + "\r\n";
            strTemp = strTemp + "IFABD(Standard  fat thickness of abdomen)==> " + m_InbodyData[81] + "\r\n";
            strTemp = strTemp + "IFACR(Standard  fat thickness of right arm)==> " + m_InbodyData[82] + "\r\n";
            strTemp = strTemp + "IFACL(Standard  fat thickness of left arm)==> " + m_InbodyData[83] + "\r\n";
            strTemp = strTemp + "IFTHIGHR(Standard  fat thickness of right thigh)==> " + m_InbodyData[84] + "\r\n";
            strTemp = strTemp + "IFTHIGHL( Standard  fat thickness of left thigh)==> " + m_InbodyData[85] + "\r\n";
            strTemp = strTemp + "DNECK(Difference  in measured and standard circumference values of neck)==> " + m_InbodyData[86] + "\r\n";
            strTemp = strTemp + "DCHEST(Difference  in measured and standard circumference values of chest)==> " + m_InbodyData[87] + "\r\n";
            strTemp = strTemp + "DABD(Difference  in measured and standard circumference values of abdomen)==> " + m_InbodyData[88] + "\r\n";
            strTemp = strTemp + "DHIP(Difference  in measured and standard circumference values of hip)==> " + m_InbodyData[89] + "\r\n";
            strTemp = strTemp + "DACR(Difference  in measured and standard circumference values of right arm)==> " + m_InbodyData[90] + "\r\n";
            strTemp = strTemp + "DACL(Difference  in measured and standard circumference values of left arm)==> " + m_InbodyData[91] + "\r\n";
            strTemp = strTemp + "DTHIGHR(Difference  in measured and standard circumference values of right thigh)==> " + m_InbodyData[92] + "\r\n";
            strTemp = strTemp + "DTHIGHL(Difference  in measured and standard circumference values of left thigh)==> " + m_InbodyData[93] + "\r\n";
            strTemp = strTemp + "DCCHEST(Difference  in measured and standard muscle circumference values of chest)==> " + m_InbodyData[94] + "\r\n";
            strTemp = strTemp + "DCABD(Difference  in measured and standard muscle circumference values of abdomen)==> " + m_InbodyData[95] + "\r\n";
            strTemp = strTemp + "DCACR(Difference  in measured and standard muscle circumference values of right arm)==> " + m_InbodyData[96] + "\r\n";
            strTemp = strTemp + "DCACL(Difference  in measured and standard muscle circumference values of left arm)==> " + m_InbodyData[97] + "\r\n";
            strTemp = strTemp + "DCTHIGHR(Difference  in measured and standard muscle circumference values of right thigh==> " + m_InbodyData[98] + "\r\n";
            strTemp = strTemp + "DCTHIGHL(Difference  in measured and standard muscle circumference values of left thigh)==> " + m_InbodyData[99] + "\r\n";
            strTemp = strTemp + "DFCHEST(Difference  in measured and standard fat thickness of chest)==> " + m_InbodyData[100] + "\r\n";
            strTemp = strTemp + "DFABD(Standard  fat thickness of abdomen==> " + m_InbodyData[101] + "\r\n";
            strTemp = strTemp + "DFACR(Difference  in measured and standard fat thickness of right arm)==> " + m_InbodyData[102] + "\r\n";
            strTemp = strTemp + "DFACL(Difference  in measured and standard fat thickness of left arm)==> " + m_InbodyData[103] + "\r\n";
            strTemp = strTemp + "DFTHIGHR(Difference  in measured and standard fat thickness of right thigh)==> " + m_InbodyData[104] + "\r\n";
            strTemp = strTemp + "DFTHIGHL(Difference  in measured and standard fat thickness of left thigh)==> " + m_InbodyData[105] + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody370 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody370_Data(string strData)
        {
            string[] m_InbodyData = new string[92];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 92; i++)
            {
                try
                {
                    m_InbodyData[i] = "0";
                    intPos1 = intPos2 + 1;
                    intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                    if (intPos2 > 0)
                        m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            strTemp = "=============================Profile=============================\r\n";
            strTemp += "ID==> " + m_InbodyData[83] + "\r\n";
            strTemp += "Height==> " + m_InbodyData[84] + "\r\n";
            strTemp += "Age==> " + m_InbodyData[85] + "\r\n";
            strTemp += "Gender==> " + m_InbodyData[86] + "\r\n";
            strTemp += "Datetimes==> " + m_InbodyData[0] + "\r\n";

            strTemp += "====================Body Composition Analysis===================\r\n";
            strTemp += "TBW(Total Body Water)==> " + m_InbodyData[6] + "\r\n";
            strTemp += "Protein==> " + m_InbodyData[3] + "\r\n";
            strTemp += "Mineral==> " + m_InbodyData[4] + "\r\n";
            strTemp += "Body Fat Mass==> " + m_InbodyData[5] + "\r\n";
            strTemp += "Soft Lean Mass==> " + m_InbodyData[7] + "\r\n";
            strTemp += "Fat Free Mass==> " + m_InbodyData[8] + "\r\n";
            strTemp += "OSSEOUS==> " + m_InbodyData[9] + "\r\n";
            strTemp += "Weight==> " + m_InbodyData[1] + "\r\n";

            strTemp += "===============Body Composition Analysis Normal Range===========\r\n";
            strTemp += "Total Body Water Max Normal Range==> " + m_InbodyData[75] + "\r\n";
            strTemp += "Total Body Water Min Normal Range==> " + m_InbodyData[76] + "\r\n";
            strTemp += "PROTEIN Max Normal Range==> " + m_InbodyData[57] + "\r\n";
            strTemp += "PROTEIN Min Normal Range==> " + m_InbodyData[58] + "\r\n";
            strTemp += "MINERAL Max Normal Range==> " + m_InbodyData[59] + "\r\n";
            strTemp += "MINERAL Min Normal Range==> " + m_InbodyData[60] + "\r\n";
            strTemp += "Body Fat Mass Max Normal Range==> " + m_InbodyData[72] + "\r\n";
            strTemp += "Body Fat Mass Min Normal Range==> " + m_InbodyData[71] + "\r\n";

            strTemp += "=========================Muscle-Fat Analysis====================\r\n";
            strTemp += "Weight==> " + m_InbodyData[1] + "\r\n";
            strTemp += "Skeletal Muscle Mass==> " + m_InbodyData[11] + "\r\n";
            strTemp += "Body Fat Mass==> " + m_InbodyData[5] + "\r\n";
            strTemp += "Percent Weight(Graph)==> " + m_InbodyData[10] + "\r\n";
            strTemp += "Percent Skeletal Muscle Mass(Graph)==> " + m_InbodyData[12] + "\r\n";
            strTemp += "Percent Body Fat Mass(Graph)==> " + m_InbodyData[21] + "\r\n";

            strTemp += "=================Muscle-Fat Analysis Normal Range===============\r\n";
            strTemp += "Weight Max Normal Range==> " + m_InbodyData[61] + "\r\n";
            strTemp += "Weight Min Normal Range==> " + m_InbodyData[62] + "\r\n";
            strTemp += "Skeletal Muscle Mass Max Normal Range==> " + m_InbodyData[63] + "\r\n";
            strTemp += "Skeletal Muscle Mass Min Normal Range==> " + m_InbodyData[64] + "\r\n";
            strTemp += "Body Fat Mass Max Normal Range==> " + m_InbodyData[72] + "\r\n";
            strTemp += "Body Fat Mass Min Normal Range==> " + m_InbodyData[71] + "\r\n";

            strTemp += "==========================Obesity Diagnosis=====================\r\n";
            strTemp += "BMI==> " + m_InbodyData[13] + "\r\n";
            strTemp += "PBF==> " + m_InbodyData[14] + "\r\n";
            strTemp += "WHR==> " + m_InbodyData[15] + "\r\n";

            strTemp += "==================Obesity Diagnosis Normal Range================\r\n";
            strTemp += "BMI Max Normal Range==> " + m_InbodyData[65] + "\r\n";
            strTemp += "BMI Min Normal Range==> " + m_InbodyData[66] + "\r\n";
            strTemp += "PBF Max Normal Range==> " + m_InbodyData[67] + "\r\n";
            strTemp += "PBF Min Normal Range==> " + m_InbodyData[68] + "\r\n";
            strTemp += "WHR Max Normal Range==> " + m_InbodyData[69] + "\r\n";
            strTemp += "WHR Min Normal Range==> " + m_InbodyData[70] + "\r\n";

            strTemp += "===========================Weight Control=======================\r\n";
            strTemp += "Target Weight(Hidden)==> " + m_InbodyData[22] + "\r\n";


            strTemp += "Weight Control==> " + m_InbodyData[23] + "\r\n";
            strTemp += "Fat Control==> " + m_InbodyData[24] + "\r\n";
            strTemp += "Muscle Control==> " + m_InbodyData[25] + "\r\n";

            strTemp += "Fitness Score==> " + m_InbodyData[26] + "\r\n";
            strTemp += "BMR(Basal Metabolic Rate)==> " + m_InbodyData[27] + "\r\n";
            strTemp += "BMR Max Normal Range==> " + m_InbodyData[74] + "\r\n";
            strTemp += "BMR Min Normal Range==> " + m_InbodyData[73] + "\r\n";

            strTemp += "============================Segmental Lean======================\r\n";
            strTemp += "Segmental Lean Right Arm==> " + m_InbodyData[16] + "\r\n";
            strTemp += "Segmental Lean Left Arm==> " + m_InbodyData[17] + "\r\n";
            strTemp += "Segmental Lean Trunk==> " + m_InbodyData[18] + "\r\n";
            strTemp += "Segmental Lean Right Leg==> " + m_InbodyData[19] + "\r\n";
            strTemp += "Segmental Lean Left Leg==> " + m_InbodyData[20] + "\r\n";

            strTemp += "=============================Impedance==========================\r\n";
            strTemp += "RA5==> " + m_InbodyData[45] + "\r\n";
            strTemp += "LA5==> " + m_InbodyData[42] + "\r\n";
            strTemp += "TR5==> " + m_InbodyData[44] + "\r\n";
            strTemp += "RL5==> " + m_InbodyData[46] + "\r\n";
            strTemp += "LL5==> " + m_InbodyData[43] + "\r\n";

            strTemp += "RA50==> " + m_InbodyData[50] + "\r\n";
            strTemp += "LA50==> " + m_InbodyData[47] + "\r\n";
            strTemp += "TR50==> " + m_InbodyData[49] + "\r\n";
            strTemp += "RL50==> " + m_InbodyData[51] + "\r\n";
            strTemp += "LL50==> " + m_InbodyData[48] + "\r\n";

            strTemp += "RA250==> " + m_InbodyData[55] + "\r\n";
            strTemp += "LA250==> " + m_InbodyData[52] + "\r\n";
            strTemp += "TR250==> " + m_InbodyData[54] + "\r\n";
            strTemp += "RL250==> " + m_InbodyData[56] + "\r\n";
            strTemp += "LL250==> " + m_InbodyData[53] + "\r\n";

            strTemp += "==========================Evaluation Data=======================\r\n";
            strTemp += "Evaluation Data==> " + m_InbodyData[2] + "\r\n";

            strTemp += "=====================Segmental Fat(Fat Mass)=======================\r\n";
            strTemp += "Segmental Fat(Fat Mass) Right Arm==> " + m_InbodyData[37] + "\r\n";
            strTemp += "Segmental Fat(Fat Mass) Left Arm==> " + m_InbodyData[38] + "\r\n";
            strTemp += "Segmental Fat(Fat Mass) Trunk Arm==> " + m_InbodyData[39] + "\r\n";
            strTemp += "Segmental Fat(Fat Mass) Right Leg==> " + m_InbodyData[40] + "\r\n";
            strTemp += "Segmental Fat(Fat Mass) Left Leg==> " + m_InbodyData[41] + "\r\n";

            strTemp += "=====================Segmental Fat(PBF)=======================\r\n";
            strTemp += "Segmental Fat(PBF) Right Arm==> " + m_InbodyData[77] + "\r\n";
            strTemp += "Segmental Fat(PBF) Left Arm==> " + m_InbodyData[78] + "\r\n";
            strTemp += "Segmental Fat(PBF) Trunk Arm==> " + m_InbodyData[79] + "\r\n";
            strTemp += "Segmental Fat(PBF) Right Leg==> " + m_InbodyData[80] + "\r\n";
            strTemp += "Segmental Fat(PBF) Left Leg==> " + m_InbodyData[81] + "\r\n";

            strTemp += "==========================Segmental Evaluation Data=======================\r\n";
            strTemp += "Segmental Evaluation Data==> " + m_InbodyData[82] + "\r\n";


            strTemp += "==========================Segmental Evaluation Data=======================\r\n";
            strTemp += "STYHG1==> " + m_InbodyData[87] + "\r\n";
            strTemp += "STYHG2==> " + m_InbodyData[88] + "\r\n";
            strTemp += "STYHG3==> " + m_InbodyData[89] + "\r\n";

            //			strTemp += "Neck==> " + m_InbodyData[28] + "\r\n";
            //			strTemp += "Chest==> " + m_InbodyData[29] + "\r\n";
            //			strTemp += "Abdomen==> " + m_InbodyData[30] + "\r\n";
            //			strTemp += "Left Thigh==> " + m_InbodyData[31] + "\r\n";
            //			strTemp += "Arm Circumference Left==> " + m_InbodyData[32] + "\r\n";
            //			strTemp += "Hip==> " + m_InbodyData[33] + "\r\n";
            //			strTemp += "Right Thigh==> " + m_InbodyData[34] + "\r\n";
            //			strTemp += "VFA==> " + m_InbodyData[35] + "\r\n";
            //			strTemp += "Arm Circumference Right==> " + m_InbodyData[36] + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody220 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody220_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 45; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Evaluation Data==> " + m_InbodyData[2] + "\r\n";
            strTemp = strTemp + "BMI==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Percent Body Fat==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Fat Free Mass==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "Body Fat Mass==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "MINERAL==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Soft Lean Mass==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "Protein==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass==> " + m_InbodyData[11] + "\r\n";
            strTemp = strTemp + "Percent Weight==> " + m_InbodyData[12] + "\r\n";
            strTemp = strTemp + "Percent Fat Free Mass==> " + m_InbodyData[13] + "\r\n";
            strTemp = strTemp + "Percent Body Fat Mass==> " + m_InbodyData[14] + "\r\n";
            strTemp = strTemp + "Percent Skeletal Muscle Mass==> " + m_InbodyData[15] + "\r\n";
            strTemp = strTemp + "Fitness Score==> " + m_InbodyData[16] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[17] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[18] + "\r\n";
            strTemp = strTemp + "Fat Control==> " + m_InbodyData[19] + "\r\n";
            strTemp = strTemp + "Weight Control==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "Target Weight==> " + m_InbodyData[21] + "\r\n";
            strTemp = strTemp + "WHR==> " + m_InbodyData[22] + "\r\n";
            strTemp = strTemp + "RBMR==> " + m_InbodyData[23] + "\r\n";
            strTemp = strTemp + "TBW MIN==> " + m_InbodyData[24] + "\r\n";
            strTemp = strTemp + "TBW MAX==> " + m_InbodyData[25] + "\r\n";
            strTemp = strTemp + "PROTEIN MIN==> " + m_InbodyData[26] + "\r\n";
            strTemp = strTemp + "PROTEIN MAX==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "MINERAL MIN==> " + m_InbodyData[28] + "\r\n";
            strTemp = strTemp + "MINERAL MAX==> " + m_InbodyData[29] + "\r\n";
            strTemp = strTemp + "BFM MIN==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "BFM MAX==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Weight MIN==> " + m_InbodyData[32] + "\r\n";
            strTemp = strTemp + "Weight MAX==> " + m_InbodyData[33] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass  MIN==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass  MAX==> " + m_InbodyData[35] + "\r\n";
            strTemp = strTemp + "BMI MIN==> " + m_InbodyData[36] + "\r\n";
            strTemp = strTemp + "BMI MAX==> " + m_InbodyData[37] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MIN==> " + m_InbodyData[38] + "\r\n";
            strTemp = strTemp + "Percent Body Fat  MAX==> " + m_InbodyData[39] + "\r\n";
            strTemp = strTemp + "WHR MIN==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "WHR MAX==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "STHYG1==> " + m_InbodyData[42] + "\r\n";
            strTemp = strTemp + "STHYG2==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "STHYG3==> " + m_InbodyData[44] + "\r\n";

            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody430 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody430_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp;

            for (int i = 0; i < 81; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES ==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight ==> " + m_InbodyData[1] + "\r\n";
            strTemp = strTemp + "Evaluation Data==> " + m_InbodyData[2] + "\r\n";
            strTemp = strTemp + "Protein ==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Minreal ==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Body Fat Mass ==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "Soft Lean Body Mass==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass==> " + m_InbodyData[8] + "\r\n";

            strTemp = strTemp + "BMI==> " + m_InbodyData[9] + "\r\n";

            strTemp = strTemp + "Percent Body Fat==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Waist-Hip-Ratio==> " + m_InbodyData[11] + "\r\n";

            strTemp = strTemp + "Segment Lean Right Arm==> " + m_InbodyData[12] + "\r\n";
            strTemp = strTemp + "Segment Lean Left Arm==> " + m_InbodyData[13] + "\r\n";
            strTemp = strTemp + "Segment Lean Trunk==> " + m_InbodyData[14] + "\r\n";
            strTemp = strTemp + "Segment Lean Right Leg==> " + m_InbodyData[15] + "\r\n";
            strTemp = strTemp + "Segment Lean Left Leg==> " + m_InbodyData[16] + "\r\n";


            strTemp = strTemp + "Percent Segment Lean Right Arm ==> " + m_InbodyData[17] + "\r\n";
            strTemp = strTemp + "Percent Segment  Lean Left Arm==> " + m_InbodyData[18] + "\r\n";
            strTemp = strTemp + "Percent Segment Lean Trunk==> " + m_InbodyData[19] + "\r\n";
            strTemp = strTemp + "Percent Segment  Lean Right Leg==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "Percent Segment Lean Left Leg ==> " + m_InbodyData[21] + "\r\n";

            strTemp = strTemp + "Fat Control==> " + m_InbodyData[22] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[23] + "\r\n";

            strTemp = strTemp + "Fitness Score==> " + m_InbodyData[24] + "\r\n";

            strTemp = strTemp + "BMR==> " + m_InbodyData[25] + "\r\n";

            strTemp = strTemp + "HIP==> " + m_InbodyData[26] + "\r\n";

            strTemp = strTemp + "VFA==> " + m_InbodyData[27] + "\r\n";


            strTemp = strTemp + "Segment Fat Right Arm ==> " + m_InbodyData[28] + "\r\n";
            strTemp = strTemp + "Segment Fat Left Arm==> " + m_InbodyData[29] + "\r\n";
            strTemp = strTemp + "Segment Fat Trunk==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "Segment Fat Right Leg==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Segment Fat Left Leg ==> " + m_InbodyData[32] + "\r\n";


            strTemp = strTemp + "IRA5==> " + m_InbodyData[33] + "\r\n";
            strTemp = strTemp + "ILA5==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "IT5==> " + m_InbodyData[35] + "\r\n";
            strTemp = strTemp + "IRL5==> " + m_InbodyData[36] + "\r\n";
            strTemp = strTemp + "ILL5==> " + m_InbodyData[37] + "\r\n";
            strTemp = strTemp + "IRA50==> " + m_InbodyData[38] + "\r\n";
            strTemp = strTemp + "ILA50==> " + m_InbodyData[39] + "\r\n";
            strTemp = strTemp + "IT50==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "IRL50==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "ILL50==> " + m_InbodyData[42] + "\r\n";

            strTemp = strTemp + "IRA250==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "ILA250==> " + m_InbodyData[44] + "\r\n";
            strTemp = strTemp + "IT250==> " + m_InbodyData[45] + "\r\n";
            strTemp = strTemp + "IRL250==> " + m_InbodyData[46] + "\r\n";
            strTemp = strTemp + "ILL250==> " + m_InbodyData[47] + "\r\n";

            strTemp = strTemp + "PROTEIN MAX==> " + m_InbodyData[48] + "\r\n";
            strTemp = strTemp + "PROTEIN MIN==> " + m_InbodyData[49] + "\r\n";
            strTemp = strTemp + "MINERAL MAX==> " + m_InbodyData[50] + "\r\n";
            strTemp = strTemp + "MINERAL MIN==> " + m_InbodyData[51] + "\r\n";


            strTemp = strTemp + "Weight MAX==> " + m_InbodyData[52] + "\r\n";
            strTemp = strTemp + "Weight MIN==> " + m_InbodyData[53] + "\r\n";

            strTemp = strTemp + "Skeletal Muscle Mass MAX==> " + m_InbodyData[54] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MIN==> " + m_InbodyData[55] + "\r\n";
            strTemp = strTemp + "BMI_MAX==> " + m_InbodyData[56] + "\r\n";
            strTemp = strTemp + "BMI_MIN==> " + m_InbodyData[57] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MAX==> " + m_InbodyData[58] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MIN==> " + m_InbodyData[59] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MAX==> " + m_InbodyData[60] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MIN==> " + m_InbodyData[61] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MIN==> " + m_InbodyData[62] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MAX==> " + m_InbodyData[63] + "\r\n";

            strTemp = strTemp + "BMR MIN==> " + m_InbodyData[64] + "\r\n";
            strTemp = strTemp + "BMR MAX==> " + m_InbodyData[65] + "\r\n";

            strTemp = strTemp + "Total Body Water MAX==> " + m_InbodyData[67] + "\r\n";
            strTemp = strTemp + "Total Body Water MIN==> " + m_InbodyData[68] + "\r\n";

            strTemp = strTemp + "Percent Segment Fat Right Arm==> " + m_InbodyData[71] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Left Arm==> " + m_InbodyData[72] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Trunk==> " + m_InbodyData[73] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Right Leg==> " + m_InbodyData[74] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Left Leg==> " + m_InbodyData[75] + "\r\n";
            strTemp = strTemp + "ETYPE3==> " + m_InbodyData[76] + "\r\n";

            strTemp = strTemp + "Soft Lean Body Mass MIN==> " + m_InbodyData[78] + "\r\n";
            strTemp = strTemp + "Soft Lean Body Mass MAX==> " + m_InbodyData[79] + "\r\n";
            //textBox2.Text = strTemp;
        }

        /// <summary>
        /// Inbody230 Data
        /// </summary>
        /// <param name="strData"></param>
        private void InBody230_Data(string strData)
        {
            string[] m_InbodyData = new string[300];
            int intPos1 = 0, intPos2 = -1;
            string strTemp = string.Empty;

            for (int i = 0; i < 68; i++)
            {
                m_InbodyData[i] = "0";
                intPos1 = intPos2 + 1;
                intPos2 = strData.IndexOf((char)27, intPos1, strData.Length - intPos1);
                if (intPos2 > 0)
                    m_InbodyData[i] = strData.Substring(intPos1, intPos2 - intPos1);

            }

            strTemp = "DATETIMES ==> " + m_InbodyData[0] + "\r\n";
            strTemp = strTemp + "Weight ==> " + m_InbodyData[1] + "\r\n";

            strTemp = strTemp + "Body Fat Mass ==> " + m_InbodyData[2] + "\r\n";
            strTemp = strTemp + "Total Body Water==> " + m_InbodyData[3] + "\r\n";
            strTemp = strTemp + "Fat Free Mass==> " + m_InbodyData[4] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass==> " + m_InbodyData[5] + "\r\n";
            strTemp = strTemp + "BMI==> " + m_InbodyData[6] + "\r\n";
            strTemp = strTemp + "Percent Body Fat==> " + m_InbodyData[7] + "\r\n";
            strTemp = strTemp + "Waist-Hip-Ratio==> " + m_InbodyData[8] + "\r\n";
            strTemp = strTemp + "Segment Lean Right Arm==> " + m_InbodyData[9] + "\r\n";
            strTemp = strTemp + "Segment Lean Left Arm==> " + m_InbodyData[10] + "\r\n";
            strTemp = strTemp + "Segment Lean Trunk==> " + m_InbodyData[11] + "\r\n";
            strTemp = strTemp + "Segment Lean Right Leg==> " + m_InbodyData[12] + "\r\n";
            strTemp = strTemp + "Segment Lean Left Leg==> " + m_InbodyData[13] + "\r\n";

            strTemp = strTemp + "Fat Control==> " + m_InbodyData[14] + "\r\n";
            strTemp = strTemp + "Muscle Control==> " + m_InbodyData[15] + "\r\n";
            strTemp = strTemp + "BMR==> " + m_InbodyData[16] + "\r\n";
            strTemp = strTemp + "Lean Right Arm ==> " + m_InbodyData[17] + "\r\n";
            strTemp = strTemp + "Lean Left Arm==> " + m_InbodyData[18] + "\r\n";
            strTemp = strTemp + "Lean Trunk==> " + m_InbodyData[19] + "\r\n";
            strTemp = strTemp + "Lean Right Leg==> " + m_InbodyData[20] + "\r\n";
            strTemp = strTemp + "Lean Left Leg ==> " + m_InbodyData[21] + "\r\n";

            strTemp = strTemp + "Weight MAX==> " + m_InbodyData[22] + "\r\n";
            strTemp = strTemp + "Weight MIN==> " + m_InbodyData[23] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MAX==> " + m_InbodyData[24] + "\r\n";
            strTemp = strTemp + "Skeletal Muscle Mass MIN==> " + m_InbodyData[25] + "\r\n";
            strTemp = strTemp + "BMI_MAX==> " + m_InbodyData[26] + "\r\n";
            strTemp = strTemp + "BMI_MIN==> " + m_InbodyData[27] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MAX==> " + m_InbodyData[28] + "\r\n";
            strTemp = strTemp + "Percent Body Fat MIN==> " + m_InbodyData[29] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MAX==> " + m_InbodyData[30] + "\r\n";
            strTemp = strTemp + "Waist-Hip Ratio MIN==> " + m_InbodyData[31] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MIN==> " + m_InbodyData[32] + "\r\n";
            strTemp = strTemp + "Body Fat Mass MAX==> " + m_InbodyData[33] + "\r\n";

            strTemp = strTemp + "BMR MIN==> " + m_InbodyData[34] + "\r\n";
            strTemp = strTemp + "BMR MAX==> " + m_InbodyData[35] + "\r\n";
            strTemp = strTemp + "IRA20==> " + m_InbodyData[36] + "\r\n";
            strTemp = strTemp + "ILA20==> " + m_InbodyData[37] + "\r\n";
            strTemp = strTemp + "IT20==> " + m_InbodyData[38] + "\r\n";
            strTemp = strTemp + "IRL20==> " + m_InbodyData[39] + "\r\n";
            strTemp = strTemp + "ILL20==> " + m_InbodyData[40] + "\r\n";
            strTemp = strTemp + "IRA100==> " + m_InbodyData[41] + "\r\n";
            strTemp = strTemp + "ILA100==> " + m_InbodyData[42] + "\r\n";
            strTemp = strTemp + "IT100==> " + m_InbodyData[43] + "\r\n";
            strTemp = strTemp + "IRL100==> " + m_InbodyData[44] + "\r\n";
            strTemp = strTemp + "ILL100==> " + m_InbodyData[45] + "\r\n";
            strTemp = strTemp + "RBMR==> " + m_InbodyData[46] + "\r\n";
            strTemp = strTemp + "Total Body Water MAX==> " + m_InbodyData[47] + "\r\n";
            strTemp = strTemp + "Total Body Water MIN==> " + m_InbodyData[48] + "\r\n";
            strTemp = strTemp + "Fat Free Mass MAX==> " + m_InbodyData[49] + "\r\n";
            strTemp = strTemp + "Fat Free Mass MIN==> " + m_InbodyData[50] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Right Arm==> " + m_InbodyData[51] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Left Arm==> " + m_InbodyData[52] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Trunk==> " + m_InbodyData[53] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Right Leg==> " + m_InbodyData[54] + "\r\n";
            strTemp = strTemp + "Percent Segment Fat Left Leg==> " + m_InbodyData[55] + "\r\n";
            strTemp = strTemp + "ETYPE3==> " + m_InbodyData[56] + "\r\n";

            // 추가된 데이터 키, 단백질 Max, Min 무기질 Max, Min, 혈압데이터
            strTemp = strTemp + "Height==> " + m_InbodyData[58] + "\r\n";

            strTemp = strTemp + "Protein==> " + m_InbodyData[59] + "\r\n";
            strTemp = strTemp + "Mineral==> " + m_InbodyData[60] + "\r\n";

            strTemp = strTemp + "Protein Max==> " + m_InbodyData[61] + "\r\n";
            strTemp = strTemp + "Protein Min==> " + m_InbodyData[62] + "\r\n";
            strTemp = strTemp + "Mineral Max==> " + m_InbodyData[63] + "\r\n";
            strTemp = strTemp + "Mineral Min==> " + m_InbodyData[64] + "\r\n";

            strTemp = strTemp + "STHYG1==> " + m_InbodyData[65] + "\r\n";
            strTemp = strTemp + "STHYG2==> " + m_InbodyData[66] + "\r\n";
            strTemp = strTemp + "STHYG3==> " + m_InbodyData[67] + "\r\n";

            //textBox2.Text = strTemp;
        }

        #endregion ParsingInBodyData

        #region UnitConvertor

        // Changed Unit
        private string ConvertCmToInNoUnit(string data, int iUnit)
        {
            if (iUnit == 1)
                return (Convert.ToSingle(data) * 0.393701f * 0.393701f).ToString("0.00");
            else
                return data;
        }

        private string ConvertKgTolb(string data, int iUnit)
        {
            if (iUnit == 1)
                return (Convert.ToSingle(data) * 2.20459f).ToString("0.0");
            else
                return data;
        }

        private string GetGradeId(String grade, String gubun2)
        {
            try
            {
                if (grade.Contains("3세"))
                {
                    return "-5";
                }
                if (grade.Contains("4세"))
                {
                    return "-4";
                }
                if (grade.Contains("5세"))
                {
                    return "-3";
                }
                if (grade.Contains("6세"))
                {
                    return "-2";
                }
                if (grade.Contains("7세"))
                {
                    return "-1";
                }

                int gradeID = int.Parse(grade);

                if (gubun2.StartsWith("중학"))
                {
                    gradeID += 6;
                }
                else if (gubun2.StartsWith("고등"))
                {
                    gradeID += 9;
                }

                if (gradeID > 12)
                {
                    gradeID = 12;
                }

                if (gradeID <= 0)
                {
                    gradeID = 1;
                }

                return gradeID.ToString();
            }
            catch (Exception e)
            {
                return grade;
            }
        }

        #endregion UnitConvertor

        private void btnReuseSmoke_Click(object sender, EventArgs e)
        {
            int selectedIndex = dgvSmokeMemberHistory.CurrentCell.RowIndex;

            if (selectedIndex >= 0)
            {
                this.txtPPM.Text = this.dgvSmokeMemberHistory.Rows[selectedIndex].Cells[8].Value.ToString();
                this.txtCOHD.Text = this.dgvSmokeMemberHistory.Rows[selectedIndex].Cells[9].Value.ToString();
            }
            else
            {
                mainForm.ShowNotice("선택된 측정 이력 정보가 없습니다.");
            }
        }

        private void spSmoke_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            mainForm.ShowNotice("1.", 1000);
        }











    }
}
