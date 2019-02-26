using SpaceInvaders.Contracts;
using System;
using System.Collections.Generic;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Base
{
    public class BeamsBase : IRenderable, IMovable
    {
        protected IRenderer<string> _renderer;
        protected List<BeamBase> _beams;
        protected int _beamsCount;
        protected int _maxBeamsCount;
        protected Timer _moveTimer;
        protected int _endRow;

        public BeamsBase(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _beams = new List<BeamBase>();
            _beamsCount = 0;
        }

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

        public void Render()
        {
            _renderer.SetColor(ConsoleColor.Yellow);

            foreach (var beam in _beams)
                beam.Render();
        }

        public List<BeamBase> GetBeams()
        {
            return _beams;
        }

        public void Unrender()
        {
            throw new System.NotImplementedException();
        }
    }
}
