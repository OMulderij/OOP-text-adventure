class RipperDoc : Merchant
{
    public RipperDoc() : base("ripper") {}

    public override string Talk(Player player)
    {
        string str = "";

        str += "What kind of cyberware would you like?";
        
        return str;
    }

    public override Cyberware Buy(int playerMoney, string itemName) {
        Item item = stock.Get(itemName);


        if (item != null) {

        }
        return null;
    }
}