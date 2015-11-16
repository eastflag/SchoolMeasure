namespace HealthCare
{
	partial class frmMain
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
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.pnl_top = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnLogout = new System.Windows.Forms.Button();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.pnl_bottom = new System.Windows.Forms.Panel();
			this.pnlNotice = new System.Windows.Forms.Panel();
			this.btnNoticeClose = new System.Windows.Forms.Button();
			this.lblNotice = new System.Windows.Forms.Label();
			this.pnl_top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.pnl_bottom.SuspendLayout();
			this.pnlNotice.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl_top
			// 
			this.pnl_top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl_top.Controls.Add(this.btnExit);
			this.pnl_top.Controls.Add(this.btnLogout);
			this.pnl_top.Controls.Add(this.picLogo);
			this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_top.Location = new System.Drawing.Point(0, 0);
			this.pnl_top.Name = "pnl_top";
			this.pnl_top.Size = new System.Drawing.Size(1354, 71);
			this.pnl_top.TabIndex = 1;
			// 
			// btnExit
			// 
			this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExit.Location = new System.Drawing.Point(1251, 3);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(98, 63);
			this.btnExit.TabIndex = 2;
			this.btnExit.Text = "종료";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnLogout
			// 
			this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLogout.Location = new System.Drawing.Point(1147, 3);
			this.btnLogout.Name = "btnLogout";
			this.btnLogout.Size = new System.Drawing.Size(98, 63);
			this.btnLogout.TabIndex = 1;
			this.btnLogout.Text = "로그아웃";
			this.btnLogout.UseVisualStyleBackColor = true;
			this.btnLogout.Visible = false;
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			// 
			// picLogo
			// 
			this.picLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLogo.BackgroundImage")));
			this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.picLogo.Location = new System.Drawing.Point(3, 3);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(206, 63);
			this.picLogo.TabIndex = 0;
			this.picLogo.TabStop = false;
			// 
			// pnl_bottom
			// 
			this.pnl_bottom.Controls.Add(this.pnlNotice);
			this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnl_bottom.Location = new System.Drawing.Point(0, 73);
			this.pnl_bottom.Name = "pnl_bottom";
			this.pnl_bottom.Size = new System.Drawing.Size(1354, 563);
			this.pnl_bottom.TabIndex = 2;
			// 
			// pnlNotice
			// 
			this.pnlNotice.BackColor = System.Drawing.Color.White;
			this.pnlNotice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlNotice.Controls.Add(this.btnNoticeClose);
			this.pnlNotice.Controls.Add(this.lblNotice);
			this.pnlNotice.Font = new System.Drawing.Font("굴림", 9F);
			this.pnlNotice.Location = new System.Drawing.Point(435, 181);
			this.pnlNotice.Name = "pnlNotice";
			this.pnlNotice.Size = new System.Drawing.Size(577, 97);
			this.pnlNotice.TabIndex = 0;
			this.pnlNotice.Visible = false;
			// 
			// btnNoticeClose
			// 
			this.btnNoticeClose.Location = new System.Drawing.Point(434, 22);
			this.btnNoticeClose.Name = "btnNoticeClose";
			this.btnNoticeClose.Size = new System.Drawing.Size(121, 52);
			this.btnNoticeClose.TabIndex = 1;
			this.btnNoticeClose.Text = "닫기";
			this.btnNoticeClose.UseVisualStyleBackColor = true;
			this.btnNoticeClose.Click += new System.EventHandler(this.btnNoticeClose_Click);
			// 
			// lblNotice
			// 
			this.lblNotice.AutoSize = true;
			this.lblNotice.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
			this.lblNotice.ForeColor = System.Drawing.Color.Red;
			this.lblNotice.Location = new System.Drawing.Point(18, 40);
			this.lblNotice.Name = "lblNotice";
			this.lblNotice.Size = new System.Drawing.Size(0, 12);
			this.lblNotice.TabIndex = 0;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1354, 636);
			this.Controls.Add(this.pnl_bottom);
			this.Controls.Add(this.pnl_top);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "헬스케어 측정어플";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.pnl_top.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			this.pnl_bottom.ResumeLayout(false);
			this.pnlNotice.ResumeLayout(false);
			this.pnlNotice.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_top;
		private System.Windows.Forms.PictureBox picLogo;
		private System.Windows.Forms.Button btnNoticeClose;
		public System.Windows.Forms.Label lblNotice;
		public System.Windows.Forms.Panel pnlNotice;
		private System.Windows.Forms.Button btnExit;
		public System.Windows.Forms.Button btnLogout;
		public System.Windows.Forms.Panel pnl_bottom;
	}
}

