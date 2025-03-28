class RipperDoc : Merchant
{
    private bool firstVisit;
    private Player player;
    public RipperDoc(Player newPlayer) : base("ripper") {
        // Initialize the fields
        player = newPlayer;
        firstVisit = true;
        stock = new Inventory(500, 0);

        Cyberware carryWeightUpgrade = new Cyberware("carryweight", 15, 50);
        Cyberware hpUpgrade = new Cyberware("maxHP", 50, 150);
        Cyberware armorUpgrade = new Cyberware("armor", 25, 250);
        Cyberware damageUpgrade = new Cyberware("damage", 20, 250);

        stock.Put(carryWeightUpgrade.EffectType, carryWeightUpgrade);
        stock.Put(hpUpgrade.EffectType, hpUpgrade);
        stock.Put(armorUpgrade.EffectType, armorUpgrade);
        stock.Put(damageUpgrade.EffectType, damageUpgrade);
    }

    public override string Talk(Player player)
    {
        string str = "";

        if (firstVisit) {
            str += "I haven't seen you around here before...\n";
            str += "You want to start chroming up, you say?\n";
            str += "Then you've come to the right place.\n";
            str += "Here's the chrome I can get for ya:\n";
        } else {
            str += "What kind of chrome can I get for ya this time?\n";
        }
        str += stock.RipperShow();
        
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

        return item;
    }
}