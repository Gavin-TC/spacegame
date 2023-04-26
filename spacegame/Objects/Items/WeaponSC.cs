namespace Spacegame.Objects.Items;

public class WeaponSC : ItemSC
{
    public int damage { get; set; }

    public WeaponSC(string name, int weight, int damage) : base(name, weight)
    {
        this.damage = damage;
    }

    public void Attack(int damage)
    {
        
    }
}