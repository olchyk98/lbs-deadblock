using Deadblock.Generic;
using Deadblock.Extensions;
using Deadblock.Logic;
using Deadblock.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Deadblock.Engine
{
    public abstract class HeadBar : DeliveredGameSlot
    {
        protected DrawableEntity TargetEntity { get; private set; }
        protected Texture2D Rectangle { get; private set; }

        protected readonly static int RegularWidth = 80;

        public HeadBar(GameProcess aGame, DrawableEntity anEntity, int someHeight, Color aColor) : base(aGame)
        {
            TargetEntity = anEntity;
            SetupTexture(someHeight, aColor);
        }

        /// <summary>
        /// Setups a new texture for the bar.
        /// </summary>
        /// <param name="someHeight">
        /// Height for the texturee.
        /// Width will be taken from the default value.
        /// </param>
        /// <param name="aColor">
        /// Color of the bar.
        /// </param>
        private void SetupTexture(int someHeight, Color aColor)
        {
            var tempSize = new Vector2(RegularWidth, someHeight);
            Rectangle = NativeUtils.ConstructRectangle(gameInstance, tempSize, aColor);
        }

        /// <summary>
        /// Draws bar on the canvas.
        /// </summary>
        /// <param name="someGapY">
        /// The value that represents the distance
        /// between the head (Position position) of the entity,
        /// and the bar.
        /// </param>
        virtual public void Draw(float someGapY)
        {
            Draw(someGapY, 1f);
        }

        /// <summary>
        /// Draws bar on the canvas.
        /// </summary>
        /// <param name="someGapY">
        /// The value that represents the distance
        /// between the head (Position position) of the entity,
        /// and the bar.
        /// </param>
        /// <param name="aValue">
        /// Float coef between 0 and 1,
        /// that represents how much the bar should be filled.
        /// </param>
        virtual public void Draw(float someGapY, float aValue)
        {
            var tempEntitySize = TargetEntity.Texture.GetDimensions();
            var tempEntityPosition = TargetEntity.Position;

            var tempPosition = new Vector2(tempEntityPosition.X + tempEntitySize.X / 2 - Rectangle.Width / 2,
                tempEntityPosition.Y - someGapY);

            var tempScale = new Vector2(aValue, 1);

            gameInstance.SpriteBatch.Draw(Rectangle,
                tempPosition,
                Color.White,
                scale: tempScale);
        }

        /// <summary>
        /// Returns bar dimensions.
        /// </summary>
        public Vector2 GetDimensions()
        {
            return new Vector2(Rectangle.Width, Rectangle.Height);
        }
    }
}
