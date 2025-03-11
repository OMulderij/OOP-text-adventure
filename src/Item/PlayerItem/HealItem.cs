class HealItem : PlayerItem
{
    private int amount;

    public HealItem(int newHealAmount) : base("Using this item will heal you, did you expect anything else?", 1) {
        amount = newHealAmount;
    }

    public override string Use(Player player, string itemName) {
        if (!UseItem()) {
            return $"You don't have enough left to use.";
        }
        string result = $"You have been healed for {this.amount} points.";
        player.Heal(this.amount);
        return result;
    }
}