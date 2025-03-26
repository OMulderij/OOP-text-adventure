abstract class Npc
{
    // Every npc needs a name and a Talk() function.
    
    public string Name {get; init;}
    public Npc(string newName) {
        Name = newName;
    }
    public abstract string Talk(Player player);
}