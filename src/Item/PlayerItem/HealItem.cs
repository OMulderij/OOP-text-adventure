class HealItem : PlayerItem
{
    private int amount;

    public HealItem(int newHealAmount) : base("Using this item will heal you, did you expect anything else?", 0) {
        amount = newHealAmount;
    }

    public override string Use(Object o, string itemName) {
        if (o.GetType() != typeof(Player)) {
            return $"You can't use this item on a non-{typeof(Player)}.";
        }

        Player player = (Player) o;
        
        if (!UseItem()) {
            return $"You don't have enough left to use.";
        }
        string result = $"You have been healed for {this.amount} points.";
        player.Heal(this.amount);
        return result;
    }
}