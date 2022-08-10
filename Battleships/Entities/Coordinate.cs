using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Entities
{
    public struct Coordinate
    {
        public int Row { get; private set; }

        public int Column { get; private set; }

        public Coordinate(int row, int column)
        {
            if (row < 1 || row > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column < 1 || column > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            Row = row;
            Column = column;
        }
    }
}
