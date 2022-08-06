using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class GameBoard
    {
        #region Field
        public TileType[,] tile;    // 타일타입 배열 선언
        Random random;              // 랜덤 변수 선언

        // 열거형 타일타입
        public enum TileType
        {
            Empty,
            Wall,
        }

        #endregion
        public void Awake()
        {
            tile = new TileType[GameLoop.mapSize, GameLoop.mapSize];     // 타일 배열 생성
            random = new Random();

        }

        public void Start()
        {

            // 초기화 할때 미리 배열에 값을 넣어 둔다.

            #region MapGenerater
            for (int y = 0; y < GameLoop.mapSize; y++)
            {
                for (int x = 0; x < GameLoop.mapSize; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        tile[y, x] = TileType.Wall;
                    else
                        tile[y, x] = TileType.Empty;
                }
            }


            for (int y = 0; y < GameLoop.mapSize; y++)
            {
                for (int x = 0; x < GameLoop.mapSize; x++)
                {
                    // 짝수 일때 리턴
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    // 막힌 길이 없도록 y축 벽 바로 위 x 축 끝 바로 앞은 뚫어준다.
                    if (x == GameLoop.mapSize - 2 && y == GameLoop.mapSize - 2)
                        continue;
                    // 길을 뚫어주는 작업
                    if (y == GameLoop.mapSize - 2)
                    {
                        tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    // 길을 뚫어주는 작업
                    if (x == GameLoop.mapSize - 2)
                    {
                        tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                   
                    if (random.Next(0, 2) == 0)
                    {
                        tile[y, x + 1] = TileType.Empty;  // 랜덤으로 오른쪽 뚫기
                    }
                    else
                    {
                        tile[y + 1, x] = TileType.Empty;  // 랜덤으로 아래 뚫기
                    }
                }
                #endregion
            }
        }
        // 타일 타입 색깔 정의
        public ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Black;
                case TileType.Wall:
                    return ConsoleColor.DarkBlue;
                default:
                    return ConsoleColor.Black;
            }
        }
    }
}
