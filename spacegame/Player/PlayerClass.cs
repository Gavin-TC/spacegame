using System.Globalization;
using Spacegame.Input;
using Spacegame.Utilities;

namespace Spacegame.Player
{
    public class PlayerClass
    {
        private HandleInput? _handleInput;

        public int px { get; set; } // Player left and right coordinate
        public int py { get; set; }

        // public int dirX; // Player direction up and down
        // public int dirY;

        public int dirX = 0;
        public int dirY = 0;
        
        public char playerChar;
        public string playerName;

        public bool interactState = false;
        
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

        public (int px, int py) Position()
        {
            return (px, py);
        }

        public void OnInput(ConsoleKey key)
        {
            
            
            //Console.WriteLine("PRESSING KEY " + key);
            switch (key)
            {
                case ConsoleKey.W:
                    dirY = -1;
                    // if (!Obstruction(true))
                    // {
                    //     py -= 1;
                    // }
                    break;
                
                case ConsoleKey.S:
                    dirY = 1;
                    // if (!Obstruction(true))
                    // {
                    //     py += 1;
                    // }
                    break;
                
                case ConsoleKey.D:
                    dirX = 1;
                    // if (!Obstruction(false))
                    // {
                    //     px += 1;
                    // }
                    break;
                
                case ConsoleKey.A:
                    dirX = -1;
                    // if (!Obstruction(false))
                    // {
                    //     px -= 1;
                    // }
                    break;
                
                case ConsoleKey.E: // interact;
                    interactState = !interactState;
                    break;
            }

            


            // This doesn't work, continues to read input and delays it, so you keep moving becuase you've held the key.
            //Thread.Sleep(speed);
        }
        
        public bool Obstruction()
        {
            if (dirY is -1 or 1)
            {
                if (dirY == -1 && Global.currentMap[py - 1, px] is Global.wallChar or Global.closedDoorChar
                    || dirY == 1 && Global.currentMap[py + 1, px] is Global.wallChar or Global.closedDoorChar)
                {
                    return true;
                }
            }

            if (dirX is -1 or 1)
            {
                if (dirX == 1 && Global.currentMap[py, px + 1] is Global.wallChar or Global.closedDoorChar
                    || dirX == -1 && Global.currentMap[py, px - 1] is Global.wallChar or Global.closedDoorChar)
                {
                    return true;
                }
            }

            return false;
        }
        
        // _mapRenderer.DrawCharacter(playerX, playerY, '@'); // Draw player character at current position

        public void Movement()
        {
            // Move player based on input
            if (dirY == -1 && !Obstruction())
            {
                py--;
            }
            if (dirY == 1 && !Obstruction())
            {
                py++;
            }
            if (dirX == 1 && !Obstruction())
            {
                px++;
            }
            if (dirX == -1 && !Obstruction())
            {
                px--;
            }
        }

        public void Render()
        {
            Global.currentMap[py, px] = playerChar;
        }
        
        public void Update(double deltaTime)
        {
            Movement();
            dirX = 0;
            dirY = 0;
        }
    }
}