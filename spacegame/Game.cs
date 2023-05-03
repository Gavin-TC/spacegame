using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Spacegame.Graphics;
using Spacegame.Player;
using Spacegame.Utilities;

namespace Spacegame
{
    class Game
    {
        private MapRenderer _mapRenderer;
        private TextRenderer _textRenderer;
        private PlayerClass _playerClass;

        private bool _gameRunning;
        private bool _paused;

        private static int _screenWidth = 50;
        private static int _screenHeight = 25;

        private static int _textWidth = _screenWidth * 2; // Allows for double the screen width's amount of space;
        private static int _textHeight = _screenHeight + 10; // Allows for 10 characters of free space BELOW the screen;

        int playerX = 5;
        int playerY = 5;

        private bool goBack = false;

        private void Initialize()
        {
            // Perform any game initialization tasks here
            // such as setting up game objects, loading assets, etc.

            _mapRenderer = new MapRenderer(_screenWidth, _screenHeight);
            _textRenderer = new TextRenderer(_textWidth, _textHeight);
            _playerClass = new PlayerClass("Gavin", 1, 100, 0, 0);
            
            // Initialize classes
            _playerClass.Initialize();

            Console.CursorVisible = false;
            Console.SetWindowSize(100, 35); // Windows only

            _gameRunning = true; // Set the game to running state
        }

        private async void Update(double deltaTime)
        {
            // Update game state, handle input, etc.
            
            // Player updates goes under here
            playerX = _playerClass.px;
            playerY = _playerClass.py;

            _playerClass.Update(deltaTime);

            // <summary>
            // Anything that needs to update that isn't the player goes here
            // </summary>
            if (!_playerClass.interactState) 
            {
                
            }
        }

        private void Render()
        {
            // Render the game screen, draw ASCII art, etc.
            _mapRenderer.Clear();
            
            _mapRenderer.DrawMap(Global.currentMap); // Draw map to the screen buffer
            _mapRenderer.DrawCharacter(playerX, playerY, '@'); // Draw player character at current position
            //_playerClass.Render();
            _mapRenderer.RenderScreen(false);
            //_playerClass.Render();

            
            //_textRenderer.Clear();
            // _textRenderer.DrawText(0, 16, "Name: " + _playerClass.playerName);
            // _textRenderer.DrawText(0, 16, "playerX: " + playerX);
            // _textRenderer.DrawText(0, 17, "playerY: " + playerY);
            
            //_textRenderer.DrawText(0, 0, "going right: " + _playerClass.dirX);
            //_textRenderer.DrawText(0, 1, "going up: " + _playerClass.dirY);
        }

        private void TestRender()
        {
            _mapRenderer.Clear();
            _mapRenderer.DrawMap(Global.currentMap); // Draw map to the screen buffer
            _mapRenderer.DrawCharacter(playerX, playerY, _playerClass.playerChar); // Draw player character at current position
            _mapRenderer.RenderScreen(false);
            
            _textRenderer.DrawText(0, 16, Global.currentMap[14, 0], true);
        }

        public void Run()
        {
            Initialize();

            int maxFPS = 30;
            int maxUPS = 30;

            double fOptimalTime = 1_000 / maxFPS; // Optimal time to draw (1,000ms or 1 second divided by maxFPS/UPS)
            double uOptimalTime = 1_000 / maxUPS; // Optimal time to update

            double uDeltaTime = 0, fDeltaTime = 0; // Update delta time, fps delta time
            Stopwatch stopwatch = Stopwatch.StartNew();
            long startTime = stopwatch.ElapsedMilliseconds;

            Stopwatch fpsTimer = Stopwatch.StartNew();
            int frames = 0;
            int updates = 0;

            while (_gameRunning)
            {
                // Calculate difference in time
                long currentTime = stopwatch.ElapsedMilliseconds;
                fDeltaTime += (currentTime - startTime);
                uDeltaTime += (currentTime - startTime);
                startTime = currentTime;


                // <summary>
                // If amount of time since last frame is the optimal time to update, then update.
                // </summary>
                if (uDeltaTime >= uOptimalTime && !_paused)
                {
                    Update(uDeltaTime);

                    uDeltaTime -= uOptimalTime;

                    updates += 1;
                }

                if (fDeltaTime >= fOptimalTime)
                {
                    Render();
                    // TestRender();
                    fDeltaTime -= fOptimalTime;

                    frames += 1;
                }

                // if (fpsTimer.ElapsedMilliseconds >= 1000)
                // {
                //     fpsTimer.Restart();
                //
                //     Console.WriteLine("UPS: " + updates);
                //     Console.WriteLine("FPS: " + frames);
                //     
                //     frames = 0;
                //     updates = 0;
                // }
            }
        }
    }
}