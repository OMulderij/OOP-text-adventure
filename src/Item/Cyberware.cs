class Cyberware : Item {
    public int EffectStrength {get; init;}
    public string EffectType {get; init;}
    public int Price {get; init;}
    public Cyberware(string newEffectType, int newEffectStrength, int newPrice) : base ("A permanent upgrade to your stats.", 1  , 100) {
        EffectStrength = newEffectStrength;
        EffectType = newEffectType;
        Price = newPrice;
    }
}