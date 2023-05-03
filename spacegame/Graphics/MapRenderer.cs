using System.Data;
using System.Runtime.InteropServices;
using Spacegame.Utilities;

namespace Spacegame.Graphics;

public class MapRenderer
{
    private char[,] characterBuffer; // 2D array to store ASCII characters
    private int screenWidth; // Screen width in columns
    private int screenHeight; // Screen height in rows

    public MapRenderer(int screenWidth, int screenHeight)
    {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        characterBuffer = new char[screenHeight, screenWidth];
        Clear();
    }

    public void Clear()
    {
        Console.SetCursorPosition(0, 0);
    }

    // Set a character at x and y coordinate
    public void DrawCharacter(int x, int y, char character)
    {
        if (x >= 0 && x < screenWidth && y >= 0 && y < screenHeight)
        {
            characterBuffer[y, x] = character;
        }
    }
    
    public void DrawMap(char[,] map)
    {
        int mapWidth = map.GetLength(1);
        int mapHeight = map.GetLength(0);

        for (int row = 0; row < mapHeight; row++)
        {
            for (int col = 0; col < mapWidth; col++)
            {
                char tileChar = map[row, col];
                DrawCharacter(col, row, tileChar);
            }
        }
    }
    
    public void DrawMap(int mapWidth, int mapHeight, char map)
    {
        for (int row = 0; row < mapHeight; row++)
        {
            for (int col = 0; col < mapWidth; col++)
            {
                char tileChar = Global.currentMap[row, col];
                DrawCharacter(col, row, tileChar);
            }
        }
    }

    public void DrawMap(int x, int y, char[,] map)
    {
        if (x >= 0 && x < screenWidth && y >= 0 && y < screenHeight)
        {
            characterBuffer[y, x] = map[y, x];
        }
    }

    public void RenderScreen(bool clearScreen)
    {
        if (clearScreen)
        {
            Console.Clear();
        }
        
        for (var rows = 0; rows < screenHeight; rows++)
        {
            for (var cols = 0; cols < screenWidth; cols++)
            {
                Console.Write(characterBuffer[rows, cols]);
            }
            Console.WriteLine();
        }
    }
}