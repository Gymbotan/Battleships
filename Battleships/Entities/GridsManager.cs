using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Entities
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
        /// <param name="coordinate">Coordinate you fired.</param>
        /// <param name="isHit">result of attack.</param>
        public void ChangeEnemyGrid(Coordinate coordinate, bool isHit)
        {
            enemyGrid[coordinate.Row - 1, coordinate.Column - 1] = (isHit || enemyGrid[coordinate.Row - 1, coordinate.Column - 1] == '@') ? '@' : '·';
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

            for (int i = 0; i < Ships.Length; i++)
            {
                Ships[i].IsSet = false;
            }
        }

        /// <summary>
        /// Reaction on getting shot.
        /// </summary>
        /// <param name="coordinate">Coordinate the enemy fired.</param>
        /// <returns>Result of attack (is enemy hit, is ship sink, is you lose).</returns>
        public (bool, bool, bool) GetShot(Coordinate coordinate)
        {
            if (coordinate.Row < 1 || coordinate.Row > 10 || coordinate.Column < 1 || coordinate.Column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate));
            }
            
            bool isHit = false;
            bool isDead = false;
            bool isLose = false;
            int HittedShipIndex = shipsPlacement[coordinate.Row - 1, coordinate.Column - 1];

            ownGrid[coordinate.Row - 1, coordinate.Column - 1] = (shipsPlacement[coordinate.Row - 1, coordinate.Column - 1] > 0 
                || ownGrid[coordinate.Row - 1, coordinate.Column - 1] == '@') ? '@' : '·';

            if (HittedShipIndex <= 0)
            {
                return (isHit, isDead, isLose);
            }
            else
            {
                shipsPlacement[coordinate.Row - 1, coordinate.Column - 1] = -1;
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
        /// <param name="shipIndex">Ship's index.</param>
        /// <param name="coordinate1">Coordinate1 (one ship's endpoint)</param>
        /// <param name="coordinate2">Coordinate2 (another ship's endpoint)</param>
        public void SetShip(int shipIndex, Coordinate coordinate1, Coordinate coordinate2)
        {
            if (shipIndex < 1 || shipIndex > Ships.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(shipIndex), $"ShipIndex should be between 1 and {Ships.Length}.");
            }

            if (Ships[shipIndex - 1].IsSet)
            {
                throw new ArgumentException("This ship is already set.");
            }

            if (coordinate1.Row < 1 || coordinate1.Row > 10 || coordinate1.Column < 1 || coordinate1.Column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate1));
            }

            if (coordinate2.Row < 1 || coordinate2.Row > 10 || coordinate2.Column < 1 || coordinate2.Column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate2));
            }

            if (coordinate1.Row != coordinate2.Row && coordinate1.Column != coordinate2.Column)
            {
                throw new ArgumentException("A ship should have the same row or the same column.");
            }

            if (Math.Abs(coordinate1.Row - coordinate2.Row) + Math.Abs(coordinate1.Column - coordinate2.Column) != Ships[shipIndex - 1].Size - 1)
            {
                throw new ArgumentException("This ship has different size.");
            }

            if (!IsPossibleToSetShip(coordinate1, coordinate2))
            {
                throw new ArgumentException("You can not place your ship here. You collide with another ship. please try again.");
            }

            if (coordinate1.Row == coordinate2.Row)
            {
                for (int i = Math.Min(coordinate1.Column, coordinate2.Column); i <= Math.Max(coordinate1.Column, coordinate2.Column); i++)
                {
                    shipsPlacement[coordinate1.Row - 1, i - 1] = shipIndex;
                    ownGrid[coordinate1.Row - 1, i - 1] = '#';
                }
            }
            else
            {
                for (int i = Math.Min(coordinate1.Row, coordinate2.Row); i <= Math.Max(coordinate1.Row, coordinate2.Row); i++)
                {
                    shipsPlacement[i - 1, coordinate1.Column - 1] = shipIndex;
                    ownGrid[i - 1, coordinate1.Column - 1] = '#';
                }
            }
            Ships[shipIndex - 1].IsSet = true;
        }

        /// <summary>
        /// Show how own and enemy's grids look like.
        /// </summary>
        public void ShowGrids()
        {
            Console.WriteLine("\nIt is a current situation of your grid:\n");
            ShowGrid(ownGrid);
            Console.WriteLine("\nIt is a current situation of enemy's grid:\n");
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

        /// <summary>
        /// Check is is posiible to place a ship in choosen coorddinates.
        /// </summary>
        /// <param name="coordinate1">Coordinate1 (one ship's endpoint).</param>
        /// <param name="coordinate2">Coordinate2 (another ship's endpoint).</param>
        /// <returns></returns>
        public bool IsPossibleToSetShip(Coordinate coordinate1, Coordinate coordinate2)
        {
            if (coordinate1.Row < 1 || coordinate1.Row > 10 || coordinate1.Column < 1 || coordinate1.Column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate1));
            }
            
            if (coordinate2.Row < 1 || coordinate2.Row > 10 || coordinate2.Column < 1 || coordinate2.Column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinate2));
            }
            
            if (coordinate1.Row == coordinate2.Row)
            {
                for (int i = Math.Min(coordinate1.Column, coordinate2.Column); i <= Math.Max(coordinate1.Column, coordinate2.Column); i++)
                {
                    if (shipsPlacement[coordinate1.Row - 1, i - 1] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (coordinate1.Column == coordinate2.Column)
            {
                for (int i = Math.Min(coordinate1.Row, coordinate2.Row); i <= Math.Max(coordinate1.Row, coordinate2.Row); i++)
                {
                    if (shipsPlacement[i - 1, coordinate1.Column - 1] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false; // remove
        }

        /// <summary>
        /// Check is this square's coordinate was shooted previously.
        /// </summary>
        /// <param name="coordinate">Coordinate.</param>
        /// <returns>Was shooted or not.</returns>
        public bool isSquareFired (Coordinate coordinate)
        {
            return enemyGrid[coordinate.Row - 1, coordinate.Column - 1] != ' ';
        }
    }
}
