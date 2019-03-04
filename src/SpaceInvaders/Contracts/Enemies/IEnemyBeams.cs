using SpaceInvaders.Models.Entities.Base;
using System.Collections.Generic;
using SpaceInvaders.Contracts.Base;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemyBeams
    {
        void Invoke(List<IPosition> positions);
        bool HasDestroyedShip(IPosition shipPosition);
        void DeleteBeams(List<BeamBase> beams);
    }
}
