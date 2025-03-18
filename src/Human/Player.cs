class Player : Human
{
    private Room currentRoom;
    public Enemy TargetEnemy {get; set;}
    public bool ActiveQuest {get; set;}

    public Player() : base() {
        backpack = new Inventory(25);

        // Generate the standard items in the player backpack.
        HealItem healer = new HealItem(50);
        GrenadeItem grenade = new GrenadeItem(500);
        this.backpack.Put("healer", healer);
        this.backpack.Put("grenade", grenade);
    }

    public Room CurrentRoom {
        get {
            return this.currentRoom;
        }
        set {
            this.currentRoom = value;
        }
    }

    public string UseItem(Command command) {
        Object basicObject = this;
        if (backpack.ItemInInventory(command.SecondWord)) {
            // Checks if the object is a different type of item, to make sure the object matches what the Use() method expects.
            Item item = backpack.GetItemByString(command.SecondWord);
            // if (item is PlayerItem) {
            //     basicObject = this;
            // }
            // Checks if the player selected an enemy, and if so, attacks the enemy using the current weapon.
            if (item.GetType() == typeof(Weapon)) {
                if (command.ThirdWord == null) {
                    return $"Use {command.SecondWord} on *who*?";
                }

                if (int.TryParse(command.ThirdWord, out int result)) {
                    if (this.currentRoom.Enemies.Count - 1 <= result && this.currentRoom.Enemies.Count > 0) {
                        basicObject = this.currentRoom.Enemies[result];
                    } else {
                        return result + " is not a valid enemy.";
                    }
                }
                else {
                    return "Please select an enemy by using the number in front of their name.";
                }
            }

            // Calls the Use() method on the chosen item.
            string str = this.backpack.GetItemByString(command.SecondWord).Use(basicObject);
            
            // Removes all dead enemies from the currentRoom.
            int count = this.currentRoom.Enemies.Count - 1;
            for (int i = count; i >= 0; i--) {
                if (!this.currentRoom.Enemies[i].IsAlive()) {
                    this.currentRoom.Enemies.RemoveAt(i);
                }
            }
            return str;
        }
        return $"This {command.SecondWord} is not in your inventory.";
    }


    // Remove item from room, then put it in the backpack, or back in the room if it can't be put in the backpack.
    public bool TakeFromChest(string itemName) {
        Item chestItem = currentRoom.Chest.Get(itemName);
        if (this.backpack.Put(itemName, chestItem)) {
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

    public string DropToChest(string itemName)
    {
        // Removes item from backpack and tries to put it in the room, or back in the backpack if the player isn't allowed to drop the item.
        Item backpackItem = backpack.Get(itemName);
        if (backpackItem is PlayerItem) {
            this.backpack.Put(itemName, backpackItem);
            return $"You can't drop the {itemName}.";
        }
        if (this.currentRoom.Chest.Put(itemName, backpackItem)) {
            return $"You succeeded in dropping the {itemName}.";
        }

        this.backpack.Put(itemName, backpackItem);
        return $"You are not allowed to drop this {itemName} here.";
    }
}