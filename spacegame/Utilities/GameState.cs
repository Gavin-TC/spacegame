namespace Spacegame.Utilities;

public class GameState
{
    public bool gameRunning { get; set; }
    public bool gamePaused { get; set; }

    public GameState(bool gameRunning, bool gamePaused)
    {
        this.gameRunning = gameRunning;
        this.gamePaused = gamePaused;
    }
}