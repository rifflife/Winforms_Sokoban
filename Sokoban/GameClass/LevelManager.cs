using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformSokoban;

namespace Sokoban.GameClass
{
	class LevelManager
	{
		public const string DataDirectory = @"Map\Map_";
		public const string Extension = ".dat";

		public const int CELL = 32;
		public const int LevelClassScale = 10;

		// System
		private SokobanMain sokobanMain;
		private TileBase[,] tileBases;
		
		// Images
		public Image[] tileImages;
		public Image[] playerImage;
		public Image[] boxImage;
		private string ImageDirectory = @"Image/";
		
		// Color
		public Color NoneAreaColor;

		// Stage
		private Stage stage;
		private int Level;
		public Point MapSize;
		private int Goals;
		private int MaxGoals;
		private int Width;
		private int Height;

		// Player
		private int PlayerX;
		private int PlayerY;
		private int Moves;
		private bool IsStarted;
		private DateTime StartTime;
		private TimeSpan SpendTime;
		private bool IsLevelCompleted;

		// Player Input
		private const int MoveDelayInit = 10;
		private int MoveDelay;

		// Draw
		private int OffsetX = 0;
		private int OffsetY = 0;
		private int StatusWidth = 0;

		/// <summary>
		/// 레벨 메니저 초기화
		/// </summary>
		/// <param name="Level"></param>
		/// <param name="sokobanMain"></param>
		public LevelManager(int level, SokobanMain sokobanMain, int StatusWidth)
		{
			Level = level;
			this.sokobanMain = sokobanMain;
			this.StatusWidth = StatusWidth;

			Reset();
		}
		
		public void Reset()
		{
			StartTime = new DateTime(0);
			// 레벨 계수는 1부터 시작
			int LevelScale = Level / LevelClassScale;

			if (LevelScale > 4)
			{
				LevelScale = 4;
			}
			
			NoneAreaColor = (Sokoban.SokobanMain.BackgroundColorSet[LevelScale]);

			// #############################################
			//					Image Load
			// #############################################

			// TileType { None, Empty, Wall, Goal }

			tileImages = new Image[3];
			tileImages[0] = Image.FromFile(ImageDirectory + "Tile_Empty_" + LevelScale + ".png");
			tileImages[1] = Image.FromFile(ImageDirectory + "Tile_Wall_" + LevelScale + ".png");
			tileImages[2] = Image.FromFile(ImageDirectory + "Tile_Goal_" + LevelScale + ".png");

			playerImage = new Image[4];
			playerImage[0] = Image.FromFile(ImageDirectory + "Tile_Player_0.png");
			playerImage[1] = Image.FromFile(ImageDirectory + "Tile_Player_1.png");
			playerImage[2] = Image.FromFile(ImageDirectory + "Tile_Player_2.png");
			playerImage[3] = Image.FromFile(ImageDirectory + "Tile_Player_3.png");

			boxImage = new Image[2];
			boxImage[0] = Image.FromFile(ImageDirectory + "Tile_Box_" + LevelScale + ".png");
			boxImage[1] = Image.FromFile(ImageDirectory + "Tile_BoxOn_" + LevelScale + ".png");

			IsLevelCompleted = false;
			MoveDelay = 0;
			IsStarted = false;

			// #############################################
			//					Stage Load
			// #############################################

			stage = new Stage("Stage " + Level);
			
			try
			{
				FileStream fs = new FileStream(DataDirectory + Level + Extension, FileMode.Open);
				StreamReader r = new StreamReader(fs);

				Width  = Convert.ToInt32(r.ReadLine());
				Height = Convert.ToInt32(r.ReadLine());
				r.ReadLine();
				r.ReadLine();

				stage.data = new int[Height, Width];

				for (int row = 0; row < Height; row ++)
				{
					for (int col = 0; col < Width; col ++)
					{
						stage.data[row, col] = Convert.ToInt32(r.ReadLine());
					}
				}

				r.Close();
			}
			catch (IOException ex)
			{
			}

			MapSize = stage.GetSize();

			// #############################################
			//				Tile Image Setting
			// #############################################

			MaxGoals = 0;
			Goals = 0;
			Moves = 0;

			tileBases = new TileBase[MapSize.Y, MapSize.X];

			for(int y = 0; y < MapSize.Y; y ++)
			{
				for(int x = 0; x < MapSize.X; x ++)
				{
					tileBases[y, x] = new TileBase(this, x, y);
					int data = stage.data[y, x];
					
					// TileType { None, Empty, Wall, Goal }
					switch(data)
					{
						case -1: // None 데이터 할당은 하지만 출력하지 않는다.
							tileBases[y, x].SetTileBase(tileImages[0], (TileBase.TileType)data);
							break;

						case 0: // Empty 움직일 수 있는 공간
							tileBases[y, x].SetTileBase(tileImages[data], TileBase.TileType.Empty);
							break;

						case 1: // Wall
							tileBases[y, x].SetTileBase(tileImages[data], TileBase.TileType.Wall);
							break;

						case 2: // Goal
							tileBases[y, x].SetTileBase(tileImages[data], TileBase.TileType.Goal);
							MaxGoals ++;
							break;

						case 3: // Box
							tileBases[y, x].SetTileBase(tileImages[0], TileBase.TileType.Empty);
							tileBases[y, x].SetBox(true);
							break;

						case 4: // Player
							tileBases[y, x].SetTileBase(tileImages[0], TileBase.TileType.Empty);
							PlayerX = x;
							PlayerY = y;
							tileBases[y, x].SetPlayer(true);
							break;
							
						case 5: // Box on Goal
							tileBases[y, x].SetTileBase(tileImages[2], TileBase.TileType.Goal);
							tileBases[y, x].SetBox(true);
							MaxGoals ++;
							break;
							
						case 6: // Player on Goal
							tileBases[y, x].SetTileBase(tileImages[2], TileBase.TileType.Goal);
							PlayerX = x;
							PlayerY = y;
							MaxGoals ++;
							tileBases[y, x].SetPlayer(true);
							break;
					}
				}
			}

			// Draw offset Set
			int TotalWidth = CELL * Width;
			int TotalHeight = CELL * Height;
			OffsetX = (sokobanMain.ClientSize.Width - TotalWidth - StatusWidth) / 2;
			OffsetY = (sokobanMain.ClientSize.Height - TotalHeight) / 2;
		}

