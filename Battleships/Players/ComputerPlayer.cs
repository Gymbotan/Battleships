#pragma warning disable CS0168
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    /// <summary>
    /// Class computer player.
    /// </summary>
    public class ComputerPlayer : IPlayer
    {
        /// <summary>
        /// Grids manager.
        /// </summary>
        public GridsManager gridsManager { get; }

        /// <summary>
        /// Create object of ComputerPlayer.
        /// </summary>
        public ComputerPlayer()
        {
            gridsManager = new GridsManager();
        }

        /// <summary>
        /// Choose coordinates to fire opponent.
        /// </summary>
        /// <returns>Coordinates you want to fire.</returns>
        public (int, int) Attack()
        {
            Random rnd = new();
            int row, column;
            while (true)
            {
                row = rnd.Next(1, 10);
                column = rnd.Next(1, 10);
                if (!gridsManager.isSquareFired(row, column))
                {
                    return (row, column);
                }
            }
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
            gridsManager.DeleteShips();
            for (int i = 0; i < gridsManager.Ships.Length; i++)
            {
                ChooseShipPlacement(gridsManager.Ships[i].Size, i + 1);
                gridsManager.Ships[i].IsSet = true;
            }
        }

        /// <summary>
        /// Randomly choose place for a ship.
        /// </summary>
        /// <param name="shipSize">Size of a ship.</param>
        /// <param name="shipNumber">Index number of a ship.</param>
        private void ChooseShipPlacement(int shipSize, int shipNumber)
        {
            bool isSet = false;
            while (!isSet)
            {
                // we choose a random point on the grid. It will be initial endpoint of the ship
                Random rnd = new();
                int row1 = rnd.Next(1, 10);
                int column1 = rnd.Next(1, 10);
                int row2, column2;
                int count = 0;
                while (!isSet && count < 5) // if we can not set (place) the ship, maybe it is imposiible. Let's choose a new initial point
                {
                    count++;
                    switch (rnd.Next(1, 4)) // we randomly choose direction where to set the ship from initial point
                    {
                        case 1: // up
                            if (row1 - shipSize + 1 < 1)
                            {
                                break;
                            }
                            else
                            {
                                row2 = row1 - shipSize + 1;
                                try
                                {
                                    gridsManager.SetShip(shipNumber, row1, column1, row2, column1);
                                    isSet = true;
                                }
                                catch(Exception ex)
                                {
                                    // just do nothing
                                }

                                break;
                            }
                        case 2: // down
                            if (row1 + shipSize - 1 > 10)
                            {
                                break;
                            }
                            else
                            {
                                row2 = row1 + shipSize - 1;
                                try
                                {
                                    gridsManager.SetShip(shipNumber, row1, column1, row2, column1);
                                    isSet = true;
                                }
                                catch (Exception ex)
                                {
                                    // just do nothing
                                }
                                
                                break;
                            }
                        case 3: // left
                            if (column1 - shipSize + 1 < 1)
                            {
                                break;
                            }
                            else
                            {
                                column2 = column1 - shipSize + 1;
                                try
                                {
                                    gridsManager.SetShip(shipNumber, row1, column1, row1, column2);
                                    isSet = true;
                                }
                                catch (Exception ex)
                                {
                                    // just do nothing
                                }

                                break;
                            }
                        case 4: // right
                            if (row1 + shipSize - 1 > 10)
                            {
                                break;
                            }
                            else
                            {
                                column2 = row1 + shipSize - 1;
                                try
                                {
                                    gridsManager.SetShip(shipNumber, row1, column1, row1, column2);
                                    isSet = true;
                                }
                                catch (Exception ex)
                                {
                                    // just do nothing
                                }

                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Show how own and enemy's grids look like.
        /// </summary>
        public void ShowGrids()
        {
            gridsManager.ShowGrids();
        }

        /// <summary>
        /// Check is this player ready to play (are all the ships placed on the grid.).
        /// </summary>
        /// <returns>Is ready or not.</returns>
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
