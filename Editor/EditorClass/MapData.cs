using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.EditorClass
{
	class MapData
	{
		public const string DataDirectory = @"Map\Map_";
		public const string Extension = ".dat";
		
		private int Level;
		private int[,] Data;

		// 에디터 상의 유효 영역
		public const int AllowWidth = 21;
		public const int AllowHeight = 18;

		// 맵의 유효 영역
		private int Width;
		private int Height;

		public MapData(int level)
		{
			Initialize(level);
		}

		public void Initialize(int level)
		{
			Width = 0;
			Height = 0;
			Level = level;

			Data = new int[AllowHeight, AllowWidth];

			for (int row = 0; row < AllowHeight; row ++)
			{
				for (int col = 0; col < AllowWidth; col ++)
				{
					Data[row, col] = -1;
				}
			}
		}

		public int GetWidth()
		{
			return Width;
		}

		public int GetHeight()
		{
			return Height;
		}

		public int GetData(int posX, int posY)
		{
			if(posX >= 0 && posX < MapData.AllowWidth && posY >= 0 && posY < MapData.AllowHeight)
			{
				return Data[posY, posX];
			}
			else
			{
				return -2; // Out of Range
			}
		}

		public bool IsEmpty()
		{
			return (Width == 0 && Height == 0);
		}

		/// <summary>
		/// 맵 데이터 설정
		/// </summary>
		public void Set(int posX, int posY, int input)
		{
			// Out of Range
			if (posX < 0 && posX >= MapData.AllowWidth && posY < 0 && posY >= MapData.AllowHeight)
			{
				return;
			}

			Data[posY, posX] = input;

			if (input >= 0)
			{
				// 영역 재설정
				if (posX > Width - 1)
				{
					Width = posX + 1;
				}
				if (posY > Height - 1)
				{
					Height = posY + 1;
				}
			}
			else
			{
				bool breakAll = false;

				Height = 0;
				Width = 0;

				// 영역 재설정
				for (int row = AllowHeight - 1; row >= 0; row --)
				{
					for (int col = AllowWidth - 1; col >= 0; col --)
					{
						if (Data[row, col] != -1)
						{
							Height = row + 1;
							breakAll = true;
							break;
						}
					}
					if (breakAll) {break;}
				}
				
				breakAll = false;

				for (int col = AllowWidth - 1; col >= 0; col --)
				{
					for (int row = AllowHeight - 1; row >= 0; row --)
					{
						if (Data[row, col] != -1)
						{
							Width = col + 1;
							breakAll = true;
							break;
						}
					}
					if (breakAll) {break;}
				}
			}
		}

		/// <summary>
		/// 저장
		/// </summary>
		public bool Save()
		{
			try
			{
				FileStream fs = new FileStream(DataDirectory + Level + Extension, FileMode.Create);

				StreamWriter r = new StreamWriter(fs);

				r.WriteLine(Width);
				r.WriteLine(Height);
				r.WriteLine("");
				r.WriteLine("");

				
				for (int row = 0; row < Height; row ++)
				{
					for (int col = 0; col < Width; col ++)
					{
						r.WriteLine(Data[row, col]);
					}
				}

				r.Close();
				return true;
			}
			catch (IOException ex)
			{
				return false;
			}
		}

		/// <summary>
		/// 불러오기
		/// </summary>
		public bool Load()
		{
			try
			{
				FileStream fs = new FileStream(DataDirectory + Level + Extension, FileMode.Open);
				StreamReader r = new StreamReader(fs);

				Width  = Convert.ToInt32(r.ReadLine());
				Height = Convert.ToInt32(r.ReadLine());
				r.ReadLine();
				r.ReadLine();

				
				for (int row = 0; row < Height; row ++)
				{
					for (int col = 0; col < Width; col ++)
					{
						Data[row, col] = Convert.ToInt32(r.ReadLine());
					}
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
