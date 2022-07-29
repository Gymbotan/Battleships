//using Battleships.Ships;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Battleships.Players
//{
//    /// <summary>
//    /// Class of a real human player.
//    /// </summary>
//    public class RealPlayer : BasePlayer
//    {
//        /// <summary>
//        /// Create new object of RealPlayer.
//        /// </summary>
//        public RealPlayer() : base()
//        { }
        
//        /// <summary>
//        /// Attack of enemy grid. You should input coordinates you want to fire.
//        /// </summary>
//        /// <returns> Coordinates (row, column) you want to attack.</returns>
//        public override (int, int) Attack()
//        {
//            Console.WriteLine("\nInput coordinates you want to attack (from a1 to j10) or 'position' to see grids:");
//            return InputCoordinates();
//            //while (true)
//            //{
//            //    Console.WriteLine("Input coordinates you want to attack (from a1 to j10):");
//            //    var input = Console.ReadLine().ToLower();
//            //    char charRow = input[0];
//            //    if (charRow < 'a' || charRow > 'j')
//            //    {
//            //        Console.WriteLine("You inputed wrong coordinates. The first coordinate should be from 'a' to 'j'. Please try again.");
//            //        continue;
//            //    }

//            //    int column;
//            //    if (!int.TryParse(input.Substring(1), out column))
//            //    {
//            //        Console.WriteLine("Can not recognize second coordinate as a number. Please try again.");
//            //        continue;
//            //    }

//            //    if (column < 1 || column > 10)
//            //    {
//            //        Console.WriteLine("You inputed wrong coordinates. The second coordinate should be from '1' to '10'. Please try again.");
//            //        continue;
//            //    }

//            //    int row = CharToInt(charRow);
//            //    return (row, column);
//            //}
//        }

//        /// <summary>
//        /// Set (place) a ship on player's grid.
//        /// </summary>
//        public override void SetShip()
//        {
//            Console.WriteLine("You have next ships:");
//            int count = 1;
//            foreach (Ship ship in ships)
//            {
//                Console.Write("{0} - {1} ({2} squares) {3}", count++, ship.Name, ship.Size, ship.IsSet ? " - is already set.\n" : ".\n");
//            }

//            bool isQuited = false;
//            int shipNumber = 1;

//            while (true)
//            {
//                Console.WriteLine("\nPlease enter a number of the ship you want to set (or 'r' to return to previous menu)");
//                var input = Console.ReadLine();
                
//                if (input == "r" || input == "R")
//                {
//                    isQuited = true;
//                    break;
//                }

//                if (!int.TryParse(input, out shipNumber))
//                {
//                    Console.WriteLine("You inputed not a number. Please try again.");
//                    continue;
//                }

//                if (shipNumber < 1 || shipNumber > ships.Length)
//                {
//                    Console.WriteLine("There is no ship with that number. Please try again.");
//                    continue;
//                }
                
//                if (ships[shipNumber - 1].IsSet)
//                {
//                    Console.WriteLine("This ship is already set. Please choose another ship or clear the grid (delete all the ships).");
//                    continue;
//                }

//                break;
//            }

//            if (!isQuited)
//            {
//                ChooseShipPlacement(ships[shipNumber - 1].Size, shipNumber);
//                ships[shipNumber - 1].IsSet = true;
//                Console.WriteLine($"Ship {ships[shipNumber - 1].Name} was successfully set.");
//            }
//        }

