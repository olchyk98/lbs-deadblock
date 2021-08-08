using Microsoft.Xna.Framework;
using Deadblock.Logic;
using Deadblock.Engine;
using Deadblock.Extensions;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Generic
{
    public enum DrawPositionMode
    {
        CENTER_TO_POINT,
        BOTTOM_TO_POINT,
        TOP_TO_POINT,
        ORIGINAL_POINT,
    }

    public enum ResizeMode
    {
        ORIGINAL,
        SCALE_X,
        SCALE_Y,
        SCALE_FULL
    }

    public class SpriteBlock : DeliveredGameSlot, ISpriteBlock
    {
        /// <summary>
        /// Decides if the value should have a collider.
        /// If the block has a collider,
        /// entities that are present in the world
        /// are not able to go through the block.
        /// </summary>
        public bool HasCollider { get; } = false;

        /// <summary>
        /// Alpha modifier for the texture.
        /// Value between 0 and 1.
        /// </summary>
        public float Alpha { get; private set; } = 1f;

        protected WorkerTexture TextureSpec { get; private set; }
        protected string CurrentTextureKey { get; private set; } = "Default";

        /// <summary>
        /// Additional number that will be added
        /// in case of full resizing to fulfill
        /// and patch extra gaps and margins between tile-blocks.
        ///
        /// Recommended value according to the documentation is 0.1.
        /// </summary>
        private readonly static float ScaleStabilityValue = .1f;

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
            TextureSpec = gameInstance.GameContents.GetTexture(aTextureKey);

            if (!TextureSpec.Textures.ContainsKey(CurrentTextureKey))
            {
                throw new FormatException($"Loaded texture does not have the default texture. Texture name: { TextureSpec.Name }. Contact DEV.");
            }
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
            const int blockSize = GameGlobals.SCREEN_BLOCK_SIZE;
            var tempTexture = GetActiveTexture();

            int w = tempTexture.Width;
            int h = tempTexture.Height;
            int x = (int)aPosition.X;
            int y = (int)aPosition.Y;

            switch (TextureSpec.DrawPositionMode)
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
        /// Calculates appliable scale for the texture
        /// so it would satisfy specified ResizeMode.
        /// </summary>
        /// <param name="aTexture">
        /// Targeted texture.
        /// </param>
        /// <returns>
        /// Scale modifiers,
        /// represented as 2D Vector.
        /// </returns>
        private Vector2 GetRelativeScale(Texture2D aTexture)
        {
            var tempWidth = aTexture.Width;
            var tempHeight = aTexture.Height;
            var tempBlockSize = GameGlobals.SCREEN_BLOCK_SIZE;

            Func<float> getScaleX = () => tempBlockSize / tempWidth;
            Func<float> getScaleY = () => tempBlockSize / tempHeight;

            switch (TextureSpec.ResizeMode)
            {
                case ResizeMode.SCALE_X:
                    return new Vector2(getScaleX(), 1);
                case ResizeMode.SCALE_Y:
                    return new Vector2(getScaleY(), 1);
                case ResizeMode.SCALE_FULL:
                    return new Vector2(getScaleY() + ScaleStabilityValue, getScaleY() + ScaleStabilityValue);
                default:
                case ResizeMode.ORIGINAL:
                    return Vector2.One;
            }
        }

        /// <summary>
        /// Sets variant key,
        /// that is used to decide which
        /// texture will be rendered.
        /// The default/original value is "Default".
        /// </summary>
        public void SetTextureVariant(string aVariant = "Default")
        {
            if (!TextureSpec.Textures.ContainsKey(aVariant))
            {
                throw new AggregateException($"Tried to set an invalid sprite variant: {aVariant} for texture {TextureSpec.Name}");
            }

            // Prevent an extra observer/sys-call.
            if (CurrentTextureKey == aVariant) return;

            CurrentTextureKey = aVariant;
        }

        /// <summary>
        /// Returns a currently
        /// active texture.
        /// </summary>
        public Texture2D GetActiveTexture()
        {
            return TextureSpec.Textures[CurrentTextureKey];
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
        virtual public void Render(Vector2 aPosition, bool isRelative = true)
        {

            var tempTexture = GetActiveTexture();
            var tempPosition = (isRelative) ? GetRelativePosition(aPosition) : aPosition;
            var tempScale = GetRelativeScale(tempTexture);

            var tempColor = Color.White * Alpha;
            gameInstance.SpriteBatch.Draw(tempTexture,
                    tempPosition,
                    tempColor,
                    scale: tempScale);
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
            var tempRawTexture = GetActiveTexture();
            return new Vector2(tempRawTexture.Width, tempRawTexture.Width);
        }

        /// <summary>
        /// Sets texture alpha,
        /// prevents it to go over 1.
        /// </summary>
        /// <returns>
        /// The new alpha value.
        /// </returns>
        public float SetAlpha(float aValue)
        {
            if (aValue > 1f) Alpha = 1f;
            else if (aValue < 0f) Alpha = 0f;
            else Alpha = aValue;

            return Alpha;
        }
    }
}
