class Cyberware : Item {
    public int EffectStrength {get; init;}
    public string EffectType {get; init;}
    public Cyberware(string newDescription, int newEffectStrength, string newEffectType) : base (newDescription, 1  , 100) {
        EffectStrength = newEffectStrength;
        EffectType = newEffectType;
    }
}