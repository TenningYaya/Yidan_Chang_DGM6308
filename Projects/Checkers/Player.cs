namespace Checkers;

public class Player
{
	public bool IsHuman { get; } // if the player is human
	public PieceColor Color { get; } // the color of the player
	public bool extraTurnUsed; // if the player has an extra turn
	public Piece LastPieceMoved; // the last piece moved by the player
	public int count;
	// Constructor for the player
	public Player(bool isHuman, PieceColor color) 
	{
		IsHuman = isHuman;
		Color = color;
		count = 20;
		extraTurnUsed = false;
	}
}
