using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Enemies;
using SpaceInvaders.Contracts.Features;
using SpaceInvaders.Contracts.Player;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Features;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Entities.Visual;
using SpaceInvaders.Models.Grid;
using System;
using System.Collections.Generic;
using System.Threading;
using Timer = SpaceInvaders.Models.Helpers.Timer;

namespace SpaceInvaders.Models
{
    /// <summary>
    /// Space invaders console game ready to be played.
    /// </summary>
    public sealed class SpaceInvadersConsoleGame : IConsoleGame
    {
        private const int DELAY = 20;
        private const int INIT_LIFES = 3;
        private const int INIT_LEVEL = 1;
        private const int INIT_SCORE = 0;

        private readonly IRenderer<string> _renderer;
        private bool _isGameOver;
        private int _score;
        private int _lifes;
        private int _level;
        private IShip _ship;
        private IEnemies _enemies;
        private EnemyBeamsBase _enemyBeams;
        private readonly IGameHeader _gameHeader;
        private readonly IExtraLife _extraLife;
        private IUfo _ufo;
        private readonly IBarriers _barriers;

        /// <summary>
        /// Space invaders console game constructor.
        /// </summary>
        /// <param name="renderer">The class that can render stuff on the console.</param>
        public SpaceInvadersConsoleGame(IRenderer<string> renderer, 
            IShip ship, 
            IEnemies enemies,
            EnemyBeamsBase enemyBeams,
            IGameHeader gameHeader,
            IExtraLife extraLife,
            IUfo ufo,
            IBarriers barriers)
        {
            _isGameOver = false;
            _score = INIT_SCORE;
            _lifes = INIT_LIFES;
            _level = INIT_LEVEL;

            _renderer = renderer;
            _ship = ship;
            _enemies = enemies;
            _enemyBeams = enemyBeams;
            _gameHeader = gameHeader;
            _extraLife = extraLife;
            _ufo = ufo;
            _barriers = barriers;

            PrepareConsole();

            _gameHeader.RenderHeader();
            _gameHeader.RenderStats(_score, _level, _lifes);

            _barriers.Render();
        }

        /// <summary>
        /// This is the game cycle. Goes on until game over.
        /// </summary>
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

                //Delete barrier parts if hit by ship or enemy
                CheckIfBarrierPartIsDestroyed();

                //Update stats
                _gameHeader.RenderStats(_score, _level, _lifes);

                //Short delay
                Delay();

                //Read player input
                _ship.ReadInput();

                //Move entities
                Move();
            }

            GameOver();
        }

        private void RenderEntities()
        {
            _ship.Render();
            _ship.RenderBeams();
            _enemies.Render();
            _enemyBeams.Render();
            _ufo.Render();
        }

        private void Move()
        {
            _ship.Move();
            _ship.MoveBeams();
            _enemies.Move();
            _enemyBeams.Move();
        }

        private void InitNextLevel()
        {
            _ship.ReInitialize(new ConsolePosition(45, 57));
            _enemies.ReInitialize(_level);
            _enemyBeams.ReInitialize(_level);
        }

        private void ExtraLifeCheck()
        {
            if (!_extraLife.Invoke(_score, _lifes))
                return;

            _lifes++;
            _gameHeader.RenderLifes(_lifes);
        }

        private void CheckIfUfoIsDestroyed()
        {
            if (_ufo.IsNotFlying())
                return;

            var shipBeams = _ship.GetBeams();
            var beamsToDelete = new List<BeamBase>();

            foreach (var beam in shipBeams)
            {
                if (!_ufo.IsDestroyed(beam.Position))
                    continue;

                beamsToDelete.Add(beam);
                _ufo.Unrender();
                _ufo = new Ufo(_renderer);
                _score += 1000;
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
                _enemyBeams.Render();
                _ufo.Render();

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
                _enemyBeams.Move();
                _ufo.Invoke();
            }

            _level++;
            _gameHeader.RenderLevel(_level);
            _ship.Unrender();
            Delay(800);

            _renderer.DrawAtPosition(37, 22, "                              ");
            _renderer.DrawAtPosition(39, 24, "                          ");
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
        }

        private void CheckIfBarrierPartIsDestroyed()
        {
            var beamsToDelete = new List<BeamBase>();

            foreach (var beam in _ship.GetBeams())
            {
                var beamPos = beam.Position;

                if (beamPos.Y <= 49 || beamPos.Y >= 54)
                    continue;

                if (!_barriers.IsBarrierPartDestroyed(beam.Position))
                    continue;

                beamsToDelete.Add(beam);
                beam.Unrender();
            }
            _ship.DeleteBeams(beamsToDelete);

            beamsToDelete = new List<BeamBase>();
            foreach (var beam in _enemyBeams.GetBeams())
            {
                var beamPos = beam.Position;

                if (!(beamPos.Y > 49 & beamPos.Y < 54)) continue;
                if (!_barriers.IsBarrierPartDestroyed(beamPos)) continue;

                beamsToDelete.Add(beam);
                beam.Unrender();
            }

            _enemyBeams.DeleteBeams(beamsToDelete);
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
                _ufo.Render();

                Delay();

                if (_lifes > 0)
                    _enemies.MoveUp();

                _ship.MoveBeams();
                _enemyBeams.Move();
                _ufo.Invoke();

                if (blinkCounter.IsCounting())
                    continue;

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

        private void CheckIfShipDestroyedEnemy()
        {
            var beamsToDelete = new List<BeamBase>();

            var beams = _ship.GetBeams();
            if (beams.Count <= 0)
                return;

            foreach (var beam in beams)
            {
                if (!_enemies.IsDestroyed(beam.Position))
                    continue;

                beamsToDelete.Add(beam);
                _score += 100;
            }

            _ship.DeleteBeams(beamsToDelete);
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

        private static void PrepareConsole()
        {
            Console.SetWindowSize(100, 60);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
        }

        private static void Delay(int delay = DELAY)
        {
            Thread.Sleep(delay);
        }
    }
}