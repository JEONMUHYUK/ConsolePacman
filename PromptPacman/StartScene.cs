using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptPacman
{
    internal class StartScene
    {
        public void Start()
        {

            Console.Title = "Prompt_PacMan";
            RunMainMenu();


        }

        private void RunMainMenu()
        {
            string prompt = @"




       ________        ________          ________          _____ ______           ________          ________      
      |\   __  \      |\   __  \        |\   ____\        |\   _ \  _   \        |\   __  \        |\   ___  \    
      \ \  \|\  \     \ \  \|\  \       \ \  \___|        \ \  \\\__\ \  \       \ \  \|\  \       \ \  \\ \  \   
       \ \   ____\     \ \   __  \       \ \  \            \ \  \\|__| \  \       \ \   __  \       \ \  \\ \  \  
        \ \  \___|      \ \  \ \  \       \ \  \____        \ \  \    \ \  \       \ \  \ \  \       \ \  \\ \  \ 
         \ \__\          \ \__\ \__\       \ \_______\       \ \__\    \ \__\       \ \__\ \__\       \ \__\\ \__\
          \|__|           \|__|\|__|        \|_______|        \|__|     \|__|        \|__|\|__|        \|__| \|__|
                                                                                                            
                                                                                                            
                                                                                                            

                                                                     

";            // 프롬프트 값
            string[] options = { "Play", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    GameStart();
                    break;
                case 1:
                    ExitGame();
                    break;
            }
        }

        private void ExitGame()
        {
            Console.WriteLine("\nPressed any Key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void GameStart()
        {
            Console.Clear();
            return;
        }
    }
}
