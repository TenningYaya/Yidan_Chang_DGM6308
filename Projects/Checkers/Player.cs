﻿namespace Checkers;

public class Player
{
	public bool IsHuman { get; } // if the player is human
	public PieceColor Color { get; } // the color of the player

	public int count;
	// Constructor for the player
	public Player(bool isHuman, PieceColor color) 
	{
		IsHuman = isHuman;
		Color = color;
		count = 20;
	}
}
