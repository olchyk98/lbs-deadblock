using Microsoft.Xna.Framework;
using Logic;
using Deadmind.Engine;
using System;

namespace Deadblock.Generic
{
    public enum DrawPositionMode
    {
        CENTER_TO_POINT,
        BOTTOM_TO_POINT,
        TOP_TO_POINT,
        ORIGINAL_POINT,
    }

    public class SpriteBlock : DeliveredGameSlot, ISpriteBlock
    {
        private WorkerTexture myTextureSpec;

        public SpriteBlock (GameProcess aGame, string aTextureKey) : base(aGame)
        {
            LoadTexture(aTextureKey);
        }

        /// <summary>
        /// Loads texture from
        /// the active content worker.
        /// </summary>
        /// <param name="aTextureKey">
        /// Key of the requested texture.
        /// </param>
        private void LoadTexture (string aTextureKey)
        {
            myTextureSpec = gameInstance.GameContents.GetTexture(aTextureKey);
        }

        /// <summary>
        /// Calculates draw position using
        /// texture dimensions and relative setting value.
        ///
        /// May be used to, for example, place sprite
        /// in the center of a block.
        /// </summary>
        /// <param name="aPosition">
        /// Requested position.
        /// </param>
        /// <returns>
        /// Calibrated position.
        /// </returns>
        private Vector2 GetRelativePosition (Vector2 aPosition)
        {
            var tempTexture = myTextureSpec.Texture;
            const int blockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            int w = tempTexture.Width;
            int h = tempTexture.Height;
            int x = (int) aPosition.X;
            int y = (int) aPosition.Y;

            switch (myTextureSpec.DrawPositionMode)
            {
                default:
                case DrawPositionMode.CENTER_TO_POINT:
                    var relativeX = x + blockSize / 2 - w / 2;
                    var relativeY = y + blockSize / 2 - y / 2;

                    return new Vector2(relativeX, relativeY);
                case DrawPositionMode.ORIGINAL_POINT:
                    return aPosition;
            }
        }

        public void Draw (Vector2 aPosition)
        {
            var tempPosition = GetRelativePosition(aPosition);
            gameInstance.SpriteBatch.Draw(myTextureSpec.Texture, tempPosition, Color.White);
        }
    }
}
