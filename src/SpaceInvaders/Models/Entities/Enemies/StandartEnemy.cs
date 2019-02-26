using System;
using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class StandartEnemy : EnemyBase
    {
        public StandartEnemy(int x, int y, ConsoleColor color, IRenderer<string> renderer)
            : base(x, y, color, renderer)
        {
            _image = new string[3]
            {
                @"  _ _  ",
                @" |~.~| ",
                @" ^^^^^ "
            };

            _secondImage = new string[3]
            {
                @"  \_/  ",
                @" |~.~| ",
                @" ^^^^^ "
            };
        }
    }
}