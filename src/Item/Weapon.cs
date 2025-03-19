class Weapon : Item
{
    private int bulletDamage;
    private string advantage;
    private int level;
    public Weapon(string newAdvantage, int newValue) : base("Deals more damage to ", 5, newValue) {
        this.advantage = newAdvantage;
        this.description += advantage + " armor.";
        this.advantage = newAdvantage;
        bulletDamage = 20;
        level = 1;
    }

    public int Level {
        get {
            return level;
        }
    }

    // Calculates the damage dealt by the weapon
    public int Shoot(bool advantage) {
        int damageDealt = (int)(bulletDamage * Math.Round(Math.Pow(1.25, level)));
        if (advantage == false) {
            return damageDealt;
        }
        return (int)Math.Round(damageDealt*1.5);
    }

    // Upgrades the weapon,
    // Upgrading the weapon increases its damage dealt.
    public bool UpgradeWeapon() {
        if (level < 5) {
            level++;
            return true;
        }
        return false;
    }

    // Attempts to use the weapon to attack an Enemy object.
    public override string Use(Object o) {
        if (o.GetType() != typeof(Enemy)) {
            return "You can't attack this " + o.GetType();
        }
        Enemy targetEnemy = (Enemy)o;

        bool hasAdvantage = targetEnemy.ArmorType == advantage;
        targetEnemy.Damage(Shoot(hasAdvantage));
        return $"You dealt {Shoot(hasAdvantage)} damage.";
    }
}