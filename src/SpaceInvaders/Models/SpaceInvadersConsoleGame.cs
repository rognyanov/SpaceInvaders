using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Entities.Visual;
using SpaceInvaders.Models.Grid;
using System;
using System.Collections.Generic;
using System.Threading;
using SpaceInvaders.Models.Entities.Features;
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
        private EnemyBeams _enemyBeams;
        private GameHeader _gameHeader;
        private ExtraLife _extraLife;
        private Ufo _ufo;

        public SpaceInvadersConsoleGame(IRenderer<string> renderer)
        {
            _renderer = renderer;
            _isGameOver = false;
            _score = 0;
            _lifes = INIT_LIFES;
            _level = 1;
            _ship = new Ship(_renderer);
            _enemies = new Enemies(_renderer);
            _enemyBeams = new EnemyBeams(_renderer);
            _gameHeader = new GameHeader(_renderer);
            _extraLife = new ExtraLife(_renderer, _gameHeader);
            _ufo = new Ufo(_renderer);
            PrepareConsole();
            _gameHeader.Render();
        }

        public void Play()
        {
            while (!_isGameOver)
            {
                //Plot ship, enemies, beams
                RenderEntities();

                // We summon UFO
                _ufo.Invoke();
                // UFO destroyed check
                CheckIfUfoIsDestroyed();

                //Is level completed
                if (IsLevelCompleted())
                {
                    LevelCompleted();

                    //Init next level
                    InitNextLevel();
                    _gameHeader.RenderStats(_score, _level, _lifes);
                }

                // Invoke enemy beams
                _enemyBeams.Invoke(_enemies.GetPositions());

                //Check if ship destroyed enemy and increase score
                CheckIfShipDestroyedEnemy();

                //Check if ship is destroyed
                if (CheckIfEnemeyDestroyedShip())
                {
                    ShipDestroyed();
                    _isGameOver = _lifes == 0;
                }

                //Extra life
                ExtraLifeCheck();

                //Update score
                _gameHeader.RenderScore(_score);

                //Short delay
                Delay();

                //Read player input
                _ship.ReadInput();

                //Move entities
                Move();
            }

            GameOver();
        }

        private void ExtraLifeCheck()
        {
            if (_extraLife.Invoke(_score, _lifes))
            {
                _lifes++;
                _gameHeader.RenderLifes(_lifes);
            }
        }

        private void CheckIfUfoIsDestroyed()
        {
            if (_ufo.IsNotFlying())
                return;

            var beams = _ship.GetBeams();
            var beamsToDelete = new List<BeamBase>();

            foreach (var beam in beams)
            {
                if (_ufo.IsDestroyed(beam.Position))
                {
                    beamsToDelete.Add(beam);
                    _ufo.Unrender();
                    _ufo = new Ufo(_renderer);
                    _score += 1000;
                }
            }

            _ship.DeleteBeams(beamsToDelete);
        }

        private void LevelCompleted()
        {
            var counter = new Timer(201);
            var scoreCounter = new Timer(8);
            var levelBlinker = new TextBlinker(new ConsolePosition(37, 22), _renderer, "L E V E L   C O M P L E T E D!");
            var bonusBlinker = new TextBlinker(new ConsolePosition(39, 24), _renderer, "Level bonus - 1000 points.");
            var displayScore = false;

            while (counter.IsCounting())
            {
                levelBlinker.Invoke();
                bonusBlinker.Invoke();
                _ship.RenderBeams();

                if (!scoreCounter.IsCounting())
                {
                    displayScore = !displayScore;

                    if (displayScore)
                        _gameHeader.RenderLevel(_level);
                    else
                        _gameHeader.UnrenderLifes();
                }

                _score += 5;
                _gameHeader.RenderScore(_score);
                Delay();
                _ship.MoveBeams();
            }

            _level++;
            _gameHeader.RenderLevel(_level);
            _ship.Unrender();
            Delay(800);

            _renderer.DrawAtPosition(37, 22, "                              ");
            _renderer.DrawAtPosition(39, 24, "                          ");
        }

        private void InitNextLevel()
        {
            _ship = new Ship(_renderer);
            _enemies = new Enemies(_renderer);
        }

        private void GameOver()
        {
            _renderer.DrawAtPosition(42, 15, "G A M E   O V E R!");
        }

        private void ShipDestroyed()
        {
            _lifes--;

            _enemies.Render();
            _ship.Unrender();

            LifeLost();

            _gameHeader.RenderStats(_score, _level, _lifes);
        }

        private void LifeLost()
        {
            var time = (_lifes == 0) ? 170 : 100;
            var counter = new Timer(time);
            var blinkCounter = new Timer(10);

            var displayLifes = true;

            _ship.ReInitialize();
            _enemies.ResetMoveUp();

            while (counter.IsCounting())
            {
                _enemies.Render();
                _enemyBeams.Render();
                _ship.RenderBeams();
                Delay();

                if (_lifes > 0)
                    _enemies.MoveUp();

                _ship.MoveBeams();
                _enemyBeams.Move();

                if (!blinkCounter.IsCounting())
                {
                    if (displayLifes)
                    {
                        displayLifes = false;
                        _gameHeader.RenderLifes(_lifes + 1);
                    }
                    else
                    {
                        displayLifes = true;
                        _gameHeader.UnrenderLifes();
                    }
                }
            }

            _gameHeader.RenderLifes(_lifes);
        }

        private void RenderEntities()
        {
            _ship.Render();
            _ship.RenderBeams();
            _enemies.Render();
            _enemyBeams.Render();
            _ufo.Render();
        }

        private void Delay(int delay = DELAY)
        {
            Thread.Sleep(delay);
        }

        private void Move()
        {
            _ship.Move();
            _ship.MoveBeams();
            _enemies.Move();
            _enemyBeams.Move();
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

            var result = (_enemies.HasDestroyedShip(position) || _enemyBeams.HasDestroyedShip(position));

            return result;
        }

        private bool IsLevelCompleted()
        {
            return _enemies.IsEmpty();
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
