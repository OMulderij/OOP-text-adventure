using System;

class Item 
{
    private string itemName;
    private string itemDescription;
    private string itemEffect;

    public Item(string name) {
        itemName = name;
    }

    public string AddItemDescription(string selectedItemDescription) {
        itemDescription = selectedItemDescription;
        return itemDescription;
    }

    public string AddItemEffect(string selectedItemEffect) {
        itemDescription = selectedItemEffect;
        return itemDescription;
    }

    public string GetItemName() {
        return itemName;
    }

    public string GetItemDescription() {
        if (itemDescription == null) {
            return "This item does not have a description.";
        }
        return itemDescription;
    }

    public string GetItemEffect() {
        if (itemEffect == null) {
            return "This item does not any effects.";
        }
        return itemEffect;
    }
}