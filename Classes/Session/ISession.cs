namespace Deadblock.Session
{
    public interface ISession
    {
        /// <summary>
        /// Called on each frame.
        /// Supposed to draw objects,
        /// or force available abstracted instances/
        /// subsystems/subinstances to draw
        /// available objects.
        /// </summary>
        public void Draw();

        /// <summary>
        /// Called on each frame,
        /// before draw.
        /// Supposed to update internal/external
        /// mechanical values for available
        /// instances, when needed.
        /// </summary>
        public void Update();

        /// <summary>
        /// Setups all the necessary instances
        /// for the session.
        /// Will be called once, before
        /// the game loop starts executing.
        /// </summary>
        public void Initialize();
    }
}
