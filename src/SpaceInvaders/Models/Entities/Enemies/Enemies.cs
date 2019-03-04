using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class Enemies : IMovable
    {
        private const int Y_MIN = 32;
        private const int MOVE_UP_SPEED = 5;
        private const int MOVE_UP_STEPS = 15;
        private const int LEFT_BOUNDARY = 1;
        private const int RIGHT_BOUNDARY = 93;
        private const int BOTTOM_BOUNDARY = 55;
        private const int MOVE_INIT_SPEED = 6;
        private const int MOVE_SPEED_INCREASE = 120;

        private List<EnemyBase> _enemies;
        private int _moveSpeed;
        private Timer _moveSpeedIncrease;
        private Timer _moveTimer;
        private Timer _moveUpSteps;
        private Timer _moveUpSpeed;
        private MoveType _currentMove;
        private IRenderer<string> _renderer;

        public Enemies(int level, IRenderer<string> renderer)
        {
            _enemies = new List<EnemyBase>();
            _moveTimer = new Timer(MOVE_INIT_SPEED);
            _moveUpSteps = new Timer(MOVE_UP_STEPS);
            _moveUpSpeed = new Timer(MOVE_UP_SPEED);
            _moveSpeed = MOVE_INIT_SPEED;
            _moveSpeedIncrease = new Timer(MOVE_SPEED_INCREASE);
            _renderer = renderer;
            _currentMove = MoveType.Right;

            InitEnemies(level);
        }

        public void Move()
        {
            var minX = 100;
            var maxX = 0;
            var increaseY = false;

            if (_moveTimer.IsCounting())
                return;

            if (!_moveSpeedIncrease.IsCounting())
            {
                if (_moveSpeed > 0)
                {
                    _moveSpeed--;
                    _moveTimer = new Timer(_moveSpeed);
                }
            }

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

            _enemies.ForEach(e => e.Move(_currentMove, increaseY));

            HasReachedBottom();
        }

        public void MoveUp()
        {
            if (_moveUpSpeed.IsCounting())
                return;

            if (_moveUpSteps.IsCounting())
                return;

            var min = 60;

            foreach (var enemy in _enemies)
            {
                var position = enemy.Position;
                if (min > position.Y)
                    min = position.Y;
            }

            if (min <= Y_MIN)
                return;

            foreach (var enemy in _enemies)
            {
                var position = enemy.Position;
                _renderer.DrawAtPosition(position.X, position.Y + 2, "       ");
                enemy.DecreaseY();
            }
        }

        public void Render()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Position.X > 0)
                {
                    enemy.Render();
                }
            }
        }

        public EnemyBase IsDestroyed(IPosition beamPosition)
        {
            IPosition position = null;
            EnemyBase temp = null;
            bool isDestroyed = false;

            foreach (var enemy in _enemies)
            {
                position = enemy.Position;

                if (beamPosition.X >= position.X + 1
                    && beamPosition.X < position.X + 6
                    && beamPosition.Y >= position.Y
                    && beamPosition.Y < position.Y + 2)
                {
                    enemy.Unrender();
                    temp = enemy;
                    isDestroyed = true;
                    break;
                }
            }

            if (temp != null)
            {
                _enemies.Remove(temp);
            }

            return new EnemyBase(0, 0, ConsoleColor.White, _renderer, isDestroyed);
        }

        public bool HasDestroyedShip(IPosition shipPosition)
        {
            foreach (var enemy in _enemies)
            {
                var position = enemy.Position;

                if ((shipPosition.X > position.X - 3
                    && shipPosition.X < position.X + 3
                    && shipPosition.Y == position.Y + 2)
                    ||
                    (shipPosition.X > position.X - 4
                     && shipPosition.X < position.X + 4
                     && shipPosition.Y == position.Y + 1))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsEmpty()
        {
            return _enemies.Count == 0;
        }

        private void InitEnemies(int level)
        {
            var counter = 0;
            var moreEnemies = 0;

            for (int i = 0; i < level; i++)
            {
                counter++;
                if (counter == 4)
                {
                    counter = 0;

                    if (moreEnemies < 5)
                        moreEnemies++;
                }
            }
            var rowsOfEnemeies = 6 + moreEnemies;

            for (var i = 0; i < rowsOfEnemeies; i++)
            {
                var x = i * 8 + 1;

                _enemies.Add(new StandartEnemy(x, 10, ConsoleColor.Green, _renderer));
                _enemies.Add(new AdvancedEnemy(x, 14, ConsoleColor.Yellow, _renderer));
                _enemies.Add(new HardEnemy(x, 18, ConsoleColor.Red, _renderer));
                _enemies.Add(new StandartEnemy(x, 22, ConsoleColor.Blue, _renderer));
                _enemies.Add(new AdvancedEnemy(x, 26, ConsoleColor.Cyan, _renderer));
                _enemies.Add(new HardEnemy(x, 30, ConsoleColor.Magenta, _renderer));
            }
        }

        private void HasReachedBottom()
        {
            var enemiesToDelete = new List<EnemyBase>();

            foreach (var enemy in _enemies)
            {
                var position = enemy.Position;
                if (position.Y <= BOTTOM_BOUNDARY) continue;

                enemy.Unrender();
                enemiesToDelete.Add(enemy);
            }

            enemiesToDelete.ForEach(e => _enemies.Remove(e));
        }

        public void ResetMoveUp()
        {
            _moveUpSteps = new Timer(MOVE_UP_STEPS);
            _moveUpSpeed = new Timer(MOVE_UP_SPEED);
        }

        public List<IPosition> GetPositions()
        {
            return _enemies.Select(e => e.Position).ToList();
        }
    }
}