using static Spacegame.Game;

namespace Spacegame.Input
{
    public class DetectInput
    {
        public void CheckInput()
        {
            while (true)
            {
                Console.ReadKey(true);
            }
        }
    }
}