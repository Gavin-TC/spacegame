using Spacegame.Graphics;

namespace Spacegame;

public class Game
{
    private Renderer renderer;
    private Map gameMap;
    
    private bool _isRunning;
        
    private void Initialize()
    {
        // Perform any game initialization tasks here
        // such as setting up game objects, loading assets, etc.

        gameMap = new Map();
        gameMap.InitializeMap(20, 20);

        _isRunning = true;  // Set the game to running state
    }
        
    private void Update()
    {
        // Update game state, handle input, etc.
        gameMap.Render();
    }
        
    private void Render()
    {
        // Render the game screen, draw ASCII art, etc.
        gameMap.Render();
    }

    public void Run()
    {
        Initialize();

        while (_isRunning)
        {
            Update();
            Render();
        }
    }
}