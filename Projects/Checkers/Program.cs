Exception? exception = null; // Handle exceptions

Encoding encoding = Console.OutputEncoding; // Set encoding instance
Traps trap = new(); // Create a new trap instance

try
{
	Console.OutputEncoding = Encoding.UTF8; // Set encoding to UTF-8
	Game game = ShowIntroScreenAndGetOption(); // Create game and show intro screen
	Console.Clear();
	RunGameLoop(game); // Run main game loop
	RenderGameState(game, promptPressKey: true);  // Render game state
	Console.ReadKey(true);
}
// Handle exceptions
catch (Exception e)
{
	exception = e;
	throw;
}
// Close the game and clear the console
finally
{
	Console.OutputEncoding = encoding; // Set the encoding to the default encoding
	Console.CursorVisible = true; 
	Console.Clear();
	Console.WriteLine(exception?.ToString() ?? "Checkers was closed."); // Tell the user the game is closed 
}

// Method to show the intro screen and get the option
Game ShowIntroScreenAndGetOption()
{
	//write all the instructions 
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Checkers");
	Console.WriteLine();
	Console.WriteLine("  Checkers is played on an 8x8 board between two sides commonly known as black");
	Console.WriteLine("  and white. The objective is simple - capture all your opponent's pieces. An");
	Console.WriteLine("  alternative way to win is to trap your opponent so that they have no valid");
	Console.WriteLine("  moves left.");
	Console.WriteLine();
	Console.WriteLine("  Black starts first and players take it in turns to move their pieces forward");
	Console.WriteLine("  across the board diagonally. Should a piece reach the other side of the board");
	Console.WriteLine("  the piece becomes a king and can then move diagonally backwards as well as");
	Console.WriteLine("  forwards.");
	Console.WriteLine();
	Console.WriteLine("  Pieces are captured by jumping over them diagonally. More than one enemy piece");
	Console.WriteLine("  can be captured in the same turn by the same piece. If you can capture a piece");
	Console.WriteLine("  you must capture a piece.");
	Console.WriteLine();
	Console.WriteLine("  Moves are selected with the arrow keys. Use the [enter] button to select the");
	Console.WriteLine("  from and to squares. Invalid moves are ignored.");
	Console.WriteLine();
	Console.WriteLine("   Steping on traps will cause this pieces to be removed.");
	Console.WriteLine();
	Console.WriteLine("  Press a number key to choose number of human players:");
	Console.WriteLine("    [0] Black (computer) vs White (computer)");
	Console.WriteLine("    [1] Black (human) vs White (computer)");
	Console.Write("    [2] Black (human) vs White (human)");

	int? humanPlayerCount = null;
	// Loop until the user presses a valid key to select the number of human players
	while (humanPlayerCount is null)
	{
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.D0 or ConsoleKey.NumPad0: humanPlayerCount = 0; break;
			case ConsoleKey.D1 or ConsoleKey.NumPad1: humanPlayerCount = 1; break;
			case ConsoleKey.D2 or ConsoleKey.NumPad2: humanPlayerCount = 2; break;
		}
	}
	return new Game(humanPlayerCount.Value); // Return a new game instance with the number of human players
}

