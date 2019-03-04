using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Features;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Grid;
using System.Collections.Generic;

namespace SpaceInvaders.Models.Entities.Features
{
    public sealed class Barriers : IBarriers
    {
        private const int BARRIERS_COUNT = 4;
        private const int START_X = 16;
        private const int POSITION_OFFSET = 20;
        private const int Y_POS = 51;

        private readonly List<IBarrier> _barriers;

        public Barriers(IRenderer<string> renderer)
        {
            _barriers = new List<IBarrier>();
            var position = 0;

            for (var i = 0; i < BARRIERS_COUNT; i++)
            {
                _barriers.Add(new Barrier(new ConsolePosition(START_X + (position * POSITION_OFFSET), Y_POS), renderer));
                position++;
            }
        }

        public void Render()
        {
            _barriers.ForEach(b => b.Render());
        }

        public void Unrender()
        {
            _barriers.ForEach(b => b.Unrender());
        }

        public bool IsBarrierPartDestroyed(IPosition beamPosition)
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
