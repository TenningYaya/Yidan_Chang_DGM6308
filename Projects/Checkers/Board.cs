namespace Checkers;

public class Board
{
	public List<Piece> Pieces { get; } // The pieces on the board

	public Piece? Aggressor { get; set; } // The aggressor of the board

	public Piece? this[int x, int y] =>
		Pieces.FirstOrDefault(piece => piece.X == x && piece.Y == y);

	public Board()
	{
		Aggressor = null;
		// Create all the pieces on the board
		Pieces = new List<Piece>
			{
				new() { NotationPosition ="A3", Color = Black},
				new() { NotationPosition ="A1", Color = Black},
				new() { NotationPosition ="B2", Color = Black},
				new() { NotationPosition ="C3", Color = Black},
				new() { NotationPosition ="C1", Color = Black},
				new() { NotationPosition ="D2", Color = Black},
				new() { NotationPosition ="E3", Color = Black},
				new() { NotationPosition ="E1", Color = Black},
				new() { NotationPosition ="F2", Color = Black},
				new() { NotationPosition ="G3", Color = Black},
				new() { NotationPosition ="G1", Color = Black},
				new() { NotationPosition ="H2", Color = Black},

				new() { NotationPosition ="A7", Color = White},
				new() { NotationPosition ="B8", Color = White},
				new() { NotationPosition ="B6", Color = White},
				new() { NotationPosition ="C7", Color = White},
				new() { NotationPosition ="D8", Color = White},
				new() { NotationPosition ="D6", Color = White},
				new() { NotationPosition ="E7", Color = White},
				new() { NotationPosition ="F8", Color = White},
				new() { NotationPosition ="F6", Color = White},
				new() { NotationPosition ="G7", Color = White},
				new() { NotationPosition ="H8", Color = White},
				new() { NotationPosition ="H6", Color = White}
			};
	}

	// Converts a position to a string in position notation.
	public static string ToPositionNotationString(int x, int y)
	{
		if (!IsValidPosition(x, y)) throw new ArgumentException("Not a valid position!"); // Check if the position is valid and throw an exception if not
		return $"{(char)('A' + x)}{y + 1}"; // Return the position
	}

	// Converts a string notation to position notation.
	public static (int X, int Y) ParsePositionNotation(string notation)
	{
		if (notation is null) throw new ArgumentNullException(nameof(notation)); // Check if the notation is null and throw an exception if it is
		notation = notation.Trim().ToUpper(); // Remove all white spaces and convert the notation to uppercase
		if (notation.Length is not 2 ||
			notation[0] < 'A' || 'H' < notation[0] ||
			notation[1] < '1' || '8' < notation[1]) // Check if the notation is not valid and throw an exception if it is not
			throw new FormatException($@"{nameof(notation)} ""{notation}"" is not valid");
		return (notation[0] - 'A', notation[1] - '1'); // Return the position
	}

	// Check if the position is valid (within the board).
	public static bool IsValidPosition(int x, int y) =>
		0 <= x && x < 8 &&
		0 <= y && y < 8;

	// Get the closest rival pieces.
	public (Piece A, Piece B) GetClosestRivalPieces(PieceColor priorityColor)
	{
		double minDistanceSquared = double.MaxValue; // Set the minimum distance squared to the maximum value of double
		(Piece A, Piece B) closestRivals = (null!, null!); // Create the closest rivals
		// Loop through all the pieces that are the same color as the priority color
		foreach (Piece a in Pieces.Where(piece => piece.Color == priorityColor))
		{
			// Loop through all the pieces that are not the same color as the priority color
			foreach (Piece b in Pieces.Where(piece => piece.Color != priorityColor))
			{
				(int X, int Y) vector = (a.X - b.X, a.Y - b.Y); // Calculate the vector
				double distanceSquared = vector.X * vector.X + vector.Y * vector.Y; // Calculate the distance squared
				// Check if the distance squared is less than the minimum distance squared
				if (distanceSquared < minDistanceSquared)
				{
					minDistanceSquared = distanceSquared; // Set the minimum distance squared to the distance squared
					closestRivals = (a, b); // Set the closest rivals to the current pieces
				}
			}
		}
		return closestRivals; // Return the closest rivals
	}

