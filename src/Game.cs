class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	private List<Room> dungeon;
 	private Room outside;
	private ArmsDealer dealer;
	private RipperDoc ripperDoc;
	
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
		outside = new Room("in the City of Dreams, surrounded by neon lights and advertisements");
		Room bar = new Room("in a small, but cozy bar in Night City");
		Room market = new Room("in a small market filled with merchants");
		Room clinic = new Room("in a surprisingly large ripper doc clinic");
		dungeon = CreateDungeon(10);

		// Initialise room exits
		outside.AddExit("north", clinic);
		outside.AddExit("east", market);
		outside.AddExit("west", bar);

		bar.AddExit("east", outside);

		market.AddExit("west", outside);

		clinic.AddExit("south", outside);

		// Create the Objects
		Weapon handgun = new Weapon("light", 15);
		handgun.UpgradeWeapon();
		handgun.UpgradeWeapon();
		handgun.UpgradeWeapon();
		handgun.UpgradeWeapon();

		Item beer = new Item("Want some?", 5, 2);
		Item bread = new Item("Some complementary bread.", 2, 5);

		bar.Chest.Put("beer", beer);
		bar.Chest.Put("bread", bread);
		bar.Chest.Put("bread", bread);
		bar.Chest.Put("bread", bread);
		bar.Chest.Put("handgun", handgun);

		dungeon[0].Chest.Put("handgun", handgun);

		// Initialise Npcs
		Npc fixer = new Fixer(player);
		dealer = new ArmsDealer(player);
		ripperDoc = new RipperDoc();

		// Add the Npcs
		bar.Npcs.Add(fixer);

		market.Npcs.Add(dealer);

		clinic.Npcs.Add(ripperDoc);

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
			int halved = (int)Math.Round((double)i / 2);
			
			if (halved == 0 && i > 0) {
				halved = 1;
			}
			dungeonfloor.BulkAddEnemies(halved, halved);
		}
		floors[^1].AddEnemy(5, 150);
		outside.RemoveExit("south");
		outside.AddExit("south", floors[0]);
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

			if (player.CompletedQuest) {
				Console.WriteLine("\n Congratulations on beating the game!");
				Console.WriteLine("You are now ready to tackle the big leagues.");
				finished = true;
				continue;
			}

			if (dungeon.Contains(player.CurrentRoom)) {
				if (player.CurrentRoom.Enemies.Count == 0 && !player.CurrentRoom.HasExit()) {
					if (dungeon.IndexOf(player.CurrentRoom) < dungeon.Count - 1) {
						player.CurrentRoom.AddExit("up", dungeon[dungeon.IndexOf(player.CurrentRoom)+1]);
					} else {
						player.CurrentRoom.AddExit("out", outside);
					}
				}
			}

			if (player.CurrentRoom.Npcs.Count > 0) {
				parser.AddCommand("talk");
			} else {
				parser.RemoveCommand("talk");
			}

			Command command = parser.GetCommand();
			finished = ProcessCommand(command);

			player.CurrentRoom.KillDeadEnemies();
		}

		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}



	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to CyberText!");
		Console.WriteLine("CyberText is a new, incredibly interesting adventure game.");
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
			case "leave":
				LeaveDungeon();
				break;
			case "talk":
				TalkToNpc(command);
				break;
			case "buy":
				BuyItem(command);
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
		Console.WriteLine("You wander around Night City, seeing the bustling streets, brutal gangs and brightly lit advertisements flash past you like they're nothing.");
		Console.WriteLine("Ending up in the reconciliation park, you think this place is more pleasant than initially expected.");
		Console.WriteLine("You sit here for a while, meditating...");
		Console.WriteLine("But at that moment, a monk approaches you, and you start to talk.");
		Console.WriteLine("He discovers that you are lost, alone, and devoid of ideas on what to do with your life.");
		Console.WriteLine("And like a saint from the heavens, it's almost like he can read your destiny, and tells you:\n");
		// let the parser print the commands
		parser.PrintValidCommands();
		Console.WriteLine("\nAfter this mysterious encounter, you wander back to where you came from.");
		Console.WriteLine("Filled with inspiration from the monk, you are ready to tackle the world again.");
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
		if (!player.CurrentRoom.Chest.IsEmpty()) {
			Console.WriteLine("\nWhile looking around, you spot a few things, the most notable one(s):");
			Console.WriteLine(player.CurrentRoom.Chest.Show());
		} else {
			Console.WriteLine("This place does not contain any items of note.\n");
		}

		if (player.CurrentRoom.HasEnemies()) {
			Console.WriteLine("These are the enemies within the room:");
			Console.WriteLine(player.CurrentRoom.ShowEnemies());
		}

		if (player.CurrentRoom.Npcs.Count > 0) {
			Console.WriteLine("You can talk to these peeps:");
			Console.WriteLine(player.CurrentRoom.GetNpcList() + "\n");
		}

		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Print out the current status of the player:
	// Of their health and backpack status.
	private void PrintStatus() {
		Console.WriteLine($"You have {player.Health}/{player.MaxHP} health at your disposal.");
		if (player.Armor > 0) {
			Console.WriteLine($"With a total of {player.Armor} armor.");
		}

		Console.WriteLine("\nYou have these items in your pocket:");
		Console.WriteLine(player.Backpack.ShowPlayerItems());
		
		if (!player.Backpack.IsEmpty()) {
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

		// Checks if the player has activated the quest by talking to the Fixer : Npc in the bar.
		if (dungeon.Contains(nextRoom) && !player.ActiveQuest) {
			Console.WriteLine("You are standing in front of an abandoned building.");
			Console.WriteLine("The smell of blood oozes from the building, leaving you frozen in front of the entrance.");
			Console.WriteLine("Despite your determination to explore the city, you decide to back off for now.");
			return;
		} else if (dungeon.Contains(nextRoom) && player.ActiveQuest && dungeon.IndexOf(nextRoom) == 0) {
			Console.WriteLine("You are ready to take on the hideout now.");
			Console.WriteLine("Iron in hand, meds at the ready and filled with determination.");
			Console.WriteLine("It's time to send some lead flying.\n");
		}

		MoveRoom(nextRoom);
	}

	private void LeaveDungeon() {
		Console.WriteLine("You decide that it's best to leave for now.");
		Console.WriteLine("After the heart-wrenching firefight that you have just witnessed, you decide to go to the reconciliation park.");
		Console.WriteLine("Following in the steps of the nearby monks, you meditate a bit.");
		Console.WriteLine("This meditating session has restored your health back to full.");
		Console.WriteLine("This session also took long enough to restore your grenade charges back to full.");
		MoveRoom(outside);
	}

	private void TalkToNpc(Command command) {
		// Can't talk to an npc if the user didn't tell us which one.
		if (!command.HasSecondWord()) {
			Console.WriteLine("Talk to *who*?");
			return;
		}

		// String.ToLower() since the Look command prints the Fixer name with a capital letter.
		Npc npc = player.CurrentRoom.GetNpcByString(command.SecondWord.ToLower());
		
		if (npc == null) {
			Console.WriteLine(command.SecondWord + " is not in this room.");
			return;
		}

		if (npc.GetType() == typeof(Fixer) && player.HighestFloor == dungeon.Count-1) {
			player.CompletedQuest = true;
		}

		if (npc is Merchant) {
			parser.AddCommand("buy");
		} else {
			parser.RemoveCommand("buy");
		}

		// Talk to an npc, and write each message 2 seconds after the last.
		string[] talkStr = npc.Talk().Split("\n");
		foreach (string str in talkStr) {
			if (str != "") {
				Console.WriteLine(str);
				Task.Delay(2000).Wait();
			}
		}
		player.lastTalkedToNpc = npc;
	}

	private void BuyItem(Command command) {
		Merchant merchant = (Merchant)player.lastTalkedToNpc;
		Item boughtItem = (Item)merchant.Buy(player.Backpack.GetItemByString("eddies").Value, command.SecondWord);
		Eddies eddies = (Eddies)player.Backpack.GetItemByString("eddies");

		if (boughtItem != null && boughtItem.GetType() != typeof(Eddies)) {
			if (boughtItem is PlayerItem) {
				PlayerItem item = (PlayerItem)player.Backpack.GetItemByString(command.SecondWord);
				item.UpgradeItem();
			} else if (boughtItem is Cyberware cyberware) {
				player.InstallCyberWare(cyberware);
			} else {
				player.Backpack.Put(command.SecondWord, boughtItem);
			}
			eddies.RemoveValue(boughtItem.Value);
			Console.WriteLine("Thank you for your patronage.");
		} else if (boughtItem is Eddies) {
			Console.WriteLine("Just what are you trying to pull here?");
		}
	}

	private void MoveRoom(Room nextRoom) {
		// Add Commands if the player is in a specific room
		if (dungeon.Contains(nextRoom)) {
			parser.AddCommand("leave");
		} else if (dungeon.Contains(player.CurrentRoom)) {
			// Run if player is exiting the dungeon
			player.HighestFloor = dungeon.IndexOf(player.CurrentRoom);

			dungeon = CreateDungeon(dungeon.Count - 1);

			dealer.RandomizeStock();
			
			// Remove the buy command if you leave the room

			player.Backpack.AddCharge("grenade");
			player.Backpack.AddCharge("grenade");
			player.Backpack.AddCharge("grenade");
			player.HealPercentage(100);


			parser.RemoveCommand("leave");
		}

		parser.RemoveCommand("buy");
		player.lastTalkedToNpc = null;

		// Run everything that needs to be ran every time the player.CurrentRoom changes.
		player.CurrentRoom = nextRoom;
		player.Backpack.AddCharge("healer");
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}