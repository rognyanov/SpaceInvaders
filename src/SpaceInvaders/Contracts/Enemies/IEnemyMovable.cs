using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts.Enemies
{
    public interface IEnemyMovable
    {
        /// <summary>
        /// Moves the enemy in the given direction and moves it down if flag's set to true
        /// </summary>
        /// <param name="moveType">The move direction</param>
        /// <param name="increaseY">Whether we should move the enemies down</param>
        void Move(MoveType moveType, bool increaseY);

        /// <summary>
        /// Moves the enemies up.
        /// </summary>
        void DecreaseY();
    }
}