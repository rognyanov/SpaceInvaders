using System.Collections.Generic;
using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Ship
{
    public class ShipBeams : BeamsBase
    {
        private const int MOVE_SPEED = 1;
        private const int UPPER_BOUNDARY = 6; //TODO: When header added must change this value to header boundary

        public ShipBeams(IRenderer<string> renderer)
            : base(renderer)
        {
            _maxBeamsCount = 3;
            _moveTimer = new Timer(MOVE_SPEED);
            _endRow = UPPER_BOUNDARY;
        }

        public void Add(int x)
        {
            if (_beamsCount >= _maxBeamsCount)
                return;

            _beams.Add(new ShipBeam(x, _renderer));
            _beamsCount++;
        }

        public void Delete(BeamBase beam)
        {
            _beams.Remove(beam);
        }

        public void DeleteBeams(List<BeamBase> beams)
        {
            foreach (var beam in beams)
            {
                Delete(beam);
                _beamsCount--;
            }
        }
    }
}
