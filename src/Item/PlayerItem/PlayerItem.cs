class PlayerItem : Item
{
    private int maxCount;
    private int usesLeft;

    public PlayerItem(string newDescription, int newWeight, int newPrice) : base(newDescription, newWeight, newPrice) {
        maxCount = 2;
        usesLeft = maxCount;
    }

    public int UsesLeft {
        get {
            return this.usesLeft;
        }
    }
    
    // Increase the max count of the item.
    public void UpgradeItem() {
        this.maxCount = 3;
        this.usesLeft = 3;
    }

    // Check if the item is already upgraded
    public bool IsItemUpgraded() {
        if (this.maxCount == 3) {
            return true;
        }
        return false;
    }

    // Remove one use of the item.
    protected bool UseItem() {
        if (this.usesLeft > 0) {
            this.usesLeft--;
            return true;
        }
        return false;
    }

    // Increase one use of the item.
    public void AddItemUse() {
        if (this.usesLeft < this.maxCount) {
            this.usesLeft++;
        }
    }
}