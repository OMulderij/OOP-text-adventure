using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Item> roomItems;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest;

	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		roomItems = new Dictionary<string, Item>();
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

	public void AddObjectToRoom(string itemName, Item item) {
		roomItems.Add(itemName, item);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
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

	public string LookForObjects() {
		if (roomItems.Count != 0) {
			string str = "While looking around, you notice a few things in this place, namely:\n";
			foreach(KeyValuePair<string, Item> entry in roomItems)
			{
				str += $"-A {entry.Key} \n";
			}
			str += GetExitString();
			Console.WriteLine(str);
			return str;
		}
		Console.WriteLine("This place is strangely empty");
		return "This place is strangely empty";
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
