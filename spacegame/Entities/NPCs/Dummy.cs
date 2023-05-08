using Spacegame.Utilities;
namespace Spacegame.Entities.NPCs;

public class Dummy : Entity
{
    public int x;
    public int y;
    public char character;
    
    public int dirX;
    public int dirY;

    private bool goBack = false;

    public Dummy(int x, int y, char character) : base(x, y, character)
    {
        this.x = x;
        this.y = y;
        this.character = character;
    }
}