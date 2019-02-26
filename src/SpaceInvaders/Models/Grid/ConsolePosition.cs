using SpaceInvaders.Contracts;
using SpaceInvaders.Enums;

namespace SpaceInvaders.Models.Grid
{
    public class ConsolePosition : IPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public ConsolePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(MoveType type)
        {
            switch (type)
            {
                case MoveType.None:
                    break;
                case MoveType.Left:
                    MoveLeft();
                    break;
                case MoveType.Right:
                    MoveRight();
                    break;
                case MoveType.Up:
                    MoveUp();
                    break;
                case MoveType.Down:
                    MoveDown();
                    break;
                default:
                    break;
            }
        }

        public void ReInitialize()
        {
            throw new System.NotImplementedException();
        }

        private void MoveLeft() => X--;
        private void MoveRight() => X++;
        private void MoveUp() => Y--;
        private void MoveDown() => Y++;
    }
}
