using Battleships.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Players
{
    /// <summary>
    /// IPlayer interface. For Battleships game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Grids manager.
        /// </summary>
        public GridsManager gridsManager { get; }

        /// <summary>
        /// Choose coordinates to fire opponent.
        /// </summary>
        /// <returns>Coordinates you want to fire.</returns>
        public Coordinate Attack();

        /// <summary>
        /// Set ships on the grid.
        /// </summary>
        public void SetShips();

        /// <summary>
        /// Change enemy grid after getting results of your attack.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="column">Column.</param>
        /// <param name="isHit">Is hit.</param>
        public void ChangeEnemyGrid(Coordinate coordinate, bool isHit);

        /// <summary>
        /// Reaction on getting shot.
        /// </summary>
        /// <param name="row">Row the enemy fired.</param>
        /// <param name="column">Column the enemy fired.</param>
        /// <returns>Result of attack (is enemy hit, is ship sink, is you lose).</returns>
        public (bool, bool, bool) GetShot(Coordinate coordinate);

        /// <summary>
        /// Show how own and enemy's grids look like.
        /// </summary>
        public void ShowGrids();

        /// <summary>
        /// Delete all the ships from your grid.
        /// </summary>
        public void DeleteShips();

        /// <summary>
        /// Check is this player ready to play (are all the ships placed on the grid.).
        /// </summary>
        /// <returns>Is ready or not.</returns>
        public bool isReadyToPlay();
    }
}
