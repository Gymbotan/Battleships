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
                ClickHandling(ref isGameRunning, button);
            }

            Console.WriteLine("\nThank you for playing the Battleships!");
        }

        /// <summary>
        /// Keyboard button click handling.
        /// </summary>
        /// <param name="isGameRunning">should we stop the game.</param>
        /// <param name="button">button a player press.</param>
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
