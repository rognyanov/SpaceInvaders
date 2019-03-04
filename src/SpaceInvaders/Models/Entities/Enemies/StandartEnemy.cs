using SpaceInvaders.Contracts.Base;
using SpaceInvaders.Contracts.Visual;
using SpaceInvaders.Models.Entities.Base;
using System;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public sealed class StandartEnemy : EnemyBase
    {
        public StandartEnemy(IPosition position, ConsoleColor color, IRenderer<string> renderer)
            : base(position, color, renderer)
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