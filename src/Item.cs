using System;

public class Item
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

    public virtual string Use() {
        return this.description;
    }
}

class Tree : Item 
{
    public Tree() : base("You feel refreshed.", 1) {}
}

public class Cat : Item
{
    public Cat() : base("Its a very cute cat", 5) {}

    public override string Use() {
        this.description = "Meow";

        return this.description;
    }
}