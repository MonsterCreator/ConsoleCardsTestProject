using System;
using System.Runtime.InteropServices;


public class Program
{
    public static void Main()
    {
        Card[] cards = null; //create empty cards array
        MyConsole.SystemLog(MyConsole.GetCommandsList); //print command list and set yellow(system) color

        while (true)
        {
            MyConsole.GetCommand(ref cards);
        }
    }
}





