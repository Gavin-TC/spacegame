using Spacegame.Input;

namespace Spacegame.Player
{
    public class PlayerClass
    {
        private DetectInput _detectInput;

        public int px; // Player x and y
        public int py;
        public char playerChar;

        private bool up;
        private bool down;
        private bool right;
        private bool left;

        public int speed { get; set; }

        public int health { get; set; }
        public int exp { get; set; } // Experience points
        public int level { get; set; } // Current level

        public PlayerClass(int speed, int health, int exp, int level)
        {
            this.speed = speed;
            this.health = health;
            this.exp = exp;
            this.level = level;
        }
        
        public void Initialize()
        {
            px = 5;
            py = 5;

            _detectInput = new DetectInput();
            _detectInput.InputEvent += OnInput;

            Thread inputThread = new Thread(_detectInput.CheckInput);
            inputThread.Start();
        }

        public void OnInput(ConsoleKey key)
        {
            //Console.WriteLine("PRESSING KEY " + key);
            switch (key)
            {
                case ConsoleKey.W:
                    py -= 1;
                    break;
                case ConsoleKey.S:
                    py += 1;
                    break;
                case ConsoleKey.D:
                    px += 1;
                    break;
                case ConsoleKey.A:
                    px -= 1;
                    break;
            }
            
            // This doesn't work, continues to read input and delays it, so you keep moving if you've held the key.
            //Thread.Sleep(speed);
        }

        public void Movement()
        {
        }

        public void Update(double deltaTime)
        {
            Movement();
        }
    }
}