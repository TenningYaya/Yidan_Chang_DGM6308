namespace Checkers;

public class Piece
{
	public int X { get; set; } // The x position of the piece

	public int Y { get; set; } // The y position of the piece

	// The position of the piece in notation
	public string NotationPosition 
	{
		get => Board.ToPositionNotationString(X, Y);
		set => (X, Y) = Board.ParsePositionNotation(value);
	}

 	public PieceColor Color { get; init; } // The color of the piece

	public bool Promoted { get; set; } // If the piece is promoted
}
