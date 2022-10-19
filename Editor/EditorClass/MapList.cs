using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban.EditorClass
{
	class MapList
	{
		public const int ImageWidth = 200;
		public const int ImageHeight = 150;
		public const string MapPreviewDirectory = @"Preview/Preview_";
		public List<MapData> MapLists;
		public ListBox listBox;
		public int CurrentMapSize = 0;
		public int Theme = 0;
		public int Cell;
		public int CurrentIndex = -1;

		public Image[,] TileImages;

		public MapList(ListBox control, int cell)
		{
			this.Cell = cell;

			string ImageDirectory = @"Image/";

			TileImages = new Image[6,7];

			// 이미지 로드
			for (int theme = 0; theme < 5; theme ++)
			{
				//TileImages[Theme, -1] = null;
				TileImages[theme, 0] = Image.FromFile(ImageDirectory + "Tile_Empty_" + theme + ".png");
				TileImages[theme, 1] = Image.FromFile(ImageDirectory + "Tile_Wall_" + theme + ".png");
				TileImages[theme, 2] = Image.FromFile(ImageDirectory + "Tile_Goal_" + theme + ".png");
				TileImages[theme, 3] = Image.FromFile(ImageDirectory + "Tile_Box_" + theme + ".png");
				TileImages[theme, 4] = Image.FromFile(ImageDirectory + "Tile_Player_" + 3 + ".png");
				TileImages[theme, 5] = Image.FromFile(ImageDirectory + "Tile_BoxOn_" + theme + ".png");
				TileImages[theme, 6] = Image.FromFile(ImageDirectory + "Tile_PAG.png");
			}

			listBox = control;

			MapLists = new List<MapData>();

			int MapCount = 0;

			// 맵 파일이 몇개 있는지 확인
			while(true)
			{
				FileInfo fi = new FileInfo(MapData.DataDirectory + MapCount + MapData.Extension);

				if (!fi.Exists)
				{
					break;
				}

				MapCount ++;
			}

			CurrentMapSize = MapCount;

			// 맵이 한개라도 있으면
			if (CurrentMapSize > 0)
			{
				// 맵 로드 시도
				for(int level = 0; level < CurrentMapSize; level ++)
				{
					MapLists.Add(new MapData(level));
					MapLists[level].Load();
				}
			
				// 비어있는 맵 찾고 리스트 박스에 리스트 추가
				for(int level = 0; level < CurrentMapSize; level ++)
				{
					if (MapLists[level].IsEmpty())
					{
						listBox.Items.Insert(level, "Map_" + level + "[NO DATA]");
					}
					else
					{
						listBox.Items.Insert(level, "Map_" + level);
					}
				}

				CurrentIndex = 0;
			}
		}

		public void SetTheme(int theme)
		{
			Theme = theme;
		}

		public void SetData(int posX, int posY, int inputData)
		{
			if (CurrentIndex != -1)
			{
				if (inputData <= 2)
				{
					MapLists[CurrentIndex].Set(posX, posY, inputData);
					ListNameReset();
				}
				else
				{
					// 골인경우
					if (MapLists[CurrentIndex].GetData(posX, posY) == 2 ||
						MapLists[CurrentIndex].GetData(posX, posY) == 5 ||
						MapLists[CurrentIndex].GetData(posX, posY) == 6) // goal
					{
						if (inputData == 3) // box
						{
							MapLists[CurrentIndex].Set(posX, posY, 5);
							ListNameReset();
						}
						else if (inputData == 4) // Player
						{
							MapLists[CurrentIndex].Set(posX, posY, 6);
							ListNameReset();
						}
					}
					else
					{
						MapLists[CurrentIndex].Set(posX, posY, inputData);
						ListNameReset();
					}
				}
			}
		}

		public void CreateNewMap()
		{
			// 새로 추가
			MapLists.Add(new MapData(CurrentMapSize));
			listBox.Items.Add("Map_" + CurrentMapSize + "[NO DATA]");
			CurrentMapSize ++;
			CurrentIndex = CurrentMapSize - 1;
			listBox.SelectedIndex = CurrentIndex;
		}

		public void InitailizeData()
		{
			MapLists[CurrentIndex].Initialize(CurrentIndex);
			ListNameReset();
		}

		public void Save()
		{
			if (CurrentIndex != -1 && CurrentIndex < CurrentMapSize)
			{
				// 맵 저장 시도
				MapLists[CurrentIndex].Save();
			}
		}

		public void Update()
		{
			CurrentIndex = listBox.SelectedIndex;
		}

		public void ListNameReset()
		{
			for (int level = 0; level < CurrentMapSize; level++)
			{
				if (MapLists[level].IsEmpty())
				{
					listBox.Items[level] = "Map_" + level + "[NO DATA]";
				}
				else
				{
					listBox.Items[level] = "Map_" + level;
				}
			}
		}

		public void DeleteMap()
		{
			int temp = CurrentIndex;
			if (temp == MapLists.Count() - 1)
			{
				if (DialogResult.No == MessageBox.Show("정말 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
				{
					return;
				}

				MapLists.RemoveAt(temp);
				listBox.Items.RemoveAt(temp);
				CurrentMapSize --;

				FileInfo fi = new FileInfo(MapData.DataDirectory + temp + MapData.Extension);
				if (fi.Exists)
				{
					fi.Delete();
				}

				CurrentIndex = temp - 1;
				listBox.SelectedIndex = CurrentIndex;
			}
			else if (temp < MapLists.Count() - 1 && temp != -1)
			{
				if (DialogResult.No == MessageBox.Show("정말 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
				{
					return;
				}

				MapLists[temp].Initialize(temp);
				listBox.Items[temp] = "Map_" + temp + "[NO DATA]";
			}
		}
		
		public void Draw(Graphics graphics, int offsetX, int offsetY)
		{
			if (CurrentIndex != -1)
			{
				for (int row = 0; row < MapLists[CurrentIndex].GetHeight(); row ++)
				{
					for (int col = 0; col < MapLists[CurrentIndex].GetWidth(); col ++)
					{
						int tile = MapLists[CurrentIndex].GetData(col, row);
						
						if (tile >= 2)
						{
							graphics.DrawImage(TileImages[Theme, 0], offsetX + col * Cell, offsetY + row * Cell, Cell, Cell);
						}

						if (tile >= 0)
						{
							graphics.DrawImage(TileImages[Theme, tile], offsetX + col * Cell, offsetY + row * Cell, Cell, Cell);
						}
					}
				}

				// 맵 영역 경계선 그리기

				graphics.DrawRectangle(new Pen(Brushes.Red, 5), offsetX, offsetY,
									   MapLists[CurrentIndex].GetWidth() * Cell, MapLists[CurrentIndex].GetHeight() * Cell);
			}
		}

		public void SavePreview()
		{
			if (CurrentIndex == -1 || CurrentIndex > CurrentMapSize - 1)
			{
				return;
			}

			int pWidth = ImageWidth;
			int pHeight = ImageHeight;
			Bitmap MapPreview = new Bitmap(pWidth, pHeight);
			Graphics g = Graphics.FromImage(MapPreview);

			// 프리뷰 그리기
			if (CurrentIndex >= 0)
			{
				int LevelClassScale = 10;
				// 레벨 계수는 1부터 시작
				int LevelScale = CurrentIndex / LevelClassScale;

				if (LevelScale > 4)
				{
					LevelScale = 4;
				}

				int drawWidth = MapLists[CurrentIndex].GetWidth();
				int drawHeight = MapLists[CurrentIndex].GetHeight();

				if (drawWidth == 0 || drawHeight == 0)
				{
					return;
				}

				int Margin = 5;

				int wSize = ((pWidth - Margin * 2) / drawWidth);
				int hSize = ((pHeight - Margin * 2) / drawHeight);

				int Size = (wSize > hSize) ? hSize : wSize;

				int offsetX = (pWidth - (Size * drawWidth)) / 2;
				int offsetY = (pHeight - (Size * drawHeight)) / 2;

				if (CurrentIndex != -1)
				{
					for (int row = 0; row < drawHeight; row ++)
					{
						for (int col = 0; col < drawWidth; col ++)
						{
							int tile = MapLists[CurrentIndex].GetData(col, row);
						
							if (tile >= 2)
							{
								g.DrawImage(TileImages[LevelScale, 0],  offsetX + col * Size, offsetY + row * Size, Size, Size);
							}

							if (tile >= 0 && tile <= 6)
							{
								g.DrawImage(TileImages[LevelScale, tile], offsetX + col * Size, offsetY + row * Size, Size, Size);
							}
						}
					}
				}

			}

			MapPreview.Save(MapPreviewDirectory + CurrentIndex + ".png", ImageFormat.Png);
			MapPreview.Dispose();
			MapPreview = null;
			g.Dispose();
		}


	}
}
