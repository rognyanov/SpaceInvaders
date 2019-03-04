using SpaceInvaders.Contracts.Visual;
using System;

namespace SpaceInvaders.Models.Grid
{
    public sealed class ConsoleRenderer : IRenderer<string>
    {
        private ConsoleColor _color;

        public void DrawAtPosition(int x, int y, string element)
        {
            Console.ForegroundColor = _color;
            SetCursorAt(x, y);
            Console.Write(element);
        }

        public void SetColor(ConsoleColor color)
        {
            _color = color;
        }

        private static void SetCursorAt(int x, int y)
        {
            Console.SetCursorPosition(x,y);
        }
    }
}
