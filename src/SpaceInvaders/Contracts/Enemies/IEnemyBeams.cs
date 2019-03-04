using SpaceInvaders.Models.Entities.Base;
using System.Collections.Generic;
using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemyBeams
    {
        /// <summary>
        /// Invokes random enemies to shoot beams
        /// </summary>
        /// <param name="positions">The enemies positions</param>
        void Invoke(List<IPosition> positions);

        /// <summary>
        /// Check if an enemy beam has destroyed the ship
        /// </summary>
        /// <param name="shipPosition">The ship position</param>
        /// <returns>If the ship is destroyed</returns>
        bool HasDestroyedShip(IPosition shipPosition);

        /// <summary>
        /// Deletes the beams given from the canvas
        /// </summary>
        /// <param name="beams">The beams to delete</param>
        void DeleteBeams(List<BeamBase> beams);
    }
}
