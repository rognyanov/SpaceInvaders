using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IUfo : IGameObject, IRenderable, IInvokable
    {
        bool IsNotFlying();
        bool IsDestroyed(IPosition beamPosition);
    }
}
