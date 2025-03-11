class PlayerItem : Item
{
    private int maxCount = 2;
    private int usesLeft = 2;
    public PlayerItem(string newDescription, int newWeight) : base(newDescription, newWeight) {}

    public int UsesLeft {
        get {
            return usesLeft;
        }
    }
    public void UpgradeItem() {
        maxCount = 3;
        usesLeft = 3;
    }

    public bool IsItemUpgraded() {
        if (maxCount == 3) {
            return true;
        }
        return false;
    }

    protected bool UseItem() {
        if (usesLeft > 0) {
            usesLeft--;
            return true;
        }
        return false;
    }

    public void AddItemUse() {
        if (usesLeft < maxCount) {
            usesLeft++;
        }
    }
}