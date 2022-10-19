using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Sokoban.GameClass
{
	class Readme
	{
		private string Contents =
		"  # 변경 사항\n\n" +
		"1. TileBase, Stage 클래스를 LevelManager에서 관리				\n" +
		"2. TileBase, Stage 클래스를 수정								\n" +
		"3. 소코반 에디터 제작											\n" +
		"4. GameSystem 클래스로 전체적인 게임 Scene 제작					\n" +
		"5. 게임 레벨 구성												\n" +
		"\n" +
		"\n" +
		"  # 소코반 데이터 저장\n" +
		"\n" +
		"개인 기록은 모두 저장됩니다.\n" +
		"\n" +
		"\n" +
		"  # 바인드 키\n\n" +
		"- WASD 방향키\t\t이동 키\n" +
		"- ESC\t\t\t뒤로가기 키\n" +
		"- R\t\t\t재시작 키\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"202013207 최지욱\n" +
		"\n";
		private string Contents2 =
		"  # 소코반 에디터\n" +
		"\n" +
		"1. 에디터를 맵 생성 및 수정 후 저장시 맵 프리뷰와 맵 데이터를 생성합니다. \n" +
		"2. 생성된 데이터들은 게임에 반영됩니다.\n" +
		"3. 에디터 실행시 메인 Form은 종료되고 새로운 창으로 실행됩니다.\n" +
		"4. 반드시 저장을 해야 프리뷰가 생성됩니다.\n" +
		"5. 모든 데이터는 실행 파일과 동일한 위치에 생성됩니다.\n" +
		"6. 에디터는 게임 실행 파일과 동일한 위치에 있어야 합니다.\n" +
		"\n" +
		"\n" +
		"  # 에디터 바인드 키\n" +
		"\n" +
		"1 ~ 5\t\t타일 지정키\n" +
		"Ctrl + S\t\t저장 단축키\n" +
		"좌클릭\t\t타일 지정\n" +
		"우클릭\t\t타일 삭제\n" +
		"\n" +
		"\n" +
		"  # Goal 위치에 박스와 플레이어 놓기\n" +
		"\n" +
		"- Goal 지점을 먼저 생성하고 그 위에 플레이어나\n" +
		"  박스를 다시 생성해 Goal 위에 올린다.\n" +
		"\n" +
		"\n" +
		"  # 모든 파일이 완전히 있어야 정상 작동합니다.\n" +
		"\n" +
		"Image 폴더\t게임 및 에디터 이미지 데이터\n" +
		"Map 폴더\t게임 맵 데이터 폴더 (확장자 .dat)\n" +
		"Preview 폴더\t게임 미리보기 이미지 파일 자동 생성 폴더\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n" +
		"\n";

		private string Title = "README";

		public void Draw(Graphics graphics)
		{
			graphics.Clear(Color.Black);
			graphics.DrawString(Title, new Font("굴림", 30, FontStyle.Bold), Brushes.White, 200, 30);
			graphics.DrawString(Contents, new Font("굴림", 10), Brushes.White, 100, 100);
			graphics.DrawString(Contents2, new Font("굴림", 10), Brushes.White, 700, 100);
		}

		public void Update()
		{

		}
	}
}
