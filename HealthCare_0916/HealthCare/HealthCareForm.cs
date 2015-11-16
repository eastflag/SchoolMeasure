using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HealthCare
{
	public partial class frmMain : Form
	{
		public bool _isLogin = false;
		public bool _isError = false;
		private LoginControl loginControl = null;
		private InbodyControl inbodyControl = null;
		private int timer_limit_count = 0;

		public frmMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 로드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_Load(object sender, EventArgs e)
		{
			this.btnLogout.Visible = false;

			LoadLoginControl();			
		}

		/// <summary>
		/// Notice 닫기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNoticeClose_Click(object sender, EventArgs e)
		{
			CloseNotice();
		}

		/// <summary>
		/// 로그 아웃 버튼 클릭 시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLogout_Click(object sender, EventArgs e)
		{
			this._isLogin = false;

			this.btnLogout.Visible = false;
			this.btnLogout.Hide();

			LoadLoginControl();
		}

		/// <summary>
		/// 종료 버튼 클릭 시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 로그인 컨트롤 로드
		/// </summary>
		public void LoadLoginControl()
		{
			loginControl = new LoginControl();
						
			loginControl.Dock = DockStyle.Top;
			loginControl.Size = new Size(this.pnl_bottom.Width, this.pnl_bottom.Height);
			loginControl.Anchor = AnchorStyles.Top;
			loginControl.AutoSize = true;

			if (this.inbodyControl != null)
			{
				this.pnl_bottom.Controls.Remove(this.inbodyControl);
			}
			
			this.pnl_bottom.Controls.Add(loginControl);

			loginControl.Show();
		}

		/// <summary>
		/// 인바디 측정 컨트롤 로드
		/// </summary>
		public void LoadInbodyControl()
		{
			inbodyControl = new InbodyControl();

			inbodyControl.Dock = DockStyle.Top;
			inbodyControl.Size = new Size(this.pnl_bottom.Width, this.pnl_bottom.Height);
			inbodyControl.Anchor = AnchorStyles.Top;
			inbodyControl.AutoSize = true;

			if (this.loginControl != null)
			{
				this.pnl_bottom.Controls.Remove(this.loginControl);
			}

			this.pnl_bottom.Controls.Add(inbodyControl);

			inbodyControl.Show();
			inbodyControl.InitData();
		}

		/// <summary>
		/// Notice 표시
		/// </summary>
		/// <param name="notice"></param>
		public void ShowNotice(string notice, int closeTime = 0)
		{
			this.lblNotice.Text = notice;

			this.pnlNotice.Visible = true;
			this.pnlNotice.Show();

			if (closeTime > 0)
			{
				Timer timer = new Timer();
				timer.Interval = 500;
				timer.Tick += (sender, e) => 
				{
					if (timer_limit_count >= closeTime)
					{
						CloseNotice();

						timer.Stop();
						timer_limit_count = 0;
					}	
					else
					{
						timer_limit_count += 500;
					}
				};

				timer.Start();
			}
		}

		/// <summary>
		/// Notice 숨김
		/// </summary>
		public void CloseNotice()
		{
			this.lblNotice.Text = String.Empty;

			this.pnlNotice.Visible = false;
			this.pnlNotice.Hide();
		}

		
	}
}
