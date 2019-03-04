using System.Collections.Generic;
using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Models.Entities.Base;

namespace SpaceInvaders.Contracts.Player
{
    public interface IShip : IGameObject, IMovable, IPlayer, IRenderable
    {
        void ReInitialize();
        void RenderBeams();
        void MoveBeams();
        void DeleteBeams(List<BeamBase> beams);
        List<BeamBase> GetBeams();
    }
}
