using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Helpers;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class EnemyBeams : BeamsBase
    {
        private const int MOVE_SPEED = 2;
        private const int LOWER_BOUNDARY = 63;

        public EnemyBeams(IRenderer<string> renderer)
            :base(renderer)
        {
            _moveTimer = new Timer(MOVE_SPEED);
            _maxBeamsCount = 2;
            _endRow = LOWER_BOUNDARY;
        }

        public void Invoke(List<IPosition> positions)
        {
            if (_beams.Count >= _maxBeamsCount)
                return;

            var rnd = new Random();
            int index = rnd.Next(0, positions.Count);
            int x = positions[index].X + 3;
            int y = positions[index].Y + 3;

            _beams.Add(new EnemyBeam(x, y));
        }

        public bool HasDestroyedShip(IPosition shipPosition)
        {
            bool result = false;

            var beamsToDelete = new List<BeamBase>();
            foreach (var beam in _beams)
            {
                var pos = beam.Position;

                if ((pos.X > shipPosition.X + 2
                    && pos.X < shipPosition.X + 4
                    && pos.Y == shipPosition.Y)
                    ||
                    (pos.X > shipPosition.X + 1
                     && pos.X < shipPosition.X + 5
                     && pos.Y == shipPosition.Y + 1)
                    ||
                    (pos.X > shipPosition.X
                     && pos.X < shipPosition.X + 6
                     && pos.Y == shipPosition.Y + 2))
                {
                    beamsToDelete.Add(beam);
                    beam.Unrender();
                    result = true;
                }
            }

            beamsToDelete.ForEach(b => _beams.Remove(b));

            return result;
        }
    }
}