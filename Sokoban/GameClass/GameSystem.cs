using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban.GameClass
{
	struct Record
	{
		public int Moves;
		public int SpendTime;

		public Record(int moves, int spendTime)
		{
			Moves = moves;
			SpendTime = spendTime;
		}
	}

	class GameSystem
	{
		// File Directory
		public const string DataDirectory = @"PlayerData.dat";
		
		private SokobanMain MainForm;

		// Button
		private Image[] ButtonImage;
		private bool IsMouseOn = false;
		private bool IsPressed = false;

		public enum Scene { MAIN, LEVEL, IN_GAME, README, NONE, RESTART_IN_GAME };
		private Scene CurrentScene;
		private Scene CurrentSceneTemp = Scene.NONE;

		private MainMenu mainMenu;
		private LevelMenu levelMenu;
		private InGame inGame;
		private Readme readme;
		private int LevelSelected;

		private int BackTimer;
		private int BackTimerInit = 30;

		private int ChangeAlpha = 0;
		private int AlphaSpeed = 30;

		// Player Data
		public Record[] PlayerRecord;
		public const int MaxLevel = 500;

		public GameSystem(SokobanMain form)
		{
			MainForm = form;
			CurrentScene = Scene.MAIN;

			ButtonImage = new Image[2];
			ButtonImage[0] = Image.FromFile(@"Image/Button_Return_0.png");
			ButtonImage[1] = Image.FromFile(@"Image/Button_Return_1.png");

			mainMenu = new MainMenu(form, this);
			levelMenu = new LevelMenu(form, this);
			inGame = new InGame(form, this);
			readme = new Readme();

			PlayerRecord = new Record[MaxLevel];
			for(int num = 0; num < MaxLevel; num ++)
			{
				PlayerRecord[num] = new Record(0, 0);
			}

			Load();
		}

		public Scene GetCurrentScene()
		{
			return CurrentScene;
		}

		public int GetCurrentLevel()
		{
			return LevelSelected;
		}

		public void Update()
		{
			if (CurrentScene != Scene.MAIN)
			{	
				// 뒤로가기 버튼
				int mouseX = MainForm.GetMouseX();
				int mouseY = MainForm.GetMouseY();

				// 마우스 클릭
				if (MainForm.IsLeftMousePressed() && IsPressed == false)
				{
					IsPressed = true;
				}

				// 마우스가 버튼 위에 있다.
				if ((0 < mouseX && mouseX < 80) &&
					(0 < mouseY && mouseY < 60))
				{
					IsMouseOn = true;

					if (!MainForm.IsLeftMousePressed() && IsPressed)
					{
						IsPressed = false;
						// 눌림
						if (CurrentScene == Scene.IN_GAME)
						{
							ChangeScene(GameSystem.Scene.LEVEL);
						}
						else
						{
							ChangeScene(GameSystem.Scene.MAIN);
						}
					}
				}
				else // 마우스가 버튼 밖으로 나갔다.
				{
					IsMouseOn = false;
					IsPressed = false;
				}
			}

			LevelSelected = levelMenu.GetLevel();

			switch(CurrentScene)
			{
				case Scene.MAIN:
					mainMenu.Update();
					break;
					
				case Scene.LEVEL:
					levelMenu.Update();
					break;

				case Scene.IN_GAME:
					inGame.Update();
					break;

				case Scene.RESTART_IN_GAME:
					ChangeScene(Scene.IN_GAME);
					break;
					
				case Scene.README:
					readme.Update();
					break;
			}

			if (BackTimer > 0)
			{
				BackTimer --;
			}
			else
			{
				if (MainForm.KeyInputESC)
				{
					BackTimer = BackTimerInit;
					GoBack();
				}
			}

			// 화면 암전후 전환
			if (CurrentSceneTemp != Scene.NONE)
			{
				if (ChangeAlpha < 255 - AlphaSpeed)
				{
					ChangeAlpha += AlphaSpeed;
				}
				else
				{
					CurrentScene = CurrentSceneTemp;
					CurrentSceneTemp = Scene.NONE;
					inGame.Reset();

					ChangeAlpha = 255;
				}
			}
			else
			{
				if (ChangeAlpha > AlphaSpeed)
				{
					ChangeAlpha -= AlphaSpeed;
				}
				else
				{
					ChangeAlpha = 0;
				}
			}
		}

		public void Draw(Graphics graphics)
		{
			switch(CurrentScene)
			{
				case Scene.MAIN:
					mainMenu.Draw(graphics);
					break;

				case Scene.LEVEL:
					levelMenu.Draw(graphics);
					break;
					
				case Scene.IN_GAME:
					inGame.Draw(graphics);
					break;
					
				case Scene.README:
					readme.Draw(graphics);
					break;
			}
			
			// 버튼
			if (CurrentScene != Scene.MAIN)
			{
				int LevelScale = 0;
				
				Image b1 = ButtonImage[0];
				Image b2 = ButtonImage[1];

				if (CurrentScene == Scene.IN_GAME)
				{
					LevelScale = inGame.GetCurrentLevel() / 10;
				
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
				}

				if (IsMouseOn)
				{
					if (IsPressed)
					{
						graphics.DrawImage(b1, 0, 0, 80, 60);
					}
					else
					{
						graphics.DrawImage(b2, 0, 0, 80, 60);
					}
				}
				else
				{
					graphics.DrawImage(b2, 0, 0, 80, 60);
				}
			}
			

			graphics.FillRectangle(new SolidBrush(Color.FromArgb(ChangeAlpha, 0, 0, 0)), 0, 0, 3000, 3000);
		}

		public void ChangeScene(Scene scene)
		{
			CurrentSceneTemp = scene;
		}

		public void GoBack()
		{
			switch(CurrentScene)
			{
				case Scene.MAIN:
					break;
					
				case Scene.LEVEL:
					ChangeScene(Scene.MAIN);
					break;

				case Scene.IN_GAME:
					ChangeScene(Scene.LEVEL);
					break;

				case Scene.README:
					ChangeScene(Scene.MAIN);
					break;
			}
		}

		public bool Load()
		{
			try
			{
				FileStream fs = new FileStream(DataDirectory, FileMode.Open);

				StreamReader r = new StreamReader(fs);

				for(int num = 0; num < MaxLevel; num ++)
				{
					PlayerRecord[num].Moves = Convert.ToInt32(r.ReadLine());
					PlayerRecord[num].SpendTime = Convert.ToInt32(r.ReadLine());
				}

				r.Close();
				return true;
			}
			catch (IOException ex)
			{
				return false;
			}
		}

		public bool Save()
		{
			try
			{
				FileStream fs = new FileStream(DataDirectory, FileMode.Create);

				StreamWriter r = new StreamWriter(fs);

				for(int num = 0; num < MaxLevel; num ++)
				{
					r.WriteLine(PlayerRecord[num].Moves);
					r.WriteLine(PlayerRecord[num].SpendTime);
				}

				r.Close();
				return true;
			}
			catch (IOException ex)
			{
				return false;
			}
		}
	}
}
