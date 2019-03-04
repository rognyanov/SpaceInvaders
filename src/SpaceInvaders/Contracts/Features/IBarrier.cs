using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IBarrier : IGameObject, IRenderable
    {
        /// <summary>
        /// Check if the beam hit the barrier
        /// </summary>
        /// <param name="beamPosition">The beam fired</param>
        /// <returns>If a part of the barrier is hit</returns>
        bool IsSet(IPosition beamPosition);

        /// <summary>
        /// Set the barrier grid piece to 0, so it disappears
        /// </summary>
        /// <param name="beamPosition">The fired beam</param>
        void SetTaken(IPosition beamPosition);
    }
}
