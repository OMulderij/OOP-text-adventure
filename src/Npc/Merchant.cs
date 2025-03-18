class Merchant : Npc
{
    private Inventory stock;
    private Dictionary<string, Item> guaranteedDict;
    public Merchant() : base("merchant") 
    {
        // Initialize the fields
        stock = new Inventory(500);
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
        str += "Anyway, here's what I'm selling:";
        
        return str;
    }

    public void RandomizeStock() {
         stock = new Inventory(500);
         Random random = new Random();

         switch (random.Next(3)) {
            case 0:
                Weapon handgun = new Weapon("light");
                stock.Put("handgun", handgun);
                break;
            case 1:
                Weapon rifle = new Weapon("medium");
                stock.Put("rifle", rifle);
                break;
            default:
                Weapon shotgun = new Weapon("heavy");
                stock.Put("shotgun", shotgun);
                break;
         }
    }
}