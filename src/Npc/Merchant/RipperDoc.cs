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

        str += "I haven't seen you around here before..";
        str += "You want to start chroming up, you say?";
        str += "Then you've come to the right place.";
        str += "Here's the chrome I can get for ya:";
        str += stock.MerchantShow() + "\n \n";
        
        return str;
    }

    public override Cyberware Buy(int playerMoney, string itemName) {
        Item item = stock.Get(itemName);


        if (item != null) {

        }
        return null;
    }
}