class Player : Human
{
    private Room currentRoom;
    private Enemy targetEnemy;

    public Player() : base() {
        backpack = new Inventory(25);
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

    public virtual string UseItem(Command command) {
        if (backpack.ItemInInventory(command.SecondWord)) {
            return backpack.GetItemByString(command.SecondWord).Use(this);
        }
        
        return $"This {command.SecondWord} is not in your inventory.";
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


}