		/// <summary>
		/// 레벨 클리어 유무 반환
		/// </summary>
		/// <returns></returns>
		public bool IsCompleted()
		{
			return IsLevelCompleted;
		}

		public TimeSpan GetSpendTime()
		{
			return SpendTime;
		}

		public DateTime GetStartTime()
		{
			return StartTime;
		}

		public int GetMoves()
		{
			return Moves;
		}

		/// <summary>
		/// 키보드 입력을 받아서 움직임을 조정한다.
		/// MoveDelay 변수를 통해 키를 누르고 있을 때의 연속 움직임을 제어
		/// </summary>
		public void KeyInput(bool iRight, bool iUp, bool iLeft, bool iDown)
		{
			// 키가 눌리지 않았을 때
			if (!iRight && !iUp && !iLeft && !iDown)
			{
				MoveDelay = 0;
				return;
			}

			if (IsLevelCompleted)
			{
				if (IsStarted)
				{
					SpendTime = DateTime.Now - StartTime;
				}

				IsStarted = false;
				return;
			}
			
			// 타이머 시작
			if (!IsStarted)
			{
				IsStarted = true;
				StartTime = DateTime.Now;
			}

			if (MoveDelay > 0)
			{
				MoveDelay --;
				return;
			}
			
			int AccX = 0;
			int AccY = 0;

			TileBase.Direction direction = TileBase.Direction.None;

			if (iRight) { AccX++; }
			if (iLeft)  { AccX--; }
			if (iUp)	{ AccY--; }
			if (iDown)	{ AccY++; }

			if (AccX != 0 && AccY != 0)
			{
				// 다른 방향키 동시에 눌리면 움직이지 않는다.
				return;
			}
			else
			{
				if (AccX > 0) { direction = TileBase.Direction.Right; }
				if (AccX < 0) { direction = TileBase.Direction.Left; }
				if (AccY < 0) { direction = TileBase.Direction.Up; }
				if (AccY > 0) { direction = TileBase.Direction.Down; }
			}

			switch (direction)
			{
				case TileBase.Direction.Up:
					HandleMovement(PlayerX, PlayerY - 1, direction);
					break;

				case TileBase.Direction.Down:
					HandleMovement(PlayerX, PlayerY + 1, direction);
					break;

				case TileBase.Direction.Left:
					HandleMovement(PlayerX - 1, PlayerY, direction);
					break;

				case TileBase.Direction.Right:
					HandleMovement(PlayerX + 1, PlayerY, direction);
					break;
				
				default:
					break;
			}
			
			MoveDelay = MoveDelayInit;

			IsLevelCompleted = CheckForTheGoals();
		}

