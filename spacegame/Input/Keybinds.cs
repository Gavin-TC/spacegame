using System;
using System.Collections.Generic;
namespace Spacegame.Input;

// <summary>
// TODO: MAKE THIS FILE ACTUALLY USEFUL AND CHANGE THE CODE IN PLAYERCLASS.CS
// </summary>

public class Keybinds
{
    
    public void SetKeybinds()
    {
        // todo: set the keybindings value to a getter so that the user can change controls
        
        Dictionary<string, char> Keybindings = new Dictionary<string, char>();

        Keybindings.Add("UP", 's');
        Keybindings.Add("DOWN", 's');
        Keybindings.Add("LEFT", 'a');
        Keybindings.Add("RIGHT", 'd');
        Keybindings.Add("INTERACT", 'e');
        Keybindings.Add("INVENTORY", 'i');
    }
}