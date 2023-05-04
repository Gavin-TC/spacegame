using Spacegame.Utilities;

namespace Spacegame.Objects.NPCs;

public class Dummy
{
    public int x;
    public int y;
    public int dirX;
    public int dirY;

    private bool goBack = false;

    public Dummy(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    // How many steps should the dummy take in the X-axis
    public void MoveX(int steps)
    {
        for (var i = 0; i < steps; i++)
        {
            if (!CheckCollision())
            {
                dirX = 1;
                x += dirX;
            }
            else
            {
                dirX = -1;
                x += dirX;
            }
        }
    }
    
    public void MoveY(int steps)
    {
        if (!goBack)
        {
            for (var i = 0; i < steps; i++)
            {
                dirY = -1;
                if (!CheckCollision())
                {
                    y += dirY;
                }
            }
            goBack = true;
        }

        if (goBack)
        {
            for (var i = 0; i < steps; i++)
            {
                dirY = 1;
                if (!CheckCollision())
                {
                    y += dirY;
                }
            }
            goBack = false;
        }
        
        
    }

    private bool CheckCollision()
    {
        if (Global.currentMap[y+dirY, x+dirX] == Global.wallChar)
        {
            return false;
        }

        return true;
    }
}