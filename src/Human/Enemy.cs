class Enemy : Human
{
    private Weapon weapon; // <--- weapon : Item class
    private string armorType;
    public Enemy(int newHP) {
        backpack = new Inventory(25);
        string weaponName = "base";
        this.health = newHP;

        Random random = new Random();
        // Generate which weapon the enemy will use to attack, and eventually drop
        switch (random.Next(3)) {
            case 0:
                weapon = new Weapon("light");
                weaponName = "SubmachineGun";
                break;
            case 1:
                weapon = new Weapon("medium");
                weaponName = "AssaultRifle";
                break;
            case 2:
                weapon = new Weapon("heavy");
                weaponName = "ShotGun";
                break;
        }

        // Generate armortype to have advantage against
        switch (random.Next(3)) {
            case 0:
                armorType = "light";
                break;
            case 1:
                armorType = "medium";
                break;
            case 2:
                armorType = "heavy";
                break;
        }
        backpack.Put(weaponName, weapon);
        Console.WriteLine(weapon.Description);
        Console.WriteLine(armorType);
    }

    public string ArmorType {
        get {
            return armorType;
        }
    }

    public void Attack(Player player) {
        if (player.TargetEnemy == this) {
            player.Damage(weapon.Shoot(false));
            return;
        }
        player.Damage((int)Math.Round(weapon.Shoot(false)*0.25));
    }
}