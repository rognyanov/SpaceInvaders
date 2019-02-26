using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts
{
    public interface IPosition
    {
        int X { get; }
        int Y { get; }

        void Move(MoveType type);
        void ReInitialize();
    }
}
