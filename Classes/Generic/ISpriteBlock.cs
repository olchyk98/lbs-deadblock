using Microsoft.Xna.Framework;

namespace Deadblock.Generic
{
    public interface ISpriteBlock
    {
        /// <summary>
        /// Draws the sprite at the specified position.
        /// </summary>
        /// <param name="aPosition">
        /// Position for the drawing.
        /// </param>
        public void Draw(Vector2 aPosition);
    }
}
