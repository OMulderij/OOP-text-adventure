class Player : Human
{
    private Room currentRoom;
    public Enemy TargetEnemy {get; set;}
    public bool ActiveQuest {get; set;}
    public int HighestFloor {get; set;}
    public bool CompletedQuest {get; set;}
    public Npc lastTalkedToNpc {get; set;}


    public Player() : base(100) {
        backpack = new Inventory(25, 999);

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
            // Checks if the player selected an enemy, and if so, attacks the enemy using the current weapon.
            if (item.GetType() == typeof(Weapon)) {
                if (command.ThirdWord == null) {
                    return $"Use {command.SecondWord} on *who*?";
                }

                if (int.TryParse(command.ThirdWord, out int result)) {
                    if (result <= this.currentRoom.Enemies.Count - 1 && this.currentRoom.Enemies.Count > 0) {
                        basicObject = this.currentRoom.Enemies[result];
                        TargetEnemy = (Enemy)basicObject;
                    } else {
                        return result + " is not a valid enemy.";
                    }
                }
                else {
                    return "Please select an enemy by using the number in front of their name.";
                }
            }

            // Calls the Use() method on the chosen item.
            string str = item.Use(basicObject);

            if (item.GetType() == typeof(Weapon) || item.GetType() == typeof(GrenadeItem)) {
                currentRoom.EnemyTurn(this);
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