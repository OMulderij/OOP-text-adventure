using System;

class Player
{
    private Room currentRoom;
    private int health;

    public Player()
    {
        health = 100;
    }

    public int Health {
        get {
            return this.health;
        }
    }

    public Room CurrentRoom {
        get {
            return this.currentRoom;
        }
        set {
            this.currentRoom = value;
        }
    }

    public void Damage(int amount) {
        health -= amount;
    }

    public void Heal(int amount) {
        health += amount;
    }

    public bool IsAlive() {
        if (health <= 0) {
            return false;
        }
        return true;
    }
}