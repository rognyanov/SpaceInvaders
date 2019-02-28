﻿using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Entities.Visual;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using SpaceInvaders.Models.Grid;
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

                //Is level completed
                if (IsLevelCompleted())
                {
                    LevelCompleted();
                    //Init next level
                }

                //Check if ship destroyed enemy and increase score
                CheckIfShipDestroyedEnemy();

                //Check if ship is destroyed
                if (CheckIfEnemeyDestroyedShip())
                {
                    ShipDestroyed();
                    _isGameOver = _lifes == 0;
                }

                //Short delay
                Delay();

                //Read player input
                _ship.ReadInput();

                //Move entities
                Move();
            }

            GameOver();
        }

        private void LevelCompleted()
        {
            var counter = new Timer(200);
            var scoreCounter = new Timer(8);
            var levelBlinker = new TextBlinker(new ConsolePosition(43,22), _renderer, "L E V E L   C O M P L E T E D!" );
            var bonusBlinker = new TextBlinker(new ConsolePosition(45,24), _renderer, "Level bonus - 1000 points." );
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
            Delay(800);

            _renderer.DrawAtPosition(43, 22, "                              ");
            _renderer.DrawAtPosition(45, 24, "                          ");
        }

        private void GameOver()
        {
            _renderer.DrawAtPosition(42,15,"G A M E   O V E R!");
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
                _ship.RenderBeams(); 
                Delay();

                if (_lifes>0)
                _enemies.MoveUp();

                _ship.MoveBeams();

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
