class GrenadeItem : PlayerItem
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 1) {
        damage = newDamage;
    }

    public override string Use(Player player, string itemName)
    {

        // player.CurrentRoom.enemies.Damage(damage);
        return "HUUUGE DAMAGE!!!!";
    }
}