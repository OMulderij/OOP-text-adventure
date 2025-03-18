class Fixer : Npc
{
    public Fixer() : base() {}

    public override string Talk(Player player) {
        string str = "";

        if (!player.ActiveQuest) {
            str += "You want to fly through the big leagues right into the realm of solos?\n";
            str += "Everyone in Night City knows that you're asking for the impossible.\n";
            str += "But whatever, choom, you do you.\n";
            str += "Why don't you show me what skills you've got by flatlinin' everyone in that Maelstrom hideout around the corner?\n";
            str += "I'm afraid I won't be able to get you to the big leagues otherwise.";
            player.ActiveQuest = true;
        } else {
            str += "Feeling lonely?\n";
            str += "You know damn well that I'm no joytoy.\n";
            str += "Come back when you've cleared the Maelstrom hideout.";
        }
        return str;
    }

    public bool GetQuest() {
        return true;
    }
}