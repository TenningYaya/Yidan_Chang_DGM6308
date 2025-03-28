﻿using System;
using System.Collections.Generic;
List<Card> deck = new List<Card>();//build a new list to store all the cards
List<Card> discardPile = new();//a new list to store all the discarded cards
List<Card> ability = new();//ability card
List<Card> discardAbility = new();//a new list to store all the discarded cards
Card player1Hand = new();//player1’s card
int player1Score = 0;
int player2Score = 0;
int player1HandValue = 0;
int player2HandValue = 0;
Card player2Hand = new();//player1’s card
Card player1Ability = new();//player’s ability card
Card player2Ability = new();
bool currentPlayer = true;
State state = State.Intro;
int player = 0;
bool useAbility = false;
bool chooseAbility = false;

try
{
    Initialize();
    while (!(state is State.End))
    {
        Render();
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Enter:
                if (state is State.Intro)
                {
                    state = State.PlayerNum;
                }
                else if (state is State.PlayerNum && (player == 1 || player == 2))
                {
                    state = State.PlayerTurn;
                }
                else if (state is State.PlayerTurn && player == 1)
                {
                    state = State.ComputerTurn;
                }
                else if (state is State.PlayerTurn && player == 2)
                {
                    state = State.PlayerTurn;
                }
                else if (state is State.ComputerTurn)
                {
                    state = State.PlayerTurn;
                }
                break;

            case ConsoleKey.D1 or ConsoleKey.NumPad1:
                if (state is State.PlayerNum)
                {
                    player = 1;
                }
                if (state is State.PlayerTurn)
                {
                    useAbility = true;
                    chooseAbility = true;
                }
                break;
            case ConsoleKey.D2 or ConsoleKey.NumPad2:
                if (state is State.PlayerNum)
                {
                    player = 2;
                }
                if (state is State.PlayerTurn)
                {
                    useAbility = false;
                    chooseAbility = true;
                }

                break;
            case ConsoleKey.Escape:
                state = State.End;
                break;
        }
    }
    Render();
GetEnterOrEscape:
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.Enter: return;
        case ConsoleKey.Escape: return;
        default: goto GetEnterOrEscape;
    }
}
catch (Exception e)
{
    exception = e;
    throw;
}
//         start:
//         Console.Clear();
//         Console.WriteLine("Press Enter to draw a card(escape to quit):");
//         Console.WriteLine(deck.Count + "cards left in the deck");//render the hint
//         switch (Console.ReadKey(true).Key)
//         {
//             case ConsoleKey.Enter:
//                 playerHand = deck[^1];//get the last variable in the list as player’s hand
//                 deck.RemoveAt(deck.Count - 1);//remove it from the list
//                 discardPile.Add(playerHand);//add it into the discard pile
//                 Console.WriteLine("You draw " + playerHand.Suit + " " + playerHand.Value);//show the player’s hand
//                 break;
//             case ConsoleKey.Escape:
//                 return;
//             default:
//                 Console.WriteLine("Press Enter only.");
//                 goto start;
//         }
//         Console.WriteLine("Dealer Draw a card:");//do the same for dealer
//                 dealerHand = deck[^1];
//                 deck.RemoveAt(deck.Count - 1);
//                 Console.WriteLine("Dealer draw " + dealerHand.Suit +" " + dealerHand.Value);
// //compare hands for both player
//         if(playerHand.Value > dealerHand.Value){
//             Console.WriteLine("You win");
//         }
//         else if(playerHand.Value < dealerHand.Value){
//             Console.WriteLine("You lose");
//         }
//         else{
//             Console.WriteLine("Draw");
//         }
//         if(deck.Count == 0){//shuffle the card after the deck pile was run out
//             Shuffle(discardPile);
//             deck = discardPile;
//         }
//         Console.WriteLine("Press any key to Continue");
//         Console.ReadKey();
//     }
finally
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine("Game End");
}

