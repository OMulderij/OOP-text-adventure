using System;
using System.Runtime.Intrinsics.X86;

class Human
{
    protected Inventory backpack;
    private Dictionary<string, Cyberware> implants;
    private int health;
    private int maxHP;
    private int currentArmor;

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

    public void CalculateCyberwareEffects() {
        int bonusHP = 0;
        int armor = 0;

        foreach (KeyValuePair<string, Cyberware> pair in implants) {
            switch (pair.Value) {
                case HPCyberware:
                    bonusHP += pair.Value.UseEffect(this);
                    break;
                case ArmorCyberware:
                    armor += pair.Value.UseEffect(this);
                    break;
            }
        }

        currentArmor = armor;
        maxHP += bonusHP;
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

    public void InstallCyberWare(string itemName, Cyberware cyberware) {
        if (!implants.ContainsKey(itemName)) {
            implants.Add(itemName, cyberware);
        }
    }
}