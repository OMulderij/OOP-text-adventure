using System;
using System.Runtime.Intrinsics.X86;

class Human
{
    public int BaseDamage {get; private set;}
    protected Inventory backpack;
    private Dictionary<string, Cyberware> implants;
    private int health;
    private int maxHP;
    private int armor;

    public Human(int newMaxHP, int newBaseDamage)
    {
        maxHP = newMaxHP;
        health = maxHP;
        BaseDamage = newBaseDamage; // 100%, 120% damage would be baseDamage = 120;
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
        health -= amount - amount *  (armor / 100);
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

    public void InstallCyberWare(Cyberware cyberware) {
        switch (cyberware.EffectType) {
            case "maxHP":
                maxHP += cyberware.EffectStrength;
                break;
            case "armor":
                armor += cyberware.EffectStrength;
                break;
            case "damage":
                BaseDamage += cyberware.EffectStrength;
                break;
        }
    }
}