void Render()
{
    Console.CursorVisible = false;
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("  Card Fight");
    Console.WriteLine();
    if (state is State.Intro)
    {
        Console.WriteLine("  This is the War card game. It is played with 40 regular cards and  10");
        Console.WriteLine("  ability cards. ");
        Console.WriteLine();
        Console.WriteLine("  Press [escape] to close the game at any time.");
        Console.WriteLine();
        Console.WriteLine("  Press [enter] to continue...");
        return;
    }
    Console.WriteLine($"  Cards In Playing Deck: {deck.Count}");
    Console.WriteLine($"  Cards In Discard Pile: {discardPile.Count}");
    Console.WriteLine($"  Ability Cards In Deck: {ability.Count}");
    Console.WriteLine($"  Discard Ability Pile: {discardAbility.Count}");
    Console.WriteLine();
    if (state is State.PlayerNum)
    {
        Console.WriteLine("  Choose the number of players:");
        Console.WriteLine();
        Console.WriteLine("  [1] One Player");
        Console.WriteLine("  [2] Two Players");
        Console.WriteLine();
        Console.WriteLine("  Press [1] or [2] to select the number of players...");
        return;
    }
    if (state is State.PlayerTurn)
    {
        Console.WriteLine();
        Console.WriteLine($"  Your Hand:");
        if (currentPlayer)
        {
            for (int i = 0; i < Card.RenderHeight; i++)
            {
                string d = player1Hand.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            Console.WriteLine($"    Your Ability Card:");
            for (int i = 0; i < Card.RenderHeight; i++)
            {
                string d = player1Ability.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            if (player1Ability.AbilitySuit is AbilitySuit.Red && player1Ability.AbilityValue is AbilityValue.Joker)
            {
                Console.WriteLine($"    Card Effects: Swap hands with the other player");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Black && player1Ability.AbilityValue is AbilityValue.Joker)
            {
                Console.WriteLine($"    Card Effects: You win this round");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Hearts && player1Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 1");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Spades && player1Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 2");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Clubs && player1Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 3");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Diamonds && player1Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 4");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Hearts && player1Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 1");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Spades && player1Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 2");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Clubs && player1Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 3");
            }
            else if (player1Ability.AbilitySuit is AbilitySuit.Diamonds && player1Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 4");
            }
            Console.WriteLine();
            Console.WriteLine("  Press 1 for using the ability card press 2 for not using the ability card.");
            Console.WriteLine();
            player1HandValue = CalculateScore(player1Hand, player1Ability, useAbility);
            Console.WriteLine("  Your score is" + player1HandValue);
        }
        else{
            for (int i = 0; i < Card.RenderHeight; i++)
            {
                string d = player2Hand.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            Console.WriteLine($"    Your Ability Card:");
            for (int i = 0; i < Card.RenderHeight; i++)
            {
                string d = player2Ability.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            if (player2Ability.AbilitySuit is AbilitySuit.Red && player2Ability.AbilityValue is AbilityValue.Joker)
            {
                Console.WriteLine($"    Card Effects: Swap hands with the other player");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Black && player2Ability.AbilityValue is AbilityValue.Joker)
            {
                Console.WriteLine($"    Card Effects: You win this round");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Hearts && player2Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 1");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Spades && player2Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 2");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Clubs && player2Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 3");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Diamonds && player2Ability.AbilityValue is AbilityValue.King)
            {
                Console.WriteLine($"    Card Effects: Your hand value plus 4");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Hearts && player2Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 1");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Spades && player2Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 2");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Clubs && player2Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 3");
            }
            else if (player2Ability.AbilitySuit is AbilitySuit.Diamonds && player2Ability.AbilityValue is AbilityValue.Queen)
            {
                Console.WriteLine($"    Card Effects: Your hand value minus 4");
            }
            Console.WriteLine();
            while(!chooseAbility){
                Console.WriteLine("  Press 1 for using the ability card press 2 for not using the ability card.");
                Console.WriteLine();
            }
            player2HandValue = CalculateScore(player2Hand, player2Ability, useAbility);
            Console.WriteLine("  Your score is" + player2HandValue);

        }
        Console.WriteLine();
        Console.WriteLine("  press [Enter] to continue...");
    }
    if (state is State.ComputerTurn)
    {
        Console.WriteLine();
        Console.WriteLine($"  The other player's Hand:");
        for (int i = 0; i < Card.RenderHeight; i++)
        {
            string d = dealerHand.Render()[i];
            Console.Write(d);
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine("  Press [Enter] to continue...");
    }
    if (state is State.Draw)
    {
        Console.WriteLine();
        Console.WriteLine($"  Player1's Hand value is: {player1Score}");
        Console.WriteLine($"  Player2's Hand value is: {player2Score}");
        Console.WriteLine("  Draw! No one wins.");
        Console.WriteLine($"  Player1's Total Score: {player1Score}");
        Console.WriteLine($"  Player2's Total Score: {player2Score}");
        Console.WriteLine("  Press [Enter] to continue...");
        chooseAbility = false;
        currentPlayer = true;
    }
    if (state is State.Player1Win)
    {
        Console.WriteLine();
        Console.WriteLine($"  Player1's Hand value is: {player1Score}");
        Console.WriteLine($"  Player2's Hand value is: {player2Score}");
        Console.WriteLine("  Player1 wins!");
        Console.WriteLine($"  Player1's Total Score: {player1Score}");
        Console.WriteLine($"  Player2's Total Score: {player2Score}");
        Console.WriteLine("  Press [Enter] to continue...");
        chooseAbility = false;
        currentPlayer = true;
    }
    if (state is State.Player2Win)
    {
        Console.WriteLine();
        Console.WriteLine($"  Player1's Hand value is: {player1Score}");
        Console.WriteLine($"  Player2's Hand value is: {player2Score}");
        Console.WriteLine("  Player2 wins!");
        Console.WriteLine($"  Player1's Total Score: {player1Score}");
        Console.WriteLine($"  Player2's Total Score: {player2Score}");
        Console.WriteLine("  Press [Enter] to continue...");
        chooseAbility = false;
        currentPlayer = true;
    }
}

void Initialize()
{

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
    }//add 44 cards with 4 different patterns combined with 11 numbers

    foreach (AbilitySuit suit in Enum.GetValues<AbilitySuit>())
    {
        foreach (AbilityValue value in Enum.GetValues<AbilityValue>())
        {
            if (suit is AbilitySuit.Red && value is AbilityValue.Joker)
            {
                ability.Add(new()
                {
                    AbilitySuit = suit,
                    AbilityValue = value,
                });
            }
            if ((suit is AbilitySuit.Clubs || suit is AbilitySuit.Diamonds || suit is AbilitySuit.Hearts || suit is AbilitySuit.Spades) && (value is AbilityValue.Queen || value is AbilityValue.King))
            {
                ability.Add(new()
                {
                    AbilitySuit = suit,
                    AbilityValue = value,
                });
            }

        }
    }//add 8 ability cards with 2 different patterns combined with 4 numbers
    Shuffle(deck);
    Shuffle(ability);
}
void Shuffle(List<Card> cards)
{//method to shuffle the card
    for (int i = 0; i < cards.Count; i++)
    {
        int swap = Random.Shared.Next(cards.Count);
        (cards[i], cards[swap]) = (cards[swap], cards[i]);
    }
}

