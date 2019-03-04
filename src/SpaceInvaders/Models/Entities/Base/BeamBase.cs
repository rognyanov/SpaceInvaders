using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Enums;
using System;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class BeamBase : IGameObject, IMovable, IRenderable
    {
        public IPosition Position { get; }

        protected int _increment;
        protected char _beam;
        protected IRenderer<string> _renderer;

        protected BeamBase(IPosition position, IRenderer<string> renderer)
        {
            Position = position;
            _renderer = renderer;
        }

        public void Move()
        {
            Unrender();
            Position.Move(_increment == 1 ? MoveType.Down : MoveType.Up);
        }

        public void Render()
        {
            _renderer.SetColor(ConsoleColor.Yellow);
            _renderer.DrawAtPosition(Position.X, Position.Y, _beam.ToString());
        }

        public void Unrender()
        {
            _renderer.DrawAtPosition(Position.X, Position.Y, " ");
        }
    }
}