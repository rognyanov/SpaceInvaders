using SpaceInvaders.Contracts;
using SpaceInvaders.Models.Entities.Base;
using System;

namespace SpaceInvaders.Models.Entities.Enemies
{
    public class AdvancedEnemy : EnemyBase
    {
        public AdvancedEnemy(int x, int y, ConsoleColor color, IRenderer<string> renderer)
            : base(x, y, color, renderer)
        {
            _image = new string[3]
            {
                @"  |_|  ",
                @" |x.x| ",
                @" /-^-\ "
            };

            _secondImage = new string[3]
            {
                @"  \_/  ",
                @" |x.x| ",
                @" /'|'\  "
            };
        }
    }
}
