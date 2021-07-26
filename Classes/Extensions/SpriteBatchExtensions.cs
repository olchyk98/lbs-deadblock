using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Extensions
{
    public static class SpriteBatchExtensions
    {
        private static void DrawBase(SpriteBatch anInstance,
                Texture2D texture,
                Vector2 position,
                Color color,
                Rectangle? sourceRectangle = null,
                float rotation = 0f,
                Vector2 origin = default,
                Vector2 scale = default,
                SpriteEffects effects = default,
                float layerDepth = 1f)
        {
            var tempOrigin = (origin == default) ? Vector2.Zero : origin;
            var tempScale = (scale == default) ? Vector2.One : scale;
            var tempEffects = (effects == default) ? SpriteEffects.None : effects;

            anInstance.Draw(texture: texture,
                    position: position,
                    color: color,
                    rotation: rotation,
                    scale: tempScale,
                    origin: tempOrigin,
                    layerDepth: layerDepth,
                    sourceRectangle: sourceRectangle,
                    effects: tempEffects);
        }

        public static void Draw(this SpriteBatch anInstance,
                Texture2D texture,
                Vector2 position,
                Color color,
                Vector2 scale) => DrawBase(anInstance, texture, position, color, scale: scale);

        public static void Draw(this SpriteBatch anInstance,
                Texture2D texture,
                Vector2 position,
                Color color,
                float layerDepth) => DrawBase(anInstance, texture, position, color, layerDepth: layerDepth);
    }
}
