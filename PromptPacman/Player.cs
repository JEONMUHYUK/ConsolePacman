using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class Player
    {
        #region Field
        // 플레이어 변수.
        int posY;                       //외부에서 위치값을 get으로 읽게만 하고 set을 private함으로 set을 방지한다.
        int posX;                      // 즉, 단순 읽기 속성
        int dir;            // 위치 이동을 확인할 변수이다.

        string prefab;
        int PrefabAinmationCount;
        GameBoard gameBoard;
        #endregion
        public int GetPosY() { return posY; }
        public int GetPosX() { return posX; }


        public void Start(GameBoard gameBoard)
        {
            // 플레이어 초기값
            posY = 7;
            posX = 7;
            dir = 1;                        // 플레이어 이동방향을 담을 변수 초기값.
            prefab = "⊂";
            PrefabAinmationCount = 0;

            this.gameBoard = gameBoard;
        }

        #region Player_Render_&_Move

        private int MOVE_TICK = 100;         // 0.1 초 마다 움직이게
        private int _sumTick = 0;
        public void Update(int deltaTick)   // deltaTick은 메인 클래스에서 1프레임이 지날때마다 업데이트 되는 값을 받아온다
        {
            _sumTick += deltaTick;          // sumTick에 deltaTick을 더해준다.
            if (_sumTick >= MOVE_TICK)      //만약 sumTIck이 MOVE_TICK보다 커지거나 같아지면
            {
                _sumTick = 0;               //_sumTick을 초기화 시켜준다.


                #region Player_Pos_Update
                switch (dir)
                {
                    case 0:
                        // 오른쪽 이동
                        if (gameBoard.tile[posY, posX + 1] != GameBoard.TileType.Wall)
                            posX++;
                        if (PrefabAinmationCount > 5)
                        { 
                            prefab = "⊂";
                            PrefabAinmationCount = 0;
                        }
                        else prefab = " <";
                        break;
                    case 1:
                        // 위쪽 이동
                        if (gameBoard.tile[posY - 1, posX] != GameBoard.TileType.Wall)
                            posY--;
                        if (PrefabAinmationCount > 5)
                        {
                            prefab = "∪";
                            PrefabAinmationCount = 0;
                        }
                        else prefab = "∨";
                        break;
                    case 2:
                        // 왼쪽 이동
                        if (gameBoard.tile[posY, posX - 1] != GameBoard.TileType.Wall)
                            posX--;
                        if (PrefabAinmationCount > 5)
                        {
                            prefab = "⊃";
                            PrefabAinmationCount = 0;
                        }
                        else prefab= "> ";
                        break;
                    case 3:
                        // 아래쪽 이동
                        if (gameBoard.tile[posY + 1, posX] != GameBoard.TileType.Wall)
                            posY++;
                        if (PrefabAinmationCount > 5)
                        {
                            prefab = "∩";
                            PrefabAinmationCount = 0;
                        }
                        else prefab = "∧";
                        break;
                }
                #endregion

                PrefabAinmationCount++;
            }
        }
        #endregion

        public void Render()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write($"{prefab}");
            Console.ResetColor();
        }

        public void MoveLeft()
        {
            // 왼쪽 이동
            dir = 2;
        }
        public void MoveRight()
        {
            // 오른쪽 이동
            dir = 0;
        }
        public void MoveUp()
        {
            // 위쪽 이동
            dir = 1;
        }
        public void MoveDown()
        {
            // 아래쪽 이동
            dir = 3;
        }
    }
}
