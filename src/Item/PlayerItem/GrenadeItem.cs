class GrenadeItem : PlayerItem
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 0) {
        damage = newDamage;
    }

    public override string Use(Object o, string itemName)
    {
        if (o.GetType() != typeof(Player)) {
            return $"You can't use this {itemName} on this {o.GetType()}";
        }

        Player player = (Player)o;
        int enemiesHit = player.CurrentRoom.DamageAllEnemies(this.damage);
        this.Amount--;

        return $"You dealt {damage} damage to {enemiesHit} enemies!";
    }
}