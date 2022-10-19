using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.GameClass
{
	class LevelMenu
	{
		// File Directory
		public const string DataDirectory = @"Map\Map_";
		public const string Extension = ".dat";

		private SokobanMain MainForm;
		private GameSystem gameSystem;
		private const int Col = 5;
		private int Row;
		private int MapCount;
		private ButtonLevel[,] buttonLevels; 

		// Scroll
		private double Scroll = 0;
		private int ScrollSection = 100;
		private double ScrollSpeed = 0;
		private double ScrollIncrease = 1.5;
		private double ScrollStop = 0.9;
		private double ScrollMaxSpeed = 20;
		private double ScrollLimite = 0;

		private StringFormat sf;
		private Font TextTitleFont;
		private Font TextSubTitleFont;
		private string Title = "맵을 선택하세요";
		private string SubTitle = "마우스 위 아래로 스크롤";
		private SolidBrush TextBrush;
		private int Alpha = 255;
		private int LevelSelected = 0;
		
		public LevelMenu (SokobanMain form, GameSystem gForm)
		{
			MainForm = form;
			gameSystem = gForm;

			// Font Set
			TextTitleFont = new Font("HY견고딕", 30);
			TextSubTitleFont = new Font("HY견고딕", 15);

			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			
			MapCount = 0;

			// 맵 파일이 몇개 있는지 확인
			while(true)
			{
				FileInfo fi = new FileInfo(DataDirectory + MapCount + Extension);

				if (!fi.Exists)
				{
					break;
				}

				MapCount ++;
			}

			Row = MapCount / Col;

			if (MapCount % Col > 0)
			{
				Row ++;
			}

			buttonLevels = new ButtonLevel[Row, Col];

			int count = 0;

			for (int row = 0; row < Row; row ++)
			{
				for (int col = 0; col < Col; col++)
				{
					if (count < MapCount)
					{
						buttonLevels[row, col] = new ButtonLevel(MainForm, count, col, row, Col);
					}
					else
					{
						buttonLevels[row, col] = null;
					}

					count++;
				}
			}

			ScrollLimite = (ButtonLevel.ImageHeight + ButtonLevel.Margin)  * (Row + 1);
		}

		public int GetLevel()
		{
			return LevelSelected;
		}

		public void Update()
		{
			int count = 0;

			for (int row = 0; row < Row; row ++)
			{
				for (int col = 0; col < Col; col++)
				{
					if (count < MapCount)
					{
						buttonLevels[row, col].ResetPosition(Scroll);
						if (buttonLevels[row, col].IsClicked())
						{
							LevelSelected = count;
							gameSystem.ChangeScene(GameSystem.Scene.IN_GAME);
							return;
						}
					}

					count++;
				}
			}

			if (MainForm.GetMouseY() < ScrollSection)
			{
				if (ScrollSpeed > ScrollMaxSpeed)
				{
					ScrollSpeed -= ScrollIncrease;
				}
				else
				{
					ScrollSpeed = -ScrollMaxSpeed;
				}
			}
			else if (MainForm.GetMouseY() > MainForm.GetScreenHeight() - ScrollSection)
			{
				if (ScrollSpeed < ScrollMaxSpeed)
				{
					ScrollSpeed += ScrollIncrease;
				}
				else
				{
					ScrollSpeed = ScrollMaxSpeed;
				}
			}
			else
			{
				if (Math.Abs(ScrollSpeed) > 0.1)
				{
					ScrollSpeed *= ScrollStop;
				}
				else
				{
					ScrollSpeed = 0;
				}
			}

			Scroll += ScrollSpeed;

			if (Scroll < 0)
			{
				Scroll = 0;
				ScrollSpeed = 0;
			}

			if (Scroll > ScrollLimite)
			{
				Scroll = ScrollLimite;
				ScrollSpeed = 0;
			}
		}

		public void Draw(Graphics graphics)
		{
			// 배경 출력
			graphics.FillRectangle(Brushes.LightGray, 0, 0, 10000, 10000);
			
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			
			// 버튼 출력
			int count = 0;

			for (int row = 0; row < Row; row ++)
			{
				for (int col = 0; col < Col; col++)
				{
					if (count < MapCount)
					{
						buttonLevels[row, col].Draw(graphics);
					}

					count++;
				}
			}

			// 안내문 출력
			int speed = 20;
			if (Scroll < 50)
			{
				if (Alpha < 255 - speed)
				{
					Alpha += speed;
				}
				else
				{
					Alpha = 255;
				}
			}
			else
			{
				if (Alpha > speed)
				{
					Alpha -= speed;
				}
				else
				{
					Alpha = 0;
				}
			}

			TextBrush = new SolidBrush(Color.FromArgb(Alpha, 0, 0, 0));
			graphics.DrawString(Title, TextTitleFont, TextBrush, MainForm.GetScreenWidth() / 2, 50, sf);
			graphics.DrawString(SubTitle, TextSubTitleFont, TextBrush, MainForm.GetScreenWidth() / 2, 80, sf);
		}
	}
}
