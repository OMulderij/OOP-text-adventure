class ArmorCyberware : Cyberware
{
    public ArmorCyberware() : base("Decreases the amount of damage taken.", 100) {}

    public override int Effect(Human player)
    {
        return 0;
    }
}