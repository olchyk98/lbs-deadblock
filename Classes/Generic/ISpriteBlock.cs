using Microsoft.Xna.Framework;

namespace Deadblock.Generic
{
    public interface ISpriteBlock
    {
        /// <summary>
        /// Draws object on the screen
        /// on the specified position.
        /// Position is transformed with
        /// the specified drawMode before using.
        /// </summary>
        /// <param name="aPosition">
        /// Requested Position.
        /// To use raw version of this value,
        /// specify isRelative to false.
        /// </param>
        /// <param name="isRelative">
        /// When to true,
        /// the specified position will be processed
        /// with the specified drawMode converter.
        /// DrawMode is usually specified via assets config.
        /// </param>
        public void Draw(Vector2 aPosition, bool isRelative = true);

        /// <summary>
        /// Constructs height and width
        /// of the targeted object.
        /// </summary>
        /// <returns>
        /// Vector of dimensions
        /// for the actor object.
        /// </returns>
        public Vector2 GetDimensions();
    }
}
