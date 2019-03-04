using System;

namespace SpaceInvaders.Contracts.Visual
{
    public interface IRenderer<TElement>
    {
        /// <summary>
        /// Set the color for drawing
        /// </summary>
        /// <param name="color">The picked color</param>
        void SetColor(ConsoleColor color);

        /// <summary>
        /// Renders the element at the given coords with the set color
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        /// <param name="element">Element to draw</param>
        void DrawAtPosition(int x, int y, TElement element);
    }
}
