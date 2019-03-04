using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Enemies;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Helpers;
using System;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class EnemyBase : IGameObject, IEnemyMovable, IDestroyable, IRenderable
    {
        public IPosition Position { get; }
        public bool IsDestroyed { get; }

        private const int ANIMATIONS_INIT_SPEED = 12;
        private const int SPRITE_HEIGHT = 3;
        private const int SPRITE_WIDTH = 7;

        protected string[] _image;
        protected string[] _secondImage;
        protected IRenderer<string> _renderer;
        protected ConsoleColor _color;
        private readonly Timer _animationTimer;
        private bool _switchImage;

        protected EnemyBase(IPosition position, ConsoleColor color, IRenderer<string> renderer, bool isDestoryed = false)
            :base()
        {
            Position = position;
            _renderer = renderer;
            _color = color;
            _animationTimer = new Timer(ANIMATIONS_INIT_SPEED);
            IsDestroyed = isDestoryed;
        }

        /// <inheritdoc />
        public void Move(MoveType move, bool increaseY)
        {
            if (increaseY)
            {
                _renderer.DrawAtPosition(Position.X, Position.Y, "       ");
                Position.Move(MoveType.Down);
                return;
            }

            Position.Move(move);
        }

        /// <inheritdoc />
        public void Render()
        {
            if (!_animationTimer.IsCounting())
                _switchImage = !_switchImage;

            _renderer.SetColor(_color);
            var image = _switchImage ? _image : _secondImage;

            for (var row = 0; row < SPRITE_HEIGHT; row++)
                for (var col = 0; col < SPRITE_WIDTH; col++)
                    _renderer.DrawAtPosition(Position.X + col, Position.Y + row, image[row][col].ToString());
        }

        /// <inheritdoc />
        public void Unrender()
        {
            for (var row = 0; row < SPRITE_HEIGHT; row++)
                _renderer.DrawAtPosition(Position.X, Position.Y + row, "       ");
        }

        /// <inheritdoc />
        public void DecreaseY()
        {
            Position.Move(MoveType.Up);
        }
    }
}