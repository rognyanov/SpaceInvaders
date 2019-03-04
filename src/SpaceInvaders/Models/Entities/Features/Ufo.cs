using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Features;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;
using System;

namespace SpaceInvaders.Models.Entities.Features
{
    public sealed class Ufo : IUfo
    {
        private enum State
        {
            Waiting,
            Flying
        }

        public IPosition Position { get; private set; }
        private readonly IRenderer<string> _renderer;
        private const int INIT_X = 0;
        private const int INIT_Y = 7;
        private const int UFO_HEIGHT = 2;
        private const int UFO_WIDTH = 8;
        private const int ANIMATION_SPEED = 8;
        private const int WAITING_TIME = 1000; //Bigger value => more waiting for ufo to appear
        private const int MOVE_SPEED = 2;
        private const int RIGHT_BOUNDARY = 91;
        private readonly string[] _image;
        private readonly string[] _secondImage;
        private int _animationIndex;
        private readonly Timer _timer;
        private readonly Timer _waitTimer;
        private readonly Timer _moveTimer;
        private State _state;

        public Ufo(IRenderer<string> renderer)
        {
            _renderer = renderer;
            Position = new ConsolePosition(INIT_X, INIT_Y);
            _animationIndex = 0;
            _timer = new Timer(ANIMATION_SPEED);
            _waitTimer = new Timer(WAITING_TIME);
            _moveTimer = new Timer(MOVE_SPEED);
            _state = State.Waiting;

            _image = new string[2]
            {
                " ______ ",
                " |____| "
            };

            _secondImage = new string[3]
            {
                " X  X X ",
                "X  X   X",
                " X   X  "
            };
        }

        /// <inheritdoc />
        public void Invoke()
        {
            if (_state == State.Waiting)
                Waiting();
            else
                Flying();
        }

        /// <inheritdoc />
        public bool IsNotFlying()
        {
            return _state != State.Flying;
        }

        /// <inheritdoc />
        public bool IsDestroyed(IPosition beamPosition)
        {
            if (beamPosition.X > Position.X
                && beamPosition.X < Position.X +8
                && beamPosition.Y == Position.Y+1)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public void Render()
        {
            if (_state == State.Waiting)
                return;

            if (!_timer.IsCounting())
            {
                _animationIndex++;
                if (_animationIndex == 3)
                    _animationIndex = 0;
            }

            _renderer.SetColor(ConsoleColor.DarkMagenta);

            for (int i = 0; i < UFO_HEIGHT; i++)
            {
                for (int j = 0; j < UFO_WIDTH; j++)
                {
                    if (i == 1)
                    {
                        if (_secondImage[_animationIndex][j] == 'X')
                            _renderer.SetColor(ConsoleColor.Magenta);
                        else
                            _renderer.SetColor(ConsoleColor.DarkMagenta);
                    }
                    _renderer.DrawAtPosition(Position.X + j, Position.Y + i, _image[i][j].ToString());
                }
            }
        }

        /// <inheritdoc />
        public void Unrender()
        {
            _renderer.DrawAtPosition(Position.X, Position.Y, "        ");
            _renderer.DrawAtPosition(Position.X, Position.Y + 1, "        ");
        }

        private void Waiting()
        {
            if (_waitTimer.IsCounting())
                return;

            _state = State.Flying;
        }

        private void Flying()
        {
            if (_moveTimer.IsCounting())
                return;

            Position.Move(MoveType.Right);

            if (Position.X >= RIGHT_BOUNDARY)
            {
                Unrender();
                while (Position.X > 0)
                {
                    Position.Move(MoveType.Left);
                }

                _state = State.Waiting;
            }
        }
    }
}