//        /// <summary>
//        /// Select starting and ending places (endpoints) of a choosen ship.
//        /// </summary>
//        /// <param name="shipSize">Size of a ship.</param>
//        /// <param name="shipNumber">Index number of a ship.</param>
//        private void ChooseShipPlacement(int shipSize, int shipNumber)
//        {
//            bool isPlaceSuccesfullyFinded = false;
//            while (!isPlaceSuccesfullyFinded)
//            {
//                Console.WriteLine("\nTo set a ship you should input both endpoint of a ship (consider ship's size)");
//                Console.WriteLine("Input first endpoint's coordinates (from a1 to j10):");
//                var coordinate1 = InputCoordinates();
//                Console.WriteLine("Input second endpoint's coordinates (from a1 to j10):");
//                var coordinate2 = InputCoordinates();
//                if (coordinate1.Item1 == coordinate2.Item1 && Math.Abs(coordinate1.Item2 - coordinate2.Item2) == shipSize - 1)
//                {
//                    if (IsPossibleToSetShip(coordinate1.Item1, coordinate1.Item2, coordinate2.Item1, coordinate2.Item2))
//                    {
//                        int row = coordinate1.Item1;
//                        int column = Math.Min(coordinate1.Item2, coordinate2.Item2);
//                        for (int i = 0; i < shipSize; i++)
//                        {
//                            shipsPlacement[row - 1, column + i - 1] = shipNumber;
//                            ownGrid[row - 1, column + i - 1] = '#';
//                        }
//                        isPlaceSuccesfullyFinded = true;
//                    }
//                    else
//                    {
//                        Console.WriteLine("You can not place your ship here. You collide with another ship. please try again.");
//                    }
//                }
//                else if (coordinate1.Item2 == coordinate2.Item2 && Math.Abs(coordinate1.Item1 - coordinate2.Item1) == shipSize - 1)
//                {
//                    if (IsPossibleToSetShip(coordinate1.Item1, coordinate1.Item2, coordinate2.Item1, coordinate2.Item2))
//                    {
//                        int row = Math.Min(coordinate1.Item1, coordinate2.Item1);
//                        int column = coordinate1.Item2;
//                        for (int i = 0; i < shipSize; i++)
//                        {
//                            shipsPlacement[row + i - 1, column - 1] = shipNumber;
//                            ownGrid[row + i - 1, column - 1] = '#';
//                        }
//                        isPlaceSuccesfullyFinded = true;
//                    }
//                    else
//                    {
//                        Console.WriteLine("You can not place your ship here. You collide with another ship. please try again.");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Ship can not have such endpoints. Please try again.");
//                }
//            }
//        }

//        /// <summary>
//        /// Convert choosen row index from char to int.
//        /// </summary>
//        /// <param name="ch">Row index.</param>
//        /// <returns></returns>
//        private int CharToInt (char ch) => ch switch
//        {
//            'a' => 1,
//            'b' => 2,
//            'c' => 3,
//            'd' => 4,
//            'e' => 5,
//            'f' => 6,
//            'g' => 7,
//            'h' => 8,
//            'i' => 9,
//            'j' => 10,
//            _ => throw new ArgumentOutOfRangeException(nameof(ch)),
//        };

//        /// <summary>
//        /// Allow to input coordinates of place (like a1 of c5).
//        /// </summary>
//        /// <returns></returns>
//        private (int, int) InputCoordinates()
//        {
//            int column;
//            char charRow;

//            while (true)
//            {
//                var input = Console.ReadLine().ToLower();
//                if (input == "position")
//                {
//                    ShowGrids();
//                    continue;
//                }
                
//                charRow = input[0];
//                if (charRow < 'a' || charRow > 'j')
//                {
//                    Console.WriteLine("You inputed wrong coordinates. The first coordinate should be from 'a' to 'j'. Please try again.");
//                    continue;
//                }

//                if (!int.TryParse(input.Substring(1), out column))
//                {
//                    Console.WriteLine("Can not recognize second coordinate as a number. Please try again.");
//                    continue;
//                }

//                if (column < 1 || column > 10)
//                {
//                    Console.WriteLine("You inputed wrong coordinates. The second coordinate should be from '1' to '10'. Please try again.");
//                    continue;
//                }
                
//                break;
//            }

//            int row = CharToInt(charRow);
//            return (row, column);
//        }
//    }
//}
