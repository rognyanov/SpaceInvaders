using System;
using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Features
{
    public class Ufo
    {
        enum State
        {
            Waiting,
            Flying
        }

        private readonly IRenderer<string> _renderer;
        private const int INIT_X = 0;
        private const int INIT_Y = 7;
        private const int UFO_HEIGHT = 2;
        private const int UFO_WIDTH = 8;
        private const int ANIMATION_SPEED = 8;
        private const int WAITING_TIME = 300;
        private const int MOVE_SPEED = 2;
        private const int RIGHT_BOUNDARY = 91;
        private IPosition _position;
        private string[] _image;
        private string[] _secondImage;
        private int _animationIndex;
        private Timer _timer;
        private Timer _waitTimer;
        private Timer _moveTimer;
        private State _state;

        public Ufo(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _position = new ConsolePosition(INIT_X, INIT_Y);
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

        public void Invoke()
        {
            if (_state == State.Waiting)
                Waiting();
            else
                Flying();
        }

        public bool IsNotFlying()
        {
            return _state != State.Flying;
        }

        public bool IsDestroyed(IPosition beamPosition)
        {
            if (beamPosition.X > _position.X
                && beamPosition.X < _position.X +8
                && beamPosition.Y == _position.Y+1)
            {
                return true;
            }

            return false;
        }

        public IPosition GetPosition()
        {
            return _position;
        }

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
                    _renderer.DrawAtPosition(_position.X + j, _position.Y + i, _image[i][j].ToString());
                }
            }
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

            _position.Move(MoveType.Right);

            if (_position.X >= RIGHT_BOUNDARY)
            {
                Unrender();
                while (_position.X > 0)
                {
                    _position.Move(MoveType.Left);
                }

                _state = State.Waiting;
            }
        }

        public void Unrender()
        {
            _renderer.DrawAtPosition(_position.X, _position.Y, "        ");
            _renderer.DrawAtPosition(_position.X, _position.Y + 1, "        ");
        }
    }
}