int CalculateScore(Card Hand, Card Ability, bool use){
    int score = Hand.Value;
    if(Hand.Suit is Suit.Hearts){
        score -= 1;
    }
    else if(Hand.Suit is Suit.Spades){
        score -= 2;
    }
    else if(Hand.Suit is Suit.Clubs){
        score += 1;
    }
    else if(Hand.Suit is Suit.Diamonds){
        score += 2;
    }

    if(use){
        if(Ability.AbilitySuit is AbilitySuit.Red && Ability.AbilityValue is AbilityValue.Joker){
            Card temp = player1Hand;
            player1Hand = player2Hand;
            player2Hand = temp;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Black && Ability.AbilityValue is AbilityValue.Joker){
            score = 100;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Hearts && Ability.AbilityValue is AbilityValue.King){
            score += 1;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Spades && Ability.AbilityValue is AbilityValue.King){
            score += 2;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Clubs && Ability.AbilityValue is AbilityValue.King){
            score += 3;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Diamonds && Ability.AbilityValue is AbilityValue.King){
            score += 4;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Hearts && Ability.AbilityValue is AbilityValue.Queen){
            score -= 1;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Spades && Ability.AbilityValue is AbilityValue.Queen){
            score -= 2;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Clubs && Ability.AbilityValue is AbilityValue.Queen){
            score -= 3;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Diamonds && Ability.AbilityValue is AbilityValue.Queen){
            score -= 4;
        }
    }
}


class Card
{
    public Suit Suit;
    public Value Value;
    public const int RenderHeight = 7;
    public string[] Render()
    {
        char suit = Suit.ToString()[0];
        string value = Value switch
        {
            Value.Ace => "A",
            Value.Ten => "10",
            Value.Jack => "J",
            Value.Queen => "Q",
            Value.King => "K",
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
class Ability
{//a class of ability cards with 2 properties
    public Suit AbilitySuit;
    public Value AbilityValue;
}
enum AbilitySuit
{//the card’s suit
    Hearts,
    Clubs,
    Spades,
    Diamonds,
    Red,
    Black,
}

enum AbilityValue
{
    Queen = Queen,
    King = King,
    Joker = Joker,
}
enum Suit
{//the card’s suit


    Hearts,
    Clubs,
    Spades,
    Diamonds,
}
enum Value
{//the card’s value
    Ace = 01,
    Two = 02,
    Three = 03,
    Four = 04,
    Five = 05,
    Six = 06,
    Seven = 07,
    Eight = 08,
    Nine = 09,
    Ten = 10,
    Jack = 11,
}

enum State
{
    Intro,
    PlayerNum,
    PlayerTurn,
    ComputerTurn,
    Draw,
    Player1Win,
    Player2Win,
    End,
}