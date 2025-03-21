class Merchant : Npc
{
    private Inventory stock;
    private Dictionary<string, Item> guaranteedDict;
    private bool firstVisit;

    public Merchant() : base("merchant")
    {
        // Initialize the fields
        firstVisit = true;
        stock = new Inventory(500, 0);
        guaranteedDict = new Dictionary<string, Item>();

        // Initialize items to add to the lists.
        PlayerItem healer = new HealItem(50);
        PlayerItem grenade = new GrenadeItem(500);

        guaranteedDict.Add("healer", healer);
        guaranteedDict.Add("grenade", grenade);

        RandomizeStock();
    }

    public override string Talk(Player player)
    {
        string str = "I've got the goods, you've got the eddies. You understand what I'm getting at?\n";
        str += "Anyway, here's what I'm selling:\n";
        str += stock.Show() + "\n \n";
        
        if (firstVisit) {
            str += "And I believe I've got something else you might be interested in.\n";
            str += "I've managed to get a mechanic working for my business you see.\n";
            str += "And now the lad gave me a list of items he can work on.\n";
            str += "Heres the list:\n";
            str += stock.SimplePlayerItemsShow();
            firstVisit = false;
        } else {
            str += "I remember you from the last time, here's the list:\n";
            str += stock.SimplePlayerItemsShow();
        }
        return str;
    }

    public void RandomizeStock() {
        stock = new Inventory(500, 0);
        Random random = new Random();

        foreach(KeyValuePair<string, Item> entry in guaranteedDict) {
            stock.Put(entry.Key, entry.Value);
        }

        switch (random.Next(3)) {
            case 0:
                Weapon handgun = new Weapon("light", 40);
                stock.Put("handgun", handgun);
               break;
            case 1:
                Weapon rifle = new Weapon("medium", 40);
                stock.Put("rifle", rifle);
                break;
            default:
                Weapon shotgun = new Weapon("heavy", 40);
                stock.Put("shotgun", shotgun);
                break;
        }
    }

    public Item Buy(int playerMoney, string itemName) {
        Item item = stock.Get(itemName);

        if (item != null) {
            if (playerMoney < item.Value) {
                stock.Put(itemName, item);
                Console.WriteLine("I'm afraid that you don't quite have enough eddies for that.");
                return null;
            }
        } else {
            Console.WriteLine($"I've got no clue what {itemName} is.");
			Console.WriteLine("I do know that I don't sell it though.");
        }
        return item;
    }
}