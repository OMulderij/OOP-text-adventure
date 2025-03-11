class GrenadeItem : PlayerItem
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 0) {
        damage = newDamage;
    }

    public override string Use(Object o, string itemName)
    {

        // player.CurrentRoom.enemies.Damage(damage);
        return "HUUUGE DAMAGE!!!!";
    }
}