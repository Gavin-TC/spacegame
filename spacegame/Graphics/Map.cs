using Spacegame.Utilities;

namespace Spacegame.Graphics;

public class Map
{
    private char[,]? _tiles;

    public void InitializeMap(int width, int height)
    {
        _tiles = new char[width, height]; // Initialize the map with the given width and height
    }

    private void SetTile(int x, int y, char tile)
    {
        if (_tiles == null)
        {
            throw new Exception("There is no map loaded or it doesn't exist yet!");
        }
        
        _tiles[x, y] = tile; // Set the tile at the given coordinates to the specified character
    }
    
    public char GetCharAtLocation(int x, int y)
    {
        return _tiles[x, y]; // Return the char at the given coordinates
    }

    public void Render()
    {
        if (_tiles == null)
        {
            throw new Exception("There is no map loaded or it doesn't exist yet!");
        }

        for (var i = 0; i < _tiles.Length; i++)
        {
            for (var j = 0; j < _tiles.Length; i++)
            {
                SetTile(i, j, '#');
            }
        }
    }
}