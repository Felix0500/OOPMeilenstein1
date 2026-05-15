using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security;

namespace Meilenstein1;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Spiel erstellen...");
        int length = 0;
        do
        {
            try
            {
                Console.Write("Wie viele Felder soll das Brett haben (mind. 5) ? ");
                length = Convert.ToInt32(Console.ReadLine());
            }
            catch(System.FormatException)
            {
                Console.WriteLine("Bitte eine Zahl eingeben!");
                continue;
            }
        }
        while(length < 5);

        GameBoard? game1 = CreateGameBoard(length);


        int numberOfPlayers = 0;
        do
        {
            try
            {
                Console.Write("Wie viele Spieler wollen spielen (mind. 2) ? ");
                numberOfPlayers = Convert.ToInt32(Console.ReadLine());
            }
            catch(System.FormatException)
            {
                Console.WriteLine("Bitte eine Zahl eingeben!");
                continue;
            }
        }
        while(numberOfPlayers < 2);

        
        Player[]? players = CreatePlayers(numberOfPlayers, game1);

        CreateRandomSpecialFields(game1, length/3, length);


        PrintGameBoard(game1);

        bool gameOver = false;

        while(gameOver == false)
        {
            foreach(Player player in players)
            {
                Console.WriteLine($"{player.Name} ist dran!");
                Console.WriteLine($"{player.Name} befindet sich auf Feld {player.Position.FieldNumber} !");
                Console.WriteLine("Drücke Enter, um zu würfeln...");
                int steps = Dice();
                Console.WriteLine($"{player.Name} hat eine  {steps} gewürfelt!");
                int gameStatus = player.Move(steps, game1);
                switch(gameStatus)
                {
                    case 0:
                        Console.WriteLine($"{player.Name} rückt vor auf Feld {player.Position.FieldNumber} !");
                        Console.WriteLine();
                        break;
                    case 1:
                        Console.WriteLine($"Oh Nein! {player.Name} ist auf einem Feld mit einer Schlange gelandet! {player.Name} rutscht auf Feld {player.Position.FieldNumber} !");
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine($"Hell Yeah! {player.Name} ist auf einem Feld mit einer Leiter gelandet! {player.Name} klettert vor auf Feld {player.Position.FieldNumber} !");
                        Console.WriteLine();
                        break;
                    case 3:
                        Console.WriteLine($"{player.Name} hat gewonnen in {player.Throws} Würfen. Glückwunsch!");
                        Console.WriteLine("Spiel ist beendet!");
                        gameOver = true;
                        return;
                }
            }
        }

    }

    public static int Dice()
    {
        Console.ReadLine();

        Random rnd = new Random();
        int steps = rnd.Next(1,7);
        return steps;
    }

    public static GameBoard? CreateGameBoard(int length)
    {
        return new GameBoard(length);
    }
    public static Player[]? CreatePlayers(int numberOfPlayers,GameBoard game)
    {
        if(numberOfPlayers < 2)
        {
        Console.WriteLine("Mindestens 2 Spieler erforderlich");   
           return null; 
        }
        Player[] players = new Player[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = new Player(game.Start, $"Player {i + 1}");
        }
        return players;
    }
    public static void CreateRandomSpecialFields(GameBoard game, int max, int gameBoardLength)
    {
        Random rnd = new Random();
        int created = 0;
        while (created < max)
        {
            int field1 = rnd.Next(2,gameBoardLength - 1);
            int field2 = rnd.Next(2,gameBoardLength - 1);

            if(field1 == field2)
                continue;

            bool worked = game.AddSpecialField(field1, field2);
            
            if (worked == false)
            {
                continue;
            }
            
            created++;
        }
    }
    public static void PrintGameBoard(GameBoard game)
    {
        GameBoard.FieldNode? element = game.Start;

        while(element != null)
        {
            Console.Write($"{element.FieldNumber} ");
            if (element.Ladder == true)
            {
                Console.Write($"(Leiter auf {element.SpecialDestination.FieldNumber}) ");
            }
            else if (element.Snake == true)
            {
                Console.Write($"(Schlange auf {element.SpecialDestination.FieldNumber}) ");
            }
            element = element.Next;
        }
        Console.WriteLine();
    }
}

