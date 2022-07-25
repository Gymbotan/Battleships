﻿using Battleships.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public class ComputerRookiePlayer : IPlayer
    {
        private readonly int[,] shipsPlacement;
        private readonly char[,] ownGrid;
        private readonly char[,] enemyGrid;
        private readonly Ship[] ships;

        public ComputerRookiePlayer()
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
            Random rnd = new();
            int row, column;
            while (true)
            {
                row = rnd.Next(0, 9);
                column = rnd.Next(0, 9);
                if (enemyGrid[row, column] == ' ')
                {
                    return (row, column);
                }
            }
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
            DeleteShips();
            for (int i = 0; i < ships.Length; i++)
            {
                ChooseShipPlacement(ships[i].Size, i + 1);
                ships[i].IsSet = true;
            }
        }

        private void ChooseShipPlacement(int shipSize, int shipNumber)
        {
            // TODO: add checking is ships collide with each other
            bool isSet = false;
            while (!isSet)
            {
                // we choose a random point on the grid. It will be initial endpoint of the ship
                Random rnd = new();
                int row1 = rnd.Next(0, 9);
                int column1 = rnd.Next(0, 9);
                int row2, column2;
                int count = 0;
                while(!isSet && count < 5) // if we can not set the ship, maybe it is imposiible. Let's choose a new initial point
                {
                    count++;
                    switch (rnd.Next(1,4)) // we randomly choose direction where to set the ship from initial point
                    {
                        case 1: // up
                            if (row1 - shipSize + 1 < 0)
                            { 
                                break;
                            }
                            else
                            {
                                row2 = row1 - shipSize + 1; 
                                SetShipVertical(row1, row2, column1, shipNumber);
                                isSet = true;
                                break;
                            }
                        case 2: // down
                            if (row1 + shipSize - 1 > 9)
                            {
                                break;
                            }
                            else
                            {
                                row2 = row1 + shipSize - 1;
                                SetShipVertical(row1, row2, column1, shipNumber);
                                isSet = true;
                                break;
                            }
                        case 3: // left
                            if (column1 - shipSize + 1 < 0)
                            {
                                break;
                            }
                            else
                            {
                                column2 = column1 - shipSize + 1;
                                SetShipHorizontal(row1, column1, column2, shipNumber);
                                isSet = true;
                                break;
                            }
                        case 4: // right
                            if (row1 + shipSize - 1 > 9)
                            {
                                break;
                            }
                            else
                            {
                                column2 = row1 + shipSize - 1;
                                SetShipHorizontal(row1, column1, column2, shipNumber);
                                isSet = true;
                                break;
                            }
                    }
                }
            }
        }

        private void SetShipVertical(int row1, int row2, int column, int shipNumber)
        {
            for (int i = Math.Min(row1, row2); i < Math.Max(row1, row2); i++)
            {
                shipsPlacement[i, column] = shipNumber;
                ownGrid[i, column] = '#';
            }
        }

        private void SetShipHorizontal(int row, int column1, int column2, int shipNumber)
        {
            for (int i = Math.Min(column1, column2); i < Math.Max(column1, column2); i++)
            {
                shipsPlacement[row, i] = shipNumber;
                ownGrid[row, i] = '#';
            }
        }

        public void ShowGrids()
        {
            Console.WriteLine("You can not see computer's ships' location!");
        }

        public bool IsReadyToPlay()
        {
            return ships.All(x => x.IsSet);
        }

        //private void SetShipIntoGrids(int row1, int column1, int row2, int column2)
        //{
        //    throw new NotImplementedException();
        //}

        //private int CharToInt(char ch) => ch switch
        //{
        //    'a' => 1,
        //    'b' => 2,
        //    'c' => 3,
        //    'd' => 4,
        //    'e' => 5,
        //    'f' => 6,
        //    'g' => 7,
        //    'h' => 8,
        //    'i' => 9,
        //    'j' => 10,
        //    _ => throw new ArgumentOutOfRangeException(nameof(ch)),
        //};

        //private (int, int) InputCoordinates()
        //{
        //    int column;
        //    char charRow;

        //    while (true)
        //    {
        //        var input = Console.ReadLine().ToLower();
        //        charRow = input[0];
        //        if (charRow < 'a' || charRow > 'j')
        //        {
        //            Console.WriteLine("You inputed wrong coordinates. The first coordinate should be from 'a' to 'j'. Please try again.");
        //            continue;
        //        }

        //        if (!int.TryParse(input.Substring(1), out column))
        //        {
        //            Console.WriteLine("Can not recognize second coordinate as a number. Please try again.");
        //            continue;
        //        }

        //        if (column < 1 || column > 10)
        //        {
        //            Console.WriteLine("You inputed wrong coordinates. The second coordinate should be from '1' to '10'. Please try again.");
        //            continue;
        //        }

        //        break;
        //    }

        //    int row = CharToInt(charRow);
        //    return (row, column);
        //}
    }
}
