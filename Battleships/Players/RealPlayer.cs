﻿using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public class RealPlayer : IPlayer
    {
        private readonly int[,] shipsPlacement;
        private readonly char[,] ownGrid;
        private readonly char[,] enemyGrid;
        private readonly Ship[] ships;

        public RealPlayer()
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
            ships = new Ship[1] { new BattleShip() };
        }

        public (int, int) Attack()
        {
            Console.WriteLine("Input coordinates you want to attack (from a1 to j10):");
            return InputCoordinates();
            //while (true)
            //{
            //    Console.WriteLine("Input coordinates you want to attack (from a1 to j10):");
            //    var input = Console.ReadLine().ToLower();
            //    char charRow = input[0];
            //    if (charRow < 'a' || charRow > 'j')
            //    {
            //        Console.WriteLine("You inputed wrong coordinates. The first coordinate should be from 'a' to 'j'. Please try again.");
            //        continue;
            //    }

            //    int column;
            //    if (!int.TryParse(input.Substring(1), out column))
            //    {
            //        Console.WriteLine("Can not recognize second coordinate as a number. Please try again.");
            //        continue;
            //    }

            //    if (column < 1 || column > 10)
            //    {
            //        Console.WriteLine("You inputed wrong coordinates. The second coordinate should be from '1' to '10'. Please try again.");
            //        continue;
            //    }

            //    int row = CharToInt(charRow);
            //    return (row, column);
            //}
        }

        public void ChangeEnemyGrid(int row, int column, bool isHit)
        {
            if (isHit)
            {
                enemyGrid[row, column] = '@';
            }
            else
            {
                enemyGrid[row, column] = '·';
            }
        }

        public void ChangeOwnGrid(int row, int column)
        {
            if (shipsPlacement[row, column] != 0)
            {
                ownGrid[row, column] = '@';
            }
            else
            {
                ownGrid[row, column] = '·';
            }
        }

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

        public (bool, bool, bool) GetShot(int row, int column)
        {
            bool isHit = false;
            bool isDead = false;
            bool isLose = false;
            int placeOfShot = shipsPlacement[row, column];
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
        
        public void SetShip()
        {
            Console.WriteLine("You have next ships:");
            int count = 1;
            foreach (Ship ship in ships)
            {
                Console.Write("{0} - {1} ({2} squares) {3}", count++, ship.Name, ship.Size, ship.IsSet ? " - is already set.\n" : ".\n");
            }

            bool isQuited = false;
            int shipNumber = 1;

            while (true)
            {
                Console.WriteLine("\nPlease enter a number of the ship you want to set (or 'r' to return to previous menu)");
                var input = Console.ReadLine();
                
                if (input == "r" || input == "R")
                {
                    isQuited = true;
                    break;
                }

                if (!int.TryParse(input, out shipNumber))
                {
                    Console.WriteLine("You inputed not a number. Please try again.");
                    continue;
                }

                if (shipNumber < 1 || shipNumber > ships.Length)
                {
                    Console.WriteLine("There is no ship with that number. Please try again.");
                    continue;
                }
                
                if (ships[shipNumber - 1].IsSet)
                {
                    Console.WriteLine("This ship is already set. Please choose another ship or clear the grid (delete all the ships).");
                    continue;
                }

                break;
            }

            if (!isQuited)
            {
                ChooseShipPlacement(ships[shipNumber - 1].Size, shipNumber);
                ships[shipNumber - 1].IsSet = true;
                Console.WriteLine($"Ship {ships[shipNumber - 1].Name} was successfully set.");
            }
        }

        private void ChooseShipPlacement(int shipSize, int shipNumber)
        {
            // TODO: add checking is ships collide with each other
            while (true)
            {
                Console.WriteLine("\nTo set a ship you should input both endpoint of a ship (consider ship's size)");
                Console.WriteLine("Input first endpoint's coordinates (from a1 to j10):");
                var coordinate1 = InputCoordinates();
                Console.WriteLine("Input second endpoint's coordinates (from a1 to j10):");
                var coordinate2 = InputCoordinates();
                if (coordinate1.Item1 == coordinate2.Item1 && Math.Abs(coordinate1.Item2 - coordinate2.Item2) == shipSize - 1)
                {
                    int row = coordinate1.Item1;
                    int column = Math.Min(coordinate1.Item2, coordinate2.Item2);
                    for (int i = 0; i < shipSize; i++)
                    {
                        shipsPlacement[row - 1, column + i - 1] = shipNumber;
                        ownGrid[row - 1, column + i - 1] = '#';
                    }
                    break;
                }
                else if (coordinate1.Item2 == coordinate2.Item2 && Math.Abs(coordinate1.Item1 - coordinate2.Item1) == shipSize - 1)
                {
                    int row = Math.Min(coordinate1.Item1, coordinate2.Item1);
                    int column = coordinate1.Item2;
                    for (int i = 0; i < shipSize; i++)
                    {
                        shipsPlacement[row + i - 1, column - 1] = shipNumber;
                        ownGrid[row + i - 1, column - 1] = '#';
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Ship can not have such endpoints. Please try again.");
                }
            }
        }

        public void ShowGrids()
        {
            Console.WriteLine("\nIt is a current situation of your grid:");
            ShowGrid(ownGrid);
        }

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

        //private void SetShipIntoGrids(int row1, int column1, int row2, int column2)
        //{
        //    throw new NotImplementedException();
        //}

        private int CharToInt (char ch) => ch switch
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

        private (int, int) InputCoordinates()
        {
            int column;
            char charRow;

            while (true)
            {
                var input = Console.ReadLine().ToLower();
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

        public bool IsReadyToPlay()
        {
            return ships.All(x => x.IsSet);
        }
    }
}