using System;
using System.Runtime.Intrinsics.X86;

class Human
{
    public int BaseDamage {get; private set;}
    protected Inventory backpack;
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

    public int MaxHP {
        get {
            return this.maxHP;
        }
    }

    public int Armor {
        get {
            return armor;
        }
    }

    // Damages the Object.
    public int Damage(int amount) {
        int damage = amount - (int)Math.Round(amount * ((double)armor / 100));
        health -= damage;
        return damage;
    }

    // Heals the Object
    public void Heal(int amount) {
        health += amount;

        StopHPOvercap();
    }

    // Heals the Object based on a percentage.
    public int HealPercentage(int amount) {
        int healAmount = (int)Math.Round((double)amount / 100 * maxHP);
        health += healAmount;
        
        StopHPOvercap();
        return healAmount;
    }

    // Makes sure that the health isn't higher than maxHP.
    // Call this function whenever the Human gets healed.
    private void StopHPOvercap() {
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
        // Installing cyberware increases stats permanently.
        switch (cyberware.EffectType) {
            case "maxHP":
                maxHP += cyberware.EffectStrength;
                health = maxHP;
                break;
            case "armor":
                armor += cyberware.EffectStrength;
                break;
            case "damage":
                BaseDamage += cyberware.EffectStrength;
                break;
            case "carryweight":
                backpack.UpgradeBackpack(cyberware.EffectStrength);
                break;
        }
    }
}