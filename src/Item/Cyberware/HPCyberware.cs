class HPCyberware : Cyberware
{
    public HPCyberware() : base("Increases your maximum HP.", 100) {}


    public override int UseEffect(Human player) {
        return 0;
    }
}