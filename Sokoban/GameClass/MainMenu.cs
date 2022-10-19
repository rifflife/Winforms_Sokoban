using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban.GameClass
{
	class MainMenu
	{
		// Main Form
		private SokobanMain MainForm;
		private GameSystem gameSystem;

		// Game Menu enum
		private enum Menu { START, EDITOR, README, EXIT }
		
		// Text
		private const string TextTitle	= "Sokaban";
		private const string TextStart	= "게임시작";
		private const string TextEditor	= "맵에디터";
		private const string TextReadme	= "README";
		private const string TextExit	= "종료";

		// Font
		private Font TitleFont;
		private Font MenuFont;
		private StringFormat sf;

		// Text Color
		private Brush BrushTitle;
		private Brush BrushTitleShadow;

		// Draw Values
		private int CenterX;
		private int CenterY;

		private const int TitleXset = 10;
		private const int TitleOffsetY = 200;
		private const int TextOffsetTitle = 7;

		// Button Position
		private const int ButtonOffsetY = 40;
		private const int ButtonDistanceY = 70;
		private const int ButtonWidth = 400;
		private const int ButtonHeight = 60;

		// Button Object
		private ButtonMenu[] buttonMenus;

		public MainMenu(SokobanMain form, GameSystem gForm)
		{
			// Main Form Set
			MainForm = form;
			gameSystem = gForm;

			// Font Set
			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			TitleFont = new Font("HY견고딕", 100, FontStyle.Italic);

			// Text Color Set
			BrushTitle			= Brushes.White;
			BrushTitleShadow	= Brushes.Black;
			
			CenterX = MainForm.ClientSize.Width / 2;
			CenterY = MainForm.ClientSize.Height / 2;

			buttonMenus = new ButtonMenu[4];
			buttonMenus[(int)Menu.START]	= new ButtonMenu(MainForm, TextStart, CenterX, CenterY + ButtonOffsetY, ButtonWidth, ButtonHeight);
			buttonMenus[(int)Menu.EDITOR]	= new ButtonMenu(MainForm, TextEditor, CenterX, CenterY + ButtonOffsetY + ButtonDistanceY, ButtonWidth, ButtonHeight);
			buttonMenus[(int)Menu.README]	= new ButtonMenu(MainForm, TextReadme, CenterX, CenterY + ButtonOffsetY + ButtonDistanceY * 2, ButtonWidth, ButtonHeight);
			buttonMenus[(int)Menu.EXIT]		= new ButtonMenu(MainForm, TextExit, CenterX, CenterY + ButtonOffsetY + ButtonDistanceY * 3, ButtonWidth, ButtonHeight);
		}

		public void Draw(Graphics graphics)
		{
			CenterX = MainForm.ClientSize.Width / 2;
			CenterY = MainForm.ClientSize.Height / 2;
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 50)), 0, 0, 10000, 10000);

			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			
			// Draw Title "Sokoban"
			graphics.DrawString(TextTitle, TitleFont, BrushTitleShadow, CenterX - TitleXset, CenterY - TitleOffsetY, sf);
			graphics.DrawString(TextTitle, TitleFont, BrushTitle, CenterX - TextOffsetTitle - TitleXset, CenterY - TitleOffsetY - TextOffsetTitle, sf);

			// Draw Buttons
			for (int i = 0; i < 4; i ++)
			{
				buttonMenus[i].Draw(graphics);
			}
		}

		public void Update()
		{
			if (buttonMenus[(int)Menu.START].IsClicked())
			{
				gameSystem.ChangeScene(Sokoban.GameClass.GameSystem.Scene.LEVEL);
			}
			
			if (buttonMenus[(int)Menu.EDITOR].IsClicked())
			{
				MainForm.Exit();
				Process.Start("Editor.exe");
			}
			
			if (buttonMenus[(int)Menu.README].IsClicked())
			{
				gameSystem.ChangeScene(Sokoban.GameClass.GameSystem.Scene.README);
			}
			
			if (buttonMenus[(int)Menu.EXIT].IsClicked())
			{
				MainForm.Exit();
			}
		}

	}
}
