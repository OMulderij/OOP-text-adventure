abstract class Cyberware : Item {
    public Cyberware(string newDescription, int newValue) : base (newDescription, 1, newValue) {}

    public abstract int Effect(Human player);
}