using Battleships.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.GameModes
{
    /// <summary>
    /// Standard game mod of battleships.
    /// </summary>
    public class StandardGame
    {
        private readonly BasePlayer HumanPlayer;
        private readonly BasePlayer ComputerPlayer;

        /// <summary>
        /// Create a new object of StandardGame.
        /// </summary>
        public StandardGame()
        {
            HumanPlayer = new RealPlayer();
            ComputerPlayer = new ComputerRookiePlayer();
        }

        /// <summary>
        /// Preparation for game. Place all the ships oh grids.
        /// </summary>
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

        /// <summary>
        /// Keyboard button click handling.
        /// </summary>
        /// <param name="button">button a player press.</param>
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
                        Console.WriteLine("\nNot all your ships are set. Please place them all on the grid.\n");
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

        /// <summary>
        /// Game start.
        /// </summary>
        private void Start()
        {
            Console.WriteLine("The game is begin!!!");

            bool isGameRunning = true;
            int turn = 0;
            ComputerPlayer.ShowGrids();// delete
            while (isGameRunning)
            {
                turn++;
                if (turn % 2 == 1)
                {
                    Console.WriteLine("\nIt is your turn to fire.");
                    bool isPlayerTurn = true;
                    while (isPlayerTurn)
                    {
                        (isPlayerTurn, isGameRunning) = PlayerTurn();
                    }
                }
                else
                {
                    bool isComputerTurn = true;
                    while (isComputerTurn)
                    {
                        (isComputerTurn, isGameRunning) = ComputerTurn();
                    }
                }
            }

            Console.WriteLine($"The game is over. You win! You spent {(turn + 1) / 2} turns for this.");
            Console.WriteLine("Congratulations!!!");
            
            
            Console.ReadLine();

        }

        private (bool, bool) PlayerTurn()
        {
            int row, column;
            (row, column) = HumanPlayer.Attack();
            bool isHit, isSink, isWin;
            (isHit, isSink, isWin) = ComputerPlayer.GetShot(row, column);
            HumanPlayer.ChangeEnemyGrid(row, column, isHit);

            if (isSink)
            {
                Console.WriteLine("Ha-Ha you sink enemy's ship!!! Let's repeat!");
            }
            else if (isHit)
            {
                Console.WriteLine("You hit enemy's ship! You can shoot one more time!");
            }
            else
            {
                Console.WriteLine("Unfortunately you miss. It's enemy turn.");
            }


            return (isHit, !isWin);
        }

        private (bool, bool) ComputerTurn()
        {
            int row, column;
            (row, column) = ComputerPlayer.Attack();
            bool isHit, isSink, isWin;
            (isHit, isSink, isWin) = HumanPlayer.GetShot(row, column);
            ComputerPlayer.ChangeEnemyGrid(row, column, isHit);

            if (isSink)
            {
                Console.WriteLine($"Unfortunately enemy fires {IntToChar(row)}{column} and sinks our ship (((  Now he shoots again.");
                System.Threading.Thread.Sleep(1000);
            }
            else if (isHit)
            {
                Console.WriteLine($"Enemy strikes {IntToChar(row)}{column} and hits our ship ( Now he fires one more time.");
                System.Threading.Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine($"This dumb computer shoots {IntToChar(row)}{column} and misses.");
            }

            return (isHit, !isWin);
        }

        /// <summary>
        /// Convert choosen row index from char to int.
        /// </summary>
        /// <param name="ch">Row index.</param>
        /// <returns></returns>
        private char IntToChar(int num) => num switch
        {
            1 => 'A',
            2 => 'B',
            3 => 'C',
            4 => 'D',
            5 => 'E',
            6 => 'F',
            7 => 'G',
            8 => 'H',
            9 => 'I',
            10 => 'J',
            _ => throw new ArgumentOutOfRangeException(nameof(num)),
        };
    }
}
