using Battleships.GameModes;
using Battleships.Ships;
using System;
using System.Linq;

namespace Battleships
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The game Battleships is greeting you!");
            Console.WriteLine("Press 's' to start a new game");
            Console.WriteLine("Press 'h' to see a help");
            Console.WriteLine("Press 'q' to quit application");
            bool isGameRunning = true;
            while (isGameRunning)
            {
                char button = Console.ReadKey(true).KeyChar;
                //if (button == 'q' || button == 'Q')
                //{
                //    break;
                //}

                ClickHandling(ref isGameRunning, button);
            }

            //Ship[] ships = new Ship[] { new BattleShip(), new BattleShip(), new BattleShip() };
            //Console.WriteLine(ships.All(s => s.IsAlive));

            //int a = 1;
            //string b = "abc";
            //bool c = false;

            //Console.Write("{0} - {1}{2}", a, b, c ? " - is already set." : ".");

            //char ch = (char)((int)'A' + 1);
            //Console.WriteLine(ch);
            Console.WriteLine("·");
            Console.WriteLine("\nThank you for playing the Battleships!");
        }

        private static void ClickHandling(ref bool isGameRunning, char button)
        {
            switch(button)
            {
                case 's':
                case 'S':
                    //Console.WriteLine("\nThe game is started\n");
                    Console.WriteLine();
                    var game = new StandardGame();
                    game.Prepare();
                    isGameRunning = false;
                    break;
                case 'q':
                case 'Q':
                    isGameRunning = false;
                    break;
                case 'h':
                case 'H':
                    Console.WriteLine("\nIt is a help.\n");
                    break;
                default:
                    Console.WriteLine("\nUnknown command. please try again.\n");
                    break;
            }
        }
    }
}
