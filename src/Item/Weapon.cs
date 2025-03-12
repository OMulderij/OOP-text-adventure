class Weapon : Item
{
    private int bulletDamage;
    private string advantage;
    private double level;
    public Weapon(string newAdvantage) : base("Deals more damage to ", 5) {
        this.advantage = newAdvantage;
        this.description += advantage + " armor.";
        this.advantage = newAdvantage;
        bulletDamage = 20;
        level = 1;
    }

    public int Shoot(bool advantage) {
        int damageDealt = (int)(bulletDamage * Math.Round(Math.Pow(1.25, level)));
        if (advantage == false) {
            return damageDealt;
        }
        return (int)Math.Round(damageDealt*1.5);
    }

    public string Advantage {
        get {
            return advantage;
        }
    }

    public bool UpgradeWeapon() {
        if (level < 5) {
            level++;
            return true;
        }
        return false;
    }

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