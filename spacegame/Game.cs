using System.Diagnostics;
using Spacegame.Graphics;
using Spacegame.Objects.NPCs;
using Spacegame.PlayerStuff;
using Spacegame.Utilities;

namespace Spacegame
{
    class Game
    {
        // TODO: ADD CAMERA! PLAYER SHOULD BE CENTER OF SCREEN
        // TODO: MAP WILL BE MANY TIMES BIGGER AFTER THIS

        private MapRenderer _mapRenderer;
        private TextRenderer _textRenderer;
        private Dummy _dummyClass;
        private Player _player;
        private Camera _cameraClass;

        private bool _gameRunning;
        private bool _paused;


        private static int _cameraWidth = 30;
        private static int _cameraHeight = 18;
        
        private static int _screenWidth = _cameraWidth;
        private static int _screenHeight = _cameraHeight;

        private static int _textWidth = _cameraWidth + 10; // Allows for double the screen width's amount of space;
        private static int _textHeight = _cameraHeight + 20; // Allows for 10 characters of free space BELOW the screen;

        private int playerX;
        private int playerY;

        private void Initialize()
        {
            // Perform any game initialization tasks here
            // such as setting up game objects, loading assets, etc.

            _mapRenderer = new MapRenderer(_screenWidth, _screenHeight);
            _textRenderer = new TextRenderer(_textWidth, _textHeight);

            _dummyClass = new Dummy(10, 10);
            
            _player = new PlayerStuff.Player("Gavin", 1000, 100, 0, 0);
            _cameraClass = new Camera(1, 1, _cameraWidth, _cameraHeight, Global.currentMap.GetLength(1), Global.currentMap.GetLength(0));
            
            // Initialize classes
            _player.Initialize();
            _cameraClass.Initialize(_player);

            // Anything console related
            Console.CursorVisible = false;

            // Looks weird
            //Console.ForegroundColor = ConsoleColor.Gray;

            // This is a windows only feature and doesn't compile/work on other OS's
            //Console.SetWindowSize(_cameraWidth+1, _cameraHeight+1);

            _gameRunning = true; // Set the game to running state
        }

        private void Update(double deltaTime)
        {
            // Update game state, handle input, etc.

            playerX = _player.px;
            playerY = _player.py;

            _player.Update(deltaTime);
            _cameraClass.Update(_player);
            
            _dummyClass.MoveY(4);

            // <summary>
            // Anything that needs to update that isn't the player goes here
            // </summary>
            if (!_player.interactState) 
            {
                
            }
        }

        private void Render()
        {
            // Render the game screen, draw ASCII art, etc.
            _cameraClass.Draw(Global.currentMap, _player, _mapRenderer);
            
            _mapRenderer.Clear();
            _mapRenderer.DrawMap(_cameraClass);
            _mapRenderer.DrawCharacter(_dummyClass.x, _dummyClass.y, 'D');
            _mapRenderer.RenderScreen();
            
            
            //_textRenderer.Clear();

            //_playerClass.Inventory.GetItems();
            
            // _textRenderer.WriteText(0, 0, "Current quest: ");
            // if (_player.interactState)
            // {
            //     Console.WriteLine();
            //     _textRenderer.WriteText(_cameraWidth + 1, -_cameraHeight - 1, "Interact mode");
            // }
            // else
            // {
            //     _textRenderer.WriteText(_cameraWidth + 1, -_cameraHeight - 1, "             ");
            // }
        }
        

        public void Run()
        {
            Initialize();

            int maxFPS = 30;
            int maxUPS = 30;

            int frameCount = 0;

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

                    //updates += 1;
                }

                if (fDeltaTime >= fOptimalTime)
                {
                    Render();

                    if (frameCount >= maxFPS)
                    {
                        frameCount = 0;
                    }
                    
                    fDeltaTime -= fOptimalTime;
                    //frames += 1;
                }
                
                Console.CursorVisible = false;

                frameCount++;
                

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