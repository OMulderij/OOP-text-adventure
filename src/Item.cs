using System;

class Item
{
    private int weight;
    private string description;

    public Item(string newDescription, int newWeight) {
        this.description = newDescription;
        this.weight = newWeight;
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
}