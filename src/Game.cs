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
		Item tree = new Item("a very calming tree", 1000);
		Item cat = new Item("an incredibly cute cat", 25);

		Item desk = new Item("looks very new, despite it's usage", 150);
		Item whiteboard = new Item("very clean, with some markers to the side", 100);

		Item beer = new Item("want some?", 5);
		Item bread = new Item("some complementary bread", 2);

		Item computer = new Item("no cables to connect it to the desktop", 20);

		Item printer = new Item("printer but no computer?", 20);
		
		// Add objects to the rooms
		outside.AddObjectToRoom("tree", tree); 				// refreshing
		outside.AddObjectToRoom("cat", cat); 				// revitalised 

		theatre.AddObjectToRoom("desk", desk);				// HDMI cable 
		theatre.AddObjectToRoom("whiteboard", whiteboard);	// markers 

		pub.AddObjectToRoom("beer", beer);					// wanna drink?
		pub.AddObjectToRoom("bread", bread);				// complementary bread

		lab.AddObjectToRoom("computer", computer);			// not connected to the desktop

		office.AddObjectToRoom("printer", printer);			// printer but no computer

		heaven.AddObjectToRoom("cat", cat);

		hell.AddObjectToRoom("beer", beer);

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
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);

			if (!player.IsAlive()) {
				finished = true;
				Console.WriteLine("\nYou have died.");
			}
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
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				if (player.CurrentRoom.Chest.FreeWeight() != player.CurrentRoom.Chest.MaxWeight) {
					Console.WriteLine("\nWhile looking around, you notice a few things in this place, namely:");
					player.CurrentRoom.Chest.PrintItemsInInventory();
				} else {
					Console.WriteLine("This place is strangely empty.");
				}
				break;
			case "status":
				Console.WriteLine("You have " + player.Health + " health at your disposal.\n");
				if (player.Backpack.FreeWeight() != player.Backpack.MaxWeight) {
					Console.WriteLine("You have stored these items in your backpack:");
					player.Backpack.PrintItemsInInventory();
				} else {
					Console.WriteLine("Your backpack is empty.");
				}
				Console.WriteLine($"You have {player.Backpack.FreeWeight()} kgs left in your backpack.");
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
			Console.WriteLine("Take *what*?");
			return;
		}
		
		if (!player.CurrentRoom.Chest.ItemInInventory(command.SecondWord)) {
			Console.WriteLine($"There is no {command.SecondWord} in the room with you.");
			return;
		}
		player.TakeFromChest(command.SecondWord);
	}
	private void Drop(Command command)
	{
		if (!command.HasSecondWord()) {
			Console.WriteLine("Drop *what*?");
			return;
		}

		if (!player.Backpack.ItemInInventory(command.SecondWord)) {
			Console.WriteLine($"This {command.SecondWord} is not in your backpack.");
			return;
		}
		
		player.DropToChest(command.SecondWord);
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