// Method to run the main game loop
void RunGameLoop(Game game)
{
	// Loop until there is a winner
	while (game.Winner is null)
	{
		trap.currentTrapPosition = trap.GetRandomTrapPosition(); // Get a random trap position

		// Get the current player based on the turn
		Player currentPlayer = game.Players.First(player => player.Color == game.Turn); //If the player's color is equal to the game's turn, set the current player to that player
		// If the current player is a human player 
		if (currentPlayer.IsHuman)
		{
			// Loop until the game's turn is equal to the current player's color
			while (game.Turn == currentPlayer.Color)
			{
				(int X, int Y)? selectionStart = null; // Set the start of selection to null
				(int X, int Y)? from = game.Board.Aggressor is not null ? (game.Board.Aggressor.X, game.Board.Aggressor.Y) : null; // Set the from position to the aggressor's position if there is an aggressor
				List<Move> moves = game.Board.GetPossibleMoves(game.Turn); // Get all possible moves
				// If there is only one piece that can move
				if (moves.Select(move => move.PieceToMove).Distinct().Count() is 1)
				{
					Move must = moves.First(); // Set the must move to the first move
					from = (must.PieceToMove.X, must.PieceToMove.Y); // Set the from position to the must's position
					selectionStart = must.To; 
				}
				// Get the from and to positions
				while (from is null)
				{
					from = HumanMoveSelection(game); // Get the from position from the human player
					selectionStart = from; // Set the selection start to the from position
				}
				(int X, int Y)? to = HumanMoveSelection(game, selectionStart: selectionStart, from: from); // Get the to position from the human player
				Piece? piece = null; // Set the piece to null
				piece = game.Board[from.Value.X, from.Value.Y]; // Set the piece to the from position
				// If the piece is null or the piece's color is not equal to the game's turn
				if (piece is null || piece.Color != game.Turn)
				{
					// Set the from and to positions to null
					from = null;
					to = null;
				}
				// If the from and to positions are valid
				if (from is not null && to is not null)
				{
					// Validate the move
					Move? move = game.Board.ValidateMove(game.Turn, from.Value, to.Value); 
					if (move is not null &&
						(game.Board.Aggressor is null || move.PieceToMove == game.Board.Aggressor)) // If the move is valid and the piece to move is equal to the aggressor or there is no aggressor
					{
						game.PerformMove(move, trap); // Perform the move
					}
					if(to == trap.currentTrapPosition){

					}
				}
			}
		}
		// If the current player is a computer player
		else
		{
			List<Move> moves = game.Board.GetPossibleMoves(game.Turn); // Get all possible moves
			List<Move> captures = moves.Where(move => move.PieceToCapture is not null).ToList(); // Get all possible captures
			// If there are captures
			if (captures.Count > 0)
			{
				game.PerformMove(captures[Random.Shared.Next(captures.Count)], trap); // Perform a random capture
			}
			// If there are no captures and there are no pieces that can be promoted
			else if (!game.Board.Pieces.Any(piece => piece.Color == game.Turn && !piece.Promoted))
			{
				var (a, b) = game.Board.GetClosestRivalPieces(game.Turn); // Get the closest rival pieces
				Move? priorityMove = moves.FirstOrDefault(move => move.PieceToMove == a && Board.IsTowards(move, b)); // Get the priority move
				game.PerformMove(priorityMove ?? moves[Random.Shared.Next(moves.Count)], trap); // Perform the priority move or a random move
			}
			else
			{
				game.PerformMove(moves[Random.Shared.Next(moves.Count)], trap); // Perform a random move
			}
		}
	    // Render the game state
		RenderGameState(game, playerMoved: currentPlayer, promptPressKey: true);
		Console.ReadKey(true);
	}
}

