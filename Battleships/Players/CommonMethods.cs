using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public static class CommonMethods
    {
        public static void ClearOwnGrids(ref char[,] ownGrid, ref int[,] shipsPlacement)
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
    }
}
