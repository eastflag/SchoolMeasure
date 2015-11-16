using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HealthCare
{
	public partial class LoginControl : UserControl
	{
		public LoginControl()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			frmMain mainForm = this.ParentForm as frmMain;

			if (String.IsNullOrWhiteSpace(txtID.Text))
			{
				mainForm.ShowNotice("로그인 아이디를 입력하세요.");
				return;
			}
			else if (String.IsNullOrWhiteSpace(txtPassword.Text))
			{
				mainForm.ShowNotice("로그인 비밀번호를 입력하세요.");
				return;
			}

			AdoUtil adoUtil = new AdoUtil(mainForm);
			DataTable dt = adoUtil.SelectLoginInfo(txtID.Text, txtPassword.Text);

			if (dt != null && dt.Rows.Count > 0)
			{
				int role_id = int.Parse(dt.Rows[0]["role_id"].ToString());
				int status = int.Parse(dt.Rows[0]["status"].ToString());
			
				if (status == 0 && (role_id == 1 || role_id == 3))
				{
					txtID.Text = String.Empty;
					txtPassword.Text = String.Empty;

					mainForm._isLogin = true;

					//mainForm.btnLogout.Visible = true;
					//mainForm.btnLogout.Show();

					// 인바디 측정 화면 로드
					mainForm.LoadInbodyControl();
				}
				else
				{
					txtID.Text = "";
					txtPassword.Text = "";

					mainForm.ShowNotice("권한이 없거나 계정의 상태가 부적합합니다.");
				}
			}
			else
			{
				txtID.Text = "";
				txtPassword.Text = "";

				mainForm.ShowNotice("아이디 혹은 비밀번호가 일치하지 않습니다. 다시 입력해 주십시요.");
			}
		}

		private void txtID_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				txtPassword.Focus();
			}
		}

		private void txtPassword_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnLogin_Click(null, null);
			}
		}

		private void LoginControl_Load(object sender, EventArgs e)
		{
			txtID.Focus();
		}
	}
}
