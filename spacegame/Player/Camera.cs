using Spacegame.Graphics;
using Spacegame.Utilities;

namespace Spacegame.Player;

public class Camera {
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    
    public Camera(int x, int y, int width, int height, int mapWidth, int mapHeight) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        MapWidth = mapWidth;
        MapHeight = mapHeight;
    }
    
    public void Initialize(PlayerClass player)
    {
        player.px = Width / 2;
        player.py = Height / 2;
    }
    
    public void Update(PlayerClass player) {
        // Center the camera on the player
        X = player.px - (Width / 2);
        Y = player.py - (Height / 2); // + 1;

        // Make sure the camera doesn't go off the edges of the map
        X = Math.Max(0, Math.Min(MapWidth - Width, X));
        Y = Math.Max(0, Math.Min(MapHeight - Height, Y));
    }
    
    public void Draw(char[,] map, MapRenderer mapRenderer) {
        // Draw the part of the map that's within the camera's view
        
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width+1; x++) { // Width+1 ensures the it's drawing one more pixel to the width so the player is in the center.
                //mapRenderer.DrawMap(Width, Height, map[Y+y, X+x]);
                Console.Write(map[Y + y, X + x]);
            }
            Console.WriteLine();
        }
    }
}