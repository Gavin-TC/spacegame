﻿using System.Data;
using System.Runtime.InteropServices;

namespace Spacegame.Graphics;

// fixme: CURRENTLY INCREDIBLY BROKEN

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
        // Sets console cursor the top left, so when next write happens it will write over it
        // </summary>
        Console.SetCursorPosition(0, screenHeight+10); // screenHeight is initialized 10 'pixels' more than MapRenderer.screenHeight;
    }

    public void DrawText(int x, int y, string text)
    {
        // Janky way to do this, but the extra space clears any left over zeroes when subtracting a number...
        y += screenHeight;
        Console.SetCursorPosition(x, y);
        Console.WriteLine(text);
    }
    
    public void DrawText(int x, int y, char character, bool withQuote)
    {
        // Janky way to do this, but the extra space clears any left over zeroes when subtracting a number...
        y += screenHeight;
        Console.SetCursorPosition(x, y);

        if (withQuote)
        {
            Console.WriteLine("'" + character + "'");
        }
        else
        {
            Console.WriteLine(character);
        }
    }
}