using System;
using System.Collections.Generic;
using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Helpers;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class Enemies : IMovable
    {
        private const int LEFT_BOUNDARY = 7;
        private const int RIGHT_BOUNDARY = 90;
        private const int BOTTOM_BOUNDARY = 60;
        private const int MOVE_INIT_SPEED = 6;

        private bool _firstAnimation;
        private List<EnemyBase> _enemies;
        private Timer _moveTimer;
        private MoveType _currentMove;
        private IRenderer<string> _renderer;

        public Enemies(IRenderer<string> renderer)
        {
            _enemies = new List<EnemyBase>();
            _moveTimer = new Timer(MOVE_INIT_SPEED);
            _renderer = renderer;
            _firstAnimation = false;
            _currentMove = MoveType.Right;

            InitEnemies();
        }

        public void Move()
        {
            var minX = 100;
            var maxX = 0;
            var increaseY = false;

            if (_moveTimer.IsCounting())
                return;

            foreach (var enemy in _enemies)
            {
                var x = enemy.Position.X;

                switch (_currentMove)
                {
                    case MoveType.Left when x < minX:
                        minX = x;
                        break;
                    case MoveType.Right when x > maxX:
                        maxX = x;
                        break;
                    default:
                        break;
                }
            }

            if (_currentMove == MoveType.Left && minX == LEFT_BOUNDARY)
            {
                _currentMove = MoveType.Right;
                increaseY = true;
            }

            if (_currentMove == MoveType.Right && maxX == RIGHT_BOUNDARY)
            {
                _currentMove = MoveType.Left;
                increaseY = true;
            }

            foreach (var enemy in _enemies)
            {
                enemy.Move(_currentMove, increaseY);
            }

            //HasReachedBottom();
        }

        public void Render()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Position.X > 5)
                {
                    enemy.Render();
                }
            }
        }

        private void InitEnemies()
        {
            for (var i = 0; i < 6; i++)
            {
                var x = i * 8;

                _enemies.Add(new StandartEnemy(x, 10, ConsoleColor.Green, _renderer));
                _enemies.Add(new AdvancedEnemy(x, 14, ConsoleColor.Yellow, _renderer));
                _enemies.Add(new HardEnemy(x, 18, ConsoleColor.Red, _renderer));
                _enemies.Add(new StandartEnemy(x, 22, ConsoleColor.Blue, _renderer));
                _enemies.Add(new AdvancedEnemy(x, 26, ConsoleColor.Cyan, _renderer));
                _enemies.Add(new HardEnemy(x, 30, ConsoleColor.Magenta, _renderer));
            }
        }
    }
}
