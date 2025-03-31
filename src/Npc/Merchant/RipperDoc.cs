class RipperDoc : Merchant
{
    private bool firstVisit;
    public RipperDoc() : base("ripper") {
        // Initialize the fields
        firstVisit = true;
        stock = new Inventory(500);

        Cyberware carryWeightUpgrade = new Cyberware("carryweight", 15, 50);
        Cyberware hpUpgrade = new Cyberware("maxHP", 50, 150);
        Cyberware armorUpgrade = new Cyberware("armor", 25, 250);
        Cyberware damageUpgrade = new Cyberware("damage", 20, 250);

        stock.Put(carryWeightUpgrade.EffectType, carryWeightUpgrade);
        stock.Put(hpUpgrade.EffectType, hpUpgrade);
        stock.Put(armorUpgrade.EffectType, armorUpgrade);
        stock.Put(armorUpgrade.EffectType, armorUpgrade);
        stock.Put(damageUpgrade.EffectType, damageUpgrade);
    }

    public override string Talk()
    {
        string str = "";

        if (!stock.IsEmpty()) {
            if (firstVisit) {
                str += "I haven't seen you around here before...\n";
                str += "You want to start chroming up, you say?\n";
                str += "Then you've come to the right place.\n";
                str += "Here's the chrome I can get for ya:\n";
                firstVisit = false;
            } else {
                str += "What kind of chrome can I get for ya this time?\n";
            }
            str += stock.RipperShow();
            
        } else {
            str += "You've bought everything I have to offer.";
        }
        return str;
    }

    public override Cyberware Buy(int playerMoney, string itemName) {
        // RipperDoc should not have anything in stock other than Cyberware.
        Cyberware item = (Cyberware)stock.Get(itemName);

        if (item != null) {
            if (playerMoney < item.Value) {
                stock.Put(itemName, item);
                Console.WriteLine("I understand that you might want something of a higher quality,\n");
                Console.WriteLine("But please come back once you've got enough eddies to back those feelings up.\n");
                return null;
            }
        } else {
            Console.WriteLine($"I'm not sure what kind of Cyberware you're asking for.\n");
            Console.WriteLine("I am certain that I do not sell or install it though.\n");
        }

        // Double the price of the item for subsequent purchases.
        if (stock.ItemInInventory(itemName)) {
            stock.GetItemByString(itemName).AddValue(stock.GetItemByString(itemName).Value);
        }
        return item;
    }
}