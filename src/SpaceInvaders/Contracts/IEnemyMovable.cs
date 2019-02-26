using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts
{
    public interface IEnemyMovable
    {
        void Move(MoveType moveType, bool increaseY);
    }
}