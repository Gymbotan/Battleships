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
            bool isGameRunning = true;
            while (isGameRunning)
            {
                Console.WriteLine("Press 's' to start a new game");
                Console.WriteLine("Press 'h' to see a help");
                Console.WriteLine("Press 'q' to quit application");
            
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
                    Console.WriteLine("\nIt is a help.");
                    Console.WriteLine("To start a game you should press 's'.");
                    Console.WriteLine("Then you should press 'p' and place all your ships on the grid one by one (pressing 'p' and inputing coordinates).");
                    Console.WriteLine("After that you should press 's' again to begin.");
                    Console.WriteLine("Each turn you should input coordinates you want to fire until you'll miss.");
                    Console.WriteLine("When you sink all computer's ships (or it will destroy yours) the game will over.\n");
                    break;
                default:
                    Console.WriteLine("\nUnknown command. please try again.\n");
                    break;
            }
        }
    }
}
