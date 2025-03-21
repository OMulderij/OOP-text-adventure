class Eddies : Item
{
    public Eddies() : base("Used to buy stuff with.", 1, 1) {
        this.Amount = 1;
        this.value = 0;
    }

    public void AddValue(int amount) {
        if (this is Eddies) {
            this.value += amount;

        }
    }

    public void RemoveValue(int amount) {
        this.value -= amount;
        if (this.value < 0) {
            this.value = 0;
        }
    }
}