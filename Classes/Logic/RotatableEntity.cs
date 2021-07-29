namespace Deadblock.Logic
{
    /// <summary>
    /// Handles model rotation using direction
    /// value inherited from entity.
    ///
    /// The used entity should include all the rotation variants:
    /// "Right", "Left", "Down", "Up".
    /// </summary>
    public abstract class RototableEntity : DrawableEntity
    {
        public RototableEntity(GameProcess aGame,
                string aTextureName,
                float someHealth) : base(aGame, aTextureName, someHealth)
        { }

        /// <summary>
        /// Sets entity sprite according
        /// to the movement direction.
        /// </summary>
        protected void RotateSprite()
        {
            if (Direction.X == 1)
            {
                SetSpriteVariant("Right");
                return;
            }

            if (Direction.X == -1)
            {
                SetSpriteVariant("Left");
                return;
            }

            if (Direction.Y == 1)
            {
                SetSpriteVariant("Down");
                return;
            }

            if (Direction.Y == -1)
            {
                SetSpriteVariant("Up");
                return;
            }
        }

        override public void Update()
        {
            RotateSprite();
            base.Update();
        }
    }
}
