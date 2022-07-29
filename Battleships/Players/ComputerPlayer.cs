using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public class ComputerPlayer : IPlayer
    {
        public GridsManager gridsManager { get; }

        public ComputerPlayer()
        {
            gridsManager = new GridsManager();
        }

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

        public void ChangeEnemyGrid(int row, int column, bool isHit)
        {
            gridsManager.ChangeEnemyGrid(row, column, isHit);
        }

        public (bool, bool, bool) GetShot(int row, int column)
        {
            return gridsManager.GetShot(row, column);
        }

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
                                    //Console.WriteLine(ex.Message);
                                    // just do nothing
                                }

                                //if (IsPossibleToSetShip(row1 + 1, column1 + 1, row2 + 1, column1 + 1))
                                //{
                                //    SetShipVertical(row1, row2, column1, shipNumber);
                                //    isSet = true;
                                //}
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
                                    //Console.WriteLine(ex.Message);
                                    // just do nothing
                                }
                                //if (IsPossibleToSetShip(row1 + 1, column1 + 1, row2 + 1, column1 + 1))
                                //{
                                //    SetShipVertical(row1, row2, column1, shipNumber);
                                //    isSet = true;
                                //}
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
                                    //Console.WriteLine(ex.Message);
                                    // just do nothing
                                }

                                //if (IsPossibleToSetShip(row1 + 1, column1 + 1, row1 + 1, column2 + 1))
                                //{
                                //    SetShipHorizontal(row1, column1, column2, shipNumber);
                                //    isSet = true;
                                //}
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
                                    //Console.WriteLine(ex.Message);// just do nothing
                                }

                                //if (IsPossibleToSetShip(row1 + 1, column1 + 1, row1 + 1, column2 + 1))
                                //{
                                //    SetShipHorizontal(row1, column1, column2, shipNumber);
                                //    isSet = true;
                                //}
                                break;
                            }
                    }
                }
            }
        }

        public void ShowGrids()
        {
            gridsManager.ShowGrids();
        }

        public bool isReadyToPlay()
        {
            return gridsManager.IsReadyToPlay();
        }

        public void DeleteShips()
        {
            gridsManager.DeleteShips();
        }
    }
}
