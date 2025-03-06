class Enemy
{
    private int Hitpoints;
    private Weapon weapon; // <--- weapon : Item class
    public Enemy(int newHP) {
        Random random = new Random();
        this.Hitpoints = newHP;
        switch (random.Next(3)) {
            case 0:
                weapon = new SubmachineGun();
                break;
            case 1:
                weapon = new AssaultRifle();
                break;
            case 2:
                weapon = new ShotGun();
                break;
        }
        Console.WriteLine(weapon.Description);
    }
}