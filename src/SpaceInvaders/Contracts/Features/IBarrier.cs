using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IBarrier : IGameObject, IRenderable
    {
        bool IsSet(IPosition beamPosition);
        void SetTaken(IPosition beamPosition);
    }
}
