using System;

class Human
{
    protected int health;
    protected Inventory backpack;

    public Human()
    {
        health = 100;
    }

    public Inventory Backpack {
        get {
            return backpack;
        }
    }

    public int Health {
        get {
            return this.health;
        }
    }

    // Damages the Object.
    public void Damage(int amount) {
        health -= amount;
    }

    // Heals the Object
    public void Heal(int amount) {
        health += amount;
    }

    // Checks if the Object is alive
    public bool IsAlive() {
        if (health <= 0) {
            return false;
        }
        return true;
    }
}