using Spacegame.Utilities;

namespace Spacegame.Objects.Items;

// Technically a gun
public class Bullet : Weapon
{
    public int x;
    public int y;
    public int dirX;
    public int dirY;
    public char bulletChar = '*';

    private bool canMove = true;
    
    public Bullet(int x, int y, int dirX, int dirY, int damage, string name, int weight) : base(damage, name, weight)
    {
        this.x = x;
        this.y = y;
        this.dirX = dirX;
        this.dirY = dirY;
        
        this.damage = damage;
        this.name = name;
        this.weight = weight;
    }

    public void Move()
    {
        if (canMove)
        {
            Global.currentMap[y, x] = ' ';

            x += dirX;
            y += dirY;

            // x = nextX;
            // y = nextY;

            Global.currentMap[y, x] = bulletChar;
        }
    }

    public bool CollidedWall()
    {
        int nextX = x + dirX;
        int nextY = y + dirY;
        
        if (Global.currentMap[nextY, nextX] == '█')
        {
            canMove = false;
            return true;
        }

        return false;
    }
}