using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    /// <summary>
    /// Interface of battleship player.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Choose and return coordinates to attack.
        /// </summary>
        /// <returns>Coordinates to attack.</returns>
        public (int, int) Attack();

        /// <summary>
        /// Reaction on shooting into player.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>Result of attack (is enemy hit, is ship sink, is player lose).</returns>
        public (bool, bool, bool) GetShot(int row, int column);

        /// <summary>
        /// Set (place) ship on a grid.
        /// </summary>
        public void SetShip();

        /// <summary>
        /// Delete ships from a grid.
        /// </summary>
        public void DeleteShips();

        /// <summary>
        /// Make changes on own grid after enemy's attack.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        public void ChangeOwnGrid(int row, int column);

        /// <summary>
        /// Make changes on enemy grid after getting results of your attack.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <param name="isHit">Are you hit enemy's ship.</param>
        public void ChangeEnemyGrid(int row, int column, bool isHit);

        /// <summary>
        /// Show current situation on grids.
        /// </summary>
        public void ShowGrids();

        /// <summary>
        /// Check is player ready to play (all ships are set on a grid.
        /// </summary>
        /// <returns></returns>
        public bool IsReadyToPlay();
    }
}
