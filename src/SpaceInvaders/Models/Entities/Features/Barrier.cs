using System;
using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders.Models.Entities.Features
{
    public class Barrier : GameObject
    {
        private const int BARRIER_HEIGHT = 4;
        private const int BARRIER_WIDTH = 8;
        private readonly IRenderer<string> _renderer;
        private string[] _image;
        private int[][] _barrierGrid;
        

        public Barrier(int x, int y, IRenderer<string> renderer)
        : base(new ConsolePosition(x, y))
        {
            _renderer = renderer;

            _image = new string[]
            {
                @" ______ ",
                @"/||||||\",
                @"||||||||",
                @"^^^^^^^^"
            };

            _barrierGrid = new int[BARRIER_WIDTH][];

            for (int i = 0; i < BARRIER_HEIGHT; i++)
            {
                _barrierGrid[i] = new int[BARRIER_WIDTH];

                for (int j = 0; j < BARRIER_WIDTH; j++)
                {
                    _barrierGrid[i][j] = 1;
                }
            }

            _barrierGrid[0][0] = 0;
            _barrierGrid[0][7] = 0;
        }

        public override void Render()
        {
            _renderer.SetColor(ConsoleColor.Red);

            for (int i = 0; i < BARRIER_HEIGHT; i++)
            {
                _renderer.DrawAtPosition(Position.X,Position.Y+i-1, _image[i]);
            }
        }

        public override void Unrender()
        {
            throw new NotImplementedException();
        }

        public bool IsSet(IPosition beamPosition)
        {
            var result = false;

            if (beamPosition.X >= Position.X
                && beamPosition.X < Position.X + BARRIER_WIDTH)
            {
                if (_barrierGrid[beamPosition.Y - Position.Y+1][beamPosition.X - Position.X] == 1)
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
