using Sokoban.GameClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformSokoban
{
	class TileBase
	{
		public enum TileType { None = -1, Empty, Wall, Goal }
		public enum Direction { Right, Up, Left, Down, None}
		private TileType tiletype;

		private Image image;
		private int x;
		private int y;
		private bool hasPlayer;
		private Direction PlayerDirection;
		private bool hasBox;
		private LevelManager levelManager;
		private int Cell;

		public TileBase(LevelManager levelManager, int xSource, int ySource)
		{
			PlayerDirection = Direction.Down;
			this.levelManager = levelManager;
			Cell = this.levelManager.GetCellSize();
			x = xSource;
			y = ySource;
		}

		public void SetTileBase(Image imageSource, TileType tileTypeSource)
		{
			image = imageSource;
			tiletype = tileTypeSource;
		}

		public void SetBox(bool value)
		{
			hasBox = value;
		}

		public void SetPlayer(bool value)
		{
			hasPlayer = value;
		}

		public void SetPlayerDirection(Direction direction)
		{
			PlayerDirection = direction;
		}

		public bool HasBox()
		{
			return hasBox;
		}

		public TileType GetTileType()
		{
			return tiletype;
		}

		public void Draw(Graphics graphics, int offsetX, int offsetY)
		{
			int posX = offsetX + x * Cell;
			int posY = offsetY + y * Cell;

			if (tiletype == TileType.Goal)
			{
				graphics.DrawImage(levelManager.tileImages[(int)TileType.Empty], posX, posY, Cell, Cell);
			}

			if (tiletype != TileType.None)
			{
				graphics.DrawImage(image, posX, posY, Cell, Cell);
			}

			if (hasBox)
			{
				if (IsBoxOnTheGoal())
				{
					graphics.DrawImage(levelManager.boxImage[1], posX, posY, Cell, Cell);
				}
				else
				{
					graphics.DrawImage(levelManager.boxImage[0], posX, posY, Cell, Cell);
				}
			}

			if (hasPlayer)
			{
				graphics.DrawImage(levelManager.playerImage[(int)PlayerDirection], posX, posY, Cell, Cell);
			}
		}

		public bool IsBoxOnTheGoal()
		{
			return tiletype == TileType.Goal && hasBox;
		}
	}
}
