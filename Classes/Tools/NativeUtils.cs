using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        /// Randomizes value in the specified range.
        /// </summary>
        /// <param name="aMin">
        /// Minimal boundary.
        /// </param>
        /// <param name="aMax">
        /// Maximal boundary.
        /// </param>
        /// <returns>
        /// Randomized value in boundaries.
        /// </returns>
        public static float RandomizeValue(float aMin, float aMax)
        {
            var randomInstance = new Random();

            var tempMin = (float)Convert.ToDouble(aMin);
            var tempMax = (float)Convert.ToDouble(aMax);
            var tempSample = (float)randomInstance.NextDouble();

            return (tempSample * tempMax) + tempMin;
        }

        /// <summary>
        /// Randomizes value in the specified range.
        /// </summary>
        /// <param name="aMin">
        /// Minimal boundary.
        /// </param>
        /// <param name="aMax">
        /// Maximal boundary.
        /// </param>
        /// <returns>
        /// Randomized value in boundaries.
        /// </returns>
        public static int RandomizeValue(int aMin, int aMax)
        {
            var randomInstance = new Random();

            var tempMin = Convert.ToInt32(aMin);
            var tempMax = Convert.ToInt32(aMax);

            return (int)randomInstance.Next(tempMin, tempMax);
        }

        /// <summary>
        /// Returns a random element from
        /// the passed array.
        /// </summary>
        /// <param name="anArr">
        /// Targeted array.
        /// </param>
        /// <returns>
        /// Random element from the array.
        /// </returns>
        public static T Choice<T>(T[] anArr)
        {
            var tempRandomIndex = RandomizeValue(0, anArr.Length);
            return anArr[tempRandomIndex];
        }

        /// <summary>
        /// Returns a random element from
        /// the passed list.
        /// </summary>
        /// <param name="aList">
        /// Targeted list.
        /// </param>
        /// <returns>
        /// Random element from the list.
        /// </returns>
        public static T Choice<T>(List<T> aList)
        {
            var tempRandomIndex = RandomizeValue(0, aList.Count);
            return aList[tempRandomIndex];
        }

        /// <summary>
        /// Checks the specified boolean,
        /// and returns 1 if boolean is true
        /// and 0 if boolean is false.
        /// </summary>
        public static int ParseBooleanToDirection(bool aTarget)
        {
            return aTarget ? 1 : -1;
        }

        /// <summary>
        /// Returns current timestamp,
        /// represented in unix-time.
        /// </summary>
        public static long GetTime()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Creates texture of a rectangle.
        /// </summary>
        /// <param name="aGame">
        /// Instance of the targeted game process.
        /// </param>
        /// <param name="someDimensions">
        /// Dimensions of the rectangle texture.
        /// </param>
        /// <param name="aColor">
        /// Requested color for the texture.
        /// </param>
        /// <returns>
        /// Texture2D with custom-filled pixels,
        /// based on the specified color and dimensions.
        /// </returns>
        public static Texture2D ConstructRectangle(GameProcess aGame, Vector2 someDimensions, Color aColor)
        {
            var tempWidth = (int)someDimensions.X;
            var tempHeight = (int)someDimensions.Y;

            Color[] pixelsData = new Color[tempWidth * tempHeight];
            Texture2D rectTexture = new Texture2D(aGame.GraphicsDevice, tempWidth, tempHeight);

            for (var ma = 0; ma < pixelsData.Length; ++ma)
                pixelsData[ma] = aColor;

            rectTexture.SetData(pixelsData);
            return rectTexture;
        }

        /// <summary>
        /// Returns position of a random point
        /// on the screen, represented as 2d vector.
        /// </summary>
        public static Vector2 RandomizeScreenPosition(GameProcess aGame)
        {
            var tempScreenResolution = GetScreenResolution(aGame);

            var tempRandomX = RandomizeValue(0, tempScreenResolution.X);
            var tempRandomY = RandomizeValue(0, tempScreenResolution.Y);

            return new Vector2(tempRandomX, tempRandomY);
        }
    }
}
