using Spacegame.Graphics;
using Spacegame.Utilities;

namespace Spacegame.Menus;

public class Menu
{
    private readonly Game game;
    
    public Menu(Game game)
    {
        this.game = game;
    }
    
    public void ShowMenu()
    {
        var textRenderer = new TextRenderer(30, 20);
        
        Console.Clear();
        
        Console.WriteLine(" PAUSED ");
        Console.WriteLine();
        Console.WriteLine("Press enter to resume");
        Console.WriteLine("1) Options");
        Console.WriteLine("2) Help");
        Console.WriteLine("3) Main Menu");
        Console.WriteLine();
        int choice = Console.Read();

        switch (choice)
        {
            case 1:
                Global.menuActive = false;
                game.Run();
                break;
            
            case 2:
                break;
            
            case 3:
                break;
            
            default:
                Global.menuActive = false;
                game.Run();
                break;
        }
    }
    
    private class HelpMenu
    {
    }

    private class MainMenu
    {
    }
}