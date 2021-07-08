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
        public static Vector2 GetScreenResolution(GameProcess aGame)
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
        public static Vector2 GetScreenCenterPosition(GameProcess aGame)
        {
            var tempScreenResolution = GetScreenResolution(aGame);
            return new Vector2(tempScreenResolution.X / 2, tempScreenResolution.Y / 2);
        }

        /// <summary>
        /// Returns true if specified
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
        /// False if out of the screen.
        /// </returns>
        public static bool IsPointOnCanvas(GameProcess aGame, Vector2 aPosition)
        {
            var tempScreenSize = GetScreenResolution(aGame);

            if (aPosition.X < 0 || aPosition.X > tempScreenSize.X)
                return false;

            if (aPosition.Y < 0 || aPosition.Y > tempScreenSize.Y)
                return false;

            return true;
        }

        public static float RandomizeValue<T>(T aMin, T aMax)
        {
            var randomInstance = new Random();

            if (typeof(T) == typeof(int))
            {
                var tempMin = Convert.ToInt32(aMin);
                var tempMax = Convert.ToInt32(aMax);
                return (int)randomInstance.Next(tempMin, tempMax);
            }

            if (typeof(T) == typeof(float))
            {
                var tempMin = (float)Convert.ToDouble(aMin);
                var tempMax = (float)Convert.ToDouble(aMax);
                var tempSample = (float)randomInstance.NextDouble();
                return (tempSample * tempMax) + tempMin;
            }

            return default;
        }
    }
}
