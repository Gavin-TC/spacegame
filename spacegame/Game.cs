using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Spacegame.Graphics;
using Spacegame.Player;

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

        int playerX = 1;
        int playerY = 5;

        private bool goBack = false;

        public char[,] map =
        {
            { '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
            { '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█' }
        };

        private void Initialize()
        {
            // Perform any game initialization tasks here
            // such as setting up game objects, loading assets, etc.

            _mapRenderer = new MapRenderer(_screenWidth, _screenHeight);
            _textRenderer = new TextRenderer(_textWidth, _textHeight);
            _playerClass = new PlayerClass(1000, 100, 0, 0);

            _playerClass.Initialize();

            Console.CursorVisible = false;

            _gameRunning = true; // Set the game to running state
        }

        private async void Update(double deltaTime)
        {
            // Update game state, handle input, etc.

            playerX = _playerClass.px;
            playerY = _playerClass.py;
        }

        private void Render()
        {
            // Render the game screen, draw ASCII art, etc.
            _mapRenderer.Clear();
            
            _mapRenderer.DrawMap(map); // Draw map to the screen buffer
            _mapRenderer.DrawCharacter(playerX, playerY, '@'); // Draw player character at current position
            _mapRenderer.RenderScreen(false);

            
            _textRenderer.Clear();
            
            _textRenderer.DrawText(0, 16, "playerX: " + playerX);
            _textRenderer.DrawText(0, 17, "playerY: " + playerY);
            _textRenderer.DrawText(13, 16, "goBack: " + goBack);

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