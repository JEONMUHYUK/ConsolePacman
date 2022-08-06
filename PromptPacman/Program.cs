using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop gameLoop = new GameLoop();

            gameLoop.Awake();
            gameLoop.StartScene();
            gameLoop.Start();

            while (true)
            {
                gameLoop.Update();
                gameLoop.Render();
                if (gameLoop.GetIsGameOver())
                {
                    Console.Clear();
                    gameLoop.GameOver();
                    Console.ReadKey(true);
                    break;
                }
            }
        }
    }
}