		/// <summary>
		/// 출력
		/// </summary>
		/// <param name="graphics">그래픽</param>
		public void Draw(Graphics graphics)
		{
			graphics.Clear(NoneAreaColor);

			for(int y = 0; y < MapSize.Y; y ++)
			{
				for(int x = 0; x < MapSize.X; x ++)
				{
					tileBases[y, x].Draw(graphics, OffsetX, OffsetY);
				}
			}
		}

		/// <summary>
		/// 움직임 제어
		/// </summary>
		/// <param name="direction">플레이어 움직임 방향</param>
		private void HandleMovement(int targetX, int targetY, TileBase.Direction direction)
		{
			TileBase.TileType targetTileType = tileBases[targetY, targetX].GetTileType();

			if (targetTileType == TileBase.TileType.Wall)
			{
				return;
			}

			bool targetHasBox = tileBases[targetY, targetX].HasBox();

			if (targetHasBox)
			{
				int xOffset = targetX - PlayerX;
				int yOffset = targetY - PlayerY;

				int spotX = targetX + xOffset;
				int spotY = targetY + yOffset;

				TileBase.TileType spotTileType = tileBases[spotY, spotX].GetTileType();

				bool spotHasBox = tileBases[spotY, spotX].HasBox();

				if (!spotHasBox)
				{
					if (spotTileType == TileBase.TileType.Empty || spotTileType == TileBase.TileType.Goal)
					{
						SetBox(spotX, spotY, true);
						SetBox(targetX, targetY, false);
						SetPlayer(targetX, targetY, true, direction);
						SetPlayer(PlayerX, PlayerY, false, direction);
						PlayerX = targetX;
						PlayerY = targetY;

						Moves ++;
					}
				}
			}
			else
			{
				if (targetTileType == TileBase.TileType.Empty || targetTileType == TileBase.TileType.Goal)
				{
					SetPlayer(targetX, targetY, true, direction);
					SetPlayer(PlayerX, PlayerY, false, direction);
					PlayerX = targetX;
					PlayerY = targetY;

					Moves ++;
					return;
				}
			}
		}

		/// <summary>
		/// 플레이어 위치 설정
		/// </summary>
		/// <param name="direction">플레이어 움직임 방향</param>
		private void SetPlayer(int x, int y, bool value, TileBase.Direction direction)
		{
			tileBases[y, x].SetPlayer(value);
			tileBases[y, x].SetPlayerDirection(direction);
		}

		private void SetBox(int x, int y, bool value)
		{
			tileBases[y, x].SetBox(value);
		}

		private bool CheckForTheGoals()
		{
			Goals = 0;

			for(int y = 0; y < MapSize.Y; y ++)
			{
				for(int x = 0; x < MapSize.X; x ++)
				{
					if (tileBases[y, x].HasBox() && tileBases[y, x].GetTileType() == TileBase.TileType.Goal)
					{
						Goals++;
					}
				}
			}

			if (Goals >= MaxGoals)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public int GetCellSize()
		{
			return CELL;
		}
	}
}
