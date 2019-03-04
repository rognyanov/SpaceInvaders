using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemyMovable
    {
        void Move(MoveType moveType, bool increaseY);
        void DecreaseY();
    }
}