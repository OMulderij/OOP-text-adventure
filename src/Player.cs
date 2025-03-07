using System;

class Player
{
    private Room currentRoom;
    protected int health;
    protected Inventory backpack;
    private PlayerInventory playerItems;
    private Enemy targetEnemy;

    public Player()
    {
        health = 100;
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

    public Inventory Backpack {
        get {
            return backpack;
        }
    }

    public PlayerInventory PlayerItems
    {
        get {
            return playerItems;
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

    public virtual string UseItem(string itemName) {
        if (backpack.ItemInInventory(itemName)) {
            if (backpack.GetItemByString(itemName) is Weapon) {
                return Attack((Weapon)backpack.GetItemByString(itemName));
            }
        }
        if (backpack.ItemInInventory(itemName)) {
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

        targetEnemy.Damage(weapon.Shoot());
        return $"You dealt {weapon.Shoot()} damage.";
    }
}

class Enemy : Player
{
    private Weapon weapon; // <--- weapon : Item class
    public Enemy(int newHP) {
        string weaponName = "base";
        this.health = newHP;
        Random random = new Random();
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
        backpack.Put(weaponName, weapon);
        Console.WriteLine(weapon.Description);
    }

    public void Attack(Player player) {
        if (player.TargetEnemy == this) {
            player.Damage(weapon.Shoot());
            return;
        }
        player.Damage((int)Math.Round(weapon.Shoot()*0.25));
    }
}