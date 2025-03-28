class Cyberware : Item {
    // Basic Cyberware class.
    // Actual effects can be found in the InstallCyberWare Method in the Human class.
    public int EffectStrength {get; init;}
    public string EffectType {get; init;}
    public Cyberware(string newEffectType, int newEffectStrength, int newPrice) : base ("A permanent upgrade to your stats.", 1  , newPrice) {
        EffectStrength = newEffectStrength;
        EffectType = newEffectType;
    }
}