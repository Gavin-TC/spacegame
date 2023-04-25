using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Channels;
using Spacegame.Graphics;

namespace Spacegame;

class Game
{
    private Renderer renderer;
    private Map gameMap;

    private bool _gameRunning;

    private int _width, _height = 10;

    private void Initialize()
    {
        // Perform any game initialization tasks here
        // such as setting up game objects, loading assets, etc.

        gameMap = new Map();
        gameMap.InitializeMap(_width, _height);

        _gameRunning = true;  // Set the game to running state
    }
        
    private void Update(double deltaTime)
    {
        // Update game state, handle input, etc.
    }
        
    private void Render()
    {
        // Render the game screen, draw ASCII art, etc.
        gameMap.Render();
    }

    public void Run()
    {
        Initialize();

        int maxFPS = 5;
        int maxUPS = 5;

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

            if (uDeltaTime >= uOptimalTime)
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