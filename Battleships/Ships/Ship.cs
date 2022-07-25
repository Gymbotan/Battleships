using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Ships
{
    public abstract class Ship
    {
        private byte size;
        public byte Size
        {
            get
            {
                return size;
            }
            set
            {
                if (this.size == 0)
                {
                    size = value;
                }
            }
        }
        public bool IsAlive { get; set; }
        public bool IsSet { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
    }
}
