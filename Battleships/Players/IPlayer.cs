using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    public interface IPlayer
    {
        public (int, int) Attack(); //{int,int)
        public (bool, bool, bool) GetShot(int row, int column);
        public void SetShip();
        public void DeleteShips();
        public void ChangeOwnGrid(int row, int column);
        public void ChangeEnemyGrid(int row, int column, bool isHit);
        public void ShowGrids();
        public bool IsReadyToPlay();
    }
}
