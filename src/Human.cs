using System;

class Human
{
    protected int health;
    protected Inventory backpack;

    public Human()
    {
        health = 100;
    }

    public Inventory Backpack {
        get {
            return backpack;
        }
    }

    public int Health {
        get {
            return this.health;
        }
    }

    public void Damage(int amount) {
        health -= amount;
    }

    public void Heal(int amount) {
        health += amount;
    }

    public bool IsAlive() {
        if (health <= 0) {
            return false;
        }
        return true;
    }

}

class Player : Human
{
    private Room currentRoom;
    private PlayerInventory playerItems;
    private Enemy targetEnemy;

    public Player() : base() {
        backpack = new Inventory(25);
        playerItems = new PlayerInventory();
    }
    
    public Enemy TargetEnemy {
        get {
            return targetEnemy;
        }
        set {
            targetEnemy = value;
        }
    }

    public Room CurrentRoom {
        get {
            return this.currentRoom;
        }
        set {
            this.currentRoom = value;
        }
    }

    public PlayerInventory PlayerItems
    {
        get {
            return playerItems;
        }
    }

    public virtual string UseItem(string itemName) {
        if (backpack.ItemInInventory(itemName)) {
            if (backpack.GetItemByString(itemName).GetType() == typeof(Weapon)) {
                return Attack((Weapon)backpack.GetItemByString(itemName));
            }
            return backpack.GetItemByString(itemName).Use(this, itemName);
        }
        
        if (playerItems.ItemInInventory(itemName)) {
            return playerItems.GetItemByString(itemName).Use(this, itemName);
        }
        return $"This {itemName} is not in your inventory.";
    }


    // Remove item from room, then put it in the backpack, or back in the room if it can't be put in the backpack.
    public bool TakeFromChest(string itemName) {
        Item chestItem = currentRoom.Chest.Get(itemName);
        if (backpack.Put(itemName, chestItem)) {
            Console.WriteLine($"Succesfully stored the {itemName} in your backpack.");
            return true;
        }
        
        currentRoom.Chest.Put(itemName, chestItem);
        if (chestItem.Weight > backpack.MaxWeight) {
            Console.WriteLine($"This {itemName} is too heavy to store in your backpack.");
        } else {
            Console.WriteLine($"You don't have enough storage space left to pick this {itemName} up.");
        }
        return false;
    }

    public bool DropToChest(string itemName)
    {
        // Removes item from backpack and tries to put it in the room, or back in the backpack if the player isn't allowed to drop the item.
        Item backpackItem = backpack.Get(itemName);
        if (currentRoom.Chest.Put(itemName, backpackItem)) {
            Console.WriteLine($"You succeeded in dropping the {itemName}.");
            return true;
        }

        backpack.Put(itemName, backpackItem);
        Console.WriteLine($"You are not allowed to drop this {itemName} here.");
        return false;
    }

    private string Attack(Weapon weapon) {
        if (targetEnemy == null) {
            return "There is no selected enemy.";
        }

        bool hasAdvantage = targetEnemy.ArmorType == weapon.Advantage;
        targetEnemy.Damage(weapon.Shoot(hasAdvantage));
        return $"You dealt {weapon.Shoot(hasAdvantage)} damage.";
    }

}

class Enemy : Human
{
    private Weapon weapon; // <--- weapon : Item class
    private string armorType;
    public Enemy(int newHP) {
        backpack = new Inventory(25);
        string weaponName = "base";
        this.health = newHP;

        Random random = new Random();
        // Generate which weapon the enemy will use to attack, and eventually drop
        switch (random.Next(3)) {
            case 0:
                weapon = new Weapon("light");
                weaponName = "SubmachineGun";
                break;
            case 1:
                weapon = new Weapon("medium");
                weaponName = "AssaultRifle";
                break;
            case 2:
                weapon = new Weapon("heavy");
                weaponName = "ShotGun";
                break;
        }

        // Generate armortype to have advantage against
        switch (random.Next(3)) {
            case 0:
                armorType = "light";
                break;
            case 1:
                armorType = "medium";
                break;
            case 2:
                armorType = "heavy";
                break;
        }
        backpack.Put(weaponName, weapon);
        Console.WriteLine(weapon.Description);
        Console.WriteLine(armorType);
    }

    public string ArmorType {
        get {
            return armorType;
        }
    }

    public void Attack(Player player) {
        if (player.TargetEnemy == this) {
            player.Damage(weapon.Shoot(false));
            return;
        }
        player.Damage((int)Math.Round(weapon.Shoot(false)*0.25));
    }
}