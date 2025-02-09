using System;
using System.Text.RegularExpressions;

// declare a nullable exception variable to store any errors during program execution
Exception? exception = null;


// Define an array of game levels,each level contains:
// startPosition - player's initial position (Left,Top coordinates)
// array of map frames,each contains a map string and a frame delay time
((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[])[] levels =
[
	#region level 00
	((15, 16), // Set the player's starting position at (15,16)
	new (string Map, TimeSpan Delay)[]
	{
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.1)), // a single map frame with 0.1 second delay 
	}),
	#endregion
    /*
    **********************************************We change the LV2**********************************
    */
	#region level 01 
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═══════╗
		            ║   @   ║
		            ║       ║
		            ║  #####║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║#####  ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ║       ║
		            ╚═══════╝
		""", TimeSpan.FromSeconds(.2)),
	}),
	#endregion
	#region level 02
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║ ##      ║
		            ║###      ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║      ## ║
		            ║      ###║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║  ##     ║
		            ║ ###     ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║     ##  ║
		            ║     ### ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║  ###    ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║    ###  ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║   ###   ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║   ###   ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
        		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║    ###  ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║  ###    ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
        		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║     ##  ║
		            ║     ### ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║  ##     ║
		            ║ ###     ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
                ("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║      ## ║
		            ║      ###║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║ ##      ║
		            ║###      ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
//*************************************************************************************************************
        		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║     ##  ║
		            ║     ### ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║  ##     ║
		            ║ ###     ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
        		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║    ###  ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║  ###    ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),		
        ("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║   ###   ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║   ###   ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║   ##    ║
		            ║  ###    ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║    #    ║
		            ║    ##   ║
		            ║    ###  ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
                ("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║  ##     ║
		            ║ ###     ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║     ##  ║
		            ║     ### ║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
        		("""

		            ╔═════════╗
		            ║    @    ║
		            ║         ║
		            ║    #    ║
		            ║   #     ║
		            ║ ##      ║
		            ║###      ║
		            ║         ║
		            ║         ║
		            ║    #    ║
		            ║     #   ║
		            ║      ## ║
		            ║      ###║
		            ║         ║
		            ║         ║
		            ╚═════════╝
		""", TimeSpan.FromSeconds(.2)),
        

	}),
	#endregion
	#region level 03
	((15, 16),
	new (string Map, TimeSpan Delay)[]
	{
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#####    ######║
		            ║#####    ######║
		            ║###############║
		            ║###########    ║
		            ║###############║
		            ║    ###########║
		            ║    ###########║
		            ║###############║
		            ║#########    ##║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║######    #####║
		            ║######    #####║
		            ║###############║
		            ║##########    #║
		            ║###############║
		            ║#    ##########║
		            ║#    ##########║
		            ║###############║
		            ║########    ###║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#######    ####║
		            ║#######    ####║
		            ║###############║
		            ║#########    ##║
		            ║###############║
		            ║##    #########║
		            ║##    #########║
		            ║###############║
		            ║#######    ####║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║########    ###║
		            ║########    ###║
		            ║###############║
		            ║########    ###║
		            ║###############║
		            ║###    ########║
		            ║###    ########║
		            ║###############║
		            ║######    #####║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#########    ##║
		            ║#########    ##║
		            ║###############║
		            ║#######    ####║
		            ║###############║
		            ║####    #######║
		            ║####    #######║
		            ║###############║
		            ║#####    ######║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║##########    #║
		            ║##########    #║
		            ║###############║
		            ║######    #####║
		            ║###############║
		            ║#####    ######║
		            ║#####    ######║
		            ║###############║
		            ║####    #######║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║###########    ║
		            ║###########    ║
		            ║###############║
		            ║#####    ######║
		            ║###############║
		            ║######    #####║
		            ║######    #####║
		            ║###############║
		            ║###    ########║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(1)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║##########    #║
		            ║##########    #║
		            ║###############║
		            ║######    #####║
		            ║###############║
		            ║#####    ######║
		            ║#####    ######║
		            ║###############║
		            ║####    #######║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#########    ##║
		            ║#########    ##║
		            ║###############║
		            ║#######    ####║
		            ║###############║
		            ║####    #######║
		            ║####    #######║
		            ║###############║
		            ║#####    ######║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║########    ###║
		            ║########    ###║
		            ║###############║
		            ║########    ###║
		            ║###############║
		            ║###    ########║
		            ║###    ########║
		            ║###############║
		            ║######    #####║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#######    ####║
		            ║#######    ####║
		            ║###############║
		            ║#########    ##║
		            ║###############║
		            ║##    #########║
		            ║##    #########║
		            ║###############║
		            ║#######    ####║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║######    #####║
		            ║######    #####║
		            ║###############║
		            ║##########    #║
		            ║###############║
		            ║#    ##########║
		            ║#    ##########║
		            ║###############║
		            ║########    ###║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
		("""

		            ╔═══════════════╗
		            ║       @       ║
		            ║               ║
		            ║###############║
		            ║#####    ######║
		            ║#####    ######║
		            ║###############║
		            ║###########    ║
		            ║###############║
		            ║    ###########║
		            ║    ###########║
		            ║###############║
		            ║#########    ##║
		            ║               ║
		            ║               ║
		            ╚═══════════════╝
		""", TimeSpan.FromSeconds(.3)),
	}),
	#endregion
];

// the main loop of the game,contains the important game logic and exception handling
try
{
	// Initialize game variables
	bool escape = false; // to control game exit
	int lives = 100; // Player's starting lives
	int frame = 0; // Current animation frame index
	int levelIndex = 0; // Current level index
	// Get the first level's data and initialize player position
	((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[] Frames) level = levels[levelIndex];
	(int Top, int Left) position = level.StartPosition;

	// Set initial player direction and hide cursor
	ConsoleKey lastMovementKey = ConsoleKey.UpArrow;
	Console.CursorVisible = false;
	Console.Clear();

	// Main game loop
	while (!escape)
	{
	NextLevel:
		// Update level data for current level
		level = levels[levelIndex];
		Console.CursorVisible = false;

		// Draw game UI header
		Console.SetCursorPosition(0, 0); // Set cursor position to the top left corner
		Console.WriteLine();
		Console.WriteLine("  Bound");
		Console.WriteLine();
		Console.WriteLine($"  Lives: {lives}   ");
		Console.WriteLine($"  Level: {levelIndex}");

		// Show special instruction for level 3
		if (levelIndex == 3) {
			Console.WriteLine("  Press [space] to jump forward!");
		}

		// Store current cursor position for map drawing
		int mapTop = Console.CursorTop;
		Console.Write(level.Frames[frame].Map); // Draw the current frame's map
		string[] map = LineEndRegex().Split(level.Frames[frame].Map); // Split map into lines for collision detection

		// Check if player is on hazard
		if (map[position.Top][position.Left] is '#')
		{
			lastMovementKey = ConsoleKey.UpArrow;
			position = levels[levelIndex].StartPosition;
			lives--;

			// Check for game over condition
			if (lives <= 0)
			{
				goto YouLose;
			}
		}
		Console.SetCursorPosition(position.Left, position.Top + mapTop);
		Console.Write(GetPlayerChar());
		DateTime start = DateTime.Now;
		while (DateTime.Now - start < level.Frames[frame].Delay)
		{
			while (Console.KeyAvailable)
			{
				// Handle keyboard input for player movement
				switch (Console.ReadKey(true).Key)
				{
					// Handle upward movement
					case ConsoleKey.UpArrow:
						lastMovementKey = ConsoleKey.UpArrow;
						// Check if the space above is available for movement
						if (map[position.Top - 1][position.Left] is ' ' or '@' or '#') 
						{
							lastMovementKey = ConsoleKey.UpArrow; // Set last movement key to up
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' '); // Clear current position
							position.Top--; // Move player up
						}
						{
							lastMovementKey = ConsoleKey.UpArrow;
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' '); // Clear current position
							position.Top--; // Move player up
						}
						break;

					// Handle downward movement
					case ConsoleKey.DownArrow:
						lastMovementKey = ConsoleKey.DownArrow;
						// Check if the space below is available
						if (map[position.Top + 1][position.Left] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Top++; //down
						}
						break;
					case ConsoleKey.LeftArrow:
						lastMovementKey = ConsoleKey.LeftArrow;
						if (map[position.Top][position.Left - 1] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Left--; //left
						}
						break;
					case ConsoleKey.RightArrow:
						lastMovementKey = ConsoleKey.RightArrow;
						if (map[position.Top][position.Left + 1] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Left++; //right
						}
						break;

						/*The jumping mechanism we added for the third level*/
					case ConsoleKey.Spacebar:
						lastMovementKey = ConsoleKey.UpArrow;
						// Check if jumping space is available
						if (map[position.Top - 2][position.Left] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' '); // Clear current position
							position.Top -= 2; // Jump up 2 spaces
						}
						break;
					case ConsoleKey.Escape:
						return;
				}
				// continue to handle player movement and collision detection
				if (map[position.Top][position.Left] is '@')
				{
					// when the player reaches the goal(@),move to the next level
					frame = 0;
					levelIndex++;
					if (levelIndex >= 4) // the plyer completes all levels 
					{
						// if the player completes all levels,display the win message
						goto YouWin;
					}
					else
					{
						// move the player to the next level and reset the player's position,and clear the console
						position = levels[levelIndex].StartPosition;
						Console.Clear();
						goto NextLevel;
					}
				}
				// if the player collides with a wall(#),reset the player's position and decrease the player's lives
				else if (map[position.Top][position.Left] is '#')
				{
					lastMovementKey = ConsoleKey.UpArrow;
					position = levels[levelIndex].StartPosition; // reset the player's position
					lives--; // decrease the player's lives
					if (lives <= 0) //player's lives are less than or equal to 0
					{
						goto YouLose; // player loses the game
					}
					Console.SetCursorPosition(position.Left, position.Top + mapTop);
					Console.Write(GetPlayerChar());
				}
				else // if the player is on an empty space,display the player's character
				{
					Console.SetCursorPosition(position.Left, position.Top + mapTop);
					Console.Write(GetPlayerChar());
				}
			}
		}
		frame = (frame + 1) % level.Frames.Length; // refresh the frame
	}

	// something when the player presses the escape key,end the game
	if (escape)
	{
		return;
	}

	// the player wins the game
YouWin:
	Console.Clear();
	Console.WriteLine("You Win!");
	Console.WriteLine("Press [enter] to continue...");
	PressEnterToContinue();
	return;

	// the player loses the game
YouLose:
	Console.Clear();
	Console.WriteLine("You Lose!");
	Console.WriteLine("Press [enter] to continue...");
	PressEnterToContinue();
	return;

// Display player character,based on last movement direction
	char GetPlayerChar() =>
		lastMovementKey switch
		{
			ConsoleKey.UpArrow    => '^', // display the player character as '^' when the player moves up
			ConsoleKey.DownArrow  => 'v', // down
			ConsoleKey.LeftArrow  => '<', // left
			ConsoleKey.RightArrow => '>', // right
			_ => throw new NotImplementedException(),
		};

// Display the message "Press [enter] to continue..." and wait for the player to press the enter key,or the Escape key to exit the game
	void PressEnterToContinue()
	{
	GetEnterOrEscape:
		Console.CursorVisible = false; // Hide cursor while waiting
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: break; // continue the game
			case ConsoleKey.Escape: return; // exit the game
			default: goto GetEnterOrEscape; // Loop for valid input
		}
	}
}
// exception handling
catch (Exception e)
{
	exception = e; // Store exception for logging
	throw; // Re-throw the exception
}

// Restores console cursor visibility and displays exit message
finally
{
	Console.CursorVisible = true; // Restore cursor visibility
	Console.Clear(); // Clear screen
	Console.WriteLine(exception?.ToString() ?? "Bound was closed."); // Display either exception message or clean exit message
}

// Partial class definition for line ending regex
partial class Program
{
	// Generate regex for splitting map strings on line endings
	[GeneratedRegex(@"\n|\r\n")]
	private static partial Regex LineEndRegex();
}