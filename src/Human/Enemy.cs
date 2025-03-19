class Enemy : Human
{
    public string WeaponName {get; private set;}
    public string ArmorType {get; private set;}
    public Enemy(int newHP) {
        this.backpack = new Inventory(25);
        this.health = newHP;
        Weapon weapon;

        Random random = new Random();
        // Generate which weapon the enemy will use to attack, and eventually drop.
        switch (random.Next(3)) {
            case 0:
                weapon = new Weapon("light", 40);
                this.WeaponName = "HandGun";
                break;
            case 1:
                weapon = new Weapon("medium", 40);
                this.WeaponName = "Rifle";
                break;
            // Default option is called when random.Next() is 2;
            default:
                weapon = new Weapon("heavy", 40);
                this.WeaponName = "ShotGun";
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
        // Console.WriteLine(weapon.Description);
        // Console.WriteLine(armorType);
    }
    
    public Weapon Weapon {
        get {
            return (Weapon)backpack.GetItemByString(WeaponName);
        }
    }

    public void Attack(Player player) {
        // Attack the player using the weapon inside of it's inventory
        Weapon weapon = (Weapon)backpack.GetItemByString(WeaponName);
        if (player.TargetEnemy == this) {
            player.Damage(weapon.Shoot(false));
            return;
        }
        player.Damage((int)Math.Round(weapon.Shoot(false)*0.25));
    }

    public Inventory Drop() {
        return backpack;
    }
}