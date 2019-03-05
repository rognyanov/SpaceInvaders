using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public sealed class EnemyBeams : EnemyBeamsBase
    {
        public EnemyBeams(int level, IRenderer<string> renderer)
            :base(renderer)
        {
            _moveTimer = new Timer(MOVE_SPEED);
            _maxBeamsCount = INIT_NUM_OF_BEAMS + (level-1);
            _endRow = LOWER_BOUNDARY;
        }
    }
}