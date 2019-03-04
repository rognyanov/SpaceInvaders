using SpaceInvaders.Models.Grid;
using System.Collections.Generic;
using System.Threading;
using SpaceInvaders.Contracts;

namespace SpaceInvaders.Models.Entities.Features
{
    public class Barriers
    {
        private readonly IRenderer<string> _renderer;
        private const int BARRIERS_COUNT = 4;
        private const int START_X = 16;
        private const int POSITION_OFFSET = 20;
        private const int Y_POS = 51;

        private List<Barrier> _barriers;

        public Barriers(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _barriers = new List<Barrier>();
            int position = 0;

            for (int i = 0; i < BARRIERS_COUNT; i++)
            {
                _barriers.Add(new Barrier(START_X + position * POSITION_OFFSET, Y_POS, _renderer));
                position++;
            }
        }

        public void Render()
        {
            foreach (var barrier in _barriers)
            {
                barrier.Render();
            }
        }

        public bool DeleteBarrierPart(IPosition beamPosition)
        {
            var result = false;

            foreach (var barrier in _barriers)
            {
                if (barrier.IsSet(beamPosition))
                {
                    barrier.SetTaken(beamPosition);
                    result = true;
                }
            }

            return result;
        }
    }
}
