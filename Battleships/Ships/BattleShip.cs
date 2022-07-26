using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Ships
{
    /// <summary>
    /// Ship with 5 squares.
    /// </summary>
    public class BattleShip : Ship
    {
        /// <summary>
        /// Create new object of BattleShip.
        /// </summary>
        public BattleShip()
        {
            Size = 5;
            IsAlive = true;
            Name = "Battleship";
            Holes = 0;
        }
    }
}
