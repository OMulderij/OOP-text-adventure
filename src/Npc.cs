abstract class Npc
{
    public string Name {get; init;}
    public Npc(string newName) {
        Name = newName;
    }
    public abstract string Talk(Player player);
}