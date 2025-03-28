class Enemy : Human
{
    public string WeaponName {get; private set;}
    public string ArmorType {get; private set;}
    public Enemy(int newMaxHP) : base(newMaxHP, 75) {
        this.backpack = new Inventory(25, 0);
        Weapon weapon;

        Random random = new Random();
        // Generate which weapon the enemy will use to attack, and eventually drop.
        switch (random.Next(3)) {
            case 0:
                weapon = new Weapon("light", 40);
                this.WeaponName = "handgun";
                break;
            case 1:
                weapon = new Weapon("medium", 40);
                this.WeaponName = "rifle";
                break;
            // Default option is called when random.Next() is 2;
            default:
                weapon = new Weapon("heavy", 40);
                this.WeaponName = "shotgun";
                break;
        }

        backpack.Put(WeaponName, weapon);

        // Generate armortype to have advantage against
        switch (random.Next(3)) {
            case 0:
                this.ArmorType = "light";
                break;
            case 1:
                this.ArmorType = "medium";
                break;
            case 2:
                this.ArmorType = "heavy";
                break;
        }
    }
    
    public Weapon Weapon {
        get {
            return (Weapon)backpack.GetItemByString(WeaponName);
        }
    }

    public int Attack(Player player) {
        // Attack the player using the weapon inside of it's inventory
        Weapon weapon = (Weapon)backpack.GetItemByString(WeaponName);
        int damageCalc = weapon.Shoot(false) * (int)Math.Round((double)this.BaseDamage / 100);
        
        if (player.TargetEnemy != this) {
            damageCalc = (int)Math.Round(damageCalc * 0.25);
        }

        player.Damage(damageCalc);
        return damageCalc;
    }

    public Inventory Drop() {
        return backpack;
    }
}