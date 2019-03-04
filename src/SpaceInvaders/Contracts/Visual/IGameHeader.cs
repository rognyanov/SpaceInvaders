namespace SpaceInvaders.Contracts.Visual
{
    public interface IGameHeader
    {
        /// <summary>
        /// Renders the game header
        /// </summary>
        void RenderHeader();

        /// <summary>
        /// Renders the score, level and lifes
        /// </summary>
        /// <param name="score">The current score</param>
        /// <param name="level">The current level</param>
        /// <param name="lifes">The lives count</param>
        void RenderStats(int score, int level, int lifes);

        /// <summary>
        /// Render the score
        /// </summary>
        /// <param name="score">The score to render</param>
        void RenderScore(int score);

        /// <summary>
        /// Render the level
        /// </summary>
        /// <param name="level">The level to render</param>
        void RenderLevel(int level);

        /// <summary>
        /// Render the lives
        /// </summary>
        /// <param name="lifes">The lives to render</param>
        void RenderLifes(int lifes);

        /// <summary>
        /// Unrender the lives;
        /// </summary>
        void UnrenderLifes();
    }
}