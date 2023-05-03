using System;
using Spacegame.Utilities;

namespace Spacegame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var global = new Global();
            
            //global.RemakeMap();
            game.Run();
        }
    }
}