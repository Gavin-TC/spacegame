namespace Spacegame.Assets.Maps;

public class Map
{
    public string name;
    public List<List<int>> tiles;

    public Map(string name, List<List<int>> tiles)
    {
        this.name = name;
        this.tiles = tiles;
    }
}