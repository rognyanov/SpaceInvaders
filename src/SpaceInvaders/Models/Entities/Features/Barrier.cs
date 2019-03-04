using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Features;
using SpaceInvaders.Contracts.Visual;
using System;

namespace SpaceInvaders.Models.Entities.Features
{
    public sealed class Barrier : IBarrier
    {
        public IPosition Position { get; }

        private const int BARRIER_HEIGHT = 4;
        private const int BARRIER_WIDTH = 8;
        private readonly string[] _image;
        private readonly int[][] _barrierGrid;
        private readonly IRenderer<string> _renderer;

        public Barrier(IPosition position, IRenderer<string> renderer)
        {
            Position = position;
            _renderer = renderer;

            _image = new string[]
            {
                @" ______ ",
                @"/||||||\",
                @"||||||||",
                @"^^^^^^^^"
            };

            _barrierGrid = new int[BARRIER_WIDTH][];

            for (var row = 0; row < BARRIER_HEIGHT; row++)
            {
                _barrierGrid[row] = new int[BARRIER_WIDTH];

                for (var col = 0; col < BARRIER_WIDTH; col++)
                {
                    _barrierGrid[row][col] = 1;
                }
            }

            _barrierGrid[0][0] = 0;
            _barrierGrid[0][7] = 0;
        }

        public void Render()
        {
            _renderer.SetColor(ConsoleColor.Red);

            for (var i = 0; i < BARRIER_HEIGHT; i++)
            {
                _renderer.DrawAtPosition(Position.X, Position.Y + i - 1, _image[i]);
            }
        }

        public void Unrender()
        {
            for (var i = 0; i < BARRIER_HEIGHT; i++)
            {
                _renderer.DrawAtPosition(Position.X, Position.Y + i - 1, " ");
            }
        }

        public bool IsSet(IPosition beamPosition)
        {
            var result = false;

            if (beamPosition.X >= Position.X
                && beamPosition.X < Position.X + BARRIER_WIDTH)
            {
                if (_barrierGrid[beamPosition.Y - Position.Y + 1][beamPosition.X - Position.X] == 1)
                {
                    result = true;
                }
            }

            return result;
        }

        public void SetTaken(IPosition beamPosition)
        {
            _barrierGrid[beamPosition.Y - Position.Y + 1][beamPosition.X - Position.X] = 0;
        }
    }
}
