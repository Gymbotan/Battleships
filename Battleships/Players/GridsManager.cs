using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public class GridsManager
    {
        private readonly int[,] shipsPlacement;
        private readonly char[,] ownGrid;
        private readonly char[,] enemyGrid;
        public Ship[] Ships { get; }

        /// <summary>
        /// Create new object of RealPlayer.
        /// </summary>
        public GridsManager()
        {
            ownGrid = new char[10, 10];
            enemyGrid = new char[10, 10];
            shipsPlacement = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ownGrid[i, j] = ' ';
                    enemyGrid[i, j] = ' ';
                }
            }
            Ships = new Ship[3] { new Ship(5, "Battleship"), new Ship(4, "Destroyer"), new Ship(4, "Destroyer") };
        }

        /// <summary>
        /// Changing of enemy grid after getting results of your attack.
        /// </summary>
        /// <param name="row">row you fired.</param>
        /// <param name="column">column you fired.</param>
        /// <param name="isHit">result of attack.</param>
        public void ChangeEnemyGrid(int row, int column, bool isHit)
        {
            enemyGrid[row - 1, column - 1] = (isHit || enemyGrid[row - 1, column - 1] == '@') ? '@' : '·';
        }

        /// <summary>
        /// Delete all the ships from own grid.
        /// </summary>
        public void DeleteShips()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ownGrid[i, j] = ' ';
                    shipsPlacement[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Reaction on getting shot.
        /// </summary>
        /// <param name="row">row the enemy fired.</param>
        /// <param name="column">column the enemy fired.</param>
        /// <returns>Result of attack (is enemy hit, is ship sink, is you lose).</returns>
        public (bool, bool, bool) GetShot(int row, int column)
        {
            if (row < 1 || row > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }
            if (column < 1 || column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            bool isHit = false;
            bool isDead = false;
            bool isLose = false;
            int HittedShipIndex = shipsPlacement[row - 1, column - 1];

            ownGrid[row - 1, column - 1] = (shipsPlacement[row - 1, column - 1] > 0 || ownGrid[row - 1, column - 1] == '@') ? '@' : '·';

            if (HittedShipIndex <= 0)
            {
                return (isHit, isDead, isLose);
            }
            else
            {
                shipsPlacement[row - 1, column - 1] = -1;
                isHit = true;
                Ships[HittedShipIndex - 1].Holes++;
                if (Ships[HittedShipIndex - 1].Holes == Ships[HittedShipIndex - 1].Size)
                {
                    Ships[HittedShipIndex - 1].IsAlive = false;
                    isDead = true;
                }

                if (Ships.All(s => !s.IsAlive))
                {
                    isLose = true;
                }

                return (isHit, isDead, isLose);
            }
        }

        /// <summary>
        /// Set (place) a ship on player's grid.
        /// </summary>
        public void SetShip(int shipIndex, int row1, int column1, int row2, int column2)
        {
            if (shipIndex < 1 || shipIndex > Ships.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(shipIndex), $"ShipIndex should be between 1 and {Ships.Length}.");
            }

            if (row1 != row2 && column1 != column2)
            {
                throw new ArgumentException("A ship should have the same row or the same column");
            }

            if (Math.Abs (row1 - row2) + Math.Abs(column1 - column2) != Ships[shipIndex - 1].Size - 1)
            {
                throw new ArgumentException("This ship has different size.");
            }

            if (!IsPossibleToSetShip(row1, column1, row2, column2))
            {
                throw new ArgumentException("You can not place your ship here. You collide with another ship. please try again.");
            }

            if (row1 == row2)
            {
                for (int i = Math.Min(column1, column2); i <= Math.Max(column1, column2); i++)
                {
                    shipsPlacement[row1 - 1, i - 1] = shipIndex;
                    ownGrid[row1 - 1, i - 1] = '#';
                }
            }
            else 
            {
                for (int i = Math.Min(row1, row2); i <= Math.Max(row1, row2); i++)
                {
                    shipsPlacement[i - 1, column1 - 1] = shipIndex;
                    ownGrid[i - 1, column1 - 1] = '#';
                }
            }
            Ships[shipIndex - 1].IsSet = true;
        }

        /// <summary>
        /// Return own grid.
        /// </summary>
        /// <returns>Own grid.</returns>
        public char[,] GetOwnGrid() => ownGrid;

        /// <summary>
        /// Return enemy grid.
        /// </summary>
        /// <returns>Enemy grid.</returns>
        public char[,] GetEnemyGrid() => enemyGrid;

        /// <summary>
        /// Show how own and enemy's grids look like.
        /// </summary>
        public void ShowGrids()
        {
            Console.WriteLine("\nIt is a current situation of your grid:");
            ShowGrid(ownGrid);
            Console.WriteLine("\nIt is a current situation of enemy's grid:");
            ShowGrid(enemyGrid);
        }

        /// <summary>
        /// Show how choosen grid looks like.
        /// </summary>
        /// <param name="grid">Choosen grid.</param>
        private void ShowGrid(char[,] grid)
        {
            Console.WriteLine("   12345678910");
            Console.WriteLine("  _____________");
            for (int i = 0; i < 10; i++)
            {
                char ch = (char)('A' + i);
                Console.Write($" {ch}|");
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i, j] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{grid[i, j]}", Console.ForegroundColor);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{grid[i, j]}");
                    }
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("  -------------");
        }

        /// <summary>
        /// Check is this player ready to play (are all the ships placed on the grid.).
        /// </summary>
        /// <returns>Is ready or no.</returns>
        public bool IsReadyToPlay()
        {
            return Ships.All(x => x.IsSet);
        }

        public bool IsPossibleToSetShip(int row1, int column1, int row2, int column2)
        {
            if (row1 < 1 || row1 > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(row1));
            }
            if (column1 < 1 || column1 > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(column1));
            }
            if (row2 < 1 || row2 > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(row2));
            }
            if (column2 < 1 || column2 > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(column2));
            }

            if (row1 == row2)
            {
                for (int i = Math.Min(column1, column2); i <= Math.Max(column1, column2); i++)
                {
                    if (shipsPlacement[row1 - 1, i - 1] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (column1 == column2)
            {
                for (int i = Math.Min(row1, row2); i <= Math.Max(row1, row2); i++)
                {
                    if (shipsPlacement[i - 1, column1 - 1] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false; // remove
        }

        public bool isSquareFired (int row, int column)
        {
            return enemyGrid[row - 1, column - 1] != ' ';
        }
    }
}
