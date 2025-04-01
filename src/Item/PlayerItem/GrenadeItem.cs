class GrenadeItem : PlayerItem
{
    private int damage;
    public GrenadeItem(int newDamage) : base("Deals great damage to all enemies on a floor.", 0, 300) {
        this.damage = newDamage;
    }

    // Tries to use the grenade on the given object.
    // Deals damage to all enemies in the current room if theres enough charges left.
    public override string Use(Object o)
    {
        if (o.GetType() != typeof(Player)) {
            return $"You can't use this Grenade on this {o.GetType()}";
        }

        if (!UseItem()) {
            return $"You don't have enough left to use.";
        }

        Player player = (Player)o;
        int damageCalc = (int)Math.Round(this.damage * (double)player.BaseDamage / 100);
        int enemiesHit = player.CurrentRoom.DamageAllEnemies(damageCalc);

        return $"You dealt {damageCalc} damage to {enemiesHit} enemies!";
    }
}