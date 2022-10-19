using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.GameClass
{
	class InGame
	{
		public Color[] StatusColorSet;

		private SokobanMain MainForm;
		private GameSystem gameSystem;
		private LevelManager levelManager;

		private int CurrentLevel = 0;
		private bool IsGamePlaying = false;

		// Button
		private Image[] ButtonImage;
		private bool IsMouseOn = false;
		private bool IsPressed = false;

		// Timer
		private int StartTimer = 0;
		private int StartTimerInit = (Second * 4) - 1;
		private const int Second = 50;

		private int RestartTimer = 0;
		private int RestartTimerInit = (Second * 4) - 1;
		private bool Restart = false;

		private int RestartButtonTimer = 0;
		private int	RestartButtonTimerInit = 30;

		// Font
		private StringFormat sf;
		private	Font TextNoticeFont = new Font("HY견고딕", 100, FontStyle.Italic);
		private	Font TextStatusFont = new Font("HY견고딕", 18);
		private Brush BrushStatus		= Brushes.White;
		private Brush BrushStatusShadow = Brushes.Black;
		private Brush BrushNotice		= Brushes.White;
		private Brush BrushNoticeShadow = Brushes.Black;
		private const int TextNoticeOffset = 4;
		private const int TextStatusOffset = 1;

		private const int StatusWidth = 300;

		public InGame(SokobanMain form, GameSystem gForm)
		{
			StatusColorSet = new Color[5];
			StatusColorSet[0] = Color.FromArgb(67, 60, 44);
			StatusColorSet[1] = Color.FromArgb(53, 39, 25);
			StatusColorSet[2] = Color.FromArgb(41, 41, 41);
			StatusColorSet[3] = Color.FromArgb(41, 17, 17);
			StatusColorSet[4] = Color.FromArgb(19, 19, 19);

			ButtonImage = new Image[2];
			ButtonImage[0] = Image.FromFile(@"Image/Button_Restart_0.png");
			ButtonImage[1] = Image.FromFile(@"Image/Button_Restart_1.png");

			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			
			MainForm = form;
			gameSystem = gForm;

			levelManager = new LevelManager(0, MainForm, StatusWidth);
		}

		public int GetCurrentLevel()
		{
			return CurrentLevel;
		}

		public void Update()
		{
			// 버튼
			// 재시작 버튼
			int mouseX = MainForm.GetMouseX();
			int mouseY = MainForm.GetMouseY();

			// 마우스 클릭
			if (MainForm.IsLeftMousePressed() && IsPressed == false)
			{
				IsPressed = true;
			}

			// 마우스가 버튼 위에 있다.
			if ((80 < mouseX && mouseX < 160) &&
				(0 < mouseY && mouseY < 60))
			{
				IsMouseOn = true;

				if (!MainForm.IsLeftMousePressed() && IsPressed)
				{
					IsPressed = false;
					// 눌림
					gameSystem.ChangeScene(GameSystem.Scene.RESTART_IN_GAME);
				}
			}
			else // 마우스가 버튼 밖으로 나갔다.
			{
				IsMouseOn = false;
				IsPressed = false;
			}

			// 재시작
			if (RestartButtonTimer > 0)
			{
				RestartButtonTimer --;
			}
			else
			{
				if (MainForm.KeyInputR)
				{
					RestartButtonTimer = RestartButtonTimerInit;
					gameSystem.ChangeScene(GameSystem.Scene.RESTART_IN_GAME);
					return;
				}
			}

			// 로직
			if (!IsGamePlaying)
			{
				Reset();
				IsGamePlaying = true;
			}
			else
			{
				if (StartTimer > Second)
				{
					StartTimer --;
					return;
				}
				else
				{
					StartTimer = 0;
				}

				levelManager.KeyInput(MainForm.KeyInputRight, MainForm.KeyInputUp, MainForm.KeyInputLeft, MainForm.KeyInputDown);
			}

			// 레벨 완료
			if(levelManager.IsCompleted())
			{
				if (RestartTimer > Second)
				{
					RestartTimer --;
					return;
				}
				// 최고기록 저장
				else if (RestartTimer == Second)
				{
					int bestRecordMoves = gameSystem.PlayerRecord[CurrentLevel].Moves;
					int bestRecordSpendTime = gameSystem.PlayerRecord[CurrentLevel].SpendTime;

					bool isBestRecord = (bestRecordMoves >= levelManager.GetMoves() || bestRecordMoves == 0) &&
						(bestRecordSpendTime >= (int)(levelManager.GetSpendTime().TotalSeconds) || bestRecordSpendTime == 0);

					if (isBestRecord)
					{
						gameSystem.PlayerRecord[CurrentLevel].Moves = levelManager.GetMoves();
						gameSystem.PlayerRecord[CurrentLevel].SpendTime = (int)(levelManager.GetSpendTime().TotalSeconds);
					}

					RestartTimer = 0;

					gameSystem.ChangeScene(GameSystem.Scene.RESTART_IN_GAME);
				}
			}
		}

		public void Reset()
		{
			IsGamePlaying = false;
			StartTimer = StartTimerInit;
			RestartTimer = RestartTimerInit;
			CurrentLevel = gameSystem.GetCurrentLevel();
			levelManager = new LevelManager(CurrentLevel, MainForm, StatusWidth);
		}

		public void Draw(Graphics graphics)
		{
			// 로직
			levelManager.Draw(graphics);
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			int CenterX = (MainForm.ClientSize.Width - StatusWidth) / 2;
			int CenterY = MainForm.ClientSize.Height / 2;

			if (StartTimer > 0)
			{
				graphics.DrawString(Convert.ToString((int)(StartTimer / Second)), TextNoticeFont, BrushNoticeShadow, CenterX + TextNoticeOffset, CenterY + TextNoticeOffset, sf);
				graphics.DrawString(Convert.ToString((int)(StartTimer / Second)), TextNoticeFont, BrushNotice	   , CenterX - TextNoticeOffset, CenterY - TextNoticeOffset, sf);
			}

			// 승리 문구 출력

			if (levelManager.IsCompleted())
			{
				graphics.DrawString("Clear !", TextNoticeFont, BrushNoticeShadow , CenterX + TextNoticeOffset, CenterY + TextNoticeOffset, sf);
				graphics.DrawString("Clear !", TextNoticeFont, BrushNotice		 , CenterX - TextNoticeOffset, CenterY - TextNoticeOffset, sf);
			}

			// 스테이터스 출력
			int LevelScalse = CurrentLevel / 10;
			if (LevelScalse > 4)
			{
				LevelScalse = 4;
			}
			SolidBrush sb = new SolidBrush(StatusColorSet[LevelScalse]);
			graphics.FillRectangle(sb, MainForm.ClientSize.Width - StatusWidth, 0, MainForm.ClientSize.Width, MainForm.ClientSize.Height);
			
			// 타이머 출력
			DateTime startTime = levelManager.GetStartTime();

			string textTime = "00 : 00";
			int textPosX = MainForm.ClientSize.Width - StatusWidth / 2;
			TimeSpan time = DateTime.Now - levelManager.GetStartTime();

			if (startTime != new DateTime(0))
			{
				if (levelManager.IsCompleted())
				{
					time = levelManager.GetSpendTime();
				}
				else
				{
					textTime = AddZeroForTime(time.Minutes.ToString()) + " : " + AddZeroForTime(time.Seconds.ToString());
				}
				
				textTime = AddZeroForTime(time.Minutes.ToString()) + " : " + AddZeroForTime(time.Seconds.ToString());
			}

			int posY = 70;
			int shortDistance = 35;
			int longDistance = 70;

			SolidBrush mSb = new SolidBrush(Color.FromArgb(30, 0, 0, 0));
			Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0), 2);
			int leftX = textPosX - 120;
			int leftX2 = textPosX - 130;

			graphics.FillRectangle(mSb, leftX, 220 - 20, 240, 85);
			graphics.FillRectangle(mSb, leftX, 325 - 20, 240, 85);
			graphics.FillRectangle(mSb, leftX, 530 - 20, 240, 85);
			graphics.FillRectangle(mSb, leftX, 635 - 20, 240, 85);
			graphics.DrawRectangle(pen, leftX2, 150 - 25, 260, 275);
			graphics.DrawRectangle(pen, leftX2, 460 - 25, 260, 275);

			DrawNoticeString(graphics, "LEVEL " + CurrentLevel, new Font("HY견고딕", 30, FontStyle.Italic), textPosX, posY);
			posY += 90;
			DrawNoticeString(graphics, "현재 기록", TextStatusFont, textPosX, posY);
			posY += longDistance;

			DrawNoticeString(graphics, "진행 시간", TextStatusFont, textPosX, posY);
			posY += shortDistance;
			DrawNoticeString(graphics, textTime, TextStatusFont, textPosX, posY);
			posY += longDistance;
			
			DrawNoticeString(graphics, "이동 횟수", TextStatusFont, textPosX, posY);
			posY += shortDistance;
			DrawNoticeString(graphics, levelManager.GetMoves().ToString(), TextStatusFont, textPosX, posY);
			posY += longDistance + 30;
			
			int bestRecordMoves = gameSystem.PlayerRecord[CurrentLevel].Moves;
			int bestRecordSpendTime = gameSystem.PlayerRecord[CurrentLevel].SpendTime;

			DrawNoticeString(graphics, "최고 기록", TextStatusFont, textPosX, posY);
			posY += longDistance;

			if (bestRecordMoves == 0)
			{
				DrawNoticeString(graphics, "기록 없음", TextStatusFont, textPosX, posY);
				posY += shortDistance;
				DrawNoticeString(graphics, "-", TextStatusFont, textPosX, posY);
				posY += longDistance;
				DrawNoticeString(graphics, "기록 없음", TextStatusFont, textPosX, posY);
				posY += shortDistance;
				DrawNoticeString(graphics, "-", TextStatusFont, textPosX, posY);
				posY += longDistance;
			}
			else
			{
				DrawNoticeString(graphics, "진행 시간", TextStatusFont, textPosX, posY);
				posY += shortDistance;
				DrawNoticeString(graphics, ToTimeString(bestRecordSpendTime), TextStatusFont, textPosX, posY);
				posY += longDistance;
				DrawNoticeString(graphics, "이동 횟수", TextStatusFont, textPosX, posY);
				posY += shortDistance;
				DrawNoticeString(graphics, bestRecordMoves.ToString(), TextStatusFont, textPosX, posY);
				posY += longDistance;
			}

			int LevelScale = 0;
				
			Image b1 = ButtonImage[0];
			Image b2 = ButtonImage[1];

			LevelScale = CurrentLevel / 10;
				
			if (LevelScale > 4)
			{
				LevelScale = 4;
			}

			// 색상 변경
			if (LevelScale > 0)
			{
				b1 = ButtonImage[1];
				b2 = ButtonImage[0];
			}

			// 버튼
			if (IsMouseOn)
			{
				if (IsPressed)
				{
					graphics.DrawImage(b1, 80, 0, 80, 60);
				}
				else
				{
					graphics.DrawImage(b2, 80, 0, 80, 60);
				}
			}
			else
			{
				graphics.DrawImage(b2, 80, 0, 80, 60);
			}
		}


		public void DrawNoticeString(Graphics graphics, string str, Font font, int posX, int posY)
		{
			graphics.DrawString(str, font, BrushStatusShadow, posX + TextStatusOffset, posY + TextStatusOffset, this.sf);
			graphics.DrawString(str, font, BrushStatus	   , posX - TextStatusOffset, posY - TextStatusOffset, this.sf);
		}
		
		public string AddZeroForTime(string str)
		{
			if (str.Length == 1)
			{
				return "0" + str;
			}
			return str;
		}

		public string ToTimeString(int time)
		{
			int second = time % 60;
			int min = time / 60;

			return AddZeroForTime(min.ToString()) + " : " + AddZeroForTime(second.ToString());
		}
	}
}
