namespace Checkers;

public class Move
{
	public Piece PieceToMove { get; set; } // The piece to move around

	public (int X, int Y) To { get; set; } // The position to move the piece to

	public Piece? PieceToCapture { get; set; } // The piece to capture

	// Constructor for the move
	public Move(Piece pieceToMove, (int X, int Y) to, Piece? pieceToCapture = null)
	{
		PieceToMove = pieceToMove;
		To = to;
		PieceToCapture = pieceToCapture;
	}
}
