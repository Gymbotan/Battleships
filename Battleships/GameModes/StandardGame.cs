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
        //private readonly BasePlayer HumanPlayer;
        //private readonly BasePlayer ComputerPlayer;
        private readonly IPlayer humanPlayer;
        private readonly IPlayer computerPlayer;


        /// <summary>
        /// Create a new object of StandardGame.
        /// </summary>
        public StandardGame()
        {
            //HumanPlayer = new RealPlayer();
            //ComputerPlayer = new ComputerRookiePlayer();
            humanPlayer = new HumanPlayer();
            computerPlayer = new ComputerPlayer();
        }

        /// <summary>
        /// Preparation for game. Place all the ships oh grids.
        /// </summary>
        public void Prepare()
        {
            //computerPlayer.ShowGrids();
            while (!computerPlayer.isReadyToPlay())
            {
                computerPlayer.SetShips();
            }

            Console.WriteLine("To start the game you should place all your chips on the grid.\n");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nPress 'p' to place ships,\npress 'v' to view current ships' positions,\npress 'd' to delete all the ship from your grid, " +
                    "\npress 's' to start the game, \npress 'q' to quit the game.");
                char button = Console.ReadKey(true).KeyChar;
                //if (button == 'q' || button == 'Q')
                //{
                //    break;
                //}

                ClickHandling(button, ref isRunning);
            }
        }

        /// <summary>
        /// Keyboard button click handling.
        /// </summary>
        /// <param name="button">button a player press.</param>
        private void ClickHandling(char button, ref bool isRunning)
        {
            switch (button)
            {
                case 'q':
                case 'Q':
                    isRunning = false;
                    break;
                case 's':
                case 'S':
                    if (humanPlayer.isReadyToPlay())
                    {
                        Console.WriteLine("\nLet the game begin!\n");
                        Start();
                        isRunning = false;
                    }
                    else
                    {
                        Console.WriteLine("\nNot all your ships are set. Please place them all on the grid.\n");
                    }
                    break;
                case 'p':
                case 'P':
                    humanPlayer.SetShips();
                    break;
                case 'v':
                case 'V':
                    humanPlayer.ShowGrids();
                    break;
                case 'd':
                case 'D':
                    humanPlayer.DeleteShips();
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
            computerPlayer.ShowGrids();// delete
            while (isGameRunning)
            {
                turn++;
                if (turn % 2 == 1)
                {
                    Console.WriteLine("\nIt is your turn to fire.");
                    bool isPlayerTurn = true;
                    while (isPlayerTurn && isGameRunning)
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

            Console.WriteLine($"\nThe game is over. You win! You spent {(turn + 1) / 2} turns for this.");
            Console.WriteLine("Congratulations!!!");
            
            
            Console.ReadLine();

        }

        private (bool, bool) PlayerTurn()
        {
            int row, column;
            (row, column) = humanPlayer.Attack();
            bool isHit, isSink, isWin;
            (isHit, isSink, isWin) = computerPlayer.GetShot(row, column);
            humanPlayer.ChangeEnemyGrid(row, column, isHit);

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
            (row, column) = computerPlayer.Attack();
            bool isHit, isSink, isWin;
            (isHit, isSink, isWin) = humanPlayer.GetShot(row, column);
            computerPlayer.ChangeEnemyGrid(row, column, isHit);

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
                Console.WriteLine($"The computer shoots {IntToChar(row)}{column} and misses.");
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
