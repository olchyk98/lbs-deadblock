using Microsoft.Xna.Framework;
using Logic;
using Deadmind.Engine;

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
        public bool HasCollider { get; } = false;

        public SpriteBlock(GameProcess aGame, string aTextureKey) : base(aGame)
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
        private void LoadTexture(string aTextureKey)
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
        private Vector2 GetRelativePosition(Vector2 aPosition)
        {
            var tempTexture = myTextureSpec.Texture;
            const int blockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            int w = tempTexture.Width;
            int h = tempTexture.Height;
            int x = (int)aPosition.X;
            int y = (int)aPosition.Y;

            switch (myTextureSpec.DrawPositionMode)
            {
                case DrawPositionMode.CENTER_TO_POINT:
                    {
                        var relativeX = x + blockSize / 2 - w / 2;
                        var relativeY = y + blockSize / 2 - y / 2;

                        return new Vector2(relativeX, relativeY);
                    }
                case DrawPositionMode.BOTTOM_TO_POINT:
                    {
                        var relativeX = x + blockSize / 2 - w / 2;
                        var relativeY = y - h + blockSize;

                        return new Vector2(relativeX, relativeY);
                    }
                default:
                case DrawPositionMode.ORIGINAL_POINT:
                    return aPosition;
            }
        }

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
        public void Render(Vector2 aPosition, bool isRelative = true)
        {
            var tempPosition = (isRelative) ? GetRelativePosition(aPosition) : aPosition;
            gameInstance.SpriteBatch.Draw(myTextureSpec.Texture, tempPosition, Color.White);
        }

        /// <summary>
        /// Constructs height and width
        /// of the targeted object.
        /// </summary>
        /// <returns>
        /// Vector of dimensions
        /// for the actor object.
        /// </returns>
        public Vector2 GetDimensions()
        {
            var tempRawTexture = myTextureSpec.Texture;
            return new Vector2(tempRawTexture.Width, tempRawTexture.Width);
        }
    }
}
