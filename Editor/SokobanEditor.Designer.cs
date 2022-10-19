namespace Sokoban
{
	partial class SokobanEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SokobanEditor));
			this.EditorTick = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.Button_DeleteMap = new System.Windows.Forms.Button();
			this.Button_CreateMap = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.Button_Empty = new System.Windows.Forms.Button();
			this.Button_Wall = new System.Windows.Forms.Button();
			this.Button_Box = new System.Windows.Forms.Button();
			this.Button_Goal = new System.Windows.Forms.Button();
			this.Button_Player = new System.Windows.Forms.Button();
			this.Button_Theme_0 = new System.Windows.Forms.Button();
			this.Button_Theme_1 = new System.Windows.Forms.Button();
			this.Button_Theme_2 = new System.Windows.Forms.Button();
			this.Button_Theme_3 = new System.Windows.Forms.Button();
			this.Button_Theme_4 = new System.Windows.Forms.Button();
			this.Button_Initialize = new System.Windows.Forms.Button();
			this.Button_Save = new System.Windows.Forms.Button();
			this.MapDataListBox = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// EditorTick
			// 
			this.EditorTick.Enabled = true;
			this.EditorTick.Interval = 10;
			this.EditorTick.Tick += new System.EventHandler(this.EditorTick_Tick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Controls.Add(this.Button_DeleteMap, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.Button_CreateMap, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 681);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1284, 80);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// Button_DeleteMap
			// 
			this.Button_DeleteMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Button_DeleteMap.Location = new System.Drawing.Point(1187, 3);
			this.Button_DeleteMap.Name = "Button_DeleteMap";
			this.Button_DeleteMap.Size = new System.Drawing.Size(94, 74);
			this.Button_DeleteMap.TabIndex = 1;
			this.Button_DeleteMap.Text = "삭제";
			this.Button_DeleteMap.UseVisualStyleBackColor = true;
			this.Button_DeleteMap.Click += new System.EventHandler(this.Button_DeleteMap_Click);
			// 
			// Button_CreateMap
			// 
			this.Button_CreateMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Button_CreateMap.Location = new System.Drawing.Point(1087, 3);
			this.Button_CreateMap.Name = "Button_CreateMap";
			this.Button_CreateMap.Size = new System.Drawing.Size(94, 74);
			this.Button_CreateMap.TabIndex = 0;
			this.Button_CreateMap.Text = "새로 생성";
			this.Button_CreateMap.UseVisualStyleBackColor = true;
			this.Button_CreateMap.Click += new System.EventHandler(this.Button_CreateMap_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.Button_Empty);
			this.flowLayoutPanel1.Controls.Add(this.Button_Wall);
			this.flowLayoutPanel1.Controls.Add(this.Button_Box);
			this.flowLayoutPanel1.Controls.Add(this.Button_Goal);
			this.flowLayoutPanel1.Controls.Add(this.Button_Player);
			this.flowLayoutPanel1.Controls.Add(this.Button_Theme_0);
			this.flowLayoutPanel1.Controls.Add(this.Button_Theme_1);
			this.flowLayoutPanel1.Controls.Add(this.Button_Theme_2);
			this.flowLayoutPanel1.Controls.Add(this.Button_Theme_3);
			this.flowLayoutPanel1.Controls.Add(this.Button_Theme_4);
			this.flowLayoutPanel1.Controls.Add(this.Button_Initialize);
			this.flowLayoutPanel1.Controls.Add(this.Button_Save);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1078, 74);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// Button_Empty
			// 
			this.Button_Empty.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Empty.BackgroundImage")));
			this.Button_Empty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Empty.Location = new System.Drawing.Point(3, 3);
			this.Button_Empty.Name = "Button_Empty";
			this.Button_Empty.Size = new System.Drawing.Size(70, 70);
			this.Button_Empty.TabIndex = 2;
			this.Button_Empty.UseVisualStyleBackColor = true;
			this.Button_Empty.Click += new System.EventHandler(this.Button_Empty_Click);
			// 
			// Button_Wall
			// 
			this.Button_Wall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Wall.BackgroundImage")));
			this.Button_Wall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Wall.Location = new System.Drawing.Point(79, 3);
			this.Button_Wall.Name = "Button_Wall";
			this.Button_Wall.Size = new System.Drawing.Size(70, 70);
			this.Button_Wall.TabIndex = 3;
			this.Button_Wall.UseVisualStyleBackColor = true;
			this.Button_Wall.Click += new System.EventHandler(this.Button_Wall_Click);
			// 
			// Button_Box
			// 
			this.Button_Box.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Box.BackgroundImage")));
			this.Button_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.Button_Box.Location = new System.Drawing.Point(155, 3);
			this.Button_Box.Name = "Button_Box";
			this.Button_Box.Size = new System.Drawing.Size(70, 70);
			this.Button_Box.TabIndex = 0;
			this.Button_Box.UseVisualStyleBackColor = true;
			this.Button_Box.Click += new System.EventHandler(this.Button_Box_Click);
			// 
			// Button_Goal
			// 
			this.Button_Goal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Goal.BackgroundImage")));
			this.Button_Goal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.Button_Goal.Location = new System.Drawing.Point(231, 3);
			this.Button_Goal.Name = "Button_Goal";
			this.Button_Goal.Size = new System.Drawing.Size(70, 70);
			this.Button_Goal.TabIndex = 1;
			this.Button_Goal.UseVisualStyleBackColor = true;
			this.Button_Goal.Click += new System.EventHandler(this.Button_Goal_Click);
			// 
			// Button_Player
			// 
			this.Button_Player.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Player.BackgroundImage")));
			this.Button_Player.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Player.Location = new System.Drawing.Point(307, 3);
			this.Button_Player.Name = "Button_Player";
			this.Button_Player.Size = new System.Drawing.Size(70, 70);
			this.Button_Player.TabIndex = 4;
			this.Button_Player.UseVisualStyleBackColor = true;
			this.Button_Player.Click += new System.EventHandler(this.Button_Player_Click);
			// 
			// Button_Theme_0
			// 
			this.Button_Theme_0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Theme_0.BackgroundImage")));
			this.Button_Theme_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Theme_0.Location = new System.Drawing.Point(383, 3);
			this.Button_Theme_0.Name = "Button_Theme_0";
			this.Button_Theme_0.Size = new System.Drawing.Size(70, 70);
			this.Button_Theme_0.TabIndex = 7;
			this.Button_Theme_0.UseVisualStyleBackColor = true;
			this.Button_Theme_0.Click += new System.EventHandler(this.Button_Theme_0_Click);
			// 
			// Button_Theme_1
			// 
			this.Button_Theme_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Theme_1.BackgroundImage")));
			this.Button_Theme_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Theme_1.Location = new System.Drawing.Point(459, 3);
			this.Button_Theme_1.Name = "Button_Theme_1";
			this.Button_Theme_1.Size = new System.Drawing.Size(70, 70);
			this.Button_Theme_1.TabIndex = 8;
			this.Button_Theme_1.UseVisualStyleBackColor = true;
			this.Button_Theme_1.Click += new System.EventHandler(this.Button_Theme_1_Click);
			// 
			// Button_Theme_2
			// 
			this.Button_Theme_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Theme_2.BackgroundImage")));
			this.Button_Theme_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Theme_2.Location = new System.Drawing.Point(535, 3);
			this.Button_Theme_2.Name = "Button_Theme_2";
			this.Button_Theme_2.Size = new System.Drawing.Size(70, 70);
			this.Button_Theme_2.TabIndex = 9;
			this.Button_Theme_2.UseVisualStyleBackColor = true;
			this.Button_Theme_2.Click += new System.EventHandler(this.Button_Theme_2_Click);
			// 
			// Button_Theme_3
			// 
			this.Button_Theme_3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Theme_3.BackgroundImage")));
			this.Button_Theme_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Theme_3.Location = new System.Drawing.Point(611, 3);
			this.Button_Theme_3.Name = "Button_Theme_3";
			this.Button_Theme_3.Size = new System.Drawing.Size(70, 70);
			this.Button_Theme_3.TabIndex = 10;
			this.Button_Theme_3.UseVisualStyleBackColor = true;
			this.Button_Theme_3.Click += new System.EventHandler(this.Button_Theme_3_Click);
			// 
			// Button_Theme_4
			// 
			this.Button_Theme_4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Theme_4.BackgroundImage")));
			this.Button_Theme_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Button_Theme_4.Location = new System.Drawing.Point(687, 3);
			this.Button_Theme_4.Name = "Button_Theme_4";
			this.Button_Theme_4.Size = new System.Drawing.Size(70, 70);
			this.Button_Theme_4.TabIndex = 11;
			this.Button_Theme_4.UseVisualStyleBackColor = true;
			this.Button_Theme_4.Click += new System.EventHandler(this.Button_Theme_4_Click);
			// 
			// Button_Initialize
			// 
			this.Button_Initialize.Location = new System.Drawing.Point(763, 3);
			this.Button_Initialize.Name = "Button_Initialize";
			this.Button_Initialize.Size = new System.Drawing.Size(70, 70);
			this.Button_Initialize.TabIndex = 5;
			this.Button_Initialize.Text = "초기화";
			this.Button_Initialize.UseVisualStyleBackColor = true;
			this.Button_Initialize.Click += new System.EventHandler(this.Button_Initialize_Click);
			// 
			// Button_Save
			// 
			this.Button_Save.Location = new System.Drawing.Point(839, 3);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(56, 70);
			this.Button_Save.TabIndex = 6;
			this.Button_Save.Text = "저장";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// MapDataListBox
			// 
			this.MapDataListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MapDataListBox.FormattingEnabled = true;
			this.MapDataListBox.ItemHeight = 12;
			this.MapDataListBox.Location = new System.Drawing.Point(3, 3);
			this.MapDataListBox.Name = "MapDataListBox";
			this.MapDataListBox.ScrollAlwaysVisible = true;
			this.MapDataListBox.Size = new System.Drawing.Size(194, 675);
			this.MapDataListBox.TabIndex = 0;
			this.MapDataListBox.SelectedIndexChanged += new System.EventHandler(this.MapDataListBox_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.MapDataListBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(1084, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3);
			this.panel1.Size = new System.Drawing.Size(200, 681);
			this.panel1.TabIndex = 1;
			// 
			// SokobanEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1284, 761);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.KeyPreview = true;
			this.Name = "SokobanEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sokoban Editor";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SokobanEditor_FormClosed);
			this.Load += new System.EventHandler(this.SokobanEditor_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SokobanEditor_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SokobanEditor_KeyDown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SokobanEditor_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SokobanEditor_MouseUp);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer EditorTick;
		private System.Windows.Forms.ListBox MapDataListBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button Button_DeleteMap;
		private System.Windows.Forms.Button Button_CreateMap;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button Button_Player;
		private System.Windows.Forms.Button Button_Goal;
		private System.Windows.Forms.Button Button_Box;
		private System.Windows.Forms.Button Button_Empty;
		private System.Windows.Forms.Button Button_Wall;
		private System.Windows.Forms.Button Button_Initialize;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button Button_Theme_0;
		private System.Windows.Forms.Button Button_Theme_1;
		private System.Windows.Forms.Button Button_Theme_2;
		private System.Windows.Forms.Button Button_Theme_3;
		private System.Windows.Forms.Button Button_Theme_4;
	}
}