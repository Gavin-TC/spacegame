using System;

namespace Spacegame
{
    class Game
    {
        private bool isRunning;
        
        public void Initialize()
        {
            // Perform any game initialization tasks here
            // such as setting up game objects, loading assets, etc.
            
            
            isRunning = true;  // Set the game to running state
        }
        
        public void Update()
        {
            // Update game state, handle input, etc.
        }
        
        public void Render()
        {
            // Render the game screen, draw ASCII art, etc.
        }

        public void Run()
        {
            Initialize();

            while (isRunning)
            {
                Update();
                Render();
            }
        }

        static void Main(string[] args)
        {
            var game = new Game();
            game.Run();
        }
    }
}