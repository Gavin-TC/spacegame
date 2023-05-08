using System.Diagnostics;
using Spacegame.Assets.Maps;
using Spacegame.Graphics;
using Spacegame.Menus;
using Spacegame.Objects.NPCs;
using Spacegame.PlayerStuff;
using Spacegame.Utilities;

namespace Spacegame
{
    public class Game
    {
        // TODO: ADD CAMERA! PLAYER SHOULD BE CENTER OF SCREEN
        // TODO: MAP WILL BE MANY TIMES BIGGER AFTER THIS

        private MapRenderer _mapRenderer;
        private TextRenderer _textRenderer;
        private MapManager _mapManager;
        private EntityRenderer _entityRenderer;

        private static string mapsDir = Path.Combine("Assets", "Maps");
        private static string stationsDir = Path.Combine(mapsDir, "Stations");
        private static string tutorialStation = Path.Combine(stationsDir, "tutorialstation.txt");

        private Menu _menus;
        private InventoryMenu _inventoryMenu;
        
        private Dummy _dummyClass;
        private Player _player;
        private Camera _cameraClass;

        private bool _gameRunning;
        private bool _paused;

        private static int _cameraWidth = 51;
        private static int _cameraHeight = 27;

        private static int _screenWidth;
        private static int _screenHeight;

        private static int _textWidth = _cameraWidth + 10; // Allows for double the screen width's amount of space;
        private static int _textHeight = _cameraHeight; // Allows for 10 characters of free space BELOW the screen;

        private int playerX;
        private int playerY;

        private int dummyX = 110;
        private int dummyY = 15;

        private bool goBack = false;
        private bool initialized = false;

        private void Initialize()
        {
            // Perform any game initialization tasks here
            // such as setting up game objects, loading assets, etc.
            _mapManager = MapManager.Instance;
            LoadMap("C:\\Users\\codma\\RiderProjects\\spacegame\\spacegame\\Assets\\Maps\\Stations\\tutorialstation2.txt");
            
            _screenWidth = Global.currentMap.GetLength(1);
            _screenHeight = Global.currentMap.GetLength(0);
            
            _cameraClass = new Camera(1, 1, _cameraWidth, _cameraHeight, _screenWidth, _screenHeight);
            _textRenderer = new TextRenderer(_textWidth, _textHeight);
            
            _entityRenderer = new EntityRenderer(_cameraClass, _screenWidth, _screenHeight, _screenWidth, _screenHeight);

            _menus = new Menu(this);

            _dummyClass = new Dummy(dummyX, dummyY);
            _player = new PlayerStuff.Player(20, 20, "Gavin", 1000, 100, 0, 0);

            
            // Initialize classes
            _player.Initialize();
            _cameraClass.Initialize(_player, 105, 17);

            // Anything console related
            Console.CursorVisible = false;

            // This is a windows only feature and doesn't compile/work on other OS's
            //Console.SetWindowSize(_cameraWidth+1, _cameraHeight+1);

            _gameRunning = true; // Set the game to running state
        }

        public void LoadMap(string mapName)
        {
            // TODO: make this the absolute path instead of this path; this only works on my system
            _mapManager.ConvertMap(mapName);
        }

        private void Update(double deltaTime)
        {
            // Update game state, handle input, etc.

            playerX = _player.px;
            playerY = _player.py;

            dummyX = _dummyClass.x;
            dummyY = _dummyClass.y;

            _player.Update(deltaTime);
            _cameraClass.Update(_player);

            // <summary>
            // Anything that needs to update that doesn't involve the player goes here
            // </summary>
            if (!Global.interactState) 
            {
            }
        }

        private void Render()
        {
            // Render the game screen, draw ASCII art, etc.
            if (Global.menuActive)
            {
                _menus.ShowMenu();
            }
            _cameraClass.Draw(Global.currentMap, _player, _mapRenderer);

            _entityRenderer.DrawBuffer();
            _entityRenderer.DrawEntity(110, 15, 'D');
            _entityRenderer.RenderEntityMap();
            
            _textRenderer.WriteText(0, 0, "Player X: " + _player.px);
            _textRenderer.WriteText(0, 1, "Player Y: " + _player.py);

            if (Global.interactState)
            {
                _textRenderer.WriteText(_cameraWidth + 1, -_cameraHeight + 1, "Interact Mode!");
            }
            else
            {
                _textRenderer.WriteText(_cameraWidth + 1, -_cameraHeight + 1, "              ");
            }

            
        }
        

        public void Run()
        {
            if (!initialized)
            {
                initialized = true;
                Initialize();
            }

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

                    updates += 1;
                }

                if (fDeltaTime >= fOptimalTime)
                {
                    Render();
                    
                    if (frameCount >= maxFPS)
                    {

                        // Calculate frame rate and updates per second
                        double elapsedSeconds = fpsTimer.Elapsed.TotalSeconds;
                        double fps = frames / elapsedSeconds;
                        double ups = updates / elapsedSeconds;

                        _textRenderer.WriteText(_cameraWidth + 1, -_cameraHeight + 3, "Frame rate: " + fps.ToString("0.00") + ", Updates per second: " + ups.ToString("0.00"));

                        fpsTimer.Restart();
                        
                        frames = 0;
                        updates = 0;
                    }
                    
                    fDeltaTime -= fOptimalTime;
                    frames += 1;
                }
                
                Console.CursorVisible = false;

                frameCount++;
            }
        }
    }
}