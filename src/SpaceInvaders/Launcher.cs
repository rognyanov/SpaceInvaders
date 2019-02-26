using SpaceInvaders.Models;

namespace SpaceInvaders
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var game = new SpaceInvadersConsoleGame();
            game.PrepareConsole();
            game.Play();
        }
    }
}
