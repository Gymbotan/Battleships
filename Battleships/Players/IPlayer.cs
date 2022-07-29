using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public interface IPlayer
    {
        public GridsManager gridsManager { get; }
        public (int, int) Attack();
        public void SetShips();
        public void ChangeEnemyGrid(int row, int column, bool isHit);
        public (bool, bool, bool) GetShot(int row, int column);
        public void ShowGrids();
        public void DeleteShips();
        public bool isReadyToPlay();
    }
}
