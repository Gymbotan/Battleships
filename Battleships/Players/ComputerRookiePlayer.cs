//using Battleships.Ships;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Battleships.Players
//{
//    /// <summary>
//    /// Class of a rookie computer player.
//    /// </summary>
//    public class ComputerRookiePlayer : BasePlayer
//    {
//        /// <summary>
//        /// Create new object of ComputerRookiePlayer.
//        /// </summary>
//        public ComputerRookiePlayer() : base()
//        { }
       
//        /// <summary>
//        /// Attack of enemy grid. Rookie computer player fires randomly.
//        /// </summary>
//        /// <returns></returns>
//        public override (int, int) Attack()
//        {
//            Random rnd = new();
//            int row, column;
//            while (true)
//            {
//                row = rnd.Next(0, 9);
//                column = rnd.Next(0, 9);
//                if (enemyGrid[row, column] == ' ')
//                {
//                    return (row + 1, column + 1);
//                }
//            }
//        }

//        /// <summary>
//        /// Set (place) all ships on the grid.
//        /// </summary>
//        public override void SetShip()
//        {
//            DeleteShips();
//            for (int i = 0; i < ships.Length; i++)
//            {
//                ChooseShipPlacement(ships[i].Size, i + 1);
//                ships[i].IsSet = true;
//            }
//        }

//        /// <summary>
//        /// Randomly choose place for a ship.
//        /// </summary>
//        /// <param name="shipSize">Size of a ship.</param>
//        /// <param name="shipNumber">Index number of a ship.</param>
//        private void ChooseShipPlacement(int shipSize, int shipNumber)
//        {
//            bool isSet = false;
//            while (!isSet)
//            {
//                // we choose a random point on the grid. It will be initial endpoint of the ship
//                Random rnd = new();
//                int row1 = rnd.Next(0, 9);
//                int column1 = rnd.Next(0, 9);
//                int row2, column2;
//                int count = 0;
//                while(!isSet && count < 5) // if we can not set (place) the ship, maybe it is imposiible. Let's choose a new initial point
//                {
//                    count++;
//                    switch (rnd.Next(1,4)) // we randomly choose direction where to set the ship from initial point
//                    {
//                        case 1: // up
//                            if (row1 - shipSize + 1 < 0)
//                            { 
//                                break;
//                            }
//                            else
//                            {
//                                row2 = row1 - shipSize + 1;
//                                if (IsPossibleToSetShip(row1 + 1, column1 + 1, row2 + 1, column1 + 1))
//                                {
//                                    SetShipVertical(row1, row2, column1, shipNumber);
//                                    isSet = true;
//                                }
//                                break;
//                            }
//                        case 2: // down
//                            if (row1 + shipSize - 1 > 9)
//                            {
//                                break;
//                            }
//                            else
//                            {
//                                row2 = row1 + shipSize - 1;
//                                if (IsPossibleToSetShip(row1 + 1, column1 + 1, row2 + 1, column1 + 1))
//                                {
//                                    SetShipVertical(row1, row2, column1, shipNumber);
//                                    isSet = true;
//                                }
//                                break;
//                            }
//                        case 3: // left
//                            if (column1 - shipSize + 1 < 0)
//                            {
//                                break;
//                            }
//                            else
//                            {
//                                column2 = column1 - shipSize + 1;
//                                if (IsPossibleToSetShip(row1 + 1, column1 + 1, row1 + 1, column2 + 1))
//                                {
//                                    SetShipHorizontal(row1, column1, column2, shipNumber);
//                                    isSet = true;
//                                }
//                                break;
//                            }
//                        case 4: // right
//                            if (row1 + shipSize - 1 > 9)
//                            {
//                                break;
//                            }
//                            else
//                            {
//                                column2 = row1 + shipSize - 1;
//                                if (IsPossibleToSetShip(row1 + 1, column1 + 1, row1 + 1, column2 + 1))
//                                {
//                                    SetShipHorizontal(row1, column1, column2, shipNumber);
//                                    isSet = true;
//                                }
//                                break;
//                            }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Set (place) a ship on a grid on selected position (vertically).
//        /// </summary>
//        /// <param name="row1">starting row index.</param>
//        /// <param name="row2">ending row index.</param>
//        /// <param name="column">column index.</param>
//        /// <param name="shipNumber">Ship's index number.</param>
//        private void SetShipVertical(int row1, int row2, int column, int shipNumber)
//        {
//            for (int i = Math.Min(row1, row2); i <= Math.Max(row1, row2); i++)
//            {
//                shipsPlacement[i, column] = shipNumber;
//                ownGrid[i, column] = '#';
//            }
//        }
//        /// <summary>
//        /// Set (place) a ship on a grid on selected position (horizontally).
//        /// </summary>
//        /// <param name="row">row index.</param>
//        /// <param name="column1">starting column index.</param>
//        /// <param name="column2">ending column index.</param>
//        /// <param name="shipNumber">Ship's index number.</param>
//        private void SetShipHorizontal(int row, int column1, int column2, int shipNumber)
//        {
//            for (int i = Math.Min(column1, column2); i <= Math.Max(column1, column2); i++)
//            {
//                shipsPlacement[row, i] = shipNumber;
//                ownGrid[row, i] = '#';
//            }
//        }
//    }
//}
