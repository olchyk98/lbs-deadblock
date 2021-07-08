using Deadblock.Generic;
using Microsoft.Xna.Framework;

namespace Deadblock.Tools
{
    public class EngineUtils
    {
        /// <summary>
        /// Checks if there are any blocks
        /// with collider on the position,
        /// by info about blocks from the world instance.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game instance.
        /// </param>
        /// <param name="aPosition">
        /// Targeted position of a
        /// potential obstacle.
        /// </param>
        /// <returns>
        /// Boolean that represents
        /// if there's a collider at
        /// the specified position.
        /// </returns>
        public static bool IsTouchingCollider (GameProcess aGame, Vector2 aPosition)
        {
            var tempBlocks = aGame.World.GetBlocksOnPosition(aPosition);

            foreach(ISpriteBlock block in tempBlocks)
            {
                if(block.HasCollider) return true;
            }

            return false;
        }
    }
}
