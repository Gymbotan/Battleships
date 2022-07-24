using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Ships
{
    public class BattleShip : Ship
    {
        public BattleShip()
        {
            Size = 5;
            IsAlive = true;
            Name = "Battleship";
        }
    }
}
