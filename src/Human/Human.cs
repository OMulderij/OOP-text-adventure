using System;

class Human
{
    private int health;
    private int maxHP;
    protected Inventory backpack;

    public Human(int newMaxHP)
    {
        maxHP = newMaxHP;
        health = maxHP;
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
        
        if (health > maxHP) {
            health = maxHP;
        }
    }

    // Checks if the Object is alive
    public bool IsAlive() {
        if (health <= 0) {
            return false;
        }
        return true;
    }
}