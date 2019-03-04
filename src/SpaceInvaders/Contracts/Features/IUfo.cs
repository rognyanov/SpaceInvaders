using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IUfo : IGameObject, IRenderable, IInvokable
    {
        /// <summary>
        /// Check if the ufo is not flying at the moment
        /// </summary>
        /// <returns>Whether the ufo is flying</returns>
        bool IsNotFlying();

        /// <summary>
        /// Check if the ufo is destroyed
        /// </summary>
        /// <param name="beamPosition">The fired beam position</param>
        /// <returns>Whether the ufo is destroyed</returns>
        bool IsDestroyed(IPosition beamPosition);
    }
}
