using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformSokoban
{
	class Stage
	{
		private string stageName;
		public int[,] data;

		public Stage(string stageName)
		{
			this.stageName = stageName;
		}

		public string GetStageName()
		{
			return stageName;
		}

		public Point GetSize()
		{
			return new Point(data.GetLength(1), data.GetLength(0));
		}
	}
}
