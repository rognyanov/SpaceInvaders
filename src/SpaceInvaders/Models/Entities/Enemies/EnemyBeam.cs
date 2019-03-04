using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public sealed class EnemyBeam : BeamBase
    {
        public EnemyBeam(IPosition position) 
            : base(position, new ConsoleRenderer())
        {
            _beam = '*';
            _increment = 1;
        }
    }
}