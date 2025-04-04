using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest;
	private List<Enemy> enemies;
	public List<Npc> Npcs {get; set;}


	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(999999);
		enemies = new List<Enemy>();
		Npcs = new List<Npc>();
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

	public void RemoveExit(string direction) 
	{
		if (exits.ContainsKey(direction)) {
			exits.Remove(direction);
		}
	}

	public bool HasExit() {
		if (exits.Count > 0) {
			return true;
		}
		return false;
	}

	// Add an enemy to the Room.
	public void BulkAddEnemies(int count, int weaponLevel) {
		for (int i = 0;i < count; i++) {
			Enemy enemy = new Enemy(50);
			for (int l = 1; l < weaponLevel; l++) {
				enemy.Weapon.UpgradeWeapon();
			}
			enemies.Add(enemy);
		}
	}

	public void AddEnemy(int weaponLevel, int maxHP) {
		Enemy enemy = new Enemy(maxHP);
		for (int l = 1; l < weaponLevel; l++) {
			enemy.Weapon.UpgradeWeapon();
		}
		enemies.Add(enemy);
	}

	public string ShowEnemies() {
		string str = "";
		int count = 0;
		foreach(Enemy enemy in enemies) {
			str += $"Enemy {count} with {enemy.Health} HP, {enemy.ArmorType} armor and a level {enemy.Weapon.Level} {enemy.WeaponName}.\n";
			count++;
		}
		return str;
	}

	public bool HasEnemies() {
		return enemies.Count > 0;
	}

	// Damage all enemies within the Room.
	public int DamageAllEnemies(int damage) {
		int enemiesHit = enemies.Count;
		foreach(Enemy enemy in enemies) {
			enemy.Damage(damage);
		}
		return enemiesHit;
	}

	public string EnemyTurn(Player player) {
		int totalDamage = 0;
		foreach (Enemy enemy in enemies) {
			totalDamage += enemy.Attack(player);
		}
		return $"The enemies dealt {totalDamage} damage to you!";
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

	public string GetNpcList() {
		string str = "";
		List<string> nameList = new List<string>();

		foreach (Npc npc in Npcs) {
			nameList.Add(npc.Name);
		}

		if (Npcs.Count > 0) {
			str += String.Join(", ", nameList);
		}
		return str;
	}

	public Npc GetNpcByString(string name) {
		foreach (Npc npc in Npcs) {
			if (npc.Name == name) {
				return npc;
			}
		}
		return null;
	}

	public bool HasMerchant() {
		foreach (Npc npc in Npcs) {
			if (npc is Merchant) {
				return true;
			}
		}
		return false;
	}

	public void KillDeadEnemies() {
        // Removes all dead enemies from the currentRoom.
        // And drops the items / value of items in gold.
        int count = this.Enemies.Count - 1;
        for (int i = count; i >= 0; i--) {
            Enemy enemy  = this.Enemies[i];
            if (!enemy.IsAlive()) {
                Inventory inv = enemy.Drop();
                Random random = new Random();

                switch (random.Next(100)) {
                    case >= 90:
                        foreach (KeyValuePair<string, Item> entry in inv.Items) {
                            this.Chest.Put(entry.Key, entry.Value);
                        }
                        break;
                    default:
                        foreach (KeyValuePair<string, Item> entry in inv.Items) {
                            Eddies eddies = new Eddies();
                            eddies.AddValue((int)Math.Round((double)entry.Value.Value * 0.80));

                            this.Chest.Put("eddies", eddies);
                        }
                        break;
                }
                
                this.Enemies.RemoveAt(i);
            }
        }
    }
}