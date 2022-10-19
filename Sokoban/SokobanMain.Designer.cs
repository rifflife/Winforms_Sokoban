namespace Sokoban
{
	partial class SokobanMain
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
			this.components = new System.ComponentModel.Container();
			this.GameTick = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// GameTick
			// 
			this.GameTick.Enabled = true;
			this.GameTick.Interval = 5;
			this.GameTick.Tick += new System.EventHandler(this.GameTick_Tick);
			// 
			// SokobanMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1280, 720);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SokobanMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sokoban";
			this.Activated += new System.EventHandler(this.SokobanMain_Activated);
			this.Deactivate += new System.EventHandler(this.SokobanMain_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SokobanMain_FormClosing);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SokobanMain_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SokobanMain_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SokobanMain_KeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SokobanMain_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SokobanMain_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer GameTick;
	}
}

