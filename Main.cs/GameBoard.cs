namespace Meilenstein1;

public class GameBoard
{
    public FieldNode? Start { get; set;} = null;
    public FieldNode? End { get; set;} = null;
    public GameBoard(int length)
    {
        for(int i = 1; i <= length; i++)
        {
            AddFieldNode(i);
        }
    }

    public void AddFieldNode(int number)
    {
        if(Start == null)
        {
            FieldNode? neu = new FieldNode(number, null, null);
            Start = neu;
            End = neu;
        }
        else
        {
            FieldNode? neu = new FieldNode(number, null, End);
            End!.Next = neu;
            End = neu;
        }
    }
    public FieldNode? FindNode(int number)
    {
        FieldNode? element = Start;
        while(element != null)
        {
            if(element.FieldNumber == number)
            {
                return element;
            }
            else
            {
                element = element.Next;
            }
        }
        Console.WriteLine("FieldNode wurde nicht gefunden!");
        return null;
    }
     public bool AddSpecialField(int currentFieldNumber, int destinationFieldNumber)
    {
        FieldNode? currentElement = FindNode(currentFieldNumber);
        FieldNode? destinationElement = FindNode(destinationFieldNumber);

        if(currentElement.Snake == false && currentElement.Ladder == false)
        {
            if(currentFieldNumber < destinationFieldNumber)
            {
                currentElement.Ladder = true;
                currentElement.SpecialDestination = destinationElement;
                return true;
            }    
            else 
            {
                currentElement.Snake = true;
                currentElement.SpecialDestination = destinationElement;
                return true;
            }     
        }
        else
        {
            return false;
        }
        


    }
    public void AddSnake(int currentFieldNumber, int destinationFieldNumber)
    {
        if(currentFieldNumber <= destinationFieldNumber)
        {
            Console.WriteLine("Schlange muss nach unten führen!");
            return;
        }    
        FieldNode? currentElement = FindNode(currentFieldNumber);
        FieldNode? destinationElement = FindNode(destinationFieldNumber);
        if (currentElement.Ladder == true)
        {
            Console.WriteLine($"Element {currentElement.FieldNumber} ist schon eine Leiter, kann nicht Leiter und Schlange gleichzeitig sein!");
            return;
        }
        currentElement.Snake = true;
        currentElement.SpecialDestination = destinationElement;

    }
    public void AddLadder(int currentFieldNumber, int destinationFieldNumber)
    {
        if(currentFieldNumber >= destinationFieldNumber)
        {
            Console.WriteLine("Leiter muss nach oben führen!");
            return;
        }    
        FieldNode? currentElement = FindNode(currentFieldNumber);
        FieldNode? destinationElement = FindNode(destinationFieldNumber);
        if (currentElement.Snake == true)
        {
            Console.WriteLine($"Element {currentElement.FieldNumber} ist schon eine Schlange, kann nicht Schlange und Leiter gleichzeitig sein!");
            return;
        }
        currentElement.Ladder = true;
        currentElement.SpecialDestination = destinationElement;
    }
    public class FieldNode
    {
        public FieldNode? Next {get; set;}
        public FieldNode? Prev {get; set;}
        public bool Snake {get; set;}
        public bool Ladder {get; set;}
        public FieldNode? SpecialDestination {get; set;}
        public int FieldNumber {get;set;}

        public FieldNode(int fieldNumber, FieldNode? next, FieldNode? prev)
        {
            Next = next;
            Prev = prev;
            FieldNumber = fieldNumber;
            Snake = false;
            Ladder = false;
            SpecialDestination = null;
        }
    }
}
