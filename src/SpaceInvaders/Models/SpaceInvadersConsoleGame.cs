using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Entities.Visual;
using System;
using System.Collections.Generic;
using System.Threading;
using Timer = SpaceInvaders.Models.Helpers.Timer;

namespace SpaceInvaders.Models
{
    public class SpaceInvadersConsoleGame
    {
        private const int DELAY = 20;
        private const int INIT_LIFES = 3;
        private readonly IRenderer<string> _renderer;
        private bool _isGameOver;
        private int _score;
        private int _lifes;
        private int _level;
        private Ship _ship;
        private Enemies _enemies;
        private GameHeader _gameHeader;

        public SpaceInvadersConsoleGame(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _isGameOver = false;
            _score = 0;
            _lifes = INIT_LIFES;
            _level = 1;
            _ship = new Ship(_renderer);
            _enemies = new Enemies(_renderer);
            _gameHeader = new GameHeader(_renderer);
            PrepareConsole();
            _gameHeader.Render();
        }

        public void Play()
        {
            while (!_isGameOver)
            {
                //Plot ship, enemies, beams
                RenderEntities();

                //Check if ship is destroyed
                if (CheckIfEnemeyDestroyedShip())
                {
                    ShipDestroyed();
                    _isGameOver = _lifes == 0;
                }

                //Check if ship destroyed enemy and increase score
                CheckIfShipDestroyedEnemy();

                //Short delay
                Delay();

                //Read player input
                _ship.ReadInput();

                //Move entities
                Move();
            }
        }

        private void ShipDestroyed()
        {
            _lifes--;

            _enemies.Render();
            _ship.Unrender();

            if (_lifes > 0)
            {
                LifeLost();
            }
            else
            {
                //GameOver();
            }

            //Render score
        }

        private void LifeLost()
        {
            var counter = new Timer(90);
            var blinkCounter = new Timer(10);

            bool displayLifes = true;

            _ship.ReInitialize();
            //_enemies.ResetMoveUp();

            while (counter.IsCounting())
            {
                _enemies.Render();
                _ship.RenderBeams();
                Delay();
                //_enemies.MoveUp();
                _ship.MoveBeams();

                if (!blinkCounter.IsCounting())
                {
                    if (displayLifes)
                    {
                        displayLifes = false;
                        //Score.PlotLifes(_lifes + 1);
                    }
                    else
                    {
                        displayLifes = true;
                        //Score.UnplotLifes();
                    }
                }
            }

            //Score.PlotLifes(_lifes);
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

        private void CheckIfShipDestroyedEnemy()
        {
            EnemyBase enemyEntitiy;
            List<BeamBase> beams;
            var beamsToDelete = new List<BeamBase>();

            beams = _ship.GetBeams();
            if (beams.Count > 0)
            {
                foreach (var beam in beams)
                {
                    enemyEntitiy = _enemies.IsDestroyed(beam.Position);
                    if (enemyEntitiy.IsDestroyed)
                    {
                        beamsToDelete.Add(beam);
                        _score += 100;
                        _gameHeader.RenderScore(_score);
                    }
                }

                _ship.DeleteBeams(beamsToDelete);
            }
        }

        private bool CheckIfEnemeyDestroyedShip()
        {
            var position = _ship.Position;
            bool result = false;

            if (_enemies.HasDestroyedShip(position))
            {
                result = true;
            }

            return result;
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
