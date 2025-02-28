using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room heaven = new Room("at the top of the world");
		Room hell = new Room("at the bottom of the earth");

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

		// Create the Objects
		Tree tree = new Tree();
		Cat cat = new Cat();
		cat.Use(player.Backpack, "cat");

		// Item desk = new Item("Looks very new, despite it's usage.", 150);
		// Item whiteboard = new Item("Very clean, with some markers to the side.", 100);

		Item beer = new Item("Want some?", 5);
		Item bread = new Item("Some complementary bread.", 2);

		// Item computer = new Item("No cables to connect it to the desktop.", 20);

		// Item printer = new Item("This printer is not connected to.", 20);
		
		// Add objects to the rooms
		outside.AddObjectToRoom("tree", tree); 				// refreshing
		outside.AddObjectToRoom("tree", tree); 				// refreshing
		outside.AddObjectToRoom("tree", tree); 				// refreshing
		outside.AddObjectToRoom("tree", tree); 				// refreshing
		outside.AddObjectToRoom("tree", tree); 				// refreshing

		outside.AddObjectToRoom("cat", cat); 					// revitalised 

		// theatre.AddObjectToRoom("desk", desk);				// HDMI cable 
		// theatre.AddObjectToRoom("whiteboard", whiteboard);	// markers 

		pub.AddObjectToRoom("beer", beer);						// wanna drink?
		pub.AddObjectToRoom("bread", bread);					// complementary bread

		// lab.AddObjectToRoom("computer", computer);			// not connected to the desktop

		// office.AddObjectToRoom("printer", printer);			// printer but no computer

		heaven.AddObjectToRoom("cat", cat);

		// hell.AddObjectToRoom("beer", beer);

		// Start game outside
		player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished && player.IsAlive())
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}

		if (!player.IsAlive()) {
			Console.WriteLine("\nYou have died.");
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
		Console.WriteLine("But then, you hear a faint whisper, telling you:");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

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
		player.DropToChest(command.SecondWord);
	}

	private void PrintLook() {
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		if (player.CurrentRoom.Chest.FreeWeight() != player.CurrentRoom.Chest.MaxWeight) {
			Console.WriteLine("\nWhile looking around, you notice a few things in this place, namely:");
			Console.WriteLine(player.CurrentRoom.Chest.Show());
		} else { // Runs if inventory (chest) is empty.
			Console.WriteLine("This place is strangely empty.");
		}
	}

	private void PrintUse(Command command) {
		if (!command.HasSecondWord()) {
			// Doesn't know what to use without a second command word.
			Console.WriteLine("Use *what*?");
			return;
		}
		Console.WriteLine(player.UseItem(command.SecondWord));
	}

	private void PrintStatus() {
		Console.WriteLine("You have " + player.Health + " health at your disposal.\n");
		if (player.Backpack.FreeWeight() != player.Backpack.MaxWeight) {
			Console.WriteLine("You have stored these items in your backpack:");
			Console.WriteLine(player.Backpack.Show());
		} else { // Runs if inventory (backpack) is empty.
			Console.WriteLine("Your backpack is empty.");
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

		player.Damage(5);
		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}
