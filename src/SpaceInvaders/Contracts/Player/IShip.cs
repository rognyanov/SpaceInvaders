using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Models.Entities.Base;
using System.Collections.Generic;

namespace SpaceInvaders.Contracts.Player
{
    public interface IShip : IGameObject, IMovable, IPlayer, IRenderable
    {
        /// <summary>
        /// Reinitializes the ship position
        /// </summary>
        void ReInitialize();

        /// <summary>
        /// Renders the fired beams
        /// </summary>
        void RenderBeams();

        /// <summary>
        /// Moves the fired beams
        /// </summary>
        void MoveBeams();

        /// <summary>
        /// Delete beams from the collection
        /// </summary>
        /// <param name="beams">The beams to delete</param>
        void DeleteBeams(List<BeamBase> beams);

        /// <summary>
        /// Get the beams collection
        /// </summary>
        /// <returns>The contained beams collection</returns>
        List<BeamBase> GetBeams();
    }
}
