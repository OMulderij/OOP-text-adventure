class Fixer : Npc
{
    private Player player;
    // Fixer unlocks the door to the dungeon after talking to him.
    public Fixer(Player newPlayer) : base("fixer") {
        player = newPlayer;
    }

    public override string Talk() {
        string str = "";

        if (!player.ActiveQuest) {
            str += "You want to fly through the big leagues right into the realm of solos?\n";
            str += "Everyone in Night City knows that you're asking for the impossible.\n";
            str += "But whatever. Choom, you do you.\n";
            str += "Why don't you show me what skills you've got by flatlining everyone in that Maelstrom hideout south of here?\n";
            str += "I'm afraid I won't be able to get you to the big leagues otherwise.";
            player.ActiveQuest = true;
        } else {
            str += "Feeling lonely?\n";
            str += "You know damn well that I'm no joytoy.\n";
            str += "Come back when you've cleared the Maelstrom hideout.";
        }

        if (player.CompletedQuest) {
            str = "What? Are you serious?\n";
            str += "You just went in and killed everyone in the hideout?\n";
            str += "Shit choom, I don't know what to say...\n";
            str += "Seems like I've struck gold on pure accident.\n";
            str += "Be prepared kiddo, since I've already got a lot of gigs in line for you.";
        }
        return str;
    }
}