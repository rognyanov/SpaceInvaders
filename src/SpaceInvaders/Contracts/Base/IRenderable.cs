namespace SpaceInvaders.Contracts.Base
{
    public interface IRenderable
    {
        /// <summary>
        /// Renders the object
        /// </summary>
        void Render();

        /// <summary>
        /// Wipes out the object
        /// </summary>
        void Unrender();
    }
}
