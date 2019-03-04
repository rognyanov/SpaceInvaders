using SpaceInvaders.Models;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            var game = new SpaceInvadersConsoleGame(new ConsoleRenderer());
            game.Play();
        }
    }
}