using Sokoban.GameClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// #############################################################################
// 레벨 계수 1부터 시작
// 레벨 가로 21
// 레벨 세로 18
// 윈도우 사이즈 800 + 16 / 600 + 39
// 임시 맵 최대 한도 500
// #############################################################################

namespace Sokoban
{
	public partial class SokobanMain : Form
	{
		// General Setting
		private const int SCREEN_WIDTH = 1280;
		private const int SCREEN_HEIGHT = 720;
		private Bitmap BitmapDoubleBuffering;
		private GameSystem gameSystem;
		static public Color[] BackgroundColorSet;

		// Images
		private Image CursorImage = Image.FromFile(@"Image/Cursor_1.png");

		// Keyboard Input
		public bool KeyInputRight = false;
		public bool KeyInputUp = false;
		public bool KeyInputLeft = false;
		public bool KeyInputDown = false;
		public bool KeyInputESC = false;
		public bool KeyInputR = false;

		// Mouse Input
		private bool LeftMousePressed = false;
		private bool RightMousePressed = false;
		private int MousePositionX = 0;
		private int MousePositionY = 0;
		private bool IsActived = true;

		public SokobanMain()
		{
			InitializeComponent();

			BackgroundColorSet = new Color[5];
			BackgroundColorSet[0] = Color.FromArgb(139, 127, 99);
			BackgroundColorSet[1] = Color.FromArgb(101, 75, 51);
			BackgroundColorSet[2] = Color.FromArgb(94, 94, 94);
			BackgroundColorSet[3] = Color.FromArgb(90, 43, 43);
			BackgroundColorSet[4] = Color.FromArgb(35, 35, 35);

			BitmapDoubleBuffering = new Bitmap(ClientSize.Width, ClientSize.Height);

			gameSystem = new GameSystem(this);
			
			Cursor.Hide();
		}

		protected override void OnPaintBackground(PaintEventArgs e)	{}
		
		private void GameTick_Tick(object sender, EventArgs e)
		{
			if (IsActived)
			{
				Cursor.Hide();
			}
			else
			{
				Cursor.Show();
			}

			MousePositionX = this.PointToClient(Cursor.Position).X;;
			MousePositionY = this.PointToClient(Cursor.Position).Y;;

			Graphics graphics = Graphics.FromImage(BitmapDoubleBuffering);

			graphics.Clear(Color.Blue);

			gameSystem.Update();
			gameSystem.Draw(graphics);
			//gameSystem.KeyInput(KeyInputRight, KeyInputUp, KeyInputLeft, KeyInputDown);
			
			// Cursor Draw
			graphics.DrawImage(CursorImage, MousePositionX, MousePositionY, CursorImage.Width, CursorImage.Height);

			Invalidate();
		}

		private void SokobanMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)	{ KeyInputRight = true; }
			if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)	{ KeyInputUp	= true; }
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)	{ KeyInputLeft	= true; }
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)	{ KeyInputDown	= true; }
			if (e.KeyCode == Keys.Escape) { KeyInputESC = true; }
			if (e.KeyCode == Keys.R) { KeyInputR = true; }
		}

		private void SokobanMain_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)	{ KeyInputRight = false; }
			if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)	{ KeyInputUp	= false; }
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)	{ KeyInputLeft	= false; }
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)	{ KeyInputDown	= false; }
			if (e.KeyCode == Keys.Escape) { KeyInputESC = false; }
			if (e.KeyCode == Keys.R) { KeyInputR = false; }
		}

		private void SokobanMain_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				LeftMousePressed = true;
				Cursor.Hide();
			}
			
			if (e.Button == MouseButtons.Right)
			{
				RightMousePressed = true;
				Cursor.Hide();
			}
		}

		private void SokobanMain_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				LeftMousePressed = false;
			}
			
			if (e.Button == MouseButtons.Right)
			{
				RightMousePressed = false;
			}
		}

		public bool IsLeftMousePressed()
		{
			return LeftMousePressed;
		}
		
		public bool IsRightMousePressed()
		{
			return RightMousePressed;
		}

		public int GetMouseX()
		{
			return MousePositionX;
		}

		public int GetMouseY()
		{
			return MousePositionY;
		}

		public int GetScreenWidth()
		{
			return ClientSize.Width;
		}

		public int GetScreenHeight()
		{
			return ClientSize.Height;
		}

		private void SokobanMain_Paint(object sender, PaintEventArgs e)
		{
			if (BitmapDoubleBuffering != null)
			{
				e.Graphics.DrawImage(BitmapDoubleBuffering, 0, 0);
			}
		}

		public void Exit()
		{
			this.Close();
		}

		private void SokobanMain_Activated(object sender, EventArgs e)
		{
			IsActived = true;
		}

		private void SokobanMain_Deactivate(object sender, EventArgs e)
		{
			IsActived = false;
		}

		private void SokobanMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			gameSystem.Save();
		}
	}
}
