using ConsoleTestProject1;

public class MyConsole
{
    public static void SystemLog(Action message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        message.Invoke();
        Console.ResetColor();
    }

    public static void ErrorLog(Action action)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        action.Invoke();
        Console.ResetColor();
    }

    public static void GetCommandsList()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine($"/commands - get this message");
        Console.WriteLine($"/createcards - creating the list of the cards");
        Console.WriteLine($"/printcards - clear the list of the cards");
        Console.WriteLine($"/shuffle - clear the list of the cards");
        Console.WriteLine($"/sortbyid - clear the list of the cards");
        Console.WriteLine($"/sort - clear the list of the cards");
    }

    public static void GetProgramInfo()
    {
        Console.WriteLine();
    }

    public static void GetCommand(ref Card[] cards)
    {
        string command = GetString("Send your command:");

        switch (command)
        {
            case "/commands":
                MyConsole.SystemLog(MyConsole.GetCommandsList);
                break;
            case "/createcards":
                CardsManager.CreateCards(ref cards);
                break;
            case "/printcards":
                CardsManager.PrintCards(cards);
                break;
            case "/shuffle":
                CardsManager.Shuffle(ref cards);
                break;
            case "/sortbyid":
                CardsManager.SortByID(ref cards);
                break;
            case "/sort":
                CardsManager.Sort(ref cards);
                break;
            default:
                MyConsole.ErrorLog(() => Console.WriteLine("Unknown command"));
                break;
        }

    }

    private static string GetString(string message)
    {
        if (message is null)
        {
            message = "Send your text";
        }
        Console.WriteLine(message);
        string userText = Console.ReadLine();
        return userText;
    }
}





