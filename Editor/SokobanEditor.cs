using Sokoban.EditorClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// None
// 0 Empty
// 1 Wall
// 2 Goal
// 3 Box
// 4 Player
// 5 Box and Goal
// 6 Player and Goal

namespace Sokoban
{
	public partial class SokobanEditor : Form
	{
		public enum TileType { None = - 1, Empty, Wall, Goal, Box, Player, BAG, PAG };
		private const int CELL = 32;
		private Bitmap BitmapDoubleBuffering;
		private TileType CurrentSelection;

		private MapList EditorMapList;

		private bool RightMousePressed = false;
		private bool LeftMousePressed = false;

		private const int OffsetX = 50;
		private const int OffsetY = 50;

		private int Alpha = 0;

		public SokobanEditor()
		{
			InitializeComponent();

			BitmapDoubleBuffering = new Bitmap(1920, 1080);

			EditorMapList = new MapList(MapDataListBox, CELL);
			if (MapDataListBox.Items.Count > 0)
			{
				MapDataListBox.SelectedIndex = 0;
			}

			Cursor.Show();
		}

		protected override void OnPaintBackground(PaintEventArgs e)	{}
		
		private void SokobanEditor_Paint(object sender, PaintEventArgs e)
		{
			if (BitmapDoubleBuffering != null)
			{
				e.Graphics.DrawImage(BitmapDoubleBuffering, 0, 0);
			}
		}

		private void EditorTick_Tick(object sender, EventArgs e)
		{
			Cursor.Show();
			Graphics graphics = Graphics.FromImage(BitmapDoubleBuffering);
			graphics.Clear(Color.Black);
			
			// 맵 편집
			int CurrentMouseX = this.PointToClient(Cursor.Position).X - OffsetX;
			int CurrentMouseY = this.PointToClient(Cursor.Position).Y - OffsetY;
			int CellX = CurrentMouseX / CELL;
			int CellY = CurrentMouseY / CELL;

			if (CellY < MapData.AllowHeight && CellX < MapData.AllowWidth && CellY >= 0 && CellX >= 0)
			{
				if (LeftMousePressed)
				{
					EditorMapList.SetData(CellX, CellY, (int)CurrentSelection);
					EditorMapList.Update();
				}
				else if (RightMousePressed)
				{
					EditorMapList.SetData(CellX, CellY, -1);
					EditorMapList.Update();
				}
			}

			int MapNamePositionX = CELL * MapData.AllowWidth + OffsetX + 100;
			int MapNamePositionY = 20;
			Font MapFont = new Font("HY견고딕", 20);

			// 맵 그리기
			if (MapDataListBox.SelectedIndex >= 0)
			{
				EditorMapList.Draw(graphics, OffsetX, OffsetY);
				graphics.DrawString(Convert.ToString(MapDataListBox.Items[MapDataListBox.SelectedIndex]), MapFont, Brushes.White, MapNamePositionX, MapNamePositionY);
			}
			else
			{
				graphics.DrawString("맵 선택 안됨", MapFont, Brushes.White, MapNamePositionX, MapNamePositionY);
			}

			// 프리뷰
			string PreviewFile = MapList.MapPreviewDirectory + MapDataListBox.SelectedIndex + ".png";
			FileInfo fi = new FileInfo(PreviewFile);

			if (fi.Exists)
			{
				graphics.DrawString("프리뷰 있음", MapFont, Brushes.White, MapNamePositionX, MapNamePositionY + 40);
			}
			else
			{
				graphics.DrawString("프리뷰 없음", MapFont, Brushes.Red, MapNamePositionX, MapNamePositionY + 40);
			}

			if (Alpha > 2)
			{
				Alpha -= 2;
			}
			else
			{
				Alpha = 0;
			}

			SolidBrush brush = new SolidBrush(Color.FromArgb(Alpha,255,255,255));
			graphics.DrawString("저장함", MapFont, brush, MapNamePositionX, MapNamePositionY + 80);

			Pen pen = new Pen(Color.FromArgb(20, 20, 20));
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			// 그리드 그리기
			for(int x = 0; x <= MapData.AllowWidth; x ++)
			{
				graphics.DrawLine(pen, OffsetX + x * CELL, OffsetY, OffsetX + x * CELL, OffsetY + MapData.AllowHeight * CELL);

				if (x < MapData.AllowWidth)
				{
					graphics.DrawString(x.ToString(), Font, Brushes.Yellow, OffsetX + (CELL / 2) + (x * CELL), OffsetY - 16, sf);
				}
			}
				
			for (int y = 0; y <= MapData.AllowHeight; y ++)
			{
				graphics.DrawLine(pen, OffsetX, OffsetY + y * CELL, OffsetX + MapData.AllowWidth * CELL, OffsetY + y * CELL);
				
				if (y < MapData.AllowHeight)
				{
					graphics.DrawString(y.ToString(), Font, Brushes.Yellow, OffsetX - 15, OffsetY + (CELL / 2) + 1 + y * CELL, sf);
				}
			}

			Invalidate();
		}

