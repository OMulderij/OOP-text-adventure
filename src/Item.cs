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

    public string Description {
        get {
            if (this.description == "none") {
                return "This item does not any effects.";
            }
            return this.description;
        }
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

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public virtual string Use(Object o, string itemName) {
        return this.description;
    }
}