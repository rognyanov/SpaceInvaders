using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;
using System;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class BeamBase : GameObject, IMovable
    {
        protected int _increment;
        protected char _beam;
        protected IRenderer<string> _renderer;

        protected BeamBase(IPosition position, IRenderer<string> renderer)
            : base(position)
        {
            _renderer = renderer;
        }

        public void Move()
        {
            Unrender();
            Position.Move(_increment == 1 ? MoveType.Down : MoveType.Up);
        }

        public override void Render()
        {
            _renderer.SetColor(ConsoleColor.Yellow);
            _renderer.DrawAtPosition(Position.X, Position.Y, _beam.ToString());
        }

        public override void Unrender()
        {
            _renderer.DrawAtPosition(Position.X, Position.Y, " ");
        }
    }
}