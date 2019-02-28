﻿using SpaceInvaders.Contracts;
using System;

namespace SpaceInvaders.Models.Entities.Visual
{
    public class GameHeader
    {
        public const int SCORE_X = 8;
        public const int SCORE_Y = 2;

        private readonly IRenderer<string> _renderer;

        public GameHeader(IRenderer<string> renderer)
        {
            _renderer = renderer;
        }

        public void Render()
        {
            _renderer.SetColor(ConsoleColor.Yellow);
            _renderer.DrawAtPosition(25, 2, @"___  ___   _  ___  __    .            _  __   __ ___  ___ ");
            _renderer.DrawAtPosition(25, 3, @"|__  |__| |_| |   |_     | |\ | |  | |_| | | |_  |__| |__ ");
            _renderer.DrawAtPosition(25, 4, @" __| |    | | |__ |__    | | \|  \/  | | |_| |__ |\    __|");

            _renderer.SetColor(ConsoleColor.Red);

            for (int i = 0; i < 100; i++)
            {
                _renderer.DrawAtPosition(i, 1, "=");
            }

            for (int i = 0; i < 100; i++)
            {
                _renderer.DrawAtPosition(i, 6, "=");
            }

            _renderer.SetColor(ConsoleColor.Cyan);
            _renderer.DrawAtPosition(1, 2, "Score: ");
            _renderer.DrawAtPosition(1, 3, "Level: ");
            _renderer.DrawAtPosition(1, 5, "Lifes: ");

            _renderer.SetColor(ConsoleColor.White);
            RenderScore(0);
            RenderLevel(1);
            RenderLifes(3);
        }

        public void RenderStats(int score, int level, int lifes)
        {
            RenderScore(score);
            RenderLevel(level);
            RenderLifes(lifes);
        }

        public void RenderScore(int score)
        {
            RenderNumber(score, 7, SCORE_X, SCORE_Y);
        }

        public void RenderLevel(int level)
        {
            RenderNumber(level, 2, SCORE_X, SCORE_Y + 1);
        }

        public void UnrenderLevel()
        {
            _renderer.DrawAtPosition(SCORE_X, SCORE_Y+1, "  ");
        }

        public void RenderLifes(int lifes)
        {
            RenderNumber(lifes, 2, SCORE_X, SCORE_Y + 3);
        }

        public void UnrenderLifes()
        {
            _renderer.DrawAtPosition(SCORE_X, SCORE_Y + 3, "  ");
        }

        private void RenderNumber(int value, int format, int x, int y)
        {
            var number = value.ToString();

            _renderer.SetColor(ConsoleColor.White);

            int index = 0;

            if (number.Length < format)
            {
                for (int i = 0; i < format - number.Length; i++)
                {
                    index = x + i;
                    RenderDigit(0, x + i, y);
                }
            }
            foreach (var c in number)
            {
                RenderDigit(int.Parse(c.ToString()), index++, y);
            }
        }

        private void RenderDigit(int value, int x, int y)
        {
            _renderer.DrawAtPosition(x, y, value.ToString());
        }
    }
}
