using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Features
{
    public interface IBarriers : IRenderable
    {
        bool IsBarrierPartDestroyed(IPosition beamPosition);
    }
}