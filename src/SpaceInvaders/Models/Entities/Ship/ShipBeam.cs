using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders.Models.Entities.Ship
{
    public class ShipBeam : BeamBase
    {
        private const int INIT_Y = 57;

        public ShipBeam(int x, IRenderer<string> renderer)
            :base(new ConsolePosition(x+3, INIT_Y), renderer)
        {
            _beam = '|';
            _increment = -1;
        }
        
        public override void Render()
        {
            base.Render();
        }

        public override void Unrender()
        {
            base.Unrender();
        }
    }
}
