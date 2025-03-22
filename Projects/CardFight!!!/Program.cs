/**
Ability card:
Joker King: Swap the unshown card of each player. 
Joker Queen: Your score is set to 100.
Heart K: adds 1 to your hand value. 
Spade K: adds 2 to your hand value.
Club K: adds 3 to your hand value.
Diamond K: adds 4 to your hand value.
Heart Q: subtract 1 from your hand value. 
Spade Q: subtract 2 from your hand value. 
Club Q: subtract 3 from your hand value. 
Diamond Q: subtract 4 from your hand value. 

Suit ability:
Heart: Your hand value minus 1.
Spade: Your hand value minus 1.
Club: Add 1 to your hand value.
Diamond: Add 2 to your hand value.
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

Exception? exception = null;

List<Card> deck = new List<Card>();//build a new list to store all the cards
List<Card> discardPile = new();//a new list to store all the discarded cards
List<Ability> ability = new();//ability card
List<Ability> discardAbility = new();//a new list to store all the discarded cards
Card player1Hand = new();//player1’s card
int player1Score = 0; //player1’s score
int player2Score = 0; //player2’s score
int player1HandValue = 0; //player1’s hand value for each round
int player2HandValue = 0; //player2’s hand value for each round
Card player2Hand = new();//player1’s card
Ability player1Ability = new();//player’s ability card
Ability player2Ability = new(); //player2’s ability card
bool currentPlayer = true; //true for player1, false for player2
State state = State.Intro; //game state, initialy set to intro
int player = 0; //number of players
bool useAbility = false; //whether to use the ability card
bool chooseAbility = false; //whether to choose the ability card

try
{
    Initialize(); //initialize the game
    //game loop while the game is not ended
    while (!(state is State.End))
    {
        Render(); //render the game
        //game state machine
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Enter:
                if (state is State.Intro)
                {
                    state = State.PlayerNum; //if the game is in intro state, set the game state to player number
                }
                else if (state is State.PlayerNum && (player == 1 || player == 2))
                {
                    state = State.PlayerTurn; //if the game is in player number state and the player number is set, set the game state to player turn
                }
                else if (state is State.PlayerTurn && player == 1)
                {
                    state = State.ComputerTurn; //if the game is in player turn state and the player number is 1, set the game state to computer turn
                }
                else if (state is State.PlayerTurn && player == 2)
                {
                    decideWinner(); // After the second player has played, decide the winner
                    useAbility = false; //set the use ability to false
                    chooseAbility = false; //set the choose ability to false
                    currentPlayer = true; //set the current player to player1
                }
                else if (state is State.ComputerTurn)
                {
                    decideWinner(); // After the computer has played, decide the winner
                }
                else if (state is State.Draw || state is State.Player1Win || state is State.Player2Win)
                {
                    state = State.PlayerTurn; //if the game is in draw, player1 win or player2 win state, set the game state to player turn
                }
                break;

            case ConsoleKey.D1 or ConsoleKey.NumPad1:
                if (state is State.PlayerNum)
                {
                    player = 1; //if the game is in player number state, set the player number to 1
                    state = State.PlayerTurn; //set the game state to player turn
                }
                if (state is State.PlayerTurn)
                {
                    useAbility = true; //if the game is in player turn state, set the use ability to true
                    chooseAbility = true; //set the choose ability to true
                }
                break;
            case ConsoleKey.D2 or ConsoleKey.NumPad2:
                if (state is State.PlayerNum)
                {
                    player = 2; //if the game is in player number state, set the player number to 2
                    state = State.PlayerTurn; //set the game state to player turn
                }
                if (state is State.PlayerTurn)
                {
                    useAbility = false; //if the game is in player turn state, set the use ability to false
                    chooseAbility = true; //set the choose ability to true
                }

                break;
            case ConsoleKey.Escape:
                state = State.End; //if the key is escape, set the game state to end
                break;
        }
    }
    Render(); //render the game
GetEnterOrEscape:
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.Enter: return; //if the key is enter, return
        case ConsoleKey.Escape: return; //if the key is escape, return
        default: goto GetEnterOrEscape; //if the key is not enter or escape, go to GetEnterOrEscape
    }
}
catch (Exception e)
{
    exception = e; //catch the exception
    throw;
}
finally
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine("Game End"); //print game end
}

void Render()
{
    Console.CursorVisible = false;
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("  Card Fight"); //print the game title
    Console.WriteLine();
    //print the Introduction screen
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
    //print the information
    Console.WriteLine($"  Cards In Playing Deck: {deck.Count}");
    Console.WriteLine($"  Cards In Discard Pile: {discardPile.Count}");
    Console.WriteLine($"  Ability Cards In Deck: {ability.Count}");
    Console.WriteLine($"  Discard Ability Pile: {discardAbility.Count}");
    Console.WriteLine();
    //print to ask the user for the number of players
    if (state is State.PlayerNum)
    {
        Console.WriteLine("  Choose the number of players:");
        Console.WriteLine();
        Console.WriteLine("  [1] One Player");
        Console.WriteLine("  [2] Two Players");
        Console.WriteLine();
        Console.WriteLine("  Press [1] or [2] to select the number of players...");
    }
    //print the player turn
    if (state is State.PlayerTurn)
    {
        DrawAndDiscard(); //draw the card and put them in the discard pile
        Console.WriteLine();
        Console.WriteLine($"  Your Hand:");
        if (currentPlayer)
        {
            for (int i = 0; i < Card.RenderHeight; i++) //print the player1’s hand
            {
                string d = player1Hand.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            Console.WriteLine($"    Your Ability Card:"); 
            for (int i = 0; i < Card.RenderHeight; i++) //print the player1’s ability card
            {
                string d = player1Ability.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            //print the ability card effects
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
            Console.WriteLine("  Your score is " + player1HandValue);
        }
        else{
            DrawAndDiscard(); //draw the card and put them in the discard pile
            for (int i = 0; i < Card.RenderHeight; i++)//print the player2’s hand
            {
                string d = player2Hand.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            Console.WriteLine($"    Your Ability Card: ");
            for (int i = 0; i < Card.RenderHeight; i++)//print the player2’s ability card
            {
                string d = player2Ability.Render()[i];
                Console.Write(d);
                Console.WriteLine();
            }
            //print the ability card effects
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
            //print to ask the user for if they want to use the ability card
            while(!chooseAbility){
                Console.WriteLine("  Press 1 for using the ability card press 2 for not using the ability card.");
                Console.WriteLine();
            }
            player2HandValue = CalculateScore(player2Hand, player2Ability, useAbility);
            Console.WriteLine("  Your score is " + player2HandValue); //tell the player the score

        }
        Console.WriteLine();
        Console.WriteLine("  press [Enter] to continue...");
    }
    if (state is State.ComputerTurn)
    { 
        CompterRun(); //draw the card and put them in the discard pile
        Console.WriteLine();
        Console.WriteLine($"  The other player's Hand:");
        for (int i = 0; i < Card.RenderHeight; i++) //print computer's hand
        {
            string d = player2Hand.Render()[i];
            Console.Write(d);
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine("  Press [Enter] to continue...");
    }
    //print the draw
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
        player1HandValue = 0;
        player2HandValue = 0;
    }
    //print the player1 win
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
        player1HandValue = 0;
        player2HandValue = 0;
    }
    //print the player2 win
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
        player1HandValue = 0;
        player2HandValue = 0;
    }
}

//initialize the game
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
    Shuffle(deck); //shuffle the deck
    ShuffleAbility(ability); //shuffle the ability cards
}
void Shuffle(List<Card> cards)
{//method to shuffle the card
    for (int i = 0; i < cards.Count; i++)
    {
        //choose random card to swap
        int swap = Random.Shared.Next(cards.Count); 
        (cards[i], cards[swap]) = (cards[swap], cards[i]); 
    }
}

void ShuffleAbility(List<Ability> cards)
{//method to shuffle the ability card
    for (int i = 0; i < cards.Count; i++)
    {
        //choose random card to swap
        int swap = Random.Shared.Next(cards.Count);
        (cards[i], cards[swap]) = (cards[swap], cards[i]);
    }
}

int CalculateScore(Card Hand, Ability Ability, bool use){
    int score = (int)Hand.Value; //initialize the score with the card value
    if(Hand.Suit is Suit.Hearts){
        score -= 1; //if the card is hearts, subtract 1 from the score
    }
    else if(Hand.Suit is Suit.Spades){
        score -= 2; //if the card is spades, subtract 2 from the score
    }
    else if(Hand.Suit is Suit.Clubs){
        score += 1; //if the card is clubs, add 1 to the score
    }
    else if(Hand.Suit is Suit.Diamonds){
        score += 2; //if the card is diamonds, add 2 to the score
    }

    if(use){ //if the player choose to use the ability card
        if(Ability.AbilitySuit is AbilitySuit.Red && Ability.AbilityValue is AbilityValue.Joker){
            Card temp = player1Hand; //swap the hands of the players
            player1Hand = player2Hand;
            player2Hand = temp;
        }
        else if(Ability.AbilitySuit is AbilitySuit.Black && Ability.AbilityValue is AbilityValue.Joker){
            score = 100; //set the score to 100
        }
        else if(Ability.AbilitySuit is AbilitySuit.Hearts && Ability.AbilityValue is AbilityValue.King){
            score += 1; //if the ability card is hearts king, add 1 to the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Spades && Ability.AbilityValue is AbilityValue.King){
            score += 2; //if the ability card is spades king, add 2 to the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Clubs && Ability.AbilityValue is AbilityValue.King){
            score += 3; //if the ability card is clubs king, add 3 to the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Diamonds && Ability.AbilityValue is AbilityValue.King){
            score += 4; //if the ability card is diamonds king, add 4 to the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Hearts && Ability.AbilityValue is AbilityValue.Queen){
            score -= 1; //if the ability card is hearts queen, subtract 1 from the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Spades && Ability.AbilityValue is AbilityValue.Queen){
            score -= 2; //if the ability card is spades queen, subtract 2 from the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Clubs && Ability.AbilityValue is AbilityValue.Queen){
            score -= 3; //if the ability card is clubs queen, subtract 3 from the score
        }
        else if(Ability.AbilitySuit is AbilitySuit.Diamonds && Ability.AbilityValue is AbilityValue.Queen){
            score -= 4; //if the ability card is diamonds queen, subtract 4 from the score
        }
    }
    return score;
}

//draw the card and put them in the discard pile
void DrawAndDiscard(){
    if(currentPlayer){
        player1Hand = deck[0]; //draw the card
        deck.RemoveAt(0); //remove the card from the deck
        player1Ability = ability[0]; //draw the ability card
        ability.RemoveAt(0); //remove the ability card from the deck
        discardPile.Add(player1Hand); //put the card in the discard pile
        discardAbility.Add(player1Ability); //put the ability card in the discard pile
    }
    else{
        player2Hand = deck[0]; //draw the card
        deck.RemoveAt(0); //remove the card from the deck
        player2Ability = ability[0]; //draw the ability card
        ability.RemoveAt(0); //remove the ability card from the deck
        discardPile.Add(player2Hand); //put the card in the discard pile
        discardAbility.Add(player2Ability); //put the ability card in the discard pile
    }
}

void CompterRun(){
        player2Hand = deck[0]; //draw the card
        deck.RemoveAt(0); //remove the card from the deck
        discardPile.Add(player2Hand); //put the card in the discard pile
}

void decideWinner(){ //method to decide the winner
    if(player1HandValue > player2HandValue){ //if player1’s hand value is greater than player2’s hand value
        player1Score += 1;
        state = State.Player1Win;
    }
    else if(player1HandValue < player2HandValue){ //if player1’s hand value is less than player2’s hand value
        player2Score += 1;
        state = State.Player2Win;
    }
    else{
        state = State.Draw; //if the hand values are equal, set the game state to draw
    }
}


class Card
{ //a class of cards with 2 properties suit and value
    public Suit Suit;
    public Value Value;
    public const int RenderHeight = 7;
    public string[] Render() //method to render the card
    {
        char suit = Suit.ToString()[0];
        string value = Value switch
        {
            Value.Ace => "A",
            Value.Ten => "10",
            Value.Jack => "J",
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
{//a class of ability cards with 2 properties suit and value
    public AbilitySuit AbilitySuit;
    public AbilityValue AbilityValue;
        public const int RenderHeight = 7;
    public string[] Render()//method to render the card
    {
        char suit = AbilitySuit.ToString()[0];
        string value = AbilityValue switch
        {
            AbilityValue.King => "K",
            AbilityValue.Queen => "Q",
            AbilityValue.Joker => "Joker",
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
enum AbilitySuit
{//the ability card’s suit
    Hearts,
    Clubs,
    Spades,
    Diamonds,
    Red,
    Black,
}

enum AbilityValue
{//the ability card’s value
    Queen,
    King,
    Joker,
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
{ //the game state
    Intro,
    PlayerNum,
    PlayerTurn,
    ComputerTurn,
    Draw,
    Player1Win,
    Player2Win,
    End,
}