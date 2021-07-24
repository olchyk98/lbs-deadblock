using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Generic
{
    public class FontDrawer : DeliveredGameSlot
    {
        private Dictionary<string, SpriteFont> myFonts;
        // { font name: name specified in content pipeline }
        private static Dictionary<string, string> ActiveFonts = new Dictionary<string, string>()
        {
            { "Arial", "ArialRegular" },
        };

        public FontDrawer(GameProcess aGame) : base(aGame)
        {
            LoadFonts();
        }

        /// <summary>
        /// Loads specified fonts from
        /// the content pipeline to memory.
        /// </summary>
        private void LoadFonts()
        {
            myFonts = new Dictionary<string, SpriteFont>();

            //////////////////

            foreach (var fontPair in ActiveFonts)
            {
                var tempFont = gameInstance.Content.Load<SpriteFont>(fontPair.Value); ;
                myFonts.Add(fontPair.Key, tempFont);
            }
        }

        /// <summary>
        /// Returns the specified font.
        /// </summary>
        /// <param name="aFontName">
        /// Name of the targeted font.
        /// </param>
        /// <returns>
        /// SpriteFont that corresponds to
        /// the specified font naem.
        /// </returns>
        /// <throws>
        /// ArgumentException, if specified
        /// font name is not in the memory.
        /// </throws>
        public SpriteFont PullFont(string aFontName)
        {
            if (!myFonts.ContainsKey(aFontName))
            {
                throw new ArgumentException("Invalid fontName specified while tried to pull a font from FontDrawer. Contact DEV.");
            }

            return myFonts[aFontName];
        }
    }
}
