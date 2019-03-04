using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts.Base
{
    public interface IPosition
    {
        int X { get; }
        int Y { get; }

        void Move(MoveType type);
    }
}
