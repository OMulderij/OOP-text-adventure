abstract class Merchant : Npc 
{
    protected Inventory stock;

    public Merchant(string newName) : base(newName) {}

    public abstract Object Buy(int playerMoney, string itemName);
}