namespace HealthCare
{
	partial class LoginControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginControl));
			this.btnLogin = new System.Windows.Forms.Button();
			this.lblID = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtID = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.picLoginImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picLoginImage)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLogin
			// 
			this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLogin.Font = new System.Drawing.Font("휴먼엑스포", 9.75F);
			this.btnLogin.Location = new System.Drawing.Point(818, 376);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(121, 48);
			this.btnLogin.TabIndex = 5;
			this.btnLogin.Text = "로그인";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// lblID
			// 
			this.lblID.AutoSize = true;
			this.lblID.Font = new System.Drawing.Font("휴먼엑스포", 9.75F);
			this.lblID.Location = new System.Drawing.Point(521, 377);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(46, 14);
			this.lblID.TabIndex = 3;
			this.lblID.Text = "아이디";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Font = new System.Drawing.Font("휴먼엑스포", 9.75F);
			this.lblPassword.Location = new System.Drawing.Point(521, 404);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(59, 14);
			this.lblPassword.TabIndex = 4;
			this.lblPassword.Text = "비밀번호";
			// 
			// txtID
			// 
			this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID.Location = new System.Drawing.Point(600, 376);
			this.txtID.MaxLength = 20;
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(198, 21);
			this.txtID.TabIndex = 1;
			this.txtID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyUp);
			// 
			// txtPassword
			// 
			this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPassword.Location = new System.Drawing.Point(600, 403);
			this.txtPassword.MaxLength = 20;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(198, 21);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyUp);
			// 
			// picLoginImage
			// 
			this.picLoginImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLoginImage.BackgroundImage")));
			this.picLoginImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.picLoginImage.Location = new System.Drawing.Point(524, 62);
			this.picLoginImage.Name = "picLoginImage";
			this.picLoginImage.Size = new System.Drawing.Size(415, 294);
			this.picLoginImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picLoginImage.TabIndex = 0;
			this.picLoginImage.TabStop = false;
			// 
			// LoginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.picLoginImage);
			this.Controls.Add(this.lblID);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtID);
			this.Controls.Add(this.txtPassword);
			this.Name = "LoginControl";
			this.Size = new System.Drawing.Size(1431, 609);
			this.Load += new System.EventHandler(this.LoginControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.picLoginImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picLoginImage;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtID;
		private System.Windows.Forms.TextBox txtPassword;
	}
}
