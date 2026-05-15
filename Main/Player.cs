namespace Meilenstein1;

public class Player
{
    public GameBoard.FieldNode Position {get; set;}
    public int Throws {get; set;}

    public string Name {get; set;}

    public Player(GameBoard.FieldNode start, string name)
    {
        Position = start;
        Name = name;
        Throws = 0;
    }

    public int Move(int steps, GameBoard game)
    {
        Throws++;
        GameBoard.FieldNode? element = Position;

        for(int i = 0; i < steps; i++)
        {
            element = element.Next;
            if(element == game.End)
            {
                return 3;
            }
            
        }
        Position = element;
        if(Position.Snake == true)
        {
            Position = Position.SpecialDestination;
            return 1;
        }
        else if(Position.Ladder == true)
        {
            Position = Position.SpecialDestination;
            return 2;
        }
        return 0;
    }
}