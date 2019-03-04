using SpaceInvaders.Enums;

namespace SpaceInvaders.Contracts.Base
{
    public interface IPosition
    {
        int X { get; }
        int Y { get; }

        /// <summary>
        /// Move the position coordinates based on side (Oxy system)
        /// </summary>
        /// <param name="type"></param>
        void Move(MoveType type);
    }
}
