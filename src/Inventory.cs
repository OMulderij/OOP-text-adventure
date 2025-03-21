class Inventory
{
    private int maxWeight;
    protected Dictionary<string, Item> items;

    public Inventory(int newMaxWeight, int eddieCount)
    {
        this.maxWeight = newMaxWeight;
        this.items = new Dictionary<string, Item>();

        Eddies eddies = new Eddies();
        eddies.AddValue(eddieCount);
    
        // if (eddieCount > 0) {
            items.Add("eddies", eddies);
        // }
    }

    public int MaxWeight {
        get {
            return maxWeight;
        }
    }

    public Dictionary<string, Item> Items {
        get {
            return items;
        }
    }
    
    public bool Put(string itemName, Item item)
    {
        // Increases the amount of items if it exists in the dictionary, otherwise add the item to the dictionary.
        if (item.Weight + TotalWeight() <= maxWeight) {
            if (items.ContainsKey(itemName)) {
                if (items[itemName].GetType() == typeof(Eddies)) {
                    Eddies eddies = (Eddies)items[itemName];
                    eddies.AddValue(item.Value);
                } else {
                    items[itemName].Amount++;
                }
            } else {
                // Adding the item directly also copies the value of the amount field, which we don't want in cases where the player or room has more than 2 of a certain item.
                // ---- So instead this creates a new object with the same properties as the item, but with the amount value reset to 1. ----
                // Now clones the Item instead, to preserve the class extensions.
                Item itemClone = (Item)item.Clone();

                itemClone.Amount = 1;
                items.Add(itemName, itemClone);
            }
            return true;
        }
        return false;
    }
    public Item Get(string itemName)
    {
        // Decrease the amount of items if it exists in the dictionary, otherwise remove the item from the dictionary.
        if (items.ContainsKey(itemName)) {
            Item takenItem = items[itemName];
            if (takenItem.Amount > 1) {
                takenItem.Amount--;
                return takenItem;
            }
            items.Remove(itemName);
            return takenItem;
        }
        return null;
    }

    // Returns the Item by string. (So you don't have to put the item back every time.)
    public Item GetItemByString(string itemName) {
        return items[itemName];
    }

    // Calculates the total amount of weight of all the items in the inventory combined.
    public int TotalWeight()
    {
        int total = 0;
        foreach(KeyValuePair<string, Item> entry in items)
        {
            total += entry.Value.Weight * entry.Value.Amount;
        }
        return total;
    }

    // Calculates the amount of weight left in the inventory.
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    // Checks if said item is in the inventory.
    public bool ItemInInventory(string itemName) {
        if (items.ContainsKey(itemName)) {
            return true;
        }
        return false;
    }

    // Prints a list of all the items in the inventory
    // Need to rework this function to it also functions properly with the Rooms.
    public string Show() {
        string str = "";
        // Loops through the items dictionary, then adds all items, their amounts, and weight per item to a string.
        foreach(KeyValuePair<string, Item> entry in items)
        {
            if (entry.Value is PlayerItem) {
                continue;
            }

            if (entry.Value.GetType() == typeof(Eddies)) {
                str += $"-{entry.Value.Value} Eddies.";
                continue;
            }

            str += $"-{entry.Value.Amount}x {entry.Key}, "; 

            if (entry.Value.Amount > 1) {
                str += "each ";
            }
            
            str += $"with a weight of {entry.Value.Weight} kgs.\n";
        }
        return str;
    }

    public string ShowPlayerItems() {
        string str = "";
        foreach(KeyValuePair<string, Item> entry in items) {
            if (entry.Value is PlayerItem) {
                PlayerItem tempItem = (PlayerItem)entry.Value;
                str += $"-{tempItem.UsesLeft}x uses left on a {entry.Key}\n";
                continue;
            }
        }
        return str;
    }

    public string SimplePlayerItemsShow() {
        string str = "";
        foreach(KeyValuePair<string, Item> entry in items) {
            if (entry.Value is PlayerItem) {
                PlayerItem tempItem = (PlayerItem)entry.Value;
                str += $"-{entry.Key}\n";
                continue;
            }
        }
        return str;
    }

    // Checks if there are any items at all in the Inventory
    public bool EmptyRoom() {
        return FreeWeight() == maxWeight;
    }

    // Add a charge of a PlayerItem (grenades and healers)
    public void AddCharge(string itemName) {
        Item item = GetItemByString(itemName);
        if (item is PlayerItem) {
            PlayerItem newItem = (PlayerItem)item;
            newItem.AddItemUse();
        }
    }

    // Check if the PlayerItem is upgraded, if not, upgrade it.
    public bool Upgrade(string itemName) {
        if (GetItemByString(itemName).GetType() != typeof(PlayerItem)) {
            return false;
        }

        PlayerItem newItem = (PlayerItem)GetItemByString(itemName); 
        if (!newItem.IsItemUpgraded()) {
            newItem.UpgradeItem();
            return true;
        }
        return false;
    }

    public void ClearInventory() {
        items = new Dictionary<string, Item>();
    }
}