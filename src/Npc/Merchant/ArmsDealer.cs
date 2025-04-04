class ArmsDealer : Merchant
{
    private Dictionary<string, Item> guaranteedDict;
    private bool firstVisit;
    private Player player;

    public ArmsDealer(Player newPlayer) : base("dealer")
    {
        // Initialize the fields
        player = newPlayer;
        firstVisit = true;
        stock = new Inventory(500);
        guaranteedDict = new Dictionary<string, Item>();

        // Initialize items to add to the lists.
        PlayerItem healer = new HealItem(50);
        PlayerItem grenade = new GrenadeItem(500);

        guaranteedDict.Add("healer", healer);
        guaranteedDict.Add("grenade", grenade);

        RandomizeStock();
    }

    public override string Talk()
    {
        string str = "";
        if (!stock.IsEmpty()) {
            str += "I've got the goods, you've got the eddies. You understand what I'm getting at?\n";
            str += "Anyway, here's what I'm selling:\n";
            str += stock.DealerShow() + "\n \n";
            
            if (firstVisit) {
                str += "And I believe I've got something else you might be interested in.\n";
                str += "I've managed to get a mechanic working for my business you see.\n";
                str += "And now the lad gave me a list of items he can work on.\n";
                str += "Heres the list:\n";
                firstVisit = false;
            } else {
                str += "I remember you from the last time, here's the list:\n";
            }
            str += stock.MerchantPlayerItemsShow();
        } else {
            str += "There's nothing more I have to offer.";
        }
        return str;
    }

    public void RandomizeStock() {
        stock = new Inventory(500);
        Random random = new Random();
        Weapon weapon;
        string weaponName;
        int floorLevel = (int)Math.Round((double)player.HighestFloor / 2);

        foreach(KeyValuePair<string, Item> entry in guaranteedDict) {
            stock.Put(entry.Key, entry.Value);
        }

        switch (random.Next(3)) {
            case 0:
                weapon = new Weapon("light", 40 * floorLevel);
                weaponName = "handgun";
               break;
            case 1:
                weapon = new Weapon("medium", 40 * floorLevel);
                weaponName = "rifle";
                break;
            default:
                weapon = new Weapon("heavy", 40 * floorLevel);
                weaponName = "shotgun";
                break;
        }
        for (int i = weapon.Level; i < floorLevel; i++) {
            weapon.UpgradeWeapon();
        }
        stock.Put(weaponName, weapon);
    }

    public override Item Buy(int playerMoney, string itemName) {
        Item item = stock.Get(itemName);

        if (item != null) {
            if (item is PlayerItem) {
                PlayerItem newItem = (PlayerItem)player.Backpack.GetItemByString(itemName);
                if (playerMoney >= newItem.Value && !newItem.IsItemUpgraded()) {
                    return item;
                } else if (newItem.IsItemUpgraded()) {
                    stock.Put(itemName, item);
                    Console.WriteLine($"The mechanic said that the technology isn't quite ready yet for another upgrade to your {itemName}.");
                    return null;
                }
            }

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