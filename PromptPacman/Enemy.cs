using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class Enemy
    {
        #region Field
        // 플레이어 변수.
        int pos_y;                       //외부에서 위치값을 get으로 읽게만 하고 set을 private함으로 set을 방지한다.
        int pos_x;                      // 즉, 단순 읽기 속성
        int dir;                        // 위치 이동을 확인할 변수이다.

        // 객체 선언
        GameBoard gameBoard;
        Random random;
        Player player;

        #endregion

        // 적 위치값을 넘기기 위한 Get 함수
        public int GetPosY() { return pos_y; }
        public int GetPosX() { return pos_x; }

        public void Awake()
        {
            random = new Random();
        }
        public void Start(GameBoard gameBoard, Player player)
        {
            // 적 위치는 랜덤 단, 랜덤한 위치가 벽 타일이라면 다시 랜덤한 위치
            pos_y = random.Next(2, GameLoop.mapSize - 2);
            pos_x = random.Next(2, GameLoop.mapSize - 2);
            if (gameBoard.tile[pos_y, pos_x] == GameBoard.TileType.Wall)
            {
                pos_y = random.Next(2, GameLoop.mapSize - 2);
                pos_x = random.Next(2, GameLoop.mapSize - 2);
            }

            // 객체 초기화 
            this.player = player;
            this.gameBoard = gameBoard;

            // 방향 초기화
            dir = 0;
        }


        int MOVE_TICK = 1000 / 5;         // 1000/5 ms 마다 움직이게
        int sumTick = 0;

        public void Update(int deltaTick)
        {
            sumTick += deltaTick;

            if (sumTick >= MOVE_TICK)
            {
                sumTick = 0;

                #region Enemy_dir_Update
                switch (dir)
                {
                    // 적 위치 변경 
                    // 상
                    case 0:
                        if (gameBoard.tile[pos_y - 1, pos_x] == GameBoard.TileType.Wall)
                        {
                            // 적이 벽을 만나면 다시 랜덤하게 방향을 바꾼다. 같은 방향이면 다시 랜덤하게 변경.
                            dir = random.Next(0, 4);
                            if (dir == 0)
                                dir = random.Next(0, 4);
                            return;
                        }
                        pos_y--;

                        break;
                    // 하
                    case 1:
                        if (gameBoard.tile[pos_y + 1, pos_x] == GameBoard.TileType.Wall)
                        {
                            dir = random.Next(0, 4);
                            if (dir == 1)
                                dir = random.Next(0, 4);
                            return;
                        }
                        pos_y++;

                        break;
                    // 좌
                    case 2:
                        if (gameBoard.tile[pos_y, pos_x - 1] == GameBoard.TileType.Wall)
                        {
                            dir = random.Next(0, 4);
                            if (dir == 2)
                                dir = random.Next(0, 4);
                            return;
                        }
                        pos_x--;

                        break;
                    // 우
                    case 3:
                        if (gameBoard.tile[pos_y, pos_x + 1] == GameBoard.TileType.Wall)
                        {
                            dir = random.Next(0, 4);
                            if (dir == 3)
                                dir = random.Next(0, 4);
                            return;
                        }
                        pos_x++;

                        break;
                }
                #endregion

                #region TarGetting_Player
                // 플레이어를 만나면 플레이어 쪽으로 위치 변환
                for (int i = 0; i < 5; i++)
                {
                    if (pos_y - i == player.GetPosY() && gameBoard.tile[pos_y - i, pos_x] != GameBoard.TileType.Wall)
                    {
                        // 플레이어가 적보다 아래에 있다면 방향은 아래로 변경
                        dir = 0;

                    }
                    if (pos_y + i == player.GetPosY() && gameBoard.tile[pos_y + i, pos_x] != GameBoard.TileType.Wall)
                    {
                        dir = 1;

                    }
                    if (pos_x - i == player.GetPosX() && gameBoard.tile[pos_y, pos_x - i] != GameBoard.TileType.Wall)
                    {
                        dir = 2;

                    }
                    if (pos_x + i == player.GetPosX() && gameBoard.tile[pos_y, pos_x + i] != GameBoard.TileType.Wall)
                    {
                        dir = 3;

                    }
                }
                #endregion

                // GameOver 조건

            }
        }

        public void Render()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("''");
            Console.ResetColor();
        }

    }
}
