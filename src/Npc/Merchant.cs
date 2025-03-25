abstract class Merchant : Npc 
{
    public Merchant(string newName) : base(newName) {}

    public abstract Object Buy(int playerMoney, string itemName);
}