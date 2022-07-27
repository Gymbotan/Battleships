using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Ships
{
    /// <summary>
    /// Base class for all the ships.
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Ship size.
        /// </summary>
        private int size;

        public Ship(int size, string name)
        {
            Size = size;
            IsAlive = true;
            Name = name;
            Holes = 0;
        }

        /// <summary>
        /// Ship size. Can be set only once.
        /// </summary>
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Ship size should be a positive number.");
                }

                if (this.size == 0)
                {
                    size = value;
                }
            }
        }

        /// <summary>
        /// Show is this ship still alive.
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Show is this ship placed on a grid.
        /// </summary>
        public bool IsSet { get; set; }

        /// <summary>
        /// Ship's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Show how many time enemy hit this ship.
        /// </summary>
        public int Holes { get; set; }
    }
}
