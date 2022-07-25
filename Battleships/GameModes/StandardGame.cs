using Battleships.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.GameModes
{
    public class StandardGame
    {
        readonly IPlayer HumanPlayer;
        readonly IPlayer ComputerPlayer;

        public StandardGame()
        {
            HumanPlayer = new RealPlayer();
            ComputerPlayer = new ComputerRookiePlayer();
        }

        public void Prepare()
        {
            while (!ComputerPlayer.IsReadyToPlay())
            {
                ComputerPlayer.SetShip();
            }

            Console.WriteLine("To start the game you should place all your chips on the grid.\n");
            while (true)
            {
                Console.WriteLine("\nPlease press 'p' to place new ship,\npress 'v' to view current ships' positions,\npress 'd' to delete all the ship from your grid, " +
                    "\npress 's' to start the game, \npress 'q' to quit the game.");
                char button = Console.ReadKey(true).KeyChar;
                if (button == 'q' || button == 'Q')
                {
                    break;
                }

                ClickHandling(button);
            }
        }

        private void ClickHandling(char button)
        {
            switch (button)
            {
                case 's':
                case 'S':
                    if (HumanPlayer.IsReadyToPlay())
                    {
                        Console.WriteLine("\nLet the game begin!\n");
                        Start();
                    }
                    else
                    {
                        Console.WriteLine("Not all your ships are set. Please place them all on the grid.\n");
                    }
                    break;
                case 'p':
                case 'P':
                    HumanPlayer.SetShip();
                    break;
                case 'v':
                case 'V':
                    HumanPlayer.ShowGrids();
                    break;
                case 'd':
                case 'D':
                    HumanPlayer.DeleteShips();
                    Console.WriteLine("\nAll the ships were deleted.\n");
                    break;
                default:
                    Console.WriteLine("\nUnknown command. please try again.\n");
                    break;
            }
        }

        private void Start()
        {
            Console.WriteLine("The game is begin!!!");
            Console.ReadLine();
        }
    }
}
