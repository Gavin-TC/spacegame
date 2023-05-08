using System.Diagnostics;
using Spacegame.Assets.Maps;
using Spacegame.Entities;
using Spacegame.Graphics;
using Spacegame.Menus;
using Spacegame.Entities.NPCs;
using Spacegame.PlayerStuff;
using Spacegame.Utilities;

namespace Spacegame
{
    public class Game
    {
        // TODO: ADD CAMERA! PLAYER SHOULD BE CENTER OF SCREEN
        // TODO: MAP WILL BE MANY TIMES BIGGER AFTER THIS
        
        static DirectoryInfo mainPath =
            Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString());

        private static string assetsPath = Path.Combine(mainPath.ToString(), "Assets");
        private static string mapsPath = Path.Combine(assetsPath, "Maps");
        private static string stationsPath = Path.Combine(mapsPath, "Stations");
        
        public static string mapName = "tutorialstation2.txt";

        private Camera _cameraClass;
        private MapRenderer _mapRenderer;
        private TextRenderer _textRenderer;
        private MapManager _mapManager;
        private EntityRenderer _entityRenderer;


        private Menu _menus;
        private InventoryMenu _inventoryMenu;

        private static int _cameraWidth = 51;
        private static int _cameraHeight = 27;
        private static int _screenWidth;
        private static int _screenHeight;
        private static int _textWidth = _cameraWidth + 10; // Allows for double the screen width's amount of space;
        private static int _textHeight = _cameraHeight; // Allows for 10 characters of free space BELOW the screen;

        private Player _player;
        private int playerX;
        private int playerY;
        
        private Dummy _dummyClass;
        private int dummyX = 110;
        private int dummyY = 15;

        public List<Entity> entities;

        private bool goBack = false;
        private bool initialized = false;
        private bool _gameRunning;
        private bool _paused;

        private void Initialize()
        {
            // Perform any game initialization tasks here, such as setting up game objects, loading assets, etc.
            _mapManager = MapManager.Instance;
            LoadMap(Path.Combine(stationsPath, mapName));

            _player = new PlayerStuff.Player();
            InitializePlayer(_player, 115, 17);
            
            InitializeClasses();

            entities = new List<Entity>();
            
            _dummyClass = new Dummy(117, 15, 'D');
            entities.Add(_dummyClass);
            
            InitializeEntities(entities);
            
            // Anything console related
            Console.CursorVisible = false;

            
            _gameRunning = true; // Set the game to running state
        }
        
        private void InitializeClasses()
        {
            _screenWidth = Global.currentMap.GetLength(1);
            _screenHeight = Global.currentMap.GetLength(0);
            
            _cameraClass = new Camera(1, 1, _cameraWidth, _cameraHeight, _screenWidth, _screenHeight);
            _textRenderer = new TextRenderer(_textWidth, _textHeight);
            _entityRenderer = new EntityRenderer(_cameraClass, _screenWidth, _screenHeight, _screenWidth, _screenHeight);
            
            _cameraClass.Initialize(_player, 105, 17);

            _menus = new Menu(this);
        }

        private void InitializeEntities(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                entity.Initialize(1, 0);
            }
        }

        private void InitializePlayer(Player player, int x, int y)
        {
            _player.Initialize(x, y, 100, 1000, 100, 0, "Gavin");
        }

        

        public void LoadMap(string filePath)
        {
            // TODO: make this the absolute path instead of this path; this only works on my system
            _mapManager.ConvertMap(filePath);
        }

        private void Update(List<Entity> entities)
        {
            // Update game state, handle input, etc.

            playerX = _player.px;
            playerY = _player.py;

            foreach (Entity entity in entities)
            {
                entity.Update();
            }

            _player.Update();
            _cameraClass.Update(_player);
            
            // Anything that needs to update that doesn't involve the player goes here
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

            _entityRenderer.ClearBuffer();
            
            // foreach (Entity entity in entities)
            // {
            //     entity.Draw(_entityRenderer);
            // }
            _cameraClass.Draw(Global.currentMap, _player, _entityRenderer);
            _entityRenderer.RenderEntities(_cameraClass, _entityRenderer, entities);

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
            
            foreach (Entity entity in entities)
            {
                entity.Draw(_entityRenderer);
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
                    Update(entities);

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