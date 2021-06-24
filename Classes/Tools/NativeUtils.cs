using System;
using Microsoft.Xna.Framework;

namespace Deadblock.Tools
{
    public static class NativeUtils
    {
        /// <summary>
        /// Returns current screen resolution.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game process.
        /// </param>
        /// <returns>
        /// Display Mode Vector2 that contains
        /// Height and Width of the targeted window.
        /// </returns>
        public static Vector2 GetScreenResolution (GameProcess aGame)
        {
            var tempDisplayMode = aGame.GraphicsDevice.Viewport;
            return new Vector2(tempDisplayMode.Width, tempDisplayMode.Height);
        }

        /// <summary>
        /// Returns point of the center of the screen.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game process.
        /// </param>
        /// <returns>
        /// Vector of the center
        /// of the targeted screen.
        /// </returns>
        public static Vector2 GetScreenCenterPosition (GameProcess aGame)
        {
            var tempScreenResolution = GetScreenResolution(aGame);
            return new Vector2(tempScreenResolution.X / 2, tempScreenResolution.Y / 2);
        }

        /// <summary>
        /// Throws an error if specified
        /// point is out of the screen.
        /// </summary>
        /// <param name="aGame">
        /// Targeted game process.
        /// </param>
        /// <param name="aPosition">
        /// The point that needs to be validated.
        /// </param>
        /// <returns>
        /// True if the point is in bounds.
        /// Throws an error if the point is not valid or of bounds.
        /// </returns>
        public static bool ValidateIfOutOfScreen (GameProcess aGame, Vector2 aPosition)
        {
            var tempScreenSize = GetScreenResolution(aGame);

            if(aPosition.X < 0 || aPosition.X > tempScreenSize.X)
            {
                throw new AggregateException("Position is out of horizontal boundaries. Contact DEV.");
            }

            if(aPosition.Y < 0 || aPosition.Y > tempScreenSize.Y)
            {
                throw new AggregateException("Position is out of vertical boundaries. Contact DEV.");
            }

            return true;
        }
    }
}
