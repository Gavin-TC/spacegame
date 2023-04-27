using Spacegame.Objects.Items;

namespace Spacegame.Objects.Items
{
    public class Weapon : Item
    {
        public int damage { get; set; }

        public Weapon(int damage, string name, int weight) : base(name, weight)
        {
            this.damage = damage;
            this.name = name;
            this.weight = weight;
        }

        public void Attack(int damage)
        {

        }
    }

    public class Material : Item
    {
        public int amount { get; set; }

        public Material(int amount, string name, int weight) : base(name, weight)
        {
            this.amount = amount;

            this.name = name;
            this.weight = weight;
        }
    }

    public class ConsumableFood : Item
    {
        public int foodAmount { get; set; } // Amount of food left
        public int healthGiven { get; set; } // How much health the food will give the consumer
        public int foodQuality { get; set; } // How quality the food is; can determine possible sicknesses (if implemented)

        public ConsumableFood(int foodAmount, int healthGiven, int foodQuality, string name, int weight) : base(name, weight)
        {
            this.foodAmount = foodAmount;
            this.healthGiven = healthGiven;
            this.foodQuality = foodQuality;

            this.name = name;
            this.weight = weight;
        }
    }

    public class ConsumablePotion : Item
    {
        public int liquidAmount { get; set; }
        public int healthGiven { get; set; } // How much health the potion will give the consumer
        public int potionQuality { get; set; } // How quality the food is; can determine possible sicknesses (if implemented)

        public ConsumablePotion(int liquidAmount, int healthGiven, int potionQuality, string name, int weight) : base(name, weight)
        {
            this.liquidAmount = liquidAmount;
            this.healthGiven = healthGiven;
            this.potionQuality = potionQuality;
            
            this.name = name;
            this.weight = weight;
        }
    }
}