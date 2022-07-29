using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    /// <summary>
    /// Class human player.
    /// </summary>
    public class HumanPlayer : IPlayer
    {
        /// <summary>
        /// Grids manager.
        /// </summary>
        public GridsManager gridsManager { get; }

        /// <summary>
        /// Create object of HumanPlayer.
        /// </summary>
        public HumanPlayer()
        {
            gridsManager = new GridsManager();
        }

        /// <summary>
        /// Choose coordinates to fire opponent.
        /// </summary>
        /// <returns>Coordinates you want to fire.</returns>
        public (int, int) Attack()
        {
            Console.WriteLine("\nInput coordinates you want to attack (from a1 to j10) or 'position' to see grids:");
            return InputCoordinates();
        }

        /// <summary>
        /// Change enemy grid after getting results of your attack.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="column">Column.</param>
        /// <param name="isHit">Is hit.</param>
        public void ChangeEnemyGrid(int row, int column, bool isHit)
        {
            gridsManager.ChangeEnemyGrid(row, column, isHit);
        }

        /// <summary>
        /// Reaction on getting shot.
        /// </summary>
        /// <param name="row">Row the enemy fired.</param>
        /// <param name="column">Column the enemy fired.</param>
        /// <returns>Result of attack (is enemy hit, is ship sink, is you lose).</returns>
        public (bool, bool, bool) GetShot(int row, int column)
        {
            return gridsManager.GetShot(row, column);
        }

        /// <summary>
        /// Set ships on the grid.
        /// </summary>
        public void SetShips()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\nPress 'p' to place new ship,\npress 'v' to view current ships' positions," +
                    "\npress 'd' to delete all the ship from your grid, \npress 'q' to stop ship placing.");
                char button = Console.ReadKey(true).KeyChar;
                switch (button)
                {
                    case 'q':
                    case 'Q':
                        isRunning = false;
                        break;
                    case 'p':
                    case 'P':
                        SetShip();
                        break;
                    case 'v':
                    case 'V':
                        gridsManager.ShowGrids();
                        break;
                    case 'd':
                    case 'D':
                        gridsManager.DeleteShips();
                        Console.WriteLine("\nAll the ships were deleted.\n");
                        break;
                    default:
                        Console.WriteLine("\nUnknown command. please try again.\n");
                        break;
                }
            }            
        }

        /// <summary>
        /// Set ship.
        /// </summary>
        private void SetShip()
        {
            Console.WriteLine("\nYou have next ships:");
            int count = 1;
            foreach (Ship ship in gridsManager.Ships)
            {
                Console.Write("{0} - {1} ({2} squares){3}", count++, ship.Name, ship.Size, ship.IsSet ? " - is already set.\n" : ".\n");
            }

            bool isQuited = false;
            int shipIndex = 1;

            while (true)
            {
                Console.WriteLine("\nPlease enter a number of the ship you want to set (or 'r' to return to previous menu)");
                var input = Console.ReadLine();

                if (input == "r" || input == "R")
                {
                    isQuited = true;
                    break;
                }

                if (!int.TryParse(input, out shipIndex))
                {
                    Console.WriteLine("You inputed not a number. Please try again.");
                    continue;
                }

                if (shipIndex < 1 || shipIndex > gridsManager.Ships.Length)
                {
                    Console.WriteLine("There is no ship with that number. Please try again.");
                    continue;
                }

                if (gridsManager.Ships[shipIndex - 1].IsSet)
                {
                    Console.WriteLine("This ship is already set. Please choose another ship or clear the grid (delete all the ships).");
                    continue;
                }

                break;
            }

            if (!isQuited)
            {
                ChooseShipPlacement(shipIndex);
                gridsManager.Ships[shipIndex - 1].IsSet = true;
                Console.WriteLine($"Ship {gridsManager.Ships[shipIndex - 1].Name} was successfully set.");
            }
        }

        /// <summary>
        /// Select starting and ending places (endpoints) of a choosen ship.
        /// </summary>
        /// <param name="shipIndex">Index number of a ship.</param>
        private void ChooseShipPlacement(int shipIndex)
        {
            bool isPlaceSuccesfullyFinded = false;
            while (!isPlaceSuccesfullyFinded)
            {
                Console.WriteLine("\nTo set a ship you should input both endpoint of a ship (consider ship's size)");
                Console.WriteLine("Input first endpoint's coordinates (from a1 to j10):");
                var coordinate1 = InputCoordinates();
                Console.WriteLine("Input second endpoint's coordinates (from a1 to j10):");
                var coordinate2 = InputCoordinates();
                try
                {
                    gridsManager.SetShip(shipIndex, coordinate1.Item1, coordinate1.Item2, coordinate2.Item1, coordinate2.Item2);
                    isPlaceSuccesfullyFinded = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ShowGrids()
        {
            gridsManager.ShowGrids();
        }

        /// <summary>
        /// Allow to input coordinates of place (like a1 of c5).
        /// </summary>
        /// <returns></returns>
        private (int, int) InputCoordinates()
        {
            int column;
            char charRow;

            while (true)
            {
                var input = Console.ReadLine().ToLower();
                if (input == "position")
                {
                    ShowGrids();
                    Console.WriteLine("Try again.");
                    continue;
                }

                charRow = input[0];
                if (charRow < 'a' || charRow > 'j')
                {
                    Console.WriteLine("You inputed wrong coordinates. The first coordinate should be from 'a' to 'j'. Please try again.");
                    continue;
                }

                if (!int.TryParse(input.Substring(1), out column))
                {
                    Console.WriteLine("Can not recognize second coordinate as a number. Please try again.");
                    continue;
                }

                if (column < 1 || column > 10)
                {
                    Console.WriteLine("You inputed wrong coordinates. The second coordinate should be from '1' to '10'. Please try again.");
                    continue;
                }

                break;
            }

            int row = CharToInt(charRow);
            return (row, column);
        }

        /// <summary>
        /// Convert choosen row index from char to int.
        /// </summary>
        /// <param name="ch">Row index.</param>
        /// <returns></returns>
        private int CharToInt(char ch) => ch switch
        {
            'a' => 1,
            'b' => 2,
            'c' => 3,
            'd' => 4,
            'e' => 5,
            'f' => 6,
            'g' => 7,
            'h' => 8,
            'i' => 9,
            'j' => 10,
            _ => throw new ArgumentOutOfRangeException(nameof(ch)),
        };

        /// <summary>
        /// Check is this player ready to play (are all the ships placed on the grid.).
        /// </summary>
        /// <returns></returns>
        public bool isReadyToPlay()
        {
            return gridsManager.IsReadyToPlay();
        }

        /// <summary>
        /// Delete all the ships from your grid.
        /// </summary>
        public void DeleteShips()
        {
            gridsManager.DeleteShips();
        }
    }
}
