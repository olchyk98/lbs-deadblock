using System;
using Deadblock.Tools;
using Microsoft.Xna.Framework;

namespace Deadblock.Generic
{
    public class AnimatedSpriteBlock : SpriteBlock
    {
        /// <summary>
        /// Time when the last frame
        /// was switched.
        /// </summary>
        private long myLastUpdateTime;

        /// <summary>
        /// Times that needs to pass between
        /// frames switch.
        /// </summary>
        private int myTimePerFrame;

        public AnimatedSpriteBlock(GameProcess aGame, string aTextureKey, int someTimePerFrame = 500) : base(aGame, aTextureKey)
        {
            myLastUpdateTime = NativeUtils.GetTime();
            myTimePerFrame = someTimePerFrame;
        }

        /// <summary>
        /// Sets activeTexture to the next frame
        /// in the animation chain.
        /// </summary>
        private void SwitchFrame()
        {
            var tempFrames = TextureSpec.GetAvailableTextureKeys();
            var tempIndex = Array.IndexOf(tempFrames, CurrentTextureKey);

            // Checks if next index is higher than the length
            var tempNextIndex = (tempIndex + 1 >= tempFrames.Length)
                ? 0 : tempIndex + 1;

            var tempNextFrame = tempFrames[tempNextIndex];

            SetTextureVariant(tempNextFrame);
        }

        /// <summary>
        /// Switches texture to the next variant texture,
        /// when it's needed.
        /// </summary>
        private void UpdateFrame()
        {
            var tempCurrentTime = NativeUtils.GetTime();
            if (tempCurrentTime < myLastUpdateTime + myTimePerFrame) return;

            myLastUpdateTime = tempCurrentTime;
            SwitchFrame();

        }

        /// <summary>
        /// Updates frame on every render tick.
        /// </summary>
        override public void Render(Vector2 aPosition, bool isRelative = true)
        {
            UpdateFrame();
            base.Render(aPosition, isRelative);
        }
    }
}
