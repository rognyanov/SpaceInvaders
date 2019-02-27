using SpaceInvaders.Models;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var game = new SpaceInvadersConsoleGame(new ConsoleRenderer());
            game.Play();
        }
    }
}