	// Get all the possible moves for one color of pieces.
	public List<Move> GetPossibleMoves(PieceColor color)
	{
		List<Move> moves = new(); // Create a list of moves
		// Check if the aggressor is not null
		if (Aggressor is not null)
		{
			// If the aggressor color is not the same as the color throw an exception
			if (Aggressor.Color != color)
			{
				throw new Exception($"{nameof(Aggressor)} is not null && {nameof(Aggressor)}.{nameof(Aggressor.Color)} != {nameof(color)}");
			}
			moves.AddRange(GetPossibleMoves(Aggressor).Where(move => move.PieceToCapture is not null)); // Add all the possible moves to the list of moves
		}
		else
		{
			// Loop through all the pieces that are the same color as the color
			foreach (Piece piece in Pieces.Where(piece => piece.Color == color))
			{
				moves.AddRange(GetPossibleMoves(piece)); // Add all the possible moves to the list of moves
			}
		}
		return moves.Any(move => move.PieceToCapture is not null)
			? moves.Where(move => move.PieceToCapture is not null).ToList()
			: moves; // Return the list of moves
	}

	// Get all the possible moves for a piece.
	public List<Move> GetPossibleMoves(Piece piece)
	{
		List<Move> moves = new(); // Create a list of moves
		// All the possible moves for a piece
		ValidateDiagonalMove(-1, -1);
		ValidateDiagonalMove(-1,  1);
		ValidateDiagonalMove( 1, -1);
		ValidateDiagonalMove( 1,  1);
		return moves.Any(move => move.PieceToCapture is not null)
			? moves.Where(move => move.PieceToCapture is not null).ToList()
			: moves; // Return the list of moves

		// Validate a diagonal move.
		void ValidateDiagonalMove(int dx, int dy)
		{
			if (!piece.Promoted && piece.Color is Black && dy is -1) return; // Check if the piece is not promoted and the color is black and the dy is -1
			if (!piece.Promoted && piece.Color is White && dy is 1) return; // Check if the piece is not promoted and the color is white and the dy is 1
			(int X, int Y) target = (piece.X + dx, piece.Y + dy); // Calculate the target position
			// If the position is not valid return
			if (!IsValidPosition(target.X, target.Y)) return;
			PieceColor? targetColor = this[target.X, target.Y]?.Color; // Get the target color
			if (targetColor is null) // If the target color is null
			{
				if (!IsValidPosition(target.X, target.Y)) return; // If the position is not valid return
				Move newMove = new(piece, target); // Create a new move
				moves.Add(newMove); // Add the move to the list of moves
			}
			else if (targetColor != piece.Color) // If the target color is not the same as the piece color
			{
				(int X, int Y) jump = (piece.X + 2 * dx, piece.Y + 2 * dy); // Calculate the jump position
				if (!IsValidPosition(jump.X, jump.Y)) return; // If the position is not valid return
				PieceColor? jumpColor = this[jump.X, jump.Y]?.Color; // Get the jump color
				if (jumpColor is not null) return; // If the jump color is not null return
				Move attack = new(piece, jump, this[target.X, target.Y]); // Create a new attack move
				moves.Add(attack); // Add the attack move to the list of moves
			}
		}
	}

	/// <summary>Returns a <see cref="Move"/> if <paramref name="from"/>-&gt;<paramref name="to"/> is valid or null if not.</summary>
	public Move? ValidateMove(PieceColor color, (int X, int Y) from, (int X, int Y) to)
	{
		Piece? piece = this[from.X, from.Y]; // Get the piece
		if (piece is null) // Return if the piece is null
		{
			return null;
		}
		// Loop through all the possible moves
		foreach (Move move in GetPossibleMoves(color))
		{
			// Check if the position of piece to move is the same as from and the move.To is the same as to
			if ((move.PieceToMove.X, move.PieceToMove.Y) == from && move.To == to)
			{
				return move; 
			}
		}
		return null;
	}

	// Method to check if a move is towards a piece.
	public static bool IsTowards(Move move, Piece piece)
	{
		(int Dx, int Dy) a = (move.PieceToMove.X - piece.X, move.PieceToMove.Y - piece.Y); // Calculate the vector a 
		int a_distanceSquared = a.Dx * a.Dx + a.Dy * a.Dy; // Calculate the distance squared of a
		(int Dx, int Dy) b = (move.To.X - piece.X, move.To.Y - piece.Y); // Calculate the vector b
		int b_distanceSquared = b.Dx * b.Dx + b.Dy * b.Dy; // Calculate the distance squared of b
		return b_distanceSquared < a_distanceSquared; // Return if b is less than a
	}
}
