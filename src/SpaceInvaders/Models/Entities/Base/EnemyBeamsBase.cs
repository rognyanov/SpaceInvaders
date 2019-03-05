using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Enemies;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class EnemyBeamsBase : BeamsBase, IEnemyBeams
    {
        protected const int MOVE_SPEED = 2;
        protected const int LOWER_BOUNDARY = 59;
        protected const int INIT_NUM_OF_BEAMS = 2;

        protected EnemyBeamsBase(IRenderer<string> renderer) 
            : base(renderer)
        {
        }

        /// <inheritdoc />
        public void Invoke(List<IPosition> positions)
        {
            if (_beams.Count >= _maxBeamsCount)
                return;

            var rnd = new Random();
            var index = rnd.Next(0, positions.Count);
            var x = positions[index].X + 3;
            var y = positions[index].Y + 3;

            _beams.Add(new EnemyBeam(new ConsolePosition(x, y)));
        }

        /// <inheritdoc />
        public bool HasDestroyedShip(IPosition shipPosition)
        {
            var result = false;

            var beamsToDelete = new List<BeamBase>();
            foreach (var beam in _beams)
            {
                var pos = beam.Position;

                if ((pos.X <= shipPosition.X + 2 || pos.X >= shipPosition.X + 4 || pos.Y != shipPosition.Y) &&
                    (pos.X <= shipPosition.X + 1 || pos.X >= shipPosition.X + 5 || pos.Y != shipPosition.Y + 1) &&
                    (pos.X <= shipPosition.X || pos.X >= shipPosition.X + 6 || pos.Y != shipPosition.Y + 2))
                    continue;

                beamsToDelete.Add(beam);
                beam.Unrender();
                result = true;
            }

            beamsToDelete.ForEach(b => _beams.Remove(b));

            return result;
        }

        /// <inheritdoc />
        public void DeleteBeams(List<BeamBase> beams)
        {
            beams.ForEach(b => _beams.Remove(b));
        }

        public void ReInitialize(int level)
        {
            _moveTimer = new Timer(MOVE_SPEED);
            _maxBeamsCount = INIT_NUM_OF_BEAMS + (level - 1);
            _endRow = LOWER_BOUNDARY;
        }
    }
}
