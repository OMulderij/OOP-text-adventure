using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest;
	private List<Enemy> enemies;


	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999);
		enemies = new List<Enemy>();
	}

	public Inventory Chest {
		get {
			return chest;
		}
	}

	public List<Enemy> Enemies {
		get {
			return enemies;
		}
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Add an enemy to the Room.
	public void AddEnemies(int count) {
		for (int i = 0;i < count; i++) {
			Enemy enemy = new Enemy(50);
			enemies.Add(enemy);
		}
	}

	// Damage all enemies within the Room.
	public int DamageAllEnemies(int damage) {
		int enemiesHit = enemies.Count;
		foreach(Enemy enemy in enemies) {
			enemy.Damage(damage);
		}
		return enemiesHit;
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