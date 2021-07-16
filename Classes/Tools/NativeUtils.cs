using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Randomizes value in the specified range.
        /// Returns the same type as the specified values.
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

        public static int RandomizeValue(int aMin, int aMax)
        {
            var randomInstance = new Random();

            var tempMin = Convert.ToInt32(aMin);
            var tempMax = Convert.ToInt32(aMax);

            return (int)randomInstance.Next(tempMin, tempMax);
        }

        /// <summary>
        /// Spits a string into a dictionary.
        /// For Example:
        ///
        /// "variant: super;" with splitter ':' and anEnd ';'
        /// will be parsed into { variant: super }
        /// </summary>
        public static Dictionary<string, string> SplitStringIntoDict(string aTarget, char aSplitter = ':', char anEnd = ';')
        {
            var tempStorage = new Dictionary<string, string>();
            var entries = aTarget.Split(anEnd).Select((f) => f.Trim());

            foreach (string entry in entries)
            {
                if (string.IsNullOrEmpty(entry)) continue;

                string[] pair = entry.Trim().Split(aSplitter).Select((f) => f.Trim()).ToArray();
                if (pair.Length != 2)
                {
                    throw new AggregateException($"Could not split pair, as it contains more than one separator: { entry }");
                }

                tempStorage.Add(pair[0], pair[1]);
            }

            return tempStorage;
        }
    }
}
