using SpaceInvaders.Models;
using SpaceInvaders.Models.Entities.Enemies;
using SpaceInvaders.Models.Entities.Features;
using SpaceInvaders.Models.Entities.Ship;
using SpaceInvaders.Models.Entities.Visual;
using SpaceInvaders.Models.Grid;

namespace SpaceInvaders
{
    public class Launcher
    {
        public static void Main(string[] args)
        {
            var startLevel = 1;
            var renderer = new ConsoleRenderer();
            var ship = new Ship(new ConsolePosition(45, 57), renderer);
            var enemies = new Enemies(startLevel, renderer);
            var enemyBeams = new EnemyBeams(startLevel, renderer);
            var gameHeader = new GameHeader(renderer);
            var extraLife = new ExtraLife(renderer, gameHeader);
            var ufo = new Ufo(renderer);
            var barriers = new Barriers(renderer);

            var game = new SpaceInvadersConsoleGame(renderer, ship, enemies, enemyBeams, gameHeader, extraLife, ufo, barriers);
            game.Play();
        }
    }
}