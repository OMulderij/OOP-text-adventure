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

class PlayerItem : Item
{
    private int maxCount = 2;
    private int usesLeft = 2;
    public PlayerItem(string newDescription, int newWeight) : base(newDescription, newWeight) {}

    public int UsesLeft {
        get {
            return usesLeft;
        }
    }
    public void UpgradeItem() {
        maxCount = 3;
        usesLeft = 3;
    }

    public bool IsItemUpgraded() {
        if (maxCount == 3) {
            return true;
        }
        return false;
    }

    protected bool UseItem() {
        if (usesLeft > 0) {
            usesLeft--;
            return true;
        }
        return false;
    }

    public void AddItemUse() {
        if (usesLeft < maxCount) {
            usesLeft++;
        }
    }
}

class HealItem : PlayerItem
{
    private int amount;

    public HealItem(int newHealAmount) : base("Using this item will heal you, did you expect anything else?", 1) {
        amount = newHealAmount;
    }

    public override string Use(Player player, string itemName) {
        if (!UseItem()) {
            return $"You don't have enough left to use.";
        }
        string result = $"You have been healed for {this.amount} points.";
        player.Heal(this.amount);
        return result;
    }
}

class GrenadeItem : PlayerItem
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 1) {
        damage = newDamage;
    }

    public override string Use(Player player, string itemName)
    {

        // player.CurrentRoom.enemies.Damage(damage);
        return "HUUUGE DAMAGE!!!!";
    }
}

class Weapon : Item
{
    private int bulletDamage;
    private string advantage;
    private double level;
    public Weapon(string newAdvantage) : base("Deals more damage to ", 5) {
        this.advantage = newAdvantage;
        this.description += advantage + " armor.";
        this.advantage = newAdvantage;
        bulletDamage = 20;
        level = 1;
    }

    public int Shoot() {
        return (int)(bulletDamage * Math.Round(Math.Pow(1.25, level)));
    }

    public string Advantage {
        get {
            return advantage;
        }
    }

    public bool UpgradeWeapon() {
        if (level < 5) {
            level++;
            return true;
        }
        return false;
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

class Tree : Item 
{
    public Tree() : base("You feel refreshed.", 1) {}

    public new string Use(Player player, string itemName) {
        player.Backpack.Get(itemName);
        return this.description;
    }
}