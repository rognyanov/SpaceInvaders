using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Grid;
using System;
using System.Threading;

namespace SpaceInvaders.Models
{
    public class SpaceInvadersConsoleGame
    {
        private const int DELAY = 20;
        private const int INIT_LIFES = 3;

        private bool _isGameOver;
        private int _score;
        private int _lifes;
        private int _level;
        private Ship _ship;
        private Enemies _enemies;

        public SpaceInvadersConsoleGame()
        {
            _isGameOver = false;
            _score = 0;
            _lifes = INIT_LIFES;
            _level = 1;
            _ship = new Ship(new ConsoleRenderer());
            _enemies = new Enemies(new ConsoleRenderer());
        }

        public void Play()
        {
            while (!_isGameOver)
            {
                //Plot ship, enemies, beams
                RenderEntities();
                //Short delay
                Delay();
                //Read player input
                _ship.ReadInput();
                //Move entities
                Move();
            }
        }

        private void RenderEntities()
        {
            _ship.Render();
            _ship.RenderBeams();
            _enemies.Render();
        }

        private void Delay()
        {
            Thread.Sleep(DELAY);
        }

        private void Move()
        {
            _ship.Move();
            _ship.MoveBeams();
            _enemies.Move();
        }

        internal void PrepareConsole()
        {
            Console.SetWindowSize(100, 60);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
        }
    }
}
