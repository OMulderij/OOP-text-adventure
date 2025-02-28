class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
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
                // So instead this creates a new object with the same properties as the item, but with the amount value reset to 1.
                Item backpackItem = new Item(item.Description, item.Weight);
                items.Add(itemName, backpackItem);
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

    public bool ItemInInventory(string itemName) {
        if (items.ContainsKey(itemName)) {
            return true;
        }
        return false;
    }

    public string Show() {
        string str = "";
        int count = 0;
        if (TotalWeight() != 0) {
            // Loops through the items dictionary, then adds all items, their amounts, and weight per item to a string.
            foreach(KeyValuePair<string, Item> entry in items)
			{
                count++;
                str += $"-{entry.Value.Amount}x {entry.Key}, "; 

                if (entry.Value.Amount > 1) {
                    str += "each ";
                }
                
                str += $"with a weight of {entry.Value.Weight} kgs.";

                if (count != items.Count) {
                    str += "\n";
                }
			}
        }
        return str;
    }
}