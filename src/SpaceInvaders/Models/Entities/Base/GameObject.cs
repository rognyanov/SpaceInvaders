using SpaceInvaders.Contracts;

namespace SpaceInvaders.Models.Entities.Base
{
    public abstract class GameObject : IRenderable
    {
        public IPosition Position { get; protected set; }

        protected GameObject(IPosition position)
        {
            Position = position;
        }

        public abstract void Render();
        public abstract void Unrender();
    }
}
