using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class GameLoop
    {
        #region Field
        public const int mapSize = 29;      // 맵사이즈

        // 객체 선언
        GameBoard       gameBoard;     
        Player          player;
        Enemy           enemy;
        StartScene      startScene;
        GameOverScene   gameOverScene;

        // 아이템을 관리할 이차원 배열
        bool[,] itemOnOffArr;

        int score;          // 스코어

        // fps 관리 변수
        int lastTick;       
        int deltaTick;

        bool isGameOver;
        #endregion

        public bool GetIsGameOver() { return isGameOver; }      // 게임오버인지 체크하기 위한 변수
        public void Awake()
        {
            // 객체 생성
            gameBoard       = new GameBoard();
            player          = new Player();
            enemy           = new Enemy();
            startScene      = new StartScene();
            gameOverScene   = new GameOverScene();

            gameBoard.Awake();
            enemy.Awake();

            itemOnOffArr = new bool[mapSize, mapSize];
        }
        public void Start()
        {
            // 콘솔 기본 세팅
            Console.CursorVisible = false;
            Console.BufferWidth = Console.WindowWidth = 71;
            Console.BufferHeight = Console.WindowHeight = 40;

            // 스코어 초기화
            score = 0;

            // Fps 관리 변수
            lastTick = 0;
            deltaTick = 0;

            // 게임오버 변수
            isGameOver = false;

            // 객체 초기화
            gameBoard.Start();
            player.Start(gameBoard);
            enemy.Start(gameBoard, player);

            // 아이템 이차원 배열 초기화
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    itemOnOffArr[x, y] = true;
                }
            }

        }
        public void Update()
        {
            //Fps 관리
            int currentTick = Environment.TickCount;
            deltaTick = currentTick - lastTick;
            if (currentTick - lastTick > 1000 / 30)
            {
                // 객체 업데이트
                player.Update(deltaTick);
                enemy.Update(deltaTick);

                #region Player_Key_Value
                // Player_Key_Value
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        #region FirstPlayer
                        // 왼쪽 이동
                        case ConsoleKey.LeftArrow:
                            player.MoveLeft();
                            break;

                        // 위쪽 이동
                        case ConsoleKey.UpArrow:
                            player.MoveUp();
                            break;

                        // 오른쪽 이동
                        case ConsoleKey.RightArrow:
                            player.MoveRight();
                            break;

                        // 아래쪽 이동
                        case ConsoleKey.DownArrow:
                            player.MoveDown();
                            break;
                            #endregion
                    }
                }
                #endregion

                if (player.GetPosX() == enemy.GetPosX() && player.GetPosY() == enemy.GetPosY())
                {
                    isGameOver = true;
                }

                lastTick = currentTick;
            }
        }
        public void Render()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(30, 0);
            Console.WriteLine($"Score : {score}");

            // 랜더링할 배열 위치
            Console.SetCursorPosition(0, 4);
            for (int y = 0; y < mapSize; y++)
            {
                Console.Write("       ");
                for (int x = 0; x < mapSize; x++)
                {
                    // 플레이어 랜더링
                    if (y == player.GetPosY() && x == player.GetPosX())
                    {
                        player.Render();

                        // 아이템 위치 값과 같으면 score += 10, 아이템 y,x 값을 false로 할당
                        if (itemOnOffArr[y, x])
                        {
                            score += 10;
                        }
                        itemOnOffArr[y, x] = false;

                    }
                    // 적 랜더링
                    else if (y == enemy.GetPosY() && x == enemy.GetPosX())
                    {
                        enemy.Render();
                    }
                    // 아이템이 배열 값이 참이고 벽이 아니라면 아이템 출력
                    else if (itemOnOffArr[y, x] && gameBoard.tile[y, x] != GameBoard.TileType.Wall)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("＊");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = gameBoard.GetTileColor(gameBoard.tile[y, x]);
                        Console.Write("■");
                    }
                }
                Console.WriteLine();
            }
        }

        public void StartScene()
        { 
            startScene.Start();
        }

        public void GameOver()
        {
            gameOverScene.Render();
        }
    }
}
