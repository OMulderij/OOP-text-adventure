class Inventory
{
    private int maxWeight;
    protected Dictionary<string, Item> items;

    public Inventory(int newMaxWeight)
    {
        this.maxWeight = newMaxWeight;
        this.items = new Dictionary<string, Item>();

        HealItem healer = new HealItem(50);
        GrenadeItem grenade = new GrenadeItem(500);
        this.items.Add("healer", healer);
        this.items.Add("grenade", grenade);
    }

    public int MaxWeight {
        get {
            return maxWeight;
        }
    }
    
    public bool Put(string itemName, Item item)
    {
        // Increases the amount of items if it exists in the dictionary, otherwise add the item to the dictionary.
        if (item.Weight + TotalWeight() <= maxWeight) {
            if (items.ContainsKey(itemName)) {
                items[itemName].Amount++;
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

    public virtual Item GetItemByString(string itemName) {
        return items[itemName];
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach(KeyValuePair<string, Item> entry in items)
        {
            total += entry.Value.Weight * entry.Value.Amount;
        }
        return total;
    }
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public virtual bool ItemInInventory(string itemName) {
        if (items.ContainsKey(itemName)) {
            return true;
        }
        return false;
    }

    public string Show() {
        string str = "You have these items in your pocket:\n";
        string basicStr = "";
        int count = 0;

        // Loops through the items dictionary, then adds all items, their amounts, and weight per item to a string.
        foreach(KeyValuePair<string, Item> entry in items)
        {

            if (entry.Value is PlayerItem) {
                PlayerItem tempItem = (PlayerItem)entry.Value;
                str += $"-{tempItem.UsesLeft}x uses left on a {entry.Key}\n";
                continue;
            }
            count++;
            basicStr += $"-{entry.Value.Amount}x {entry.Key}, "; 

            if (entry.Value.Amount > 1) {
                basicStr += "each ";
            }
            
            basicStr += $"with a weight of {entry.Value.Weight} kgs.\n";
        }
        if (basicStr != "") {
            str += $"\nYou have stored these items in your backpack:\n{basicStr}";
        } else {
            str += "\nYour backpack is empty.";
        }
        return str;
    }

    public virtual bool EmptyRoom() {
        return FreeWeight() == maxWeight;
    }

    public void AddCharge(string itemName) {
        Item item = GetItemByString(itemName);
        if (item is PlayerItem) {
            PlayerItem newItem = (PlayerItem)item;
            newItem.AddItemUse();
        }
    }

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
}