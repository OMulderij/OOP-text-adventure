using System;
class Item
{
    protected int weight;
    protected string description;
    private int amount;

    public Item(string newDescription, int newWeight) {
        this.description = newDescription;
        this.weight = newWeight;
        this.amount = 1;
    }

    public string Description {
        get {
            if (this.description == "none") {
                return "This item does not any effects.";
            }
            return this.description;
        }
    }

    public int Weight {
        get {
            return weight;
        }
    }

    public int Amount {
        get {
            return amount;
        }
        set {
            this.amount = value;
        }
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public virtual string Use(Player player, string itemName) {
        player.Backpack.Get(itemName);
        return this.description;
    }
}

class Tree : Item 
{
    public Tree() : base("You feel refreshed.", 1) {}
}

class HealItem : Item
{
    private int amount;
    public HealItem(int newHealAmount) : base("Using this item will heal you, did you expect anything else?", 1) {
        amount = newHealAmount;
    }

    public override string Use(Player player, string itemName) {
        string result = $"You have been healed for {this.amount} points.";
        player.Heal(this.amount);
        return result;
    }
}

class GrenadeItem : Item
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 1) {
        damage = newDamage;
    }

    public override string Use(Player player, string itemName)
    {

        // player.CurrentRoom.enemies.Damage();
        return "HUUUGE DAMAGE!!!!";
    }
}

class Cat : Item
{
    public Cat() : base("Its a very cute cat", 5) {}

    public override string Use(Player player, string itemName) {
        this.description = "Meow";

        return this.description;
    }
}

// Healing item, damaging item