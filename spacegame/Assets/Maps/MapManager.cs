using Spacegame.Utilities;
namespace Spacegame.Assets.Maps;

public class MapManager
{
    private Map currentMap;
    private Dictionary<string, Map> maps = new Dictionary<string, Map>();

    private char[,] mapToDraw;
    
    // Create a map based on .txt files.
    public void ConvertMap(string mapFilePath)
    {
        string[] lines = File.ReadAllLines(mapFilePath);

        if (lines.Length == 0)
        {
            throw new Exception("Map file is empty!");
        }

        int height = lines.Length;
        int width = lines[0].Length;

        char[,] map = new char[height, width];

        for (var y = 0; y < height; y++)
        {
            string line = lines[y];
            for (var x = 0; x < width; x++)
            {
                char tile = ' ';
                if (x < line.Length)
                {
                    if (line[x] == 's')
                    {
                        var random = new Random();
                        var randNum = random.Next(0, random.Next(8, 20));

                        switch (randNum)
                        {
                            case 1:
                                randNum = random.Next(0, 5);
                                switch (randNum)
                                {
                                    case 1:
                                        tile = '*';
                                        break;
                                    case 2:
                                        tile = '.';
                                        break;
                                    case 3:
                                        tile = ',';
                                        break;
                                    case 4:
                                        tile = '\'';
                                        break;
                                }

                                break;

                            default:
                                tile = ' ';
                                break;
                        }
                    }
                    else
                    {
                        tile = line[x];
                    }
                }

                map[y, x] = tile;
            }
        }

        Global.currentMap = map;
    }

    public void AddMap(Map map)
    {
        maps[map.name] = map;
    }

    public void ChangeMap(string mapName)
    {
        if (maps.ContainsKey(mapName))
        {
            currentMap = maps[mapName];
        }
        else
        {
            throw new Exception("This map does not exist!");
        }
    }

    private static MapManager instance;
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MapManager();
            }
            return instance;
        }
    }
}