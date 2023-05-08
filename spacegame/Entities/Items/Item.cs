namespace Spacegame.Objects.Items;

public class Item
{
    protected string name { get; set; }
    protected int weight { get; set; }

    protected Item(string name, int weight)
    {
        this.name = name;
        this.weight = weight;
    }
}