// Method to render the game state
void RenderGameState(Game game, Player? playerMoved = null, (int X, int Y)? selection = null, (int X, int Y)? from = null, bool promptPressKey = false)
{
	// The characters for different pieces
	const char BlackPiece = '○';
	const char BlackKing  = '☺';
	const char WhitePiece = '◙';
	const char WhiteKing  = '☻';
	const char Vacant     = '·';
	const char Trap       = 'X';

	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	// All the Render, using StringBuilder
	StringBuilder sb = new();
	sb.AppendLine();
	sb.AppendLine("  Checkers");
	sb.AppendLine();
	sb.AppendLine($"    ╔═══════════════════╗");
	sb.AppendLine($"  8 ║  {B(0, 7)} {B(1, 7)} {B(2, 7)} {B(3, 7)} {B(4, 7)} {B(5, 7)} {B(6, 7)} {B(7, 7)}  ║ {BlackPiece} = Black");
	sb.AppendLine($"  7 ║  {B(0, 6)} {B(1, 6)} {B(2, 6)} {B(3, 6)} {B(4, 6)} {B(5, 6)} {B(6, 6)} {B(7, 6)}  ║ {BlackKing} = Black King");
	sb.AppendLine($"  6 ║  {B(0, 5)} {B(1, 5)} {B(2, 5)} {B(3, 5)} {B(4, 5)} {B(5, 5)} {B(6, 5)} {B(7, 5)}  ║ {WhitePiece} = White");
	sb.AppendLine($"  5 ║  {B(0, 4)} {B(1, 4)} {B(2, 4)} {B(3, 4)} {B(4, 4)} {B(5, 4)} {B(6, 4)} {B(7, 4)}  ║ {WhiteKing} = White King");
	sb.AppendLine($"  4 ║  {B(0, 3)} {B(1, 3)} {B(2, 3)} {B(3, 3)} {B(4, 3)} {B(5, 3)} {B(6, 3)} {B(7, 3)}  ║ {Trap} = Trap");
	sb.AppendLine($"  3 ║  {B(0, 2)} {B(1, 2)} {B(2, 2)} {B(3, 2)} {B(4, 2)} {B(5, 2)} {B(6, 2)} {B(7, 2)}  ║ Taken:");
	sb.AppendLine($"  2 ║  {B(0, 1)} {B(1, 1)} {B(2, 1)} {B(3, 1)} {B(4, 1)} {B(5, 1)} {B(6, 1)} {B(7, 1)}  ║ {game.TakenCount(White),2} x {WhitePiece}");
	sb.AppendLine($"  1 ║  {B(0, 0)} {B(1, 0)} {B(2, 0)} {B(3, 0)} {B(4, 0)} {B(5, 0)} {B(6, 0)} {B(7, 0)}  ║ {game.TakenCount(Black),2} x {BlackPiece}");
	sb.AppendLine($"    ╚═══════════════════╝");
	sb.AppendLine($"       A B C D E F G H");
	sb.AppendLine();
	if (selection is not null)
	{
		sb.Replace(" $ ", $"[{ToChar(game.Board[selection.Value.X, selection.Value.Y])}]"); // Replace the selection with the piece at the selection position
	}
	// if(trap is not null){
	// 	sb.Replace(" X ", $"[{ToChar(game.Board[trap.currentTrapPosition.X, trap.currentTrapPosition.Y])}]");
	// }
	if (from is not null)
	{
		char fromChar = ToChar(game.Board[from.Value.X, from.Value.Y]); // Get the piece at the from position
		sb.Replace(" @ ", $"<{fromChar}>"); // Replace the from position with the from character
		sb.Replace("@ ",  $"{fromChar}>");
		sb.Replace(" @",  $"<{fromChar}");
	}
	PieceColor? wc = game.Winner; // Get the winner
	PieceColor? mc = playerMoved?.Color; // Get the player that moved
	PieceColor? tc = game.Turn; // Get the current turn
	// Note: these strings need to match in length
	// so they overwrite each other.
	// Print Current State
	string w = $"  *** {wc} wins ***";
	string m = $"  {mc} moved       ";
	string t = $"  {tc}'s turn      ";
	sb.AppendLine(
		game.Winner is not null ? w :
		playerMoved is not null ? m :
		t);
	string p = "  Press any key to continue...";
	string s = "                              ";
	sb.AppendLine(promptPressKey ? p : s);
	Console.Write(sb);

	// Method to get the character at a position
	char B(int x, int y) =>
		(x, y) == selection ? '$' :
		(x, y) == from ? '@' :
		(x, y) == trap.currentTrapPosition ? 'X':
		ToChar(game.Board[x, y]); 

	// Method to convert piece to character
	static char ToChar(Piece? piece) =>
		piece is null ? Vacant : // If no piece, The character is Vacant
		(piece.Color, piece.Promoted) switch
		{
			//Set different kinds of pieces
			(Black, false) => BlackPiece, 
			(Black, true)  => BlackKing,
			(White, false) => WhitePiece,
			(White, true)  => WhiteKing,
			_ => throw new NotImplementedException(),
		};
}

// Methods for human to select moving pieces to position
(int X, int Y)? HumanMoveSelection(Game game, (int X, int y)? selectionStart = null, (int X, int Y)? from = null)
{
	(int X, int Y) selection = selectionStart ?? (3, 3); // Selection position always start at 3, 3
	while (true)
	{
		RenderGameState(game, selection: selection, from: from); //render the game state
		switch (Console.ReadKey(true).Key)
		{
			//using up, down, left, right to move, enter to select, escape to cancel 
			case ConsoleKey.DownArrow:  selection.Y = Math.Max(0, selection.Y - 1); break;
			case ConsoleKey.UpArrow:    selection.Y = Math.Min(7, selection.Y + 1); break;
			case ConsoleKey.LeftArrow:  selection.X = Math.Max(0, selection.X - 1); break;
			case ConsoleKey.RightArrow: selection.X = Math.Min(7, selection.X + 1); break;
			case ConsoleKey.Enter:      return selection;
			case ConsoleKey.Escape:     return null;
		}
	}
}
