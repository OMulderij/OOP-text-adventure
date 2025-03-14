using System;
using System.Diagnostics;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	private Stopwatch stopWatch;
	private List<Room> dungeon;
	private Room outside;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		stopWatch = new Stopwatch();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room heaven = new Room("at the top of the world");
		Room hell = new Room("at the bottom of the earth");
		dungeon = CreateDungeon(10);

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		outside.AddExit("up", heaven);
		outside.AddExit("down", hell);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		heaven.AddExit("down", outside);

		hell.AddExit("up", outside);

		hell.AddExit("south", dungeon[0]);


		// Create the Objects
		Item smg = new Weapon("light");

		Item beer = new Item("Want some?", 5);
		Item bread = new Item("Some complementary bread.", 2);

		outside.Chest.Put("smg", smg);

		pub.Chest.Put("beer", beer);
		pub.Chest.Put("bread", bread);
		pub.Chest.Put("bread", bread);
		pub.Chest.Put("bread", bread);
		pub.Chest.Put("smg", smg);

		dungeon[2].Chest.Put("smg", smg);
		dungeon[10].AddExit("up", pub);


		// Start game outside
		player.CurrentRoom = outside;
	}

	// Create the dungeon.
	private List<Room> CreateDungeon(int floorCount) {
		List<Room> floors = new List<Room>();
		for (int i = 0;i <= floorCount; i++) {
			// Generate a new floor in the dungeon, add an exit to the next floor and add enemies to it.
			Room dungeonfloor = new Room($"on floor {i} of the Maelstrom hideout");
			floors.Add(dungeonfloor);
			if (i > 0) {
				floors[i - 1].AddExit("up", dungeonfloor); // Change to only add an exit when all enemies on a floor are dead.
			}
			dungeonfloor.AddEnemies(i, (int)Math.Round((double)i / 2));
			Console.WriteLine(i + "\n");
		}
		return floors;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit, dies or wins.
		bool finished = false;
		while (!finished)
		{
			if (!player.IsAlive()) {
				Console.WriteLine("\nYou have died.");
                finished = true;
				continue;
			}


        	stopWatch.Start();

			Command command = parser.GetCommand();
			
			stopWatch.Stop();
			Console.WriteLine(stopWatch.ElapsedMilliseconds);
			stopWatch.Reset();
			
			finished = ProcessCommand(command);
		}

		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}



	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly interesting adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "use":
				PrintUse(command);
				break;
			case "look":
				PrintLook();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "leave" when parser.CommandLibary.IsValidCommandWord("leave"):
				MoveRoom(outside);
				break;
		}
		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You start to wander around at the university, not knowing where you might end up next.");
		Console.WriteLine("But at that moment, you hear a faint whisper, telling you:\n");
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Attempt to take an object from the current room,
	// and print the result of it.
	private void Take(Command command)
	{
		if (!command.HasSecondWord()) {
			// Does not know what to take without a second word.
			Console.WriteLine("Take *what*?");
			return;
		}
		
		if (!player.CurrentRoom.Chest.ItemInInventory(command.SecondWord)) {
			// Can't take an item if it doesn't exist.
			Console.WriteLine($"There is no {command.SecondWord} in the room with you.");
			return;
		}
		player.TakeFromChest(command.SecondWord);
	}

	// Attempt to drop something from the player backpack to the current room,
	// And print out the result.
	private void Drop(Command command)
	{
		if (!command.HasSecondWord()) {
			// Does not know what to drop without a second word.
			Console.WriteLine("Drop *what*?");
			return;
		}

		if (!player.Backpack.ItemInInventory(command.SecondWord)) {
			// Can't drop an item if it's not in the backpack.
			Console.WriteLine($"This {command.SecondWord} is not in your backpack.");
			return;
		}
		Console.WriteLine(player.DropToChest(command.SecondWord));
	}

	// Attempt to use an item from the backpack,
	// And print out the result.
	private void PrintUse(Command command) {
		if (!command.HasSecondWord()) {
			// Doesn't know what to use without a second command word.
			Console.WriteLine("Use *what*?");
			return;
		}
		Console.WriteLine(player.UseItem(command));
	}

	// Print out the contents of the current room.
	private void PrintLook() {
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		if (!player.CurrentRoom.Chest.EmptyRoom()) {
			Console.WriteLine("\nWhile looking around, you spot a few things, the most notable one(s):");
			Console.WriteLine(player.CurrentRoom.Chest.Show());
		} else {
			Console.WriteLine("This place does not contain any items of note.\n");
		}

		if (player.CurrentRoom.HasEnemies()) {
			Console.WriteLine("These are the enemies within the room:");
			Console.WriteLine(player.CurrentRoom.ShowEnemies());
		} else {
			Console.WriteLine("You have not made any enemies here, good job choom.");
		}
	}

	// Print out the current status of the player:
	// Of their health and backpack status.
	private void PrintStatus() {
		Console.WriteLine("You have " + player.Health + " health at your disposal.\n");
		Console.WriteLine("You have these items in your pocket:");
		Console.WriteLine(player.Backpack.ShowPlayerItems());
		
		if (!player.Backpack.EmptyRoom()) {
			Console.WriteLine("You have stored these items in your backpack:");
			Console.WriteLine(player.Backpack.Show());
		} else {
			Console.WriteLine("Your backpack is completely empty.");
		}
		Console.WriteLine($"You have {player.Backpack.FreeWeight()} kgs left in your backpack.");
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go *where*?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}

		if (dungeon.Contains(nextRoom)) {
			parser.AddDungeonCommands();
		} else {
			parser.RemoveDungeonCommands();
		}
		MoveRoom(nextRoom);
	}

	private void MoveRoom(Room nextRoom) {
		player.CurrentRoom = nextRoom;
		player.Backpack.AddCharge("healer");
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}
