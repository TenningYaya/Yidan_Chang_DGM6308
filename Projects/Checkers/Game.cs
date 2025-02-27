namespace Checkers;

public class Game
{
	private const int PiecesPerColor = 12; // The number of pieces per color

	public PieceColor Turn { get; private set; } // The turn of the player
	public Board Board { get; } // The board of the game
	public PieceColor? Winner { get; private set; } // The winner of the game
	public List<Player> Players { get; } // The players of the game

	// Constructor for the game
	public Game(int humanPlayerCount)
	{
		// Check if the human player count is less than 0 or greater than 2
		if (humanPlayerCount < 0 || 2 < humanPlayerCount) throw new ArgumentOutOfRangeException(nameof(humanPlayerCount));
		Board = new Board();
		Players = new()
		{
			new Player(humanPlayerCount >= 1, Black), // Create a new player with the human player count and the color black
			new Player(humanPlayerCount >= 2, White), // Create a new player with the human player count and the color white
		};
		Turn = Black; // Set the turn to black player
		Winner = null;
	}

	// Perform a move
	public void PerformMove(Move move, Traps trap)
	{
		(move.PieceToMove.X, move.PieceToMove.Y) = move.To; // Set the piece to move to the position
		if ((move.PieceToMove.Color is Black && move.To.Y is 7) ||
			(move.PieceToMove.Color is White && move.To.Y is 0)) // Check if the piece is at the end of the board
		{
			move.PieceToMove.Promoted = true;
		}
		if (move.PieceToCapture is not null) // Check if the piece to capture is not null
		{
			Board.Pieces.Remove(move.PieceToCapture); // Remove the piece to capture from the board
		}
		if (move.PieceToCapture is not null &&
			Board.GetPossibleMoves(move.PieceToMove).Any(m => m.PieceToCapture is not null)) // Check if the piece to capture is not null and there are possible moves
		{
			Board.Aggressor = move.PieceToMove; // Set the aggressor to the piece to move
		}
		else
		{
			Board.Aggressor = null; 
			Turn = Turn is Black ? White : Black; // Set the turn to the opposite player
		}

		if(move.To == trap.currentTrapPosition) // Check if the move is on the trap
		{
			Board.Pieces.Remove(move.PieceToMove); // Remove the piece to move from the board
		}
		CheckForWinner(); // Check for the winner
	}

	// Check for the winner
	public void CheckForWinner()
	{
		if (!Board.Pieces.Any(piece => piece.Color is Black)) // Check if there are no black pieces
		{
			Winner = White; // Set the winner to white
		}
		if (!Board.Pieces.Any(piece => piece.Color is White)) // Check if there are no white pieces
		{
			Winner = Black; // Set the winner to black
		}
		if (Winner is null && Board.GetPossibleMoves(Turn).Count is 0) // Check if there is no winner and there are no possible moves
		{
			Winner = Turn is Black ? White : Black; // Set the winner to the opposite player
		}
	}

	// Get the count of the taken pieces
	public int TakenCount(PieceColor colour) =>
		PiecesPerColor - Board.Pieces.Count(piece => piece.Color == colour);

	public void Undo(Piece lastPiece){
		Board.Pieces.Remove(Board.Pieces.Last());
		Board.Pieces.Add(lastPiece);
		Turn = Turn is Black ? White : Black;
	}

}
 