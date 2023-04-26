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
        Console.SetCursorPosition(0, 16);

        for (var rows = 0; rows < screenHeight; rows++)
        {
            for (var cols = 0; cols < screenWidth; cols++)
            {
                characterBuffer[rows, cols] = ' ';
                Console.Write(characterBuffer[rows, cols]);
            }
        }
    }

    public void DrawText(int x, int y, string text)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine(text);
    }
}