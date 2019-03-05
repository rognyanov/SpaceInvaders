using System.Collections.Generic;
using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Visual;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemies : IMovable, IRenderable
    {
        /// <summary>
        /// Moves the enemies positions up (Used when the enemy touches the ship)
        /// </summary>
        void MoveUp();

        /// <summary>
        /// Check if enemy is destoryed base on the beam position
        /// </summary>
        /// <param name="beamPosition"></param>
        /// <returns>If enemy is destoryed</returns>
        bool IsDestroyed(IPosition beamPosition);

        /// <summary>
        /// Check if enemy beam has hit the ship by its position
        /// </summary>
        /// <param name="shipPosition"></param>
        /// <returns>If ship is destroyed</returns>
        bool HasDestroyedShip(IPosition shipPosition);

        /// <summary>
        /// Check if there are any enemies left
        /// </summary>
        /// <returns>If there are no enemies.</returns>
        bool IsEmpty();

        /// <summary>
        /// Reset the timers that are used to move the enemies up
        /// </summary>
        void ResetMoveUp();

        /// <summary>
        /// Get the positions of the enemies
        /// </summary>
        /// <returns>Collection of enemies' positions</returns>
        List<IPosition> GetPositions();

        void ReInitialize(int level);
    }
}