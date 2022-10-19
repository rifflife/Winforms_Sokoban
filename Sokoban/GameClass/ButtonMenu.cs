using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.GameClass
{
	class ButtonMenu
	{
		// Main Form
		private SokobanMain MainForm;

		// Text Properties
		private String Text;
		private int PositionX;
		private int PositionY;
		private int HalfWidth;
		private int HalfHeight;
		private int Width;
		private int Height;

		private Brush BrushText			 = Brushes.White;
		private Brush BrushTextShadow	 = Brushes.Black;
		private Brush BrushOnText		 = Brushes.Gray;
		private Brush BrushOnTextShadow	 = Brushes.Black;
		private Brush BrushOnButton		 = Brushes.White;
		private Brush BrushButtonPressed = Brushes.DarkGray;
		private Font TextFont = new Font("HY견고딕", 30);
		private StringFormat sf;

		private const int TextOffsetMenu = 2;
		private const int TextOffsetFromTop = 6;

		private bool IsMouseOn = false;
		private bool IsPressed = false;

		public ButtonMenu(SokobanMain form, String text, int posX, int posY, int width, int height)
		{
			// Set MainForm
			MainForm = form;

			// Set
			Text		= text;
			PositionX	= posX;
			PositionY	= posY;
			Width		= width;
			Height		= height;
			HalfWidth	= Width / 2;
			HalfHeight	= Height / 2;

			// Font Set
			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
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
			if ((PositionX - HalfWidth  < mouseX && mouseX < PositionX + HalfWidth) &&
				(PositionY - HalfHeight < mouseY && mouseY < PositionY + HalfHeight))
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
			if (IsMouseOn)
			{
				if (IsPressed)
				{
					graphics.FillRectangle(BrushButtonPressed, PositionX - HalfWidth, PositionY - HalfHeight, Width, Height);
				}
				else
				{
					graphics.FillRectangle(BrushOnButton, PositionX - HalfWidth, PositionY - HalfHeight, Width, Height);
				}
				
				graphics.DrawString(Text, TextFont, BrushOnTextShadow	, PositionX + TextOffsetMenu, PositionY + TextOffsetFromTop + TextOffsetMenu, sf);
				graphics.DrawString(Text, TextFont, BrushOnText			, PositionX - TextOffsetMenu, PositionY + TextOffsetFromTop - TextOffsetMenu, sf);
			}
			else
			{
				graphics.DrawString(Text, TextFont, BrushTextShadow	, PositionX + TextOffsetMenu, PositionY + TextOffsetFromTop + TextOffsetMenu, sf);
				graphics.DrawString(Text, TextFont, BrushText		, PositionX - TextOffsetMenu, PositionY + TextOffsetFromTop - TextOffsetMenu, sf);
			}
		}

	}
}
