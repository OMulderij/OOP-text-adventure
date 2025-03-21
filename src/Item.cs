using System;

class Item
{
    protected int weight;
    protected string description;
    private int amount;
    protected int value;

    public Item(string newDescription, int newWeight, int newValue) {
        this.value = newValue;
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

    public int Value {
        get {
            return value;
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