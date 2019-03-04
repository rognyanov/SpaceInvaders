using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders.Models.Entities.Ship
{
    public sealed class ShipBeam : BeamBase
    {
        private const int INIT_Y = 57;

        public ShipBeam(int x, IRenderer<string> renderer)
            :base(new ConsolePosition(x+3, INIT_Y), renderer)
        {
            _beam = '|';
            _increment = -1;
        }
    }
}
