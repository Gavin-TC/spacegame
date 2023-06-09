﻿namespace Spacegame.Utilities;

public class Global
{
    public const char currencyChar = '¤';
    
    public const char wallChar = '█';
    public const char closedDoorChar = '-';

    public static readonly List<char> safeChar = new List<char>{' '};

    public static bool menuActive = false;
    public static bool interactState = false;

    public static char[,] currentMap;

    // Just fills the empty spaces in the map with either a comma (',') or a period ('.');
    public void RemakeMap()
    {
        for (var i = 0; i < currentMap.GetLength(0); i++)
        {
            for (var j = 0; j < currentMap.GetLength(1); j++)
            {
                var rand = new Random();
                var randNum = rand.Next(0, 2) % 2;

                if (randNum == 0 && currentMap[i, j] == ' ')
                {
                    currentMap[i, j] = ',';
                }

                if (randNum == 1 && currentMap[i, j] == ' ')
                {
                    currentMap[i, j] = '.';
                }
                Console.Write("'" + currentMap[i, j] + "', ");
            }
            Console.WriteLine();
        }
    }
}