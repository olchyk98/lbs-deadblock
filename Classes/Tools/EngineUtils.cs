using Deadblock.Generic;
using Microsoft.Xna.Framework;

namespace Deadblock.Tools
{
    public static class EngineUtils
    {
        private readonly static string ScoreLogFile = @"./scores.txt";

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
        public static bool IsTouchingCollider(GameProcess aGame, Vector2 aPosition)
        {
            var tempBlocks = aGame.World.GetBlocksOnPosition(aPosition);

            foreach (ISpriteBlock block in tempBlocks)
            {
                if (block.HasCollider) return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the specified position with dimensions
        /// is in the described rectangle.
        ///
        /// Similar to Texture.Intersects(Texture),
        /// except it's generic and works with vectors.
        /// </summary>
        /// <param name="aPosition1">
        /// Position of the
        /// first testing object.
        /// </param>
        /// <param name="aPosition2">
        /// Position of the
        /// second testing object.
        /// </param>
        /// <param name="someDimensions2">
        /// Dimensions of the
        /// second testing object.
        /// </param>
        /// <returns>
        /// Boolean that represents if
        /// the first object intersects
        /// with the second one.
        /// </returns>
        public static bool CheckIfPointIsInRectangle(Vector2 aPosition1,
                Vector2 aPosition2,
                Vector2 someDimensions2)
        {
            var tempDoOverlapOnX = aPosition1.X > aPosition2.X
                && aPosition1.X < aPosition2.X + someDimensions2.X;

            var tempDoOverlapOnY = aPosition1.Y > aPosition2.Y
                && aPosition1.Y < aPosition2.Y + someDimensions2.Y;

            return tempDoOverlapOnX && tempDoOverlapOnY;
        }

        /// <summary>
        /// Saves score with current
        /// timestamp to the log file.
        /// </summary>
        /// <param name="aScore">
        /// Number of points that needs
        /// to be written to the log file.
        /// </param>
        public static void ReportScore(int aScore)
        {
            long tempTime = NativeUtils.GetTime();

            string tempPayload = $"{tempTime} -> score: {aScore}";
            FileUtils.AppendToFile(tempPayload, ScoreLogFile);
        }
    }
}
