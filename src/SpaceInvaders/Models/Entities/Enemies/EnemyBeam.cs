using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class EnemyBeam : BeamBase
    {
        public EnemyBeam(int x, int y) 
            : base(new ConsolePosition(x,y), new ConsoleRenderer())
        {
            _beam = '*';
            _increment = 1;
        }
    }
}