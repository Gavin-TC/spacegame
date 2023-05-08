using System;
using Spacegame.Utilities;

namespace Spacegame.Graphics
{
    public class MapRenderer
    {
        private char[,] characterBuffer; // 2D array to store ASCII characters
        private int screenWidth; // Screen width in columns
        private int screenHeight; // Screen height in rows
        private Camera camera;

        public MapRenderer(Camera camera, int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.camera = camera;

            characterBuffer = new char[screenHeight, screenWidth];
        }

        public void Clear()
        {
            Console.SetCursorPosition(0, 0);
            // for (int i = 0; i < screenHeight; i++)
            // {
            //     for (int j = 0; j < screenWidth; j++)
            //     {
            //         characterBuffer[i, j] = ' ';
            //     }
            // }
        }

        // Set a character at x and y coordinate
        public void DrawCharacter(int x, int y, char character)
        {
            
        }

        public void AddCharacter(int worldX, int worldY, char character)
        {
            int screenX = worldX - camera.X + screenWidth / 2;
            int screenY = worldY - camera.Y + screenHeight / 2;

            if (screenX >= 0 && screenX < screenWidth &&
                screenY >= 0 && screenY < screenHeight)
            {
                Global.currentMap[screenY, screenX] = character;
            }
        }

        // Draw the map using the camera
        public void DrawMap()
        {
            
        }

        public void RenderScreen()
        {
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
}


// using System.Data;
// using System.Runtime.InteropServices;
// using Spacegame.Utilities;
//
// namespace Spacegame.Graphics;
//
// public class MapRenderer
// {
//     private char[,] characterBuffer; // 2D array to store ASCII characters
//     private int screenWidth; // Screen width in columns
//     private int screenHeight; // Screen height in rows
//     
//     int mapWidth = Global.currentMap.GetLength(1);
//     int mapHeight = Global.currentMap.GetLength(0);
//
//     public MapRenderer(int screenWidth, int screenHeight)
//     {
//         this.screenWidth = screenWidth;
//         this.screenHeight = screenHeight;
//
//         characterBuffer = new char[screenHeight, screenWidth];
//         Clear();
//     }
//
//     public void Clear()
//     {
//         Console.SetCursorPosition(0, 0);
//     }
//
//     // Set a character at x and y coordinate
//     public void DrawCharacter(int x, int y, char character)
//     {
//         if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
//         {
//             characterBuffer[y, x] = character;
//         }
//     }
//     
//     public void DrawMap()
//     {
//         for (int row = 0; row < mapHeight; row++)
//         {
//             for (int col = 0; col < mapWidth; col++)
//             {
//                 char tileChar = Global.currentMap[row, col];
//                 DrawCharacter(col, row, tileChar);
//             }
//         }
//     }
//
//     public void RenderScreen()
//     {
//         Console.SetCursorPosition(0, 0);
//         for (var rows = 0; rows < screenHeight; rows++)
//         {
//             for (var cols = 0; cols < screenWidth; cols++)
//             {
//                 Console.Write(characterBuffer[rows, cols]);
//             }
//             Console.WriteLine();
//         }
//     }
// }