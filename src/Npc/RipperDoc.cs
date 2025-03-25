class RipperDoc : Merchant
{
    public RipperDoc() : base("ripper") {}

    public override string Talk(Player player)
    {
        string str = "";

        str += "Yo.";
        
        return str;
    }

    public override Cyberware Buy(int playerMoney, string itemName) {
        return null;
    }
}