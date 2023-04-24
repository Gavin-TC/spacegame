namespace Spacegame.Utilities;

public class Utilities
{
    // Not very useful v
    public static class NullCheckUtility
    {
        public static bool IsNull<T>(T? obj) where T : class
        {
            return obj == null;
        }
    }
    
    
}