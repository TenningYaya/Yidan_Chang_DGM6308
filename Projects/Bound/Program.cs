using System;
using System.Text.RegularExpressions;


// kxwsyw,ylhssl
Exception? exception = null;

((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[])[] levels =
[
	#region level 00
	((15, 16),
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
		""", TimeSpan.FromSeconds(.1)),
	}),
	#endregion
    /*
    **********************************************这个LV2改了**********************************
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
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
		""", TimeSpan.FromSeconds(0.3)),
	}),
	#endregion
];

try
{
	bool escape = false;//呆的像鸟一样
	int lives = 100;
	int frame = 0;
	int levelIndex = 0;
	((int Left, int Top) StartPosition, (string Map, TimeSpan Delay)[] Frames) level = levels[levelIndex];
	(int Top, int Left) position = level.StartPosition;
	ConsoleKey lastMovementKey = ConsoleKey.UpArrow;
	Console.CursorVisible = false;
	Console.Clear();
	while (!escape)
	{
	NextLevel:
		level = levels[levelIndex];
		Console.CursorVisible = false;
		Console.SetCursorPosition(0, 0);
		Console.WriteLine();
		Console.WriteLine("  Bound");
		Console.WriteLine();
		Console.WriteLine($"  Lives: {lives}   ");
		Console.WriteLine($"  Level: {levelIndex}");
		if (levelIndex == 3) {
			Console.WriteLine("  Press [space] to jump forward!");
		}
		int mapTop = Console.CursorTop;
		Console.Write(level.Frames[frame].Map);
		string[] map = LineEndRegex().Split(level.Frames[frame].Map);
		if (map[position.Top][position.Left] is '#')
		{
			lastMovementKey = ConsoleKey.UpArrow;
			position = levels[levelIndex].StartPosition;
			lives--;
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
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						lastMovementKey = ConsoleKey.UpArrow;
						if (map[position.Top - 1][position.Left] is ' ' or '@' or '#')
						{
							lastMovementKey = ConsoleKey.UpArrow;
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Top--;
						}
						break;
					case ConsoleKey.DownArrow:
						lastMovementKey = ConsoleKey.DownArrow;
						if (map[position.Top + 1][position.Left] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Top++;
						}
						break;
					case ConsoleKey.LeftArrow:
						lastMovementKey = ConsoleKey.LeftArrow;
						if (map[position.Top][position.Left - 1] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Left--;
						}
						break;
					case ConsoleKey.RightArrow:
						lastMovementKey = ConsoleKey.RightArrow;
						if (map[position.Top][position.Left + 1] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Left++;
						}
						break;
						/*我们加上去的用于第三关的跳跃机制*/
					case ConsoleKey.Spacebar:
						lastMovementKey = ConsoleKey.UpArrow;
						if (map[position.Top - 2][position.Left] is ' ' or '@' or '#')
						{
							Console.SetCursorPosition(position.Left, position.Top + mapTop);
							Console.Write(' ');
							position.Top -= 2;
						}
						break;
					case ConsoleKey.Escape:
						return;
				}
				if (map[position.Top][position.Left] is '@')
				{
					frame = 0;
					levelIndex++;
					if (levelIndex >= 4)
					{
						goto YouWin;
					}
					else
					{
						position = levels[levelIndex].StartPosition;
						Console.Clear();
						goto NextLevel;
					}
				}
				else if (map[position.Top][position.Left] is '#')
				{
					lastMovementKey = ConsoleKey.UpArrow;
					position = levels[levelIndex].StartPosition;
					lives--;
					if (lives <= 0)
					{
						goto YouLose;
					}
					Console.SetCursorPosition(position.Left, position.Top + mapTop);
					Console.Write(GetPlayerChar());
				}
				else
				{
					Console.SetCursorPosition(position.Left, position.Top + mapTop);
					Console.Write(GetPlayerChar());
				}
			}
		}
		frame = (frame + 1) % level.Frames.Length;
	}
	if (escape)
	{
		return;
	}
YouWin:
	Console.Clear();
	Console.WriteLine("You Win!");
	Console.WriteLine("Press [enter] to continue...");
	PressEnterToContinue();
	return;
YouLose:
	Console.Clear();
	Console.WriteLine("You Lose!");
	Console.WriteLine("Press [enter] to continue...");
	PressEnterToContinue();
	return;

	char GetPlayerChar() =>
		lastMovementKey switch
		{
			ConsoleKey.UpArrow    => '^',
			ConsoleKey.DownArrow  => 'v',
			ConsoleKey.LeftArrow  => '<',
			ConsoleKey.RightArrow => '>',
			_ => throw new NotImplementedException(),
		};

	void PressEnterToContinue()
	{
	GetEnterOrEscape:
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: break;
			case ConsoleKey.Escape: return;
			default: goto GetEnterOrEscape;
		}
	}
}
catch (Exception e)
{
	exception = e;
	throw;
}
finally
{
	Console.CursorVisible = true;
	Console.Clear();
	Console.WriteLine(exception?.ToString() ?? "Bound was closed.");
}

partial class Program
{
	[GeneratedRegex(@"\n|\r\n")]
	private static partial Regex LineEndRegex();
}