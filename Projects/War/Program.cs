using System;
using System.Collections.Generic;
using System.Linq;

public enum Suit { Heart, Spade, Club, Diamond }
public enum AbilityType { 
    JokerKing, JokerQueen, 
    HeartK, SpadeK, ClubK, DiamondK, 
    HeartQ, SpadeQ, ClubQ, DiamondQ 
}

public class PlayCard
{
    public Suit Suit { get; set; }
    public int Value { get; set; }
}

public class AbilityCard
{
    public AbilityType Type { get; set; }
}

public class Player
{
    public List<PlayCard> PlayCards { get; } = new List<PlayCard>();
    public AbilityCard Ability { get; set; }
    public int Modifier { get; set; }
}

public class PlayDeck
{
    private List<PlayCard> cards = new List<PlayCard>();
    private readonly Random random = new Random();

    public PlayDeck() => Initialize();

    private void Initialize()
    {
        cards.Clear();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            for (int value = 1; value <= 11; value++)
                cards.Add(new PlayCard { Suit = suit, Value = value });
        Shuffle();
    }

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public PlayCard Draw()
    {
        if (cards.Count == 0) Initialize();
        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public bool IsEmpty() => cards.Count == 0;
}

public class AbilityDeck
{
    private List<AbilityCard> cards = new List<AbilityCard>();
    private readonly Random random = new Random();

    public AbilityDeck() => Initialize();

    private void Initialize()
    {
        cards.Clear();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            cards.Add(new AbilityCard { Type = (AbilityType)((int)suit * 2 + 2) });
            cards.Add(new AbilityCard { Type = (AbilityType)((int)suit * 2 + 3) });
        }
        cards.Add(new AbilityCard { Type = AbilityType.JokerKing });
        cards.Add(new AbilityCard { Type = AbilityType.JokerQueen });
        Shuffle();
    }

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }
    }

    public AbilityCard Draw()
    {
        if (cards.Count == 0) Initialize();
        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public bool IsEmpty() => cards.Count == 0;
}

public class Game
{
    private readonly PlayDeck playDeck = new PlayDeck();
    private readonly AbilityDeck abilityDeck = new AbilityDeck();
    private readonly Player human = new Player();
    private readonly Player computer = new Player();
    private int humanScore;
    private int computerScore;

    public void Start()
    {
        InitializePlayers();
        GameLoop();
    }

    private void InitializePlayers()
    {
        human.Ability = abilityDeck.Draw();
        computer.Ability = abilityDeck.Draw();
        human.PlayCards.Add(playDeck.Draw());
        computer.PlayCards.Add(playDeck.Draw());
    }

    private void GameLoop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Press Escape to quit or any key to play...");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) return;

            PlayRound();
            ApplySuitAbilities();
            DisplayResults();
            HandleRoundEnd();
        }
    }

    private void PlayRound()
    {
        human.PlayCards.Add(playDeck.Draw());
        computer.PlayCards.Add(playDeck.Draw());

        HandleAbilities();
    }

    private void HandleAbilities()
    {
        if (human.Ability != null && PromptUseAbility())
            UseAbility(human, computer);
        
        if (computer.Ability != null && new Random().Next(2) == 0)
            UseAbility(computer, human);
    }

    private bool PromptUseAbility()
    {
        Console.Write("Use ability card? (Y/N): ");
        return Console.ReadKey().Key == ConsoleKey.Y;
    }

    private void UseAbility(Player user, Player opponent)
    {
        switch (user.Ability.Type)
        {
            case AbilityType.JokerKing:
                (user.PlayCards[0], opponent.PlayCards[0]) = (opponent.PlayCards[0], user.PlayCards[0]);
                break;
            case AbilityType.JokerQueen:
                if (opponent.PlayCards.Count > 1)
                    opponent.PlayCards.RemoveAt(opponent.PlayCards.Count - 1);
                break;
            case AbilityType.HeartK: user.Modifier += 1; break;
            case AbilityType.SpadeK: user.Modifier += 2; break;
            case AbilityType.ClubK: user.Modifier += 3; break;
            case AbilityType.DiamondK: user.Modifier += 4; break;
            case AbilityType.HeartQ: user.Modifier -= 1; break;
            case AbilityType.SpadeQ: user.Modifier -= 2; break;
            case AbilityType.ClubQ: user.Modifier -= 3; break;
            case AbilityType.DiamondQ: user.Modifier -= 4; break;
        }
        user.Ability = null;
    }

    private void ApplySuitAbilities()
    {
        ApplySuitEffect(human, computer);
        ApplySuitEffect(computer, human);
    }

    private static void ApplySuitEffect(Player player, Player opponent)
    {
        switch (player.PlayCards[0].Suit)
        {
            case Suit.Heart:
            case Suit.Spade:
                opponent.Modifier--;
                break;
            case Suit.Club:
                player.Modifier++;
                break;
            case Suit.Diamond:
                player.Modifier += 2;
                break;
        }
    }

    private void DisplayResults()
    {
        Console.WriteLine("\nYour Cards:");
        human.PlayCards.Skip(1).ToList().ForEach(c => Console.WriteLine($"{c.Value} of {c.Suit}"));
        
        Console.WriteLine("\nComputer's Cards:");
        computer.PlayCards.Skip(1).ToList().ForEach(c => Console.WriteLine($"{c.Value} of {c.Suit}"));

        int humanTotal = human.PlayCards.Sum(c => c.Value) + human.Modifier;
        int computerTotal = computer.PlayCards.Sum(c => c.Value) + computer.Modifier;

        Console.WriteLine($"\nYour Total: {humanTotal}");
        Console.WriteLine($"Computer's Total: {computerTotal}");

        if (humanTotal > computerTotal) humanScore++;
        else if (computerTotal > humanTotal) computerScore++;
        
        Console.WriteLine($"\nScore: You {humanScore} - Computer {computerScore}");
    }

    private static void HandleRoundEnd()
    {
        Console.WriteLine("\nPress Enter to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
    }
}

class Program
{
    static void Main() => new Game().Start();
}