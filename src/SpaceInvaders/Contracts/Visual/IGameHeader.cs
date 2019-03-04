namespace SpaceInvaders.Contracts.Visual
{
    public interface IGameHeader
    {
        void RenderHeader();
        void RenderStats(int score, int level, int lifes);
        void RenderScore(int score);
        void RenderLevel(int level);
        void RenderLifes(int lifes);
        void UnrenderLifes();
    }
}