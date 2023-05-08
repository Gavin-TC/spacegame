using Spacegame.Graphics;
using Spacegame.Utilities;
namespace Spacegame.Entities;

public class Entity
{
    public int x;
    public int y;

    public int dirX;
    public int dirY;
    
    public char character;

    public Entity(int x, int y, char character)
    {
        this.x = x;
        this.y = y;
        this.character = character;
    }

    public void Initialize(int dirX, int dirY)
    {
        this.dirX = dirX;
        this.dirY = dirY;
    }

    public void Update()
    {
        if (!CheckCollision())
        {
            x += dirX;
            y += dirY;            
        }
    }
    
    public bool CheckCollision()
    {
        if (Global.currentMap[y+dirY, x+dirX] == Global.wallChar)
        {
            return false;
        }
        return true;
    }

    public void Draw(EntityRenderer renderer)
    {
        renderer.DrawEntity(x, y, character);
    }
}