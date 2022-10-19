using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.GameClass
{
	class ButtonLevel
	{
		// Main Form
		private SokobanMain MainForm;

		// Position Properties
		private int Level;
		private int Col;
		private int Row;
		private int PositionX;
		private int PositionY;
		public const int Margin = 10;
		public const int ImageWidth = 200;
		public const int ImageHeight = 150;
		private const int InnerMargin = 5;
		private const int TopSpace = 50;
		private int Width;
		private int Height;
		private int OriginX;
		private int OriginY;

		private Image ImagePreview;

		private string TextLevel;
		private Brush BrushText			 = Brushes.White;
		private Brush BrushTextShadow	 = Brushes.Black;
		private Brush BrushOnText		 = Brushes.Gray;
		private Brush BrushOnTextShadow	 = Brushes.Black;
		private Brush BrushButton		 = Brushes.Gray;
		private Brush BrushOnButton		 = Brushes.White;
		private Brush BrushButtonPressed = Brushes.DarkGray;
		private Font TextFont = new Font("HY견고딕", 25);
		private StringFormat sf;

		private const int TextOffsetMenu = 1;
		private const int TextOffsetFromTop = 30;

		private bool IsMouseOn = false;
		private bool IsPressed = false;

		public ButtonLevel(SokobanMain form, int level, int posX, int posY, int col)
		{
			// Set MainForm
			MainForm = form;

			// Set
			Level		= level;
			Col			= posX;
			Row			= posY;

			if (Level > 0)
			{
				TextLevel = "LEVEL " + level;
			}
			else
			{
				TextLevel = "TUTORIAL";
			}

			// Font Set
			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			ImagePreview = Image.FromFile(@"Preview/Preview_" + Level + ".png");
			
			// Position Set
			Width = ImageWidth + InnerMargin * 2;
			Height = ImageHeight + InnerMargin + TopSpace;
			
			OriginX = (MainForm.GetScreenWidth() - (Width * col + Margin * (col - 1))) / 2;
			OriginY = 120;

			PositionX = OriginX + Col * (Width + Margin);
			PositionY = OriginY + Row * (Height + Margin);
		}

		/// <summary>
		/// 레벨 버튼들을 스크롤한다
		/// </summary>
		/// <param name="scroll"></param>
		public void ResetPosition(double scroll)
		{
			PositionY = OriginY + Row * (Height + Margin) - (int)scroll;
		}

		/// <summary>
		/// 업데이트 하면서 클릭 감지
		/// </summary>
		public bool IsClicked()
		{
			int mouseX = MainForm.GetMouseX();
			int mouseY = MainForm.GetMouseY();
			
			// 마우스 클릭
			if (MainForm.IsLeftMousePressed() && IsPressed == false)
			{
				IsPressed = true;
			}

			// 마우스가 버튼 위에 있다.
			if ((PositionX < mouseX && mouseX < PositionX + Width) &&
				(PositionY < mouseY && mouseY < PositionY + Height))
			{
				IsMouseOn = true;

				if (!MainForm.IsLeftMousePressed() && IsPressed)
				{
					IsPressed = false;
					return true;
				}
			}
			else // 마우스가 버튼 밖으로 나갔다.
			{
				IsMouseOn = false;
				IsPressed = false;
			}

			return false;
		}

		public void Draw(Graphics graphics)
		{
			int posX = OriginX + PositionX + Col * (Width + Margin);
			int posY = OriginY + PositionY + Row * (Height + Margin);

			if (IsMouseOn)
			{
				if (IsPressed)
				{
					graphics.FillRectangle(BrushButtonPressed, PositionX, PositionY, Width, Height);
				}
				else
				{
					graphics.FillRectangle(BrushOnButton, PositionX, PositionY, Width, Height);
				}
				
				graphics.DrawString(TextLevel, TextFont, BrushOnTextShadow	, PositionX + Width / 2 + TextOffsetMenu, PositionY + TextOffsetFromTop + TextOffsetMenu, sf);
				graphics.DrawString(TextLevel, TextFont, BrushOnText		, PositionX + Width / 2 - TextOffsetMenu, PositionY + TextOffsetFromTop - TextOffsetMenu, sf);
			}
			else
			{
				graphics.FillRectangle(BrushButton, PositionX, PositionY, Width, Height);
				graphics.DrawString(TextLevel, TextFont, BrushTextShadow	, PositionX + Width / 2 + TextOffsetMenu, PositionY + TextOffsetFromTop + TextOffsetMenu, sf);
				graphics.DrawString(TextLevel, TextFont, BrushText			, PositionX + Width / 2 - TextOffsetMenu, PositionY + TextOffsetFromTop - TextOffsetMenu, sf);
			}

			// 레벨 계수는 1부터 시작
			int LevelScale = Level / 10;

			if (LevelScale > 4)
			{
				LevelScale = 4;
			}

			SolidBrush sb = new SolidBrush(Sokoban.SokobanMain.BackgroundColorSet[LevelScale]);

			graphics.FillRectangle(sb, PositionX + InnerMargin, PositionY + TopSpace, ImageWidth, ImageHeight);
			graphics.DrawImage(ImagePreview, PositionX + InnerMargin, PositionY + TopSpace, ImageWidth, ImageHeight);
		}

	}
}
