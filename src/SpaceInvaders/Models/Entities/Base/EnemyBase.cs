using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;
using System;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Base
{
    public class EnemyBase : GameObject, IEnemyMovable, IDestroyable
    {
        private const int ANIMATIONS_INIT_SPEED = 12;
        private const int SPRITE_HEIGHT = 3;
        private const int SPRITE_WIDTH = 7;

        protected string[] _image;
        protected string[] _secondImage;
        protected IRenderer<string> _renderer;
        protected ConsoleColor _color;
        private Timer _animationTimer;
        private bool _switchImage = false;

        public bool IsDestroyed { get; private set; }

        public EnemyBase(int x, int y, ConsoleColor color, IRenderer<string> renderer, bool isDestoryed = false)
            :base(new ConsolePosition(x,y))
        {
            _renderer = renderer;
            _color = color;
            _animationTimer = new Timer(ANIMATIONS_INIT_SPEED);
            IsDestroyed = isDestoryed;
        }

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

        public override void Render()
        {
            if (!_animationTimer.IsCounting())
                _switchImage = !_switchImage;

            _renderer.SetColor(_color);
            var image = _switchImage ? _image : _secondImage;

            for (var row = 0; row < SPRITE_HEIGHT; row++)
                for (var col = 0; col < SPRITE_WIDTH; col++)
                    _renderer.DrawAtPosition(Position.X + col, Position.Y + row, image[row][col].ToString());
        }

        public override void Unrender()
        {
            for (var row = 0; row < SPRITE_HEIGHT; row++)
                _renderer.DrawAtPosition(Position.X, Position.Y + row, "       ");
        }

        public void IncreaseY()
        {
            Position.Move(MoveType.Down);
        }

        public void DecreaseU()
        {
            Position.Move(MoveType.Up);
        }
    }
}
