using System;

class Player
{
    private Room currentRoom;
    private int health;
    private Inventory backpack;

    public Player()
    {
        health = 100;
        backpack = new Inventory(25);
    }

    public Room CurrentRoom {
        get {
            return this.currentRoom;
        }
        set {
            this.currentRoom = value;
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
        Item backpackItem = backpack.Get(itemName);
        if (currentRoom.Chest.Put(itemName, backpackItem)) {
            Console.WriteLine($"You succeeded in dropping the {itemName}.");
            return true;
        }

        backpack.Put(itemName, backpackItem);
        Console.WriteLine($"You are not allowed to drop this {itemName} here.");
        return false;
    }
}