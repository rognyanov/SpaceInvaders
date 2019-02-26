using System;

namespace SpaceInvaders.Contracts
{
    public interface IRenderer<TElement>
    {
        void SetColor(ConsoleColor color);
        void DrawAtPosition(int x, int y, TElement element);
    }
}
