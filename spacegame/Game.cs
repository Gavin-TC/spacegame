using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Spacegame.Graphics;
using Spacegame.Objects;

namespace Spacegame;

class Game
{
    private MapRenderer _mapRenderer;
    private TextRenderer _textRenderer;

    private bool _gameRunning;
    private bool _paused;

    private static int _screenWidth = 25;
    private static int _screenHeight = 15;
    
    private static int _textWidth = _screenWidth;
    private static int _textHeight = 1;
    
    int playerX = 1;
    int playerY = 5;

    private bool goBack = false;

    public char[,] map = {
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
        { '█', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '█' },
        { '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█', '█' }
    };

    private void Initialize()
    {
        // Perform any game initialization tasks here
        // such as setting up game objects, loading assets, etc.

        _mapRenderer = new MapRenderer(_screenWidth, _screenHeight);
        _textRenderer = new TextRenderer(_textWidth, _textHeight);

        Console.CursorVisible = false;

        _gameRunning = true;  // Set the game to running state
    }
        
    private async void Update(double deltaTime)
    {
        // Update game state, handle input, etc.

        if (playerX + 1 < _screenWidth-2 && !goBack)
        {
            playerX += 1; 
        }
        else
        {
            goBack = true;
        }

        if (playerX - 1 > 0  && goBack)
        {
            playerX -= 1;
        }
        else
        {
            goBack = false;
        }
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
        
    }

    public void Run()
    {
        Initialize();

        int maxFPS = 60;
        int maxUPS = 15;

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