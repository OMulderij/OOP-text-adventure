class HealItem : PlayerItem
{
    private int amount;

    public HealItem(int newHealAmount) : base("Using this item will heal you, did you expect anything else?", 0) {
        this.amount = newHealAmount;
    }

    // Attempts to use the Healer on the player.
    public override string Use(Object o) {
        if (o.GetType() != typeof(Player)) {
            return $"You can't use this item on a {o.GetType()}.";
        }

        if (!UseItem()) {
            return $"You don't have enough left to use.";
        }

        Player player = (Player) o;
        string result = $"You have been healed for {this.amount} points.";
        player.Heal(this.amount);
        return result;
    }
}