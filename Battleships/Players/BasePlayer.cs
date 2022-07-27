using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public abstract class BasePlayer
    {
        protected readonly int[,] shipsPlacement;
        protected readonly char[,] ownGrid;
        protected readonly char[,] enemyGrid;
        protected readonly Ship[] ships;

        /// <summary>
        /// Create new object of RealPlayer.
        /// </summary>
        protected BasePlayer()
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
            ships = new Ship[2] { new Ship(5, "Battleship"), new Ship(4, "Destroyer") };
        }

        /// <summary>
        /// Attack of enemy grid. You should input coordinates you want to fire.
        /// </summary>
        /// <returns> Coordinates (row, column) you want to attack.</returns>
        public abstract (int, int) Attack(); 
        
        /// <summary>
        /// Changing of enemy grid after getting results of your attack.
        /// </summary>
        /// <param name="row">row you fired.</param>
        /// <param name="column">column you fired.</param>
        /// <param name="isHit">result of attack.</param>
        public void ChangeEnemyGrid(int row, int column, bool isHit)
        {
            enemyGrid[row, column] = isHit ? '@' : '·';
        }

        /// <summary>
        /// Changing of own grid after getting enemy shot.
        /// </summary>
        /// <param name="row">row the enemy fired.</param>
        /// <param name="column">column the enemy fired.</param>
        protected void ChangeOwnGrid(int row, int column)
        {
            ownGrid[row, column] = shipsPlacement[row, column] != 0 ? '@' : '·';
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
            bool isHit = false;
            bool isDead = false;
            bool isLose = false;
            int placeOfShot = shipsPlacement[row - 1, column - 1];
            if (placeOfShot <= 0)
            {
                return (isHit, isDead, isLose);
            }
            else
            {
                shipsPlacement[row, column] = -1;
                isHit = true;
                ships[placeOfShot - 1].Holes++;
                if (ships[placeOfShot - 1].Holes == ships[placeOfShot - 1].Size)
                {
                    ships[placeOfShot - 1].IsAlive = false;
                    isDead = true;
                }

                if (ships.All(s => !s.IsAlive))
                {
                    isLose = true;
                }

                return (isHit, isDead, isLose);
            }
        }

        /// <summary>
        /// Set (place) a ship on player's grid.
        /// </summary>
        public abstract void SetShip();
        
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
                    Console.Write($"{grid[i, j]}");
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
            return ships.All(x => x.IsSet);
        }
    }
}
