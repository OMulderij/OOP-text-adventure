class Eddies : Item
{
    // Basic class to make it easier to differentiate items and eddies from eachother.
    // There's probably a better way to do this, but I'm lazy so I'll just use this.
    public Eddies() : base("Used to buy stuff with.", 1, 1) {
        this.Amount = 1;
        this.value = 0;
    }
    public void RemoveValue(int amount) {
        this.value -= amount;
        if (this.value < 0) {
            this.value = 0;
        }
    }
}