using System;

class Item
{
    protected int weight;
    protected string description;
    private int amount;

    public Item(string newDescription, int newWeight) {
        this.description = newDescription;
        this.weight = newWeight;
        this.amount = 1;
    }

    public int Weight {
        get {
            return weight;
        }
    }

    public int Amount {
        get {
            return amount;
        }
        set {
            this.amount = value;
        }
    }

    // Creates a clone of the current Object.
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    // Basic Use() function
    // Gets overriden by subclasses.
    public virtual string Use(Object o) {
        return this.description;
    }
}