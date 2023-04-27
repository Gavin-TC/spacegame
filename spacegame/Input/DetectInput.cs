using static Spacegame.Game;

namespace Spacegame.Input
{
    public class DetectInput
    {
        public delegate void InputEventHandler(ConsoleKey key);
        public event InputEventHandler InputEvent;
        
        public void CheckInput()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                InputEvent?.Invoke(key);
            }
        }
    }
}