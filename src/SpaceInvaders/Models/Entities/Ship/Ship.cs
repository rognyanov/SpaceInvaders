using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Player;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Grid;
using SpaceInvaders.Models.Helpers;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Models.Entities.Ship
{
    public sealed class Ship : IShip
    {
        public IPosition Position { get; private set; }

        private const int INIT_X = 45;
        private const int INIT_Y = 57;
        private const int IMAGE_HEIGHT = 3;
        private const int IMAGE_WIDTH = 7;
        private const int INIT_MOVE_SPEED = 1;
        private const int LEFT_BOUNDARY = 0;
        private const int RIGHT_BOUNDARY = 92;

        private readonly string[] _image;
        private MoveType _currentMove;
        private IRenderer<string> _renderer;
        private ShipBeams _beams;
        private Timer _moveTimer;

        public Ship(IPosition position, IRenderer<string> renderer)
        {
            Position = position;

            _image = new string[3]
            {
                @"   |   ",
                @"  _|_  ",
                @" /|-|\ ﻿"
            };

            _renderer = renderer;
            _currentMove = MoveType.None;
            _beams = new ShipBeams(renderer);
            _moveTimer = new Timer(INIT_MOVE_SPEED);
        }

        public void ReInitialize()
        {
            Position = new ConsolePosition(INIT_X, INIT_Y);
            _currentMove = MoveType.None;
        }

        public void Move()
        {
            if (_moveTimer.IsCounting())
                return;

            switch (_currentMove)
            {
                case MoveType.Left when Position.X > LEFT_BOUNDARY:
                    Position.Move(MoveType.Left);
                    break;
                case MoveType.Right when Position.X < RIGHT_BOUNDARY:
                    Position.Move(MoveType.Right);
                    break;
            }
        }

        public void ReadInput()
        {
            var info = new ConsoleKeyInfo();

            while (Console.KeyAvailable)
            {
                info = Console.ReadKey(true);
            }

            switch (info.Key)
            {
                case ConsoleKey.LeftArrow:
                    _currentMove = MoveType.Left;
                    break;
                case ConsoleKey.RightArrow:
                    _currentMove = MoveType.Right;
                    break;
                case ConsoleKey.X:
                    _currentMove = MoveType.None;
                    break;
                case ConsoleKey.Spacebar:
                    _beams.Add(Position.X);
                    break;
                default:
                    break;
            }
        }

        public void Render()
        {
            _renderer.SetColor(ConsoleColor.White);

            for (var row = 0; row < IMAGE_HEIGHT; row++)
            {
                for (var col = 0; col < IMAGE_WIDTH; col++)
                {
                    var part = _image[row][col].ToString();
                    _renderer.DrawAtPosition(Position.X + col, Position.Y + row, part);
                }
            }
        }

        public void Unrender()
        {
            for (var i = 0; i < IMAGE_HEIGHT; i++)
            {
                for (var j = 0; j < IMAGE_WIDTH; j++)
                {
                    _renderer.DrawAtPosition(Position.X, Position.Y + i, "       ");
                }
            }
        }

        public void RenderBeams()
        {
            _beams.Render();
        }

        public void MoveBeams()
        {
            _beams.Move();
        }

        public List<BeamBase> GetBeams()
        {
            return _beams.GetBeams();
        }

        public void DeleteBeams(List<BeamBase> beams)
        {
            _beams.DeleteBeams(beams);
        }
    }
}
