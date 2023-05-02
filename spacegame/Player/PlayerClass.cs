using System.Globalization;
using Spacegame.Input;
using Spacegame.Utilities;

namespace Spacegame.Player
{
    public class PlayerClass
    {
        private HandleInput? _handleInput;

        public int px; // Player left and right coordinate
        public int py;

        // public int dirX; // Player direction up and down
        // public int dirY;

        public bool goingRight;
        public bool goingUp;
        
        public char playerChar;
        public string playerName;
        
        public int speed { get; set; }

        public int health { get; set; }
        public int exp { get; set; } // Experience points
        public int level { get; set; } // Current level

        public PlayerClass(string playerName, int speed, int health, int exp, int level)
        {
            this.speed = speed;
            this.health = health;
            this.exp = exp;
            this.level = level;
            this.playerName = playerName;
        }
        
        public void Initialize()
        {
            px = 5;
            py = 5;

            _handleInput = new HandleInput();
            _handleInput.InputEvent += OnInput;

            var inputThread = new Thread(_handleInput.CheckInput);
            inputThread.Start();
        }

        public void OnInput(ConsoleKey key)
        {
            //Console.WriteLine("PRESSING KEY " + key);
            switch (key)
            {
                case ConsoleKey.W:
                    goingUp = true;
                    if (!CheckWall(true))
                    {
                        py -= 1;
                    }
                    break;
                
                case ConsoleKey.S:
                    goingUp = false;
                    if (!CheckWall(true))
                    {
                        py += 1;
                    }
                    break;
                
                case ConsoleKey.D:
                    goingRight = true;
                    if (!CheckWall(false))
                    {
                        px += 1;
                    }
                    break;
                
                case ConsoleKey.A:
                    goingRight = false;
                    if (!CheckWall(false))
                    {
                        px -= 1;
                    }
                    break;
                
                case ConsoleKey.E: // interact;
                    break;
            }


            // This doesn't work, continues to read input and delays it, so you keep moving becuase you've held the key.
            //Thread.Sleep(speed);
        }
        
        public bool CheckWall(bool checkUp)
        {
            if (checkUp)
            {
                if (goingUp && Global.currentMap[py - 1, px] == Global.wallChar
                    || !goingUp && Global.currentMap[py + 1, px] == Global.wallChar)
                {
                    return true;
                }
            }

            if (!checkUp)
            {
                if (goingRight && Global.currentMap[py, px + 1] == Global.wallChar
                    || !goingRight && Global.currentMap[py, px - 1] == Global.wallChar)
                {
                    return true;
                }
            }

            return false;
        }

        public char Above()
        {
            return Global.currentMap[px, py - 1];
        }

        public char Below()
        {
            return Global.currentMap[px, py + 1];
        }

        public char Right()
        {
            return Global.currentMap[px + 1, py];
        }

        public char Left()
        {
            return Global.currentMap[px - 1, py];
        }
        
        public void Update(double deltaTime)
        {
        }
    }
}