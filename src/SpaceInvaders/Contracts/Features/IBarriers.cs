using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IBarriers : IRenderable
    {
        /// <summary>
        /// Check if the beam destroyed a part form the barrier
        /// </summary>
        /// <param name="beamPosition">The fired beam position.</param>
        /// <returns>Whether a part is destroyed.</returns>
        bool IsBarrierPartDestroyed(IPosition beamPosition);
    }
}