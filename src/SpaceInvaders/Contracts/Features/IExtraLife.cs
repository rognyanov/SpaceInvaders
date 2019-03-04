namespace SpaceInvaders.Contracts.Features
{
    public interface IExtraLife
    {
        /// <summary>
        /// Invokes the extra life checker which checks if we have enough score for extra life
        /// </summary>
        /// <param name="score">The current score</param>
        /// <param name="lifes">The current lifes count</param>
        /// <returns>True if extra life is given.</returns>
        bool Invoke(int score, int lifes);
    }
}
