using System.Diagnostics;
using Spacegame.Utilities;

namespace Spacegame.Graphics;

public class Map
{
    private char[,]? _tiles;

    public void InitializeMap(int width, int height)
    {
        // Initialize the map with the given width and height
        _tiles = new char[width, height];

        Console.WriteLine(_tiles.Length);
        
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                _tiles[i, j] = '#';
            }
        }


        // {
        //     {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#'},
        //     {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
        // };
    }

    private void SetTile(int x, int y, char tile)
    {
        // Set the tile at the given coordinates to the specified character
        
        if (_tiles == null)
        {
            throw new Exception("There is no map loaded or it doesn't exist yet!");
        }

        _tiles[x, y] = tile;
    }
    
    public char GetCharAtLocation(int x, int y)
    {
        return _tiles![x, y]; // Return the char at the given coordinates
    }

    public void Render()
    {
        if (_tiles == null)
        {
            throw new Exception("There is no map loaded or it doesn't exist yet!");
        }

        for (var i = 0; i < _tiles.GetLength(0); i++)
        {
            for (var j = 0; j < _tiles.GetLength(1); j++)
            {
                Console.Write(_tiles[i, j]);
            }
            Console.WriteLine();
        }

        // foreach (var row in _tiles)
        // {
        //     Console.WriteLine(row);
        // }
    }
}