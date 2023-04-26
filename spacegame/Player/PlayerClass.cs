using Spacegame.Input;

namespace Spacegame.Player
{
    public class PlayerClass
    {
        static DetectInput inputDetector = new DetectInput();
        Thread inputThread = new Thread(inputDetector.CheckInput);

        private int px; // Player x and y
        private int py;
        private char playerChar;

        public int health { get; set; }
        public int exp { get; set; } // Experience points
        public int level { get; set; } // Current level

        public void Initialize()
        {
            inputThread.Start();
        }

        public void Movement()
        {
            Console.WriteLine();
        }

        public void Update()
        {
            Movement(); // Check if player has clicked a button
        }
    }
}