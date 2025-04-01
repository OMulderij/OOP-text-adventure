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
        int damageDealt = (int)Math.Round(bulletDamage * Math.Pow(1.25, level));
        if (advantage == false) {
            return damageDealt;
        }
        return (int)Math.Round(damageDealt*1.5);
    }

    // Upgrades the weapon.
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
        Player player = (Player)o;
        if (player.TargetEnemy == null) {
            return "how? Did I mess my calculations up somewhere? -Dev";
        }

        Enemy enemy = player.TargetEnemy;

        int damageCalc = (int)Math.Round(Shoot(enemy.ArmorType == advantage) * (double)player.BaseDamage / 100);
        enemy.Damage(damageCalc);
        return $"You dealt {damageCalc} damage.";
    }
}