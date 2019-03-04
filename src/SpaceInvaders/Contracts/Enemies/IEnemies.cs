using System.Collections.Generic;
using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemies : IMovable, IRenderable
    {
        void MoveUp();
        bool IsDestroyed(IPosition beamPosition);
        bool HasDestroyedShip(IPosition shipPosition);
        bool IsEmpty();
        void ResetMoveUp();
        List<IPosition> GetPositions();
    }
}