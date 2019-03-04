using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Helpers;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class BeamsBase : IRenderable, IMovable
    {
        protected IRenderer<string> _renderer;
        protected List<BeamBase> _beams;
        protected int _beamsCount;
        protected int _maxBeamsCount;
        protected Timer _moveTimer;
        protected int _endRow;

        protected BeamsBase(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _beams = new List<BeamBase>();
            _beamsCount = 0;
        }

        /// <inheritdoc />
        public void Move()
        {
            if (_moveTimer.IsCounting())
                return;

            var beamsToDelete = new List<BeamBase>();

            foreach (var beam in _beams)
            {
                beam.Move();
                if (beam.Position.Y == _endRow)
                {
                    beamsToDelete.Add(beam);
                }
            }

            foreach (var beam in beamsToDelete)
            {
                _beamsCount--;
                _beams.Remove(beam);
            }
        }

        /// <inheritdoc />
        public void Render()
        {
            _renderer.SetColor(ConsoleColor.Yellow);

            _beams.ForEach(b=>b.Render());
        }

        /// <inheritdoc />
        public void Unrender()
        {
            _beams.ForEach(b=>b.Unrender());
        }

        /// <inheritdoc />
        public List<BeamBase> GetBeams()
        {
            return _beams;
        }
    }
}