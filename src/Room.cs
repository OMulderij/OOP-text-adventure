using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest;
	protected EnemyInventory enemies;


	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999);
	}

	public Inventory Chest {
		get {
			return chest;
		}
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return "You are " + description + ".";
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	public string GetLongDescription()
	{
		string str = "You are ";
		str += description;
		str += ".\n";
		str += GetExitString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		string str = "Exits: ";
		str += String.Join(", ", exits.Keys);

		return str;
	}
}

class Dungeon : Room
{
	Dictionary<int, DungeonFloor> floors;
	public Dungeon(int floorCount) : base("at the entrance of the Maelstrom hideout") {
		floors = new Dictionary<int, DungeonFloor>();
		for (int i = floorCount;i >= 1; i--) {
			DungeonFloor dungeonfloor = new DungeonFloor(i);
			if (i != 1&& i != floorCount) {
				dungeonfloor.AddExit("up", floors[i+1]);
			}
			floors.Add(i, dungeonfloor);
		}
		AddExit("south",floors[1]);
	}
}

class DungeonFloor : Room
{
	public DungeonFloor(int enemyCount) : base("inside the Maelstrom hideout.") {
		enemies = new EnemyInventory(enemyCount);
		Console.WriteLine(enemies.EnemyCount());
	}

	public EnemyInventory Enemies {
		get {
			return enemies;
		}
	}
}