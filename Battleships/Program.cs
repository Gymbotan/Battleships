using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The game Battleships is greeting you!");
            Console.WriteLine("To start a new game press s");
            Console.WriteLine("To see help press h");
            Console.WriteLine("To quit application press q");
            while (true)
            {
                char button = Console.ReadKey(true).KeyChar;
                if (button == 'q' || button == 'Q')
                {
                    break;
                }

                ClickHandling(button);
            }
            
            //int a = 1;
            //string b = "abc";
            //bool c = false;

            //Console.Write("{0} - {1}{2}", a, b, c ? " - is already set." : ".");

            //char ch = (char)((int)'A' + 1);
            //Console.WriteLine(ch);
            Console.WriteLine("·");
            Console.WriteLine("\nThank you for playing the Battleships!");
        }

        private static void ClickHandling(char button)
        {
            switch(button)
            {
                case 's':
                case 'S':
                    Console.WriteLine("\nThe game is started");
                    break;
                case 'h':
                case 'H':
                    Console.WriteLine("\nIt is a help.");
                    break;
                default:
                    Console.WriteLine("\nUnknown command. please try again.");
                    break;
            }
        }
    }
}
