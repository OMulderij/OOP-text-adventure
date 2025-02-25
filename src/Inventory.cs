class Inventory
{
    private int maxWeight;
    
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }
    
    public bool Put(string itemName, Item item)
    {
        int totalInvWeight = 0;
        foreach(KeyValuePair<string, Item> entry in items)
        {
            totalInvWeight += entry.Value.Weight;
        }

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
}