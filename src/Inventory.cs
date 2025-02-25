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

    public Dictionary<string, Item> Items {
        get {
            return items;
        }
    }
    
    public bool Put(string itemName, Item item)
    {
        int totalInvWeight = TotalWeight();

        if (item.Weight + totalInvWeight <= maxWeight) {
            items.Add(itemName, item);
            return true;
        }
        return false;
    }
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName)) {
            Item gottenItem = items[itemName];
            items.Remove(itemName);
            return gottenItem;
        }
        return null;
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach(KeyValuePair<string, Item> entry in items)
        {
            total += entry.Value.Weight;
        }
        return total;
    }
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }
}