using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
List<Card> deck = new List<Card>();
List<Card> discardPile = new();
Card playerHand = new();
Card dealerHand = new();
int playerScore = 0;
int ComputerScore = 0;

try{
    	foreach (Suit suit in Enum.GetValues<Suit>())
	{
		foreach (Value value in Enum.GetValues<Value>())
		{
			deck.Add(new()
			{
				Suit = suit,
				Value = value,
			});
		}
	}
    restart:
    Shuffle(deck);
    while(deck.Count > 0){
        start:
        Console.Clear();
        Console.WriteLine("Press Enter to draw a card(escape to quit):");
        Console.WriteLine(deck.Count + " cards left in the deck");
    	switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter:
                playerHand = deck[^1];
                deck.RemoveAt(deck.Count - 1);
                discardPile.Add(playerHand);
                Console.WriteLine("You draw :");
                for(int i = 0; i < Card.RenderHeight; i++){
                    string s = playerHand.Render()[i];
                    Console.Write(s);
			        Console.WriteLine();
                }
                break;
            case ConsoleKey.Escape:
                return;
            default:
                Console.WriteLine("Press Enter only.");
                goto start;
        }
        Console.WriteLine("Dealer Draw a card:");
                dealerHand = deck[^1];
                deck.RemoveAt(deck.Count - 1);
                discardPile.Add(dealerHand);
                Console.WriteLine("Dealer draw " + dealerHand.Suit +" " + dealerHand.Value);
                for(int i = 0; i < Card.RenderHeight; i++){
                    string d = dealerHand.Render()[i];
                    Console.Write(d);
			        Console.WriteLine();
                }
        if(playerHand.Value > dealerHand.Value){
            Console.WriteLine("You win");
            playerScore++;
        }
        else if(playerHand.Value < dealerHand.Value){
            Console.WriteLine("You lose");
            ComputerScore++;
        }
        else{
            Console.WriteLine("Draw");
        }
        Console.WriteLine("Your score: "+ playerScore + " Dealer score: " + ComputerScore);
        Console.WriteLine("Press any key to Continue");
        Console.ReadKey();
    }
    if(deck.Count == 0){
            Shuffle(discardPile); 
            deck = discardPile;
        }
    Console.WriteLine("Game Over, all cards are used up.");
    Console.WriteLine("If you want to play again press Enter, if you want to quit press Escape");
    askforinput:
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.Enter:
            goto restart;
        case ConsoleKey.Escape:
            return;
        default:
            Console.WriteLine("Press Enter or Escape only.");
            goto askforinput;
    }
}
finally{
    Console.WriteLine("Game End");
}

void Shuffle(List<Card> cards)
{
	for (int i = 0; i < cards.Count; i++)
	{
		int swap = Random.Shared.Next(cards.Count);
		(cards[i], cards[swap]) = (cards[swap], cards[i]);
	}
}
class Card {
	public Suit Suit;
	public Value Value;
    public const int RenderHeight = 7;
    public string[] Render()
    {
        char suit = Suit.ToString()[0];
        		string value = Value switch
		{
			Value.Ace   =>  "A",
			Value.Ten   => "10",
			Value.Jack  =>  "J",
			Value.Queen =>  "Q",
			Value.King  =>  "K",
			_ => ((int)Value).ToString(CultureInfo.InvariantCulture),
		};
		string card = $"{value}{suit}";
		string a = card.Length < 3 ? $"{card} " : card;
		string b = card.Length < 3 ? $" {card}" : card;
		return
		[
			$"┌───────┐",
			$"│{a}    │",
			$"│       │",
			$"│       │",
			$"│       │",
			$"│    {b}│",
			$"└───────┘",
		];
    }
}
enum Suit
{
	Hearts,
	Clubs,
	Spades,
	Diamonds,
}
enum Value
{
	Ace   = 14,
	Two   = 02,
	Three = 03,
	Four  = 04,
	Five  = 05,
	Six   = 06,
	Seven = 07,
	Eight = 08,
	Nine  = 09,
	Ten   = 10,
	Jack  = 11,
	Queen = 12,
	King  = 13,
}
