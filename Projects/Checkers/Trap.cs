namespace Checkers;
public class Traps
{
    public List<(int X, int Y)> trapPositions = new List<(int X, int Y)>
    {
        (3,3),
        (3,4),
        (3,5),
        (4,3),
        (4,5),
        (5,3),
        (5,4),
        (5,5)
    };

    public (int X, int Y) currentTrapPosition;
    public bool IsTrap(int x, int y)
    {
        return trapPositions.Contains((x, y));
    }

    public (int X, int Y) GetRandomTrapPosition()
    {
        Random random = new Random();
        return trapPositions[random.Next(0, trapPositions.Count)];
    }

}