		private void Button_Initialize_Click(object sender, EventArgs e)
		{
			EditorMapList.InitailizeData();
		}
		
		private void SokobanEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.S && e.Control)
			{
				EditorMapList.SavePreview();
				EditorMapList.Save();
				Alpha = 255;
			}

			if (e.KeyCode == Keys.D1)
			{
				CurrentSelection = TileType.Empty;
			}
			
			if (e.KeyCode == Keys.D2)
			{
				CurrentSelection = TileType.Wall;
			}
			
			if (e.KeyCode == Keys.D3)
			{
				CurrentSelection = TileType.Box;
			}
				
			if (e.KeyCode == Keys.D4)
			{
				CurrentSelection = TileType.Goal;
			}
			
			if (e.KeyCode == Keys.D5)
			{
				CurrentSelection = TileType.Player;
			}
		}

		private void Button_Save_Click(object sender, EventArgs e)
		{
			EditorMapList.SavePreview();
			EditorMapList.Save();
			Alpha = 255;
		}

		private void Button_Player_Click(object sender, EventArgs e)
		{
			CurrentSelection = TileType.Player;
		}

		private void Button_Goal_Click(object sender, EventArgs e)
		{
			CurrentSelection = TileType.Goal;
		}

		private void Button_Box_Click(object sender, EventArgs e)
		{
			CurrentSelection = TileType.Box;
		}

		private void Button_Empty_Click(object sender, EventArgs e)
		{
			CurrentSelection = TileType.Empty;
		}

		private void Button_Wall_Click(object sender, EventArgs e)
		{
			CurrentSelection = TileType.Wall;
		}

		private void Button_CreateMap_Click(object sender, EventArgs e)
		{
			EditorMapList.CreateNewMap();
		}

		private void Button_DeleteMap_Click(object sender, EventArgs e)
		{
			EditorMapList.DeleteMap();
		}

		private void MapDataListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			EditorMapList.Update();
		}

		private void SokobanEditor_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				LeftMousePressed = true;
				Cursor.Show();
			}
			else if (e.Button == MouseButtons.Right)
			{
				RightMousePressed = true;
				Cursor.Show();
			}
		}

		private void SokobanEditor_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				LeftMousePressed = false;
			}
			else if (e.Button == MouseButtons.Right)
			{
				RightMousePressed = false;
			}
		}

		private void Button_Theme_0_Click(object sender, EventArgs e)
		{
			EditorMapList.SetTheme(0);
		}

		private void Button_Theme_1_Click(object sender, EventArgs e)
		{
			EditorMapList.SetTheme(1);
		}

		private void Button_Theme_2_Click(object sender, EventArgs e)
		{
			EditorMapList.SetTheme(2);
		}

		private void Button_Theme_3_Click(object sender, EventArgs e)
		{
			EditorMapList.SetTheme(3);
		}

		private void Button_Theme_4_Click(object sender, EventArgs e)
		{
			EditorMapList.SetTheme(4);
		}

		private void SokobanEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			System.GC.Collect();
			Process.Start("Sokoban.exe");
		}

		private void SokobanEditor_Load(object sender, EventArgs e)
		{
			this.Focus();
		}
	}
}
