using System.Data;
using System.Runtime.InteropServices;

namespace Spacegame.Graphics;

public class TextRenderer
{
    private char[,] characterBuffer; // 2D array to store ASCII characters
    private int screenWidth; // Screen width in columns
    private int screenHeight; // Screen height in rows

    public TextRenderer(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        characterBuffer = new char[screenHeight, screenWidth];
        Clear();
    }

    public void Clear()
    {
        // <summary>
        // Sets console cursor the top left, so when next write happens it will overwrite
        // what was already there instead of clearing it first
        // </summary>
        Console.SetCursorPosition(0, screenHeight+10); // screenHeight is initialized 10 'pixels' more than MapRenderer.screenHeight;
    }

    public void DrawText(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        
        // Janky way to do this, but the extra space clears any left over zeroes when subtracting a number...
        Console.WriteLine(text + " "); 
    }
}